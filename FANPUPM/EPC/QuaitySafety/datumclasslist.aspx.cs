using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumClassList : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ViewState["TYPEID"] = base.Request.QueryString["TypeId"];
				this.ViewState["VALId"] = "1";
				this.GridBind(this.ViewState["TYPEID"].ToString());
				this.hdnParentId.Value = this.ViewState["TYPEID"].ToString();
			}
		}
		private void GridBind(string Type)
		{
			string @class = base.Request.QueryString["Parent"].ToString();
			this.DG_List.PageSize = 10;
			this.PaginationControl1.PageCount = (DatumClass.GetClassCount(@class, Type) - 1) / this.DG_List.PageSize + 1;
			this.DG_List.DataSource = DatumClass.GetTypeList(@class, Type, this.DG_List.PageSize, this.PaginationControl1.CurrentPageIndex);
			this.DG_List.DataBind();
		}
		protected void PaginationControl1_PageIndexChange(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["TYPEID"].ToString());
		}
		private void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				int num = e.Item.ItemIndex + 1;
				string text = this.DG_List.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Cells[0].Text = num.ToString();
				e.Item.Cells[3].Text = ((e.Item.Cells[3].Text == "True") ? "<img src='../images/green.gif'>可见" : "<img src='../images/red.gif'>不可见");
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DG_List.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					e.Item.Cells[4].Text,
					"');"
				});
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"ReturnCheck('",
					text,
					"','",
					e.Item.Cells[1].Text,
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DG_List.ItemDataBound += new DataGridItemEventHandler(this.DG_List_ItemDataBound);
		}
	}


