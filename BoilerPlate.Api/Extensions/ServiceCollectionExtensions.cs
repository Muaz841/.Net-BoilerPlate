using BoilerPlate.Api.Configuration;
using BoilerPlate.Api.Controllers.Users.DTOS;
using BoilerPlate.Application.Services.UserServices;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using BoilerPlate.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoilerPlate.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configurations)
        {
            // User Service
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            // DbContext
            services.AddDbContext<BoilerPlateDbContext>(options =>
               options.UseSqlServer(connectionStrings.Default));

            // Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

            return services;

        }

        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddFluentValidationAutoValidation();  
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();     
            services.AddFluentValidationClientsideAdapters();

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
