using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_EvenAnalysisDetail : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string year = string.Empty;
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private DataTable dt = new DataTable();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
		PrjInfoModel prjInfoModel = new PrjInfoModel();
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			prjInfoModel = pTPrjInfoBll.GetModelByPrjGuid(this.prjId);
			this.year = prjInfoModel.StartDate.Year.ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void ComputeTotal()
	{
		this.txtTaskCode.Text = "";
		this.txtTaskName.Text = "";
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable evenAnalysisDetail = EReport.GetEvenAnalysisDetail(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0, this.hfldIsWBSRelevance.Value);
		string[] array = new string[4];
		if (evenAnalysisDetail.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(ContractCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(ConstructCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(GainLoss)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
		}
		if (System.Convert.ToDecimal(array[0]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]);
			array[3] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
		}
		else
		{
			array[3] = "0.00%";
		}
		int[] value = new int[]
		{
			3,
			4,
			5,
			6
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetEvenAnalysisDetailCount(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId);
		this.dt = EReport.GetEvenAnalysisDetail(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, this.hfldIsWBSRelevance.Value);
		this.ViewState["dt"] = this.dt;
		this.gvCost.DataSource = this.dt;
		this.gvCost.DataBind();
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
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "任务编码";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "任务名称";
			cells.Add(new TableHeaderCell());
			cells[3].ColumnSpan = 4;
			cells[3].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 1;
			cells[4].Text = "合同预算";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 1;
			cells[5].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 1;
			cells[6].Text = "盈亏";
			cells[6].Attributes.Add("title", "盈亏 = 合同预算 &ndash; 实际成本");
			cells[6].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 1;
			cells[7].Attributes.Add("title", "盈亏率 = 盈亏 &divide; 合同预算");
			cells[7].CssClass = "tooltip";
			cells[7].Text = "盈亏率";
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
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable evenAnalysisDetail = EReport.GetEvenAnalysisDetail(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0, this.hfldIsWBSRelevance.Value);
		string[] array = new string[4];
		if (evenAnalysisDetail.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(ContractCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(ConstructCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(evenAnalysisDetail.Compute("sum(GainLoss)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
		}
		if (System.Convert.ToDecimal(array[0]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]);
			array[3] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
		}
		else
		{
			array[3] = "0.00%";
		}
		int[] value = new int[]
		{
			3,
			4,
			5,
			6
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = EReport.GetEvenAnalysisDetail(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0, this.hfldIsWBSRelevance.Value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["No"] = "合计";
			dataRow["ContractCost"] = dataTable.Compute("sum(ContractCost)", "1>0");
			dataRow["ConstructCost"] = dataTable.Compute("sum(ConstructCost)", "1>0");
			dataRow["GainLoss"] = dataTable.Compute("sum(GainLoss)", "1>0");
			if (System.Convert.ToDecimal(dataRow["ContractCost"]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(dataRow["GainLoss"]) / System.Convert.ToDecimal(dataRow["ContractCost"]);
				dataRow["RatioGainLoss"] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				dataRow["RatioGainLoss"] = "0.00%";
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		dataTable.Columns.Remove("OrderNumber");
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("累计数", 1, 4, 3, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 3)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 3)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "盈亏明细表", "盈亏明细表", "盈亏明细表.xls", list, null, 3, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["No"] != null)
		{
			dt.Columns["No"].ColumnName = "序号";
		}
		if (dt.Columns["TaskCode"] != null)
		{
			dt.Columns["TaskCode"].ColumnName = "任务编码";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "任务名称";
		}
		if (dt.Columns["ContractCost"] != null)
		{
			dt.Columns["ContractCost"].ColumnName = "合同预算";
		}
		if (dt.Columns["ConstructCost"] != null)
		{
			dt.Columns["ConstructCost"].ColumnName = "实际成本";
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


