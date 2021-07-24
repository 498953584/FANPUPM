using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_AmountTreasury : NBasePage, IRequiresSessionState
{
	private TreasuryStockBll treasuryStock = new TreasuryStockBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwTreasury.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		DataTable treasuryData = new TreasuryStockBll().GetTreasuryData(this.txtResourceName.Text.Trim(), this.txtResourceCode.Text.Trim(), this.txtTrea.Text.Trim(), this.txtCorpName.Text.Trim(), base.UserCode, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		if (treasuryData.Rows.Count == 0)
		{
			treasuryData.Rows.Add(treasuryData.NewRow());
			this.gvwTreasury.DataSource = treasuryData;
			this.gvwTreasury.DataBind();
			this.gvwTreasury.Rows[0].Visible = false;
			return;
		}
		this.gvwTreasury.DataSource = treasuryData;
		this.gvwTreasury.DataBind();
		string total = Convert.ToString(treasuryData.Compute("Sum(total)", string.Empty));
		GridViewUtility.AddTotalRow(this.gvwTreasury, total, 10);
	}
	public DataTable GetExportDataSource()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("资源编号");
		dataTable.Columns.Add("资源名称");
		dataTable.Columns.Add("规格");
		dataTable.Columns.Add("品牌");
		dataTable.Columns.Add("型号");
		dataTable.Columns.Add("技术参数");
		dataTable.Columns.Add("单位");
		dataTable.Columns.Add("单价");
		dataTable.Columns.Add("数量");
		dataTable.Columns.Add("小计");
		dataTable.Columns.Add("供应商");
		dataTable.Columns.Add("仓库名称");
		DataTable treasuryData = new TreasuryStockBll().GetTreasuryData(this.txtResourceName.Text.Trim(), this.txtResourceCode.Text.Trim(), this.txtTrea.Text.Trim(), this.txtCorpName.Text.Trim(), base.UserCode, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		if (treasuryData.Rows.Count > 0)
		{
			for (int i = 0; i < treasuryData.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["序号"] = (i + 1).ToString();
				dataRow["资源编号"] = treasuryData.Rows[i]["scode"].ToString();
				dataRow["资源名称"] = treasuryData.Rows[i]["ResourceName"].ToString();
				dataRow["规格"] = treasuryData.Rows[i]["Specification"].ToString();
				dataRow["品牌"] = treasuryData.Rows[i]["Brand"].ToString();
				dataRow["型号"] = treasuryData.Rows[i]["ModelNumber"];
				dataRow["技术参数"] = treasuryData.Rows[i]["TechnicalParameter"];
				dataRow["单位"] = treasuryData.Rows[i]["Name"].ToString();
				dataRow["数量"] = treasuryData.Rows[i]["snumber"].ToString();
				dataRow["单价"] = treasuryData.Rows[i]["sprice"].ToString();
				dataRow["小计"] = treasuryData.Rows[i]["total"].ToString();
				dataRow["供应商"] = treasuryData.Rows[i]["CorpName"].ToString();
				dataRow["仓库名称"] = treasuryData.Rows[i]["tname"].ToString();
				dataTable.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = "合计";
			dataRow2["资源编号"] = "";
			dataRow2["资源名称"] = "";
			dataRow2["规格"] = "";
			dataRow2["品牌"] = "";
			dataRow2["型号"] = "";
			dataRow2["技术参数"] = "";
			dataRow2["单位"] = "";
			dataRow2["数量"] = "";
			dataRow2["单价"] = "";
			dataRow2["小计"] = Convert.ToString(treasuryData.Compute("Sum(total)", string.Empty));
			dataRow2["供应商"] = "";
			dataRow2["仓库名称"] = "";
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, EventArgs e)
	{
		DataTable exportDataSource = this.GetExportDataSource();
		Common2 common = new Common2();
		common.ExportToExecelAll(exportDataSource, "仓库信息—按数量报表.xls", base.Request.Browser.Browser);
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwTreasury.PageIndex = e.NewPageIndex;
		this.BindGvw();
	}
}


