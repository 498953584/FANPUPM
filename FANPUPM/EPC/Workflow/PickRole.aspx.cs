using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PickRole : NBasePage, IRequiresSessionState
	{
		public string types
		{
			get
			{
				object obj = this.ViewState["types"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "0";
			}
			set
			{
				this.ViewState["types"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.types = base.Request["tp"];
				int roleType;
				if (this.types == "3")
				{
					roleType = 2;
					this.ddlRoleType.SelectedValue = "2";
				}
				else
				{
					if (this.types == "4")
					{
						roleType = 1;
						this.ddlRoleType.SelectedValue = "1";
					}
					else
					{
						roleType = 3;
						this.ddlRoleType.SelectedValue = "3";
					}
				}
				this.ddlRoleType.Enabled = false;
				this.dgdRoleList_Bind(roleType);
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
		private void dgdRoleList_Bind(int roleType)
		{
			base.Response.Cache.SetNoStore();
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
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick('",
					dataRowView["RoleID"].ToString(),
					"','",
					dataRowView["rolename"],
					"')"
				});
				e.Item.Attributes["ondblclick"] = string.Concat(new object[]
				{
					"dodblClick('",
					dataRowView["RoleID"],
					"','",
					dataRowView["RoleName"],
					"');"
				});
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				return;
			}
			default:
				return;
			}
		}
		private void ddlRoleType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		protected void dgdRoleList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			string a;
			int roleType;
			if ((a = base.Request["tp"]) != null)
			{
				if (a == "3")
				{
					roleType = 2;
					goto IL_3C;
				}
				if (a == "4")
				{
					roleType = 1;
					goto IL_3C;
				}
			}
			roleType = 3;
			IL_3C:
			if (this.dgdRoleList.PageCount == 1)
			{
				this.dgdRoleList.CurrentPageIndex = 1;
			}
			if (this.dgdRoleList.CurrentPageIndex > this.dgdRoleList.PageCount && this.dgdRoleList.PageCount > 2)
			{
				this.dgdRoleList.CurrentPageIndex = this.dgdRoleList.PageCount - 1;
			}
			else
			{
				this.dgdRoleList.CurrentPageIndex = e.NewPageIndex;
			}
			this.dgdRoleList.DataSource = FlowRoleAction.QueryAllRole(roleType);
			this.dgdRoleList.DataBind();
		}
	}


