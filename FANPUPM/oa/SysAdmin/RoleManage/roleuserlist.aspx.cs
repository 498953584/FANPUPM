using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class roleUserList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.IsPostBack)
			{
				return;
			}
			if (!this.Page.IsPostBack && base.Request.Params["id"] != null)
			{
				roleManageDb roleManageDb = new roleManageDb();
				DataTable dataTable = roleManageDb.selAllUsers(this.Page.Request.Params["id"].ToString());
				this.DataGrid1.DataSource = dataTable;
				this.DataGrid1.DataBind();
				int count = dataTable.Rows.Count;
				dataTable.Clear();
				dataTable = roleManageDb.roleQuary(this.Page.Request.Params["id"].ToString());
				this.lblCaption.Text = string.Concat(new object[]
				{
					"共有 <font color='red'>",
					count,
					"</font> 个用户使用角色 <font color='red'>",
					dataTable.Rows[0]["v_jsmc"].ToString(),
					"</font>"
				});
			}
		}
		protected override void OnInit(EventArgs e)
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
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				break;
			}
			if (e.Item.ItemIndex != -1)
			{
				HyperLink hyperLink = (HyperLink)e.Item.Cells[2].Controls[1];
				hyperLink.Text = "修改";
				hyperLink.NavigateUrl = "#";
				hyperLink.Attributes["onclick"] = "JavaScript:window.showModalDialog('../UserManage/Broker.aspx?Op=mod&id=" + ((DataRowView)e.Item.DataItem)["v_yhdm"].ToString() + "',window,'dialogHeight:350px;dialogWidth:350px;center:1;help:0;status:0;');return false;";
			}
		}
	}


