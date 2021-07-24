using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ProgressManagement_Plan_Planl : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindPlans();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
	}
	protected void BindPlans()
	{
		this.hfldPrjId.Value = this.prjId;
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			string userCode = base.UserCode;
			this.AspNetPager1.RecordCount = Progress.GetInitalizePlansCount(this.prjId, userCode);
			dataSource = Progress.GetInitalizePlans(this.prjId, userCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes.Add("id", this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString());
			e.Row.Attributes["guid"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressId"].ToString();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldChecked.Value;
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		Progress.Del(list);
		this.BindPlans();
	}
	protected string GetLatest(string flowState)
	{
		string result = "否";
		if (flowState == "1")
		{
			result = "是";
		}
		return result;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
}


