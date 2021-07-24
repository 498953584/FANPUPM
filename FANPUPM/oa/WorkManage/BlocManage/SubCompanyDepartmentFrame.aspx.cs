using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_BlocManage_SubCompanyDepartmentFrame : BasePage, IRequiresSessionState
{

	public OAOfficeResDepartmentApplicationAction mcAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnTransact.Attributes["onclick"] = "javascript:if(!confirm('确认办理吗?')) return false;";
	}
	protected void GVManager_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["DARecordID"].ToString(),
				"','",
				dataRowView["IsHaveDo"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetDepartmentName(int.Parse(dataRowView["ApplyDepartment"].ToString()));
			string a;
			if ((a = dataRowView["IsHaveDo"].ToString()) != null)
			{
				if (a == "" || a == "0")
				{
					e.Row.Cells[3].Text = "未办理";
					return;
				}
				if (!(a == "1"))
				{
					return;
				}
				e.Row.Cells[3].Text = "已办理";
			}
		}
	}
	protected void btnTransact_Click(object sender, EventArgs e)
	{
		int num = this.mcAction.Update(this.HdnRecordID.Value);
		if (num > 0)
		{
			this.GVManager.DataBind();
			this.Page.RegisterStartupScript("", "<script>alert('办理成功!');</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('办理失败!');</script>");
	}
}


