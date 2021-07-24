using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_WorkFlowCount : NBasePage, IRequiresSessionState
{
	protected WorkFlowCount wfc
	{
		get
		{
			return new WorkFlowCount();
		}
	}
	protected int search
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["SEARCH"]);
		}
		set
		{
			this.ViewState["SEARCH"] = value;
		}
	}
	protected string BusinessClass
	{
		get
		{
			object obj = this.ViewState["businessclass"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["businessclass"] = value;
		}
	}
	protected string BuinessCode
	{
		get
		{
			object obj = this.ViewState["businesscode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["businesscode"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request["ti"] != null)
			{
				this.ViewState["businessclass"] = base.Request["class"].ToString();
				this.ViewState["businesscode"] = base.Request["ti"].ToString();
				this.BindTemp(string.Concat(new string[]
				{
					" where BusinessCode = '",
					base.Request["ti"].ToString(),
					"'  AND [BusinessClass]='",
					base.Request["class"].ToString(),
					"' and IsComplete = '1'"
				}));
				this.BindInstance(string.Concat(new string[]
				{
					" where a.BusinessCode = '",
					base.Request["ti"].ToString(),
					"'AND a.[BusinessClass]='",
					base.Request["class"].ToString(),
					"' and IsComplete = '1'"
				}));
				return;
			}
			this.BindTemp("");
			this.BindInstance("");
		}
	}
	private void BindTemp(string where)
	{
		string text = "SELECT [TemplateID], [BusinessCode], [TemplateName], [Remark], [IsAbnormality], [BusinessClass], [RecordID], [IsComplete], [CorpCode], [IsGeneral],(select count(*) from WF_TemplateNode where TemplateID =WF_Template.TemplateID  ) as number,(select sum(During) from WF_TemplateNode where TemplateID = WF_Template.TemplateID) as during FROM [WF_Template]";
		if (where != "")
		{
			text += where;
		}
		DataTable dataSource = publicDbOpClass.DataTableQuary(text);
		this.GVWorkTemp.DataSource = dataSource;
		this.GVWorkTemp.DataBind();
	}
	private void BindInstance(string where)
	{
		string text = "select a.*,b.* , (SELECT TemplateName FROM WF_Template WHERE (TemplateID = a.TemplateID)) AS TemplateName from WF_Instance_Main a inner join WF_Business_Class  b on  a.BusinessCode = b.BusinessCode and a.BusinessClass = b.BusinessClass LEFT join WF_Template c ON c.[TemplateID] = a.[TemplateID]";
		if (where != "")
		{
			text += where;
		}
		DataTable dataSource = publicDbOpClass.DataTableQuary(text);
		this.GVInstance.DataSource = dataSource;
		this.GVInstance.DataBind();
	}
	protected void GVWorkTemp_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["TemplateID"].ToString() + "')";
			e.Row.Cells[4].Text = this.wfc.UseDegree(System.Convert.ToInt32(dataRowView["TemplateID"]));
		}
	}
	protected void GVInstance_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[4].Text = this.wfc.GetAduitUserName(System.Convert.ToInt32(dataRowView["TemplateID"]), (System.Guid)dataRowView["InstanceCode"]);
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[3].Text = userManageDb.GetUserName(dataRowView["Organiger"].ToString());
		}
	}
	protected void GVWorkTemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.hdTem.Value = "1";
		this.GVWorkTemp.PageIndex = e.NewPageIndex;
		if (this.ViewState["businessclass"] != null && this.ViewState["businesscode"] != null)
		{
			this.BindTemp(string.Concat(new object[]
			{
				" where BusinessCode = '",
				this.ViewState["businesscode"],
				"'  AND [BusinessClass]='",
				this.ViewState["businessclass"],
				"'and IsComplete = '1' AND IsGeneral='0'"
			}));
			this.BindInstance(string.Concat(new object[]
			{
				" where a.BusinessCode = '",
				this.ViewState["businesscode"],
				"' AND a.[BusinessClass]='",
				this.ViewState["businessclass"],
				"' and IsComplete = '1' AND IsGeneral='0'"
			}));
			return;
		}
		this.BindTemp("");
		this.BindInstance("");
	}
	protected void GVInstance_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.hdTem.Value = "0";
		this.GVInstance.PageIndex = e.NewPageIndex;
		if (this.ViewState["businessclass"] != null && this.ViewState["businesscode"] != null)
		{
			this.BindTemp(string.Concat(new object[]
			{
				" where BusinessCode = '",
				this.ViewState["businesscode"],
				"'  AND [BusinessClass]='",
				this.ViewState["businessclass"],
				"'and IsComplete = '1' AND IsGeneral='0'"
			}));
			this.BindInstance(string.Concat(new object[]
			{
				" where a.BusinessCode = '",
				this.ViewState["businesscode"],
				"' AND a.[BusinessClass]='",
				this.ViewState["businessclass"],
				"' and IsComplete = '1' AND IsGeneral='0'"
			}));
			return;
		}
		this.BindTemp("");
		this.BindInstance("");
	}
}


