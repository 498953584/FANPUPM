namespace cn.justwin.Domain.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class PopupConfig
    {
        public static IList<PopupModule> GetAllModules(string xmlPath)
        {
            return (from e in XElement.Load(xmlPath).Elements("popup") select new PopupModule { Module = e.Element("module").Value, Name = e.Element("name").Value, Url = e.Element("url").Value }).ToList<PopupModule>();
        }
    }
}

