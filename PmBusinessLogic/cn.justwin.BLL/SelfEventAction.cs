namespace cn.justwin.BLL
{
    using PmBusinessLogic;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    public class SelfEventAction
    {
        public static string GetTypeName(string path, string tableName, string columnName)
        {
            System.Func<XElement, bool> predicate = null;
            string str = string.Empty;
            try
            {
                if (predicate == null)
                {
                    predicate = s => (s.Element("TableName").Value == tableName) && (s.Element("ColumnName").Value == columnName);
                }
                XElement element2 = XElement.Load(path).Elements("EventInfo").Where<XElement>(predicate).FirstOrDefault<XElement>();
                if (element2 != null)
                {
                    str = element2.Element("TypeName").Value;
                }
            }
            catch
            {
            }
            return str;
        }

        public static void SuperDelete(string key, string tableName, string columnName)
        {
            string str2 = GetTypeName(AppDomain.CurrentDomain.BaseDirectory + "/SelfEventInfo.xml", tableName, columnName);
            if (!string.IsNullOrEmpty(str2))
            {
                ISelfEvent event2 = Assembly.Load("PmBusinessLogic").CreateInstance(str2) as ISelfEvent;
                if (event2 != null)
                {
                    event2.SuperDelete(key);
                }
            }
        }
    }
}

