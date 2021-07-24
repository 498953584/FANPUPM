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
	public partial class WorkCost : PageBase, IRequiresSessionState
	{
		private CryReport ca = new CryReport();

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ddl_pn.DataSource = this.ca.GetWorkCostpcList();
				this.ddl_pn.DataTextField = "pn";
				this.ddl_pn.DataValueField = "pc";
				this.ddl_pn.DataBind();
				this.ddl_year.DataSource = this.ca.GetCostYear();
				this.ddl_year.DataTextField = "y";
				this.ddl_year.DataValueField = "y";
				this.ddl_year.DataBind();
				if (this.ddl_month.Items.Count == 0)
				{
					for (int i = 1; i <= 12; i++)
					{
						ListItem listItem = new ListItem();
						listItem.Selected = false;
						listItem.Value = i.ToString();
						listItem.Text = i.ToString();
						this.ddl_month.Items.Add(listItem);
					}
				}
				try
				{
					this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
					this.ddl_month.SelectedValue = DateTime.Now.Month.ToString();
				}
				catch
				{
				}
				this.ddl_year_SelectedIndexChanged(sender, null);
				this.ddl_month_SelectedIndexChanged(sender, null);
				this.ddl_pn_SelectedIndexChanged(sender, null);
				new ContAction();
				this.Lb_userName.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
			}
		}
		private void databind()
		{
			this.dg_ManPower.DataSource = this.ca.GetWorkCostList(this.ddl_pn.SelectedValue.ToString(), this.ddl_year.SelectedValue.ToString(), this.ddl_month.SelectedValue.ToString());
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
			this.dg_ManPower.ItemDataBound += new DataGridItemEventHandler(this.dg_ManPower_ItemDataBound);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void dg_ManPower_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				e.Item.Cells.Clear();
				string text = "项目</TD><TD width='40' rowspan='3' class='grid_head'>行次</TD><TD width='300' colspan='4' class='grid_head'>本期数</TD><TD width='300' colspan='4' class='grid_head'>累计数</TD></tr><TR class='grid_head'><TD width='100' colspan='1' class='grid_head'>预算成本</TD><TD width='100' colspan='1' class='grid_head'>实际成本</TD><TD width='65' colspan='1' class='grid_head'>降低额</TD><TD width='65' colspan='1' class='grid_head'>降低率</TD><TD width='100' colspan='1' class='grid_head'>预算成本</TD><TD width='100' colspan='1' class='grid_head'>实际成本</TD><TD width='65' colspan='1' class='grid_head'>降低额</TD><TD width='65' colspan='1' class='grid_head'>降低率</TD></TR><TR class='grid_head'><TD width='100' class='grid_head'>1</TD><TD width='100' class='grid_head'>2</TD><TD width='65' class='grid_head'>3</TD><TD width='65' class='grid_head'>4</TD><TD width='100' class='grid_head'>5</TD><TD width='100' class='grid_head'>6</TD><TD width='65' class='grid_head'>7</TD><TD width='65' class='grid_head'>8";
				TableCell tableCell = new TableCell();
				tableCell.RowSpan = 3;
				tableCell.CssClass = "grid_head";
				tableCell.Text = text;
				e.Item.Cells.Add(tableCell);
			}
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
			this.ExportToExcel("application/ms-excel", "工程成本表.xls");
		}
		protected void btnWord_Click(object sender, EventArgs e)
		{
			this.ExportToExcel("application/ms-excel", "工程成本表.doc");
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
			this.Table4.RenderControl(writer);
			base.Response.Write(stringWriter.ToString());
			base.Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
	}


