using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Budget_ResourceMapping : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

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
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
			this.hfldYear.Value = this.year;
		}
	}
	public void BindGv()
	{
		this.aspNetPageBudget.PageSize = NBasePage.pagesize;
		this.aspNetPageBudget.RecordCount = ResourceTemp.GetResourceTemCount(this.prjId);
		int num = this.aspNetPageBudget.CurrentPageIndex;
		if (num > this.aspNetPageBudget.PageCount)
		{
			num--;
		}
		System.Collections.Generic.List<ResourceTemp> resourceTempByPrj = ResourceTemp.GetResourceTempByPrj(this.prjId, NBasePage.pagesize, num);
		this.gvBudget.DataSource = resourceTempByPrj;
		this.gvBudget.DataBind();
		if (this.hfldIsWBSRelevance.Value == "0")
		{
			this.gvBudget.Columns[2].Visible = false;
		}
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = value;
			object obj = this.gvBudget.DataKeys[e.Row.RowIndex]["ResourceId"];
			e.Row.Attributes["resourceId"] = ((obj == null) ? string.Empty : obj.ToString());
			object obj2 = this.gvBudget.DataKeys[e.Row.RowIndex]["ResourceName"];
			e.Row.Attributes["resourceName"] = ((obj2 == null) ? string.Empty : obj2.ToString());
			BudTask budTask = this.gvBudget.DataKeys[e.Row.RowIndex]["BudTask"] as BudTask;
			e.Row.Attributes["taskId"] = ((budTask == null) ? string.Empty : budTask.Id);
		}
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		if (this.hfldType.Value == "task")
		{
			if (!string.IsNullOrEmpty(this.hfldTaskId.Value))
			{
				ResourceTemp byId = ResourceTemp.GetById(this.hfldPurchaseChecked.Value.ToString());
				byId.BudTask = BudTask.GetById(this.hfldTaskId.Value.Trim());
				string inputUser = PageHelper.QueryUser(this, base.UserCode);
				ResourceTemp.Delete(byId, inputUser, this.hfldIsWBSRelevance.Value);
				this.BindGv();
				BudTaskService budTaskService = new BudTaskService();
				budTaskService.UpdateTotal2(this.hfldTaskId.Value.Trim());
			}
		}
		else
		{
			if (this.hfldType.Value == "resource" && !string.IsNullOrEmpty(this.hfldResourceIds.Value))
			{
				ResourceTemp byId2 = ResourceTemp.GetById(this.hfldPurchaseChecked.Value.ToString());
				byId2.ResourceId = this.hfldResourceIds.Value.Trim();
				byId2.BudTask = BudTask.GetById(this.hfldTaskId.Value);
				string inputUser2 = PageHelper.QueryUser(this, base.UserCode);
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					byId2.BudTask = BudTask.GetById(this.hfldTaskId.Value);
				}
				ResourceTemp.Delete(byId2, inputUser2, this.hfldIsWBSRelevance.Value);
				this.BindGv();
				BudTaskService budTaskService2 = new BudTaskService();
				budTaskService2.UpdateTotal2(this.hfldTaskId.Value.Trim());
			}
		}
		this.hfldTaskId.Value = "";
		this.hfldResourceIds.Value = "";
		this.SelectResource1.ResourceId = string.Empty;
		this.SelectResource1.ResTempType = string.Empty;
	}
	protected void aspNetPageBudget_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string value = this.hfldPurchaseChecked.Value;
		if (value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		try
		{
			ResourceTemp.DelResMapping(list);
		}
		catch
		{
		}
		this.BindGv();
	}
}


