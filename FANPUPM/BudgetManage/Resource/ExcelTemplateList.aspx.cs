using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using System;
using System.Collections;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_ExcelTemplateList : NBasePage, IRequiresSessionState
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
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.SetBtn();
		this.BindGv();
	}
	public void SetBtn()
	{
		if (base.Request["num"] == "1")
		{
			this.btnDel.Text = "放入回收站";
			this.btnDel.Attributes.Add("onclick", "return confirm('您确定要放入回收站吗??')");
			base.RegisterScript("document.getElementById('btnReturn').style.display='none';");
			return;
		}
		this.btnDel.Text = "删 除";
		this.btnDel.Attributes.Add("onclick", "return confirm('您确定要删除吗??')");
		base.RegisterScript("document.getElementById('btnReturn').style.display='block';");
	}
	public void BindGv()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			bool valid = false;
			if (base.Request["num"] == "1")
			{
				valid = true;
			}
			else
			{
				valid = false;
			}
			IOrderedQueryable<Res_Template> source =
				from m in pm2Entities.Res_Template
				where m.IsValid == (bool?)valid
				orderby m.InputDate descending
				select m;
			this.AspNetPager1.RecordCount = source.Count<Res_Template>();
			this.gvResource.DataSource = source.Skip(NBasePage.pagesize * (this.AspNetPager1.CurrentPageIndex - 1)).Take(NBasePage.pagesize);
			this.gvResource.DataBind();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
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
						if (base.Request["num"] == "1")
						{
							Res_Template res_Template = (
								from m in pm2Entities.Res_Template
								where m.TemplateId == cb.ToolTip
								select m).FirstOrDefault<Res_Template>();
							if (res_Template != null)
							{
								res_Template.IsValid = new bool?(false);
								pm2Entities.SaveChanges();
							}
						}
						else
						{
							IQueryable<Res_TemplateItem> queryable =
								from m in pm2Entities.Res_TemplateItem
								where m.Res_Template.TemplateId == cb.ToolTip
								select m;
							foreach (Res_TemplateItem current in queryable)
							{
								pm2Entities.DeleteObject(current);
							}
							Res_Template res_Template2 = (
								from m in pm2Entities.Res_Template
								where m.TemplateId == cb.ToolTip
								select m).FirstOrDefault<Res_Template>();
							if (res_Template2 != null)
							{
								pm2Entities.DeleteObject(res_Template2);
								pm2Entities.SaveChanges();
							}
						}
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
	protected void btnReturn_Click(object sender, System.EventArgs e)
	{
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
						Res_Template res_Template = (
							from m in pm2Entities.Res_Template
							where m.TemplateId == cb.ToolTip
							select m).FirstOrDefault<Res_Template>();
						if (res_Template != null)
						{
							res_Template.IsValid = new bool?(true);
							pm2Entities.SaveChanges();
						}
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
		base.RegisterScript("location.href=location.href;");
	}
}


