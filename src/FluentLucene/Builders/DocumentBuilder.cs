using System;
using System.Linq.Expressions;
using FluentLucene.Mapping;
using Lucene.Net.Documents;

namespace FluentLucene.Builders
{
    public class DocumentBuilder
    {
        public Document BuildDocumentForMapping<T>(T item, IMappingProvider mapping)
        {
            var doc = new Document();
            foreach (var map in mapping.Mappings)
            {
                //var value = map.
                var value = map.Member.GetValue(item) ?? string.Empty; //handle this better?
                if (map.ExpressionDelegate != null)
                {
                    var obj = map.ExpressionDelegate.DynamicInvoke(value);
                    value = obj;
                }

                var field = new Field(map.Name, value.ToString(), map.Store, map.Index, map.TermVector);
                doc.Add(field);
            }

            return doc;
        }
    }
}