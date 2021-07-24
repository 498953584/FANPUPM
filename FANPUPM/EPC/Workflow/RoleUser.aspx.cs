using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class RoleUser : NBasePage, IRequiresSessionState
	{

		public int RoleID
		{
			get
			{
				object obj = this.ViewState["RoleID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RoleID"] = value;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RoleID = System.Convert.ToInt32(base.Request["ri"]);
				this.hdnRoleID.Value = this.RoleID.ToString();
				this.Session["HumanCode"] = "";
				this.Session["HumanName"] = "";
				this.dgdUser_Bind();
				this.btnAdd.Attributes["onclick"] = "openRoleEdit('" + this.RoleID + "')";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
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
			this.dgdUser.ItemDataBound += new DataGridItemEventHandler(this.dgdUser_ItemDataBound);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void dgdUser_Bind()
		{
			this.dgdUser.DataSource = FlowRoleAction.QueryRoleUser(this.RoleID);
			this.dgdUser.DataBind();
		}
		private void btnDel_Click(object sender, System.EventArgs e)
		{
			int userId = System.Convert.ToInt32(this.hdnUserID.Value);
			if (FlowRoleAction.DelUser(userId))
			{
				this.Page.RegisterClientScriptBlock("warn", "<script language=\"javascript\">alert('删除成功！');</script>");
			}
			this.dgdUser_Bind();
		}
		private void dgdUser_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				e.Item.Attributes["id"] = System.Convert.ToString(e.Item.ItemIndex + 1);
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Cells[1].Text = PageHelper.QueryUser(this, dataRowView["usercode"].ToString());
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdUser.ClientID,
					"');doClickRow('",
					dataRowView["Role_User_ID"].ToString(),
					"');"
				});
				return;
			}
			default:
				return;
			}
		}
		protected void dgdUser_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgdUser.CurrentPageIndex = e.NewPageIndex;
			this.dgdUser_Bind();
		}
	}


