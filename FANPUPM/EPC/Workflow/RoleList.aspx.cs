using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class RoleList : NBasePage, IRequiresSessionState
	{
		public int RoleType
		{
			get
			{
				object obj = this.ViewState["RoleType"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RoleType"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RoleType = System.Convert.ToInt32(base.Request["rt"]);
				this.hdnTypeID.Value = this.RoleType.ToString();
				this.btnAdd.Attributes["onclick"] = "openRoleEdit(1)";
				this.btnEdit.Attributes["onclick"] = "openRoleEdit(2)";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				this.dgdRole_Bind();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			this.dgdRole.ItemDataBound += new DataGridItemEventHandler(this.dgdRole_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgdRole_Bind()
		{
			DataTable dataSource = FlowRoleAction.QueryAllRole(this.RoleType);
			this.dgdRole.DataSource = dataSource;
			this.dgdRole.DataBind();
		}
		private void dgdRole_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				e.Item.Attributes["id"] = System.Convert.ToString(e.Item.ItemIndex + 1);
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdRole.ClientID,
					"');doClickRow('",
					dataRowView["RoleID"].ToString(),
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
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int roleId = System.Convert.ToInt32(this.hdnRoleID.Value);
			if (FlowRoleAction.IsHaveUser(roleId))
			{
				this.Page.RegisterClientScriptBlock("warn", "<script language=\"javascript\">alert('该角色下有用户,不允许删除！');</script>");
			}
			else
			{
				if (FlowRoleAction.DelRole(roleId))
				{
					this.Page.RegisterClientScriptBlock("warn", "<script language=\"javascript\">alert('删除成功！');parent.location.reload();</script>");
				}
			}
			this.dgdRole_Bind();
		}
		protected void dgdRole_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgdRole.CurrentPageIndex = e.NewPageIndex;
			this.dgdRole_Bind();
		}
	}


