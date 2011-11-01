using System;
using System.Linq.Expressions;
using FluentLucene.Mapping;
using FluentLucene.Members;
using Lucene.Net.Documents;

namespace FluentLucene.Builders
{
    public class PropertyBuilder<T>
    {
        private readonly PropertyMapping _mapping;

        public PropertyBuilder(PropertyMapping mapping, Type containingEntityType, Member member)
        {
            _mapping = mapping;
            _mapping.Type = containingEntityType;
            _mapping.Member = member;

            SetDefaults();
        }

        public PropertyBuilder<T> Store(Field.Store store)
        {
            _mapping.Store = store;
            return this;
        }

        public PropertyBuilder<T> Index(Field.Index index)
        {
            _mapping.Index = index;
            return this;
        }

        public PropertyBuilder<T> TermVector(Field.TermVector termVector)
        {
            _mapping.TermVector = termVector;
            return this;
        }

        public PropertyBuilder<T> Name(string name)
        {
            _mapping.Name = name;
            return this;
        }

        public PropertyBuilder<T> Format(Expression<Func<T, string>> expression)
        {
            LambdaExpression lambda = Expression.Lambda<Func<T, string>>(expression.Body, expression.Parameters);
            _mapping.ExpressionDelegate = lambda.Compile();
            return this;
        }

        internal void SetDefaults()
        {
            TermVector(Field.TermVector.NO);
            Store(Field.Store.YES);
            Index(Field.Index.NO);
        }
    }
}