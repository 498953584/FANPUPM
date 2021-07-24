namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class XpmCodeServices
    {
        public IList<XpmCode> GetBySignCode(string signCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                IList<XpmCode> list = new List<XpmCode>();
                foreach (XPM_Basic_CodeList list3 in (from c in entities.XPM_Basic_CodeList
                    join t in entities.XPM_Basic_CodeType on c.TypeID equals t.TypeID 
                    where (((t.SignCode == signCode) && c.IsValid) && c.IsVisible.HasValue) && c.IsVisible.Value
                    orderby c.CodeID
                    select c).ToList<XPM_Basic_CodeList>())
                {
                    list.Add((XpmCode) list3);
                }
                return list;
            }
        }
    }
}

