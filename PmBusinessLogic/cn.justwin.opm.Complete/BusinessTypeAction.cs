namespace cn.justwin.opm.Complete
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class BusinessTypeAction
    {
        public DataTable GetAllType()
        {
            string str = "select * from OPM_File_BusinessType";
            return publicDbOpClass.DataTableQuary(str.ToString());
        }
    }
}

