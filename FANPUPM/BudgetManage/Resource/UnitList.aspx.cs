using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_UnitList : NBasePage, IRequiresSessionState
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
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IOrderedQueryable<Res_Unit> source =
				from m in pm2Entities.Res_Unit
				where m.Name.Contains(this.txtName.Text.Trim()) || this.txtName.Text.Trim() == ""
				orderby m.InputDate descending
				select m;
			this.AspNetPager1.RecordCount = source.Count<Res_Unit>();
			this.gvResource.DataSource = source.Skip(NBasePage.pagesize * (this.AspNetPager1.CurrentPageIndex - 1)).Take(NBasePage.pagesize);
			this.gvResource.DataBind();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		bool flag = false;
		System.Collections.IEnumerator enumerator = this.gvResource.Rows.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
				CheckBox cb = gridViewRow.FindControl("cbBox") as CheckBox;
				if (cb != null && cb.Checked)
				{
					using (pm2Entities pm2Entities = new pm2Entities())
					{
						IQueryable<Res_Resource> source =
							from m in pm2Entities.Res_Resource
							where m.Res_Unit.UnitId == cb.ToolTip
							select m;
						if (source.Count<Res_Resource>() > 0)
						{
							flag = true;
							break;
						}
						list.Add(cb.ToolTip);
					}
				}
			}
		}
		finally
		{
			System.IDisposable disposable = enumerator as System.IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		if (flag)
		{
			base.RegisterScript("alert('系统提示：\\n\\n请先删除子项！')");
			return;
		}
		foreach (string item in list)
		{
			using (pm2Entities pm2Entities2 = new pm2Entities())
			{
				Res_Unit entity = (
					from m in pm2Entities2.Res_Unit
					where m.UnitId == item
					select m).First<Res_Unit>();
				pm2Entities2.DeleteObject(entity);
				pm2Entities2.SaveChanges();
			}
		}
		base.RegisterScript("location.href=location.href;");
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
				e.Row.Attributes["id"] = this.gvResource.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


