using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Plan_SetDate : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			ProjectInfo byId = ProjectInfo.GetById(this.prjId);
			this.txtStartDate.Text = byId.StartDate.Value.ToString("yyyy-MM-dd");
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = this.txtStartDate.Text;
		string text2 = this.txtEndDate.Text;
		System.DateTime? startDate = null;
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(text))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(text));
		}
		if (!string.IsNullOrEmpty(text2))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(text2));
		}
		string text3 = base.Request["prjId"];
		ProjectInfo.UpdateProjectDate(text3, startDate, endDate);
		base.RegisterScript("top.ui.winSuccess({ parentName: '_plandetail' })");
	}
}


