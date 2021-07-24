using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame2_Desktop : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();
	private string userCode = string.Empty;
	protected string timeStr = DateTime.Now.ToString();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ValidateMemcached();
			this.InitPage();
		}
	}
	private void InitPage()
	{
		this.userCode = this.Session["yhdm"].ToString();
		this.hfldUserCode.Value = this.userCode;
		this.InitMenu();
		PTYhmcService pTYhmcService = new PTYhmcService();
		this.lblCurrentUser.Text = pTYhmcService.GetById(this.userCode).v_xm;
		if (ConfigurationManager.AppSettings["PhoneNumber"] != null)
		{
			this.lblPhoneNumber.Text = ConfigurationManager.AppSettings["PhoneNumber"].ToString();
		}
		if (base.Application["UserCodeCollection"] != null)
		{
			this.lblOnlineNum.Text = ((DataTable)base.Application["UserCodeCollection"]).Rows.Count.ToString() + " ";
		}
		this.lblVersion.Text = ConfigurationManager.AppSettings["HomePageTitle"].ToString();
		HttpCookie cookie = new HttpCookie("UserCode", base.UserCode);
		base.Response.Cookies.Add(cookie);
	}
	private void InitMenu()
	{
		string text = string.Empty;
		if (this.userCode == "00000000")
		{
			object obj = CacheHelper.Get("MENU00000000");
			if (obj == null)
			{
				text = this.GetMenuJson();
				CacheHelper.Set("MENU00000000", text);
			}
			else
			{
				text = obj.ToString();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetMenuJson();
			}
		}
		else
		{
			text = this.GetMenuJson();
		}
		this.hfldDepEmployeeJson.Value = text;
	}
	private string GetMenuJson()
	{
		PTMKService pTMKService = new PTMKService();
		PTMK[] t = pTMKService.GetFirstMenu(this.userCode).ToArray<PTMK>();
		return JsonHelper.JsonSerializer<PTMK[]>(t);
	}
	private void InitChildMenu(PTMK[] ptmkArr)
	{
		PTMKService pTMKService = new PTMKService();
		Dictionary<int, string> obj = new Dictionary<int, string>();
		int num = 0;
		if (num >= ptmkArr.Length)
		{
			this.hfldChildMk.Value = JsonNetWrap.SerializeObject(obj);
		}
		else
		{
			PTMK pTMK = ptmkArr[num];
			IList<PTMK> all = pTMKService.GetAll(this.userCode, pTMK.C_MKDM);
			string value = JsonHelper.JsonSerializer<PTMK[]>(all.ToArray<PTMK>());
			this.hfldChildMk.Value = value;
		}
	}
	protected void btn_exit_Click(object sender, EventArgs e)
	{
		try
		{
			base.Application.Lock();
			DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
			string iP = this.GetIP();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				if (dataRow["userCode"].ToString() == base.UserCode && dataRow["ip"].ToString() == iP)
				{
					dataTable.Rows.Remove(dataRow);
				}
			}
			base.Application["UserCodeCollection"] = dataTable;
			base.Application.UnLock();
		}
		finally
		{
			base.RegisterScript("window.location.href = '../';");
		}
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
	private void ValidateMemcached()
	{
		CacheHelper.Set("ValidateMemcached", "ValidateMemcached");
		if (!CacheHelper.Exists("ValidateMemcached"))
		{
			base.RegisterScript("_show('请联系管理员配置Memcached。')");
		}
	}
}


