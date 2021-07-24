using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class DelRole : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string text = base.Request.QueryString["strRoleCode"].Trim();
			if (text.Length != 0)
			{
				roleManageDb roleManageDb = new roleManageDb();
				DataTable dataTable = roleManageDb.selAllUsers(text);
				if (dataTable.Rows.Count == 0)
				{
					if (roleManageDb.roleDel(text))
					{
						string text2 = "";
						text2 += "<script language=JavaScript>alert(\"删除成功！\");window.location='roleList.aspx';</script>";
						this.RegisterStartupScript("alert", text2);
						return;
					}
					string script = "<script language=JavaScript>alert(\"删除失败！\");window.location='roleList.aspx';</script>";
					this.RegisterStartupScript("alert", script);
					return;
				}
				else
				{
					if (dataTable.Rows.Count > 0)
					{
						string script2 = "<script language=JavaScript>alert(\"该角色被使用，不能删除！\");window.location='roleList.aspx';</script>";
						this.RegisterStartupScript("alert", script2);
						return;
					}
				}
			}
			else
			{
				string script3 = "<script language=JavaScript>alert(\"操作失败！\");window.location='roleList.aspx';</script>";
				this.RegisterStartupScript("alert", script3);
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


