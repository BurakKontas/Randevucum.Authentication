using System.Reflection;

namespace Randevucum.Authentication.Orchestrator.API.Extensions;

public static class InfoExtension
{
    public static void MapInfo(this WebApplication app)
    {
        app.MapGet("/info", () =>
        {
            var version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            const string appName = "Authentication API";
            var environment = app.Environment.EnvironmentName;

            return new InfoResult() { AppName = appName, Version = version, Environment = environment };
        });
    }

    private class InfoResult
    {
        public required string AppName { get; set; }
        public required string? Version { get; set; }
        public required string Environment { get; set; }
    }
}