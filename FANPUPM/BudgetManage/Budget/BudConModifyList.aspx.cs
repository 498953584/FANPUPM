using ASP;
using cn.justwin.BLL;
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
public partial class BudgetManage_Budget_BudConModifyList : NBasePage, IRequiresSessionState
{
	private BudContractTaskAuditService budConTaskAuditSer = new BudContractTaskAuditService();
	private BudConModifyService budConModifySer = new BudConModifyService();
	private BudConModifyTaskService budConModifyTaskSer = new BudConModifyTaskService();
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
		bool flag = false;
		BudContractTaskAudit byProject = this.budConTaskAuditSer.GetByProject(this.hfldPrjId.Value);
		if (byProject != null && byProject.FlowState == 1)
		{
			flag = true;
		}
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
			from bcms in this.budConModifySer
			where bcms.PrjId == this.prjId
			select bcms).Count<BudConModify>();
		System.Collections.Generic.List<BudConModify> dataSource = (
			from bcms in this.budConModifySer
			where bcms.PrjId == this.prjId
			orderby bcms.InputDate descending
			select bcms).Take(this.AspNetPager1.PageSize).Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).ToList<BudConModify>();
		this.gvConBudget.DataSource = dataSource;
		this.gvConBudget.DataBind();
	}
	protected void gvConBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["Id"] = this.gvConBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvConBudget.DataKeys[e.Row.RowIndex].Value.ToString();
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
		try
		{
			this.budConModifySer.Delete(list);
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
		System.Collections.Generic.List<BudConModifyTask> source = (
			from mt in this.budConModifyTaskSer
			where mt.ModifyId == modifyId
			select mt).ToList<BudConModifyTask>();
		return source.Sum((BudConModifyTask mt) => mt.Total);
	}
}


