using System.Linq;
using FluentLucene.Configuration;
using FluentLucene.Mapping;
using FluentLucene.Tests.Model.Domain;
using FluentLucene.Tests.Specification;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    public class PropertyNameMappingSpec
    {
        public class when_column_name_is_not_specified : ConfiguredContextSpecification
        {
            private IMappingProvider _mapping;

            protected override void Context()
            {
                _mapping = LuceneMapper.GetMappingForType(typeof(GeneratedNameObject));
            }

            [Test]
            public void _property_is_mapped()
            {
                _mapping.ShouldNotBeNull();
                _mapping.Mappings.Count.ShouldEqual(2);
            }

            [Test]
            public void property_has_name()
            {
                _mapping.Mappings.First().Name.ShouldEqual("AutomaticName");
            }
        }

        public class when_column_name_is_specified : ConfiguredContextSpecification
        {
            private IMappingProvider _mapping;

            protected override void Context()
            {
                _mapping = LuceneMapper.GetMappingForType(typeof(GeneratedNameObject));
            }

            [Test]
            public void property_has_name()
            {
                _mapping.Mappings.Last().Name.ShouldEqual("NewName");
            } 
        }
    }
}