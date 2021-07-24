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
	public partial class workLogUpdate : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				int num = Convert.ToInt32(this.Page.Request.QueryString["id"].ToString());
				this.logContent.Attributes["onkeyup"] = "javascript:textCounter('999999999');";
				string yhdm = this.Page.Session["yhdm"].ToString();
				DataTable dataTable = new DataTable();
				DateTime today = DateTime.Today;
				workLogDb workLogDb = new workLogDb();
				if (num > 0)
				{
					dataTable = workLogDb.getTable(num);
				}
				else
				{
					dataTable = workLogDb.getTable(yhdm, today);
				}
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
					string str = workLogDb.chineseWeek(Convert.ToDateTime(dataTable.Rows[0]["dtm_zxsj"].ToString()).DayOfWeek.ToString());
					this.dateLbl.Text = Convert.ToDateTime(dataTable.Rows[0]["dtm_zxsj"].ToString()).ToString("d") + " " + str;
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
			int num = Convert.ToInt32(this.Page.Request.QueryString["id"].ToString());
			float num2 = Convert.ToSingle(this.Page.Request.Form["numLbl"].ToString());
			if ((double)num2 < 50.0)
			{
				this.js.Text = "alert('不 能 少 于 50 个 汉 字 或 100 个 英 文 字 母 ！');";
				return;
			}
			workLogDb workLogDb = new workLogDb();
			string text = this.weather.Text;
			string text2 = this.logContent.Text;
			string yhdm = this.Page.Session["yhdm"].ToString();
			bool flag;
			if (num > 0)
			{
				flag = workLogDb.workLogUpdate(text, text2, num);
			}
			else
			{
				flag = workLogDb.workLogUpdate(text, text2, yhdm);
			}
			if (flag)
			{
				this.js.Text = "window.location='workLogList.aspx';";
				return;
			}
			this.js.Text = "alert('日志更新失败！');";
		}
	}


