namespace cn.justwin.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public sealed class GridViewUtility
    {
        public static void AddTotalRow(GridView gvw, string total, int totalColumnIndex)
        {
            GridViewRow child = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell cell = new TableCell();
            cell.Attributes["colspan"] = totalColumnIndex.ToString();
            cell.Attributes["align"] = "center";
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            cell3.Attributes["colspan"] = Convert.ToString((int) ((gvw.Columns.Count - totalColumnIndex) - 1));
            cell2.Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign.Right.ToString());
            Label label = new Label {
                Text = "总计"
            };
            cell.Controls.Add(label);
            Label label2 = new Label {
                CssClass = "_total_",
                Text = total
            };
            cell2.Controls.Add(label2);
            child.Cells.Add(cell);
            child.Cells.Add(cell2);
            child.Cells.Add(cell3);
            if (gvw.Rows.Count > 0)
            {
                gvw.Controls[0].Controls.AddAt(gvw.Rows.Count + 1, child);
            }
        }

        public static void AddTotalRow(GridView gvw, string[] value, int[] index)
        {
            if ((value.Length != 0) && (value.Length == index.Length))
            {
                GridViewRow child = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.Attributes["colspan"] = index[0].ToString();
                cell.Attributes["align"] = "center";
                cell.Text = "总 计";
                cell.Font.Bold = true;
                child.Cells.Add(cell);
                for (int i = 0; i < index.Length; i++)
                {
                    TableCell cell2 = new TableCell {
                        Text = value[i]
                    };
                    cell2.Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign.Right.ToString());
                    cell2.CssClass = "decimal_input";
                    cell2.Font.Bold = true;
                    child.Cells.Add(cell2);
                    if (i < (index.Length - 1))
                    {
                        int num2 = (index[i + 1] - index[i]) - 1;
                        if (num2 > 0)
                        {
                            TableCell cell3 = new TableCell();
                            cell3.Attributes["colspan"] = num2.ToString();
                            cell3.Font.Bold = true;
                            child.Cells.Add(cell3);
                        }
                    }
                }
                if (((gvw.Columns.Count - index[index.Length - 1]) - 1) > 0)
                {
                    TableCell cell4 = new TableCell();
                    cell4.Attributes["colspan"] = Convert.ToString((int) ((gvw.Columns.Count - index[index.Length - 1]) - 1));
                    child.Cells.Add(cell4);
                }
                if (gvw.Rows.Count > 0)
                {
                    gvw.Controls[0].Controls.AddAt(gvw.Rows.Count + 1, child);
                }
            }
        }

        public static void DataBind<T>(GridView gvw, IList list) where T: class
        {
            if (list.Count == 0)
            {
                try
                {
                    T local = default(T);
                    list.Add(local);
                    gvw.DataSource = list;
                    gvw.DataBind();
                    gvw.Rows[0].Visible = false;
                    if (string.IsNullOrEmpty(gvw.Columns[0].HeaderText))
                    {
                        gvw.Columns[0].Visible = false;
                    }
                }
                catch
                {
                }
            }
            else
            {
                gvw.DataSource = list;
                gvw.DataBind();
            }
        }

        public static void DataBind(GridView gvw, DataTable table)
        {
            if (table.Rows.Count == 0)
            {
                table.Rows.Add(table.NewRow());
                gvw.DataSource = table;
                gvw.DataBind();
                gvw.Rows[0].Visible = false;
                if (string.IsNullOrEmpty(gvw.Columns[0].HeaderText))
                {
                    gvw.Columns[0].Visible = false;
                }
            }
            else
            {
                gvw.DataSource = table;
                gvw.DataBind();
            }
        }
    }
}

