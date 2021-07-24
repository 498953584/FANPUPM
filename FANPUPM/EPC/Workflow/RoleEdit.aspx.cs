using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class RoleEidt : NBasePage, IRequiresSessionState
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
		public int RoleType
		{
			get
			{
				object obj = this.ViewState["RoleType"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RoleType"] = value;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RoleID = System.Convert.ToInt32(base.Request["ri"]);
				this.RoleType = System.Convert.ToInt32(base.Request["rt"]);
				this.hfldSelectedValue.Value = base.Request["rt"];
				if (this.RoleID != 0)
				{
					this.RoleRestore(this.RoleID);
					return;
				}
				this.ddlRoleType.SelectedValue = this.RoleType.ToString();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void RoleRestore(int roleId)
		{
			DataTable dataTable = FlowRoleAction.QueryOneRole(roleId);
			if (dataTable.Rows.Count == 1)
			{
				dataTable.Rows[0]["roletype"].ToString();
				this.txtRoleName.Text = dataTable.Rows[0]["rolename"].ToString();
				this.ddlRoleType.SelectedValue = dataTable.Rows[0]["roletype"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["remark"].ToString();
			}
		}
		private void BtnAdd_Click(object sender, System.EventArgs e)
		{
			string text = this.txtRoleName.Text;
			string text2 = this.txtRemark.Text;
			int roleType = System.Convert.ToInt32(this.ddlRoleType.SelectedValue);
			if (this.RoleID == 0)
			{
				if (FlowRoleAction.AddRole(text, roleType, text2))
				{
					base.RegisterScript("successed();");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
			else
			{
				if (FlowRoleAction.UpdRole(this.RoleID, text, roleType, text2))
				{
					base.RegisterScript("successed();");
					return;
				}
				base.RegisterScript("top.ui.alert('保存失败');");
				return;
			}
		}
	}


