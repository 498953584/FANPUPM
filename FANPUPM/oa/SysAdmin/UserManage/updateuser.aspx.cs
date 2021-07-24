using ASP;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UpdateUser : BasePage, IRequiresSessionState
	{
		private userManageDb umd = new userManageDb();
		private string strRoleCodes = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["id"] == null)
				{
					this.js.Text = "alert('参数错误！');window.returnValue=false;window.close();";
					return;
				}
				if (this.Session["pttest"] == null || this.Session["pttest"].ToString() != "notest")
				{
					base.Response.Write("<font color=\"red\">对不起，DEMO版无此权限！</font>");
					base.Response.End();
				}
				this.hidd_userCode.Value = base.Request.Params["id"];
				this.FillItems();
				if (ConfigHelper.ProjectType == "ZdContract")
				{
					this.tr_mailSace.Visible = false;
				}
			}
		}
		private void FillItems()
		{
			DataTable dataTable = this.umd.userQuaryDt(base.Request.Params["id"]);
			this.Label1.Text = "修改用户账号信息";
			this.tbUserName.Text = dataTable.Rows[0]["v_xm"].ToString();
			this.tbID.Text = dataTable.Rows[0]["v_dlid"].ToString();
			this.cbIsManager.Checked = Convert.ToBoolean(Convert.ToInt32(dataTable.Rows[0]["IsManager"]));
			this.rb2.Attributes["onclick"] = "alert('该操作将置用户的密码为初始密码！');";
			this.rb3.Attributes["onclick"] = "alert('该操作将置用户的密码为初始密码！');";
			this.hidd_userCode.Value = base.Request.Params["id"];
			this.tbSpace.Text = dataTable.Rows[0]["MailSpace"].ToString();
			this.strRoleCodes = dataTable.Rows[0]["RoleCodes"].ToString();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			if (this.tbUserName.Text.Trim() == "")
			{
				base.RegisterScript("top.ui.alert('用户名称必填');");
				return;
			}
			string mmFlag = "";
			if (this.rb1.Checked)
			{
				mmFlag = "n";
			}
			else
			{
				if (this.rb2.Checked)
				{
					mmFlag = "y";
				}
			}
			userManageDb userManageDb = new userManageDb();
			string text = ",";
			for (int i = 0; i < this.cblRoleList.Items.Count; i++)
			{
				if (this.cblRoleList.Items[i].Selected)
				{
					text = text + this.cblRoleList.Items[i].Value + ",";
				}
			}
			text = text.Trim(new char[]
			{
				','
			});
			bool flag = userManageDb.userMod(this.hidd_userCode.Value, this.tbUserName.Text, mmFlag, text, this.cbIsManager.Checked ? "1" : "0", this.tbSpace.Text, this.rb3.Checked);
			if (flag)
			{
				base.RegisterScript("top.ui.winSuccess({ parentName: '_userlist' })");
				return;
			}
			base.RegisterScript("top.ui.alert('" + userManageDb.MessageString + "');");
		}
		protected void cblRoleList_DataBound(object sender, EventArgs e)
		{
			if (this.strRoleCodes.Trim() != "")
			{
				this.strRoleCodes = "," + this.strRoleCodes + ",";
			}
			else
			{
				this.strRoleCodes = ",";
			}
			for (int i = 0; i < this.cblRoleList.Items.Count; i++)
			{
				string value = "," + this.cblRoleList.Items[i].Value + ",";
				int num = this.strRoleCodes.IndexOf(value);
				if (num >= 0)
				{
					this.cblRoleList.Items[i].Selected = true;
				}
			}
		}
	}


