namespace cn.justwin.BLL
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class WordExporter : IExportable
    {
        private int[] percentColumns;

        public void Export(DataTable table, string fileName, string brower)
        {
            HttpContext current = HttpContext.Current;
            StringWriter writer = null;
            HtmlTextWriter writer2 = null;
            current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            current.Response.Charset = "utf-8";
            current.Response.HeaderEncoding = Encoding.GetEncoding("GB2312");
            if (table.Rows.Count == 0)
            {
                current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            }
            writer = new StringWriter();
            writer2 = new HtmlTextWriter(writer);
            GridView view = new GridView();
            view.RowDataBound += new GridViewRowEventHandler(this.gvwExport_RowDataBound);
            view.DataSource = table.DefaultView;
            view.AllowPaging = false;
            view.DataBind();
            view.RenderControl(writer2);
            current.Response.Write(writer.ToString());
            current.Response.End();
        }

        private void gvwExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex > -1)
                {
                    foreach (int num in this.percentColumns)
                    {
                        e.Row.Cells[num].Text = string.Format("{0:P}", Convert.ToDouble(e.Row.Cells[num].Text));
                    }
                }
            }
            catch
            {
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

