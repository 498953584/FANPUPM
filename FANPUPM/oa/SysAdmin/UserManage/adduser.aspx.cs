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
	public partial class AddUser : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Label1.Text = "请填写用户信息";
			if (!this.Page.IsPostBack)
			{
				if (base.Request["id"] != null)
				{
					this.hdnTempDeptCode.Value = base.Request.QueryString["id"].ToString();
				}
				if (this.hdnTempDeptCode.Value != "")
				{
					DeptManageDb deptManageDb = new DeptManageDb();
					DataTable deptmentDetail = deptManageDb.GetDeptmentDetail(Convert.ToInt32(this.hdnTempDeptCode.Value));
					this.hdnTempDeptName.Value = deptmentDetail.Rows[0]["v_bmmc"].ToString();
					this.txtDept.Text = this.hdnTempDeptName.Value;
				}
				this.jsBindate();
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
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			if (this.tbUserName.Text.Trim() == "")
			{
				this.js.Text = "alert('用户名称必填！');";
				return;
			}
			if (this.tbID.Text.Trim() == "")
			{
				this.js.Text = "alert('用户登录ID必填！');";
				return;
			}
			if (this.tbPwd.Text.Trim() == "")
			{
				this.js.Text = "alert('用户密码必填！');";
				return;
			}

			userManageDb userManageDb = new userManageDb();
            if (userManageDb.IsReturnLoginID(this.tbID.Text.Trim()))
            {
                this.js.Text = "alert('登录ID重复！');";
                return;
            }
			bool flag = userManageDb.userAdd(this.tbID.Text.Trim(), this.tbUserName.Text.Trim(), this.tbPwd.Text.Trim(), this.hdnTempDeptCode.Value, "-1", 2097152, 100, this.DDLyhjs.SelectedValue.ToString());
			if (flag)
			{
				this.js.Text = "alert('增加成功！');window.close();";
				return;
			}
			this.js.Text = "alert('" + userManageDb.MessageString + "');";
		}
		private void jsBindate()
		{
		}
	}


