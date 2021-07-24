using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometModify_AddConBudModify : NBasePage, IRequiresSessionState
{
	public BudConModifyService budModifySev = new BudConModifyService();
	private BudConModifyTaskService conModifyTaskSer = new BudConModifyTaskService();
	private BudContractTaskService conTaskSev = new BudContractTaskService();

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.hfldAction.Value = base.Request["action"];
			if (this.hfldAction.Value == "add")
			{
				this.hfldBudModifyTaskId.Value = Guid.NewGuid().ToString();
				this.fileBud.RecordCode = this.hfldBudModifyTaskId.Value;
			}
			if (this.hfldAction.Value == "Edit")
			{
				this.hfldModifyTask.Value = base.Request.Cookies["budConMidifyTaskJson"].Value;
				this.BindInfo();
			}
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.hfldPrjId.Value = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.hfldContractId.Value = base.Request["contractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["modifyId"]))
		{
			this.hfldModifyId.Value = base.Request["modifyId"];
		}
		base.OnInit(e);
	}
	public void BindInfo()
	{
		BudConModifyTask budConModifyTask = JsonConvert.DeserializeObject<BudConModifyTask>(this.hfldModifyTask.Value);
		if (budConModifyTask != null)
		{
			this.hfldBudModifyTaskId.Value = budConModifyTask.ModifyTaskId;
			this.ModifyType.Value = budConModifyTask.ModifyType.ToString();
			this.ModifyType.Disabled = true;
			if (budConModifyTask.ModifyType == 1)
			{
				this.txtTaskName.Value = this.GetTaskCode(budConModifyTask.TaskId);
				this.TaskId.Value = budConModifyTask.TaskId;
				cn.justwin.Domain.Entities.BudContractTask taskById = this.conTaskSev.GetTaskById(budConModifyTask.TaskId);
				this.txtTaskCode.Text = taskById.TaskName;
				this.txtModifyTaskName.Text = budConModifyTask.ModifyTaskContent;
				this.txtUnit.Text = budConModifyTask.Unit;
				this.txtQuantity.Value = budConModifyTask.Quantity.ToString();
				this.txtUnitPrice.Value = taskById.UnitPrice.ToString();
				this.txtTotal.Value = budConModifyTask.Total.ToString();
				this.txtStartDate.Value = Common2.GetTime(budConModifyTask.StartDate);
				this.txtEndDate.Text = Common2.GetTime(budConModifyTask.EndDate);
				this.txtConstructPeriod.Text = budConModifyTask.ConstructionPeriod.ToString();
				this.txtNode.Text = budConModifyTask.Note;
				this.ParentId.Value = budConModifyTask.ParentId;
			}
			else
			{
				if (budConModifyTask.ModifyType == 0)
				{
					this.ModifyType.Value = budConModifyTask.ModifyType.ToString();
					this.txtTaskName.Value = this.GetTaskCode(budConModifyTask.ParentId);
					this.TaskId.Value = budConModifyTask.TaskId;
					this.txtTaskCode.Text = budConModifyTask.ModifyTaskCode;
					this.txtModifyTaskName.Text = budConModifyTask.ModifyTaskContent;
					this.txtUnit.Text = budConModifyTask.Unit;
					this.txtQuantity.Value = budConModifyTask.Quantity.ToString();
					this.txtUnitPrice.Value = budConModifyTask.UnitPrice.ToString();
					this.txtTotal.Value = budConModifyTask.Total.ToString();
					this.txtStartDate.Value = Common2.GetTime(budConModifyTask.StartDate);
					this.txtEndDate.Text = Common2.GetTime(budConModifyTask.EndDate);
					this.txtConstructPeriod.Text = budConModifyTask.ConstructionPeriod.ToString();
					this.txtNode.Text = budConModifyTask.Note;
					this.ParentId.Value = budConModifyTask.ParentId;
				}
			}
			this.fileBud.RecordCode = budConModifyTask.ModifyTaskId;
		}
	}
	public void btnSave_Click(object sender, EventArgs e)
	{
		BudConModifyTask budConModifyTask = new BudConModifyTask();
		string value = this.hfldModifyType.Value;
		budConModifyTask.ModifyTaskId = this.hfldBudModifyTaskId.Value;
		budConModifyTask.ModifyId = this.hfldModifyId.Value;
		budConModifyTask.PrjId2 = this.hfldPrjId.Value;
		DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartDate.Value.Trim()))
		{
			startDate = new DateTime?(Convert.ToDateTime(this.txtStartDate.Value.Trim()));
		}
		budConModifyTask.StartDate = startDate;
		DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			endDate = new DateTime?(Convert.ToDateTime(this.txtEndDate.Text.Trim()));
		}
		budConModifyTask.EndDate = endDate;
		int? constructionPeriod = new int?(0);
		if (!string.IsNullOrEmpty(this.txtConstructPeriod.Text.Trim()))
		{
			constructionPeriod = new int?(int.Parse(this.txtConstructPeriod.Text.Trim()));
		}
		budConModifyTask.ConstructionPeriod = constructionPeriod;
		if (value == "0")
		{
			if (this.hfldAction.Value == "Edit")
			{
				budConModifyTask.TaskId = this.TaskId.Value;
				budConModifyTask.ParentId = this.ParentId.Value;
				budConModifyTask.OrderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.hfldPrjId.Value, budConModifyTask.ParentId);
			}
			else
			{
				budConModifyTask.TaskId = this.TaskId.Value + "-" + cn.justwin.Domain.BudContractTask.GetOrderNumber(this.hfldPrjId.Value, this.TaskId.Value);
				budConModifyTask.OrderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.hfldPrjId.Value, this.TaskId.Value);
				budConModifyTask.ParentId = this.TaskId.Value;
			}
			budConModifyTask.ModifyTaskCode = this.txtTaskCode.Text.Trim();
			budConModifyTask.ModifyType = new int?(0);
		}
		else
		{
			budConModifyTask.TaskId = this.TaskId.Value;
			budConModifyTask.ModifyTaskCode = this.txtTaskName.Value.Trim();
			budConModifyTask.ModifyType = new int?(1);
			budConModifyTask.ParentId = this.GetParentId(this.TaskId.Value);
			budConModifyTask.OrderNumber = (
				from r in this.conTaskSev
				where r.TaskId == this.TaskId.Value
				select r.OrderNumber).FirstOrDefault<string>();
		}
		budConModifyTask.ModifyTaskContent = this.txtModifyTaskName.Text.Trim();
		budConModifyTask.Unit = this.txtUnit.Text.Trim();
		decimal unitPrice = 0m;
		if (!string.IsNullOrEmpty(this.txtUnitPrice.Value.Trim()))
		{
			unitPrice = Convert.ToDecimal(this.txtUnitPrice.Value.Trim());
		}
		budConModifyTask.UnitPrice = unitPrice;
		decimal quantity = 0m;
		if (!string.IsNullOrEmpty(this.txtQuantity.Value.Trim()))
		{
			quantity = Convert.ToDecimal(this.txtQuantity.Value.Trim());
		}
		budConModifyTask.Quantity = quantity;
		decimal total = 0m;
		if (!string.IsNullOrEmpty(this.txtTotal.Value.Trim()))
		{
			total = Convert.ToDecimal(this.txtTotal.Value.Trim());
		}
		budConModifyTask.Total = total;
		budConModifyTask.ContractId = this.hfldContractId.Value;
		budConModifyTask.Note = this.txtNode.Text.Trim();
		string value2 = JsonConvert.SerializeObject(budConModifyTask);
		base.Response.Cookies["budConMidifyTaskJson"].Value = value2;
		base.RegisterScript("btnSave();");
	}
	protected string GetTaskCode(string taskId)
	{
		string result = string.Empty;
		cn.justwin.Domain.Entities.BudContractTask budContractTask = new cn.justwin.Domain.Entities.BudContractTask();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		budContractTask = budContractTaskService.GetById(taskId);
		if (budContractTask != null)
		{
			result = budContractTask.TaskCode;
		}
		else
		{
			BudConModifyTask byId = this.conModifyTaskSer.GetById(taskId);
			if (byId != null)
			{
				result = byId.ModifyTaskCode;
			}
		}
		return result;
	}
	private string GetParentId(string taskId)
	{
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		cn.justwin.Domain.Entities.BudContractTask byId = budContractTaskService.GetById(taskId);
		BudConModifyTaskService source = new BudConModifyTaskService();
		BudConModifyTask budConModifyTask = (
			from r in source
			where r.TaskId == taskId
			select r).FirstOrDefault<BudConModifyTask>();
		if (byId != null)
		{
			return byId.ParentId;
		}
		return budConModifyTask.ParentId;
	}
}


