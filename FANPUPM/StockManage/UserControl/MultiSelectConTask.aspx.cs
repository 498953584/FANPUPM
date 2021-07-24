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
public partial class StockManage_UserControl_MultiSelectConTask : NBasePage, IRequiresSessionState
{
	private BudContractTaskService ctSer = new BudContractTaskService();
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindYear(true);
			this.BindContractTask();
		}
	}
	protected void gvwConTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvwConTask.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			int subCount = this.ctSer.GetSubCount(text);
			string text2 = this.gvwConTask.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text2.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["orderNumber"] = text2;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = subCount.ToString();
		}
	}
	protected void btnBindTask_Click(object sender, System.EventArgs e)
	{
		this.BindContractTask();
	}
	protected void dropTaskType_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		string selectedValue = this.dropTaskType.SelectedValue;
		if (string.IsNullOrEmpty(selectedValue))
		{
			this.dropYear.SelectedIndex = 0;
			this.dropMonth.SelectedIndex = 0;
		}
		if (selectedValue == "Y")
		{
			this.dropMonth.SelectedIndex = 0;
		}
		this.BindContractTask();
	}
	protected void dropYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindMonth(this.dropYear.SelectedValue, false);
		this.BindContractTask();
	}
	protected void dropMonth_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindContractTask();
	}
	protected string GetTypeByOrderNumber(string orderNumber)
	{
		string result = string.Empty;
		XPMBasicCodeListService xPMBasicCodeListService = new XPMBasicCodeListService();
		XPMBasicCodeList nameByCodeId = xPMBasicCodeListService.GetNameByCodeId(orderNumber.Length / 3, "TaskType");
		if (nameByCodeId != null)
		{
			result = nameByCodeId.CodeName;
		}
		return result;
	}
	private void BindYear(bool changeType)
	{
		System.Collections.Generic.IList<int> years = this.ctSer.GetYears(this.prjId);
		if (years != null)
		{
			foreach (int current in years)
			{
				ListItem listItem = new ListItem(current + "年", current.ToString());
				this.dropYear.Items.Add(listItem);
				if (current == System.DateTime.Now.Year)
				{
					this.dropYear.ClearSelection();
					listItem.Selected = true;
					if (changeType)
					{
						this.dropTaskType.SelectedIndex = 1;
					}
					this.BindMonth(current.ToString(), true);
				}
			}
		}
	}
	private void BindMonth(string year, bool changeType)
	{
		if (string.IsNullOrEmpty(year))
		{
			return;
		}
		this.dropMonth.Items.Clear();
		ListItem item = new ListItem("选择月份", string.Empty);
		this.dropMonth.Items.Add(item);
		System.Collections.Generic.IList<int> months = this.ctSer.GetMonths(this.prjId, year);
		foreach (int current in months)
		{
			ListItem listItem = new ListItem(current + "月", current.ToString());
			this.dropMonth.Items.Add(listItem);
			if (current == System.DateTime.Now.Month)
			{
				listItem.Selected = true;
				if (changeType)
				{
					this.dropTaskType.SelectedIndex = 2;
				}
			}
		}
	}
	private void BindContractTask()
	{
		string selectedValue = this.dropTaskType.SelectedValue;
		int num = string.IsNullOrEmpty(this.dropYear.SelectedValue) ? 0 : System.Convert.ToInt32(this.dropYear.SelectedValue);
		int num2 = string.IsNullOrEmpty(this.dropMonth.SelectedValue) ? 0 : System.Convert.ToInt32(this.dropMonth.SelectedValue);
		System.Collections.Generic.IList<BudContractTask> list = new System.Collections.Generic.List<BudContractTask>();
		if (string.IsNullOrEmpty(selectedValue))
		{
			list = (
				from ct in this.ctSer.GetByProject(this.prjId, 2)
				orderby ct.OrderNumber
				select ct).ToList<BudContractTask>();
		}
		else
		{
			if (selectedValue == "Y" && num != 0)
			{
				list = this.ctSer.GetYearTask(this.prjId, System.Convert.ToInt32(num), 2);
			}
			else
			{
				if (selectedValue == "M" && num != 0 && num2 != 0)
				{
					list = this.ctSer.GetMonthTasks(this.prjId, num, num2, 2);
				}
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].OrderNumber.Length / 3 >= 3)
			{
				list.RemoveAt(i);
			}
		}
		this.gvwConTask.DataSource = list;
		this.gvwConTask.DataBind();
	}
}


