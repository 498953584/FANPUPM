using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using EeekSoft.Web;
using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class platform_czDesktop : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		this.lblVersion.Text = ConfigurationManager.AppSettings["HomePageTitle"].ToString();
		string newValue = DateTime.Now.Year.ToString();
		this.lblCopyright.Text = this.lblCopyright.Text.Replace("2011", newValue);
		HttpCookie cookie = new HttpCookie("UserCode", base.UserCode);
		base.Response.Cookies.Add(cookie);
	}
	public void exetimer()
	{
		MailManage mailManage = new MailManage();
		int newMailNumber = mailManage.GetNewMailNumber(this.Session["yhdm"].ToString());
		if (newMailNumber > 0)
		{
			this.PopupWin_Email.Visible = true;
			this.PopupWin_Email.AutoShow = true;
			this.PopupWin_Email.Message = "你有" + newMailNumber + "封新邮件!";
		}
	}
}


