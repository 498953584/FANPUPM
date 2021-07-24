namespace cn.justwin.Fund.PlanSummary.DAL
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.PlanSummary.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PlanSummaryMainDAL
    {
        public bool Add(PlanSummaryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Plan_Summary_Main(");
            builder.Append("MID,PlanName,Reporter,ReportTime,FlowState,remark,PlanType)");
            builder.Append(" values (");
            builder.Append("@MID,@PlanName,@Reporter,@ReportTime,@FlowState,@remark,@PlanType)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PlanName", SqlDbType.VarChar, 50), new SqlParameter("@Reporter", SqlDbType.VarChar, 50), new SqlParameter("@ReportTime", SqlDbType.DateTime), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@remark", SqlDbType.VarChar), new SqlParameter("@PlanType", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = model.MID;
            commandParameters[1].Value = model.PlanName;
            commandParameters[2].Value = model.Reporter;
            commandParameters[3].Value = model.ReportTime;
            commandParameters[4].Value = model.FlowState;
            commandParameters[5].Value = model.remark;
            commandParameters[6].Value = model.PlanType;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid MID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Plan_Summary_Main ");
            builder.Append(" where MID=@MID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = MID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(string PlanName, string PlanType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_Plan_Summary_Main");
            builder.Append(" where PlanName=@PlanName and PlanType=@PlanType and FlowState!=-2 ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlanName", SqlDbType.VarChar, 50), new SqlParameter("@PlanType", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = PlanName;
            commandParameters[1].Value = PlanType;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ma.*,(select isnull(sum(de.currPlanMoney),0) from Fund_Plan_Summary_Detail as de where de.MID=ma.MID) as TotalMoney ");
            builder.Append(" FROM Fund_Plan_Summary_Main as ma");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by ma.PlanName desc");
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
            builder.Append(" MID,PlanName,Reporter,ReportTime,FlowState,remark,PlanType ");
            builder.Append(" FROM Fund_Plan_Summary_Main ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public PlanSummaryMain GetModel(Guid MID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 MID,PlanName,Reporter,ReportTime,FlowState,remark,PlanType from Fund_Plan_Summary_Main ");
            builder.Append(" where MID=@MID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = MID;
            PlanSummaryMain main = new PlanSummaryMain();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["MID"].ToString() != "")
            {
                main.MID = new Guid(table.Rows[0]["MID"].ToString());
            }
            main.PlanName = table.Rows[0]["PlanName"].ToString();
            main.Reporter = table.Rows[0]["Reporter"].ToString();
            if (table.Rows[0]["ReportTime"].ToString() != "")
            {
                main.ReportTime = new DateTime?(DateTime.Parse(table.Rows[0]["ReportTime"].ToString()));
            }
            if (table.Rows[0]["FlowState"].ToString() != "")
            {
                main.FlowState = new int?(int.Parse(table.Rows[0]["FlowState"].ToString()));
            }
            main.remark = table.Rows[0]["remark"].ToString();
            main.PlanType = table.Rows[0]["PlanType"].ToString();
            return main;
        }

        public bool Update(PlanSummaryMain model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Plan_Summary_Main set ");
            builder.Append("Reporter=@Reporter,");
            builder.Append("ReportTime=@ReportTime,");
            builder.Append("remark=@remark ");
            builder.Append(" where MID=@MID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Reporter", SqlDbType.VarChar, 50), new SqlParameter("@ReportTime", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.VarChar) };
            commandParameters[0].Value = model.MID;
            commandParameters[1].Value = model.Reporter;
            commandParameters[2].Value = model.ReportTime;
            commandParameters[3].Value = model.remark;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public void UpdateFlow(string PlanGuid)
        {
            SqlHelper.ExecuteScalar(CommandType.Text, (" update Fund_Plan_MonthMain set FlowState=-3 where MonthPlanID='" + PlanGuid + "'").ToString(), null);
        }
    }
}

