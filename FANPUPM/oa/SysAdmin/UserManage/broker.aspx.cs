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
			if (a == "modPriv")
			{
				value = "UpdateUserPriv.aspx?" + str;
				this.js.Text = "window.document.title = '修改用户权限'";
			}
			else
			{
				if (a == "add")
				{
					value = "AddUser.aspx?" + str;
					this.js.Text = "window.document.title = '增加用户'";
				}
				else
				{
					if (a == "mod")
					{
						value = "UpdateUser.aspx?" + str;
						this.js.Text = "window.document.title = '修改用户信息'";
					}
					else
					{
						if (a == "PrivMaintenance")
						{
							value = "SupermanPriv.aspx?" + str;
							this.js.Text = "window.document.title = '管理员权限维护'";
						}
						else
						{
							if (a == "deptMaintenance")
							{
								value = "ManagerDeptSet.aspx?" + str;
								this.js.Text = "window.document.title = '管理部门维护'";
							}
						}
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


