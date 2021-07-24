using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.Fund.PrjLoad;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.stockBLL.Fund.PrjLoad;
using com.jwsoft.pm.data;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjLoanEdit : NBasePage, IRequiresSessionState
{
	private string temLoanID = string.Empty;
	private string temAction = string.Empty;
	private string temAccountid = string.Empty;
	private PrjLoadModel model;
	private PrjLoadLogic PLBLL = new PrjLoadLogic();
	private AccountLogic bll = new AccountLogic();

	protected string TemLoanID
	{
		get
		{
			object obj = this.ViewState["temLoanID"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["temLoanID"] = value;
		}
	}
	protected string TemAction
	{
		get
		{
			object obj = this.ViewState["temAction"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["temAction"] = value;
		}
	}
	protected string TemAccountid
	{
		get
		{
			object obj = this.ViewState["temAccountid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["temAccountid"] = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["accountid"] != null && base.Request.QueryString["accountid"].ToString() != "")
		{
			this.TemAccountid = base.Request.QueryString["accountid"].ToString();
		}
		if (base.Request.QueryString["Action"] != null && base.Request.QueryString["Action"].ToString() != "")
		{
			this.TemAction = base.Request.QueryString["Action"].ToString();
		}
		if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"].ToString() != "")
		{
			this.TemLoanID = base.Request.QueryString["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			"'ad'fasdfa's'".Replace("'", "");
			this.FileUpload1.Class = "PrjLoan";
			this.bindPage();
			if (this.TemAction == "add")
			{
				this.ViewState["RecordCode"] = Guid.NewGuid().ToString();
				this.FileUpload1.RecordCode = this.ViewState["RecordCode"].ToString();
				string userNameByUserCode = this.bll.GetUserNameByUserCode(this.Session["yhdm"].ToString());
				this.labLoanMan.Text = userNameByUserCode;
				this.txtLoanCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				return;
			}
			if (this.TemAction == "edit" && !string.IsNullOrEmpty(this.TemLoanID))
			{
				this.FileUpload1.RecordCode = this.TemLoanID;
				this.initData(this.TemLoanID);
			}
		}
	}
	protected void initData(string _LoanID)
	{
		this.model = new PrjLoadModel();
		this.model = this.PLBLL.GetModel(new Guid(_LoanID));
		if (this.model != null)
		{
			this.txaRemark.Value = this.model.Remark;
			this.txtLoanDate.Text = (this.model.LoanDate.HasValue ? DateTime.Parse(this.model.LoanDate.ToString()).ToShortDateString() : "");
			this.txtLoanFund.Value = this.model.LoanFund.ToString();
			this.txtLoanRate.Value = string.Format("{0:0.00}", decimal.Parse(this.model.LoanRate.ToString()) * 100m);
			this.txtPlanReturnDate.Text = (this.model.PlanReturnDate.HasValue ? DateTime.Parse(this.model.PlanReturnDate.ToString()).ToShortDateString() : "");
			if (!string.IsNullOrEmpty(this.model.LoanCode))
			{
				this.txtLoanCode.Text = this.model.LoanCode;
			}
			else
			{
				this.txtLoanCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
			}
			this.txtLoanCause.Value = this.model.LoanCause;
			this.labLoanMan.Text = this.model.LoanmanName;
			this.hdnLoanMan.Value = this.model.LoanMan;
			ListItem listItem = this.dropPrjCodeList.Items.FindByValue(this.model.PrjGuid.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
			}
		}
	}
	protected void bindPage()
	{
		if (!string.IsNullOrEmpty(this.TemAccountid))
		{
			AccounModel accounModel = new AccounModel();
			accounModel = this.bll.GetModel(this.TemAccountid);
			if (accounModel != null)
			{
				string[] array = accounModel.PrjGuid.ToString().Split(new char[]
				{
					','
				});
				new ArrayList();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("id", typeof(string));
				dataTable.Columns.Add("name", typeof(string));
				for (int i = 0; i < array.Length; i++)
				{
					string str = array[i].ToString().Replace("'", "");
					string sqlString = "select * from pt_prjinfo where isvalid=1 and PrjGuid='" + str + "' ";
					DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString);
					if (dataTable2 != null && dataTable2.Rows.Count > 0)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["id"] = dataTable2.Rows[0]["PrjGuid"];
						dataRow["name"] = dataTable2.Rows[0]["PrjName"];
						dataTable.Rows.Add(dataRow);
					}
				}
				this.dropPrjCodeList.DataTextField = "name";
				this.dropPrjCodeList.DataValueField = "id";
				this.dropPrjCodeList.DataSource = dataTable;
				this.dropPrjCodeList.DataBind();
			}
		}
	}
	private PrjLoadModel GetPageModel()
	{
		this.model = new PrjLoadModel();
		if (this.TemAction == "add")
		{
			this.model.LoanID = new Guid(this.ViewState["RecordCode"].ToString());
			this.model.FlowState = new int?(-1);
		}
		else
		{
			this.model.LoanID = new Guid(this.TemLoanID);
		}
		if (!string.IsNullOrEmpty(this.TemAccountid))
		{
			this.model.LoanCode = this.txtLoanCode.Text.ToString();
			this.model.LoanCause = this.txtLoanCause.Value.ToString();
			if (!string.IsNullOrEmpty(this.txtLoanDate.Text.ToString()))
			{
				this.model.LoanDate = new DateTime?(DateTime.Parse(this.txtLoanDate.Text.ToString()));
			}
			else
			{
				this.model.LoanDate = null;
			}
			if (!string.IsNullOrEmpty(this.txtLoanFund.Value.ToString()))
			{
				this.model.LoanFund = new decimal?(decimal.Parse(this.txtLoanFund.Value.ToString()));
			}
			else
			{
				this.model.LoanFund = new decimal?(0m);
			}
			if (!string.IsNullOrEmpty(this.hdnLoanMan.Value.ToString()))
			{
				this.model.LoanMan = this.hdnLoanMan.Value.ToString();
			}
			else
			{
				this.model.LoanMan = this.Session["yhdm"].ToString();
			}
			if (!string.IsNullOrEmpty(this.txtPlanReturnDate.Text.ToString()))
			{
				this.model.PlanReturnDate = new DateTime?(DateTime.Parse(this.txtPlanReturnDate.Text.Trim().ToString()));
			}
			else
			{
				this.model.PlanReturnDate = null;
			}
			if (!string.IsNullOrEmpty(this.txtLoanRate.Value.ToString()))
			{
				this.model.LoanRate = new decimal?(decimal.Parse(this.txtLoanRate.Value.ToString()) / 100m);
			}
			else
			{
				this.model.LoanRate = new decimal?(0m);
			}
			if (!string.IsNullOrEmpty(this.txaRemark.Value.ToString()))
			{
				this.model.Remark = this.txaRemark.Value.ToString();
			}
			else
			{
				this.model.Remark = "";
			}
			if (!string.IsNullOrEmpty(this.dropPrjCodeList.SelectedValue))
			{
				this.model.PrjGuid = new Guid(this.dropPrjCodeList.SelectedValue);
			}
		}
		this.model.ReturnState = new int?(0);
		return this.model;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.model = this.GetPageModel();
		if (this.TemAction == "add")
		{
			if (this.PLBLL.Add(this.model))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_prjloan' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('保存失败');");
			}
		}
		else
		{
			if (this.TemAction == "edit")
			{
				if (this.PLBLL.Update(this.model))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_prjloan' });");
				}
				else
				{
					base.RegisterScript("top.ui.alert('保存失败');");
				}
			}
		}
		base.ClientScript.RegisterClientScriptBlock(base.GetType(), "sc", "<script>" + stringBuilder.ToString() + "</script>");
	}
}


