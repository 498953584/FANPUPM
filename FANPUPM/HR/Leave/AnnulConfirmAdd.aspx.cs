using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_AnnulConfirmAdd : BasePage, IRequiresSessionState
{
	private ApplicationAction aa
	{
		get
		{
			return new ApplicationAction();
		}
	}
	private Guid rid
	{
		get
		{
			return (Guid)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["rid"] != null)
			{
				this.rid = new Guid(base.Request["rid"]);
				this.GetPageData();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.aa.AnnulConfirmUpdate(this.getHRLeaveApplication()) == 1)
		{
			this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	protected HRLeaveApplication getHRLeaveApplication()
	{
		HRLeaveApplication hRLeaveApplication = new HRLeaveApplication();
		hRLeaveApplication.RecordID = this.rid;
		hRLeaveApplication.IsConfirm = "1";
		hRLeaveApplication.LeaveDays = Convert.ToDecimal(this.txtLeaveDays.Text);
		userManageDb userManageDb = new userManageDb();
		hRLeaveApplication.ConfirmUser = userManageDb.GetUserName(this.Session["yhdm"].ToString());
		return hRLeaveApplication;
	}
	protected void GetPageData()
	{
		HRLeaveApplication model = this.aa.GetModel(this.rid);
		this.DDLLeaveType.SelectedValue = model.LeaveType.ToString();
		this.DBBeginDate.Text = model.BeginDate.ToString("yyyy-MM-dd");
		this.txtDays.Text = model.Days.ToString();
		this.txtReason.Text = model.Reason.ToString();
		this.txtLeaveDays.Text = model.LeaveDays.ToString();
	}
}


