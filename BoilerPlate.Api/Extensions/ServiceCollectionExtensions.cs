using BoilerPlate.Api.Configuration;
using BoilerPlate.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoilerPlate.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configurations)
        {
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddDbContext<BoilerPlateDbContext>(options =>
               options.UseSqlServer(connectionStrings.Default));

            return services;

        }


        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            return services;
        }


    }
}
