using System.Reflection;

namespace Randevucum.Authentication.Microservices.Basic.Infrastructure;

public static class InfrastructureAssembly
{
    public static Assembly Assembly => typeof(InfrastructureAssembly).Assembly;
}