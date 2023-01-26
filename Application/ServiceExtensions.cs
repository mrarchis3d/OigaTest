using Application.Filters;
using Domain.Interfaces.Data;
using Domain.Interfaces.UnitOfWork;
using FluentValidation;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Infrastructure.Data;
using Application.Behaviors;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddControllers(options =>
            {
                options.Filters.Add<ErrorHandlingFilterAttribute>();
            });
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionFactory, DBConnection>();
            services.AddTransient<IUnitOfWork, UnitOfWorkDapper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
