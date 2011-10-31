using FluentLucene.Mapping;
using FluentLucene.Tests.Model.Domain;
using Lucene.Net.Documents;

namespace FluentLucene.Tests.Model.Overrides
{
    public class GeneratedNameObjectOverride : ILuceneMappingOverride<GeneratedNameObject>
    {
        public void Override(LuceneMapping<GeneratedNameObject> mapping)
        {
            mapping.Map(x => x.AutomaticName).Store(Field.Store.YES);
            mapping.Map(x => x.SetName).Name("NewName");
        }
    }
}