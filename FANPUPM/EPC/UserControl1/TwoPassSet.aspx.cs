using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserManage_TwoPassSet : PageBase, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.Session["twopass"] = null;
			if (base.Request.QueryString["tt"].ToString() == "1")
			{
				this.labhead.Text = "输入超级删除密码";
			}
			else
			{
				this.labhead.Text = "设置超级删除密码";
			}
			this.ttText.Value = base.Request.QueryString["tt"].ToString();
		}
	}
	protected void Button1_ServerClick(object sender, EventArgs e)
	{
		string text = this.txtpass.Text.Trim().ToString();
		if (!(base.Request.QueryString["tt"].ToString() == "1"))
		{
			try
			{
				BasicConfigService basicConfigService = new BasicConfigService();
				BasicConfig byName = basicConfigService.GetByName("TheDeletePwd");
				byName.ParaValue = text;
				basicConfigService.Update(byName);
				string script = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttop.ui.show( '设置成功!');\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
				base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), script);
			}
			catch
			{
				string script2 = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttop.ui.show( '设置失败!');\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
				base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), script2);
			}
			return;
		}
		bool twoPWD = myxml.GetTwoPWD(text);
		BasicConfigService basicConfigService2 = new BasicConfigService();
		basicConfigService2.GetValue("TheDeletePwd");
		if (twoPWD)
		{
			this.Session["twopass"] = "IsAllowDel";
			base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), "<script>window.opener=null;window.close(this);</script>");
			return;
		}
		base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), "<script>alert('密码错误!');</script>");
	}
	private bool CheckPwd(string pwd)
	{
		string arg = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
		string sqlString = string.Format("SELECT COUNT(*) FROM PT_login WHERE V_YHDM = '{0}' AND AuditPwd = '{1}'; ", base.UserCode, arg);
		object obj = publicDbOpClass.ExecuteScalar(sqlString);
		return obj.ToString() == "1";
	}
}


