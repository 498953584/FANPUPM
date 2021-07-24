using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutInvoice_PayoutInvoiceEdit : NBasePage, IRequiresSessionState
{
	private string contractId = string.Empty;
	private PayoutInvoice invoice = new PayoutInvoice();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			this.contractId = base.Request["ContractID"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdContractID.Value = this.contractId;
			string text = " Con_Payout_Contract.IsArchived = 0 ";
			text += string.Format(" AND Con_Payout_Contract.ContractID = '{0}' ", this.contractId);
			List<PayoutInvoiceInfo> list = this.invoice.GetList(text, base.UserCode);
			this.gvwInvoice.DataSource = list;
			this.gvwInvoice.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldInvoice.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldInvoice.Value);
		}
		else
		{
			list.Add(this.hfldInvoice.Value);
		}
		try
		{
			this.invoice.Delete(list);
			base.RegisterScriptRefresh();
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
	}
	protected void gvwInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwInvoice.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


