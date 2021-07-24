namespace cn.justwin.BLL
{
    using cn.justwin.Tender;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    public class ProvincesProjectCode : IProjectCode
    {
        public string CreateProjectCode()
        {
            throw new NotImplementedException();
        }

        public string CreateTenderCode()
        {
            throw new NotImplementedException();
        }

        public List<Area> GetCityByProvinceList(string province)
        {
            System.Func<XElement, bool> predicate = null;
            List<Area> list = new List<Area>();
            string uri = AppDomain.CurrentDomain.BaseDirectory + @"XML\ProvincesCode.xml";
            try
            {
                if (predicate == null)
                {
                    predicate = ss => ss.Attribute("Name").Value.ToString() == province.Trim();
                }
                foreach (XElement element3 in (from ss in XElement.Load(uri).Elements("Area").Elements<XElement>("Province").Where<XElement>(predicate).FirstOrDefault<XElement>().Elements("City") select ss).ToList<XElement>())
                {
                    list.Add(new Area(element3.Attribute("Code").Value.ToString(), element3.Attribute("Name").Value.ToString()));
                }
            }
            catch
            {
            }
            return list;
        }

        public List<Area> GetProvinceList()
        {
            List<Area> list = new List<Area>();
            string uri = AppDomain.CurrentDomain.BaseDirectory + @"XML\ProvincesCode.xml";
            try
            {
                IEnumerable<XElement> enumerable = (from ss in XElement.Load(uri).Elements("Area").Elements<XElement>("Province") select ss).ToList<XElement>();
                list.Add(new Area("", "请选择"));
                foreach (XElement element2 in enumerable)
                {
                    Area item = new Area(element2.Attribute("Code").Value.ToString(), element2.Attribute("Name").Value.ToString());
                    list.Add(item);
                }
            }
            catch
            {
            }
            return list;
        }

        public string GetProvincesCode(string province, string city)
        {
            System.Func<XElement, bool> predicate = null;
            System.Func<XElement, bool> func2 = null;
            StringBuilder builder = new StringBuilder();
            string uri = AppDomain.CurrentDomain.BaseDirectory + @"XML\ProvincesCode.xml";
            try
            {
                if (predicate == null)
                {
                    predicate = s => s.Attribute("Name").Value.ToString() == province.Trim();
                }
                XElement element2 = XElement.Load(uri).Elements("Area").Elements<XElement>("Province").Where<XElement>(predicate).FirstOrDefault<XElement>();
                if (element2 != null)
                {
                    XElement parent = element2.Parent;
                    if (func2 == null)
                    {
                        func2 = ss => ss.Attribute("Name").Value.ToString() == city.Trim();
                    }
                    XElement element4 = element2.Elements("City").Where<XElement>(func2).FirstOrDefault<XElement>();
                    builder.Append(parent.Attribute("Code").Value.ToString());
                    builder.Append(element2.Attribute("Code").Value.ToString());
                    builder.Append(element4.Attribute("Code").Value.ToString());
                }
            }
            catch
            {
            }
            return builder.ToString();
        }
    }
}

