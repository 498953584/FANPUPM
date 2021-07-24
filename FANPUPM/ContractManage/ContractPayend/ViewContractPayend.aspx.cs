using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_ViewContractPayend : NBasePage, IRequiresSessionState
{
	private ContractPayendBll contractPayendBll = new ContractPayendBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PayendFeedbackBll payendFeedbackBll = new PayendFeedbackBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.lblTitle.Text = "查看收入合同交底";
		ContractPayendModel model = this.contractPayendBll.GetModel(base.Request.QueryString["id"]);
		this.lblBWasPerson.Text = this.GetUserName(model.BWasPerson);
		this.lblInputDate.Text = Common2.GetTime(model.InTime.ToString());
		this.lblInputPerson.Text = model.InPerson;
		this.txtOtherExplain.Text = model.OtherExplain;
		this.txtProjectCondition.Text = model.ProjectCondition;
		this.txtProvisionMatter.Text = model.ProvisionMatter;
		this.hdGuid.Value = model.PayendID;
		IncometContractModel model2 = this.incometContractBll.GetModel(model.ContractID);
		this.lblContractCode.Text = model2.ContractCode;
		this.lblContractName.Text = model2.ContractName;
		this.lblContractPrice.Text = model2.ContractPrice.ToString();
		this.lblSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
		this.lblPayendTopics.Text = model.PayendTopics;
		this.FileLink1.MID = 1909;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public new string GetUserName(string userCode)
	{
		string text = "";
		string[] array = userCode.Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string v_yhdm = array2[i];
			PtYhmc modelById = new PtYhmcBll().GetModelById(v_yhdm);
			if (modelById != null)
			{
				text = text + modelById.v_xm + ",";
			}
		}
		return text;
	}
}


