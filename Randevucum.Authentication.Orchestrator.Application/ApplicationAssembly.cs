using System.Reflection;

namespace Randevucum.Authentication.Orchestrator.Application;

public static class ApplicationAssembly
{
    public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
}