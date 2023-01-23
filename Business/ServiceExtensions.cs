using Business.Repositories;
using Application.Services;
using Domain.Interfaces.Data;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services.ProjectCharterBLL;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.UnitOfWork;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Application.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Application.CacheService;
using Cache =  Application.CacheService.CacheService;

namespace Application
{

    public static class ServiceExtensions
    {
        public static IServiceCollection ConfiguringControllers(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddEndpointsApiExplorer();
            services.AddControllers(options =>
            {
                options.Filters.Add<ErrorHandlingFilterAttribute>();
            });
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDbConnectionFactory, DBConnection>();
            services.AddTransient<IUnitOfWork, UnitOfWorkDapper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProjectCharterBLL, ProjectCharterBLL>();
            services.AddScoped<BlobStorageService>();
            services.AddScoped<ICacheService, Cache>();
            services.AddHttpClient();
            services.AddScoped<IThirdPartyRepository, ThirdPartyRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        );
            });

            return services;
        }

        public static IServiceCollection ConfiguringSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
            });
            return services;
        }


        public static WebApplication ConfiguringApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }

}
