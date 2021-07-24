using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WaitingJob : BasePage, IRequiresSessionState
	{
		protected DataRow _InfoRow;
		protected string _mkdm = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["mkdm"] != null)
			{
				this._mkdm = base.Request.QueryString["mkdm"];
				this._InfoRow = this.GetInfoRow();
				if (!base.IsPostBack)
				{
					this.thePageBind();
				}
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
		private DataRow GetInfoRow()
		{
			DataTable agentWorkSetting = com.jwsoft.pm.entpm.action.WaitingJob.GetAgentWorkSetting(this._mkdm);
			if (agentWorkSetting.Rows.Count > 0)
			{
				return agentWorkSetting.Rows[0];
			}
			return null;
		}
		private void thePageBind()
		{
			if (this._InfoRow != null)
			{
				this.tbx_memo.Text = this._InfoRow["memo"].ToString();
				this.tbx_mkdm.Text = this._InfoRow["mkdm"].ToString();
				this.tbx_sql.Text = this._InfoRow["sql"].ToString();
				this.tbx_title.Text = this._InfoRow["title"].ToString();
				this.tbx_url.Text = this._InfoRow["url"].ToString();
				bool flag = (bool)this._InfoRow["Isvalid"];
				if (flag)
				{
					this.rdi_Y.Checked = true;
					this.rdi_N.Checked = false;
				}
				else
				{
					this.rdi_Y.Checked = false;
					this.rdi_N.Checked = true;
				}
				this.btn_Delete.Visible = true;
				return;
			}
			DataTable oneModule = SystemModule.GetOneModule(this._mkdm);
			DataRow dataRow = oneModule.Rows[0];
			this.tbx_memo.Text = com.jwsoft.pm.entpm.action.WaitingJob.GetFullModuleName(this._mkdm);
			this.tbx_mkdm.Text = this._mkdm;
			this.tbx_sql.Text = "";
			this.tbx_title.Text = dataRow["v_mkmc"].ToString();
			this.tbx_url.Text = dataRow["v_cdlj"].ToString();
			this.rdi_Y.Checked = true;
			this.rdi_N.Checked = false;
			this.btn_Delete.Visible = false;
		}
		protected void btn_Save_Click(object sender, EventArgs e)
		{
			string value = this.tbx_title.Text.Trim();
			string value2 = this.tbx_url.Text.Trim();
			string value3 = this.tbx_sql.Text.Trim();
			string value4 = this.tbx_memo.Text.Trim();
			bool @checked = this.rdi_Y.Checked;
			SqlParameter[] myparms = new SqlParameter[]
			{
				new SqlParameter("@title", value),
				new SqlParameter("@url", value2),
				new SqlParameter("@sql", value3),
				new SqlParameter("@memo", value4),
				new SqlParameter("@isvalid", @checked)
			};
			com.jwsoft.pm.entpm.action.WaitingJob waitingJob = new com.jwsoft.pm.entpm.action.WaitingJob(this._mkdm);
			try
			{
				if (this._InfoRow == null)
				{
					waitingJob.Insert(myparms);
					this.btn_Delete.Visible = true;
				}
				else
				{
					waitingJob.Update(myparms);
				}
				this.JavaScriptControl1.Text = "alert('保存成功！');";
			}
			catch (Exception ex)
			{
				this.JavaScriptControl1.Text = string.Format("alert(\"{0}\")", ex.Message.Replace("\r\n", "\\r\\n"));
			}
		}
		protected void btn_Delete_Click(object sender, EventArgs e)
		{
			if (this._InfoRow == null)
			{
				return;
			}
			com.jwsoft.pm.entpm.action.WaitingJob waitingJob = new com.jwsoft.pm.entpm.action.WaitingJob(this._mkdm);
			try
			{
				waitingJob.Delete();
				this.JavaScriptControl1.Text = "alert('删除成功！');";
				this.btn_Delete.Visible = false;
			}
			catch (Exception ex)
			{
				this.JavaScriptControl1.Text = string.Format("alert(\"{0}\");", ex.Message.Replace("\r\n", "\\r\\n"));
			}
		}
	}


