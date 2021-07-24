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
	public partial class AuditEdit : BasePage, IRequiresSessionState
	{
		public System.Guid InstanceCode
		{
			get
			{
				object obj = this.ViewState["InstanceCode"];
				if (obj != null)
				{
					return (System.Guid)obj;
				}
				return System.Guid.Empty;
			}
			set
			{
				this.ViewState["InstanceCode"] = value;
			}
		}
		public int InstanceID
		{
			get
			{
				object obj = this.ViewState["InstanceID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["InstanceID"] = value;
			}
		}
		public string IsAllPass
		{
			get
			{
				object obj = this.ViewState["IsAllPass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["IsAllPass"] = value;
			}
		}
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				base.Response.Cache.SetNoStore();
				this.InstanceCode = new System.Guid(base.Request["ic"]);
				this.InstanceID = System.Convert.ToInt32(base.Request["id"]);
				this.IsAllPass = base.Request["pass"];
				this.hdnNodeID.Value = base.Request["nid"];
				if (this.hdnNodeID.Value == "0" || this.hdnNodeID.Value == "")
				{
					this.btnFront.Disabled = true;
					this.btnAfter.Disabled = true;
				}
				else
				{
					this.btnFront.Disabled = false;
					this.btnAfter.Disabled = false;
				}
				this.UserCode = System.Convert.ToString(this.Session["yhdm"]);
				this.hdnInstanceCode.Value = this.InstanceCode.ToString();
				this.NodeInfoRestore();
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new System.EventHandler(this.Page_Load);
		}
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			string text = this.txtAuditInfo.Text;
			bool auditResult = this.ddlRoleType.SelectedValue == "1";
			string value = this.hdnType.Value;
			string value2;
			if (value == "前插")
			{
				value2 = this.hdnFrontPerson.Value;
			}
			else
			{
				value2 = this.hdnAfterPerson.Value;
			}
			if (value == "前插" || value == "后插")
			{
				FlowAuditAction.InsertNode(this.InstanceID, this.UserCode, value, this.IsAllPass, value2, auditResult, text, "");
				this.JS.Text = "alert('" + value + "节点成功！');window.returnValue = true;window.close()";
				return;
			}
			FlowAuditAction.ProcessFlow(this.InstanceID, this.IsAllPass, this.UserCode, auditResult, text);
			this.JS.Text = "alert('保存审核结果成功！');window.returnValue = true;window.close()";
		}
		private void NodeInfoRestore()
		{
			DataTable dataTable = FlowTemplateAction.QueryOneNode(System.Convert.ToInt32(this.hdnNodeID.Value));
			if (dataTable.Rows.Count > 0)
			{
				this.LbAuditMain.Text = dataTable.Rows[0]["AuditMain"].ToString();
			}
		}
	}


