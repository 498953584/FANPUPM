using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PickRole : BasePage, IRequiresSessionState
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.dgdRoleList_Bind();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.ddlRoleType.SelectedIndexChanged += new System.EventHandler(this.ddlRoleType_SelectedIndexChanged);
			this.dgdRoleList.ItemDataBound += new DataGridItemEventHandler(this.dgdRoleList_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgdRoleList_Bind()
		{
			int roleType = System.Convert.ToInt32(this.ddlRoleType.SelectedValue);
			this.dgdRoleList.DataSource = FlowRoleAction.QueryAllRole(roleType);
			this.dgdRoleList.DataBind();
		}
		private void dgdRoleList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgdRoleList.ClientID + "');";
				e.Item.Attributes["ondblclick"] = string.Concat(new object[]
				{
					"dodblClick('",
					dataRowView["RoleID"],
					"','",
					dataRowView["RoleName"],
					"');"
				});
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			}
			default:
				return;
			}
		}
		private void ddlRoleType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.dgdRoleList_Bind();
		}
	}


