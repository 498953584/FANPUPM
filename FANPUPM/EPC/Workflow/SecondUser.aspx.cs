using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SecondUser : BasePage, IRequiresSessionState
	{

		private void Page_Load(object sender, System.EventArgs e)
		{
			bool arg_0B_0 = this.Page.IsPostBack;
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			string value = this.hdnUserCode.Value;
			System.DateTime dtBegin = System.Convert.ToDateTime(this.txtBeginDate.Text);
			System.DateTime dtEnd = System.Convert.ToDateTime(this.txtEndDate.Text);
			if (FlowAuditAction.AddSecondUser(base.UserCode, value, dtBegin, dtEnd))
			{
				this.JS.Text = "alert('增加成功！');";
				this.btnOK.Visible = false;
				this.btnUpd.Visible = true;
				return;
			}
			this.JS.Text = "alert('增加失败！');";
		}
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			if (FlowAuditAction.DelSecondUser(base.UserCode))
			{
				this.txtSecondUser.Text = "";
				this.hdnUserCode.Value = "";
				this.JS.Text = "alert('删除成功！');";
				this.btnUpd.Visible = false;
				this.btnOK.Visible = true;
				return;
			}
			this.JS.Text = "alert('删除失败！');";
		}
		private void btnUpd_Click(object sender, System.EventArgs e)
		{
			string value = this.hdnUserCode.Value;
			System.DateTime dtBegin = System.Convert.ToDateTime(this.txtBeginDate.Text);
			System.DateTime dtEnd = System.Convert.ToDateTime(this.txtEndDate.Text);
			if (FlowAuditAction.UpdSecondUser(base.UserCode, value, dtBegin, dtEnd))
			{
				this.JS.Text = "alert('更新成功！');";
				this.btnOK.Visible = false;
				this.btnUpd.Visible = true;
				return;
			}
			this.JS.Text = "alert('更新失败！');";
		}
	}


