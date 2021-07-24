namespace Business.EPCAction.Basic
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class SelfEventAction
    {
        public static DataTable GetInstance(string tablename)
        {
            return publicDbOpClass.DataTableQuary("select * from SelfEventInfo where tablename ='" + tablename + "'");
        }
    }
}

