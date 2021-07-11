using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RAK.Core.UI.Xam.General
{
    public static class Serializer
    {
        public static string Serialize(this object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        public static T DeserializeJson<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<T>(data);

            return result;
        }
    }
}
