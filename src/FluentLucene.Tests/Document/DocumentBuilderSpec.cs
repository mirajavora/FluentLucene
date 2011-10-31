using System;
using FluentLucene.Builders;
using FluentLucene.Configuration;
using FluentLucene.Tests.Model.Domain;
using FluentLucene.Tests.Specification;
using NUnit.Framework;

namespace FluentLucene.Tests.Document
{
    public class DocumentBuilderSpec
    {
        public class when_building_lucene_document_from_object : ConfiguredContextSpecification
        {
            private Lucene.Net.Documents.Document _document;

            protected override void Context()
            {
                var builder = new DocumentBuilder();
                var mapping = LuceneMapper.GetMappingForType(typeof (TestObject));
                var item = new TestObject() { Id = Guid.NewGuid(), IgnoredProperty = "Property", LongId = 123456, ValidProperty = "Valid property", Text = "Abc def ghi ijkl mno pqr stuv"};
                _document = builder.BuildDocumentForMapping(item, mapping);
            }

            [Test]
            public void document_is_not_null()
            {
                _document.ShouldNotBeNull();
            }

            [Test]
            public void has_correct_number_of_fields()
            {
                _document.GetFields().Count.ShouldEqual(2);
            }
        } 
    }
}