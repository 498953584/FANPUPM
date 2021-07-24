namespace cn.justwin.BLL
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;

    public static class JsonNetWrap
    {
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string SerializeObject(object obj)
        {
            new JsonSerializerSettings();
            IsoDateTimeConverter converter = new IsoDateTimeConverter {
                DateTimeFormat = "yyyy-MM-dd"
            };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonConverter[] { converter });
        }
    }
}

