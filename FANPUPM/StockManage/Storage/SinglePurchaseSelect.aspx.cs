using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_SinglePurchaseSelect : NBasePage, IRequiresSessionState
{
	private const string ChkPurchaseName = "chkPurchase";
	private const string LblPpcodeName = "lblPpcode";
	private Purchase purchase = new Purchase();
	private PurchaseStock purchaseStock = new PurchaseStock();
	private ISerializable ser = new JsonSerializer();
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.gvwPurchase.PageSize = NBasePage.pagesize3;
		this.Page.ClientScript.GetPostBackEventReference(this, null);
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindPurchase();
			this.DataBindPurchaseStock();
		}
	}
	protected void gvwPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvwPurchaseStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPurchase.PageIndex = e.NewPageIndex;
		this.DataBindPurchase();
		this.SetCheckedBoxStatus();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.DataBindPurchase();
	}
	private void DataBindPurchase()
	{
		System.Collections.Generic.List<PurchaseModel> purchses = this.GetPurchses();
		GridViewUtility.DataBind<PurchaseModel>(this.gvwPurchase, purchses);
	}
	private System.Collections.Generic.List<PurchaseModel> GetPurchses()
	{
		System.DateTime startDate = Common2.GetStartDate(this.txtStartTime);
		System.DateTime dateTime = Common2.GetEndDate(this.txtEndTime);
		if (!string.IsNullOrEmpty(this.txtEndTime.Text))
		{
			dateTime = Common2.GetEndDate(this.txtEndTime).AddDays(1.0);
		}
		System.Collections.Generic.List<PurchaseModel> result = new System.Collections.Generic.List<PurchaseModel>();
		if (startDate == System.DateTime.MinValue || dateTime == System.DateTime.MinValue)
		{
			base.RegisterScript("alert('系统提示：\\n\\n日期格式错误！')");
			return result;
		}
		return this.purchase.Select(startDate, dateTime, this.txtPcode.Text.Trim(), this.prjId, base.UserCode, 1);
	}
	private void DataBindPurchaseStock()
	{
		System.Collections.Generic.List<string> purchasePlanStock = this.GetPurchasePlanStock();
		DataTable tableByPurchaseCodes = this.purchaseStock.GetTableByPurchaseCodes(purchasePlanStock.ToArray());
		GridViewUtility.DataBind(this.gvwPurchaseStock, tableByPurchaseCodes);
	}
	private System.Collections.Generic.List<string> GetPurchasePlanStock()
	{
		System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
		string[] array = this.ser.Deserialize<string[]>(this.hfldOldCodes.Value);
		if (array != null)
		{
			result = array.ToList<string>();
		}
		return result;
	}
	private void SetCheckedBoxStatus()
	{
		System.Collections.Generic.List<string> purchasePlanStock = this.GetPurchasePlanStock();
		foreach (GridViewRow gridViewRow in this.gvwPurchase.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkPurchase") as CheckBox;
			Label label = gridViewRow.FindControl("lblPpcode") as Label;
			if (checkBox != null && label != null && purchasePlanStock.Contains(label.Text) && !checkBox.Checked)
			{
				checkBox.Checked = true;
			}
			else
			{
				if (checkBox != null && label != null && !purchasePlanStock.Contains(label.Text) && checkBox.Checked)
				{
					checkBox.Checked = false;
				}
			}
		}
	}
	protected void btnRefresh_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldCurCodes.Value))
		{
			System.Collections.Generic.List<string> purchasePlanStock = this.GetPurchasePlanStock();
			purchasePlanStock.Add(this.hfldCurCodes.Value);
			this.hfldOldCodes.Value = this.ser.Serialize<string[]>(purchasePlanStock.ToArray());
		}
		this.SetCheckedBoxStatus();
		this.DataBindPurchaseStock();
	}
}


