using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Randevucum.Authentication.Orchestrator.API.Extensions;

public static class KestrelExtension
{
    public static void UseKestrelExtension(this WebApplicationBuilder builder)
    {

        builder.WebHost.UseKestrel((context, options) =>
        {
            options.ListenAnyIP(8080, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });

            options.ListenAnyIP(8081, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
                listenOptions.UseHttps();
            });
        });
    }
}