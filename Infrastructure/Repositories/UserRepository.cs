using Dapper;
using Domain.DTOs.User;
using Domain.Interfaces.Repositories;
using Domain.Utils;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository<T, Tid> : IUserRepository<T, Tid> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public UserRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public async Task<T> AddAsync(T entity)
        {
            var properties = entity.GetType().GetProperties().Where(p => p.Name.ToLower() != "id");
            var sql = $"INSERT INTO [dbo].[{typeof(T).Name}] ({string.Join(",", properties.Select(p => p.Name))}) " +
                  $"VALUES (@{string.Join(",@", properties.Select(p => p.Name))}); " +
                  $"SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _connection.QuerySingleAsync<int>(sql, entity, transaction: _transaction);

            var idProperty = entity.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == "id");
            if (idProperty != null)
            {
                idProperty.SetValue(entity, id);
            }

            return entity;
        }

        public async Task DeleteAsync(Tid id)
        {
            var sql = $"DELETE FROM [dbo].[{typeof(T).Name}] WHERE Id = @id";
            await _connection.ExecuteAsync(sql, new { id }, transaction: _transaction);
        }

        public async Task<PagedResult<T>> GetAsync(PagedCriteria criteria)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@$"SELECT * FROM [dbo].[{typeof(T).Name}] ");
            if (!string.IsNullOrEmpty(criteria.SortBy))
                sb.Append($"ORDER BY @SortBy @SortDirection ");
            else
                sb.Append($"ORDER BY Id Asc ");
            sb.Append($"OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; ");
            sb.Append($"SELECT COUNT(*) FROM [dbo].[{typeof(T).Name}];");

            var parameters = new
            {
                SortBy = criteria.SortBy,
                SortDirection = criteria.SortDirection,
                Offset = (criteria.PageNumber - 1) * criteria.PageSize,
                PageSize = criteria.PageSize
            };

            using var multi = _connection.QueryMultiple(sb.ToString(), parameters, transaction: _transaction);
            var values = await multi.ReadAsync<T>();
            var total = multi.ReadSingle<int>();

            return new PagedResult<T>
            {
                Results = values,
                Total = total
            };
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var sql = $"SELECT * FROM [dbo].[{typeof(T).Name}]";
            return await _connection.QueryAsync<T>(sql, transaction: _transaction);
        }

        public async Task<T> GetByIdAsync(Tid id)
        {
            var sql = $"SELECT * FROM [dbo].[{typeof(T).Name}] WHERE Id = @id";
            return await _connection.QuerySingleOrDefaultAsync<T>(sql, new { id }, transaction: _transaction);
        }

        public async Task<PagedResult<T>> GetFilteredUsers(UserPaged criteria)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@$"SELECT * FROM [dbo].[{typeof(T).Name}] ");
            sb.Append("WHERE ");
            var replaces = Patterns.GenerateReplacesSQL(new List<string> { "FirstName", "LastName", "UserName" }, criteria.SearchWords);
            sb.Append(replaces);

            if (!string.IsNullOrEmpty(criteria.SortBy))
                sb.Append($"ORDER BY @SortBy @SortDirection ");
            else
                sb.Append($"ORDER BY FirstName, LastName, UserName ");
            sb.Append($"OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; ");
            sb.Append($"SELECT COUNT(*) FROM [dbo].[{typeof(T).Name}];");

            var parameters = new
            {
                SearchWords = criteria.SearchWords,
                SortBy = criteria.SortBy,
                SortDirection = criteria.SortDirection,
                Offset = (criteria.PageNumber - 1) * criteria.PageSize,
                PageSize = criteria.PageSize
            };

            using var multi = _connection.QueryMultiple(sb.ToString(), parameters, transaction: _transaction);
            var values = await multi.ReadAsync<T>();
            var total = multi.ReadSingle<int>();

            return new PagedResult<T>
            {
                Results = values,
                Total = total
            };
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var properties = entity.GetType().GetProperties().Where(p => p.Name.ToLower() != "datecreated");
            var sql = @$"UPDATE 
            [dbo].[{typeof(T).Name}] 
            SET {string.Join(",", properties.Where(p => p.Name != "Id").Select(x => x.Name + " = @" + x.Name))} 
            WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, entity, transaction: _transaction);
            return entity;
        }
    }
}
