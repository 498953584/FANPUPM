using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class workLogPreview : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				int num = Convert.ToInt32(this.Page.Request.QueryString["id"].ToString());
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
					this.btnBack.Visible = false;
					dataTable = workLogDb.getTable(yhdm, today);
				}
				if (dataTable.Rows.Count > 0)
				{
					this.weatherLbl.Text = dataTable.Rows[0]["c_tq"].ToString();
					this.logContent.Text = dataTable.Rows[0]["txt_rznr"].ToString();
					this.writerLbl.Text = dataTable.Rows[0]["v_xm"].ToString();
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
	}


