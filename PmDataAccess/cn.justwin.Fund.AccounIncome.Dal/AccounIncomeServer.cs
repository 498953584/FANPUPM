namespace cn.justwin.Fund.AccounIncome.Dal
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.AccounIncome.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AccounIncomeServer
    {
        public bool Add(AccounIncomeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Prj_Accoun_Income(");
            builder.Append("InUid,PrjGuid,InCode,PlanUid,ContractID,Subject,GetDate,GetMoney,InMoney,InPeople,InDate,Remark,FlowState)");
            builder.Append(" values (");
            builder.Append("@InUid,@PrjGuid,@InCode,@PlanUid,@ContractID,@Subject,@GetDate,@GetMoney,@InMoney,@InPeople,@InDate,@Remark,@FlowState)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InUid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjGuid", SqlDbType.VarChar, 100), new SqlParameter("@InCode", SqlDbType.VarChar, 100), new SqlParameter("@PlanUid", SqlDbType.VarChar, 100), new SqlParameter("@ContractID", SqlDbType.VarChar, 100), new SqlParameter("@Subject", SqlDbType.VarChar, 100), new SqlParameter("@GetDate", SqlDbType.DateTime), new SqlParameter("@GetMoney", SqlDbType.Decimal, 9), new SqlParameter("@InMoney", SqlDbType.Decimal, 9), new SqlParameter("@InPeople", SqlDbType.VarChar, 100), new SqlParameter("@InDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar), new SqlParameter("@FlowState", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.InUid;
            commandParameters[1].Value = model.PrjGuid;
            commandParameters[2].Value = model.InCode;
            commandParameters[3].Value = model.PlanUid;
            commandParameters[4].Value = model.ContractID;
            commandParameters[5].Value = model.Subject;
            commandParameters[6].Value = model.GetDate;
            commandParameters[7].Value = model.GetMoney;
            commandParameters[8].Value = model.InMoney;
            commandParameters[9].Value = model.InPeople;
            commandParameters[10].Value = model.InDate;
            commandParameters[11].Value = model.Remark;
            commandParameters[12].Value = model.FlowState;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid InUid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Accoun_Income ");
            builder.Append(" where InUid=@InUid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InUid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = InUid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(string Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_Prj_Accoun_Income");
            builder.Append(" where InCode=@InCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InCode", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = Code;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable GetAllNews(string PayGuid)
        {
            string str = "select *,(select PlanDate from  dbo.Fund_Plan_MonthMain where MonthPlanId=p2.MonthPlanId)as PlanDate from Con_Incomet_Payment as p1 left join Fund_Plan_MonthDetail as p2 on p2.Uid=p1.MonthPlanUid ";
            str = str + " where id='" + PayGuid + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from (select acc.InUid,acc.prjguid,(select PrjName from Pt_prjinfo where prjguid =acc.prjguid)as prjname,");
            builder.Append(" con.cllectionCode,(select ContractName from Con_Incomet_Contract where ContractID=con.ContractID) as ContName, ");
            builder.Append(" case acc.Subject when 0 then '合同入账' when 1 then '启动资金' when 2 then '其他收入' end as Subject,");
            builder.Append(" (select c.V_xm from PT_yhmc as c where c.v_yhdm=acc.Inpeople) as people,acc.[getdate],acc.InMoney,acc.Remark,acc.FlowState,fpa.accountNum,fpa.acountName ");
            builder.Append(" from  dbo.Fund_Prj_Accoun_Income as acc left join Fund_Plan_MonthDetail as pl on acc.PlanUid=pl.UID left join Fund_Prj_Accoun as fpa ON fpa.PrjGuid LIKE '%' +acc.PrjGuid+'%' ");
            builder.Append(" left join dbo.Con_Incomet_Payment as con on acc.ContractID=con.ID ) as tab ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" acc.*,(select c.V_xm from PT_yhmc as c where c.v_yhdm=acc.Inpeople) as people,(select ( CONVERT(VARCHAR(30),a.PlanYear)+'年'+ CONVERT(VARCHAR(30),a.PlanMonth)+'月') as PlanName from Fund_Plan_MonthMain as a where a.MonthPlanId=pl.MonthPlanId) as PlanName,con.cllectionCode,ContractName ");
            builder.Append(" from  dbo.Fund_Prj_Accoun_Income as acc left join Fund_Plan_MonthDetail as pl on acc.PlanUid=pl.UID left join dbo.Con_Incomet_Payment as con on acc.ContractID=con.ID left join Con_Incomet_Contract on con.ContractID=Con_Incomet_Contract.ContractID  ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public AccounIncomeModel GetModel(Guid InUid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 InUid,PrjGuid,InCode,PlanUid,Fund_Prj_Accoun_Income.ContractID,Subject,GetDate,GetMoney,InMoney,InPeople,InDate,Fund_Prj_Accoun_Income.Remark,Fund_Prj_Accoun_Income.FlowState,ContractName from Fund_Prj_Accoun_Income ");
            builder.Append(" left join Con_Incomet_Payment on Fund_Prj_Accoun_Income.ContractID=Con_Incomet_Payment.ID ");
            builder.Append(" left join Con_Incomet_Contract on Con_Incomet_Payment.ContractID=Con_Incomet_Contract.ContractID ");
            builder.Append(" where InUid=@InUid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InUid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = InUid;
            AccounIncomeModel model = new AccounIncomeModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["InUid"].ToString() != "")
            {
                model.InUid = new Guid(table.Rows[0]["InUid"].ToString());
            }
            model.PrjGuid = table.Rows[0]["PrjGuid"].ToString();
            model.InCode = table.Rows[0]["InCode"].ToString();
            model.PlanUid = table.Rows[0]["PlanUid"].ToString();
            model.ContractID = table.Rows[0]["ContractID"].ToString();
            model.Subject = table.Rows[0]["Subject"].ToString();
            if (table.Rows[0]["GetDate"].ToString() != "")
            {
                model.GetDate = new DateTime?(DateTime.Parse(table.Rows[0]["GetDate"].ToString()));
            }
            if (table.Rows[0]["GetMoney"].ToString() != "")
            {
                model.GetMoney = new decimal?(decimal.Parse(table.Rows[0]["GetMoney"].ToString()));
            }
            if (table.Rows[0]["InMoney"].ToString() != "")
            {
                model.InMoney = new decimal?(decimal.Parse(table.Rows[0]["InMoney"].ToString()));
            }
            model.InPeople = table.Rows[0]["InPeople"].ToString();
            if (table.Rows[0]["InDate"].ToString() != "")
            {
                model.InDate = new DateTime?(DateTime.Parse(table.Rows[0]["InDate"].ToString()));
            }
            model.Remark = table.Rows[0]["Remark"].ToString();
            if (table.Rows[0]["FlowState"].ToString() != "")
            {
                model.FlowState = new int?(int.Parse(table.Rows[0]["FlowState"].ToString()));
            }
            model.ContractName = table.Rows[0]["ContractName"].ToString();
            return model;
        }

        public bool Update(AccounIncomeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Accoun_Income set ");
            builder.Append("PrjGuid=@PrjGuid,");
            builder.Append("InCode=@InCode,");
            builder.Append("PlanUid=@PlanUid,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("Subject=@Subject,");
            builder.Append("GetDate=@GetDate,");
            builder.Append("GetMoney=@GetMoney,");
            builder.Append("InMoney=@InMoney,");
            builder.Append("InPeople=@InPeople,");
            builder.Append("InDate=@InDate,");
            builder.Append("Remark=@Remark ");
            builder.Append(" where InUid=@InUid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@InUid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjGuid", SqlDbType.VarChar, 100), new SqlParameter("@InCode", SqlDbType.VarChar, 100), new SqlParameter("@PlanUid", SqlDbType.VarChar, 100), new SqlParameter("@ContractID", SqlDbType.VarChar, 100), new SqlParameter("@Subject", SqlDbType.VarChar, 100), new SqlParameter("@GetDate", SqlDbType.DateTime), new SqlParameter("@GetMoney", SqlDbType.Decimal, 9), new SqlParameter("@InMoney", SqlDbType.Decimal, 9), new SqlParameter("@InPeople", SqlDbType.VarChar, 100), new SqlParameter("@InDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar) };
            commandParameters[0].Value = model.InUid;
            commandParameters[1].Value = model.PrjGuid;
            commandParameters[2].Value = model.InCode;
            commandParameters[3].Value = model.PlanUid;
            commandParameters[4].Value = model.ContractID;
            commandParameters[5].Value = model.Subject;
            commandParameters[6].Value = model.GetDate;
            commandParameters[7].Value = model.GetMoney;
            commandParameters[8].Value = model.InMoney;
            commandParameters[9].Value = model.InPeople;
            commandParameters[10].Value = model.InDate;
            commandParameters[11].Value = model.Remark;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

