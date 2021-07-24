namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class OrderReportAction
    {
        public static DataTable QueryOrderForm(string formn, DateTime dBegin, DateTime dEnd)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@bdate", dBegin), new SqlParameter("@edate", dEnd) };
            return publicDbOpClass.ExecuteDataTable("p_rep_QueryForm" + formn, commandParameters);
        }

        public static DataTable QueryOrderForm1(string contactCode, string contactName, string moneytype, DateTime dBegin, DateTime dEnd)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contactCode", contactCode), new SqlParameter("@contactName", contactName), new SqlParameter("@moneytype", moneytype), new SqlParameter("@bdate", dBegin.ToShortDateString()), new SqlParameter("@edate", dEnd.ToShortDateString()) };
            return publicDbOpClass.ExecuteDataTable("p_rep_QueryForm1", commandParameters);
        }

        public static DataTable QueryOrderForm2(string contactCode, string contactName, string formCode, string moneytype, DateTime dBegin, DateTime dEnd)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contactCode", contactCode), new SqlParameter("@contactName", contactName), new SqlParameter("@formCode", formCode), new SqlParameter("@moneytype", moneytype), new SqlParameter("@bdate", dBegin.ToShortDateString()), new SqlParameter("@edate", dEnd.ToShortDateString()) };
            return publicDbOpClass.ExecuteDataTable("p_rep_QueryForm2", commandParameters);
        }

        public static DataTable QueryOrderForm3(string contactCode, string contactName, string formCode, string contractCode, string ckCode, string resourceType, string resourceName, string standard, string pinpai, string moneytype, DateTime dBegin, DateTime dEnd)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contactCode", contactCode), new SqlParameter("@contactName", contactName), new SqlParameter("@formCode", formCode), new SqlParameter("@contractCode", contractCode), new SqlParameter("@ckCode", ckCode), new SqlParameter("@resourceType", resourceType), new SqlParameter("@resourceName", resourceName), new SqlParameter("@standard", standard), new SqlParameter("@pinpai", pinpai), new SqlParameter("@moneytype", moneytype), new SqlParameter("@bdate", dBegin.ToShortDateString()), new SqlParameter("@edate", dEnd.ToShortDateString()) };
            return publicDbOpClass.ExecuteDataTable("p_rep_QueryForm3", commandParameters);
        }

        public static DataTable QueryOrderForm4(string contactCode, string contactName, string formCode, string contractCode, string ckCode, string resourceType, string resourceName, string standard, string pinpai, string moneytype, string prjCode, string prjName, DateTime dBegin, DateTime dEnd)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contactCode", contactCode), new SqlParameter("@contactName", contactName), new SqlParameter("@formCode", formCode), new SqlParameter("@contractCode", contractCode), new SqlParameter("@ckCode", ckCode), new SqlParameter("@resourceType", resourceType), new SqlParameter("@resourceName", resourceName), new SqlParameter("@standard", standard), new SqlParameter("@pinpai", pinpai), new SqlParameter("@moneytype", moneytype), new SqlParameter("@prjCode", prjCode), new SqlParameter("@prjName", prjName), new SqlParameter("@bdate", dBegin.ToShortDateString()), new SqlParameter("@edate", dEnd.ToShortDateString()) };
            return publicDbOpClass.ExecuteDataTable("p_rep_QueryForm4", commandParameters);
        }
    }
}

