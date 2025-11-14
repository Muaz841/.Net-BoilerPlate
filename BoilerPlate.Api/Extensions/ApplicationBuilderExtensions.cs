using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace BoilerPlate.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationPipeline(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
