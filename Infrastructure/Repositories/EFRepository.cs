using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using Domain.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repositories
{
    public class EFRepository<T, Tid> : IRepository<T, Tid> where T : class
    {

        private readonly DbContext _context;

        private readonly DbSet<T> _entities;

        public EFRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Tid id) => (await _entities.FindAsync(id))!;

        public async Task<IEnumerable<T>> GetAsync() => await _entities.ToListAsync();

        public async Task<T> AddAsync(T entity) => (await _entities.AddAsync(entity)).Entity;

        public virtual void DeleteRange(IEnumerable<T> entityToDelete)
        {
            if (entityToDelete != null && entityToDelete.Any())
            {
                if (_context.Entry(entityToDelete.First()).State == EntityState.Detached)
                {
                    _entities.AttachRange(entityToDelete);
                }
                _entities.RemoveRange(entityToDelete);
            }
        }

        /// <summary>
        /// Extra Methods for SPS, for the implementation Use Another Interface, 
        /// Inherith the Generic Repository and Implement all this methods
        /// </summary>
        /// <returns></returns>
        #region ExtraMethodsWithSpAndQueryable
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "") => await BuildQuery(filter, includeProperties).AsNoTracking().ToListAsync();
        public async Task<IEnumerable<T>> GetAsync(string SPName) => await ExecuteStoreProcedure($"{SPName}", null);
        public async Task<IEnumerable<T>> GetAsync(string SPName, object parameters) => await ExecuteStoreProcedure($"{SPName}", GetSqlParamete(parameters));
        private SqlParameter[] GetSqlParamete(object parameters) => parameters.GetType().GetProperties().Select(p => 
        new SqlParameter(){ParameterName = "@" + p.Name,Value = parameters.GetType().GetProperty(p.Name)!.GetValue(parameters),Direction = ParameterDirection.Input}).ToArray();
        private async Task<IEnumerable<T>> ExecuteStoreProcedure(string procedureName, SqlParameter[]? sqlParams)
        {
            var connection = _context.Database.GetDbConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                if(sqlParams != null)
                {
                    command.Parameters.AddRange(sqlParams);
                }
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                var result = DataReaderMapToList(reader);
                connection.Close();
                return result;
            }
        }
        private IQueryable<T> BuildQuery(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _entities;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) query = query.Include(includeProperty);

            return query;
        }
        private static List<T> DataReaderMapToList(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T)!;
            List<string> nombresCampos = dr.GetSchemaTable()!.AsEnumerable().Select(r => r.Field<string>("ColumnName")).ToList()!;
            var properties = obj.GetType().GetProperties();
            if(properties != null&& properties.Length > 0)
            {
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in properties)
                    {
                        if (nombresCampos.Contains(prop.Name))
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                                prop.SetValue(obj, dr[prop.Name], null);
                            continue;
                        }
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

        #endregion ExtraMethodsWithSpAndQueryable
        public async Task DeleteAsync(Tid id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _entities.Attach(entity);
                }
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PagedResult<T>> GetAsync(PagedCriteria criteria)
        {
            IQueryable<T> query = _entities;

            if (!string.IsNullOrEmpty(criteria.SortBy))
            {
                var param = Expression.Parameter(typeof(T), "p");
                var sortProperty = Expression.Property(param, criteria.SortBy);
                var sortLambda = Expression.Lambda(sortProperty, param);
                var orderByMethod = criteria.SortDirection == "ASC" ? "OrderBy" : "OrderByDescending";
                var orderBy = typeof(Queryable).GetMethods().Single(
                    method => method.Name == orderByMethod
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), sortProperty.Type);
                query = (IOrderedQueryable<T>) orderBy.Invoke(null, new object[] { query, sortLambda })!;
            }

            // Apply pagination
            var total = await query.CountAsync();
            var results = await query
                .Skip((criteria.PageNumber - 1) * criteria.PageSize)
                .Take(criteria.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Results = results,
                Total = total
            };
        }
    }
}
