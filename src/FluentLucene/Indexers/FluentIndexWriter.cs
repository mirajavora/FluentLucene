
using FluentLucene.Builders;
using FluentLucene.Configuration;
using FluentLucene.Mapping;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace FluentLucene.Indexers
{
    public class FluentIndexWriter<T> : IndexWriter
    {
        private DocumentBuilder _documentBuilder;
        private IMappingProvider _mapping;

        public FluentIndexWriter(Directory directory, Analyzer analyzer, MaxFieldLength maxFieldLength) : base(directory, analyzer, maxFieldLength)
        {
            _documentBuilder = new DocumentBuilder();
            _mapping = LuceneMapper.GetMappingForType(typeof (T));
        }

        public virtual void AddDocument(T item)
        {
            AddDocument(CreateDocument(item));
        }

        public virtual void AddDocument(T item, Analyzer analyzer)
        {
            AddDocument(CreateDocument(item), analyzer);
        }

        protected virtual Document CreateDocument(T item)
        {
            return _documentBuilder.BuildDocumentForMapping(item, _mapping);
        }
    }
}