using ASP;
using cn.justwin.BLL;
using cn.justwin.Popup;
using System;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame_PopupSet : NBasePage, IRequiresSessionState
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
		}
	}
    private void DataBindChk()
    {
        PopupSetting setting = new PopupSetting();
        EnumerableRowCollection<string> source = setting.GetSetting(base.UserCode).AsEnumerable().Where<DataRow>(m => m.Field<bool>("IsValid")).Select<DataRow, string>(m => m.Field<string>("Module"));
        if (source.Contains<string>(PopupParam.Bulletin))
        {
            this.chkBulletin.Checked = true;
        }
        if (source.Contains<string>(PopupParam.CompanyNews))
        {
            this.chkCompanyNews.Checked = true;
        }
        if (source.Contains<string>(PopupParam.Workflow))
        {
            this.chkWorkflow.Checked = true;
        }
        if (source.Contains<string>(PopupParam.Schedule))
        {
            this.chkSchedule.Checked = true;
        }
        if (source.Contains<string>(PopupParam.Mail))
        {
            this.chkMail.Checked = true;
        }
        this.chkWarning.Checked = source.Contains<string>(PopupParam.Warning);
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
			base.RegisterScript("alert('系统提示：\\n\\n修改成功。');");
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n修改失败。');");
		}
		base.RegisterScript("divClose(parent);");
	}
}


