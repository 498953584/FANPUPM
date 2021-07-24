namespace cn.justwin.Fund.RequestPayment.DAL
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.RequestPayment.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class RequestPayMainService
    {
        public bool Add(RequestPayMainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_RequestPay_Main(");
            builder.Append("RPayMainID,RPayCode,PrjGuid,FlowState,RPayUserCode,RPayDate,ReMark)");
            builder.Append(" values (");
            builder.Append("@RPayMainID,@RPayCode,@PrjGuid,@FlowState,@RPayUserCode,@RPayDate,@ReMark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RPayCode", SqlDbType.VarChar, 50), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@FlowState", SqlDbType.Int, 4), new SqlParameter("@RPayUserCode", SqlDbType.VarChar, 8), new SqlParameter("@RPayDate", SqlDbType.DateTime), new SqlParameter("@ReMark", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = model.RPayMainID;
            commandParameters[1].Value = model.RPayCode;
            commandParameters[2].Value = model.PrjGuid;
            commandParameters[3].Value = model.FlowState;
            commandParameters[4].Value = model.RPayUserCode;
            commandParameters[5].Value = model.RPayDate;
            commandParameters[6].Value = model.ReMark;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid RPayMainID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_RequestPay_Main ");
            builder.Append(" where RPayMainID=@RPayMainID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayMainID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(string RPayCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_RequestPay_Main");
            builder.Append(" where RPayCode=@RPayCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayCode", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = RPayCode;
            return (((int) SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable GetList(string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select *,(select v_xm from PT_yhmc where v_yhdm=a.RPayUserCode) as People,(select Isnull(sum(RPayMoney),0.00) from Fund_RequestPay_Detail where RPayMainID=a.RPayMainID)as Money ");
            builder.Append(" FROM Fund_RequestPay_Main a");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
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
            builder.Append(" FROM Fund_RequestPay_Main ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public RequestPayMainModel GetModel(Guid RPayMainID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from Fund_RequestPay_Main ");
            builder.Append(" where RPayMainID=@RPayMainID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = RPayMainID;
            RequestPayMainModel model = new RequestPayMainModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["RPayMainID"].ToString() != "")
            {
                model.RPayMainID = new Guid(table.Rows[0]["RPayMainID"].ToString());
            }
            model.RPayCode = table.Rows[0]["RPayCode"].ToString();
            if (table.Rows[0]["PrjGuid"].ToString() != "")
            {
                model.PrjGuid = new Guid(table.Rows[0]["PrjGuid"].ToString());
            }
            if (table.Rows[0]["FlowState"].ToString() != "")
            {
                model.FlowState = new int?(int.Parse(table.Rows[0]["FlowState"].ToString()));
            }
            model.RPayUserCode = table.Rows[0]["RPayUserCode"].ToString();
            if (table.Rows[0]["RPayDate"].ToString() != "")
            {
                model.RPayDate = new DateTime?(DateTime.Parse(table.Rows[0]["RPayDate"].ToString()));
            }
            model.ReMark = table.Rows[0]["ReMark"].ToString();
            return model;
        }

        public bool Update(RequestPayMainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_RequestPay_Main set ");
            builder.Append("RPayCode=@RPayCode,");
            builder.Append("PrjGuid=@PrjGuid,");
            builder.Append("RPayUserCode=@RPayUserCode,");
            builder.Append("RPayDate=@RPayDate,");
            builder.Append("ReMark=@ReMark");
            builder.Append(" where RPayMainID=@RPayMainID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@RPayMainID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RPayCode", SqlDbType.VarChar, 50), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@RPayUserCode", SqlDbType.VarChar, 8), new SqlParameter("@RPayDate", SqlDbType.DateTime), new SqlParameter("@ReMark", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = model.RPayMainID;
            commandParameters[1].Value = model.RPayCode;
            commandParameters[2].Value = model.PrjGuid;
            commandParameters[3].Value = model.RPayUserCode;
            commandParameters[4].Value = model.RPayDate;
            commandParameters[5].Value = model.ReMark;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

