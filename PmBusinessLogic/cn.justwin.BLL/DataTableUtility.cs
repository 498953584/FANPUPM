namespace cn.justwin.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class DataTableUtility
    {
        public static List<object> ConvertToList(DataTable dtSource, string column)
        {
            System.Func<DataRow, object> selector = null;
            List<object> list = new List<object>();
            if (dtSource == null)
            {
                return list;
            }
            if (selector == null)
            {
                selector = row => row[column];
            }
            return dtSource.Select().ToList<DataRow>().Select<DataRow, object>(selector).ToList<object>();
        }

        public static int IndexOf(DataRow row, string primaryKey, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (row[primaryKey].ToString() == dt.Rows[i][primaryKey].ToString())
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

