namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class CustomManageAction
    {
        public static bool AddCustomInfo(Hashtable htInfo)
        {
            return publicDbOpClass.Insert("[CPM_Custom]", htInfo);
        }

        public static bool DelCustomInfo(int customId)
        {
            return publicDbOpClass.NonQuerySqlString(" Delete from CPM_Custom where CustomID = " + customId.ToString());
        }

        public static DataTable QueryCustomInfo(int customId)
        {
            return publicDbOpClass.DataTableQuary(" select * from CPM_Custom where CustomID = " + customId.ToString());
        }

        public static bool UpdCustomInfo(Hashtable htInfo, string where)
        {
            return publicDbOpClass.Update("[CPM_Custom]", htInfo, where);
        }
    }
}

