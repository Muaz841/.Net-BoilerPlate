using BoilerPlate.Api.GlobalExceptionHandlerMiddleware;
using BoilerPlate.Application.Shared.InterFaces;
using BoilerPlate.Infrastructure.Database.BoilerPlateDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BoilerPlate.Extensions;

public static class ApplicationBuilderExtensions
{


    public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BoilerPlateDbContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

        await dbContext.Database.MigrateAsync();
        await seeder.SeedAsync(); 
    }
    public static IApplicationBuilder UseApplicationPipeline(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseRouting();

        

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
