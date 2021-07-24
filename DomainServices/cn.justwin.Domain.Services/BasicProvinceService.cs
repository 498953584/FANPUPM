namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class BasicProvinceService : Repository<BasicProvince>
    {
        public DataTable GetALLProvince()
        {
            return base.ExecuteQuery("SELECT * FROM Basic_Province", new SqlParameter[0]);
        }

        public BasicProvince GetBasicProvince(string provinceId)
        {
            Guid provinceID = new Guid(provinceId);
            return (from p in this
                where p.Id == provinceID
                select p).FirstOrDefault<BasicProvince>();
        }

        public Dictionary<string, string> GetDictionaryOfProvice()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<BasicProvince> list = (from m in this select m).ToList<BasicProvince>();
            for (int i = 0; i < list.Count; i++)
            {
                dictionary.Add(list[i].Id.ToString(), list[i].Name);
            }
            return dictionary;
        }

        public BasicProvince GetModelByProvinceName(string provinceName)
        {
            return (from p in this
                where p.Name == provinceName
                select p).FirstOrDefault<BasicProvince>();
        }
    }
}

