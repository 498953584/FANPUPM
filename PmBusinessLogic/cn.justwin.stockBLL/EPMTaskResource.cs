namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using System;
    using System.Collections.Generic;

    public class EPMTaskResource
    {
        private readonly cn.justwin.stockDAL.EPMTaskResource dal = new cn.justwin.stockDAL.EPMTaskResource();

        public Dictionary<string, decimal> GetDictionary(string projectCode)
        {
            return this.dal.GetDictionary(projectCode);
        }

        public List<string> GetPrjUsers(string projectCode)
        {
            string prjUsers = this.dal.GetPrjUsers(projectCode);
            if (string.IsNullOrEmpty(prjUsers))
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (string str2 in prjUsers.Split(new char[] { ',' }))
            {
                if (!string.IsNullOrEmpty(str2))
                {
                    list.Add(str2);
                }
            }
            return list;
        }

        public decimal GetQuantity(string projectCode, string resourceCode)
        {
            return this.dal.GetQuantity(projectCode, resourceCode);
        }

        public string GetResourceNameByCode(string resourceCode)
        {
            return this.dal.GetResourceNameByCode(resourceCode);
        }
    }
}

