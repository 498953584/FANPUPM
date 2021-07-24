namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class InExPlanBLL
    {
        private InExPlan iePlan = new InExPlan();

        public int Add(InExPlanModel ieModel)
        {
            SqlTransaction trans = null;
            return this.iePlan.Add(trans, ieModel);
        }

        public int Del(SqlTransaction trans, string id)
        {
            return this.iePlan.Delete(trans, id);
        }

        public DataTable GetList()
        {
            string sql = "select f.*,case when IEPtype=0 then '月度' when IEPtype=1 then '季度' else '年度' end as planType,p.prjName from fund_InExPlan as f inner join dbo.PT_PrjInfo as p on f.prjNum=convert(nvarchar(36),p.prjGuid) order by IEPdate desc";
            return DBHelper.GetTable(sql);
        }

        public DataTable GetList(string code, string coedname, string time1, string time2, string prijname)
        {
            string sql = "select f.[ID],p.prjName ,f.[IEPNum] ,f.[IEPname],f.[IEPtype] ,f.[IEPdate] ,f.[prjNum] ,f.[state],";
            sql = sql + "case when IEPtype=0 then '月度' when IEPtype=1 then '季度' else '年度' end as planType from fund_InExPlan " + "as f inner join dbo.PT_PrjInfo as p on f.prjNum=convert(nvarchar(36),p.prjGuid) where 1=1";
            if (!string.IsNullOrEmpty(code))
            {
                sql = sql + " and f.[IEPNum]  like '%" + code + "%'";
            }
            if (!string.IsNullOrEmpty(coedname))
            {
                sql = sql + " and f.[IEPname] like '%" + coedname + "%'";
            }
            if (!string.IsNullOrEmpty(time1))
            {
                sql = sql + " and f.[IEPdate]>='" + time1 + "'";
            }
            if (!string.IsNullOrEmpty(time2))
            {
                sql = sql + " and f.[IEPdate]<='" + time2 + "'";
            }
            if (!string.IsNullOrEmpty(prijname))
            {
                sql = sql + " and  f.prjNum  like '%" + prijname + "%'";
            }
            return DBHelper.GetTable(sql);
        }

        public InExPlanModel GetModel(string id)
        {
            return this.iePlan.GetModel(id);
        }

        public int Update(InExPlanModel ieModel)
        {
            SqlTransaction trans = null;
            return this.iePlan.Update(trans, ieModel);
        }
    }
}

