using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_MoreBudget : NBasePage, IRequiresSessionState
{
	private static string id = string.Empty;
	private static string amount = string.Empty;
	private static bool organization = false;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		BudgetManage_Cost_MoreBudget.id = base.Request["id"];
		BudgetManage_Cost_MoreBudget.amount = base.Request["amount"];
		BudgetManage_Cost_MoreBudget.organization = (base.Request["type"] == "zzjg");
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	protected void BindGV()
	{
		if (this.ViewState["Budget"] == null)
		{
			if (BudgetManage_Cost_MoreBudget.organization)
			{
				System.Collections.Generic.List<OrganizationMonthBudget> all = OrganizationMonthBudget.GetAll(BudgetManage_Cost_MoreBudget.id);
				this.ViewState["Budget"] = all;
			}
			else
			{
				System.Collections.Generic.List<IndirectMonthBudget> all2 = IndirectMonthBudget.GetAll(BudgetManage_Cost_MoreBudget.id);
				all2 = IndirectMonthBudget.GetAll(BudgetManage_Cost_MoreBudget.id);
				this.ViewState["Budget"] = all2;
			}
		}
		this.gvBudget.DataSource = this.ViewState["Budget"];
		this.gvBudget.DataBind();
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		if (BudgetManage_Cost_MoreBudget.organization)
		{
			System.Collections.Generic.List<OrganizationMonthBudget> list = new System.Collections.Generic.List<OrganizationMonthBudget>();
			if (this.ViewState["Budget"] != null)
			{
				this.SaveDataToViewState();
				list = (this.ViewState["Budget"] as System.Collections.Generic.List<OrganizationMonthBudget>);
			}
			OrganizationMonthBudget item = OrganizationMonthBudget.Create(System.Guid.NewGuid().ToString(), BudgetManage_Cost_MoreBudget.id, 0m, System.DateTime.Now.Year, System.DateTime.Now.Month);
			list.Add(item);
			if (list.Count > 1)
			{
				this.btnSave.Enabled = false;
			}
			else
			{
				this.btnSave.Enabled = true;
			}
			this.ViewState["Budget"] = list;
		}
		else
		{
			System.Collections.Generic.List<IndirectMonthBudget> list2 = new System.Collections.Generic.List<IndirectMonthBudget>();
			if (this.ViewState["Budget"] != null)
			{
				this.SaveDataToViewState();
				list2 = (this.ViewState["Budget"] as System.Collections.Generic.List<IndirectMonthBudget>);
			}
			IndirectMonthBudget item2 = IndirectMonthBudget.Create(System.Guid.NewGuid().ToString(), BudgetManage_Cost_MoreBudget.id, 0m, System.DateTime.Now.Year, System.DateTime.Now.Month);
			list2.Add(item2);
			if (list2.Count > 1)
			{
				this.btnSave.Enabled = false;
			}
			else
			{
				this.btnSave.Enabled = true;
			}
			this.ViewState["Budget"] = list2;
		}
		this.BindGV();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		this.SaveDataToViewState();
		bool flag = false;
		if (BudgetManage_Cost_MoreBudget.organization)
		{
			System.Collections.Generic.List<OrganizationMonthBudget> list = this.ViewState["Budget"] as System.Collections.Generic.List<OrganizationMonthBudget>;
			int num = list.RemoveAll(new System.Predicate<OrganizationMonthBudget>(this.RemoveSearch));
			if (num > 0)
			{
				flag = true;
				this.ViewState["Budget"] = list;
			}
		}
		else
		{
			System.Collections.Generic.List<IndirectMonthBudget> list2 = this.ViewState["Budget"] as System.Collections.Generic.List<IndirectMonthBudget>;
			int num2 = list2.RemoveAll(new System.Predicate<IndirectMonthBudget>(this.RemoveSearch));
			if (num2 > 0)
			{
				flag = true;
				this.ViewState["Budget"] = list2;
			}
		}
		if (flag)
		{
			base.RegisterScript("alert('系统提示：\\n删除成功！');");
		}
		this.BindGV();
	}
	private bool RemoveSearch(IndirectMonthBudget budget)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		return this.IsExist(budget.Id, listFromJson);
	}
	private bool RemoveSearch(OrganizationMonthBudget budget)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		return this.IsExist(budget.Id, listFromJson);
	}
	private bool IsExist(string budgetId, System.Collections.Generic.List<string> ids)
	{
		bool result = false;
		foreach (string current in ids)
		{
			if (current == budgetId)
			{
				result = true;
			}
		}
		return result;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (BudgetManage_Cost_MoreBudget.organization)
		{
			OrganizationMonthBudget.DelAll(BudgetManage_Cost_MoreBudget.id);
			this.SaveDataToViewState();
			System.Collections.Generic.List<OrganizationMonthBudget> list = this.ViewState["Budget"] as System.Collections.Generic.List<OrganizationMonthBudget>;
			using (System.Collections.Generic.List<OrganizationMonthBudget>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OrganizationMonthBudget current = enumerator.Current;
					current.Add(current);
				}
				goto IL_B7;
			}
		}
		IndirectMonthBudget.DelAll(BudgetManage_Cost_MoreBudget.id);
		this.SaveDataToViewState();
		System.Collections.Generic.List<IndirectMonthBudget> list2 = this.ViewState["Budget"] as System.Collections.Generic.List<IndirectMonthBudget>;
		foreach (IndirectMonthBudget current2 in list2)
		{
			current2.Add(current2);
		}
		IL_B7:
		base.RegisterScript("top.ui.winSuccess();;");
	}
	protected void AddItem(DropDownList ddl, string action)
	{
		if (string.Compare(action, "month", true) == 0)
		{
			for (int i = 1; i <= 12; i++)
			{
				ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			return;
		}
		for (int j = System.DateTime.Now.Year - 4; j <= System.DateTime.Now.Year + 1; j++)
		{
			ddl.Items.Add(new ListItem(j.ToString(), j.ToString()));
		}
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
			DropDownList dropDownList = e.Row.Cells[e.Row.Cells.Count - 1].FindControl("ddlYear") as DropDownList;
			DropDownList dropDownList2 = e.Row.Cells[e.Row.Cells.Count - 1].FindControl("ddlMonth") as DropDownList;
			if (dropDownList != null)
			{
				dropDownList.CssClass = "year";
				this.AddItem(dropDownList, "year");
				dropDownList.SelectedValue = this.gvBudget.DataKeys[e.Row.RowIndex]["Year"].ToString();
			}
			if (dropDownList2 != null)
			{
				dropDownList2.CssClass = "month";
				this.AddItem(dropDownList2, "month");
				dropDownList2.SelectedValue = this.gvBudget.DataKeys[e.Row.RowIndex]["Month"].ToString();
			}
		}
	}
	protected System.Collections.Generic.List<decimal> GetAmounts()
	{
		System.Collections.Generic.List<decimal> list = new System.Collections.Generic.List<decimal>();
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			TextBox textBox = gridViewRow.Cells[gridViewRow.Cells.Count - 2].FindControl("txtAmount") as TextBox;
			if (textBox != null)
			{
				list.Add(decimal.Parse(textBox.Text));
			}
		}
		return list;
	}
	protected System.Collections.Generic.List<int> GetYears()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			DropDownList dropDownList = gridViewRow.Cells[gridViewRow.Cells.Count - 1].FindControl("ddlYear") as DropDownList;
			if (dropDownList != null)
			{
				list.Add(System.Convert.ToInt32(dropDownList.SelectedValue));
			}
		}
		return list;
	}
	protected System.Collections.Generic.List<int> GetMonths()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			DropDownList dropDownList = gridViewRow.Cells[gridViewRow.Cells.Count - 1].FindControl("ddlMonth") as DropDownList;
			if (dropDownList != null)
			{
				list.Add(System.Convert.ToInt32(dropDownList.SelectedValue));
			}
		}
		return list;
	}
	protected void SaveDataToViewState()
	{
		System.Collections.Generic.List<decimal> amounts = this.GetAmounts();
		System.Collections.Generic.List<int> years = this.GetYears();
		System.Collections.Generic.List<int> months = this.GetMonths();
		if (BudgetManage_Cost_MoreBudget.organization)
		{
			System.Collections.Generic.List<OrganizationMonthBudget> list = this.ViewState["Budget"] as System.Collections.Generic.List<OrganizationMonthBudget>;
			int num = 0;
			foreach (OrganizationMonthBudget current in list)
			{
				current.Amount = amounts[num];
				current.Year = years[num];
				current.Month = months[num];
				num++;
			}
			this.ViewState["Budget"] = list;
			return;
		}
		System.Collections.Generic.List<IndirectMonthBudget> list2 = this.ViewState["Budget"] as System.Collections.Generic.List<IndirectMonthBudget>;
		int num2 = 0;
		foreach (IndirectMonthBudget current2 in list2)
		{
			current2.Amount = amounts[num2];
			current2.Year = years[num2];
			current2.Month = months[num2];
			num2++;
		}
		this.ViewState["Budget"] = list2;
	}
}


