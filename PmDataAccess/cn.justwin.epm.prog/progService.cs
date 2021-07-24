namespace cn.justwin.epm.prog
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class progService
    {
        public bool Exists(int ProgSortCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Prj_ProgSort");
            builder.Append(" where ProgSortCode=@ProgSortCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProgSortCode", SqlDbType.Int, 4) };
            commandParameters[0].Value = ProgSortCode;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public bool Exists(string ProgSortName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Prj_ProgSort");
            builder.Append(" where ProgSortName=@ProgSortName");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProgSortName", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = ProgSortName;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }
    }
}

