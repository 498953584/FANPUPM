namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class basicSetDal
    {
        public static int addSm_Set(basicSetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sm_set values(@paraid,@paraname,@paravalue,@remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@paraid", SqlDbType.NVarChar, 50), new SqlParameter("@paraname", SqlDbType.NVarChar, 100), new SqlParameter("@paravalue", SqlDbType.NVarChar, 100), new SqlParameter("@remark", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = Guid.NewGuid().ToString();
            commandParameters[1].Value = model.Paraname;
            commandParameters[2].Value = model.Paravalue;
            commandParameters[3].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int deleteSm_Set(string paraid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete sm_set where paraid=@paraid");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@paraid", paraid) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static string getSm_setValue(string paraname)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select paravalue from  sm_set where paraname=@paraname");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@paraname", paraname) };
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters));
        }

        public static DataTable getSm_Table()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select paraname, paravalue,remark from  sm_set ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static int UpdateSetByKeyVal(string key, string val)
        {
            string cmdText = "UPDATE Sm_Set SET paravalue = @paravalue where paraname =@paraname ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@paravalue", val), new SqlParameter("@paraname", key) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static int updateSm_Set(basicSetModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_set set paravalue=@paravalue ,remark=@ramark where paraname=@paraname ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@paravalue", model.Paravalue), new SqlParameter("@ramark", model.Remark), new SqlParameter("@paraname", model.Paraname) };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

