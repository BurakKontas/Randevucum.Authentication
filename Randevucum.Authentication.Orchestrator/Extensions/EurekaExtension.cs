namespace Randevucum.Authentication.Orchestrator.API.Extensions;

public static class EurekaExtensions
{
    public static void UseEurekaExtensions(this WebApplicationBuilder builder)
    {
        builder.Configuration["eureka:instance:instanceId"] = builder.Configuration["eureka:instance:instanceId"]!
            .Replace("${random.value}", Guid.NewGuid().ToString());
    }
}