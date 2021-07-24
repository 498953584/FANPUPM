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
				a = base.Request.QueryString["Op"].Trim();
			}
			catch
			{
				base.Response.End();
			}
			if (a == "add")
			{
				value = "AddRole.aspx?" + str;
				this.js.Text = "window.document.title = '添加角色'";
			}
			else
			{
				if (a == "edit")
				{
					value = "UpdateRole.aspx?" + str;
					this.js.Text = "window.document.title = '修改角色'";
				}
				else
				{
					if (a == "modPriv")
					{
						value = "RolePurv.aspx?" + str;
						this.js.Text = "window.document.title = '修改角色权限'";
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


