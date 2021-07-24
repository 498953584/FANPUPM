using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Land_DrillMonthReport : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindYearMonth();
			this.ComputeTotal();
			this.BindGv();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
	}
	public void BindGv()
	{
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
	}
	protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[5].Text = array[0];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[6].Text = array[1];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
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
		new DataTable();
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
	}
	private void BindYearMonth()
	{
		for (int i = 1; i <= 12; i++)
		{
			this.ddlMonth.Items.Add(new ListItem(i.ToString() + "月", i.ToString("00")));
		}
		for (int j = System.DateTime.Now.Year - 4; j <= System.DateTime.Now.Year; j++)
		{
			this.ddlYear.Items.Add(new ListItem(j.ToString() + "年", j.ToString("")));
		}
		this.ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
		this.ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString("00");
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["EquipmentId"] != null)
		{
			dt.Columns.Remove(dt.Columns["EquipmentId"]);
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "钻孔机编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "钻孔机名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["CorpName"] != null)
		{
			dt.Columns["CorpName"].ColumnName = "制造厂家";
		}
		if (dt.Columns["DrillCount"] != null)
		{
			dt.Columns["DrillCount"].ColumnName = "孔数";
		}
		if (dt.Columns["TotalLength"] != null)
		{
			dt.Columns["TotalLength"].ColumnName = "总长";
		}
		dt.AcceptChanges();
		return dt;
	}
}


