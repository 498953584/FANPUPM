using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class top : BasePage, IRequiresSessionState
	{
		public string strSkinPath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Session["SkinID"] = "4";
			HttpCookie httpCookie = base.Request.Cookies["LogName"];
			if (httpCookie != null)
			{
				if (httpCookie.Values["key1"] == null || httpCookie.Values["key1"].ToString() != "zyg")
				{
					this.JS.Text = "top.window.close();";
					this.JS.Text = "top.location.replace('/default.aspx');";
				}
			}
			else
			{
				this.JS.Text = "top.window.close();";
				this.JS.Text = "top.location.replace('/default.aspx');";
			}
			if (this.Session["SkinID"] == null)
			{
				this.Session["SkinID"] = "4";
			}
			else
			{
				this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
			}
			if (!base.IsPostBack && ConfigurationManager.AppSettings["PhoneNumber"] != null)
			{
				this.lblPhoneNumber.Text = ConfigurationManager.AppSettings["PhoneNumber"].ToString();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


