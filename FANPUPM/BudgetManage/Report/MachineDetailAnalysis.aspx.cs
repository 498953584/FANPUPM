using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_MachineDetailAnalysis : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		string taskCode = this.txtTaskCode.Text.Trim();
		string taskName = this.txtTaskName.Text.Trim();
		string resourceCode = this.txtResourceCode.Text.Trim();
		string resourceName = this.txtResourceName.Text.Trim();
		DataTable cBSTaskAnalysis = ConstructReport.GetCBSTaskAnalysis(taskCode, taskName, resourceCode, resourceName, this.prjId, "MachineCBSCode", 0, 0);
		string[] array = new string[3];
		if (cBSTaskAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(cBSTaskAnalysis.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(cBSTaskAnalysis.Compute("sum(ConsCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(cBSTaskAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		string taskCode = this.txtTaskCode.Text.Trim();
		string taskName = this.txtTaskName.Text.Trim();
		string resourceCode = this.txtResourceCode.Text.Trim();
		string resourceName = this.txtResourceName.Text.Trim();
		this.AspNetPager1.RecordCount = ConstructReport.GetCBSTaskCount(taskCode, taskName, resourceCode, resourceName, this.prjId, "MachineCBSCode");
		DataTable cBSTaskAnalysis = ConstructReport.GetCBSTaskAnalysis(taskCode, taskName, resourceCode, resourceName, this.prjId, "MachineCBSCode", this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvMachineCost.DataSource = cBSTaskAnalysis;
		this.gvMachineCost.DataBind();
	}
	protected void gvMachineCost_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[9].Text = array[0];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[1];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[2];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		string taskCode = this.txtTaskCode.Text.Trim();
		string taskName = this.txtTaskName.Text.Trim();
		string resourceCode = this.txtResourceCode.Text.Trim();
		string resourceName = this.txtResourceName.Text.Trim();
		dataTable = ConstructReport.GetCBSTaskAnalysis(taskCode, taskName, resourceCode, resourceName, this.prjId, "MachineCBSCode", 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["BudCost"] = dataTable.Compute("sum(BudCost)", "1>0");
			dataRow["ConsCost"] = dataTable.Compute("sum(ConsCost)", "1>0");
			dataRow["ReductionAmount"] = dataTable.Compute("sum(ReductionAmount)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "机械明细表", "机械明细表", "机械明细表.xls", null, null, 2, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["TaskCode"] != null)
		{
			dt.Columns["TaskCode"].ColumnName = "任务编号";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "任务名称";
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "机械编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "机械名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["TechnicalParameter"] != null)
		{
			dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		}
		if (dt.Columns["ModelNumber"] != null)
		{
			dt.Columns["ModelNumber"].ColumnName = "型号";
		}
		if (dt.Columns["Brand"] != null)
		{
			dt.Columns["Brand"].ColumnName = "品牌";
		}
		if (dt.Columns["BudCost"] != null)
		{
			dt.Columns["BudCost"].ColumnName = "目标成本金额";
		}
		if (dt.Columns["ConsCost"] != null)
		{
			dt.Columns["ConsCost"].ColumnName = "实际成本金额";
		}
		if (dt.Columns["ReductionAmount"] != null)
		{
			dt.Columns["ReductionAmount"].ColumnName = "差额";
		}
		dt.AcceptChanges();
		return dt;
	}
}


