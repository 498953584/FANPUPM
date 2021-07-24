using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Default3 : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitCookName();
		}
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		int num = Convert.ToInt32(base.Application["num"]);
		if (num == 0)
		{
			this.Useronline();
			return;
		}
		int count = ((DataTable)base.Application["UserCodeCollection"]).Rows.Count;
		if (count > num)
		{
			string script = string.Format("<script>jw.show('系统提示', '对不起，只允许登录{0}个用户。')</script>", num);
			base.ClientScript.RegisterClientScriptBlock(base.GetType(), "onlineNum", script);
			return;
		}
		this.Useronline();
	}
	private void InitCookName()
	{
		HttpCookie httpCookie = base.Request.Cookies["cbName"];
		HttpCookie httpCookie2 = base.Request.Cookies["cbPass"];
		if (httpCookie != null)
		{
			this.txtName.Value = HttpUtility.UrlDecode(httpCookie.Value);
		}
		if (httpCookie2 != null)
		{
			this.chkPwd.Checked = true;
			base.ClientScript.RegisterStartupScript(base.GetType(), Guid.NewGuid().ToString(), "<script>$('#txtPwd').val('justwin')</script>");
		}
	}
	private void Useronline()
	{
		string text = this.txtName.Value.Trim();
		string text2 = this.txtPwd.Value.Trim();
		string iP = this.GetIP();
		if (!this.IsRuleByText(text) || !this.IsRuleByText(text2))
		{
			string script = "<script>jw.alert('对不起！用户名或者密码输入错误，请重新输入!');</script>";
			base.ClientScript.RegisterStartupScript(base.GetType(), "IsRule", script);
			return;
		}
		HttpCookie httpCookie = base.Request.Cookies["cbPass"];
		if (httpCookie != null && text2 == "justwin")
		{
			text2 = EncryptionUtility.Decryption(httpCookie.Value);
		}
		object obj = userManageDb.ValidatorLoginAccess(text, text2);
		string text3 = (obj == null) ? string.Empty : obj.ToString();
		int num = this.IsReturnLogin(text3);
		if (string.IsNullOrEmpty(text3))
		{
			string script2 = "<script>jw.alert('系统提示', '对不起！用户名或者密码错误，请重新输入!');</script>";
			base.ClientScript.RegisterStartupScript(base.GetType(), "JavaScript", script2);
			return;
		}
		SysManageDb sysManageDb = new SysManageDb();
		this.Session["SysID"] = sysManageDb.GetDefault().ToString();
		this.Session["yhdm"] = text3;
		this.Session["yhdma"] = text3;
		this.Session["SkinID"] = userManageDb.getUserSkin(text3);
		this.Session["SkinID"] = "4";
		this.Session["PmSet"] = PmTypeAction.GetPmType(obj.ToString());
		this.Session["ptcs"] = myxml.Getcs(base.Server.MapPath("/ptcs.txt"));
		this.Session["pttest"] = "notest";
		if (this.Session["pttest"].ToString() != "notest" && (this.Session["ptcs"] == null || Convert.ToInt32(this.Session["ptcs"]) >= 200))
		{
			string str = "请找泛普软件联系。";
			string script3 = string.Format("<script>jw.alert('试用已到期；如需要继续使用,\\r\\n'+'" + str + "');</script>", new object[0]);
			base.ClientScript.RegisterStartupScript(base.GetType(), "Startup", script3);
			return;
		}
		this.Session["OpModules"] = userManageDb.GetUserOpModules(this.Session["yhdm"].ToString());
		this.Session["CorpCode"] = userManageDb.GetCorpCode(this.Session["yhdm"].ToString());
		HttpCookie httpCookie2 = new HttpCookie("LogName");
		httpCookie2.Values["key1"] = "zyg";
		httpCookie2.Expires = DateTime.Now.AddHours(2.0);
		base.Response.Cookies.Add(httpCookie2);
		loginLogDb loginLogDb = new loginLogDb();
		loginLogDb.UserlLoginInfo(iP, this.Session["yhdm"].ToString());
		if (num == 0)
		{
			this.AddUserInfo(this.Session["yhdm"].ToString(), false);
		}
		else
		{
			base.Application.Lock();
			DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
			foreach (DataRow dataRow in dataTable.Rows)
			{
				if (dataRow["userCode"].ToString() == text3)
				{
					dataRow["loginTime"] = DateTime.Now;
					dataRow["endTime"] = DateTime.Now;
					dataRow["ip"] = iP;
					break;
				}
			}
			base.Application["UserCodeCollection"] = dataTable;
			base.Application.UnLock();
		}
		FormsAuthentication.RedirectFromLoginPage(text3, false);
		AduitAction.AddToLog(text3, "登录系统", iP, text);
		this.LogUserInfo();
		base.Response.Redirect("./SysFrame2/Desktop.aspx");
	}
	private string GetIP()
	{
		string text = string.Empty;
		text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
		if (text == null || text == string.Empty)
		{
			text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
		}
		if (text == null || text == string.Empty)
		{
			text = HttpContext.Current.Request.UserHostAddress;
		}
		if (text == null || text == string.Empty)
		{
			return "0.0.0.0";
		}
		if (text == "::1")
		{
			text = "127.0.0.1";
		}
		return text;
	}
	private bool IsRuleByText(string strText)
	{
		return strText.IndexOf("'", 0) == -1 && strText.IndexOf("-", 0) == -1 && strText.IndexOf("/", 0) == -1;
	}
	private int IsReturnLogin(string userCode)
	{
		string iP = this.GetIP();
		if (base.Application["UserCodeCollection"] != null)
		{
			DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
			int i = 0;
			while (i < dataTable.Rows.Count)
			{
				if (dataTable.Rows[i]["userCode"].ToString() == userCode)
				{
					if (dataTable.Rows[i]["ip"].ToString() != iP)
					{
						return 2;
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
		}
		return 0;
	}
	private void AddUserInfo(string userCode, bool isReturn)
	{
		DataTable dataTable = this.GetUserTable();
		base.Application.Lock();
		if (isReturn)
		{
			if (base.Application["TempUserCodeCollection"] != null)
			{
				dataTable = (DataTable)base.Application["TempUserCodeCollection"];
			}
		}
		else
		{
			if (base.Application["UserCodeCollection"] != null)
			{
				dataTable = (DataTable)base.Application["UserCodeCollection"];
			}
		}
		DataRow dataRow = dataTable.NewRow();
		dataRow["userCode"] = userCode;
		dataRow["loginID"] = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, userCode);
		dataRow["loginTime"] = DateTime.Now;
		dataRow["endTime"] = DateTime.Now;
		dataRow["ip"] = this.GetIP();
		dataTable.Rows.Add(dataRow);
		if (isReturn)
		{
			base.Application["TempUserCodeCollection"] = dataTable;
		}
		else
		{
			base.Application["UserCodeCollection"] = dataTable;
		}
		base.Application.UnLock();
	}
	private DataTable GetUserTable()
	{
		return new DataTable
		{
			Columns =
			{

				{
					"userCode",
					typeof(string)
				},

				{
					"loginID",
					typeof(string)
				},

				{
					"loginTime",
					typeof(DateTime)
				},

				{
					"endTime",
					typeof(DateTime)
				},

				{
					"ip",
					typeof(string)
				}
			}
		};
	}
	private void LogUserInfo()
	{
		HttpCookie httpCookie = base.Request.Cookies["cbName"];
		if (httpCookie == null)
		{
			httpCookie = new HttpCookie("cbName");
		}
		httpCookie.Expires = DateTime.MaxValue;
		base.Response.Cookies.Set(httpCookie);
		HttpCookie httpCookie2 = base.Request.Cookies["cbPass"];
		if (httpCookie2 == null)
		{
			httpCookie2 = new HttpCookie("cbPass");
		}
		httpCookie2.Expires = DateTime.MaxValue;
		base.Response.Cookies.Set(httpCookie2);
		httpCookie.Value = HttpUtility.UrlEncode(this.txtName.Value.Trim());
		base.Response.Cookies.Add(httpCookie);
		if (this.chkPwd.Checked)
		{
			if (this.txtPwd.Value.Trim() != "justwin")
			{
				httpCookie2.Value = EncryptionUtility.Encryption(this.txtPwd.Value.Trim());
			}
			base.Response.Cookies.Add(httpCookie2);
			return;
		}
		httpCookie2.Expires = DateTime.Now;
		base.Response.Cookies.Set(httpCookie2);
	}
}


