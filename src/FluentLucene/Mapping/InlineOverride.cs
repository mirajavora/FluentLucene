using System;

namespace FluentLucene.Mapping
{
    public class InlineOverride
    {
        private readonly Type type;
        private readonly Action<object> action;

        public InlineOverride(Type type, Action<object> action)
        {
            this.type = type;
            this.action = action;
        }

        public Type Type
        {
            get { return type; }
        }

        public void Apply(object mapping)
        {
            this.action(mapping);
        }
    }
}