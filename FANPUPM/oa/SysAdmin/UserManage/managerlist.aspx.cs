using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ManagerList : BasePage, IRequiresSessionState
	{
		protected string _DeptCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			userManageDb userManageDb = new userManageDb();
			if (this.Page.Request.Params["code"] == null)
			{
				this._DeptCode = "1";
			}
			else
			{
				if (this.Page.Request.Params["code"] != null)
				{
					this._DeptCode = this.Page.Request.Params["code"].ToString();
				}
			}
			if (!this.Page.IsPostBack)
			{
				string a = this.Session["yhdm"].ToString();
				if (a != "00000000")
				{
					this.dgUserList.DataSource = "";
					return;
				}
				string str = userManageDb.depName(this._DeptCode);
				DataTable dataSource = userManageDb.depManagerList(this._DeptCode, "y");
				this.dgUserList.DataSource = dataSource;
				this.dgUserList.DataBind();
				this.lblCaption.Text = "<font color=red> " + str + " </font>";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgUserList.ItemDataBound += new DataGridItemEventHandler(this.dgUserList_ItemDataBound);
		}
		protected void dgUserList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			}
		}
	}


