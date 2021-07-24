using ASP;
using cn.justwin.BLL;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Budget_TaskTypeList : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindTaskType();
		}
	}
	public void DataBindTaskType()
	{
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldTaskTypeId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldTaskTypeId.Value);
		}
		else
		{
			list.Add(this.hfldTaskTypeId.Value);
		}
		try
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除成功！');");
			this.DataBindTaskType();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败，请确定要删除的是最底层的任务类型！');");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindTaskType();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvwTaskType.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


