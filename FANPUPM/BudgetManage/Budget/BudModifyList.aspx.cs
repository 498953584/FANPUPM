using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Budget_BudModifyList : NBasePage, IRequiresSessionState
{
	private BudModifyService budModifySer = new BudModifyService();
	private BudModifyTaskService budModifyTaskSer = new BudModifyTaskService();
	private BudTaskService budTaskSer = new BudTaskService();
	private BudModifyService modifySer = new BudModifyService();
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
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	private void InitPage()
	{
		this.hfldPrjId.Value = this.prjId;
		this.hfldYear.Value = this.year;
		bool flag = cn.justwin.Domain.BudTaskChange.IsAllowBudgetChange(this.hfldPrjId.Value);
		if (flag)
		{
			this.hfldIsAllowModify.Value = "1";
			this.BindGv();
			return;
		}
		this.hfldIsAllowModify.Value = "0";
	}
	private void BindGv()
	{
		this.AspNetPager1.RecordCount = (
			from bms in this.budModifySer
			where bms.PrjId == this.prjId
			select bms).Count<BudModify>();
		System.Collections.Generic.List<BudModify> dataSource = (
			from bms in this.budModifySer
			where bms.PrjId == this.prjId
			orderby bms.InputDate descending
			select bms).Take(this.AspNetPager1.PageSize).Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).ToList<BudModify>();
		this.gvBudget.DataSource = dataSource;
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["Id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = this.hfldPrjId.Value;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldModifyId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldModifyId.Value);
		}
		else
		{
			list.Add(this.hfldModifyId.Value);
		}
		foreach (string modifyId in list)
		{
			System.Collections.Generic.List<cn.justwin.Domain.Entities.BudTask> list2 = (
				from r in this.budTaskSer
				where r.ModifyId == modifyId
				select r).ToList<cn.justwin.Domain.Entities.BudTask>();
			if (list2.Count > 0)
			{
				foreach (cn.justwin.Domain.Entities.BudTask current in list2)
				{
					if (current != null)
					{
						this.budTaskSer.Delete(current);
					}
				}
			}
			System.Collections.Generic.List<BudModifyTask> list3 = (
				from r in this.budModifyTaskSer
				where r.ModifyId == modifyId
				select r).ToList<BudModifyTask>();
			foreach (BudModifyTask current2 in list3)
			{
				this.budModifyTaskSer.Delete(current2);
				this.budModifyTaskSer.UpdateTotal2(current2.ModifyTaskId);
			}
		}
		try
		{
			this.budModifySer.Delete(list);
			base.RegisterScript("top.ui.show('删除成功!');");
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败!');");
		}
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected decimal GetBudAmount(string modifyId)
	{
		System.Collections.Generic.List<BudModifyTask> source = (
			from mt in this.budModifyTaskSer
			where mt.ModifyId == modifyId
			select mt).ToList<BudModifyTask>();
		return source.Sum((BudModifyTask mt) => mt.Total);
	}
}


