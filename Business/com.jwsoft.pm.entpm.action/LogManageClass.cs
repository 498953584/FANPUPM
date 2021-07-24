namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Web.UI;

    public class LogManageClass
    {
        public static bool AddToLog(Page objPage, string strOperateInfo)
        {
            string str = "";
            string str2 = "";
            str = objPage.Session["yhdm"].ToString();
            str2 = objPage.Request.ServerVariables["REMOTE_HOST"].ToString();
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_Log", "i_ID")) + 1;
            return publicDbOpClass.NonQuerySqlString("insert into pt_Log(i_ID,v_UserIP,dt_OperateDate,v_UserID,v_OperateInfo) values(" + num.ToString() + ",'" + str + "',GetDate(),'" + str2 + "','" + strOperateInfo + "')");
        }

        public static bool DelLog(int iLogID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_Log where i_ID=" + iLogID.ToString());
        }

        public static bool DelLog(string strBeginDate, string strEndDate)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_Log where (dt_OperateDate>" + strBeginDate + ") and (dt_OperateDate<" + strEndDate + ")");
        }
    }
}

