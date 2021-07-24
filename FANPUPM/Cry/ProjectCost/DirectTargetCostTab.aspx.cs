using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DirectTargetCostTab : PageBase, IRequiresSessionState
	{

		public AccountReportAction arAction
		{
			get
			{
				return new AccountReportAction();
			}
		}
		public Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}
		public DateTime dtDateTime
		{
			get
			{
				object obj = this.ViewState["dtDateTime"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.Now;
			}
			set
			{
				this.ViewState["dtDateTime"] = value;
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				CryReport cryReport = new CryReport();
				this.ddlPrjname.DataSource = cryReport.GetWorkUsepcList("", base.UserCode);
				this.ddlPrjname.DataTextField = "pn";
				this.ddlPrjname.DataValueField = "pc";
				this.ddlPrjname.DataBind();
				this.Bind_Date();
				this.DGrd_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.DGrdMaterialExpend.ItemDataBound += new DataGridItemEventHandler(this.DGrdMaterialExpend_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void Bind_Date()
		{
			try
			{
				this.DtbStartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
			}
			catch
			{
			}
			this.DtbEndDate.Text = DateTime.Now.ToShortDateString();
		}
		private void DGrd_Bind()
		{
			DateTime dtStartDate = Convert.ToDateTime(this.DtbStartDate.Text.Trim());
			DateTime dtEndDate = Convert.ToDateTime(this.DtbEndDate.Text.Trim());
			if (this.ddlPrjname.Items.Count > 0)
			{
				this.lblPrjName.Text = "[ " + this.ddlPrjname.SelectedItem.Text + " ]";
				this.ProjectCode = new Guid(this.ddlPrjname.SelectedValue);
			}
			DataTable monthDirectTargetAccount = this.arAction.GetMonthDirectTargetAccount(this.ProjectCode, dtStartDate, dtEndDate);
			this.DGrdMaterialExpend.DataSource = monthDirectTargetAccount;
			this.DGrdMaterialExpend.DataBind();
		}
		private void DGrdMaterialExpend_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			this.DGrd_Bind();
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "直接目标成本报表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "直接目标成本报表.doc");
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
			this.Table2.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


