namespace cn.justwin.BLL
{
    using cn.justwin.Serialize;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class JsonHelper
    {
        public static List<string> GetListFromJson(string json)
        {
            List<string> list = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    return list;
                }
                if (!json.Contains("["))
                {
                    list.Add(json);
                    return list;
                }
                foreach (string str2 in json.Substring(0, json.Length - 1).Substring(1).Split(new char[] { ',' }))
                {
                    list.Add(str2.Substring(0, str2.Length - 1).Substring(1));
                }
            }
            catch
            {
            }
            return list;
        }

        public static T JsonDeserialize<T>(string jsonString) where T: class
        {
            cn.justwin.Serialize.JsonSerializer serializer = new cn.justwin.Serialize.JsonSerializer();
            return serializer.Deserialize<T>(jsonString);
        }

        public static string JsonSerializer<T>(T t)
        {
            cn.justwin.Serialize.JsonSerializer serializer = new cn.justwin.Serialize.JsonSerializer();
            return serializer.Serialize<T>(t);
        }

        public static string Serialize(string[] arr)
        {
            string str = string.Empty;
            if (arr.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("[\"");
                builder.Append(string.Join("\",\"", arr));
                return builder.Append("\"]").ToString();
            }
            return str;
        }
    }
}

