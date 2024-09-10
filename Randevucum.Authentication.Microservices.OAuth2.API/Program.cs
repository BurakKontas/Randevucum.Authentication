using Randevucum.Authentication.Microservices.OAuth2.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.Run();
