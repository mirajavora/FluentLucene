using System;
using System.Reflection;
using FluentLucene.Members;

namespace FluentLucene.Mapping
{
    internal class PropertyMember : Member
    {
    private readonly PropertyInfo _member;
    private readonly MethodInfo _getMethod;
    private readonly MethodInfo _setMethod;

    public override string Name
    {
      get
      {
        return this._member.Name;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return this._member.PropertyType;
      }
    }

    public override MemberInfo MemberInfo
    {
      get
      {
        return (MemberInfo) this._member;
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return this._member.DeclaringType;
      }
    }

        public MethodInfo Get
        {
            get { return this._getMethod; }
        }

        public MethodInfo Set
        {
      get
      {
        return this._setMethod;
      }
    }

    public PropertyMember(PropertyInfo member)
    {
      this._member = member;
      this._getMethod = member.GetGetMethod(true);
      this._setMethod = member.GetSetMethod(true);
    }

    public override void SetValue(object target, object value)
    {
      this._member.SetValue(target, value, (object[]) null);
    }

    public override object GetValue(object target)
    {
      return this._member.GetValue(target, (object[]) null);
    }

    public override string ToString()
    {
      return "{Property: " + this._member.Name + "}";
    }
  }
}
