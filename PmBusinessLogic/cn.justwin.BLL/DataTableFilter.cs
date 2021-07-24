namespace cn.justwin.BLL
{
    using System;
    using System.Data;

    public class DataTableFilter
    {
        public DataTable Filter(DataTable table, string columnName, string userCode)
        {
            DataView defaultView = table.DefaultView;
            defaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", columnName, userCode);
            return defaultView.ToTable();
        }
    }
}

