using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.sysManage.common;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class dateSetting : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			new com.jwsoft.sysManage.common.Calendar(this.Page);
			if (!this.Page.IsPostBack)
			{
				workLogDb workLogDb = new workLogDb();
				DataTable dataTable = new DataTable();
				dataTable = workLogDb.getTable();
				string text = "";
				DateTime today = DateTime.Today;
				if (dataTable.Rows.Count > 0)
				{
					text += "从 ";
					if (dataTable.Rows[0]["dtm_kq"].ToString().Length > 0)
					{
						text += Convert.ToDateTime(dataTable.Rows[0]["dtm_kq"].ToString()).ToString("d");
						this.startTxb.Text = Convert.ToDateTime(dataTable.Rows[0]["dtm_kq"].ToString()).ToString("d");
					}
					text += "到 ";
					if (dataTable.Rows[0]["dtm_kqe"].ToString().Length > 0)
					{
						text += Convert.ToDateTime(dataTable.Rows[0]["dtm_kqe"].ToString()).ToString("d");
						this.endTxb.Text = Convert.ToDateTime(dataTable.Rows[0]["dtm_kqe"].ToString()).ToString("d");
					}
					else
					{
						text += today.ToString("d");
					}
					text += " 日";
				}
				else
				{
					text = "注意：未设置考勤日期，请先设置考勤日期！";
				}
				this.currentLbl.Text = text;
			}
			this.startTxb.Attributes["onclick"] = "opencalendar(this)";
			this.endTxb.Attributes["onclick"] = "opencalendar(this)";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			string endStr = "";
			string startStr = this.startTxb.Text.Trim();
			if (this.endTxb.Text.Length > 0)
			{
				endStr = this.endTxb.Text;
			}
			workLogDb workLogDb = new workLogDb();
			bool flag = workLogDb.dateAdd(startStr, endStr);
			if (flag)
			{
				this.js.Text = "alert ('设置成功！');window.location = 'workLogList.aspx';";
				return;
			}
			this.js.Text = "alert ('设置未成功！');";
		}
	}


