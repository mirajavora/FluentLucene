using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentLucene.Mapping;
using FluentLucene.Members;

namespace FluentLucene.Reflection
{
    public static class ReflectionHelper
    {
        public static Member GetMember<TModel, TReturn>(Expression<Func<TModel, TReturn>> expression)
        {
            return GetMember(expression.Body);
        }

        public static Member GetMember<TModel>(Expression<Func<TModel, object>> expression)
        {
            return GetMember(expression.Body);
        }

        private static bool IsIndexedPropertyAccess(Expression expression)
        {
            if (IsMethodExpression(expression))
                return expression.ToString().Contains("get_Item");
            else
                return false;
        }

        private static bool IsMethodExpression(Expression expression)
        {
            if (expression is MethodCallExpression)
                return true;
            if (expression is UnaryExpression)
                return ReflectionHelper.IsMethodExpression((expression as UnaryExpression).Operand);
            else
                return false;
        }

        private static Member GetMember(Expression expression)
        {
            if (ReflectionHelper.IsMethodExpression(expression))
                return MemberExtensions.ToMember(((MethodCallExpression)expression).Method);
            else
                return MemberExtensions.ToMember(ReflectionHelper.GetMemberExpression(expression).Member);
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            return ReflectionHelper.GetMemberExpression(expression, true);
        }

        private static MemberExpression GetMemberExpression(Expression expression, bool enforceCheck)
        {
            MemberExpression memberExpression = (MemberExpression)null;
            if (expression.NodeType == ExpressionType.Convert)
                memberExpression = ((UnaryExpression)expression).Operand as MemberExpression;
            else if (expression.NodeType == ExpressionType.MemberAccess)
                memberExpression = expression as MemberExpression;
            if (enforceCheck && memberExpression == null)
                throw new ArgumentException("Not a member access", "expression");
            else
                return memberExpression;
        }
    }
}
