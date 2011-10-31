using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentLucene.Mapping;

namespace FluentLucene.Configuration
{
    public class Configuration : IConfiguration
    {
        private IList<InlineOverride> _overrides;
        private IDictionary<Type, IMappingProvider> _mappings;

        public Configuration()
        {
            _overrides = new List<InlineOverride>();
            _mappings = new Dictionary<Type, IMappingProvider>();
        }

        public IConfiguration AddFromAssembly(Assembly assembly)
        {
              foreach (var type1 in Enumerable.Select(Enumerable.Where(Enumerable.Select(assembly.GetExportedTypes(), type => new
              {
                type = type,
                entity = Enumerable.FirstOrDefault<Type>(Enumerable.Select(Enumerable.Where(type.GetInterfaces(), interfaceType =>
                                                                                                                      {
                                                                                                                          if (interfaceType.IsGenericType)
                                                                                                                              return interfaceType.GetGenericTypeDefinition() == typeof (ILuceneMappingOverride<>);
                                                                                                                          return false;
                                                                                                                      }), interfaceType => interfaceType.GetGenericArguments()[0]))
              }), param0 => param0.entity != (Type) null), param0 => new
              {
                OverrideType = param0.type,
                EntityType = param0.entity
              }))
              {
                object instance = Activator.CreateInstance(type1.OverrideType);
                IMappingProvider mappingProvider = (IMappingProvider) Activator.CreateInstance(typeof (LuceneMapping<>).MakeGenericType(new Type[1]
                {
                  type1.EntityType
                }));
                type1.OverrideType.GetMethod("Override");
                GetType().GetMethod("AddOverride", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(new Type[1]
                {
                  type1.EntityType
                }).Invoke((object) this, new object[2]
                {
                  (object) type1.EntityType,
                  instance
                });
              }
            return this;
        }

        private void AddOverride<T>(Type entity, ILuceneMappingOverride<T> mappingOverride)
        {
            AddOverrideAction(entity, x => mappingOverride.Override((LuceneMapping<T>)x));
        }

        internal void AddOverrideAction(Type type, Action<object> action)
        {
            _overrides.Add(new InlineOverride(type, action));
        }

        public void ApplyOverrides()
        {
            foreach (var inlineOverride in _overrides)
            {
                var autoMap = Activator.CreateInstance(typeof(LuceneMapping<>).MakeGenericType(new Type[1] { inlineOverride.Type}));
                inlineOverride.Apply(autoMap);

                //get the configuration
                var mapping = autoMap as IMappingProvider;
                _mappings.Add(inlineOverride.Type, mapping);
            }
            _isConfigured = true;
        }

        public IMappingProvider GetMappingForType(Type type)
        {
            return _mappings.ContainsKey(type) ? _mappings[type] : null;
        }

        private bool _isConfigured;
        public bool IsConfigured
        {
            get { return _isConfigured; }
        }
    }
}