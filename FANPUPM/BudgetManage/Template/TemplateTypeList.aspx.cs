using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Template_TemplateTypeList : NBasePage, IRequiresSessionState
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
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.gvTemplate.DataSource = BudTemplateType.Select(NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex - 1);
		this.gvTemplate.DataBind();
		this.AspNetPager1.RecordCount = BudTemplateType.GetAll().Count;
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPurchaseChecked.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		try
		{
			BudTemplateType.Delete(list);
			base.RegisterScript("alert('系统提示：\\n\\n删除成功！');");
			this.BindGv();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败，请确定要删除的是最底层的模板类型！');");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvTemplate.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


