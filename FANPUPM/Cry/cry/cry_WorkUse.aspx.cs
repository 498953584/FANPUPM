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
	public partial class WorkUse : PageBase, IRequiresSessionState
	{
		private string TempT1 = string.Empty;
		private string TempT2 = string.Empty;
		private string TempT3 = string.Empty;
		private CryReport ca = new CryReport();

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ddl_pn.DataSource = this.ca.GetWorkUsepcList("", base.UserCode);
				this.ddl_pn.DataTextField = "pn";
				this.ddl_pn.DataValueField = "pc";
				this.ddl_pn.DataBind();
				this.ddl_pn_SelectedIndexChanged(sender, null);
				this.databind(this.ddl_pn.SelectedValue.ToString());
			}
		}
		private void databind(string pc)
		{
			this.dg_ManPower.DataSource = this.ca.GetWorkUseList(pc);
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
			this.dg_ManPower.ItemDataBound += new DataGridItemEventHandler(this.dg_ManPower_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void ddl_pn_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.databind(this.ddl_pn.SelectedValue.ToString());
		}
		protected void btnexecl_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "分项工程消耗对比表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "分项工程消耗对比表.doc");
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
		private void dg_ManPower_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			string text = e.Item.Cells[0].Text;
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (text == this.TempT1)
				{
					e.Item.Cells[0].Text = "";
					e.Item.Cells[0].Style.Add("border-top", "none");
					e.Item.Cells[0].Style.Add("border-bottom", "none");
				}
				else
				{
					this.TempT1 = text;
					e.Item.Cells[0].Style.Add("border-top", "Inset");
					e.Item.Cells[0].Style.Add("border-bottom", "none");
				}
			}
			string text2 = e.Item.Cells[1].Text;
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (text2 == this.TempT2)
				{
					e.Item.Cells[1].Text = "";
					e.Item.Cells[1].Style.Add("border-top", "none");
					e.Item.Cells[1].Style.Add("border-bottom", "none");
					return;
				}
				this.TempT2 = text2;
				e.Item.Cells[1].Style.Add("border-top", "Inset");
				e.Item.Cells[1].Style.Add("border-bottom", "none");
			}
		}
	}


