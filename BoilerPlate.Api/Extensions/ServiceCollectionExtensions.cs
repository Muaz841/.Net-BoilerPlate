using BoilerPlate.Api.Controllers.Users.DTOS;
using BoilerPlate.Application.Configuration;
using BoilerPlate.Application.Services.Auth;
using BoilerPlate.Application.Services.UserServices;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Application.Shared.InterFaces.Auth;
using BoilerPlate.Application.Shared.InterFaces.UserInterface;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using BoilerPlate.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace BoilerPlate.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configurations)
        {            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddDistributedMemoryCache();


            // FOR PRODUCTION LATER – just uncomment and comment the line above
            // builder.Services.AddStackExchangeRedisCache(options =>
            // {
            //     options.Configuration = builder.Configuration.GetConnectionString("Redis");
            //     options.InstanceName = "BoilerPlate_";
            // });

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            
            var connectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            services.AddDbContext<BoilerPlateDbContext>(options =>
               options.UseSqlServer(connectionStrings.Default));
            
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));            
            services.AddScoped<IUnitOfWork, UnitOfWork>();            
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

            return services;

        }

        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddFluentValidationClientsideAdapters();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BoilerPlate API", Version = "v1" } );
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                                  "Enter your token in the text box below (without the word 'Bearer').\r\n\r\n" +
                                  "Example: 12345abcdef...",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var jwt = config.GetSection("JwtSettings").Get<JwtSettings>()!;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwt.Issuer,
                            ValidAudience = jwt.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
                        };

                    });

            services.AddEndpointsApiExplorer();


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
