namespace cn.justwin.Utility
{
    using NPOI.HSSF.UserModel;
    using System;
    using System.Data;

    public static class ExcelUtility
    {
        public static void ExportExcel(DataTable table)
        {
            new HSSFWorkbook().CreateSheet();
        }
    }
}

