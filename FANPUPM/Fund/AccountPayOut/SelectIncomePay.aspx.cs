using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Fund.RequestPayment.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayout_SelectRP : NBasePage, IRequiresSessionState
{
	private RequestPayDetail detatilBLL = new RequestPayDetail();
	private RequestPayMain mailBLL = new RequestPayMain();
	private IncometPaymentBll incomePay = new IncometPaymentBll();
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	public List<string> cinmoney = new List<string>();
	public int i;
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
	public string PayOutp(string yizhizf, string zf)
	{
		if (string.IsNullOrEmpty(zf))
		{
			return string.Empty;
		}
		if (string.IsNullOrEmpty(yizhizf))
		{
			return "0.00%";
		}
		double num = double.Parse(yizhizf);
		double num2 = double.Parse(zf);
		return (num / num2).ToString("p");
	}
	public void setTitle()
	{
		this.hfPrjName.Value = this.prjName;
		this.lblTitle.Text = this.prjName + "  合同收入";
	}
	public void InitPage()
	{
		this.BindGv();
	}
	public void BindGv()
	{
		this.hfldPrjId.Value = base.Request.QueryString["prjguid"].ToString();
		this.hfldPurchaseChecked.Value = string.Empty;
		StringBuilder stringBuilder = new StringBuilder();
		if (this.hfldPrjId.Value.ToString() != "")
		{
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.hfldPrjId.Value);
			if (modelByPrjGuid != null)
			{
				this.prjName = modelByPrjGuid.PrjName;
			}
			this.setTitle();
			string text = " where 1=1 ";
			if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
			{
				text += string.Format(" and Project = '{0}' ", this.hfldPrjId.Value);
			}
			stringBuilder.Append(" SELECT Con_Incomet_Payment.*, \r\n                            (\r\n                            SELECT ISNULL(sum(inmoney),0.00) \r\n                            FROM Fund_Prj_Accoun_Income \r\n                            WHERE Fund_Prj_Accoun_Income.FlowState='1' AND Con_Incomet_Payment.id=Fund_Prj_Accoun_Income.ContractID\r\n                            ) as Cinmoney,\r\n                            ContractName,UserCodes,\r\n                            Con_Incomet_Contract.Project   \r\n                            FROM Con_Incomet_Payment \r\n                            LEFT JOIN Con_Incomet_Contract ON Con_Incomet_Payment.ContractID=Con_Incomet_Contract.ContractID");
			stringBuilder.Append(text);
			stringBuilder.Append(" ORDER BY Con_Incomet_Payment.InputDate ");
			DataTable dataSource = SqlHelper.ExecuteQuery(CommandType.Text, stringBuilder.ToString(), null);
			this.gvBudget.DataSource = dataSource;
		}
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["code"] = this.gvBudget.DataKeys[e.Row.RowIndex]["CllectionCode"].ToString();
			if (e.Row.Cells[7].Text.ToString().Length > 23)
			{
				e.Row.Cells[7].ToolTip = e.Row.Cells[7].Text.ToString();
				e.Row.Cells[7].Text = e.Row.Cells[7].Text.Substring(0, 22) + "...";
			}
		}
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	public string GetContName(string ContID)
	{
		IncometContractBll incometContractBll = new IncometContractBll();
		IncometContractModel model = incometContractBll.GetModel(ContID);
		return model.ContractName;
	}
}


