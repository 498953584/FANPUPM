namespace cn.justwin.popupDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PopupSetting
    {
        public void Cancel(string userCode, string module)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("IF (SELECT COUNT(*) FROM PopupSetting ").AppendLine();
            builder.Append("\tWHERE UserCode = @userCode AND Module = @module) = 1").AppendLine();
            builder.Append("\tUPDATE PopupSetting SET IsValid = 0 ").AppendLine();
            builder.Append("\tWHERE UserCode = @userCode AND Module = @module").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@module", module) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetSetting(string userCode)
        {
            string str = "SELECT * FROM PopupSetting WHERE UserCode = @userCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), commandParameters);
        }

        public void Subscribe(string userCode, string module)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("IF (SELECT COUNT(*) FROM PopupSetting ").AppendLine();
            builder.Append("\tWHERE UserCode = @userCode AND Module = @module) = 1").AppendLine();
            builder.Append("\tUPDATE PopupSetting SET IsValid = 1 ").AppendLine();
            builder.Append("\tWHERE UserCode = @userCode AND Module = @module").AppendLine();
            builder.Append("ELSE").AppendLine();
            builder.Append("\tINSERT INTO PopupSetting(UserCode, Module, IsValid)").AppendLine();
            builder.Append("\tVALUES(@userCode, @module, 1)").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@module", module) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

