namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [NHibernateContext, ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), ServiceContract]
    public class PCPettyCashService : Repository<PCPettyCash>
    {
        public void Delete(IList<string> idLst)
        {
            foreach (string str in idLst)
            {
                base.Delete(this.GetById(str));
            }
        }

        [WebGet(UriTemplate="/PCPettyCash/{id}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public PCPettyCash GetById(string id)
        {
            return (from pc in this
                where pc.Id == id
                select pc).FirstOrDefault<PCPettyCash>();
        }

        public decimal GetCashTotal(int year, string userCode)
        {
            decimal num = 0M;
            IQueryable<decimal> source = from p in this
                where ((p.ApplicationDate.Year == year) && (p.Applicant == userCode)) && (p.FlowState == 1)
                select p.Cash;
            if (source.Count<decimal>() > 0)
            {
                num = source.Sum();
            }
            return num;
        }

        public DataTable GetMonthCash(int year, string userCode)
        {
            List<PCPettyCash> source = (from p in this
                where ((p.ApplicationDate.Year == year) && (p.Applicant == userCode)) && (p.FlowState == 1)
                select p).ToList<PCPettyCash>();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("No"));
            table.Columns.Add(new DataColumn("Date"));
            table.Columns.Add(new DataColumn("Matter"));
            table.Columns.Add(new DataColumn("Cash", typeof(decimal)));
            Func<PCPettyCash, bool> predicate = null;
            for (int i = 1; i <= 12; i++)
            {
                if (predicate == null)
                {
                    predicate = p => p.ApplicationDate.Month == i;
                }
                List<PCPettyCash> list2 = source.Where<PCPettyCash>(predicate).ToList<PCPettyCash>();
                List<string> values = (from p in list2 select p.Matter).ToList<string>();
                decimal num = ((IEnumerable<decimal>) (from p in list2 select p.Cash).ToList<decimal>()).Sum();
                DataRow row = table.NewRow();
                row["No"] = i;
                row["Date"] = string.Format("{0}年{1}月", year, i);
                row["Matter"] = string.Join("<br />", values);
                row["Cash"] = num;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

