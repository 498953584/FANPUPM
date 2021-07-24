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
public partial class Equ_RoadElseReport_RoadElseReportList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvReport.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvReport.DataKeys[e.Row.RowIndex].Value.ToString();
			string value = string.Empty;
			if (this.gvReport.DataKeys[e.Row.RowIndex].Values["PrjId"] != null)
			{
				value = this.gvReport.DataKeys[e.Row.RowIndex].Values["PrjId"].ToString();
			}
			e.Row.Attributes["prjId"] = value;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldIdsChecked.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldIdsChecked.Value);
		}
		else
		{
			list.Add(this.hfldIdsChecked.Value);
		}
		try
		{
			foreach (string arg_4E_0 in list)
			{
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGV();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
	private void BindGV()
	{
		this.gvReport.DataBind();
	}
}


