using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UserList : PageBase, IRequiresSessionState
	{

		protected static userManageDb UserAct
		{
			get
			{
				return new userManageDb();
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["ItemCode"].ToString() != "")
				{
					this.ViewState["ItemCode"] = base.Request.QueryString["ItemCode"];
				}
				else
				{
					this.ViewState["ItemCode"] = System.Guid.Empty;
				}
				this.gridBind();
			}
		}
		private void gridBind()
		{
			string text = base.Request.QueryString["i_bmdm"];
			if (text == null)
			{
				text = "1 and state=1 ";
			}
			System.Data.DataTable dataSource = UserList.UserAct.depUserList(text, "y");
			this.DataGrid1.DataSource = dataSource;
			this.DataGrid1.DataBind();
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DataGrid1.ClientID.ToString() + "');";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
	}


