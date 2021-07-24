using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.sysManage.common;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LogList : NBasePage, IRequiresSessionState
	{
		private Common2 cm = new Common2();

		protected void Page_Load(object sender, EventArgs e)
		{
			new com.jwsoft.sysManage.common.Calendar(this.Page);
			this.Label1.Text = "系统登录日志列表";
			if (!this.Page.IsPostBack)
			{
				this.GetTable();
				this.tbDate.Text = DateTime.Now.ToString("d");
				this.btnDel.Attributes["onclick"] = "if(!confirm('真的要删除吗？'))return false;";
			}
		}
		private DataTable GetTable()
		{
			string text = " and 1=1 ";
			if (!string.IsNullOrEmpty(this.mindate.Text.Trim()))
			{
				text = text + " and dtm_dlsj >= '" + this.mindate.Text.Trim() + "' ";
			}
			if (!string.IsNullOrEmpty(this.maxdate.Text.Trim()))
			{
				text = text + " and dtm_dlsj <= '" + Convert.ToDateTime(this.maxdate.Text.Trim()).AddDays(1.0).ToString("yyyy-MM-dd") + "' ";
			}
			if (!string.IsNullOrEmpty(this.txtPeople.Value))
			{
				text = text + " and v_xm like '%" + this.txtPeople.Value + "%'";
			}
			DataTable dataTable = PrintLogin.PintLoginDT(text);
			this.dgLogList.DataSource = dataTable;
			this.dgLogList.DataBind();
			return dataTable;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgLogList.PageIndexChanged += new DataGridPageChangedEventHandler(this.dgLogList_PageIndexChanged);
		}
		private void dgLogList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgLogList.CurrentPageIndex = e.NewPageIndex;
			try
			{
				this.dgLogList.DataSource = this.GetTable();
				this.dgLogList.DataBind();
			}
			catch (Exception)
			{
				this.dgLogList.DataSource = this.GetTable();
				this.dgLogList.DataBind();
				this.dgLogList.CurrentPageIndex = 0;
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			string text = this.tbDate.Text;
			loginLogDb loginLogDb = new loginLogDb();
			bool flag = loginLogDb.delLoginInfo(text);
			if (flag)
			{
				DataTable dataSource = loginLogDb.loginLogQuery();
				this.dgLogList.DataSource = dataSource;
				this.dgLogList.CurrentPageIndex = 0;
				this.dgLogList.DataBind();
				return;
			}
			base.RegisterScript("top.ui.alert('删除失败')");
		}
		protected void BtnQuery_Click(object sender, EventArgs e)
		{
			this.GetTable();
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			DataTable table = this.GetTable();
			this.cm.ExportToExecelAll(this.GetTitleByTable(table), this.Label1.Text + ".xls", base.Request.Browser.Browser);
		}
		public DataTable GetTitleByTable(DataTable dt)
		{
			dt.Columns.Remove("V_YHDM");
			dt.Columns["i_rzid"].ColumnName = "日志序号";
			dt.Columns["v_xm"].ColumnName = "登录人姓名";
			dt.Columns["v_dlip"].ColumnName = "登录IP";
			dt.Columns["dtm_dlsj"].ColumnName = "登录时间";
			return dt;
		}
	}


