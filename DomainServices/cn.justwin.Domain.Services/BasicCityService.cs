namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class BasicCityService : Repository<BasicCity>
    {
        public BasicCity GetCityByCityID(string cityId)
        {
            Guid cityID = new Guid(cityId);
            return (from p in this
                where p.Id == cityID
                select p).FirstOrDefault<BasicCity>();
        }

        public DataTable GetDtbByProviceId(string provinceId)
        {
            return base.ExecuteQuery("SELECT * FROM Basic_City WHERE ProvinceId='" + provinceId + "'", new SqlParameter[0]);
        }

        public BasicCity GetModelByCityName(string cityName)
        {
            return (from p in this
                where p.Name == cityName
                select p).FirstOrDefault<BasicCity>();
        }
    }
}

