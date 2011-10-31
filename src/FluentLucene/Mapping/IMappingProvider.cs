using System.Collections.Generic;

namespace FluentLucene.Mapping
{
    public interface IMappingProvider
    {
        IList<PropertyMapping> Mappings { get; }
    }
}