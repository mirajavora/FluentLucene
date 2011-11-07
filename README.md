# Fluent Lucene
## A fluent wrapper around Lucene.Net abstracting document creation. Inspired by Fluent NHibernate.

### Create Lucene Documents with Overrides

    public class ComplexObjectOverride : ILuceneMappingOverride<ComplexObject>
    {
        public void Override(LuceneMapping<ComplexObject> mapping)
        {
            mapping.Map(x => x.Title).Store(Field.Store.YES);
            mapping.Map(x => x.ListOfItems).Format(x => FlattenList(x)).Store(Field.Store.YES).Index(Field.Index.ANALYZED);
        }
    }



### Use index writers with your objects (not custom docs)

	var index = new FluentIndexWriter<TestObject>(dir, analyzer, maxFieldLength);
        index.AddDocument(new TestObject() { ... });



### Search quickly over your indexes