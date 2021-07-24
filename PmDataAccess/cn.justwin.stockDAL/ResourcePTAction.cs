namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ResourcePTAction
    {
        public static int Add(ResourcePTModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Sm_Resource_PriceType(rptid,rptcode,rptname,rptexplain,rptisshow) ");
            builder.Append("values(@rptid,@rptcode,@rptname,@rptexplain,@rptIsShow) ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptid", SqlDbType.NVarChar, 50), new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@rptname", SqlDbType.NVarChar, 0x80), new SqlParameter("@rptexplain", SqlDbType.NVarChar, 0x400), new SqlParameter("@rptIsShow", SqlDbType.Int), new SqlParameter("@IsDefault", SqlDbType.Int) };
            commandParameters[0].Value = model.rptId;
            commandParameters[1].Value = model.rptCode;
            commandParameters[2].Value = model.rptName;
            commandParameters[3].Value = model.rptExplain;
            commandParameters[4].Value = model.rptIsShow;
            commandParameters[5].Value = model.IsDefault;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Delete(string rptId)
        {
            string cmdText = "delete Sm_Resource_PriceType where rptid=@rptId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rptId;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int GetForeignKeyAmount(string rptCode)
        {
            string cmdText = "select count(*) from Sm_Resource_Price where rptCode=@rptCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = rptCode;
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters).ToString());
        }

        public static DataTable GetList()
        {
            string cmdText = "select * from Sm_Resource_PriceType ";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static DataTable GetList(string rptId)
        {
            string cmdText = "select * from Sm_Resource_PriceType where rptid=@rptId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = rptId;
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int IsPreSenceOneData(string rptCode)
        {
            string cmdText = "select count(*) from Sm_Resource_PriceType where rptcode=@rptcode";
            SqlParameter parameter = new SqlParameter {
                ParameterName = "@rptcode",
                SqlDbType = SqlDbType.NVarChar,
                Size = 0x40,
                Value = rptCode,
                Direction = ParameterDirection.Input
            };
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString());
        }

        public static int Update(ResourcePTModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Sm_Resource_PriceType set ");
            builder.Append("rptcode=@rptcode,rptname=@rptname,rptexplain=@rptexplain,rptIsShow=@rptIsShow ");
            builder.Append("where rptid=@rptid");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptid", SqlDbType.NVarChar, 50), new SqlParameter("@rptcode", SqlDbType.NVarChar, 0x40), new SqlParameter("@rptname", SqlDbType.NVarChar, 0x80), new SqlParameter("@rptexplain", SqlDbType.NVarChar, 0x400), new SqlParameter("@rptIsShow", SqlDbType.Int), new SqlParameter("@IsDefault", SqlDbType.Int) };
            commandParameters[0].Value = model.rptId;
            commandParameters[1].Value = model.rptCode;
            commandParameters[2].Value = model.rptName;
            commandParameters[3].Value = model.rptExplain;
            commandParameters[4].Value = model.rptIsShow;
            commandParameters[5].Value = model.IsDefault;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int UpdateState(string rptid)
        {
            string cmdText = "update Sm_Resource_PriceType set rptIsShow=@rptIsShow where rptid=@rptid";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rptIsShow", SqlDbType.Int), new SqlParameter("@rptid", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = rptid;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

