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
public partial class ContractManage_PayoutPayment_PayoutPayment : NBasePage, IRequiresSessionState
{
	private string contractId = string.Empty;
	private PayoutPayment payoutPayment = new PayoutPayment();
	private string module = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			this.contractId = base.Request["ContractID"];
		}
		if (!string.IsNullOrEmpty(base.Request["Module"]))
		{
			this.module = base.Request["Module"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			int num = 0;
			if (string.Compare(this.module, "Archives", true) == 0)
			{
				num = 1;
			}
			string text = string.Format(" Con_Payout_Contract.IsArchived = {0} ", num);
			text += string.Format(" AND Con_Payout_Contract.ContractID = '{0}' ", this.contractId);
			if (this.module == "Archived")
			{
				text += "and conState in (4,5)";
			}
			List<PayoutPaymentModel> list = this.payoutPayment.GetList(text, base.UserCode);
			this.gvwContract.DataSource = list;
			this.gvwContract.DataBind();
		}
	}
}


