using ASP;
using cn.justwin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_EquLease_EquLeaseList : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwEquLease_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwEquLease.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwEquLease.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldId.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldId.Value);
		}
		else
		{
			list.Add(this.hfldId.Value);
		}
		try
		{
			foreach (string arg_4E_0 in list)
			{
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGvw();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
	protected string GetLeaseType(string type)
	{
		string result = string.Empty;
		if (type == "T")
		{
			result = "承租";
		}
		if (type == "L")
		{
			result = "出租";
		}
		return result;
	}
}


