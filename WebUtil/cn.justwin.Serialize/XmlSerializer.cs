namespace cn.justwin.Serialize
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// XML序列化类
    /// </summary>
    public class XmlSerializer : ISerializable
    {
        /// <summary>
        /// 将字符串反序列化为指定类型的实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="str">XML格式的字符</param>
        /// <returns>制定类型的实例</returns>
        public T Deserialize<T>(string str) where T: class
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(str))
            {
                object obj2 = serializer.Deserialize(reader);
                if (obj2 is T)
                {
                    return (obj2 as T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 将指定类型XML序列化为字符串
        /// 注意编码方式是utf16
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实例</param>
        /// <returns>XML格式的字符</returns>
        public string Serialize<T>(T t)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, t, namespaces);
                return writer.ToString();
            }
        }
    }
}

