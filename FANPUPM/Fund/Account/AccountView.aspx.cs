using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.Fund.Account;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountView : NBasePage, IRequiresSessionState
{
	private string _action = string.Empty;
	private string _ic = string.Empty;
	private string _prjCode = string.Empty;
	private string _prjName = string.Empty;
	private string temGID;
	private accountMange am = new accountMange();

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
	public string _PrjCode
	{
		get
		{
			return this._prjCode;
		}
		set
		{
			this._prjCode = value;
		}
	}
	public string _PrjName
	{
		get
		{
			return this._prjName;
		}
		set
		{
			this._prjName = value;
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
		this.FileLink1.MID = 1755;
		if (base.Request.QueryString["Action"] != null && base.Request.QueryString["Action"].ToString() != "")
		{
			this._Action = base.Request.QueryString["Action"].ToString().ToLower();
		}
		if (base.Request.QueryString["PrjCode"] != null && base.Request.QueryString["PrjCode"].ToString() != "")
		{
			this._PrjCode = base.Request.QueryString["PrjCode"].ToString();
		}
		if (base.Request.QueryString["PrjName"] != null && base.Request.QueryString["PrjName"].ToString() != "")
		{
			this._PrjName = base.Request.QueryString["PrjName"].ToString();
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
			string text = this.Session["yhdm"].ToString();
			DataTable dataTable = PersonnelAction.QueryPersonnelById(text);
			if (dataTable != null && dataTable.Rows.Count == 1)
			{
				this.Session["HumanName"] = dataTable.Rows[0]["v_xm"].ToString();
			}
			this.Session["HumanCode"] = text;
			if (this._Action == "edit")
			{
				this.FileLink1.Type = 1;
				this.FileLink1.FID = this._Ic;
				this.ToInitialize();
				return;
			}
			if (this._Action == "add")
			{
				this.FileLink1.Type = 2;
				this.FileLink1.FID = this.TemGID.ToString();
				if (this._PrjCode != "" && this._PrjCode.Length == 36)
				{
					PMModel pMModel = new PMModel();
					pMModel = PMAction.GetSingleInfoByPrjGuid(this._PrjCode);
					if (pMModel != null)
					{
						this.txtaccountNum.Value = pMModel.PrjCode;
						this.txtacountName.Value = pMModel.PrjName;
						return;
					}
				}
			}
			else
			{
				this.FileLink1.Type = 0;
				this.FileLink1.FID = this._Ic;
				this.ToInitialize();
				this.initSate();
			}
		}
	}
	private void ToInitialize()
	{
		if (this._Ic != "" && this._Ic.Length == 36)
		{
			AccounModel accounModel = new AccounModel();
			AccountLogic accountLogic = new AccountLogic();
			accounModel = accountLogic.GetModel(this._ic);
			if (accounModel != null)
			{
				this.txtaccountNum.Value = accounModel.accountNum;
				this.txtacountName.Value = accounModel.acountName;
				string text = "";
				string[] array = accounModel.authorizer.ToString().Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (text2 != "")
					{
						text = text + "," + text2;
					}
				}
				string userName = this.am.GetUserName(text);
				this.Session["HumanCode"] = text;
				this.Session["HumanName"] = userName;
				this.txtauthorizer.Value = userName;
				this.txtRemark.Value = accounModel.Remark;
				this.txtinitialFund.Value = accounModel.initialFund.ToString();
			}
		}
	}
	private AccounModel getModel()
	{
		AccounModel accounModel = new AccounModel();
		if (this._Action == "add")
		{
			accounModel.AccountID = new Guid(this.FileLink1.FID.ToString());
			accounModel.creatDate = new DateTime?(DateTime.Now);
			accounModel.createMan = this.Session["yhdm"].ToString();
			if (!string.IsNullOrEmpty(this._PrjCode))
			{
				accounModel.PrjGuid = this._PrjCode;
			}
		}
		else
		{
			if (this._Action == "edit")
			{
				accounModel.AccountID = new Guid(this._Ic);
			}
		}
		accounModel.accountNum = this.txtaccountNum.Value;
		accounModel.acountName = this.txtacountName.Value;
		string text = "";
		string[] array = this.Session["HumanCode"].ToString().Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			if (text2 != "")
			{
				text = text + "," + text2;
			}
		}
		accounModel.authorizer = text;
		accounModel.Remark = this.txtRemark.Value;
		if (this.txtinitialFund.Value != "")
		{
			accounModel.initialFund = new decimal?(decimal.Parse(this.txtinitialFund.Value));
		}
		else
		{
			accounModel.initialFund = new decimal?(0m);
		}
		return accounModel;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		AccountLogic accountLogic = new AccountLogic();
		AccounModel model = new AccounModel();
		bool flag = false;
		if (this._Action == "add")
		{
			model = this.getModel();
			if (accountLogic.Add(model) > 0)
			{
				flag = true;
			}
		}
		else
		{
			if (this._Action == "edit")
			{
				model = this.getModel();
				flag = accountLogic.Update(model);
			}
		}
		if (flag)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("parent.desktop.flowclass.location = '/fund/Account/PrjAccount.aspx?prjId=" + base.Request["PrjCode"] + "';");
			stringBuilder.Append("alert('保存成功');");
			stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>" + stringBuilder.ToString() + "</script>");
			return;
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder2.Append("alert('保存失败');");
		base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>" + stringBuilder2.ToString() + "</script>");
	}
	private void initSate()
	{
		if (this._Ic != "" && this._Ic.Length == 36)
		{
			this.txtaccountNum.Disabled = true;
			this.txtRemark.Disabled = true;
			this.txtacountName.Disabled = true;
			this.txtinitialFund.Disabled = true;
			this.txtauthorizer.Disabled = true;
			this.btnSave.Enabled = false;
			this.btnSelectPerson.Disabled = true;
		}
	}
}


