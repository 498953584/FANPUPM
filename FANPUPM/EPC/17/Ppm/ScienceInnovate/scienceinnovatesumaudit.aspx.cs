using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScienceInnovateSumAudit : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["id"] == null)
				{
					this.js.Text = "alert('参数错误！');window.close();";
					return;
				}
				string sqlString = "select * from Prj_Summary where id=" + base.Request.Params["id"].ToString();
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					if (dataTable.Rows[0]["CheckDate"] != DBNull.Value && dataTable.Rows[0]["checker"] != DBNull.Value)
					{
						this.txtAuditPeople.Text = userManageDb.GetCurrentUserInfo(dataTable.Rows[0]["checker"].ToString()).UserName;
						this.txtAuditTime.Text = DateTime.Parse(dataTable.Rows[0]["CheckDate"].ToString()).ToShortDateString();
						this.txtRemark.Text = dataTable.Rows[0]["CheckComment"].ToString();
						this.chkResult.SelectedValue = dataTable.Rows[0]["CheckState"].ToString();
					}
					else
					{
						this.txtAuditPeople.Text = userManageDb.GetCurrentUserInfo().UserName;
						this.txtAuditTime.Text = DateTime.Today.ToShortDateString();
					}
				}
				dataTable.Dispose();
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
		protected void Button1_Click(object sender, EventArgs e)
		{
			string sqlString = string.Concat(new string[]
			{
				"update Prj_Summary set checker='",
				userManageDb.GetCurrentUserInfo().UserCode,
				"',CheckDate='",
				this.txtAuditTime.Text,
				"',CheckState=",
				this.chkResult.SelectedValue,
				",CheckComment='",
				this.txtRemark.Text,
				"' where id=",
				base.Request.Params["id"].ToString()
			});
			if (publicDbOpClass.NonQuerySqlString(sqlString))
			{
				this.js.Text = "alert('操作成功！');";
				return;
			}
			this.js.Text = "alert('操作失败！');";
		}
	}


