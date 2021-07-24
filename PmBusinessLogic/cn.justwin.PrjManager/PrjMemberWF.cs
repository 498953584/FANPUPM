namespace cn.justwin.PrjManager
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class PrjMemberWF
    {
        public static void Add(Guid prjGuid)
        {
            string str = "\r\n\t\t\t\tIF (SELECT COUNT(*) FROM PT_PrjMemberWF WHERE PrjGuid = @PrjGuid) = 0\r\n\t\t\t\t\tINSERT INTO PT_PrjMemberWF(PrjGuid, FlowState) VALUES(@PrjGuid, -1)\r\n\t\t\t";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid", prjGuid) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), commandParameters);
        }
    }
}

