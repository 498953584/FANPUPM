using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DocumentAudit : BasePage, IRequiresSessionState
	{
		public Guid InstanceCode
		{
			get
			{
				object obj = this.ViewState["InstanceCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
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
		public string NodeID
		{
			get
			{
				object obj = this.ViewState["NodeID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NodeID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = Convert.ToString(this.Session["yhdm"]);
				this.getParm();
				this.setDate();
				string str = string.Concat(new string[]
				{
					"../../ModuleSet/Workflow/AuditEdit.aspx?ic=",
					this.InstanceCode.ToString(),
					"&id=",
					this.InstanceID.ToString(),
					"&pass=",
					this.IsAllPass.ToString(),
					"&nid=",
					this.NodeID.ToString()
				});
				this.JS.Text = "this.fmAuditEidt.location.href='" + str + "';";
			}
		}
		protected void setDate()
		{
			DataTable dataTable = DocumentAction.QueryOneSendFile(this.InstanceCode);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.txtUserName.Text = dataTable.Rows[0]["userName"].ToString();
				this.txtRecordDate.Text = dataTable.Rows[0]["RecordDate"].ToString();
				this.txtOriginalName.Text = dataTable.Rows[0]["OriginalName"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
			}
		}
		protected void getParm()
		{
			string sqlString = " select b.*,a.InstanceCode from wf_instance_Main a ,wf_Message b where  a.id = b.instanceMainID and charindex('" + this.UserCode + "',b.MsgRecievers) > 0";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count == 1)
			{
				this.InstanceCode = new Guid(dataTable.Rows[0]["InstanceCode"].ToString());
				this.InstanceID = Convert.ToInt32(dataTable.Rows[0]["InstanceID"].ToString());
				this.IsAllPass = dataTable.Rows[0]["IsAllPass"].ToString();
				this.NodeID = dataTable.Rows[0]["NodeID"].ToString();
			}
		}
	}


