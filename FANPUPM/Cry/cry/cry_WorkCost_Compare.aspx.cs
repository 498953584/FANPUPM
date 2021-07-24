using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WorkCost_Compare : PageBase, IRequiresSessionState
	{
		private CryReport ca = new CryReport();

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				CryReport cryReport = new CryReport();
				this.ddl_pn.DataSource = cryReport.GetWorkUsepcList("", base.UserCode);
				this.ddl_pn.DataTextField = "pn";
				this.ddl_pn.DataValueField = "pc";
				this.ddl_pn.DataBind();
				this.ddl_pn.DataBind();
				this.ddl_pn_SelectedIndexChanged(sender, null);
			}
			new ContAction();
			this.Lb_userName.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
		}
		private void databind()
		{
			this.dg_ManPower.DataSource = this.ca.GetWorkCost_CompareList(this.ddl_pn.SelectedValue.ToString());
			this.dg_ManPower.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.ddl_pn.SelectedIndexChanged += new EventHandler(this.ddl_pn_SelectedIndexChanged);
			this.ddl_year.SelectedIndexChanged += new EventHandler(this.ddl_year_SelectedIndexChanged);
			this.ddl_month.SelectedIndexChanged += new EventHandler(this.ddl_month_SelectedIndexChanged);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void ddl_pn_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
		private void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
		private void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind();
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "工程成本差异比较表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "工程成本差异比较表.doc");
		}
		public void ExportToExcel(string FileType, string FileName)
		{
			base.Response.Charset = "GB2312";
			base.Response.ContentEncoding = Encoding.UTF8;
			base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			base.Response.ContentType = FileType;
			this.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			this.Table5.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


