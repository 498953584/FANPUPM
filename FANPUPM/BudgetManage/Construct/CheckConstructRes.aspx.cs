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
public partial class BudgetManage_Construct_CheckConstructRes : NBasePage, IRequiresSessionState
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
			this.hfldYear.Value = this.year;
			this.ComputeTotal();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetConsResCount(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.txtReourceCode.Text.Trim(), this.txtReourceName.Text.Trim(), this.prjId, WebUtil.GetUserNames(base.UserCode));
		this.gvConstruct.DataSource = ConstructReport.GetConsRes(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.txtReourceCode.Text.Trim(), this.txtReourceName.Text.Trim(), this.prjId, WebUtil.GetUserNames(base.UserCode), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvConstruct.DataBind();
		string[] value = (string[])this.ViewState["Total"];
		int[] index = (int[])this.ViewState["index"];
		GridViewUtility.AddTotalRow(this.gvConstruct, value, index);
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable consRes = ConstructReport.GetConsRes(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.txtReourceCode.Text.Trim(), this.txtReourceName.Text.Trim(), this.prjId, WebUtil.GetUserNames(base.UserCode), 0, 0);
		string[] array = new string[2];
		if (consRes.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(consRes.Compute("sum(AccountingQuantity)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(consRes.Compute("sum(Total)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
		}
		int[] value = new int[]
		{
			8,
			9
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = ConstructReport.GetConsRes(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.txtReourceCode.Text.Trim(), this.txtReourceName.Text.Trim(), this.prjId, WebUtil.GetUserNames(base.UserCode), 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["AccountingQuantity"] = dataTable.Compute("sum(AccountingQuantity)", "1>0");
			dataRow["Total"] = dataTable.Compute("sum(Total)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "施工报量查询", "施工报量查询", "施工报量查询.xls", null, null, 3, base.Request.Browser.Browser);
	}
	protected DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["TaskId"] != null)
		{
			dt.Columns.Remove(dt.Columns["TaskId"]);
		}
		if (dt.Columns["ResourceId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ResourceId"]);
		}
		if (dt.Columns["TaskCode"] != null)
		{
			dt.Columns["TaskCode"].ColumnName = "任务编码";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "任务名称";
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "资源编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "资源名称";
		}
		if (dt.Columns["UnitName"] != null)
		{
			dt.Columns["UnitName"].ColumnName = "单位";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["UnitPrice"] != null)
		{
			dt.Columns["UnitPrice"].ColumnName = "单价";
		}
		if (dt.Columns["AccountingQuantity"] != null)
		{
			dt.Columns["AccountingQuantity"].ColumnName = "数量";
		}
		if (dt.Columns["Total"] != null)
		{
			dt.Columns["Total"].ColumnName = "小计";
		}
		dt.AcceptChanges();
		return dt;
	}
}


