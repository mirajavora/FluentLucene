using System;
using System.Collections.Generic;
using System.Reflection;
using FluentLucene.Mapping;

namespace FluentLucene.Members
{
    public static class MemberExtensions
    {
        public static Member ToMember(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == (PropertyInfo)null)
                throw new NullReferenceException("Cannot create member from null.");
            else
                return (Member)new PropertyMember(propertyInfo);
        }

        public static Member ToMember(this MemberInfo memberInfo)
        {
            if (memberInfo == (MemberInfo)null)
                throw new NullReferenceException("Cannot create member from null.");
            if (memberInfo is PropertyInfo)
                return MemberExtensions.ToMember((PropertyInfo)memberInfo);
            if (memberInfo is FieldInfo)
                throw new InvalidOperationException("Cannot convert MemberInfo '" + memberInfo.Name + "' to Member.");
            if (memberInfo is MethodInfo)
                throw new InvalidOperationException("Cannot convert MemberInfo '" + memberInfo.Name + "' to Member.");
            else
                throw new InvalidOperationException("Cannot convert MemberInfo '" + memberInfo.Name + "' to Member.");
        }

        public static IEnumerable<Member> GetInstanceFields(this Type type)
        {
            foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!fieldInfo.Name.StartsWith("<"))
                    yield return MemberExtensions.ToMember(fieldInfo);
            }
        }

        public static IEnumerable<Member> GetInstanceMethods(this Type type)
        {
            foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!methodInfo.Name.StartsWith("get_") && !methodInfo.Name.StartsWith("set_") && (methodInfo.ReturnType != typeof(void) && methodInfo.GetParameters().Length == 0))
                    yield return MemberExtensions.ToMember(methodInfo);
            }
        }

        public static IEnumerable<Member> GetInstanceProperties(this Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                yield return MemberExtensions.ToMember(propertyInfo);
        }
    }
}