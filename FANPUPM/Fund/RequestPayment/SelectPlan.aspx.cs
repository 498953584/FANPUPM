using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_RequestPayment_SelectPlan : NBasePage, IRequiresSessionState
{
	private string prjGuid;
	private string type;
	private Fund_Plan_MonthMainAction PlanAction = new Fund_Plan_MonthMainAction();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["prjguid"] != null && base.Request.QueryString["prjguid"].ToString() != "")
		{
			this.prjGuid = base.Request.QueryString["prjguid"].ToString();
			this.type = base.Request.QueryString["Type"].ToString();
			this.gvwDataBind();
		}
	}
	private void gvwDataBind()
	{
		DataTable selectPlan = this.PlanAction.getSelectPlan(this.prjGuid, this.type);
		if (selectPlan.Rows.Count <= 0)
		{
			this.btnSave.Disabled = true;
		}
		this.gvwWebLineList.DataSource = selectPlan;
		this.gvwWebLineList.DataBind();
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Cells[7].Text = (Convert.ToDecimal(e.Row.Cells[5].Text.ToString()) - Convert.ToDecimal(e.Row.Cells[6].Text.ToString())).ToString();
			if (this.type == "payout")
			{
				e.Row.Cells[3].Attributes.Add("style", "display:none");
			}
			else
			{
				e.Row.Cells[2].Attributes.Add("style", "display:none");
			}
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			if (this.type == "payout")
			{
				e.Row.Cells[3].Attributes.Add("style", "display:none");
				return;
			}
			e.Row.Cells[2].Attributes.Add("style", "display:none");
		}
	}
}


