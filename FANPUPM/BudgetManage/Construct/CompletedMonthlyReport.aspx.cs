using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_CompletedMonthlyReport : NBasePage, IRequiresSessionState
{
	private string taskId = string.Empty;
	private string date = string.Empty;
	private string title = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private MeterialPlanStock materialStock = new MeterialPlanStock();

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["taskId"] != null)
		{
			this.taskId = base.Request.QueryString["taskId"];
		}
		if (base.Request.QueryString["date"] != null)
		{
			this.date = base.Request.QueryString["date"];
		}
		if (base.Request.QueryString["title"] != null)
		{
			this.title = base.Request.QueryString["title"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
		this.lblTitle.Text = this.title;
		this.BindGv();
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "项目编码";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "计量单位";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "工程量";
			cells.Add(new TableHeaderCell());
			cells[5].ColumnSpan = 2;
			cells[5].Text = "金额(元)";
			cells.Add(new TableHeaderCell());
			cells[6].ColumnSpan = 2;
			cells[6].Text = "上期末完成";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 2;
			cells[7].Text = "本期完成";
			cells.Add(new TableHeaderCell());
			cells[8].ColumnSpan = 2;
			cells[8].Text = "本期末完成</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[9].Text = "综合单价";
			cells.Add(new TableHeaderCell());
			cells[10].Text = "合计";
			cells.Add(new TableHeaderCell());
			cells[11].Text = "数量";
			cells.Add(new TableHeaderCell());
			cells[12].Text = "金额";
			cells.Add(new TableHeaderCell());
			cells[13].Text = "数量";
			cells.Add(new TableHeaderCell());
			cells[14].Text = "金额";
			cells.Add(new TableHeaderCell());
			cells[15].Text = "数量";
			cells.Add(new TableHeaderCell());
			cells[16].Text = "金额";
		}
	}
	protected void BindGv()
	{
		DataTable completedMonthlyReport = EReport.GetCompletedMonthlyReport(this.taskId, System.Convert.ToDateTime(this.date), this.hfldIsWBSRelevance.Value);
		this.gvConstruct.DataSource = this.formatData(completedMonthlyReport, false);
		this.gvConstruct.DataBind();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable completedMonthlyReport = EReport.GetCompletedMonthlyReport(this.taskId, System.Convert.ToDateTime(this.date), this.hfldIsWBSRelevance.Value);
		DataTable dataTable = this.formatData(completedMonthlyReport, true);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("金额(元)", 1, 2, 5, 0));
		list.Add(ExcelHeader.Create("上期末完成", 1, 2, 7, 0));
		list.Add(ExcelHeader.Create("本期完成", 1, 2, 9, 0));
		list.Add(ExcelHeader.Create("本期末完成", 1, 2, 11, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 4)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 4)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, this.title, this.title, this.title + ".xls", list, null, 0, base.Request.Browser.Browser);
	}
	protected DataTable formatData(DataTable dt, bool IsExport)
	{
		int num = 0;
		decimal num2 = 0m;
		decimal num3 = 0m;
		decimal num4 = 0m;
		decimal num5 = 0m;
		string orderNumber = string.Empty;
		bool flag = false;
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		System.Collections.Generic.List<DataRow> list = new System.Collections.Generic.List<DataRow>();
		foreach (DataRow dataRow in dt.Rows)
		{
			if (dataRow["OrderNumber"].ToString().Length == 12)
			{
				flag = true;
				num++;
				dataRow["Num"] = num.ToString();
				orderNumber = dataRow["OrderNumber"].ToString();
				dataRow["Quantity"] = ((dataRow["Quantity"].ToString() == "") ? "0.000" : dataRow["Quantity"].ToString());
				dataRow["UnitPrice"] = ((dataRow["UnitPrice"].ToString() == "") ? "0.000" : dataRow["UnitPrice"].ToString());
				dataRow["Sum"] = ((dataRow["Sum"].ToString() == "") ? "0.000" : dataRow["Sum"].ToString());
				decimal d5 = System.Convert.ToDecimal(dataRow["Sum"]);
				num2 += d5;
				dataRow["preQuantity"] = ((dataRow["preQuantity"].ToString() == "") ? "0.000" : dataRow["preQuantity"].ToString());
				dataRow["preSum"] = ((dataRow["preSum"].ToString() == "") ? "0.000" : dataRow["preSum"].ToString());
				decimal d6 = System.Convert.ToDecimal(dataRow["preSum"]);
				num3 += d6;
				dataRow["curQuantity"] = ((dataRow["curQuantity"].ToString() == "") ? "0.000" : dataRow["curQuantity"].ToString());
				dataRow["curSum"] = ((dataRow["curSum"].ToString() == "") ? "0.000" : dataRow["curSum"].ToString());
				decimal d7 = System.Convert.ToDecimal(dataRow["curSum"]);
				num4 += d7;
				dataRow["curEndQuantity"] = ((dataRow["curEndQuantity"].ToString() == "") ? "0.000" : dataRow["curEndQuantity"].ToString());
				dataRow["curEndSum"] = ((dataRow["curEndSum"].ToString() == "") ? "0.000" : dataRow["curEndSum"].ToString());
				decimal d8 = System.Convert.ToDecimal(dataRow["curEndSum"]);
				num5 += d8;
			}
			else
			{
				if (dataRow["OrderNumber"].ToString().Length == 9)
				{
					DataRow[] array = dt.Select("OrderNumber LIKE '" + dataRow["OrderNumber"].ToString() + "%' ");
					int num6 = array.Length;
					if (num6 > 1 && num > 0)
					{
						DataRow dataRow2 = dt.NewRow();
						dataRow2["TaskId"] = "RowChildSum";
						dataRow2["TaskName"] = "分部小计";
						dataRow2["OrderNumber"] = this.GetNewOrderNumber(orderNumber);
						dataRow2["Sum"] = num2.ToString("0.000");
						dataRow2["preSum"] = num3.ToString("0.000");
						dataRow2["curSum"] = num4.ToString("0.000");
						dataRow2["curEndSum"] = num5.ToString("0.000");
						d += num2;
						d2 += num3;
						d3 += num4;
						d4 += num5;
						num2 = 0m;
						num3 = 0m;
						num4 = 0m;
						num5 = 0m;
						list.Add(dataRow2);
						flag = false;
					}
					else
					{
						if (num6 == 1)
						{
							d += System.Convert.ToDecimal((dataRow["Sum"] == System.DBNull.Value) ? 0m : dataRow["sum"]);
							d2 += System.Convert.ToDecimal((dataRow["preSum"] == System.DBNull.Value) ? 0m : dataRow["preSum"]);
							d3 += System.Convert.ToDecimal((dataRow["curSum"] == System.DBNull.Value) ? 0m : dataRow["curSum"]);
							d4 += System.Convert.ToDecimal((dataRow["curEndSum"] == System.DBNull.Value) ? 0m : dataRow["curEndSum"]);
						}
					}
				}
			}
		}
		if (flag)
		{
			DataRow dataRow3 = dt.NewRow();
			dataRow3["TaskId"] = "RowChildSum";
			dataRow3["TaskName"] = "分部小计";
			dataRow3["OrderNumber"] = this.GetNewOrderNumber(orderNumber);
			dataRow3["Sum"] = num2.ToString("0.000");
			dataRow3["preSum"] = num3.ToString("0.000");
			dataRow3["curSum"] = num4.ToString("0.000");
			dataRow3["curEndSum"] = num5.ToString("0.000");
			d += num2;
			d2 += num3;
			d3 += num4;
			d4 += num5;
			num2 = 0m;
			num3 = 0m;
			num4 = 0m;
			num5 = 0m;
			list.Add(dataRow3);
		}
		foreach (DataRow current in list)
		{
			dt.Rows.Add(current);
		}
		DataView defaultView = dt.DefaultView;
		defaultView.Sort = "OrderNumber ASC";
		DataTable dataTable = defaultView.ToTable();
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow4 = dataTable.NewRow();
			dataRow4["Num"] = "";
			dataRow4["TaskId"] = "合计";
			dataRow4["TaskName"] = "合计";
			dataRow4["Sum"] = d.ToString("0.000");
			dataRow4["preSum"] = d2.ToString("0.000");
			dataRow4["curSum"] = d3.ToString("0.000");
			dataRow4["curEndSum"] = d4.ToString("0.000");
			dataTable.Rows.Add(dataRow4);
		}
		if (IsExport)
		{
			if (dataTable.Columns["Num"] != null)
			{
				dataTable.Columns["Num"].ColumnName = "序号";
			}
			if (dataTable.Columns["TaskCode"] != null)
			{
				dataTable.Columns["TaskCode"].ColumnName = "项目编码";
			}
			if (dataTable.Columns["TaskName"] != null)
			{
				dataTable.Columns["TaskName"].ColumnName = "项目名称";
			}
			if (dataTable.Columns["Unit"] != null)
			{
				dataTable.Columns["Unit"].ColumnName = "计量单位";
			}
			if (dataTable.Columns["Quantity"] != null)
			{
				dataTable.Columns["Quantity"].ColumnName = "工程量";
			}
			if (dataTable.Columns["UnitPrice"] != null)
			{
				dataTable.Columns["UnitPrice"].ColumnName = "综合单价";
			}
			if (dataTable.Columns["Sum"] != null)
			{
				dataTable.Columns["Sum"].ColumnName = "合计";
			}
			if (dataTable.Columns["preQuantity"] != null)
			{
				dataTable.Columns["preQuantity"].ColumnName = "数量";
			}
			if (dataTable.Columns["preSum"] != null)
			{
				dataTable.Columns["preSum"].ColumnName = "金额";
			}
			if (dataTable.Columns["curQuantity"] != null)
			{
				dataTable.Columns["curQuantity"].ColumnName = " 数量 ";
			}
			if (dataTable.Columns["curSum"] != null)
			{
				dataTable.Columns["curSum"].ColumnName = " 金额 ";
			}
			if (dataTable.Columns["curEndQuantity"] != null)
			{
				dataTable.Columns["curEndQuantity"].ColumnName = "  数量  ";
			}
			if (dataTable.Columns["curEndSum"] != null)
			{
				dataTable.Columns["curEndSum"].ColumnName = "  金额  ";
			}
			if (dataTable.Columns["TaskId"] != null)
			{
				dataTable.Columns.Remove(dataTable.Columns["TaskId"]);
			}
			if (dataTable.Columns["OrderNumber"] != null)
			{
				dataTable.Columns.Remove(dataTable.Columns["OrderNumber"]);
			}
			dataTable.AcceptChanges();
		}
		return dataTable;
	}
	protected string GetNewOrderNumber(string orderNumber)
	{
		return (System.Convert.ToDouble(orderNumber) + 1.0).ToString("000000000000");
	}
	protected void DataTableToWord(DataTable dt, string FileName, string titlename)
	{
		string str;
		if (FileName == null || FileName == "")
		{
			str = "DFSOFT";
		}
		else
		{
			str = HttpUtility.UrlPathEncode(FileName);
		}
		DataRow[] array = dt.Select();
		int count = dt.Columns.Count;
		HttpResponse response = HttpContext.Current.Response;
		response.Clear();
		response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
		response.AppendHeader("Content-disposition", "attachment;filename=" + str + ".doc");
		response.ContentType = "application/vnd.ms-word";
		string text = "<table border='0' cellpadding='0' cellspacing='0' style='border-right:#000000 0.1pt solid;border-top:#000000 0.1pt solid;'>";
		string text2 = "</table>";
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = "<tr><td style='font-size:13px;' align='center'><b>" + titlename + "</b></td></tr>";
		string text6 = "<tr>";
		string text7 = "</tr>";
		for (int i = 0; i < count; i++)
		{
			if (dt.Rows.Count > 0)
			{
				text3 = text3 + "<td style='border-left:#000000 0.1pt solid; border-bottom:#000000 1.0pt solid; font-size:15px;' align='center'><b>" + dt.Columns[i].Caption.ToString() + "</b></td>";
			}
			else
			{
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
				text3 = text3 + "<td style='border-left:#000000 0.1pt solid; border-bottom:#000000 1.0pt solid; font-size:15px;' align='center'><b>" + dt.Columns[i].Caption.ToString() + "</b></td>";
			}
		}
		text3 = text6.ToString() + text3.ToString() + text7.ToString();
		DataRow[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			DataRow dataRow = array2[j];
			string text8 = string.Empty;
			for (int i = 0; i < count; i++)
			{
				text8 = text8 + "<td style='border-left:#000000 0.1pt solid; border-bottom:#000000 1.0pt solid; font-size:15px;' align='center'>" + dataRow[i].ToString() + "</td>";
			}
			text4 = text4 + text6.ToString() + text8.ToString() + text7.ToString();
		}
		text3 = string.Concat(new string[]
		{
			"<center><table>",
			text5.ToString(),
			"<tr>",
			text.ToString(),
			text3.ToString(),
			text4.ToString(),
			text2.ToString(),
			"</tr></table></center>"
		});
		response.Write(text3.ToString());
		response.End();
	}
}


