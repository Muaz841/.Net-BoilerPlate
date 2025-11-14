using BoilerPlate.Api.Extensions;
using BoilerPlate.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentationServices();

var app = builder.Build();

app.UseApplicationPipeline();

app.Run();
