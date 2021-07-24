namespace cn.justwin.Fund.PrjLoad
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PrjLoadService
    {
        public int Add(PrjLoadModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Prj_Loan(");
            builder.Append("LoanID,LoanCode,PrjGuid,LoanDate,LoanMan,LoanFund,LoanCause,PlanReturnDate,LoanRate,FlowState,Remark,ReturnState)");
            builder.Append(" values (");
            builder.Append("@LoanID,@LoanCode,@PrjGuid,@LoanDate,@LoanMan,@LoanFund,@LoanCause,@PlanReturnDate,@LoanRate,@FlowState,@Remark,@ReturnState)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@LoanCode", SqlDbType.VarChar, 50), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@LoanDate", SqlDbType.DateTime), new SqlParameter("@LoanMan", SqlDbType.VarChar, 50), new SqlParameter("@LoanFund", SqlDbType.Decimal, 9), new SqlParameter("@LoanCause", SqlDbType.VarChar, 500), new SqlParameter("@PlanReturnDate", SqlDbType.DateTime), new SqlParameter("@LoanRate", SqlDbType.Decimal, 9), new SqlParameter("@FlowState", SqlDbType.Int), new SqlParameter("@Remark", SqlDbType.VarChar, 500), new SqlParameter("@ReturnState", SqlDbType.Int) };
            commandParameters[0].Value = model.LoanID;
            commandParameters[1].Value = model.LoanCode;
            commandParameters[2].Value = model.PrjGuid;
            if (model.LoanDate.HasValue)
            {
                commandParameters[3].Value = model.LoanDate;
            }
            else
            {
                commandParameters[3].Value = DBNull.Value;
            }
            commandParameters[4].Value = model.LoanMan;
            commandParameters[5].Value = model.LoanFund;
            commandParameters[6].Value = model.LoanCause;
            if (model.PlanReturnDate.HasValue)
            {
                commandParameters[7].Value = model.PlanReturnDate;
            }
            else
            {
                commandParameters[7].Value = DBNull.Value;
            }
            commandParameters[8].Value = model.LoanRate;
            if (model.FlowState.HasValue)
            {
                commandParameters[9].Value = model.FlowState;
            }
            else
            {
                commandParameters[9].Value = -1;
            }
            commandParameters[10].Value = model.Remark;
            commandParameters[11].Value = model.ReturnState;
            object obj2 = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public bool Delete(Guid LoanID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Loan ");
            builder.Append(" where LoanID=@LoanID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = LoanID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool DeleteList(string LoanIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Loan ");
            builder.Append(" where LoanID in (" + LoanIDlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(Guid LoanID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_Prj_Loan");
            builder.Append(" where LoanID=@LoanID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = LoanID;
            return ((SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters) != null) && (SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString() != ""));
        }

        public DataTable GetLoadListByPrjGuid(string _PrjGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT LoanID, fpl.LoanCode,fpl.PrjGuid,LoanDate, ");
            builder.Append("(SELECT top 1 py.v_xm FROM PT_yhmc py WHERE py.v_yhdm=fpl.LoanMan) AS ");
            builder.Append("LoanManName,LoanFund,LoanCause,PlanReturnDate,LoanRate, ");
            builder.Append(" fpl.Remark,fpl.FlowState,ReturnDate,ReturnMan ,");
            builder.Append(" (SELECT ppi.PrjName FROM PT_PrjInfo ppi WHERE ppi.PrjGuid=fpl.PrjGuid) AS PrjName ");
            builder.Append(" FROM Fund_Prj_Loan fpl ");
            builder.Append(" WHERE (select PrjGuid from Fund_Prj_Accoun where accountid='" + _PrjGuid + "') like '%'+CONVERT(VARCHAR(50),fpl.PrjGuid)+'%' ");
            builder.Append(" ORDER BY fpl.LoanDate DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public PrjLoadModel GetModel(Guid LoanID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP 1 fpl.LoanID,fpl.LoanCode,fpl.PrjGuid, ");
            builder.Append("fpl.LoanDate,fpl.LoanMan, ");
            builder.Append("fpl.LoanFund,fpl.LoanCause, ");
            builder.Append("fpl.PlanReturnDate,fpl.LoanRate, ");
            builder.Append("fpl.Remark,fpl.FlowState, ");
            builder.Append("fpl.ReturnDate,fpl.ReturnMan, ");
            builder.Append("fpl.ReturnState,");
            builder.Append("py.v_xm AS LoanManName,");
            builder.Append("pyc.v_xm AS ReturnManName  ");
            builder.Append(",ppi.PrjName  ");
            builder.Append("FROM Fund_Prj_Loan fpl  ");
            builder.Append("LEFT JOIN PT_yhmc py ON fpl.LoanMan=py.v_yhdm  ");
            builder.Append(" LEFT JOIN PT_PrjInfo ppi ON ppi.PrjGuid=fpl.PrjGuid  ");
            builder.Append(" LEFT JOIN PT_yhmc pyc ON fpl.ReturnMan=py.v_yhdm ");
            builder.Append(" where LoanID=@LoanID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = LoanID;
            PrjLoadModel model = new PrjLoadModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["LoanID"] != null) && (table.Rows[0]["LoanID"].ToString() != ""))
            {
                model.LoanID = new Guid(table.Rows[0]["LoanID"].ToString());
            }
            if ((table.Rows[0]["LoanCode"] != null) && (table.Rows[0]["LoanCode"].ToString() != ""))
            {
                model.LoanCode = table.Rows[0]["LoanCode"].ToString();
            }
            if ((table.Rows[0]["PrjGuid"] != null) && (table.Rows[0]["PrjGuid"].ToString() != ""))
            {
                model.PrjGuid = new Guid(table.Rows[0]["PrjGuid"].ToString());
            }
            if ((table.Rows[0]["LoanDate"] != null) && (table.Rows[0]["LoanDate"].ToString() != ""))
            {
                model.LoanDate = new DateTime?(DateTime.Parse(table.Rows[0]["LoanDate"].ToString()));
            }
            if ((table.Rows[0]["LoanMan"] != null) && (table.Rows[0]["LoanMan"].ToString() != ""))
            {
                model.LoanMan = table.Rows[0]["LoanMan"].ToString();
            }
            if ((table.Rows[0]["LoanFund"] != null) && (table.Rows[0]["LoanFund"].ToString() != ""))
            {
                model.LoanFund = new decimal?(decimal.Parse(table.Rows[0]["LoanFund"].ToString()));
            }
            if ((table.Rows[0]["LoanCause"] != null) && (table.Rows[0]["LoanCause"].ToString() != ""))
            {
                model.LoanCause = table.Rows[0]["LoanCause"].ToString();
            }
            if ((table.Rows[0]["PlanReturnDate"] != null) && (table.Rows[0]["PlanReturnDate"].ToString() != ""))
            {
                model.PlanReturnDate = new DateTime?(DateTime.Parse(table.Rows[0]["PlanReturnDate"].ToString()));
            }
            if ((table.Rows[0]["LoanRate"] != null) && (table.Rows[0]["LoanRate"].ToString() != ""))
            {
                model.LoanRate = new decimal?(decimal.Parse(table.Rows[0]["LoanRate"].ToString()));
            }
            if ((table.Rows[0]["Remark"] != null) && (table.Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = table.Rows[0]["Remark"].ToString();
            }
            if ((table.Rows[0]["FlowState"] != null) && (table.Rows[0]["FlowState"].ToString() != ""))
            {
                model.FlowState = new int?(int.Parse(table.Rows[0]["FlowState"].ToString()));
            }
            if ((table.Rows[0]["ReturnDate"] != null) && (table.Rows[0]["ReturnDate"].ToString() != ""))
            {
                model.ReturnDate = new DateTime?(DateTime.Parse(table.Rows[0]["ReturnDate"].ToString()));
            }
            if ((table.Rows[0]["ReturnMan"] != null) && (table.Rows[0]["ReturnMan"].ToString() != ""))
            {
                model.ReturnMan = table.Rows[0]["ReturnMan"].ToString();
            }
            if ((table.Rows[0]["LoanManName"] != null) && (table.Rows[0]["LoanManName"].ToString() != ""))
            {
                model.LoanmanName = table.Rows[0]["LoanManName"].ToString();
            }
            if ((table.Rows[0]["ReturnManName"] != null) && (table.Rows[0]["ReturnManName"].ToString() != ""))
            {
                model.ReturnmanName = table.Rows[0]["ReturnManName"].ToString();
            }
            if ((table.Rows[0]["PrjName"] != null) && (table.Rows[0]["PrjName"].ToString() != ""))
            {
                model.PrjName = table.Rows[0]["PrjName"].ToString();
            }
            model.ReturnState = 0;
            if (table.Rows[0]["ReturnState"].ToString() != "")
            {
                model.ReturnState = new int?(Convert.ToInt32(table.Rows[0]["ReturnState"].ToString()));
            }
            return model;
        }

        public bool Update(PrjLoadModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Loan set ");
            builder.Append("LoanCode=@LoanCode,");
            builder.Append("PrjGuid=@PrjGuid, ");
            builder.Append("LoanDate=@LoanDate,");
            builder.Append("LoanMan=@LoanMan,");
            builder.Append("LoanFund=@LoanFund,");
            builder.Append("LoanCause=@LoanCause,");
            builder.Append("PlanReturnDate=@PlanReturnDate,");
            builder.Append("LoanRate=@LoanRate,");
            builder.Append("Remark=@Remark");
            builder.Append(" where LoanID=@LoanID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanCode", SqlDbType.VarChar, 50), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier), new SqlParameter("@LoanDate", SqlDbType.DateTime), new SqlParameter("@LoanMan", SqlDbType.VarChar, 50), new SqlParameter("@LoanFund", SqlDbType.Decimal, 9), new SqlParameter("@LoanCause", SqlDbType.VarChar, 500), new SqlParameter("@PlanReturnDate", SqlDbType.DateTime), new SqlParameter("@LoanRate", SqlDbType.Decimal, 9), new SqlParameter("@Remark", SqlDbType.VarChar, 500), new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.LoanCode;
            commandParameters[1].Value = model.PrjGuid;
            commandParameters[2].Value = model.LoanDate;
            commandParameters[3].Value = model.LoanMan;
            commandParameters[4].Value = model.LoanFund;
            commandParameters[5].Value = model.LoanCause;
            commandParameters[6].Value = model.PlanReturnDate;
            commandParameters[7].Value = model.LoanRate;
            commandParameters[8].Value = model.Remark;
            commandParameters[9].Value = model.LoanID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public int updateReturnState(SqlTransaction trans, Guid LoadId, int state)
        {
            string cmdText = "update Fund_Prj_Loan set ReturnState=@state where LoanID=@LoadId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@state", SqlDbType.Int), new SqlParameter("@LoadId", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = state;
            commandParameters[1].Value = LoadId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
        }
    }
}

