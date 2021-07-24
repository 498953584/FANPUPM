using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WordLink : BasePage, IRequiresSessionState
	{
		protected string Type
		{
			get
			{
				return this.ViewState["TYPE"].ToString();
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["t"].ToString() == "1")
				{
					this.Label1.Text = "友情连接";
				}
				else
				{
					if (base.Request.QueryString["t"].ToString() == "2")
					{
						this.Label1.Text = "招商信息";
					}
					else
					{
						if (base.Request.QueryString["t"].ToString() == "3")
						{
							this.Label1.Text = "热销项目";
						}
						else
						{
							if (base.Request.QueryString["t"].ToString() == "4")
							{
								this.Label1.Text = "系统首页图片";
							}
						}
					}
				}
				if (base.Request["t"] == null)
				{
					this.js.Text = "alert('参数错误!');";
					return;
				}
				this.btnAdd.Attributes["onclick"] = "javascript:return Winopen()";
				this.Type = base.Request["t"].ToString();
				this.htnType.Value = this.Type;
				this.ViewState["Type"] = this.GetSqlWhere(this.Type);
				this.gridBind(this.ViewState["Type"].ToString());
			}
		}
		private string GetSqlWhere(string type)
		{
			string text = "LinkType = '" + type.Trim() + "'  ";
			if (this.txtName.Text.Trim() != "")
			{
				text = text + " and LinkName like '%" + this.txtName.Text.Trim() + "%'";
			}
			if (this.txtUrl.Text.Trim() != "")
			{
				text = text + " and LinkUrl like '%" + this.txtUrl.Text.Trim() + "%'";
			}
			return text;
		}
		private void gridBind(string SqlWhere)
		{
			this.PaginationControl1.PageCount = (publicDbOpClass.GetRecordCount("Web_FriendLink", SqlWhere) - 1) / 20 + 1;
			this.DataGrid1.DataSource = publicDbOpClass.GetRecordFromPage("Web_FriendLink", "LinkID", 20, this.PaginationControl1.CurrentPageIndex, 1, SqlWhere);
			this.DataGrid1.DataKeyField = "LinkID";
			this.DataGrid1.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btn_Search.Click += new EventHandler(this.Button1_Click);
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.DataGrid1.CancelCommand += new DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.DeleteCommand += new DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.PaginationControl1.PageIndexChange += new EventHandler(this.PaginationControl1_PageIndexChange);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void Button1_Click(object sender, EventArgs e)
		{
			this.ViewState["Type"] = this.GetSqlWhere(this.htnType.Value);
			this.gridBind(this.ViewState["Type"].ToString());
		}
		private void PaginationControl1_PageIndexChange(object sender, EventArgs e)
		{
			this.gridBind(this.ViewState["Type"].ToString());
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			this.gridBind(this.ViewState["Type"].ToString());
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				LinkButton linkButton = (LinkButton)e.Item.Cells[4].Controls[0];
				linkButton.Attributes["onclick"] = "return confirm('注意：你确定要删除该项吗？',1,0);";
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
			}
		}
		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				string str = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				string sqlString = " delete from Web_FriendLink where LinkId = '" + str + "'";
				int num = publicDbOpClass.ExecSqlString(sqlString);
				if (num == 1)
				{
					this.gridBind(this.ViewState["Type"].ToString());
					return;
				}
				this.js.Text = "alert('删除失败！');";
			}
		}
		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			this.gridBind(this.ViewState["Type"].ToString());
		}
		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			this.gridBind(this.ViewState["Type"].ToString());
		}
		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				string str = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				TextBox textBox = (TextBox)e.Item.Cells[1].Controls[0];
				TextBox textBox2 = (TextBox)e.Item.Cells[2].Controls[0];
				string text = " update Web_FriendLink set LinkName = '" + textBox.Text.Trim() + "',";
				text = text + " LinkUrl = '" + textBox2.Text.Trim() + "'";
				text = text + " where LinkId = '" + str + "'";
				int num = publicDbOpClass.ExecSqlString(text);
				if (num == 1)
				{
					this.DataGrid1.EditItemIndex = -1;
					this.gridBind(this.ViewState["Type"].ToString());
					return;
				}
				this.js.Text = "alert('修改失败！');";
			}
		}
	}


