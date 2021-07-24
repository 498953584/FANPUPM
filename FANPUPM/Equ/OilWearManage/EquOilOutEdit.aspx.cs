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
public partial class Equ_OilWearManage_EquOilOutEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.BindInfo();
		}
	}
	private void BindInfo()
	{
		
	}
	private void BindDrop()
	{
		this.dropBalanceMode.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and ParentCodeID=0 AND XPM_Basic_CodeList.IsValid = 1");
		this.dropBalanceMode.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.txtOutCode.Text.Trim();
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


