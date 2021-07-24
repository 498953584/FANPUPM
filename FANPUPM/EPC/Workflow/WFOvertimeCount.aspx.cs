using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_WFOvertimeCount : BasePage, IRequiresSessionState
{

	protected WorkFlowCount wfc
	{
		get
		{
			return new WorkFlowCount();
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		bool arg_16_0 = base.IsPostBack;
	}
	protected void GVInstance_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["ID"].ToString() + "')";
			e.Row.Cells[3].Text = this.wfc.GetAduitUserName(System.Convert.ToInt32(dataRowView["TemplateID"]), (System.Guid)dataRowView["InstanceCode"]);
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[2].Text = userManageDb.GetUserName(dataRowView["Organiger"].ToString());
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		SqlDataSource expr_06 = this.SqlInstance;
		expr_06.SelectCommand = expr_06.SelectCommand + " where  c.CorpCode='" + this.DDLCorpCode.SelectedValue + "' and IsComplete = '1' ";
		this.GVInstance.DataBind();
	}
}


