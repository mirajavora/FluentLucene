using System;
using FluentLucene.Mapping;
using FluentLucene.Members;
using Lucene.Net.Documents;

namespace FluentLucene.Builders
{
    public class PropertyBuilder
    {
        private readonly PropertyMapping _mapping;

        public PropertyBuilder(PropertyMapping mapping, Type containingEntityType, Member member)
        {
            _mapping = mapping;
            _mapping.Type = containingEntityType;
            _mapping.Member = member;

            //set defaults here?
            TermVector(Field.TermVector.NO);
            Store(Field.Store.YES);
        }

        public PropertyBuilder Store(Field.Store store)
        {
            _mapping.Store = store;
            return this;
        }

        public PropertyBuilder Index(Field.Index index)
        {
            _mapping.Index = index;
            return this;
        }

        public PropertyBuilder TermVector(Field.TermVector termVector)
        {
            _mapping.TermVector = termVector;
            return this;
        }

        public PropertyBuilder Name(string name)
        {
            _mapping.Name = name;
            return this;
        }
    }
}