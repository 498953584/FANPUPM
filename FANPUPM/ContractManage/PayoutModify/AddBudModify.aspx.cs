using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutModify_AddBudModify : NBasePage, IRequiresSessionState
{
	private BudTaskService tSer = new BudTaskService();
	private BudModifyTaskService mtSer = new BudModifyTaskService();
	private string isWBSRelevance = ConfigurationManager.AppSettings["IsWBSRelevance"];
	private string type = string.Empty;
	private string prjId = string.Empty;
	private string contractId = string.Empty;
	private string modifyId = string.Empty;
	private string modifyTaskId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.contractId = base.Request["contractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["modifyId"]))
		{
			this.modifyId = base.Request["modifyId"];
		}
		if (!string.IsNullOrEmpty(base.Request["modifyTaskId"]))
		{
			this.modifyTaskId = base.Request["modifyTaskId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldContractId.Value = this.contractId;
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			if (this.type == "add")
			{
				this.hfldModifyTaskId.Value = Guid.NewGuid().ToString();
			}
			if (this.type == "edit")
			{
				string value = base.Request.Cookies["modifyTaskJson"].Value;
				string modifyTaskJson = HttpUtility.UrlDecode(value);
				this.InitEidtModifyTask(modifyTaskJson);
				this.hfldModifyTaskId.Value = this.modifyTaskId;
			}
			this.fileBud.RecordCode = this.hfldModifyTaskId.Value;
		}
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		if (this.ModifyType.Value == "0")
		{
			this.AddOutModify();
			return;
		}
		this.AddInModify();
	}
	protected string GetTaskCode(BudModifyTask mt)
	{
		BudTask byId;
		if (mt.ModifyType.Value == 0)
		{
			byId = this.tSer.GetById(mt.ParentId);
		}
		else
		{
			byId = this.tSer.GetById(mt.TaskId);
		}
		if (byId != null)
		{
			return byId.TaskCode;
		}
		return string.Empty;
	}
	private void InitEidtModifyTask(string modifyTaskJson)
	{
		BudModifyTask budModifyTask = JsonNetWrap.DeserializeObject<BudModifyTask>(modifyTaskJson);
		if (budModifyTask != null)
		{
			if (this.type == "edit")
			{
				this.ModifyType.Attributes.Add("disabled", "disabled");
			}
			this.ModifyType.SelectedIndex = budModifyTask.ModifyType.Value;
			this.txtTaskName.Value = this.GetTaskCode(budModifyTask);
			this.txtTaskCode.Text = budModifyTask.ModifyTaskCode;
			this.txtModifyTaskName.Text = budModifyTask.ModifyTaskContent;
			this.txtUnit.Text = budModifyTask.Unit;
			this.txtQuantity.Value = budModifyTask.Quantity.ToString();
			this.txtUnitPrice.Value = budModifyTask.UnitPrice.ToString();
			this.txtTotal.Value = budModifyTask.Total.ToString();
			this.txtStartDate.Value = budModifyTask.StartDate.ToString();
			this.txtEndDate.Text = budModifyTask.EndDate.ToString();
			this.txtNode.Text = budModifyTask.Note;
			if (budModifyTask.ModifyType.Value == 0)
			{
				this.hfldOriginalTaskId.Value = budModifyTask.ParentId;
				return;
			}
			this.hfldOriginalTaskId.Value = budModifyTask.TaskId;
		}
	}
	private void AddOutModify()
	{
		BudModifyTask budModifyTask = new BudModifyTask();
		budModifyTask.ModifyTaskId = this.hfldModifyTaskId.Value;
		budModifyTask.ModifyId = this.modifyId;
		budModifyTask.ModifyTaskCode = this.txtTaskCode.Text.Trim();
		budModifyTask.ModifyTaskContent = this.txtModifyTaskName.Text.Trim();
		budModifyTask.Unit = this.txtUnit.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtQuantity.Value.Trim()))
		{
			budModifyTask.Quantity = Convert.ToDecimal(this.txtQuantity.Value.Trim());
		}
		else
		{
			budModifyTask.Quantity = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtUnitPrice.Value.Trim()))
		{
			budModifyTask.UnitPrice = Convert.ToDecimal(this.txtUnitPrice.Value.Trim());
		}
		else
		{
			budModifyTask.UnitPrice = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtTotal.Value.Trim()))
		{
			budModifyTask.Total = Convert.ToDecimal(this.txtTotal.Value.Trim());
		}
		else
		{
			budModifyTask.Total = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Value.Trim()))
		{
			budModifyTask.StartDate = new DateTime?(Convert.ToDateTime(this.txtStartDate.Value.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			budModifyTask.EndDate = new DateTime?(Convert.ToDateTime(this.txtEndDate.Text.Trim()));
		}
		if (this.type == "edit")
		{
			string value = base.Request.Cookies["modifyTaskJson"].Value;
			string json = HttpUtility.UrlDecode(value);
			BudModifyTask budModifyTask2 = JsonNetWrap.DeserializeObject<BudModifyTask>(json);
			budModifyTask.OrderNumber = budModifyTask2.OrderNumber;
		}
		else
		{
			budModifyTask.OrderNumber = this.tSer.GetNextChildOrderNumber(this.hfldOriginalTaskId.Value);
		}
		budModifyTask.TaskId = this.hfldOriginalTaskId.Value + "-" + budModifyTask.OrderNumber;
		budModifyTask.Note = this.txtNode.Text.Trim();
		budModifyTask.ModifyType = new int?(0);
		if (!string.IsNullOrEmpty(this.txtConstructPeriod.Text.Trim()))
		{
			budModifyTask.ConstructionPeriod = new int?(Convert.ToInt32(this.txtConstructPeriod.Text.Trim()));
		}
		budModifyTask.FeatureDescription = string.Empty;
		budModifyTask.ParentId = this.hfldOriginalTaskId.Value;
		budModifyTask.PrjId2 = this.prjId;
		budModifyTask.ContractId = this.contractId;
		budModifyTask.Total2 = new decimal?(budModifyTask.UnitPrice * budModifyTask.Quantity);
		this.hfldTaskJson.Value = JsonNetWrap.SerializeObject(budModifyTask);
		base.RegisterScript("save();");
	}
	private void AddInModify()
	{
		BudModifyTask budModifyTask = new BudModifyTask();
		budModifyTask.ModifyTaskId = this.hfldModifyTaskId.Value;
		budModifyTask.ModifyId = this.modifyId;
		budModifyTask.TaskId = this.hfldOriginalTaskId.Value;
		budModifyTask.ModifyTaskCode = this.GetTaskCode(this.hfldOriginalTaskId.Value);
		budModifyTask.ModifyTaskContent = this.txtModifyTaskName.Text.Trim();
		budModifyTask.Unit = this.txtUnit.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtQuantity.Value.Trim()))
		{
			budModifyTask.Quantity = Convert.ToDecimal(this.txtQuantity.Value.Trim());
		}
		else
		{
			budModifyTask.Quantity = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtUnitPrice.Value.Trim()))
		{
			budModifyTask.UnitPrice = Convert.ToDecimal(this.txtUnitPrice.Value.Trim());
		}
		else
		{
			budModifyTask.UnitPrice = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtTotal.Value.Trim()))
		{
			budModifyTask.Total = Convert.ToDecimal(this.txtTotal.Value.Trim());
		}
		else
		{
			budModifyTask.Total = 0m;
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Value.Trim()))
		{
			budModifyTask.StartDate = new DateTime?(Convert.ToDateTime(this.txtStartDate.Value.Trim()));
		}
		else
		{
			budModifyTask.StartDate = null;
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			budModifyTask.EndDate = new DateTime?(Convert.ToDateTime(this.txtEndDate.Text.Trim()));
		}
		else
		{
			budModifyTask.EndDate = null;
		}
		if (this.type == "edit")
		{
			string value = base.Request.Cookies["modifyTaskJson"].Value;
			string json = HttpUtility.UrlDecode(value);
			BudModifyTask budModifyTask2 = JsonNetWrap.DeserializeObject<BudModifyTask>(json);
			budModifyTask.OrderNumber = budModifyTask2.OrderNumber;
		}
		else
		{
			budModifyTask.OrderNumber = this.tSer.GetNextChildOrderNumber(this.hfldOriginalTaskId.Value);
		}
		budModifyTask.Note = this.txtNode.Text.Trim();
		budModifyTask.ModifyType = new int?(1);
		if (!string.IsNullOrEmpty(this.txtConstructPeriod.Text.Trim()))
		{
			budModifyTask.ConstructionPeriod = new int?(Convert.ToInt32(this.txtConstructPeriod.Text.Trim()));
		}
		budModifyTask.FeatureDescription = string.Empty;
		budModifyTask.ParentId = this.hfldOriginalTaskId.Value;
		budModifyTask.PrjId2 = this.prjId;
		budModifyTask.ContractId = this.contractId;
		budModifyTask.Total2 = new decimal?(budModifyTask.UnitPrice * budModifyTask.Quantity);
		this.hfldTaskJson.Value = JsonNetWrap.SerializeObject(budModifyTask);
		base.RegisterScript("save();");
	}
	private string GetTaskCode(string taskId)
	{
		BudTask byId = this.tSer.GetById(taskId);
		if (byId != null)
		{
			return byId.TaskCode;
		}
		return string.Empty;
	}
}


