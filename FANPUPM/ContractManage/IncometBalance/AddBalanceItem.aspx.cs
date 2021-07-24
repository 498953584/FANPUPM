using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometBalance_AddBalanceItem : NBasePage, IRequiresSessionState
{
	private ConBalanceItemService balanceItemSev = new ConBalanceItemService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitialPage();
		}
	}
	public void InitialPage()
	{
		if (!string.IsNullOrEmpty(base.Request["balanceId"]))
		{
			this.hfldBalanceID.Value = base.Request["balanceId"];
		}
		if (base.Request["action"] == "Add")
		{
			this.hfldBalanceItemId.Value = Guid.NewGuid().ToString();
			this.fileBalanceItem.RecordCode = this.hfldBalanceItemId.Value;
			return;
		}
		if (base.Request["action"] == "Edit")
		{
			this.hfldconBalanceItem.Value = base.Request.Cookies["balanceJson"].Value;
			ConBalanceItem conBalanceItem = JsonConvert.DeserializeObject<ConBalanceItem>(this.hfldconBalanceItem.Value);
			if (conBalanceItem != null)
			{
				this.hfldBalanceItemId.Value = conBalanceItem.Id;
				this.txtName.Text = conBalanceItem.Name;
				this.txtNode.Text = conBalanceItem.Note;
				this.txtQty.Text = conBalanceItem.Qty.ToString();
				this.txtUnitPrice.Text = conBalanceItem.UnitPrice.ToString();
				this.txtTotal.Text = (conBalanceItem.Qty * conBalanceItem.UnitPrice).ToString();
				this.dropType.SelectedValue = conBalanceItem.Type;
				this.fileBalanceItem.RecordCode = this.hfldBalanceItemId.Value;
			}
		}
	}
	public void btnSave_Click(object sender, EventArgs e)
	{
		ConBalanceItem conBalanceItem = new ConBalanceItem();
		conBalanceItem.Id = this.hfldBalanceItemId.Value;
		conBalanceItem.BalanceId = this.hfldBalanceID.Value;
		conBalanceItem.Name = this.txtName.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtQty.Text.Trim()))
		{
			conBalanceItem.Qty = Convert.ToDecimal(this.txtQty.Text.Trim());
		}
		if (!string.IsNullOrEmpty(this.txtUnitPrice.Text))
		{
			conBalanceItem.UnitPrice = Convert.ToDecimal(this.txtUnitPrice.Text.Trim());
		}
		conBalanceItem.Note = this.txtNode.Text.Trim();
		conBalanceItem.Name = this.txtName.Text.Trim();
		conBalanceItem.Type = this.dropType.SelectedItem.Value;
		string value = JsonConvert.SerializeObject(conBalanceItem);
		base.Response.Cookies["balanceJson"].Value = value;
		base.RegisterScript("btnSave();");
	}
}


