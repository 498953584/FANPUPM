using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_Storage_InitializeStorage : NBasePage, IRequiresSessionState
{
	private const int tflagIndex = 3;
	private const string rootCode = "0";
	private TreasuryPermitBll treasuryPermitBll = new TreasuryPermitBll();
	private TreasuryStockBll tStock = new TreasuryStockBll();
	private readonly Treasury treasury = new Treasury();
	private SmEnum.SmSetValue manageMode;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.manageMode = this.treasury.GetManageMode();
		if (this.treasury.GetCount() == 0)
		{
			this.treasury.AddRoot();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		string text = string.Empty;
		if (!base.IsPostBack)
		{
			this.treasury.ParseTreasuryList(this.tvTreasury, base.UserCode, true, true);
			if (!string.IsNullOrEmpty(base.Request["tcode"]))
			{
				text = base.Request["tcode"];
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("0");
				for (int i = 0; i < text.Length / 3; i++)
				{
					stringBuilder.Append("/" + text.Substring(0, 3 * (i + 1)));
				}
				this.tvTreasury.FindNode(stringBuilder.ToString()).Select();
			}
			this.BindGridView(text);
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGridView(this.tvTreasury.SelectedValue);
	}
	protected void setStorageLimits()
	{
		string text = string.Empty;
		bool disabled = true;
		int count = this.tStock.GetCount(this.tvTreasury.SelectedValue);
		if (count > 0)
		{
			text = "提示:此仓库已存在物资，不能初始化！";
		}
		else
		{
			text = string.Empty;
			disabled = false;
		}
		this.btnInitializeStorage.Disabled = disabled;
		this.lblWarningMessage.Text = text;
	}
	private void BindGridView(string currentTcode)
	{
		this.AspNetPager1.RecordCount = this.tStock.GetGoodsCount(currentTcode);
		DataTable goods = this.tStock.GetGoods(currentTcode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvwTreasury.DataSource = goods;
		this.gvwTreasury.DataBind();
		if (goods.Rows.Count > 0)
		{
			string total = this.tStock.GetTotal(currentTcode).ToString("F3");
			GridViewUtility.AddTotalRow(this.gvwTreasury, total, 11);
		}
		this.hfldTcode.Value = this.tvTreasury.SelectedValue;
		if (this.tvTreasury.SelectedNode != null)
		{
			this.hfldTcodeText.Value = this.tvTreasury.SelectedNode.Text;
		}
		if (!string.IsNullOrEmpty(currentTcode))
		{
			this.btnInitializeStorage.Disabled = false;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGridView(this.tvTreasury.SelectedValue);
	}
	protected void gvwTreasury_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			e.Row.Attributes["ResourceCode"] = this.gvwTreasury.DataKeys[e.Row.RowIndex]["ResourceCode"].ToString();
			e.Row.Attributes["sprice"] = this.gvwTreasury.DataKeys[e.Row.RowIndex]["sprice"].ToString();
			e.Row.Attributes["CorpId"] = this.gvwTreasury.DataKeys[e.Row.RowIndex]["CorpId"].ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<TreasuryStorckParms> list = new System.Collections.Generic.List<TreasuryStorckParms>();
		foreach (GridViewRow gridViewRow in this.gvwTreasury.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkSelectSingle") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				string resourcCode = this.gvwTreasury.DataKeys[gridViewRow.RowIndex]["ResourceCode"].ToString();
				decimal sprice = System.Convert.ToDecimal(this.gvwTreasury.DataKeys[gridViewRow.RowIndex]["sprice"].ToString());
				string cropId = this.gvwTreasury.DataKeys[gridViewRow.RowIndex]["CorpId"].ToString();
				list.Add(new TreasuryStorckParms
				{
					cropId = cropId,
					sprice = sprice,
					resourcCode = resourcCode
				});
			}
		}
		TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
		string selectedValue = this.tvTreasury.SelectedValue;
		treasuryStockBll.DeleteByUnite(selectedValue, list);
		this.BindGridView(selectedValue);
	}
}


