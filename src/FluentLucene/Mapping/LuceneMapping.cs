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

        public PropertyBuilder<TMember> Map<TMember>(Expression<Func<T, TMember>> memberExpression)
        {
            return Map<TMember>(memberExpression, default(string));
        }

        public PropertyBuilder<TMember> Map<TMember>(Expression<Func<T, TMember>> memberExpression, string columnName)
        {
            return Map<TMember>(ReflectionExtensions.ToMember(memberExpression), columnName);
        }

        protected virtual PropertyBuilder<TMember> Map<TMember>(Member property, string columnName)
        {
            var mapping = new PropertyMapping();
            var propertyBuilder = new PropertyBuilder<TMember>(mapping, typeof(T), property);
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