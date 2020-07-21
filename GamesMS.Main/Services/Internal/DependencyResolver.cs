using GamesMS.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GamesMS.Main.Services.Internal
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            var assemblies = new[] { Assembly.Load("GamesMS"), Assembly.Load("GamesMS.Api") }.ToList();
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("GamesMS")));
            assemblies.AddRange(assemblies.SelectMany(a => a.GetReferencedAssemblies().Select((aref) => Assembly.Load(aref))).ToList());
            assemblies = assemblies.Distinct().ToList();

            var types = assemblies
                .SelectMany(s => s.GetTypes());

            var dependencyHook = typeof(IDependency);
            var dependencies = types
                .Where(p => dependencyHook.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(p => p);

            foreach (var dependency in dependencies)
            {
                var interfaces = dependency.GetInterfaces().Where(i => i != typeof(IDependency));

                foreach(var @interface in interfaces)
                {
                    services.AddTransient(@interface, dependency);
                }
            }

            var singletonDependencyHook = typeof(ISingletonDependency);
            var singletonDependencies = types
                .Where(p => singletonDependencyHook.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(p => p);

            foreach (var dependency in singletonDependencies)
            {
                var interfaces = dependency.GetInterfaces().Where(i => i != typeof(ISingletonDependency));

                foreach (var @interface in interfaces)
                {
                    services.AddSingleton(@interface, dependency);
                }
            }
        }
    }
}
