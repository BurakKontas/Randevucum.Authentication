using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Randevucum.Authentication.Orchestrator.API.Extensions;
using Randevucum.Authentication.Orchestrator.API.Middlewares;
using Randevucum.Authentication.Orchestrator.API.Services.Common;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

builder.UseEurekaExtensions();
builder.UseKestrelExtension();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddHealthChecks();
//builder.Services.AddApplication(builder.Configuration);

builder.Services.DefineGraphQL();

builder.Services.AddDiscoveryClient(builder.Configuration);

builder.Services.AddScoped<IAuthenticationCommonService, AuthenticationCommonService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseMiddleware<ApiGatewayMiddleware>();

AppContext.SetSwitch(
    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

app.MapGrpcService<AuthenticationService>()
    .AllowAnonymous();

app.MapGraphQL("/authentication");

app.MapHealthCheck();
app.MapInfo();

app.Run();