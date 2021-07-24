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
	public partial class roleList : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.btnAdd.Attributes["onclick"] = string.Concat(new string[]
				{
					"JavaScript:return showEditWin('add','",
					base.Request.QueryString["rt"],
					"','",
					base.Request.QueryString["rn"],
					"');"
				});
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"javascript:return showEditWin('edit','",
					base.Request.QueryString["rt"],
					"','",
					base.Request.QueryString["rn"],
					"');"
				});
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除该角色吗？')){return false;}else{return true;}";
				this.btnPrivilege.Attributes["onclick"] = "JavaScript:return showEditWin('modPriv','','');";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onclick"] = "getRecordID('" + ((DataRowView)e.Item.DataItem)["RoleCode"].ToString() + "');OnRecord(this);";
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			roleManageDb roleManageDb = new roleManageDb();
			if (roleManageDb.roleDel(this.hfRecord.Value))
			{
				this.JS.Text = "alert('删除成功!');";
				JavaScriptControl expr_2F = this.JS;
				expr_2F.Text = expr_2F.Text + "window.location.href='" + base.Request.Url.ToString() + "';";
				return;
			}
			this.JS.Text = "alert('该角色已被使用，目前不能删除!');";
		}
	}


