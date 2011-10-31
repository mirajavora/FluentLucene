using FluentLucene.Mapping;
using FluentLucene.Tests.Model.Domain;
using Lucene.Net.Documents;

namespace FluentLucene.Tests.Model.Overrides
{
    public class TestObjectOverride : ILuceneMappingOverride<TestObject>
    {
        public void Override(LuceneMapping<TestObject> mapping)
        {
            mapping.Map(x => x.ValidProperty).Store(Field.Store.NO).Index(Field.Index.ANALYZED).Name("ValidProperty");
            mapping.Map(x => x.Text).Store(Field.Store.YES).Index(Field.Index.NO).Name("Text");
        }
    }
}