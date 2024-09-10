using Randevucum.Authentication.Orchestrator.API.Controllers.Grpc;
using Randevucum.Authentication.Orchestrator.API.Extensions;
using Randevucum.Authentication.Orchestrator.API.Middlewares;
using Randevucum.Authentication.Orchestrator.Application;
using Randevucum.Authentication.Orchestrator.Application.Services;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

builder.UseEurekaExtensions();
builder.UseKestrelExtension();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddHealthChecks();
builder.Services.AddApplication(builder.Configuration);

builder.Services.DefineGraphQL();

builder.Services.AddDiscoveryClient(builder.Configuration);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();

    AppContext.SetSwitch(
        "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
}

app.UseMiddleware<ApiGatewayMiddleware>();

app.MapGrpcService<AuthenticationGrpcController>();

app.MapGraphQL("/authentication");
app.MapGraphQLHttp("/authentication");

app.MapHealthCheck();
app.MapInfo();

app.Run();