using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GamesMS.Core.Models;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Linq;
using System.Reflection;

namespace GamesMS.Core.Services.Internal
{
    public interface ISessionFactoryHelper : IDependency
    {
        ISessionFactory CreateSessionFactory();
    }

    sealed class SessionFactoryHelper : ISessionFactoryHelper
    {
        private readonly IOptions<DefaultConfiguration> configuration;

        public SessionFactoryHelper(IOptions<DefaultConfiguration> configuration)
        {
            this.configuration = configuration;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                    .Database( 
                        MsSqlConfiguration.MsSql2012
                        .ConnectionString(configuration.Value.ConnectionString))
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                    .Cache(c => c.UseQueryCache()
                    .UseSecondLevelCache()
                    .ProviderClass<NHibernate.Cache.HashtableCacheProvider>())
                    .Mappings(AddAssemblies)
                .BuildSessionFactory();
        }

        private static void AddAssemblies(MappingConfiguration mappingConfiguration)
        {
            var mappings = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("GamesMS"))
                .SelectMany(s => s.GetTypes())
                .Where(t => IsMapping(t) && !t.IsInterface && !t.IsAbstract);

            foreach(var mapping in mappings)
            {
                mappingConfiguration.FluentMappings.Add(mapping);
            }
        }

        private static bool IsMapping(Type t)
        {
            return typeof(IRecordMapping).IsAssignableFrom(t);
        }
    }
}
