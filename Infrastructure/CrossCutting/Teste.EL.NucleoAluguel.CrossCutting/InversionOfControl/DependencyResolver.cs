using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Teste.EL.NucleoAluguel.CrossCutting.Assemblies;

namespace Teste.EL.NucleoAluguel.CrossCutting.InversionOfControl
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddResolverDependencies(this IServiceCollection services)
        {
            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            var applicationInterfaces = AssemblyReflection.GetApplicationInterfaces();
            var applicationClasses = AssemblyReflection.GetApplicationClasses();

            foreach (var @interface in applicationInterfaces)
            {
                var type = AssemblyReflection.FindType(@interface, applicationClasses);

                if (type != null)
                    services.AddScoped(@interface, type);
            }
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            var domainInterfaces = AssemblyReflection.GetRepositoryInterfaces();
            var repositories = AssemblyReflection.GetRepositories();

            foreach (var repo in repositories)
            {
                var @interface = AssemblyReflection.FindInterface(repo, domainInterfaces);

                if (@interface != null)
                    services.AddScoped(@interface, repo);
            }
        }
    }
}