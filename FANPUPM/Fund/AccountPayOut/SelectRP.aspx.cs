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
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayout_SelectRP : NBasePage, IRequiresSessionState
{
	private RequestPayDetail detatilBLL = new RequestPayDetail();
	private RequestPayMain mailBLL = new RequestPayMain();
	private PayoutPayment payoutPayment = new PayoutPayment();
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
			return;
		}
		List<PayoutPaymentModel> list = (List<PayoutPaymentModel>)this.ViewState["PayoutPaymentModel"];
		if (!string.IsNullOrEmpty(this.prjcode.Text))
		{
			list = (
				from model in list
				where model.PaymentCode.LastIndexOf(this.prjcode.Text.Trim()) >= 0
				select model).ToList<PayoutPaymentModel>();
		}
		if (!string.IsNullOrEmpty(this.zfstart.Text))
		{
			list = (
				from model in list
				where this.GetSumPayOutMoneyByPayoutPaymentID(model.ID) / (double)model.PaymentMoney.Value >= double.Parse(this.zfstart.Text) / 100.0
				select model).ToList<PayoutPaymentModel>();
		}
		if (!string.IsNullOrEmpty(this.zfend.Text))
		{
			list = (
				from model in list
				where this.GetSumPayOutMoneyByPayoutPaymentID(model.ID) / (double)model.PaymentMoney.Value <= double.Parse(this.zfend.Text) / 100.0
				select model).ToList<PayoutPaymentModel>();
		}
		this.gvBudget.DataSource = list;
		this.gvBudget.DataBind();
	}
	private double GetSumPayOutMoneyByPayoutPaymentID(string id)
	{
		string cmdText = string.Format("select isnull(sum(PayOutMoney),0.00) \r\n\t                                    from Fund_Prj_Accoun_Payout \r\n\t                                    where Fund_Prj_Accoun_Payout.RpGuid='{0}'  \r\n\t                                    and Fund_Prj_Accoun_Payout.floestate=1", id);
		object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null);
		return DBHelper.GetDouble(obj);
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
		this.lblTitle.Text = this.prjName + "  合同支付";
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
			string text = " Con_Payout_Payment.FlowState=1 ";
			if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
			{
				text += string.Format(" and Con_Payout_Contract.PrjGuid = '{0}' ", this.hfldPrjId.Value);
			}
			List<PayoutPaymentModel> list = this.payoutPayment.GetList(text);
			this.ViewState["PayoutPaymentModel"] = list;
			this.gvBudget.DataSource = list;
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
			if (e.Row.Cells[8].Text.ToString().Length > 23)
			{
				e.Row.Cells[8].ToolTip = e.Row.Cells[8].Text.ToString();
				e.Row.Cells[8].Text = e.Row.Cells[8].Text.Substring(0, 22) + "...";
			}
		}
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


