using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_AddPayendFeedback : NBasePage, IRequiresSessionState
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
		ContractPayendModel model = this.contractPayendBll.GetModel(base.Request.QueryString["pid"]);
		this.lblBWasPerson.Text = this.GetUserName(model.BWasPerson);
		this.lblInputDate.Text = model.InTime.ToString();
		this.lblInputPerson.Text = model.InPerson;
		this.lblOtherExplain.Text = model.OtherExplain;
		this.lblProjectCondition.Text = model.ProjectCondition;
		this.lblProvisionMatter.Text = model.ProvisionMatter;
		this.hdGuid.Value = model.PayendID;
		IncometContractModel model2 = this.incometContractBll.GetModel(model.ContractID);
		this.lblContractCode.Text = model2.ContractCode;
		this.lblContractName.Text = model2.ContractName;
		this.lblContractPrice.Text = model2.ContractPrice.ToString();
		this.lblSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
		this.hdContractId.Value = model2.ContractID;
		List<PayendFeedbackModel> listArray = this.payendFeedbackBll.GetListArray(string.Concat(new string[]
		{
			" where contractId='",
			model2.ContractID,
			"' and FeedbackPerson='",
			base.UserCode,
			"'"
		}));
		if (listArray.Count == 0)
		{
			this.lblTitle.Text = "新增收入合同交底反馈";
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.hdFeedbackState.Value = "1";
			this.AddModel();
		}
		else
		{
			this.hdFeedbackState.Value = "2";
			this.lblTitle.Text = "修改收入合同交底反馈";
			this.hdGuid.Value = listArray[0].ID;
			this.txtFeedback.Text = listArray[0].FeedbackOpinion;
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
	protected void btnSave_Click(object sender, EventArgs e)
	{
		this.Save();
	}
	private void Save()
	{
		int num = this.UpdateModel();
		if (num != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('" + this.lblTitle.Text + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("window.location.reload('PayendReceive.aspx');");
			stringBuilder.Append("window.close();");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.lblTitle.Text + "失败！');");
	}
	public int AddModel()
	{
		return this.payendFeedbackBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.payendFeedbackBll.Update(this.GetModel());
	}
	public PayendFeedbackModel GetModel()
	{
		return new PayendFeedbackModel
		{
			Annex = "",
			ContractID = this.hdContractId.Value,
			FeedbackOpinion = this.txtFeedback.Text,
			FeedbackPerson = base.UserCode,
			FeedbackState = this.hdFeedbackState.Value,
			FeedbackTime = new DateTime?(DateTime.Now),
			ID = this.hdGuid.Value,
			InPerson = PageHelper.QueryUser(this, base.UserCode),
			InTime = new DateTime?(DateTime.Now)
		};
	}
	protected void btnSub_Click(object sender, EventArgs e)
	{
		this.hdFeedbackState.Value = "3";
		this.Save();
	}
}


