namespace cn.justwin.Fund.RequestPayment.DAL
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.RequestPayment.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class RequestPayDetailService
    {
        public bool Add(RequestPayDetailModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_RequestPay_Detail(");
            builder.Append("RPayMainID,RPayUID,PlanUID,ContractID,RPaysubject,RPayMoney,ReMark,RPayUser,RPayCause,isInterest)");
            builder.Append(" values (");
            builder.Append("@RPayMainID,@RPayUID,@PlanUID,@ContractID,@RPaysubject,@RPayMoney,@ReMark,@RPayUser,@RPayCause,@isInterest)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RPayUID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PlanUID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@RPaysubject", SqlDbType.VarChar, 30), new SqlParameter("@RPayMoney", SqlDbType.Money, 8), new SqlParameter("@ReMark", SqlDbType.VarChar, 100), new SqlParameter("@RPayUser", SqlDbType.VarChar, 50), new SqlParameter("@RPayCause", SqlDbType.Text), new SqlParameter("@isInterest", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.RPayMainID;
            commandParameters[1].Value = model.RPayUID;
            commandParameters[2].Value = model.PlanUID;
            commandParameters[3].Value = model.ContractID;
            commandParameters[4].Value = model.RPaysubject;
            commandParameters[5].Value = model.RPayMoney;
            commandParameters[6].Value = model.ReMark;
            commandParameters[7].Value = model.RPayUser;
            commandParameters[8].Value = model.RPayCause;
            commandParameters[9].Value = model.isInterest;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid RPayMainID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_RequestPay_Detail ");
            builder.Append(" where RPayMainID=@RPayMainID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayMainID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteBYUID(Guid RPayUID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_RequestPay_Detail ");
            builder.Append(" where RPayUID=@RPayUID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayUID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayUID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(Guid RPayUID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Fund_RequestPay_Detail");
            builder.Append(" where RPayUID=@RPayUID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayUID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayUID;
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows.Count > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM v_Fund_RPDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
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
            builder.Append(" * ");
            builder.Append(" FROM Fund_RequestPay_Detail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public RequestPayDetailModel GetModel(Guid RPayUID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from Fund_RequestPay_Detail ");
            builder.Append(" where RPayUID=@RPayUID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayUID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayUID;
            RequestPayDetailModel model = new RequestPayDetailModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["RPayMainID"].ToString() != "")
            {
                model.RPayMainID = new Guid(table.Rows[0]["RPayMainID"].ToString());
            }
            if (table.Rows[0]["RPayUID"].ToString() != "")
            {
                model.RPayUID = new Guid(table.Rows[0]["RPayUID"].ToString());
            }
            if (table.Rows[0]["PlanUID"].ToString() != "")
            {
                model.PlanUID = new Guid(table.Rows[0]["PlanUID"].ToString());
            }
            model.ContractID = table.Rows[0]["ContractID"].ToString();
            model.RPaysubject = table.Rows[0]["RPaysubject"].ToString();
            if (table.Rows[0]["RPayMoney"].ToString() != "")
            {
                model.RPayMoney = new decimal?(decimal.Parse(table.Rows[0]["RPayMoney"].ToString()));
            }
            model.ReMark = table.Rows[0]["ReMark"].ToString();
            model.RPayUser = table.Rows[0]["RPayUser"].ToString();
            if (table.Rows[0]["OrderID"].ToString() != "")
            {
                model.OrderID = int.Parse(table.Rows[0]["OrderID"].ToString());
            }
            model.RPayCause = table.Rows[0]["RPayCause"].ToString();
            if (table.Rows[0]["isInterest"].ToString() != "")
            {
                if ((table.Rows[0]["isInterest"].ToString() == "1") || (table.Rows[0]["isInterest"].ToString().ToLower() == "true"))
                {
                    model.isInterest = true;
                    return model;
                }
                model.isInterest = false;
            }
            return model;
        }

        public bool Update(RequestPayDetailModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_RequestPay_Detail set ");
            builder.Append("RPayMainID=@RPayMainID,");
            builder.Append("PlanUID=@PlanUID,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("RPaysubject=@RPaysubject,");
            builder.Append("RPayMoney=@RPayMoney,");
            builder.Append("ReMark=@ReMark,");
            builder.Append("RPayUser=@RPayUser,");
            builder.Append("RPayCause=@RPayCause,");
            builder.Append("isInterest=@isInterest");
            builder.Append(" where RPayUID=@RPayUID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RPayUID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PlanUID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@RPaysubject", SqlDbType.VarChar, 30), new SqlParameter("@RPayMoney", SqlDbType.Money, 8), new SqlParameter("@ReMark", SqlDbType.VarChar, 100), new SqlParameter("@RPayUser", SqlDbType.VarChar, 50), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@RPayCause", SqlDbType.Text), new SqlParameter("@isInterest", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.RPayMainID;
            commandParameters[1].Value = model.RPayUID;
            commandParameters[2].Value = model.PlanUID;
            commandParameters[3].Value = model.ContractID;
            commandParameters[4].Value = model.RPaysubject;
            commandParameters[5].Value = model.RPayMoney;
            commandParameters[6].Value = model.ReMark;
            commandParameters[7].Value = model.RPayUser;
            commandParameters[8].Value = model.OrderID;
            commandParameters[9].Value = model.RPayCause;
            commandParameters[10].Value = model.isInterest;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

