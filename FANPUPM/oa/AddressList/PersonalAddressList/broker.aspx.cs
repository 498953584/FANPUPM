using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class Broker : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string str = "";
			string value = "";
			string a = "";
			try
			{
				str = base.Request.QueryString.ToString();
				a = base.Request.QueryString["Op"].Trim().ToString();
			}
			catch
			{
				base.Response.End();
			}
			if (a == "Add")
			{
				value = "EditLinkman.aspx?" + str;
				this.js.Text = "window.document.title = '增加联系人'";
			}
			else
			{
				if (a == "Mod")
				{
					value = "EditLinkman.aspx?" + str;
					this.js.Text = "window.document.title = '修改联系人'";
				}
				else
				{
					if (a == "Browse")
					{
						value = "EditLinkman.aspx?" + str;
						this.js.Text = "window.document.title = '查看联系人'";
					}
				}
			}
			this.FraOperate.Attributes.Add("src", value);
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


