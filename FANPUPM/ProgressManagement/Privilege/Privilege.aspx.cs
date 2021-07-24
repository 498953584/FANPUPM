using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ProgressManagement_Privilege_Privilege : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
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
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			this.AspNetPager1.RecordCount = Progress.GetPrivilegePlansCount(this.prjId);
			dataSource = Progress.GetPrivilegePlans(this.prjId, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressId"].ToString();
			e.Row.Attributes["strguid"] = this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
	protected void btnUpdateUserCodes_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldUserCodes.Value;
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		list = new System.Collections.Generic.List<string>(value.Split(new char[]
		{
			','
		}));
		list.RemoveAll((string a) => a == string.Empty);
		if (this.hfldChecked.Value.Contains(","))
		{
			this.hfldChecked.Value = this.hfldChecked.Value.Replace("\"", "").Substring(1).TrimEnd(new char[]
			{
				']'
			});
		}
		string[] array = this.hfldChecked.Value.Split(new char[]
		{
			','
		});
		if (array.Length > 1)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Privilege.AddPrivilegenew(array[i], list);
			}
		}
		else
		{
			Privilege.AddPrivilege(array[0], list);
		}
		this.BindPlans();
	}
}


