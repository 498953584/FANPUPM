namespace cn.justwin.BLL
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class AppSettingHelper
    {
        public static void WritToDB(object state)
        {
            try
            {
                List<XElement> list = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "AppSettings.config").Elements("add").ToList<XElement>();
                BasicConfigService service = new BasicConfigService();
                foreach (XElement element in list)
                {
                    string name = element.Attribute("key").Value;
                    string str3 = element.Attribute("value").Value;
                    BasicConfig byName = service.GetByName(name);
                    if (byName != null)
                    {
                        byName.ParaValue = str3;
                        service.Update(byName);
                    }
                    else
                    {
                        byName = new BasicConfig {
                            Id = Guid.NewGuid().ToString(),
                            ParaName = name,
                            ParaValue = str3
                        };
                        service.Add(byName);
                    }
                }
            }
            catch
            {
            }
        }
    }
}

