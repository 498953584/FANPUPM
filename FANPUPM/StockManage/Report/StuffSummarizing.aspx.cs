using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_Report_StuffSummarizing : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private DataTable dt = new DataTable();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindDrop();
			this.BindTree();
			this.hfldPrjId.Value = this.tvProject.SelectedValue;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
	}
	protected void BindTree()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindTreeNodes(this.tvProject, this.Session["PmSet"], base.UserCode, base.Request["prjId"], this.ddlYear.SelectedItem.Text, this.ddlYear.SelectedValue);
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.hfldYear.Value = this.ddlYear.SelectedValue;
		this.txtCode.Text = "";
		this.txtName.Text = "";
		this.BindTree();
		this.ComputeTotal();
		this.BindGv();
	}
	protected void tvProject_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.hfldPrjId.Value = this.tvProject.SelectedValue;
		this.txtCode.Text = "";
		this.txtName.Text = "";
		this.ComputeTotal();
		this.BindGv();
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable stuffInfo = this.treasuryStockBll.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldPrjId.Value.Trim(), 0, 0, this.hfldIsWBSRelevance.Value, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		string[] array = new string[12];
		if (stuffInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BudQuantity)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(stuffInfo.Compute("sum(PurchaseNumber)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(stuffInfo.Compute("sum(PurchaseCost)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(stuffInfo.Compute("sum(StorageNumber)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(stuffInfo.Compute("sum(StorageCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(stuffInfo.Compute("sum(RealityNumber)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(stuffInfo.Compute("sum(RealityTotal)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(stuffInfo.Compute("sum(ProfitLossNumber)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(stuffInfo.Compute("sum(ProfitLossCost)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BalanceNumber)", "1>0")).ToString("0.000");
			array[11] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BalanceCost)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
			array[10] = "0.000";
			array[11] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = this.treasuryStockBll.GetStuffCount(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldPrjId.Value.Trim(), this.hfldIsWBSRelevance.Value, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		DataTable stuffInfo = this.treasuryStockBll.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldPrjId.Value.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, this.hfldIsWBSRelevance.Value, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		this.gvStuff.DataSource = stuffInfo;
		this.gvStuff.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvStuff_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[1].Text = "材料编号";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "材料名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "规格";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "品牌";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "型号";
			cells.Add(new TableHeaderCell());
			cells[6].ColumnSpan = 3;
			cells[6].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 5;
			cells[7].Text = "在途成本";
			cells.Add(new TableHeaderCell());
			cells[8].ColumnSpan = 3;
			cells[8].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[9].ColumnSpan = 2;
			cells[9].Text = "盈亏";
			cells.Add(new TableHeaderCell());
			cells[10].ColumnSpan = 2;
			cells[10].Text = "结存</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[11].Text = "目标数量";
			cells.Add(new TableHeaderCell());
			cells[12].Text = "目标价格";
			cells.Add(new TableHeaderCell());
			cells[13].Text = "目标总金额";
			cells.Add(new TableHeaderCell());
			cells[14].Text = "采购数量累计";
			cells.Add(new TableHeaderCell());
			cells[15].Text = "采购价格";
			cells.Add(new TableHeaderCell());
			cells[16].Text = "采购金额累计";
			cells.Add(new TableHeaderCell());
			cells[17].Text = "入库数量累计";
			cells.Add(new TableHeaderCell());
			cells[18].Text = "入库金额累计";
			cells.Add(new TableHeaderCell());
			cells[19].Text = "实际数量";
			cells[19].Attributes.Add("title", "  实际数量 = 出库数量 &ndash; 退库数量 ");
			cells[19].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[20].Text = "实际价格";
			cells[20].Attributes.Add("title", "  实际价格 = 编制采购单时的价格 ");
			cells[20].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[21].Text = "实际金额";
			cells[21].Attributes.Add("title", "  实际数量 = 出库中成本归集于材料的物资金额 &ndash; 退库中成本归集于材料的物资金额 ");
			cells[21].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[22].Text = "数量";
			cells[22].Attributes.Add("title", " 数量 = 目标数量 &ndash; 采购数量 ");
			cells[22].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[23].Text = "金额";
			cells[23].Attributes.Add("title", "  金额 = 目标金额 &ndash; 采购金额 ");
			cells[23].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[24].Text = "数量";
			cells[24].Attributes.Add("title", "  数量 = 采购数量 &ndash; 出库数量 ");
			cells[24].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[25].Text = "金额";
			cells[25].Attributes.Add("title", "  金额 = 采购金额 &ndash;  出库金额 ");
			cells[25].CssClass = "tooltip";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[6].Text = array[0];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[2];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[3];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[4];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[5];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[6];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
			e.Row.Cells[16].Text = array[7];
			e.Row.Cells[16].Style.Add("text-align", "right");
			e.Row.Cells[16].CssClass = "decimal_input";
			e.Row.Cells[17].Text = array[8];
			e.Row.Cells[17].Style.Add("text-align", "right");
			e.Row.Cells[17].CssClass = "decimal_input";
			e.Row.Cells[18].Text = array[9];
			e.Row.Cells[18].Style.Add("text-align", "right");
			e.Row.Cells[18].CssClass = "decimal_input";
			e.Row.Cells[19].Text = array[10];
			e.Row.Cells[19].Style.Add("text-align", "right");
			e.Row.Cells[19].CssClass = "decimal_input";
			e.Row.Cells[20].Text = array[11];
			e.Row.Cells[20].Style.Add("text-align", "right");
			e.Row.Cells[20].CssClass = "decimal_input";
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
		dataTable = this.treasuryStockBll.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldPrjId.Value.Trim(), 0, 0, this.hfldIsWBSRelevance.Value, this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["BudQuantity"] = dataTable.Compute("sum(BudQuantity)", "1>0");
			dataRow["BudTotal"] = dataTable.Compute("sum(BudTotal)", "1>0");
			dataRow["PurchaseNumber"] = dataTable.Compute("sum(PurchaseNumber)", "1>0");
			dataRow["PurchaseCost"] = dataTable.Compute("sum(PurchaseCost)", "1>0");
			dataRow["StorageNumber"] = dataTable.Compute("sum(StorageNumber)", "1>0");
			dataRow["StorageCost"] = dataTable.Compute("sum(StorageCost)", "1>0");
			dataRow["RealityNumber"] = dataTable.Compute("sum(RealityNumber)", "1>0");
			dataRow["RealityTotal"] = dataTable.Compute("sum(RealityTotal)", "1>0");
			dataRow["ProfitLossNumber"] = dataTable.Compute("sum(ProfitLossNumber)", "1>0");
			dataRow["ProfitLossCost"] = dataTable.Compute("sum(ProfitLossCost)", "1>0");
			dataRow["BalanceNumber"] = dataTable.Compute("sum(BalanceNumber)", "1>0");
			dataRow["BalanceCost"] = dataTable.Compute("sum(BalanceCost)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("目标成本", 1, 3, 6, 0));
		list.Add(ExcelHeader.Create("在途成本", 1, 5, 9, 0));
		list.Add(ExcelHeader.Create("实际成本", 1, 3, 14, 0));
		list.Add(ExcelHeader.Create("盈亏", 1, 2, 17, 0));
		list.Add(ExcelHeader.Create("结存", 1, 2, 19, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 6)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 6)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "材料分析", "材料分析", "材料分析.xls", list, null, 3, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ResourceId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ResourceId"]);
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "材料编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "材料名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["Brand"] != null)
		{
			dt.Columns["Brand"].ColumnName = "品牌";
		}
		if (dt.Columns["ModelNumber"] != null)
		{
			dt.Columns["ModelNumber"].ColumnName = "型号";
		}
		if (dt.Columns["BudQuantity"] != null)
		{
			dt.Columns["BudQuantity"].ColumnName = "目标数量";
		}
		if (dt.Columns["BudPrice"] != null)
		{
			dt.Columns["BudPrice"].ColumnName = "目标价格";
		}
		if (dt.Columns["BudTotal"] != null)
		{
			dt.Columns["BudTotal"].ColumnName = "目标总金额";
		}
		if (dt.Columns["PurchaseNumber"] != null)
		{
			dt.Columns["PurchaseNumber"].ColumnName = "采购数量累计";
		}
		if (dt.Columns["PurchaseSprice"] != null)
		{
			dt.Columns["PurchaseSprice"].ColumnName = "采购价格";
		}
		if (dt.Columns["PurchaseCost"] != null)
		{
			dt.Columns["PurchaseCost"].ColumnName = "采购金额累计";
		}
		if (dt.Columns["StorageNumber"] != null)
		{
			dt.Columns["StorageNumber"].ColumnName = "入库数量累计";
		}
		if (dt.Columns["StorageCost"] != null)
		{
			dt.Columns["StorageCost"].ColumnName = "入库金额累计";
		}
		if (dt.Columns["RealityNumber"] != null)
		{
			dt.Columns["RealityNumber"].ColumnName = "实际数量";
		}
		if (dt.Columns["RealityPrice"] != null)
		{
			dt.Columns["RealityPrice"].ColumnName = "实际价格";
		}
		if (dt.Columns["RealityTotal"] != null)
		{
			dt.Columns["RealityTotal"].ColumnName = "实际金额";
		}
		if (dt.Columns["ProfitLossNumber"] != null)
		{
			dt.Columns["ProfitLossNumber"].ColumnName = "数量";
		}
		if (dt.Columns["ProfitLossCost"] != null)
		{
			dt.Columns["ProfitLossCost"].ColumnName = "金额";
		}
		if (dt.Columns["BalanceNumber"] != null)
		{
			dt.Columns["BalanceNumber"].ColumnName = " 数量 ";
		}
		if (dt.Columns["BalanceCost"] != null)
		{
			dt.Columns["BalanceCost"].ColumnName = " 金额 ";
		}
		dt.AcceptChanges();
		return dt;
	}
}


