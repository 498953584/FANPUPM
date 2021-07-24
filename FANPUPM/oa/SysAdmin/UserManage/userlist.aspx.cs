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
	public partial class UserList : BasePage, IRequiresSessionState
	{
		protected string _DeptCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			userManageDb userManageDb = new userManageDb();
			if (this.Page.Request.Params["code"] == null)
			{
				this.lblCaption.Text = "<font color=red>请选择部门</font>";
				this._DeptCode = "0";
				return;
			}
			if (this.Page.Request.Params["code"] != null)
			{
				if (base.UserCode != "00000000")
				{
					return;
				}
				this._DeptCode = this.Page.Request.Params["code"].ToString();
				if (!this.Page.IsPostBack)
				{
					string text = userManageDb.depName(this._DeptCode);
					DataTable dataTable = userManageDb.depUserList(this._DeptCode, "y");
					this.dgUserList.DataSource = dataTable;
					this.dgUserList.DataBind();
					this.lblCaption.Text = string.Concat(new object[]
					{
						"<font color=red> ",
						text,
						" </font>共有<font color=red> ",
						dataTable.Rows.Count,
						" </font>个用户"
					});
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgUserList.DeleteCommand += new DataGridCommandEventHandler(this.dgUserList_DeleteCommand);
			this.dgUserList.ItemDataBound += new DataGridItemEventHandler(this.dgUserList_ItemDataBound);
			this.dgInvalidUser.DeleteCommand += new DataGridCommandEventHandler(this.dgInvalidUser_DeleteCommand);
			this.dgInvalidUser.ItemDataBound += new DataGridItemEventHandler(this.dgInvalidUser_ItemDataBound);
		}
		private void dgUserList_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string text = this.dgUserList.DataKeys[e.Item.ItemIndex].ToString();
			userManageDb userManageDb = new userManageDb();
			bool flag = userManageDb.userUpdate(text, "n");
			flag = userManageDb.InsertRTXSynchronization(text, "n");
			if (flag)
			{
				this.js.Text = "top.ui.show('冻结成功！');";
				string text2 = userManageDb.depName(this._DeptCode);
				DataTable dataTable = userManageDb.depUserList(this._DeptCode, "y");
				this.dgUserList.DataSource = dataTable;
				this.dgUserList.DataBind();
				this.lblCaption.Text = string.Concat(new object[]
				{
					"<font color=red> ",
					text2,
					" </font>共有<font color=red> ",
					dataTable.Rows.Count,
					" </font>个用户"
				});
				return;
			}
			this.js.Text = "top.ui.alert('" + userManageDb.MessageString + "');";
		}
		protected void lbRecycle_Click(object sender, EventArgs e)
		{
			if (this._DeptCode == null)
			{
				return;
			}
			this.dgUserList.Visible = false;
			this.lbRecycle.Visible = false;
			this.dgInvalidUser.Visible = true;
			this.lbUserList.Visible = true;
			this.btnSure.Visible = true;
			userManageDb userManageDb = new userManageDb();
			string text = userManageDb.depName(this._DeptCode);
			DataTable dataTable = userManageDb.depUserList(this._DeptCode, "n");
			this.dgInvalidUser.DataSource = dataTable;
			this.dgInvalidUser.DataBind();
			if (this._DeptCode == "0")
			{
				this.lblCaption.Text = "共有<font color=red> " + dataTable.Rows.Count + " </font>个用户";
				return;
			}
			this.lblCaption.Text = string.Concat(new object[]
			{
				"<font color=red> ",
				text,
				" 的冻结账号列表中</font>共有<font color=red> ",
				dataTable.Rows.Count,
				" </font>个用户"
			});
		}
		protected void lbUserList_Click(object sender, EventArgs e)
		{
			this.dgUserList.Visible = true;
			this.lbRecycle.Visible = true;
			this.dgInvalidUser.Visible = false;
			this.lbUserList.Visible = false;
			this.btnSure.Visible = false;
			userManageDb userManageDb = new userManageDb();
			string text = userManageDb.depName(this._DeptCode);
			DataTable dataTable = userManageDb.depUserList(this._DeptCode, "y");
			this.dgUserList.DataSource = dataTable;
			this.dgUserList.DataBind();
			this.lblCaption.Text = string.Concat(new object[]
			{
				"<font color=red> ",
				text,
				" </font>共有<font color=red> ",
				dataTable.Rows.Count,
				" </font>个用户"
			});
		}
		private void dgInvalidUser_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string text = this.dgInvalidUser.DataKeys[e.Item.ItemIndex].ToString();
			userManageDb userManageDb = new userManageDb();
			bool flag = userManageDb.userUpdate(text, "y");
			flag = userManageDb.InsertRTXSynchronization(text, "y");
			if (flag)
			{
				userManageDb.UpdateUserPriv(text);
				this.js.Text = "top.ui.show('启用成功！');window.close();";
				string text2 = userManageDb.depName(this._DeptCode);
				DataTable dataTable = userManageDb.depUserList(this._DeptCode, "n");
				this.dgInvalidUser.DataSource = dataTable;
				this.dgInvalidUser.DataBind();
				this.lblCaption.Text = string.Concat(new object[]
				{
					"<font color=red> ",
					text2,
					" 的冻结账号列表中</font>共有<font color=red> ",
					dataTable.Rows.Count,
					" </font>个用户"
				});
				return;
			}
			this.js.Text = "top.ui.alert('" + userManageDb.MessageString + "');";
		}
		private void dgUserList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["id"] = this.dgUserList.DataKeys[e.Item.ItemIndex].ToString();
			}
		}
		private void dgInvalidUser_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Cells[1].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			}
		}
		protected void btnSure_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dgInvalidUser.Items.Count; i++)
				{
					CheckBox checkBox = this.dgInvalidUser.Items[i].FindControl("chbSure") as CheckBox;
					if (checkBox.Checked)
					{
						string text = this.dgInvalidUser.DataKeys[i].ToString();
						userManageDb userManageDb = new userManageDb();
						userManageDb.userUpdate(text, "y");
						userManageDb.InsertRTXSynchronization(text, "y");
						userManageDb.UpdateUserPriv(text);
					}
				}
				userManageDb userManageDb2 = new userManageDb();
				userManageDb2.depName(this._DeptCode);
				DataTable dataSource = userManageDb2.depUserList(this._DeptCode, "n");
				this.dgInvalidUser.DataSource = dataSource;
				this.dgInvalidUser.DataBind();
			}
			catch
			{
				this.js.Text = "alert('启用失败！');window.close();";
			}
		}
	}


