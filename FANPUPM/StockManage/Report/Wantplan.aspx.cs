using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_Wantplan : NBasePage, IRequiresSessionState
{
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private Common2 cm = new Common2();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvPurchaseplan.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public DataTable GetTable()
	{
		return this.meterialPlanStock.GetTableByParam(this.txtStartTime.Text, this.txtEndTime.Text, this.txtCode.Text, this.txtResourceNames.Text.Split(new char[]
		{
			','
		}), this.txtResourceCodes.Text.Split(new char[]
		{
			','
		}), this.txtPrjName.Text.Trim(), this.txtCategory.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
	}
	public void BindGv()
	{
		this.BindGv(this.GetTable());
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvPurchaseplan.DataSource = storageDataSource;
			this.gvPurchaseplan.DataBind();
			this.gvPurchaseplan.HeaderRow.Cells[0].Visible = false;
			this.gvPurchaseplan.Rows[0].Visible = false;
			return;
		}
		this.gvPurchaseplan.DataSource = storageDataSource;
		this.gvPurchaseplan.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPurchaseplan.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable table = this.GetTable();
		this.cm.ExportToExecelAll(this.GetTitleByTable(table), "需求计划报表.xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt.Columns.Remove("swid");
		dt.Columns.Add("num");
		dt.Columns["ResourceCode"].ColumnName = "资源编号";
		dt.Columns["ResourceName"].ColumnName = "资源名称";
		dt.Columns["ResourceTypeName"].ColumnName = "资源类别";
		dt.Columns["Specification"].ColumnName = "规格";
		dt.Columns["Brand"].ColumnName = "品牌";
		dt.Columns["ModelNumber"].ColumnName = "型号";
		dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		dt.Columns["number"].ColumnName = "数量";
		dt.Columns["prjCode"].ColumnName = "项目编号";
		dt.Columns["prjName"].ColumnName = "项目名称";
		dt.Columns["intime"].ColumnName = "录入日期";
		dt.Columns["wpcode"].ColumnName = "需求编号";
		dt.Columns.Remove("prjGuid");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dt.Rows[i]["num"] = i + 1;
		}
		dt.Columns["num"].ColumnName = "序号";
		dt.Columns["序号"].SetOrdinal(0);
		dt.Columns["资源编号"].SetOrdinal(1);
		dt.Columns["资源名称"].SetOrdinal(2);
		dt.Columns["资源类别"].SetOrdinal(3);
		dt.Columns["规格"].SetOrdinal(4);
		dt.Columns["品牌"].SetOrdinal(5);
		dt.Columns["型号"].SetOrdinal(6);
		dt.Columns["技术参数"].SetOrdinal(7);
		dt.Columns["数量"].SetOrdinal(8);
		dt.Columns["项目编号"].SetOrdinal(9);
		dt.Columns["项目名称"].SetOrdinal(10);
		dt.Columns["录入日期"].SetOrdinal(11);
		dt.Columns["需求编号"].SetOrdinal(12);
		return dt;
	}
}


