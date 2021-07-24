namespace cn.justwin.Fund.PlanSummary.DAL
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.PlanSummary.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PlanSummaryDetailDAL
    {
        public bool Add(PlanSummaryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Plan_Summary_Detail(");
            builder.Append("DID,MID,PlanID,PrjID,LastPlanMoney,LastActualMoney,CurrPlanMoney,InputPeople,InputTime,Remark)");
            builder.Append(" values (");
            builder.Append("@DID,@MID,@PlanID,@PrjID,@LastPlanMoney,@LastActualMoney,@CurrPlanMoney,@InputPeople,@InputTime,@Remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@MID", SqlDbType.VarChar, 50), new SqlParameter("@PlanID", SqlDbType.VarChar, 50), new SqlParameter("@PrjID", SqlDbType.VarChar, 50), new SqlParameter("@LastPlanMoney", SqlDbType.Decimal, 9), new SqlParameter("@LastActualMoney", SqlDbType.Decimal, 9), new SqlParameter("@CurrPlanMoney", SqlDbType.Decimal, 9), new SqlParameter("@InputPeople", SqlDbType.VarChar, 50), new SqlParameter("@InputTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar) };
            commandParameters[0].Value = model.DID;
            commandParameters[1].Value = model.MID;
            commandParameters[2].Value = model.PlanID;
            commandParameters[3].Value = model.PrjID;
            commandParameters[4].Value = model.LastPlanMoney;
            commandParameters[5].Value = model.LastActualMoney;
            commandParameters[6].Value = model.CurrPlanMoney;
            commandParameters[7].Value = model.InputPeople;
            commandParameters[8].Value = model.InputTime;
            commandParameters[9].Value = model.Remark;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid DID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Plan_Summary_Detail ");
            builder.Append(" where DID=@DID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = DID;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public bool DeleteByMain(string MID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Plan_Summary_Detail ");
            builder.Append(" where MID=@MID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MID", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = MID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(Guid DID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_Plan_Summary_Detail");
            builder.Append(" where DID=@DID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = DID;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT DID,MID,PlanID as MonthPlanID,PrjID as PrjGuid, LastPlanMoney,LastActualMoney,CurrPlanMoney,");
            builder.Append(" InputPeople as OperatorCode,InputTime as OperateTime,FD.Remark,isnull((LastActualMoney/nullif(LastPlanMoney,0))*100,0) as BL");
            builder.Append(" ,P.PrjName,FP.FlowState");
            builder.Append(" FROM Fund_Plan_Summary_Detail FD");
            builder.Append(" LEFT JOIN Pt_prjInfo P ON FD.PrjID=P.PrjGuid");
            builder.Append(" LEFT JOIN Fund_Plan_MonthMain FP ON FP.MonthPlanID=FD.PlanID");
            builder.Append(" WHERE  P.IsValid='1'");
            if (strWhere.Trim() != "")
            {
                builder.Append(" AND " + strWhere);
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
            builder.Append(" DID,MID,PlanID,PrjID,LastPlanMoney,LastActualMoney,CurrPlanMoney,InputPeople,InputTime,Remark ");
            builder.Append(" FROM Fund_Plan_Summary_Detail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public PlanSummaryDetail GetModel(Guid DID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 DID,MID,PlanID,PrjID,LastPlanMoney,LastActualMoney,CurrPlanMoney,InputPeople,InputTime,Remark from Fund_Plan_Summary_Detail ");
            builder.Append(" where DID=@DID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = DID;
            PlanSummaryDetail detail = new PlanSummaryDetail();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["DID"].ToString() != "")
            {
                detail.DID = new Guid(table.Rows[0]["DID"].ToString());
            }
            detail.MID = table.Rows[0]["MID"].ToString();
            detail.PlanID = table.Rows[0]["PlanID"].ToString();
            detail.PrjID = table.Rows[0]["PrjID"].ToString();
            if (table.Rows[0]["LastPlanMoney"].ToString() != "")
            {
                detail.LastPlanMoney = new decimal?(decimal.Parse(table.Rows[0]["LastPlanMoney"].ToString()));
            }
            if (table.Rows[0]["LastActualMoney"].ToString() != "")
            {
                detail.LastActualMoney = new decimal?(decimal.Parse(table.Rows[0]["LastActualMoney"].ToString()));
            }
            if (table.Rows[0]["CurrPlanMoney"].ToString() != "")
            {
                detail.CurrPlanMoney = new decimal?(decimal.Parse(table.Rows[0]["CurrPlanMoney"].ToString()));
            }
            detail.InputPeople = table.Rows[0]["InputPeople"].ToString();
            if (table.Rows[0]["InputTime"].ToString() != "")
            {
                detail.InputTime = new DateTime?(DateTime.Parse(table.Rows[0]["InputTime"].ToString()));
            }
            detail.Remark = table.Rows[0]["Remark"].ToString();
            return detail;
        }

        public DataTable getPlanSummary(string strWhere, string PlanType)
        {
            string str = "select (select newid()) as DID,*,IsNull(cast(round((LastActualMoney/nullif(LastPlanMoney,0))*100,2) as decimal(18,2)),0) as BL from (";
            str = (str + " select (select prjName from Pt_prjinfo where PrjGuid=MPM.PrjGuid)as PrjName,Mpm.FlowState,MPM.Remark,MPM.OperatorCode,MPM.OperateTime,MPM.PlanDate,MPM.MonthPlanID,MPM.PlanType,MPM.PrjGuid," + " (select IsNull(sum(MD.PlanMoney),0) from Fund_Plan_MonthDetail as MD where MD.MonthPlanID=MPM.MonthPlanID) as CurrPlanMoney, ") + " (select IsNull(sum(MD.PlanMoney),0) from Fund_Plan_MonthDetail as MD where MD.MonthPlanID=" + " (select MonthPlanID from Fund_Plan_MonthMain as MP where MP.PrjGuid=MPM.PrjGuid and MP.PlanDate=dateadd(Month,-1,MPM.PlanDate) and MP.PlanType=MPM.PlanType) )as LastPlanMoney,";
            if (PlanType == "payout")
            {
                str = str + " (select sum(IsNull(Pay.PaymentMoney,0)) from dbo.Con_Payout_Payment as Pay where Pay.FlowState=1 and Pay.MonthPlanUID in (select UID from  dbo.Fund_Plan_MonthDetail where MonthPlanID=" + " (select MonthPlanID from Fund_Plan_MonthMain as MP where MP.PrjGuid=MPM.PrjGuid and MP.PlanDate=dateadd(Month,-1,MPM.PlanDate) and MP.PlanType=MPM.PlanType))) as LastActualMoney";
            }
            else
            {
                str = str + " (select sum(isnull(ic.CllectionPrice,0)) from dbo.Con_Incomet_Payment as ic where ic.MonthPlanUID in (select UID from  dbo.Fund_Plan_MonthDetail where MonthPlanID=" + "(select MonthPlanID from Fund_Plan_MonthMain as MP where MP.PrjGuid=MPM.PrjGuid and MP.PlanDate=dateadd(Month,-1,MPM.PlanDate) and MP.PlanType=MPM.PlanType)))as LastActualMoney ";
            }
            str = str + " from Fund_Plan_MonthMain as MPM" + ") as tab Where PrjName is not null";
            if (!string.IsNullOrEmpty(strWhere))
            {
                str = str + " AND " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public bool Update(PlanSummaryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Plan_Summary_Detail set ");
            builder.Append("MID=@MID,");
            builder.Append("PlanID=@PlanID,");
            builder.Append("PrjID=@PrjID,");
            builder.Append("LastPlanMoney=@LastPlanMoney,");
            builder.Append("LastActualMoney=@LastActualMoney,");
            builder.Append("CurrPlanMoney=@CurrPlanMoney,");
            builder.Append("InputPeople=@InputPeople,");
            builder.Append("InputTime=@InputTime,");
            builder.Append("Remark=@Remark");
            builder.Append(" where DID=@DID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@MID", SqlDbType.VarChar, 50), new SqlParameter("@PlanID", SqlDbType.VarChar, 50), new SqlParameter("@PrjID", SqlDbType.VarChar, 50), new SqlParameter("@LastPlanMoney", SqlDbType.Decimal, 9), new SqlParameter("@LastActualMoney", SqlDbType.Decimal, 9), new SqlParameter("@CurrPlanMoney", SqlDbType.Decimal, 9), new SqlParameter("@InputPeople", SqlDbType.VarChar, 50), new SqlParameter("@InputTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar) };
            commandParameters[0].Value = model.DID;
            commandParameters[1].Value = model.MID;
            commandParameters[2].Value = model.PlanID;
            commandParameters[3].Value = model.PrjID;
            commandParameters[4].Value = model.LastPlanMoney;
            commandParameters[5].Value = model.LastActualMoney;
            commandParameters[6].Value = model.CurrPlanMoney;
            commandParameters[7].Value = model.InputPeople;
            commandParameters[8].Value = model.InputTime;
            commandParameters[9].Value = model.Remark;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }
    }
}

