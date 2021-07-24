namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data.SqlClient;

    public class BasicHelperAction
    {
        private const string SP_GETNEXTID = "XPM_P_Basic_GetNextID";

        public static int GetNextID(string signCode)
        {
            object obj2 = new object();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SignCode", signCode) };
            obj2 = publicDbOpClass.ExecuteScalar("XPM_P_Basic_GetNextID", commandParameters);
            if (obj2 == DBNull.Value)
            {
                return 0;
            }
            return (int) obj2;
        }
    }
}

