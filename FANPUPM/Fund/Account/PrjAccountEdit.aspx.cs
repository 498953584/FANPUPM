using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.Serialize;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.Fund.Account;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjAccountEdit : NBasePage, IRequiresSessionState
{
	private string _action = string.Empty;
	private string _ic = string.Empty;
	private string temGID;
	private accountMange am = new accountMange();
	private AccountLogic bll = new AccountLogic();

	public string _Action
	{
		get
		{
			return this._action;
		}
		set
		{
			this._action = value;
		}
	}
	public string _Ic
	{
		get
		{
			return this._ic;
		}
		set
		{
			this._ic = value;
		}
	}
	public string TemGID
	{
		get
		{
			return this.temGID;
		}
		set
		{
			this.temGID = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["Action"] != null && base.Request.QueryString["Action"].ToString() != "")
		{
			this._Action = base.Request.QueryString["Action"].ToString().ToLower();
		}
		if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"].ToString() != "")
		{
			this._Ic = base.Request.QueryString["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.TemGID = Guid.NewGuid().ToString();
			if (this._Action == "edit")
			{
				this.FileUpload1.Class = "PrjAccount";
				this.FileUpload1.RecordCode = this._Ic;
				this.ToInitialize();
				return;
			}
			if (this._Action == "add")
			{
				this.hdnUser.Value = "[\"00000000\",\"" + base.UserCode + "\"]";
				this.hdnAccount.Value = this.temGID.ToString();
				this.FileUpload1.Class = "PrjAccount";
				this.FileUpload1.RecordCode = this.TemGID.ToString();
				return;
			}
			this.FileUpload1.Class = "PrjAccount";
			this.FileUpload1.RecordCode = this._Ic;
			this.ToInitialize();
			this.initSate();
		}
	}
	private void ToInitialize()
	{
		if (this._Ic != "" && this._Ic.Length == 36)
		{
			AccounModel accounModel = new AccounModel();
			accounModel = this.bll.GetModel(this._ic);
			if (accounModel != null)
			{
				this.hdnAccount.Value = accounModel.AccountID.ToString();
				this.hdnUser.Value = accounModel.authorizer.ToString();
				this.txtaccountNum.Text = accounModel.accountNum;
				this.txtacountName.Text = accounModel.acountName;
				this.txtRemark.Value = accounModel.Remark;
				this.txtinitialFund.Text = accounModel.initialFund.ToString();
				this.txtPrjName.Text = this.bll.getPrjName(accounModel.PrjGuid);
				this.hdnProjectCode.Value = accounModel.PrjGuid;
			}
		}
	}
	private AccounModel getModel()
	{
		AccounModel accounModel = new AccounModel();
		if (this._Action == "add")
		{
			accounModel.AccountID = new Guid(this.hdnAccount.Value);
			accounModel.creatDate = new DateTime?(DateTime.Now);
			accounModel.createMan = base.UserCode;
			accounModel.authorizer = this.hdnUser.Value.ToString();
		}
		else
		{
			if (this._Action == "edit")
			{
				accounModel.AccountID = new Guid(this._Ic);
			}
		}
		accounModel.PrjGuid = this.convPrjCode(this.hdnProjectCode.Value);
		accounModel.accountNum = this.txtaccountNum.Text.Trim();
		accounModel.acountName = this.txtacountName.Text.Trim();
		accounModel.Remark = this.txtRemark.Value;
		if (this.txtinitialFund.Text != "")
		{
			accounModel.initialFund = new decimal?(decimal.Parse(this.txtinitialFund.Text));
		}
		else
		{
			accounModel.initialFund = new decimal?(0m);
		}
		return accounModel;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		AccounModel accounModel = new AccounModel();
		bool flag = false;
		if (this._Action == "add")
		{
			accounModel = this.getModel();
			if (!this.bll.Exists(accounModel.accountNum.ToString(), accounModel.AccountID))
			{
				if (this.bll.Add(accounModel) > 0)
				{
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
		}
		else
		{
			if (this._Action == "edit")
			{
				accounModel = this.getModel();
				flag = (!this.bll.Exists(accounModel.accountNum.ToString(), accounModel.AccountID) && this.bll.Update(accounModel));
			}
		}
		if (flag)
		{
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_prjaccount' })");
			return;
		}
		base.RegisterScript("top.ui.alert('项目账户编号不允许重复，数据保存失败');");
	}
	private void initSate()
	{
		if (this._Ic != "" && this._Ic.Length == 36)
		{
			this.txtaccountNum.Enabled = false;
			this.txtRemark.Disabled = true;
			this.txtacountName.Enabled = false;
			this.txtinitialFund.Enabled = false;
			this.btnSave.Enabled = false;
		}
	}
	protected void btnSel_Click(object sender, EventArgs e)
	{
		string value = this.hdnProjectCode.Value;
		this.txtPrjName.Text = this.bll.getPrjName(value);
	}
	protected string convPrjCode(string jsonPrjGuid)
	{
		string text = "";
		ISerializable serializable = new JsonSerializer();
		if (jsonPrjGuid.StartsWith("["))
		{
			string[] array = serializable.Deserialize<string[]>(jsonPrjGuid);
			for (int i = 0; i < array.Length; i++)
			{
				if (i == array.Length - 1)
				{
					text = text + "'" + array[i].ToString() + "'";
				}
				else
				{
					text = text + "'" + array[i].ToString() + "',";
				}
			}
		}
		else
		{
			text = jsonPrjGuid;
		}
		return text;
	}
}


