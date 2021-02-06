using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Teste.EL.NucleoAluguel.CrossCutting.Assemblies
{
    [ExcludeFromCodeCoverage]
    public class AssemblyReflection
    {
        public static IEnumerable<Type> GetApplicationInterfaces()
        {
            return Assembly.Load("Teste.EL.NucleoAluguel.API").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("Teste.EL.NucleoAluguel.API.Interfaces"));
        }

        public static IEnumerable<Type> GetApplicationClasses()
        {
            return Assembly.Load("Teste.EL.NucleoAluguel.API").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Type> GetRepositoryInterfaces()
        {
            return Assembly.Load("Teste.EL.NucleoAluguel.Domain").GetTypes().Where(
                type => type.IsInterface
                && type.Namespace != null
                && type.Namespace.StartsWith("Teste.EL.NucleoAluguel.Domain.Repositories"));
        }

        public static IEnumerable<Type> GetRepositories()
        {
            return Assembly.Load("Teste.EL.NucleoAluguel.DataAccess").GetTypes().Where(
                type => type.IsClass
                && !type.IsAbstract
                && type.Namespace != null
                && type.Namespace.StartsWith("Teste.EL.NucleoAluguel.DataAccess.Repositories")
                && type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
        }

        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("Teste.EL.NucleoAluguel.Api"),
                Assembly.Load("Teste.EL.NucleoAluguel.Domain"),
                Assembly.Load("Teste.EL.NucleoAluguel.CrossCutting"),
                Assembly.Load("Teste.EL.NucleoAluguel.DataAccess")
            };
        }

        public static Type FindType(Type @interface, IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return type;
                }
            }

            return null;
        }

        public static Type FindInterface(Type type, IEnumerable<Type> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                if (type.GetInterfaces().Contains(@interface))
                {
                    return @interface;
                }
            }

            return null;
        }
    }
}

