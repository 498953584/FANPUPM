using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class workLogAdd : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.logContent.Attributes["onkeyup"] = "javascript:textCounter('999999999');";
				DateTime today = DateTime.Today;
				string str = today.Date.ToString("d");
				workLogDb workLogDb = new workLogDb();
				string str2 = workLogDb.chineseWeek(today.DayOfWeek.ToString());
				this.dateLbl.Text = str + " " + str2;
				string yhdm = this.Page.Session["yhdm"].ToString();
				DataTable dataTable = new DataTable();
				workLogDb workLogDb2 = new workLogDb();
				bool flag = workLogDb2.logIsExist(yhdm, "pt_gzrz_nr");
				if (flag)
				{
					this.js.Text = "var flag = confirm('已存在当天日志！是否修改日志？'); if (flag) {window.location='workLogUpdate.aspx?id=0';} else {window.location='workLogPreview.aspx?id=0';};";
				}
				dataTable = workLogDb2.getTempTable(yhdm);
				if (dataTable.Rows.Count > 0)
				{
					if (dataTable.Rows[0]["c_tq"].ToString().Length > 0)
					{
						this.weather.Text = dataTable.Rows[0]["c_tq"].ToString();
					}
					if (dataTable.Rows[0]["txt_rznr"].ToString().Length > 0)
					{
						this.logContent.Text = dataTable.Rows[0]["txt_rznr"].ToString();
						this.Page.RegisterStartupScript("default", "<script language='javascript'> textCounter('" + dataTable.Rows[0]["txt_rznr"].ToString() + "');</script>");
					}
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
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			float num = Convert.ToSingle(this.Page.Request.Form["numLbl"].ToString());
			if ((double)num < 50.0)
			{
				this.js.Text = "alert('不 能 少 于 50 个 汉 字 或 100 个 英 文 字 母 ！');";
				return;
			}
			this.btnAdd.Enabled = false;
			workLogDb workLogDb = new workLogDb();
			string text = this.weather.Text;
			string text2 = this.logContent.Text;
			string yhdm = this.Page.Session["yhdm"].ToString();
			bool flag = workLogDb.workLogAdd(text, text2, yhdm);
			if (flag)
			{
				this.js.Text = "window.location='workLogPreview.aspx?id=0';";
				return;
			}
			this.js.Text = "alert('日志未保存！');";
			this.btnAdd.Enabled = true;
		}
		protected void btnAddTemp_Click(object sender, EventArgs e)
		{
			workLogDb workLogDb = new workLogDb();
			string text = this.weather.Text;
			string text2 = this.logContent.Text;
			string yhdm = this.Page.Session["yhdm"].ToString();
			bool flag = workLogDb.logIsExist(yhdm, "pt_gzrz_temp");
			bool flag2;
			if (flag)
			{
				flag2 = workLogDb.workLogTempUpdate(text, text2, yhdm);
			}
			else
			{
				flag2 = workLogDb.workLogAddTemp(text, text2, yhdm);
			}
			if (flag2)
			{
				this.js.Text = "window.location = 'workLogList.aspx';";
				return;
			}
			this.js.Text = "alert ('本次日志暂存未成功！');";
		}
	}


