using Randevucum.Authentication.Microservices.Basic.Application;
using Randevucum.Authentication.Microservices.Basic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Run();