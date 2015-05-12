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
    public class FileInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FileInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var path = (string)reader.Value;

            if (String.IsNullOrWhiteSpace(path))
                return null;

            return new FileInfo(path);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var fileInfo = (FileInfo)value;
            writer.WriteValue(fileInfo.FullName);
            writer.Flush();
        }
    }
}
