using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class EPC_Workflow_WorkFlowList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
			this.BindDrop();
		}
	}
	public void BindGv()
	{
		string safeString = this.GetSafeString(this.txtclassName.Text.Trim());
		string safeString2 = this.GetSafeString(this.txtTemplateName.Text.Trim());
		string safeString3 = this.GetSafeString(this.txtOrganizer.Text.Trim());
		DataTable workFlowList = WorkFlow.GetWorkFlowList(safeString, safeString2, safeString3, this.txtStartTime.Text.Trim(), this.dropWFState.SelectedValue, this.dropBusinessClass.SelectedValue);
		this.AspNetPager1.RecordCount = workFlowList.Rows.Count;
		this.gvWorkFlow.DataSource = EReport.GetPageDataTable(workFlowList, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvWorkFlow.DataBind();
	}
	public void BindDrop()
	{
		this.dropBusinessClass.Items.Clear();
		this.dropBusinessClass.Items.Add(new ListItem("所有类别", ""));
		this.dropBusinessClass.Items.Add(new ListItem("自定义流程", "999"));
		string sqlString = "\r\n\t\t\tSELECT bcls.* FROM WF_Business_Class bcls\r\n\t\t\tLEFT JOIN WF_BusinessCode bcode ON bcls.BusinessCode = bcode.BusinessCode\r\n\t\t\tJOIN PT_MK mk ON bcode.C_MKDM = mk.C_MKDM\r\n\t\t\tWHERE bcls.BusinessCode != 999\r\n\t\t\tORDER BY mk.I_XH, bcls.BusinessCode\r\n\t\t";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.dropBusinessClass.Items.Add(new ListItem(dataTable.Rows[i]["businessclassname"].ToString(), dataTable.Rows[i]["businesscode"].ToString()));
		}
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvWorkFlow_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"ClickRow('",
				dataRowView["InstanceCode"].ToString(),
				"','",
				dataRowView["BusinessCode"].ToString(),
				"','",
				dataRowView["BusinessClass"].ToString(),
				"');"
			});
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private string GetSafeString(string str)
	{
		str = str.Replace("'", "''");
		str = str.Replace("%", "[%]");
		return str;
	}
}


