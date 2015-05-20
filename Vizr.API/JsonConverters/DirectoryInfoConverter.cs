using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API.JsonConverters
{
    public class DirectoryInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DirectoryInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var path = (string)reader.Value;

            if (String.IsNullOrWhiteSpace(path))
                return null;

            return new DirectoryInfo(path);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var directoryInfo = (DirectoryInfo)value;
            writer.WriteValue(directoryInfo.FullName);
            writer.Flush();
        }
    }
}
