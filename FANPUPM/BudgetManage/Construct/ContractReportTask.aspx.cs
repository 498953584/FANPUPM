using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ContractReportTask : NBasePage, IRequiresSessionState
{
	private string type = string.Empty;
	private string prjId = string.Empty;
	private BudContractConsReportService rptSer = new BudContractConsReportService();
	private BudContractConsTaskService ctSer = new BudContractConsTaskService();
	private BudContractTaskService tSer = new BudContractTaskService();

	private string RptId
	{
		get
		{
			return this.ViewState["rptId"].ToString();
		}
		set
		{
			this.ViewState["rptId"] = value;
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["rptId"]))
		{
			this.RptId = base.Request["rptId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldType.Value = this.type;
			this.Initial();
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
		foreach (string taskId in listFromJson)
		{
			if (!this.IsContainsTask(fromViewState, taskId))
			{
				BudContractConsTask ct = this.ctSer.GetByTaskId(this.RptId, taskId);
				if (ct == null)
				{
					ct = new BudContractConsTask();
					ct.ConsTaskId = System.Guid.NewGuid().ToString();
					ct.TaskId = taskId;
					ct.RptId = this.RptId;
					ct.ContractTask = (
						from r in this.tSer
						where r.TaskId == taskId
						select r).FirstOrDefault<BudContractTask>();
					System.Collections.Generic.List<decimal> list = (
						from r in this.ctSer
						where r.TaskId == ct.TaskId
						select r.Amount).ToList<decimal>();
					decimal num = 0m;
					for (int i = 0; i < list.Count; i++)
					{
						num += System.Convert.ToDecimal(list[i]);
					}
					if (ct.ContractTask.Total - num <= 0m)
					{
						base.RegisterScript("alert('系统提示：\\n\\n该任务已经完成！')");
						return;
					}
					fromViewState.Add(ct);
				}
			}
		}
		this.ViewState["ContractConsTask"] = fromViewState;
		this.BindConsTask(fromViewState);
	}
	protected void btnOk_Click(object sender, System.EventArgs e)
	{
		if (this.gvwConsTask.Rows.Count == 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n没有选择任务的时候不能进行保存！')");
			return;
		}
		BudContractConsReport byId = this.rptSer.GetById(this.RptId);
		byId.IsValid = true;
		byId.InputDate = System.Convert.ToDateTime(this.txtDate.Text);
		byId.Note = this.txtNode.Value;
		this.rptSer.Update(byId);
		this.UpdateGvwToViewState();
		this.ctSer.DeleteByReport(this.RptId);
		this.rptSer.DeleteInvalid();
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		foreach (BudContractConsTask current in fromViewState)
		{
			this.ctSer.Add(current);
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_ContractReportTask' });");
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		this.UpdateGvwToViewState();
		System.Collections.Generic.List<BudContractConsTask> fromViewState = this.GetFromViewState();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldConsTaskId.Value);
		foreach (string id in listFromJson)
		{
			BudContractConsTask ct = (
				from c in fromViewState
				where c.ConsTaskId == id
				select c).FirstOrDefault<BudContractConsTask>();
			fromViewState.Remove(ct);
			BudContractConsTask budContractConsTask = (
				from r in this.ctSer
				where r.TaskId == ct.TaskId
				select r).FirstOrDefault<BudContractConsTask>();
			if (budContractConsTask != null)
			{
				this.ctSer.Delete(budContractConsTask);
			}
		}
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	private void Initial()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
		if (byId != null)
		{
			this.lblPrjName.Text = byId.PrjName;
		}
		if (this.type == "add")
		{
			this.RptId = System.Guid.NewGuid().ToString();
			this.txtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			BudContractConsReport budContractConsReport = new BudContractConsReport();
			budContractConsReport.RptId = this.RptId;
			budContractConsReport.IsValid = false;
			budContractConsReport.PrjId = this.prjId;
			budContractConsReport.InputUser = PageHelper.QueryUser(this, base.UserCode);
			this.rptSer.Add(budContractConsReport);
			return;
		}
		if (this.type == "edit")
		{
			BudContractConsReport byId2 = this.rptSer.GetById(this.RptId);
			if (byId2 != null)
			{
				this.txtDate.Text = byId2.InputDate.ToString("yyyy-MM-dd");
				this.txtNode.Value = byId2.Note;
			}
			else
			{
				this.txtDate.Text = string.Empty;
				this.txtNode.Value = string.Empty;
			}
			System.Collections.Generic.List<BudContractConsTask> list = (
				from ct in this.ctSer
				where ct.RptId == this.RptId
				select ct).ToList<BudContractConsTask>();
			BudContractTaskService source = new BudContractTaskService();
			foreach (BudContractConsTask consTask in list)
			{
				if (consTask.ContractTask == null)
				{
					consTask.ContractTask = (
						from r in source
						where r.TaskId == consTask.TaskId
						select r).FirstOrDefault<BudContractTask>();
				}
			}
			this.SaveToViewState(list);
			this.BindConsTask(list);
		}
	}
	private void BindConsTask(System.Collections.Generic.IList<BudContractConsTask> consTaskList)
	{
		this.gvwConsTask.DataSource =
			from ct in consTaskList
			orderby ct.ContractTask.OrderNumber
			select ct;
		this.gvwConsTask.DataBind();
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


