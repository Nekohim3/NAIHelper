using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NAIHelper.Utils
{
    public class TreeJsonConverter : JsonConverter
    {
        public override void    WriteJson(JsonWriter writer, object? value,      JsonSerializer serializer)
        {

        }

        public override object? ReadJson(JsonReader  reader, Type    objectType, object?        existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool    CanConvert(Type      objectType)
        {
            return false;
        }
    }
}
