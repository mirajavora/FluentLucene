using System.Linq;
using FluentLucene.Configuration;
using FluentLucene.Mapping;
using FluentLucene.Tests.Model.Domain;
using FluentLucene.Tests.Specification;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    public class FluentMappingSpec
    {
        public class when_domain_is_registered : ConfiguredContextSpecification
        {
            private IMappingProvider _mapping;

            protected override void Context()
            {
                _mapping = LuceneMapper.GetMappingForType(typeof (TestObject));
            }

            [Test]
            public void has_mapping_for_test_object()
            {
                _mapping.ShouldNotBeNull();
            }

            [Test]
            public void has_correct_properties_mapped()
            {
                _mapping.Mappings.Count.ShouldEqual(2);
            }
        }

        public class when_overriding_object_specific_member : ConfiguredContextSpecification
        {
            private PropertyMapping _mapping;

            protected override void Context()
            {
                var map = LuceneMapper.GetMappingForType(typeof(TestObject));
                _mapping = map.Mappings.First(x => x.Name == "ValidProperty");
            }

            [Test]
            public void the_index_setting_is_correct()
            {
                _mapping.Index.ShouldEqual(Field.Index.ANALYZED);
            }

            [Test]
            public void the_store_setting_is_correct()
            {
                _mapping.Store.ShouldEqual(Field.Store.NO);
            }
        }
    }
}