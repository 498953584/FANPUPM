using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_Purchase : NBasePage, IRequiresSessionState
{
	private PurchaseStock purchaseStock = new PurchaseStock();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvwPurchase.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.DataBindPurchase();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.DataBindPurchase();
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPurchase.PageIndex = e.NewPageIndex;
		this.DataBindPurchase();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable exportDataSource = this.GetExportDataSource();
		Common2 common = new Common2();
		common.ExportToExecelAll(exportDataSource, "物资采购报表.xls", base.Request.Browser.Browser);
	}
	private void DataBindPurchase()
	{
		DataTable purchaseDataSource = this.GetPurchaseDataSource();
		if (purchaseDataSource.Rows.Count == 0)
		{
			purchaseDataSource.Rows.Add(purchaseDataSource.NewRow());
			this.gvwPurchase.DataSource = purchaseDataSource;
			this.gvwPurchase.DataBind();
			this.gvwPurchase.Rows[0].Visible = false;
			return;
		}
		this.gvwPurchase.DataSource = purchaseDataSource;
		this.gvwPurchase.DataBind();
		string total = purchaseDataSource.Compute("Sum(total)", string.Empty).ToString();
		GridViewUtility.AddTotalRow(this.gvwPurchase, total, 11);
	}
	private DataTable GetPurchaseDataSource()
	{
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		return this.purchaseStock.GetReportDataSource(startDate, endDate, this.txtResourceName.Text.Trim(), this.txtResourceCode.Text.Trim(), this.txtPurchaseCode.Text.Trim(), this.txtCorpName.Text.Trim(), this.txtProjectName.Text.Trim(), this.txtCategory.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
	}
	private DataTable GetExportDataSource()
	{
		DataTable purchaseDataSource = this.GetPurchaseDataSource();
		purchaseDataSource.Columns.Remove("pid");
		purchaseDataSource.Columns.Remove("psid");
		purchaseDataSource.Columns.Remove("corp");
		purchaseDataSource.Columns.Remove("contract");
		purchaseDataSource.Columns.Remove("Project");
		purchaseDataSource.Columns["RowNumber"].ColumnName = "序号";
		purchaseDataSource.Columns["scode"].ColumnName = "资源编号";
		purchaseDataSource.Columns["ResourceName"].ColumnName = "资源名称";
		purchaseDataSource.Columns["Specification"].ColumnName = "规格";
		purchaseDataSource.Columns["Brand"].ColumnName = "品牌";
		purchaseDataSource.Columns["ModelNumber"].ColumnName = "型号";
		purchaseDataSource.Columns["TechnicalParameter"].ColumnName = "技术参数";
		purchaseDataSource.Columns["UnitName"].ColumnName = "单位";
		purchaseDataSource.Columns["pscode"].ColumnName = "单据号";
		purchaseDataSource.Columns["number"].ColumnName = "数量";
		purchaseDataSource.Columns["sprice"].ColumnName = "单价";
		purchaseDataSource.Columns["total"].ColumnName = "小计";
		purchaseDataSource.Columns["CorpName"].ColumnName = "供应商";
		purchaseDataSource.Columns["intime"].ColumnName = "采购日期";
		purchaseDataSource.Columns["PrjName"].ColumnName = "项目名称";
		purchaseDataSource.Columns["品牌"].SetOrdinal(5);
		purchaseDataSource.Columns["型号"].SetOrdinal(6);
		purchaseDataSource.Columns["技术参数"].SetOrdinal(7);
		purchaseDataSource.Columns["单价"].SetOrdinal(9);
		purchaseDataSource.Columns["ResourceTypeName"].ColumnName = "资源类别";
		purchaseDataSource.Columns["资源类别"].SetOrdinal(3);
		purchaseDataSource.Columns["项目名称"].SetOrdinal(13);
		return purchaseDataSource;
	}
}


