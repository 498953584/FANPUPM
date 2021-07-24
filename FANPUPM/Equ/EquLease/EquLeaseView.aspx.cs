using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractDAL;
using cn.justwin.contractModel;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquLease_EquLeaseView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string.IsNullOrEmpty(this.id);
		}
	}
	private string GetIncometContractName(string contractID)
	{
		IncometContract incometContract = new IncometContract();
		IncometContractModel model = incometContract.GetModel(contractID);
		if (model != null)
		{
			return model.ContractName;
		}
		return string.Empty;
	}
	private string GetPayoutContractName(string contractID)
	{
		cn.justwin.contractBLL.PayoutContract payoutContract = new cn.justwin.contractBLL.PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(contractID);
		if (model == null)
		{
			return string.Empty;
		}
		return model.ContractName;
	}
}


