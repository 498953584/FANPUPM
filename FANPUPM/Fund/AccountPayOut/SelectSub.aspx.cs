using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayout_SelectSub : NBasePage, IRequiresSessionState
{
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	public string plantype
	{
		get
		{
			object obj = this.ViewState["plantype"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["plantype"] = value;
		}
	}
	public string prjName
	{
		get
		{
			object obj = this.ViewState["prjName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["prjName"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Request.QueryString["prjguid"].ToString();
			this.PrjGuid = new Guid(base.Request.QueryString["prjguid"].ToString());
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.PrjGuid.ToString());
			if (modelByPrjGuid != null)
			{
				this.prjName = modelByPrjGuid.PrjName;
			}
			this.gvBudget.PageSize = NBasePage.pagesize;
			this.setTitle();
			this.InitPage();
		}
	}
	public string PayOutp(string zf, string pp)
	{
		if (string.IsNullOrEmpty(pp))
		{
			return "0.00%";
		}
		double num = double.Parse(pp);
		double num2 = double.Parse(string.IsNullOrEmpty(zf) ? "0" : zf);
		return (num / num2).ToString("p");
	}
	public void setTitle()
	{
		this.hfPrjName.Value = this.prjName;
		this.lblTitle.Text = this.prjName + "  费用信息";
	}
	public void InitPage()
	{
		this.BindGv();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = base.Request.QueryString["prjguid"].ToString();
		this.hfldPurchaseChecked.Value = string.Empty;
		if (this.hfldPrjId.Value.ToString() != "")
		{
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.hfldPrjId.Value);
			if (modelByPrjGuid != null)
			{
				this.prjName = modelByPrjGuid.PrjName;
			}
			this.setTitle();
			string text = " FlowState=1 and Total is not null ";
			if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
			{
				text += string.Format(" and ProjectId = '{0}' ", this.hfldPrjId.Value);
			}
			if (!string.IsNullOrEmpty(this.txtSignDate.Text))
			{
				text += string.Format(" and InputDate >='{0}'", this.txtSignDate.Text);
			}
			if (!string.IsNullOrEmpty(this.txtEndDate.Text))
			{
				text += string.Format(" and InputDate <='{0}'", this.txtEndDate.Text);
			}
			if (!string.IsNullOrEmpty(this.txtmoneyPrj.Text.Trim()))
			{
				text += string.Format(" and Name like '%{0}%'", this.txtmoneyPrj.Text.Trim());
			}
			this.gvBudget.DataSource = OrganizationDiary.getDtByWhere(text);
		}
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes.Add("OnDblClick", "dbClickRow('" + this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString() + "')");
		}
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btnSerch_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
}


