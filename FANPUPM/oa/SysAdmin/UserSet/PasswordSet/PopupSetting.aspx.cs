using ASP;
using cn.justwin.BLL;
using cn.justwin.Popup;
using cn.justwin.Web;
using System;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_PopupSetting : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblBulletin.Text = PopupParam.BulletinName;
			this.lblCompanyNews.Text = PopupParam.CompanyNewsName;
			this.lblWorkflow.Text = PopupParam.WorkflowName;
			this.lblSchedule.Text = PopupParam.ScheduleName;
			this.lblMail.Text = PopupParam.MailName;
			this.lblWarning.Text = PopupParam.WarningName;
			this.DataBindChk();
			if (ConfigHelper.ProjectType == "ZdContract")
			{
				this.tr_bulletin.Visible = false;
				this.tr_companyNews.Visible = false;
				this.tr_mail.Visible = false;
			}
		}
	}
	private void DataBindChk()
	{
        PopupSetting setting = new PopupSetting();
        EnumerableRowCollection<string> source = setting.GetSetting(base.UserCode).AsEnumerable().Where<DataRow>(m => m.Field<bool>("IsValid")).Select<DataRow, string>(m => m.Field<string>("Module"));
		if (source.Contains(PopupParam.Bulletin))
		{
			this.chkBulletin.Checked = true;
		}
		if (source.Contains(PopupParam.CompanyNews))
		{
			this.chkCompanyNews.Checked = true;
		}
		if (source.Contains(PopupParam.Workflow))
		{
			this.chkWorkflow.Checked = true;
		}
		if (source.Contains(PopupParam.Schedule))
		{
			this.chkSchedule.Checked = true;
		}
		if (source.Contains(PopupParam.Mail))
		{
			this.chkMail.Checked = true;
		}
		if (source.Contains(PopupParam.Warning))
		{
			this.chkWarning.Checked = true;
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			PopupSetting popupSetting = new PopupSetting();
			if (this.chkBulletin.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.Bulletin);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.Bulletin);
			}
			if (this.chkCompanyNews.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.CompanyNews);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.CompanyNews);
			}
			if (this.chkWorkflow.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.Workflow);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.Workflow);
			}
			if (this.chkSchedule.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.Schedule);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.Schedule);
			}
			if (this.chkMail.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.Mail);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.Mail);
			}
			if (this.chkWarning.Checked)
			{
				popupSetting.Subscribe(base.UserCode, PopupParam.Warning);
			}
			else
			{
				popupSetting.Cancel(base.UserCode, PopupParam.Warning);
			}
			base.RegisterScript("top.ui.alert('修改成功。');");
		}
		catch (Exception)
		{
			base.RegisterScript("top.ui.alert('修改失败。');");
		}
	}
}


