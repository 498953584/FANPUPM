using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UserList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			int num = 0;
			try
			{
				num = int.Parse(base.Request.QueryString["deptid"].ToString());
			}
			catch
			{
			}
			userManageDb userManageDb = new userManageDb();
			DataTable dataTable = userManageDb.DepUserQuaryDt(num.ToString(), "y");
			this.DGrdUser.DataSource = dataTable.DefaultView;
			this.DGrdUser.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdUser.ItemDataBound += new DataGridItemEventHandler(this.DGrdUser_ItemDataBound);
		}
		private void DGrdUser_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				((HyperLink)e.Item.Cells[1].Controls[1]).Attributes.Add("onclick", string.Concat(new object[]
				{
					"selectUser('",
					dataRowView["v_yhdm"],
					"','",
					dataRowView["v_xm"],
					"');"
				}));
			}
		}
	}


