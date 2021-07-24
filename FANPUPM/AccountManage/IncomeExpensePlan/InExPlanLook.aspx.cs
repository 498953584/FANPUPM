using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_IncomeExpensePlan_InExPlanLook : NBasePage, IRequiresSessionState
{
	private InExPlanBLL InExPlan = new InExPlanBLL();
	private IEPInfoBLL IEPinfo = new IEPInfoBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind();
		}
	}
	public void Bind()
	{
		this.gvwInExPlan.DataSource = this.InExPlan.GetList(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.hdPrjCode.Value.Trim());
		this.gvwInExPlan.DataBind();
	}
	protected void gvwInExPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["ID"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["id"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["auditContent"] = "支出合同 ：" + this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString(),
				"','",
				this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[1].ToString(),
				"');"
			});
		}
	}
	protected void gvwInExPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwInExPlan.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
}


