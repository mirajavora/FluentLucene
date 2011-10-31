using System;
using System.Reflection;
using FluentLucene.Mapping;

namespace FluentLucene.Configuration
{
    public interface IConfiguration
    {
        IConfiguration AddFromAssembly(Assembly assembly);
        IMappingProvider GetMappingForType(Type type);
        bool IsConfigured { get; }
        void ApplyOverrides();
        
    }
}