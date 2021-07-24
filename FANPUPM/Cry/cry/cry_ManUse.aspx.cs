using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.web;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ManUse : PageBase, IRequiresSessionState
	{
		private CryReport ca = new CryReport();

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ddl_pn.DataSource = this.ca.GetManUsepcList("", base.UserCode);
				this.ddl_pn.DataTextField = "pn";
				this.ddl_pn.DataValueField = "pc";
				this.ddl_pn.DataBind();
				this.ddl_pn_SelectedIndexChanged(sender, null);
			}
		}
		private void databind(string pc)
		{
			this.dg_ManPower.DataSource = this.ca.GetManUseList(pc);
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
		private void dg_ManPower_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				e.Item.Cells.Clear();
				string text = "日期</TD><TD width='100' colspan='2' class='grid_head'>内包工</TD><TD width='100' colspan='2' class='grid_head'>外包工</TD><TD width='100' colspan='2' class='grid_head'>其他</TD><TD width='100'colspan='2' class='grid_head'>合计</TD><TD width='100' rowspan='2' class='grid_head'>备注</TD></tr><TR class='grid_head'><TD width='50' class='grid_head'>年</TD><TD width='50' class='grid_head'>月</TD><TD width='50' class='grid_head'>数量</TD><TD width='50' class='grid_head'>金额</TD><TD width='50' class='grid_head'>数量</TD><TD width='50' class='grid_head'>金额</TD><TD width='50' class='grid_head'>数量</TD><TD width='50' class='grid_head'>金额</TD><TD width='50' class='grid_head'>数量</TD><TD width='50'class='grid_head'>金额";
				TableCell tableCell = new TableCell();
				tableCell.ColumnSpan = 2;
				tableCell.CssClass = "grid_head";
				tableCell.Text = text;
				e.Item.Cells.Add(tableCell);
			}
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		private void ddl_pn_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ddl_pn.Items.Count > 0 && this.ddl_pn.SelectedValue.Trim().Length > 0)
			{
				this.databind(this.ddl_pn.SelectedValue.ToString());
			}
		}
	}


