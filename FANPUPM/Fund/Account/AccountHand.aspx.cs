using ASP;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.Fund.Account;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class Fund_Account_AccountHand : Page, IRequiresSessionState
{
	private accountMange am = new accountMange();
	private AccountLogic AL = new AccountLogic();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.Form["AccountID"] != null && base.Request.Form["AccountID"].ToString() != "")
			{
				string s = this.setSeesionval(base.Request.Form["AccountID"].ToString());
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.Write(s);
			}
			else
			{
				if (base.Request.Form["ENDAccountID"] != null)
				{
					string s2 = this.updateAuthorizer(base.Request.Form["ENDAccountID"].ToString());
					HttpContext.Current.Response.ContentType = "text/plain";
					HttpContext.Current.Response.Write(s2);
				}
			}
			HttpContext.Current.Response.End();
		}
	}
	private string updateAuthorizer(string accid)
	{
		string result = string.Empty;
		string text = "";
		string[] array = this.Session["HumanCode"].ToString().Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text2 = array2[i];
			if (text2 != "")
			{
				text = text + "," + text2;
			}
		}
		if (this.Session["HumanCodeOld"] != null)
		{
			if (this.Session["HumanCodeOld"].ToString() == this.Session["HumanCode"].ToString())
			{
				result = "";
			}
			else
			{
				if (this.AL.Update(text, accid))
				{
					result = "true";
				}
				else
				{
					result = "false";
				}
			}
		}
		return result;
	}
	private string setSeesionval(string accid)
	{
		this.Session["HumanCodeOld"] = "";
		AccounModel model = this.AL.GetModel(accid);
		if (model != null)
		{
			string text = "";
			string[] array = model.authorizer.ToString().Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				if (text2 != "")
				{
					text = text + "," + text2;
				}
			}
			string userName = this.am.GetUserName(text);
			this.Session["HumanCode"] = text;
			this.Session["HumanCodeOld"] = text;
			this.Session["HumanName"] = userName;
			return "true";
		}
		return "false";
	}
}


