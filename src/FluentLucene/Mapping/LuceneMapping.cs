using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentLucene.Builders;
using FluentLucene.Members;
using FluentLucene.Reflection;

namespace FluentLucene.Mapping
{
    public class LuceneMapping<T> : IMappingProvider
    {
        private readonly IList<PropertyMapping> _mappings;

        public LuceneMapping()
        {
            _mappings = new List<PropertyMapping>();
        }

        public PropertyBuilder Map(Expression<Func<T, object>> memberExpression)
        {
            return Map(memberExpression, default(string));
        }

        public PropertyBuilder Map(Expression<Func<T, object>> memberExpression, string columnName)
        {
            return Map(ReflectionExtensions.ToMember<T, object>(memberExpression), columnName);
        }

        protected virtual PropertyBuilder Map(Member property, string columnName)
        {
            var mapping = new PropertyMapping();
            var propertyBuilder = new PropertyBuilder(mapping, typeof(T), property);
            if (!string.IsNullOrEmpty(columnName))
                propertyBuilder.Name(columnName);
            else
                propertyBuilder.Name(property.Name);
            _mappings.Add(mapping);
            return propertyBuilder;
        }

        public IList<PropertyMapping> Mappings
        {
            get { return _mappings; }
        }
    }
}