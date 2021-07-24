using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudContractTaskTaskEdit : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string type = string.Empty;
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
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
			this.BindDropTaskType();
			this.BindHtmlData();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = this.txtQuantity.Text;
		string value = this.txtUnitPrice.Text.Trim();
		decimal value2 = System.Convert.ToDecimal(text) * System.Convert.ToDecimal(value);
		if (string.IsNullOrEmpty(text))
		{
			base.RegisterScript("top.ui.alert('工程量必须输入');");
			this.txtQuantity.Focus();
			return;
		}
		if (string.IsNullOrEmpty(value))
		{
			base.RegisterScript("top.ui.alert('综合单价必须输入');");
			this.txtUnitPrice.Focus();
			return;
		}
		string taskCode = this.txtTaskCode.Text.Trim();
		bool flag = false;
		if (this.ViewState["oldCode"] != null)
		{
			this.ViewState["oldCode"].ToString();
		}
		else
		{
			string arg_B0_0 = string.Empty;
		}
		if (flag)
		{
			base.RegisterScript("top.ui.alert('清单编码不能重复');");
			this.txtTaskCode.Focus();
			return;
		}
		string taskName = this.txtTaskName.Text.Trim();
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartDate.Text))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartDate.Text));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndDate.Text))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndDate.Text));
		}
		string unit = this.txtUnit.Text.Trim();
		string arg_15D_0 = this.ddlTaskType.SelectedValue;
		string note = this.txtNote.Text.Trim();
		int? constructionPeriod = null;
		if (!string.IsNullOrEmpty(this.txtConstructionPeriod.Text.Trim()))
		{
			constructionPeriod = new int?(System.Convert.ToInt32(this.txtConstructionPeriod.Text.Trim()));
		}
		string arg_1B6_0 = this.txtConstructionPeriod.Text;
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		cn.justwin.Domain.Entities.BudContractTask budContractTask;
		if (this.type.ToUpper() == "EDIT")
		{
			budContractTask = budContractTaskService.GetById(this.id);
		}
		else
		{
			budContractTask = new cn.justwin.Domain.Entities.BudContractTask();
			budContractTask.TaskId = System.Guid.NewGuid().ToString();
			budContractTask.OrderNumber = cn.justwin.Domain.BudContractTask.GetOrderNumber(this.prjId, this.id);
			if (string.IsNullOrEmpty(this.id))
			{
				budContractTask.ParentId = null;
			}
			else
			{
				budContractTask.ParentId = this.id;
			}
			budContractTask.PrjId = this.prjId;
			budContractTask.InputUser = base.UserCode;
			budContractTask.InputDate = System.DateTime.Now;
		}
		budContractTask.TaskCode = taskCode;
		budContractTask.TaskName = taskName;
		budContractTask.Unit = unit;
		budContractTask.Quantity = System.Convert.ToDecimal(text);
		budContractTask.StartDate = startDate;
		budContractTask.EndDate = endDate;
		budContractTask.ConstructionPeriod = constructionPeriod;
		budContractTask.Note = note;
		budContractTask.UnitPrice = new decimal?(System.Convert.ToDecimal(value));
		budContractTask.Total = new decimal?(value2);
		budContractTask.TaskType = "";
		budContractTask.FeatureDescription = this.txtDescription.Text.Trim();
		budContractTask.MainMaterial = new decimal?(System.Convert.ToDecimal(this.txtMainMaterial.Text.Trim()));
		budContractTask.SubMaterial = new decimal?(System.Convert.ToDecimal(this.txtSubMaterial.Text.Trim()));
		budContractTask.Labor = new decimal?(System.Convert.ToDecimal(this.txtLabor.Text.Trim()));
		if (this.type.ToUpper() == "EDIT")
		{
			budContractTaskService.Update(budContractTask);
		}
		else
		{
			budContractTaskService.Add(budContractTask);
		}
		string str = "resetData();";
		string str2 = "top.ui.winSuccess({parentName:'_BudContractTaskTaskEdit'});";
		base.RegisterScript(str + str2);
	}
	public void BindDropTaskType()
	{
		System.Collections.Generic.IList<BudTaskType> all = BudTaskType.GetAll();
		this.ddlTaskType.DataSource = all;
		this.ddlTaskType.DataTextField = "Name";
		this.ddlTaskType.DataValueField = "Layer";
		this.ddlTaskType.DataBind();
		ListItem item = new ListItem("", "");
		this.ddlTaskType.Items.Add(item);
	}
	protected void BindHtmlData()
	{
		if (this.type.ToLower() == "edit")
		{
			BudContractTaskService budContractTaskService = new BudContractTaskService();
			cn.justwin.Domain.Entities.BudContractTask byId = budContractTaskService.GetById(this.id);
			if (byId != null)
			{
				this.txtTaskCode.Text = byId.TaskCode;
				this.ViewState["oldCode"] = byId.TaskCode;
				this.txtTaskName.Text = byId.TaskName;
				this.txtStartDate.Text = ((!byId.StartDate.HasValue) ? string.Empty : byId.StartDate.Value.ToString("yyyy-M-d"));
				this.txtEndDate.Text = ((!byId.EndDate.HasValue) ? string.Empty : byId.EndDate.Value.ToString("yyyy-M-d"));
				this.txtConstructionPeriod.Text = (byId.ConstructionPeriod.HasValue ? byId.ConstructionPeriod.Value.ToString() : "");
				this.txtUnit.Text = byId.Unit;
				this.txtQuantity.Text = byId.Quantity.ToString();
				this.txtUnitPrice.Text = byId.UnitPrice.ToString();
				this.txtTotal.Text = byId.Total.ToString();
				this.ddlTaskType.SelectedValue = (byId.OrderNumber.Length / 3).ToString();
				this.txtNote.Text = byId.Note;
				this.txtDescription.Text = byId.FeatureDescription;
				this.txtMainMaterial.Text = byId.MainMaterial.ToString();
				this.txtSubMaterial.Text = byId.SubMaterial.ToString();
				this.txtLabor.Text = byId.Labor.ToString();
			}
			cn.justwin.Domain.BudContractTask.GetById(this.id);
			this.hfldState.Value = "edit";
			return;
		}
		string text = base.Request["layer"];
		if (text == "")
		{
			this.ddlTaskType.SelectedValue = "1";
			return;
		}
		if (text == "0")
		{
			this.ddlTaskType.SelectedValue = string.Empty;
			return;
		}
		this.ddlTaskType.SelectedValue = text;
	}
}


