namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;

    public class PmTypeAction
    {
        public static string GetPmType(string UserCode)
        {
            return publicDbOpClass.ExecuteScalar("select PmSet from pt_login where v_yhdm = '" + UserCode + "'").ToString();
        }

        public static bool SetPmType(string UserCode, string PmSet)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set PmSet = " + PmSet + " where v_yhdm = '" + UserCode + "'");
        }
    }
}

