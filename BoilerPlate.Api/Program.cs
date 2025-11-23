using BoilerPlate.Api.Extensions;
using BoilerPlate.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);
    

var app = builder.Build();

app.MigrateAndSeedDatabaseAsync();

app.UseApplicationPipeline();

app.Run();
