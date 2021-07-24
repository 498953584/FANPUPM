using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_ViewPayendFeedback : NBasePage, IRequiresSessionState
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
		this.lblTitle.Text = "查看收入合同交底反馈";
		ContractPayendModel model = this.contractPayendBll.GetModel(base.Request.QueryString["pid"]);
		this.lblBWasPerson.Text = this.GetUserName(model.BWasPerson);
		this.lblInputDate.Text = model.InTime.ToString();
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
		List<PayendFeedbackModel> listArray = this.payendFeedbackBll.GetListArray(string.Concat(new string[]
		{
			" where contractId='",
			model2.ContractID,
			"' and FeedbackPerson='",
			base.UserCode,
			"'"
		}));
		if (listArray.Count > 0)
		{
			this.txtFeedback.Text = listArray[0].FeedbackOpinion;
		}
		else
		{
			this.AddModel(model2.ContractID);
		}
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
	public int AddModel(string contractId)
	{
		PayendFeedbackModel payendFeedbackModel = new PayendFeedbackModel();
		payendFeedbackModel.Annex = "";
		payendFeedbackModel.ContractID = contractId;
		payendFeedbackModel.FeedbackOpinion = "";
		payendFeedbackModel.FeedbackPerson = base.UserCode;
		payendFeedbackModel.FeedbackState = "1";
		payendFeedbackModel.FeedbackTime = new DateTime?(DateTime.Now);
		payendFeedbackModel.ID = Guid.NewGuid().ToString();
		payendFeedbackModel.InPerson = PageHelper.QueryUser(this, base.UserCode);
		payendFeedbackModel.InTime = new DateTime?(DateTime.Now);
		return this.payendFeedbackBll.Add(payendFeedbackModel);
	}
}


