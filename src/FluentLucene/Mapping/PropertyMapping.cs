using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentLucene.Members;
using Lucene.Net.Documents;

namespace FluentLucene.Mapping
{
    public class PropertyMapping
    {
        protected readonly PropertyStore Properties;

        public PropertyMapping(PropertyStore properties)
        {
            Properties = properties;
        }

        public PropertyMapping() : this(new PropertyStore(new Dictionary<string, object>()))
        {

        }

        public Field.Store Store { get; set; }
        public Field.Index Index { get; set; }
        public Field.TermVector TermVector { get; set; }

        public string Name { get; set; }
        internal Type Type { get; set; }
        internal Member Member { get; set; }
        internal Delegate ExpressionDelegate { get; set; }
    }
}