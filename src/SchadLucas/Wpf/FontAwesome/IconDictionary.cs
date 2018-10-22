using System.Collections.Generic;
using System.Linq;

namespace SchadLucas.Wpf.FontAwesome
{
    public class IconDictionary : Dictionary<string, char>
    {
        public char GetFromKey(string key)
        {
            return ContainsKey(key) ? this[key] : this.First().Value;
        }
    }
}