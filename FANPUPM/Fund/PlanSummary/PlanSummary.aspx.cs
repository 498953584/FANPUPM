using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.PlanSummary.BLL;
using cn.justwin.Fund.PlanSummary.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_PlanSummary_PlanSummary : NBasePage, IRequiresSessionState
{
	private PlanSummaryMainBLL MainBll = new PlanSummaryMainBLL();
	private PlanSummaryMain MainModel;
	private PlanSummaryDetailBLL detailBLL = new PlanSummaryDetailBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.HdnState.Value = base.Request["State"].ToString();
			if (base.Request["PlanType"].ToString() == "payout")
			{
				this.lblTitle.Text = "支出计划汇总";
				this.WF1.BusiCode = "104";
			}
			else
			{
				this.lblTitle.Text = "收入计划汇总";
				this.WF1.BusiCode = "105";
			}
			this.BindGv();
			this.HdnPlanType.Value = base.Request["PlanType"].ToString();
			this.hfldAdjunctPath.Value = ConfigurationManager.AppSettings["PlanSummary"];
		}
	}
	private void BindGv()
	{
		string str = base.Request.QueryString["PlanType"].ToString();
		DataTable list = this.MainBll.GetList(" ma.PlanType='" + str + "' ");
		this.gvwAccount.DataSource = list;
		this.gvwAccount.DataBind();
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.HdnAccountID.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.HdnAccountID.Value);
		}
		else
		{
			list.Add(this.HdnAccountID.Value);
		}
		try
		{
			foreach (string current in list)
			{
				this.MainBll.Delete(new Guid(current));
				this.detailBLL.DeleteByMain(current);
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
		this.BindGv();
	}
	protected void gvwAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


