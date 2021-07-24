using ASP;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Z_Demo_EMail : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	public void Send()
	{
		List<string> list = new List<string>();
		list.Add("bery.email@gmail.com");
		new MailUtility(ConfigHelper.SmtpId, list, "Send", DateTime.Now.ToString());
	}
	public void SendByThread()
	{
		List<string> list = new List<string>();
		list.Add("bery.email@gmail.com");
		Thread thread = new Thread(new ThreadStart(new MailUtility(ConfigHelper.SmtpId, list, "SendByThread", DateTime.Now.ToString())
		{
			AttachmentLst = new List<string>
			{
				"F:\\pm2_vs2010\\WebUtil\\ConfigHelper.cs"
			}
		}.Send));
		thread.Start();
	}
	public void SendAsync()
	{
		List<string> list = new List<string>();
		list.Add("bery.email@gmail.com");
		new MailUtility(ConfigHelper.SmtpId, list, "SendAsync", DateTime.Now.ToString());
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		this.Send();
	}
	protected void Button2_Click(object sender, EventArgs e)
	{
		this.SendByThread();
	}
	protected void Button3_Click(object sender, EventArgs e)
	{
		this.SendAsync();
	}
	private int SendSuccess()
	{
		Log4netHelper.Error(new Exception("SendSuccess"), "Mail", string.Empty);
		return 1;
	}
	private int SendFail()
	{
		Log4netHelper.Error(new Exception("SendFail"), "Mail", string.Empty);
		return 1;
	}
	private int SendSyncSuccess()
	{
		Log4netHelper.Error(new Exception("SendSyncSuccess"), "Mail", string.Empty);
		return 1;
	}
	private int SendSyncFail()
	{
		Log4netHelper.Error(new Exception("SendSyncFail"), "Mail", string.Empty);
		return 1;
	}
}


