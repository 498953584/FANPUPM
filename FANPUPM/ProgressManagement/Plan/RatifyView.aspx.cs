﻿using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Plan_RatifyView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
			this.BindPlans();
		}
	}
	protected void BindPlans()
	{
		string progressId = base.Request["ic"];
		DataTable plan = Progress.GetPlan(progressId);
		this.gvwPlans.DataSource = plan;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressId"].ToString();
		}
	}
}


