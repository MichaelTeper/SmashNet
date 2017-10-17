using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmashNet
{
    static class JsonExtensionMethods
    {
        public static T ValueNoNull<T>(this JToken token, T key)
        {
            if ((token[key].Type == JTokenType.Null))
                return default(T);

            return token.Value<T>(key);
        }
    }
}
