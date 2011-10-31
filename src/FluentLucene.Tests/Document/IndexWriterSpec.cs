using System;
using System.IO;
using FluentLucene.Indexers;
using FluentLucene.Tests.Model.Domain;
using FluentLucene.Tests.Specification;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using NUnit.Framework;
using Version = Lucene.Net.Util.Version;

namespace FluentLucene.Tests.Document
{
    public class IndexWriterSpec
    {
        public class when_items_are_indexed : ConfiguredContextSpecification
        {
            protected override void Context()
            {
                var dir = new SimpleFSDirectory(new DirectoryInfo(TempDirectory), new NoLockFactory());
                var analyzer = new StandardAnalyzer(Version.LUCENE_20);
                var maxFieldLength = new IndexWriter.MaxFieldLength(200);

                var index = new FluentIndexWriter<TestObject>(dir, analyzer, maxFieldLength);
                var data = new TestObject() { Id = Guid.NewGuid(), LongId = 123, ValidProperty = "Property", IgnoredProperty = "Ignored" };
                var data2 = new TestObject() { Id = Guid.Empty, LongId = 123456, ValidProperty = "Abc def ghij", IgnoredProperty = "Ignored" };

                index.AddDocument(data);
                index.AddDocument(data2);

                index.Commit();
            }

            [Test]
            public void index_file_exists()
            {
                var files = new DirectoryInfo(TempDirectory).GetFiles();
                files.Length.ShouldNotEqual(0);
            }
        }
    }
}