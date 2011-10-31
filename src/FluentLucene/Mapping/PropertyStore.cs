using System.Collections.Generic;

namespace FluentLucene.Mapping
{
    public class PropertyStore : Dictionary<string, object>
    {
        private readonly IDictionary<string, object> _defaults;

        public PropertyStore(IDictionary<string, object> defaults)
        {
            _defaults = defaults;
        }

        public new object this[string key]
        {
            get
            {
                if (ContainsKey(key))
                    return base[key];
                return _defaults.ContainsKey(key) ? _defaults[key] : null;
            }
            set { base[key] = value; }
        }


        public bool IsSpecified(string property)
        {
            return this[property] != null;
        }
    }
}