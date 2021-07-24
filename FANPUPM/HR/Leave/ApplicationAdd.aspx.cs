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
public partial class HR_Leave_ApplicationAdd : BasePage, IRequiresSessionState
{
	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
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
			string arg_2B_0 = base.Request["rid"];
			if (!string.IsNullOrEmpty(base.Request["rid"]) || !string.IsNullOrEmpty(base.Request["t"]))
			{
				this.rid = new Guid(base.Request["rid"]);
				this.Type = base.Request["t"].ToString();
				if (this.Type != "Add")
				{
					this.GetPageData();
					return;
				}
				this.rid = Guid.NewGuid();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.txtDays.Text.Trim() == "0")
		{
			this.JS.Text = "top.ui.alert('请假天数不能为0!');";
			this.txtDays.Text = "";
			this.txtDays.Focus();
			return;
		}
		if (this.Type == "Add")
		{
			if (this.aa.Add(this.getHRLeaveApplication()) == 1)
			{
				this.JS.Text = "top.ui.show( '保存成功!'); winClose();";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败!');";
			return;
		}
		else
		{
			if (this.aa.Update(this.getHRLeaveApplication()) == 1)
			{
				this.JS.Text = "top.ui.show( '保存成功!'); winClose();";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败!');";
			return;
		}
	}
	protected HRLeaveApplication getHRLeaveApplication()
	{
		return new HRLeaveApplication
		{
			RecordID = this.rid,
			AuditState = -1,
			UserCode = this.Session["yhdm"].ToString(),
			RecordDate = DateTime.Now,
			LeaveType = Convert.ToInt32(this.DDLLeaveType.SelectedValue),
			BeginDate = Convert.ToDateTime(this.DBBeginDate.Text),
			Days = Convert.ToDecimal(this.txtDays.Text),
			Reason = this.txtReason.Text,
			IsApply = "0"
		};
	}
	protected void GetPageData()
	{
		HRLeaveApplication model = this.aa.GetModel(this.rid);
		this.DDLLeaveType.SelectedValue = model.LeaveType.ToString();
		this.DBBeginDate.Text = model.BeginDate.ToString("yyyy-MM-dd");
		this.txtDays.Text = model.Days.ToString();
		this.txtReason.Text = model.Reason.ToString();
	}
}


