using System;
using System.Linq.Expressions;
using FluentLucene.Mapping;
using FluentLucene.Members;

namespace FluentLucene.Reflection
{
    public static class ReflectionExtensions
    {
        public static Member ToMember<TMapping, TReturn>(this Expression<Func<TMapping, TReturn>> propertyExpression)
        {
            return ReflectionHelper.GetMember<TMapping, TReturn>(propertyExpression);
        }
    }
}