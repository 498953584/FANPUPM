using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_FundPlanList : NBasePage, IRequiresSessionState
{
	private string contractId = string.Empty;
	private bool isExamineApprove;
	private IncometPaymentBll IncometPayment = new IncometPaymentBll();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.contractId = base.Request["ContractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["isExamineApprove"]))
		{
			if (base.Request["isExamineApprove"] == "1")
			{
				this.isExamineApprove = true;
			}
			else
			{
				this.isExamineApprove = false;
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.gvwFundPlan.DataSource = this.IncometPayment.GetFundPlan(this.contractId, this.isExamineApprove);
		this.gvwFundPlan.DataBind();
	}
	protected void gvwFundPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvwFundPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			string text2 = Convert.ToDateTime(this.gvwFundPlan.DataKeys[e.Row.RowIndex].Values[1].ToString()).ToString("yyyy年MM月");
			string text3 = Convert.ToDecimal(this.gvwFundPlan.DataKeys[e.Row.RowIndex].Values[2].ToString()).ToString("0.000");
			string text4 = Convert.ToDecimal(this.gvwFundPlan.DataKeys[e.Row.RowIndex].Values[3].ToString()).ToString("0.000");
			string text5 = Convert.ToDecimal(this.gvwFundPlan.DataKeys[e.Row.RowIndex].Values[4].ToString()).ToString("0.000");
			string text6 = this.gvwFundPlan.DataKeys[e.Row.RowIndex].Values[5].ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"setHd('",
				text,
				"','",
				text2,
				"','",
				text3,
				"','",
				text4,
				"','",
				text5,
				"','",
				text6,
				"');"
			});
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"save('",
				text,
				"','",
				text2,
				"','",
				text3,
				"','",
				text4,
				"','",
				text5,
				"','",
				text6,
				"');"
			});
		}
	}
}


