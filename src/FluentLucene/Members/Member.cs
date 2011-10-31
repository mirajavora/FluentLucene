using System;
using System.Reflection;

namespace FluentLucene.Members
{
    public abstract class Member
    {
        public abstract string Name { get; }

        public abstract Type PropertyType { get; }


        public abstract MemberInfo MemberInfo { get; }

        public abstract Type DeclaringType { get; }

        public static bool operator ==(Member left, Member right)
        {
            return object.Equals((object)left, (object)right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !object.Equals((object)left, (object)right);
        }

        public bool Equals(Member other)
        {
            if (object.Equals((object)other.MemberInfo.MetadataToken, (object)this.MemberInfo.MetadataToken))
                return object.Equals((object)other.MemberInfo.Module, (object)this.MemberInfo.Module);
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals((object)null, obj))
                return false;
            if (object.ReferenceEquals((object)this, obj))
                return true;
            if (!(obj is Member))
                return false;
            else
                return this.Equals((Member)obj);
        }

        public override int GetHashCode()
        {
            return this.MemberInfo.GetHashCode() ^ 3;
        }

        public abstract void SetValue(object target, object value);

        public abstract object GetValue(object target);
    }
}