using FluentLucene.Mapping;
using FluentLucene.Tests.Model.Domain;
using Lucene.Net.Documents;

namespace FluentLucene.Tests.Model.Overrides
{
    public class ComplexObjectOverride : ILuceneMappingOverride<ComplexObject>
    {
        public void Override(LuceneMapping<ComplexObject> mapping)
        {
            mapping.Map(x => x.Title).Store(Field.Store.YES);
        }
    }
}