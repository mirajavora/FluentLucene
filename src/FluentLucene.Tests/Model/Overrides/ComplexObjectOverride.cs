using System.Collections.Generic;
using System.Text;
using System.Linq;
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
            mapping.Map(x => x.ListOfItems).Format(x => FlattenList(x)).Store(Field.Store.YES).Index(Field.Index.ANALYZED);
        }

        private string FlattenList(IEnumerable<InnerObject> items)
        {
            return string.Join(" ", items.Select(x => x.Category).ToArray());
        }
    }
}