using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_RequirePlanReport : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.ComputeTotal();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.txtEquTypeName.Text.Trim();
	}
	public void BindGv()
	{
		this.txtEquTypeName.Text.Trim();
	}
	protected void gvRequirePlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[3].Text = array[0];
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		new DataTable();
		this.txtEquTypeName.Text.Trim();
	}
	private DataTable ReplaceMonthCount(DataTable dt)
	{
		ConvertChineseNum convertChineseNum = new ConvertChineseNum();
		foreach (DataRow dataRow in dt.Rows)
		{
			dataRow["MonthCount"] = "第" + convertChineseNum.Convert(dataRow["MonthCount"].ToString()) + "个月";
		}
		return dt;
	}
	private DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["MonthCount"] != null)
		{
			dt.Columns["MonthCount"].ColumnName = " ";
		}
		if (dt.Columns["EquTypeName"] != null)
		{
			dt.Columns["EquTypeName"].ColumnName = "设备类型名称";
		}
		if (dt.Columns["Quantity"] != null)
		{
			dt.Columns["Quantity"].ColumnName = "数量";
		}
		if (dt.Columns["EquipmentSource"] != null)
		{
			dt.Columns["EquipmentSource"].ColumnName = "设备来源";
		}
		if (dt.Columns["EnterDate"] != null)
		{
			dt.Columns["EnterDate"].ColumnName = "预计进场日期";
		}
		if (dt.Columns["OutDate"] != null)
		{
			dt.Columns["OutDate"].ColumnName = "预计出场日期";
		}
		if (dt.Columns["EnterArea"] != null)
		{
			dt.Columns["EnterArea"].ColumnName = "预计进场地点";
		}
		if (dt.Columns["BudCost"] != null)
		{
			dt.Columns["BudCost"].ColumnName = "预算费用";
		}
		dt.AcceptChanges();
		return dt;
	}
}


