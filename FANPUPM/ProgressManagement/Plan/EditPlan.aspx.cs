using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Plan_EditPlan : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string action = string.Empty;
	private string planId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"];
		}
		if (!string.IsNullOrEmpty(base.Request["planId"]))
		{
			this.planId = base.Request["planId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && this.action == "edit")
		{
			DataTable partById = Progress.GetPartById(this.planId);
			if (partById.Rows.Count > 0)
			{
				DataRow dataRow = partById.Rows[0];
				this.txtProgressName.Text = dataRow["VersionName"].ToString();
				this.txtVersionCode.Text = dataRow["VersionCode"].ToString();
				this.txtArea.Text = dataRow["Note"].ToString();
				this.chkIsMainPlan.Checked = System.Convert.ToBoolean(dataRow["IsMain"]);
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = this.txtProgressName.Text.Trim();
		string note = this.txtArea.Text.Trim();
		string versionCode = this.txtVersionCode.Text.Trim();
		bool @checked = this.chkIsMainPlan.Checked;
		if (this.action == "add")
		{
			Progress progress = Progress.Creat(System.Guid.NewGuid().ToString(), new System.Guid(this.prjId), text, versionCode, @checked, base.UserCode, System.DateTime.Now, note);
			progress.Add(progress);
		}
		else
		{
			if (this.action == "edit")
			{
				Progress.Update(this.planId, text, versionCode, @checked, note);
			}
		}
		base.RegisterScript("top.ui.winSuccess({ parentName: '_editplan' }); ");
	}
}


