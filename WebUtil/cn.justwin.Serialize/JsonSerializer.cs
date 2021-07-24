namespace cn.justwin.Serialize
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// JSON序列化类
    /// </summary>
    public class JsonSerializer : ISerializable
    {
        /// <summary>
        /// 将JSON字符串反序列化为指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="str">JSON字符串</param>
        /// <returns>该JSON字符串表示的类型实例</returns>
        public T Deserialize<T>(string str) where T: class
        {
            T local = default(T);
            if (!string.IsNullOrEmpty(str))
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    object obj2 = new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
                    if (obj2 is T)
                    {
                        local = obj2 as T;
                    }
                }
            }
            return local;
        }

        /// <summary>
        /// 讲指定类型序列化为JSON字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">类型实例</param>
        /// <returns>JSON字符串</returns>
        public string Serialize<T>(T t)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, t);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}

