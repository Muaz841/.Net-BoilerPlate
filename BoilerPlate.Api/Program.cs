using BoilerPlate.Api.Extensions;
using BoilerPlate.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationServices()
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);
    

var app = builder.Build();

app.UseApplicationPipeline();

app.Run();
