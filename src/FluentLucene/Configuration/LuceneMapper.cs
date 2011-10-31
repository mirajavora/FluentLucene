using System;
using System.Reflection;
using FluentLucene.Mapping;

namespace FluentLucene.Configuration
{
    public static class LuceneMapper
    {
        private static object _configurationSync = new object();
        private static IConfiguration _configuration;

        static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_configurationSync)
                    {
                        if (_configuration == null)
                            _configuration = new Configuration();
                    }
                }
                return _configuration;
            }
        }

        public static IConfiguration AddFromAssemblyOf<T>()
        {
            return Configuration.AddFromAssembly(typeof(T).Assembly);
        }

        public static IMappingProvider GetMappingForType(Type type)
        {
            return Configuration.GetMappingForType(type);
        }

        public static void Configure()
        {
            Configuration.ApplyOverrides();
        }

        public static bool IsConfigured
        {
            get { return Configuration.IsConfigured; }
        }
    }
}