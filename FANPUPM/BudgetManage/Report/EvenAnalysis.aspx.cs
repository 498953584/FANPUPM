using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_EvenAnalysis : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private DataTable dtEvenAnalysis = new DataTable();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable evenAnalysis = EReport.GetEvenAnalysis(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), 0, 0, base.UserCode, this.hfldIsWBSRelevance.Value);
		string[] array = new string[5];
		if (evenAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(evenAnalysis.Compute("sum(ContractBud)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(evenAnalysis.Compute("sum(DirectCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(evenAnalysis.Compute("sum(IndirectCost)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(evenAnalysis.Compute("sum(GainLoss)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
		}
		if (System.Convert.ToDecimal(array[0]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[3]) / System.Convert.ToDecimal(array[0]);
			array[4] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
		}
		else
		{
			array[4] = "0.00%";
		}
		int[] value = new int[]
		{
			3,
			4,
			5,
			6,
			7
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = EReport.GetEvenAnalysis(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), 0, 0, base.UserCode, this.hfldIsWBSRelevance.Value).Rows.Count;
		this.dtEvenAnalysis = EReport.GetEvenAnalysis(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, base.UserCode, this.hfldIsWBSRelevance.Value);
		this.gvCost.DataSource = this.dtEvenAnalysis;
		this.gvCost.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 3;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 3;
			cells[1].Text = "项目编码";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 3;
			cells[2].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[3].ColumnSpan = 5;
			cells[3].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "合同预算";
			cells.Add(new TableHeaderCell());
			cells[5].ColumnSpan = 2;
			cells[5].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "盈亏";
			cells[6].Attributes.Add("title", "盈亏 = 合同预算 &ndash; 实际成本");
			cells[6].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 2;
			cells[7].Attributes.Add("title", "盈亏率 = 盈亏 &divide; 合同预算");
			cells[7].CssClass = "tooltip";
			cells[7].Text = "盈亏率</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "直接成本";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "间接成本";
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[3].Text = array[0];
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].CssClass = "decimal_input";
			e.Row.Cells[4].Text = array[1];
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].CssClass = "decimal_input";
			e.Row.Cells[5].Text = array[2];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].CssClass = "decimal_input";
			e.Row.Cells[6].Text = array[3];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[7].Text = array[4];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = ConstructReport.GetEvenAnalysis(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), 0, 0, base.UserCode, this.hfldIsWBSRelevance.Value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["ContractBud"] = dataTable.Compute("sum(ContractBud)", "1>0");
			dataRow["DirectCost"] = dataTable.Compute("sum(DirectCost)", "1>0");
			dataRow["IndirectCost"] = dataTable.Compute("sum(IndirectCost)", "1>0");
			dataRow["GainLoss"] = dataTable.Compute("sum(GainLoss)", "1>0");
			if (System.Convert.ToDecimal(dataRow["ContractBud"]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(dataRow["GainLoss"]) / System.Convert.ToDecimal(dataRow["ContractBud"]);
				dataRow["RatioGainLoss"] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				dataRow["RatioGainLoss"] = "0.00%";
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("累计数", 1, 5, 3, 0));
		list.Add(ExcelHeader.Create("实际成本", 2, 2, 4, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 3)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 3)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 3, 0, 0, 3));
			}
			else
			{
				if (dataColumn.Ordinal > 3 && dataColumn.Ordinal < 6)
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 3, 0, 0, 0));
				}
				else
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
				}
			}
		}
		ExcelHelper.ExportExcel(dataTable, "盈亏分析", "盈亏分析", "盈亏分析.xls", list, null, 3, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["prjGuid"] != null)
		{
			dt.Columns.Remove(dt.Columns["prjGuid"]);
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns.Remove(dt.Columns["StartDate"]);
		}
		if (dt.Columns["prjCode"] != null)
		{
			dt.Columns["prjCode"].ColumnName = "项目编码";
		}
		if (dt.Columns["prjName"] != null)
		{
			dt.Columns["prjName"].ColumnName = "项目名称";
		}
		if (dt.Columns["ContractBud"] != null)
		{
			dt.Columns["ContractBud"].ColumnName = "合同预算";
		}
		if (dt.Columns["DirectCost"] != null)
		{
			dt.Columns["DirectCost"].ColumnName = "直接成本";
		}
		if (dt.Columns["IndirectCost"] != null)
		{
			dt.Columns["IndirectCost"].ColumnName = "间接成本";
		}
		if (dt.Columns["GainLoss"] != null)
		{
			dt.Columns["GainLoss"].ColumnName = "盈亏";
		}
		if (dt.Columns["RatioGainLoss"] != null)
		{
			dt.Columns["RatioGainLoss"].ColumnName = "盈亏率";
		}
		dt.AcceptChanges();
		return dt;
	}
}


