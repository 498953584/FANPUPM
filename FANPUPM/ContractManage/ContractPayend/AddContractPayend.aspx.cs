using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_AddContractPayend : NBasePage, IRequiresSessionState
{
	private ContractPayendBll contractPayendBll = new ContractPayendBll();
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑收入合同交底";
			ContractPayendModel model = this.contractPayendBll.GetModel(base.Request.QueryString["id"]);
			this.txtBWasPerson.Text = this.GetUserName(model.BWasPerson);
			this.hdBWasPerson.Value = model.BWasPerson;
			this.txtInputDate.Text = model.InTime.ToString();
			this.txtInputPerson.Text = model.InPerson;
			this.txtOtherExplain.Text = model.OtherExplain;
			this.txtProjectCondition.Text = model.ProjectCondition;
			this.txtProvisionMatter.Text = model.ProvisionMatter;
			this.txtPayendTopics.Text = model.PayendTopics;
			this.hdGuid.Value = model.PayendID;
			this.hdModifyState.Value = model.ModifyState;
			IncometContractModel model2 = this.incometContractBll.GetModel(model.ContractID);
			this.txtContractCode.Text = model2.ContractCode;
			this.txtContractName.Text = model2.ContractName;
			this.txtContractPrice.Text = model2.ContractPrice.ToString();
			this.txtSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
			this.hdContractId.Value = model2.ContractID;
			this.hdContractEditId.Value = model2.ContractID;
		}
		else
		{
			this.lblTitle.Text = "新增收入合同交底";
			this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.hdModifyState.Value = "1";
		}
		if (base.Request.QueryString["t"] != null)
		{
			this.lblTitle.Text = "查看收入合同交底";
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
		int count = this.contractPayendBll.GetListArray(" where ContractId='" + this.hdContractId.Value + "'").Count;
		if ((base.Request.QueryString["id"] == null && count > 0) || (base.Request.QueryString["id"] != null && this.hdContractEditId.Value != this.hdContractId.Value && count > 0))
		{
			base.RegisterScript("top.ui.alert('此合同已经交底,请选择其他合同进行交底!!')");
			return;
		}
		int num;
		if (base.Request.QueryString["id"] != null)
		{
			num = this.UpdateModel();
		}
		else
		{
			num = this.AddModel();
		}
		if (num != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('" + this.lblTitle.Text + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("winclose('AddContractPayend', 'PayendSend.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.lblTitle.Text + "失败！');");
	}
	public int AddModel()
	{
		return this.contractPayendBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.contractPayendBll.Update(this.GetModel());
	}
	public ContractPayendModel GetModel()
	{
		return new ContractPayendModel
		{
			Annex = "",
			BWasPerson = this.hdBWasPerson.Value,
			ContractID = this.hdContractId.Value,
			InPerson = this.txtInputPerson.Text,
			InTime = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text)),
			OtherExplain = this.txtOtherExplain.Text,
			PayendID = this.hdGuid.Value,
			ProjectCondition = this.txtProjectCondition.Text,
			ProvisionMatter = this.txtProvisionMatter.Text,
			PayendTopics = this.txtPayendTopics.Text,
			ModifyState = this.hdModifyState.Value,
			WasPerson = base.UserCode
		};
	}
	protected void btnSub_Click(object sender, EventArgs e)
	{
		this.hdModifyState.Value = "0";
		this.Save();
	}
	protected void btnRe_Click(object sender, EventArgs e)
	{
		IncometContractModel model = new IncometContractBll().GetModel(this.hdContractId.Value);
		this.txtContractCode.Text = model.ContractCode;
		this.txtContractPrice.Text = model.ContractPrice.ToString();
		this.txtSignedTime.Text = Common2.GetTime(model.SignedTime.ToString());
		this.txtContractName.Text = model.ContractName;
		this.hdContractId.Value = model.ContractID;
	}
}


