using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_AddIncometMeasure : NBasePage, IRequiresSessionState
{
	private ConIncometContractService conIncometSev = new ConIncometContractService();
	private BudContractConsReportService rptSer = new BudContractConsReportService();
	private BudContractConsTaskService ctSer = new BudContractConsTaskService();
	private BudContractTaskService tSer = new BudContractTaskService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initial();
		}
	}
	protected void Initial()
	{
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			string text = base.Request["ContractID"];
			this.hfldContractId.Value = text;
			ConIncometContract byContractId = this.conIncometSev.GetByContractId(text);
			this.hfldPrjGuid.Value = byContractId.Project;
			this.lblContractName.Text = byContractId.ContractName;
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			string rptId = base.Request["id"];
			if (rptId != string.Empty)
			{
				this.hfldType.Value = "edit";
			}
			else
			{
				this.hfldType.Value = "add";
			}
			this.hfldRptId.Value = rptId;
			BudContractConsReport byId = this.rptSer.GetById(this.hfldRptId.Value);
			this.txtDate.Text = byId.InputDate.ToString("yyyy-MM-dd");
			this.txtNode.Value = byId.Note;
			System.Collections.Generic.List<BudContractConsTask> list = (
				from ct in this.ctSer
				where ct.RptId == rptId
				select ct).ToList<BudContractConsTask>();
			BudContractTaskService budContractTaskService = new BudContractTaskService();
			DataTable dataTable = new DataTable();
			foreach (BudContractConsTask current in list)
			{
				if (current.ContractTask == null)
				{
					current.ContractTask = budContractTaskService.GetTaskById(current.TaskId);
					dataTable = cn.justwin.Domain.BudContractTask.GetSingleBudContractTask(this.hfldPrjGuid.Value, current.TaskId);
					if (dataTable.Rows.Count > 0)
					{
						current.ContractTask.Quantity = System.Convert.ToDecimal(dataTable.Rows[0]["Quantity"]);
						current.ContractTask.Total = new decimal?(System.Convert.ToDecimal(dataTable.Rows[0]["total"]));
					}
				}
			}
			this.SaveToViewState(list);
			this.BindConsTask(list);
		}
		else
		{
			this.hfldRptId.Value = System.Guid.NewGuid().ToString();
		}
		if (!string.IsNullOrEmpty(this.hfldPrjGuid.Value))
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId2 = pTPrjInfoService.GetById(this.hfldPrjGuid.Value);
			if (byId2 != null)
			{
				this.lblPrjName.Text = byId2.PrjName;
				this.txtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			}
		}
	}
	protected void gvwConsTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwConsTask.DataKeys[e.Row.RowIndex]["ConsTaskId"].ToString();
			e.Row.Attributes["Amount"] = this.gvwConsTask.DataKeys[e.Row.RowIndex]["Amount"].ToString();
			e.Row.Attributes["LastAmount"] = ((Label)e.Row.FindControl("lblLastAmount")).Text;
		}
	}
	protected void btnBindTask_Click(object sender, System.EventArgs e)
	{
		this.UpdateGvwToViewState();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldTaskId.Value);
		if (listFromJson.Count == 0)
		{
			return;
		}
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		foreach (string current in listFromJson)
		{
			if (!this.IsContainsTask(fromViewState, current))
			{
				BudContractConsTask budContractConsTask = new BudContractConsTask();
				budContractConsTask.ConsTaskId = System.Guid.NewGuid().ToString();
				budContractConsTask.TaskId = current;
				budContractConsTask.RptId = this.hfldRptId.Value;
				budContractConsTask.ContractTask = this.tSer.GetTaskById(current);
				DataTable singleBudContractTask = cn.justwin.Domain.BudContractTask.GetSingleBudContractTask(this.hfldPrjGuid.Value, current);
				if (singleBudContractTask.Rows.Count > 0)
				{
					budContractConsTask.ContractTask.Quantity = System.Convert.ToDecimal(singleBudContractTask.Rows[0]["Quantity"]);
					budContractConsTask.ContractTask.Total = new decimal?(System.Convert.ToDecimal(singleBudContractTask.Rows[0]["total"]));
				}
				decimal sumTotal = this.GetSumTotal(current);
				if (budContractConsTask.ContractTask.Total - sumTotal <= 0m)
				{
					base.RegisterScript("alert('系统提示：\\n\\n该任务已经完成！')");
					return;
				}
				fromViewState.Add(budContractConsTask);
			}
		}
		this.ViewState["ContractConsTask"] = fromViewState;
		this.BindConsTask(fromViewState);
	}
	private void BindConsTask(System.Collections.Generic.IList<BudContractConsTask> consTaskList)
	{
		this.gvwConsTask.DataSource =
			from ct in consTaskList
			orderby ct.ContractTask.OrderNumber
			select ct;
		this.gvwConsTask.DataBind();
	}
	private bool IsContainsTask(System.Collections.Generic.IList<BudContractConsTask> consTasks, string taskId)
	{
		foreach (BudContractConsTask current in consTasks)
		{
			if (current.TaskId == taskId)
			{
				return true;
			}
		}
		return false;
	}
	private void UpdateGvwToViewState()
	{
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		System.Collections.IEnumerator enumerator = this.gvwConsTask.Rows.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
				string ctId = this.gvwConsTask.DataKeys[gridViewRow.RowIndex]["ConsTaskId"].ToString();
				BudContractConsTask budContractConsTask = (
					from t in fromViewState
					where t.ConsTaskId == ctId
					select t).FirstOrDefault<BudContractConsTask>();
				if (budContractConsTask != null)
				{
					TextBox textBox = gridViewRow.FindControl("txtCompleteAmount") as TextBox;
					if (textBox != null)
					{
						budContractConsTask.Amount = System.Convert.ToDecimal(textBox.Text.Trim());
					}
					TextBox textBox2 = gridViewRow.FindControl("txtApproveAmount") as TextBox;
					if (textBox2 != null)
					{
						budContractConsTask.ApproveAmount = System.Convert.ToDecimal(textBox2.Text.Trim());
					}
					TextBox textBox3 = gridViewRow.FindControl("txtNote") as TextBox;
					if (textBox3 != null)
					{
						budContractConsTask.Note = textBox3.Text.Trim();
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
		this.SaveToViewState(fromViewState);
	}
	private void SaveToViewState(System.Collections.Generic.IList<BudContractConsTask> consTaskList)
	{
		this.ViewState["ContractConsTask"] = consTaskList;
	}
	private System.Collections.Generic.List<BudContractConsTask> GetFromViewState()
	{
		System.Collections.Generic.List<BudContractConsTask> list = this.ViewState["ContractConsTask"] as System.Collections.Generic.List<BudContractConsTask>;
		if (list == null)
		{
			list = new System.Collections.Generic.List<BudContractConsTask>();
		}
		return list;
	}
	protected void btnOk_Click(object sender, System.EventArgs e)
	{
		if (this.gvwConsTask.Rows.Count == 0)
		{
			base.RegisterScript("top.ui.alert('没有选择任务不能进行保存！')");
			return;
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			BudContractConsReport byId = this.rptSer.GetById(base.Request["id"]);
			byId.IsValid = true;
			byId.Note = this.txtNode.Value.Trim();
			System.DateTime inputDate = default(System.DateTime);
			if (!string.IsNullOrEmpty(this.txtDate.Text))
			{
				inputDate = System.Convert.ToDateTime(this.txtDate.Text);
			}
			byId.InputDate = inputDate;
			this.rptSer.Update(byId);
		}
		else
		{
			BudContractConsReport budContractConsReport = new BudContractConsReport();
			budContractConsReport.RptId = this.hfldRptId.Value;
			budContractConsReport.PrjId = this.hfldPrjGuid.Value;
			System.DateTime inputDate2 = default(System.DateTime);
			if (!string.IsNullOrEmpty(this.txtDate.Text))
			{
				inputDate2 = System.Convert.ToDateTime(this.txtDate.Text.Trim());
			}
			budContractConsReport.InputDate = inputDate2;
			budContractConsReport.InputUser = PageHelper.QueryUser(this, base.UserCode);
			budContractConsReport.IsValid = true;
			budContractConsReport.ContractId = this.hfldContractId.Value;
			budContractConsReport.FlowState = -1;
			budContractConsReport.Note = this.txtNode.Value.Trim();
			budContractConsReport.Type = "0";
			this.rptSer.Add(budContractConsReport);
		}
		this.UpdateGvwToViewState();
		this.ctSer.DeleteByReport(this.hfldRptId.Value);
		this.rptSer.DeleteInvalid();
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		foreach (BudContractConsTask current in fromViewState)
		{
			this.ctSer.Add(current);
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_AddIncometMeasure'});");
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		this.UpdateGvwToViewState();
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldConsTaskId.Value);
		foreach (string id in listFromJson)
		{
			BudContractConsTask item = (
				from c in fromViewState
				where c.ConsTaskId == id
				select c).FirstOrDefault<BudContractConsTask>();
			fromViewState.Remove(item);
		}
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	public decimal GetSumTotal(string taskId)
	{
		decimal num = 0m;
		System.Collections.Generic.List<decimal> list = (
			from r in this.ctSer
			where r.TaskId == taskId
			select r.Amount).ToList<decimal>();
		for (int i = 0; i < list.Count; i++)
		{
			num += System.Convert.ToDecimal(list[i]);
		}
		return num;
	}
}


