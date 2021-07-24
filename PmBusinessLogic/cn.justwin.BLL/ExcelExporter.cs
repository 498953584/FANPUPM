namespace cn.justwin.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public class ExcelExporter : IExportable
    {
        private int[] percentColumns;

        public void Export(DataTable table, string fileName, string brower)
        {
            if ((brower.ToUpper() == "IE") || (brower.ToUpper() == "CHROME"))
            {
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            ExcelHelper.ExportDataTableToExcel(this.FormatPercent(table), fileName, "Sheet1");
        }

        public void ExportAll(List<DataTable> tableList, HttpContext curContext, string fileName)
        {
            ExcelHelper.RenderToBrowser(ExcelHelper.RenderToExcel(tableList), curContext, fileName);
        }

        private DataTable FormatPercent(DataTable table)
        {
            if ((this.PercentColumns == null) || (this.PercentColumns.Count<int>() == 0))
            {
                return table;
            }
            DataTable table2 = new DataTable();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                DataColumn column = new DataColumn(table.Columns[i].ColumnName);
                table2.Columns.Add(column);
            }
            for (int j = 0; j < table.Rows.Count; j++)
            {
                DataRow row = table2.NewRow();
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    if (!this.PercentColumns.Contains<int>(k))
                    {
                        row[k] = table.Rows[j][k];
                    }
                    else
                    {
                        row[k] = string.Format("{0:P}", table.Rows[j][k]);
                    }
                }
                table2.Rows.Add(row);
            }
            return table2;
        }

        private void gvwExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                foreach (int num in this.percentColumns)
                {
                    e.Row.Cells[num].Text = string.Format("{0:P}", Convert.ToDecimal(e.Row.Cells[num].Text));
                }
            }
            catch
            {
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }
        }

        public int[] PercentColumns
        {
            get
            {
                return this.percentColumns;
            }
            set
            {
                this.percentColumns = value;
            }
        }
    }
}

