using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using com.jwsoft.pm.entpm;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_Completed_PrjCompletedView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["ic"]))
		{
			this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			ProjectInfo byId = ProjectInfo.GetById(base.Request["ic"].ToString());
			if (byId != null)
			{
				this.lblPrjCode.Text = byId.PrjCode;
				this.lblProjectName.Text = byId.PrjName;
				this.lblStartDate.Text = Common2.GetTime(byId.StartDate);
				this.lblEndDate.Text = Common2.GetTime(byId.EndDate);
				if (byId.PrjState == 10)
				{
					this.tr_completedDate.Visible = true;
					this.tr_completedNote.Visible = true;
					this.lblCompletedDate.Text = Common2.GetTime(PrjCompleted.GetCompletedDate(base.Request["ic"].ToString()));
					this.td_completedNote.InnerHtml = PrjCompleted.GetCompletedNote(base.Request["ic"].ToString());
				}
			}
		}
	}
}


