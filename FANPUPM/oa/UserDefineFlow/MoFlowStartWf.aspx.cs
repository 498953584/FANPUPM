using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_UserDefineFlow_MoFlowStartWf : BasePage, IRequiresSessionState
{
	public string BusinessCode
	{
		get
		{
			object obj = this.ViewState["BusinessCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "0";
		}
		set
		{
			this.ViewState["BusinessCode"] = value;
		}
	}
	public string BusinessClass
	{
		get
		{
			object obj = this.ViewState["BusinessClass"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "0";
		}
		set
		{
			this.ViewState["BusinessClass"] = value;
		}
	}
	public System.Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RecordID"];
			if (obj != null)
			{
				return (System.Guid)obj;
			}
			return System.Guid.Empty;
		}
		set
		{
			this.ViewState["RecordID"] = value;
		}
	}
	public int NodeID
	{
		get
		{
			object obj = this.ViewState["NodeID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["NodeID"] = value;
		}
	}
	public string ProjectCode
	{
		get
		{
			object obj = this.ViewState["ProjectCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["ProjectCode"] = value;
		}
	}
	public int TemplateID
	{
		get
		{
			object obj = this.ViewState["TemplateID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["TemplateID"] = value;
		}
	}
	public int TemplateCount
	{
		get
		{
			object obj = this.ViewState["TemplateCount"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["TemplateCount"] = value;
		}
	}
	public int OffsetCount
	{
		get
		{
			object obj = this.ViewState["OffsetCount"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["OffsetCount"] = value;
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

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.BusinessCode = base.Request["code"];
			this.BusinessClass = base.Request["class"];
			this.RecordID = new System.Guid(base.Request["record"]);
			this.TemplateCount = System.Convert.ToInt32(base.Request["mbs"]);
			this.ProjectCode = base.Request["project"].ToString();
			this.UserCode = this.Session["yhdm"].ToString();
			if (this.TemplateCount == 1)
			{
				this.OffsetCount = System.Convert.ToInt32(base.Request["lx"]);
				this.NodeID = System.Convert.ToInt32(base.Request["node"]);
			}
			this.TemplateID = System.Convert.ToInt32(base.Request["it"]);
			this.LbTemName.Text = FlowAuditAction.strTempLateName(this.TemplateID);
			this.IBPick.Attributes["onclick"] = "return pickPerson();";
			this.DutyBind();
		}
	}
	protected void BtnAdd_Click(object sender, System.EventArgs e)
	{
		string[] array = new string[3];
		if (this.TemplateCount > 1)
		{
			array = FlowAuditAction.GetOffsetNodeFirst(this.TemplateID, this.BusinessClass, this.RecordID);
			this.hdnNodeId.Value = array[0].ToString();
			this.hdnOrderNumber.Value = array[1].ToString();
		}
		else
		{
			if (this.TemplateCount == 1)
			{
				if (this.OffsetCount > 1)
				{
					this.hdnNodeId.Value = this.NodeID.ToString();
					this.hdnOrderNumber.Value = "1";
				}
				else
				{
					array = FlowAuditAction.GetOffsetNodeFirst(this.TemplateID, this.BusinessClass, this.RecordID);
					this.hdnNodeId.Value = array[0].ToString();
					this.hdnOrderNumber.Value = array[1].ToString();
				}
			}
		}
		int offsetorder = System.Convert.ToInt32(this.hdnOrderNumber.Value.ToString().Trim());
		this.NodeID = System.Convert.ToInt32(this.hdnNodeId.Value.ToString().Trim());
		if (this.hdnNodeId.Value.Trim() == "0")
		{
			this.JS.Text = "alert('工作流程多分支模板未设置分支条件！')";
			return;
		}
		if (FlowAuditAction.BeginFlow(this.BusinessClass, this.BusinessCode, this.RecordID, this.TemplateID, this.NodeID, offsetorder, this.ProjectCode, this.UserCode))
		{
			if (FlowAuditAction.FirstNodeIsSelected(this.TemplateID))
			{
				if (this.DDLSuperordinateDuty.Items.Count > 0)
				{
					this.hdnReceiver.Value = this.DDLSuperordinateDuty.SelectedValue;
				}
				FlowAuditAction.InsertOperator(this.BusinessClass, this.BusinessCode, this.RecordID, this.hdnReceiver.Value);
			}
			this.JS.Text = "alert('工作流程已成功启动！');window.close();returnValue=true;";
			return;
		}
		this.JS.Text = "alert('请找管理员设置 " + this.LbTemName.Text + " 流程的负责人。');";
	}
	protected void IBPick_Click(object sender, ImageClickEventArgs e)
	{
		userManageDb userManageDb = new userManageDb();
		this.txtReceiver.Text = userManageDb.GetUserName(this.hdnReceiver.Value);
		int num = 0;
		for (int i = 0; i < this.DDLSuperordinateDuty.Items.Count; i++)
		{
			if (this.DDLSuperordinateDuty.Items[0].Value == this.hdnReceiver.Value)
			{
				num = 1;
			}
		}
		if (num == 0)
		{
			this.DDLSuperordinateDuty.Items.Add(new ListItem(this.txtReceiver.Text.Trim(), this.hdnReceiver.Value));
			this.DDLSuperordinateDuty.SelectedValue = this.hdnReceiver.Value;
			return;
		}
		this.DDLSuperordinateDuty.SelectedValue = this.hdnReceiver.Value;
	}
	protected void DutyBind()
	{
		this.IBPick.ImageUrl = "../../images/contact.gif";
		this.IBPick.Enabled = true;
		this.DDLSuperordinateDuty.Enabled = true;
		this.DDLSuperordinateDuty.DataTextField = "v_xm";
		this.DDLSuperordinateDuty.DataValueField = "v_yhdm";
		this.DDLSuperordinateDuty.DataSource = FlowAuditAction.SuperordinateDutyYH(this.Session["yhdm"].ToString());
		this.DDLSuperordinateDuty.DataBind();
	}
}


