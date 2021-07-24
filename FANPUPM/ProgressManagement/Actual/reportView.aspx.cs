using ASP;
using cn.justwin.BLL;
using cn.justwin.ProgressManagement;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Actual_reportView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initalize();
		}
	}
	protected void Initalize()
	{
		string reportId = base.Request["ic"];
		this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
		this.BindReport(reportId);
		this.BindReportDetails(reportId);
	}
	protected void BindReport(string reportId)
	{
		Report byId = Report.GetById(reportId);
		if (byId != null)
		{
			this.lblInputDate.Text = byId.InputDate.Value.ToString("yyyy-MM-dd");
			this.lblInputUser.Text = PageHelper.QueryUser(this, byId.InputUser);
			this.lblNote.Text = byId.Note;
			this.lblAdjunct.Text = StringUtility.FilesBind(byId.Id, "ProgressReport");
		}
	}
	protected void BindReportDetails(string reportId)
	{
		DataTable details = ReportDetail.GetDetails(reportId);
		this.gvwReportDetails.DataSource = details;
		this.gvwReportDetails.DataBind();
	}
}


