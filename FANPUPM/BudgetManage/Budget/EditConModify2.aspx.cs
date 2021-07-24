using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_EditConModify2 : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private BudConModifyService conModifySer = new BudConModifyService();
	private BudConModifyTaskService conModifyTaskSer = new BudConModifyTaskService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			System.Collections.Generic.List<BudConModifyTask> list = new System.Collections.Generic.List<BudConModifyTask>();
			if (this.action == "Add")
			{
				this.hfldModifyId.Value = System.Guid.NewGuid().ToString();
			}
			else
			{
				this.hfldModifyId.Value = this.id;
				this.BindModifyInfo();
				list = (
					from mt in this.conModifyTaskSer
					where mt.ModifyId == this.hfldModifyId.Value
					select mt).ToList<BudConModifyTask>();
				decimal d = 0m;
				d += list.Sum((BudConModifyTask imt) => imt.Total);
				this.txtBudAmount.Value = d.ToString("0.000");
			}
			this.BindGv(list);
			this.hfldPrjId.Value = this.prjId;
			this.uploadModify.RecordCode = this.hfldModifyId.Value;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.GetConModifyTaskGV();
		int count = (
			from modify in this.conModifySer
			where modify.PrjId == this.hfldPrjId.Value.Trim() && modify.ModifyCode == this.txtModifyCode.Text.Trim()
			select modify).ToList<BudConModify>().Count;
		if (this.action == "Add")
		{
			if (count > 0)
			{
				base.RegisterScript("top.ui.show('此编码已经存在!');");
				return;
			}
			if (!string.IsNullOrEmpty(this.txtModifyFileCode.Text.Trim()))
			{
				int count2 = (
					from modify in this.conModifySer
					where modify.ModifyFileCode == this.txtModifyFileCode.Text.Trim()
					select modify).ToList<BudConModify>().Count;
				if (count2 > 0)
				{
					base.RegisterScript("top.ui.show('此变更文件编号已经存在!');");
					return;
				}
			}
			try
			{
				BudConModify model = this.GetModel(null);
				this.conModifySer.Add(model);
				this.SaveModifyTask();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("top.ui.tabSuccess({ parentName: '_EditConModify' });").Append(System.Environment.NewLine);
				base.RegisterScript(stringBuilder.ToString());
				return;
			}
			catch
			{
				base.RegisterScript("top.ui.show('添加失败!');");
				return;
			}
		}
		BudConModify byId = this.conModifySer.GetById(this.hfldModifyId.Value);
		if (byId != null && byId.ModifyCode != this.txtModifyCode.Text.Trim() && count > 0)
		{
			base.RegisterScript("top.ui.show('此编码已经存在!');");
			return;
		}
		if (!string.IsNullOrEmpty(this.txtModifyFileCode.Text.Trim()))
		{
			int count3 = (
				from modify in this.conModifySer
				where modify.ModifyFileCode == this.txtModifyFileCode.Text.Trim()
				select modify).ToList<BudConModify>().Count;
			if (byId.ModifyFileCode != this.txtModifyFileCode.Text.Trim() && count3 > 0)
			{
				base.RegisterScript("top.ui.show('此变更文件编号已经存在!');");
				return;
			}
		}
		try
		{
			this.conModifySer.Update(this.GetModel(byId));
			this.conModifyTaskSer.DelModifyTask(this.hfldModifyId.Value);
			this.SaveModifyTask();
			System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
			stringBuilder2.Append("top.ui.tabSuccess({ parentName: '_EditConModify' });").Append(System.Environment.NewLine);
			base.RegisterScript(stringBuilder2.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('编辑失败!');");
		}
	}
	public void btnEdit_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldEditModifyTaskId.Value;
		System.Collections.Generic.List<BudConModifyTask> conModifyTaskGV = this.GetConModifyTaskGV();
		foreach (BudConModifyTask current in conModifyTaskGV)
		{
			if (current.ModifyTaskId == value)
			{
				string value2 = JsonConvert.SerializeObject(current);
				base.Response.Cookies["budConMidifyTaskJson"].Value = value2;
				base.RegisterScript("editBud();");
				break;
			}
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> conModifyTaskGV = this.GetConModifyTaskGV();
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldEditModifyTaskId.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldEditModifyTaskId.Value);
		}
		else
		{
			list.Add(this.hfldEditModifyTaskId.Value);
		}
		try
		{
			foreach (string current in list)
			{
				foreach (BudConModifyTask current2 in conModifyTaskGV)
				{
					if (current2.ModifyTaskId == current)
					{
						conModifyTaskGV.Remove(current2);
						base.Request.Cookies.Remove(current);
						break;
					}
				}
			}
			this.BindGv(conModifyTaskGV);
			System.Collections.Generic.List<BudConModifyTask> conModifyTaskGV2 = this.GetConModifyTaskGV();
			decimal d = 0m;
			d += conModifyTaskGV2.Sum((BudConModifyTask imt) => imt.Total);
			this.txtBudAmount.Value = d.ToString("0.000");
		}
		catch
		{
			base.RegisterScript("top.ui.alert('删除失败！');");
		}
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex].Values["ModifyTaskId"].ToString();
			e.Row.Attributes["id"] = value;
			Label arg_6C_0 = (Label)e.Row.FindControl("lblTotal");
			string text = this.gvBudget.DataKeys[e.Row.RowIndex].Values["ModifyType"].ToString();
			e.Row.Attributes["modifyType"] = text;
			if (text == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
	}
	protected void btnCancel_Click(object sender, System.EventArgs e)
	{
		base.RegisterScript("winclose('EditConModify', 'BudConModifyList.aspx', false);");
	}
	private BudConModify GetModel(BudConModify budConModify)
	{
		if (budConModify == null)
		{
			budConModify = new BudConModify();
			budConModify.InputDate = System.DateTime.Now;
			budConModify.InputUser = base.UserCode;
			budConModify.Flowstate = -1;
		}
		budConModify.ModifyId = this.hfldModifyId.Value.Trim();
		budConModify.PrjId = this.prjId;
		budConModify.ModifyCode = this.txtModifyCode.Text.Trim();
		budConModify.ModifyContent = this.txtModifyContent.Text.Trim();
		budConModify.ModifyFileCode = this.txtModifyFileCode.Text.Trim();
		budConModify.BudAmount = 0m;
		decimal reportAmount = 0m;
		if (!string.IsNullOrEmpty(this.txtReportAmount.Text.Trim()))
		{
			reportAmount = System.Convert.ToDecimal(this.txtReportAmount.Text.Trim());
		}
		budConModify.ReportAmount = reportAmount;
		decimal approvalAmount = 0m;
		if (!string.IsNullOrEmpty(this.txtApprovalAmount.Text.Trim()))
		{
			approvalAmount = System.Convert.ToDecimal(this.txtApprovalAmount.Text.Trim());
		}
		budConModify.ApprovalAmount = approvalAmount;
		if (!string.IsNullOrEmpty(this.txtApprovalDate.Text.Trim()))
		{
			budConModify.ApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text.Trim()));
		}
		budConModify.Note = this.txtNotes.Text.Trim();
		budConModify.LastModifyUser = base.UserCode;
		budConModify.LastModifyDate = System.DateTime.Now;
		return budConModify;
	}
	private void BindModifyInfo()
	{
		BudConModify byId = this.conModifySer.GetById(this.hfldModifyId.Value);
		if (byId != null)
		{
			this.txtModifyCode.Text = byId.ModifyCode;
			this.txtModifyContent.Text = byId.ModifyContent;
			this.txtModifyFileCode.Text = byId.ModifyFileCode;
			this.txtReportAmount.Text = byId.ReportAmount.ToString();
			this.txtApprovalAmount.Text = byId.ApprovalAmount.ToString();
			if (byId.ApprovalDate.HasValue)
			{
				this.txtApprovalDate.Text = byId.ApprovalDate.Value.ToString("yyyy-MM-dd");
			}
			this.txtNotes.Text = byId.Note;
		}
	}
	private System.Collections.Generic.List<BudConModifyTask> GetConModifyTaskGV()
	{
		System.Collections.Generic.List<BudConModifyTask> list = new System.Collections.Generic.List<BudConModifyTask>();
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			BudConModifyTask budConModifyTask = new BudConModifyTask();
			budConModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldModifyTaskId")).Value;
			budConModifyTask.ModifyId = this.hfldModifyId.Value;
			budConModifyTask.TaskId = ((HiddenField)gridViewRow.FindControl("hfldTaskId")).Value;
			budConModifyTask.ModifyTaskCode = ((Label)gridViewRow.FindControl("lblModifyTaskCode")).Text;
			budConModifyTask.ModifyTaskContent = ((Label)gridViewRow.FindControl("lblModifyTaskContent")).Text;
			budConModifyTask.FeatureDescription = string.Empty;
			budConModifyTask.Unit = ((Label)gridViewRow.FindControl("lblUnit")).Text;
			budConModifyTask.Quantity = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblQuantity")).Text);
			budConModifyTask.UnitPrice = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblUnitPrice")).Text);
			budConModifyTask.Total = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblTotal")).Text);
			string value = ((Label)gridViewRow.FindControl("lblStartDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				budConModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value));
			}
			string value2 = ((Label)gridViewRow.FindControl("lblEndDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value2))
			{
				budConModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value2));
			}
			budConModifyTask.ParentId = ((HiddenField)gridViewRow.FindControl("hfldParentId")).Value;
			budConModifyTask.OrderNumber = ((HiddenField)gridViewRow.FindControl("hfldOrderNumber")).Value;
			budConModifyTask.Note = ((Label)gridViewRow.FindControl("hlinkShowNote")).Text;
			string text = ((Label)gridViewRow.FindControl("lblModifyType")).Text;
			if (text == "清单内")
			{
				budConModifyTask.ModifyType = new int?(1);
			}
			else
			{
				budConModifyTask.ModifyType = new int?(0);
			}
			budConModifyTask.PrjId2 = this.prjId;
			list.Add(budConModifyTask);
		}
		return list;
	}
	private BudConModifyTask GetEmptyConModifyTask()
	{
		return new BudConModifyTask
		{
			ModifyTaskId = System.Guid.NewGuid().ToString(),
			ModifyId = this.id,
			TaskId = string.Empty,
			ModifyTaskCode = string.Empty,
			ModifyTaskContent = string.Empty,
			FeatureDescription = string.Empty,
			Quantity = 0m,
			OrderNumber = string.Empty,
			UnitPrice = 0m,
			MainMaterial = new decimal?(0m),
			SubMaterial = new decimal?(0m),
			Labor = new decimal?(0m),
			Total = 0m,
			StartDate = null,
			EndDate = null,
			ConstructionPeriod = null,
			ModifyType = null,
			Note = string.Empty
		};
	}
	private void BindGv(System.Collections.Generic.List<BudConModifyTask> listConModifyTask)
	{
		foreach (BudConModifyTask current in listConModifyTask)
		{
			if (!current.MainMaterial.HasValue)
			{
				current.MainMaterial = new decimal?(0m);
			}
			if (!current.SubMaterial.HasValue)
			{
				current.SubMaterial = new decimal?(0m);
			}
			if (!current.Labor.HasValue)
			{
				current.Labor = new decimal?(0m);
			}
		}
		this.gvBudget.DataSource = listConModifyTask;
		this.gvBudget.DataBind();
	}
	private void SaveModifyTask()
	{
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			BudConModifyTask budConModifyTask = new BudConModifyTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budConModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldModifyTaskId")).Value;
				budConModifyTask.ModifyId = this.hfldModifyId.Value;
				budConModifyTask.TaskId = ((HiddenField)gridViewRow.FindControl("hfldTaskId")).Value;
				budConModifyTask.ModifyTaskCode = ((Label)gridViewRow.FindControl("lblModifyTaskCode")).Text;
				budConModifyTask.ModifyTaskContent = ((Label)gridViewRow.FindControl("lblModifyTaskContent")).Text;
				budConModifyTask.FeatureDescription = string.Empty;
				budConModifyTask.Unit = ((Label)gridViewRow.FindControl("lblUnit")).Text;
				budConModifyTask.Quantity = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblQuantity")).Text);
				budConModifyTask.UnitPrice = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblUnitPrice")).Text);
				budConModifyTask.Total = System.Convert.ToDecimal(((Label)gridViewRow.FindControl("lblTotal")).Text);
				budConModifyTask.ParentId = ((HiddenField)gridViewRow.FindControl("hfldParentId")).Value;
				string value2 = ((Label)gridViewRow.FindControl("lblStartDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value2))
				{
					budConModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value2));
				}
				string value3 = ((Label)gridViewRow.FindControl("lblEndDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value3))
				{
					budConModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value3));
				}
				string value4 = ((HiddenField)gridViewRow.FindControl("hfldOrderNumber")).Value;
				budConModifyTask.Note = ((Label)gridViewRow.FindControl("hlinkShowNote")).Text;
				string text = ((Label)gridViewRow.FindControl("lblModifyType")).Text;
				if (text == "清单内")
				{
					budConModifyTask.ModifyType = new int?(1);
				}
				else
				{
					budConModifyTask.ModifyType = new int?(0);
				}
				budConModifyTask.OrderNumber = this.GetModifyOrderNumber(value4, budConModifyTask.ParentId);
				budConModifyTask.PrjId2 = this.prjId;
				this.conModifyTaskSer.Add(budConModifyTask);
			}
		}
	}
	private string GetModifyOrderNumber(string orderNumber, string parentId)
	{
		System.Collections.Generic.List<BudConModifyTask> list = (
			from mts in this.conModifyTaskSer.AsQueryable<BudConModifyTask>()
			join ms in this.conModifySer.AsQueryable<BudConModify>() on mts.ModifyId equals ms.ModifyId
			where ms.PrjId == this.hfldPrjId.Value && mts.OrderNumber == orderNumber
			select mts).ToList<BudConModifyTask>();
		if (list.Count > 0)
		{
			orderNumber = this.conModifyTaskSer.GetOrderNumber(this.prjId, parentId);
		}
		return orderNumber;
	}
	private string GetParentId(string taskId)
	{
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		BudContractTask byId = budContractTaskService.GetById(taskId);
		if (byId != null)
		{
			return byId.ParentId;
		}
		return string.Empty;
	}
	protected string GetTaskCode(string taskId)
	{
		string result = string.Empty;
		BudContractTask budContractTask = new BudContractTask();
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
	public void hfldBtnMidifyTask_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> conModifyTaskGV = this.GetConModifyTaskGV();
		string value = base.Request.Cookies["budConMidifyTaskJson"].Value;
		BudConModifyTask budConModifyTask = JsonConvert.DeserializeObject<BudConModifyTask>(value);
		if (budConModifyTask != null)
		{
			BudConModifyTask budConModifyTask2 = null;
			foreach (BudConModifyTask current in conModifyTaskGV)
			{
				if (current.ModifyTaskId == budConModifyTask.ModifyTaskId)
				{
					budConModifyTask2 = current;
					break;
				}
			}
			if (budConModifyTask2 != null)
			{
				for (int i = 0; i < conModifyTaskGV.Count; i++)
				{
					if (conModifyTaskGV[i].ModifyTaskId == budConModifyTask.ModifyTaskId)
					{
						conModifyTaskGV[i] = budConModifyTask;
						break;
					}
				}
			}
			else
			{
				conModifyTaskGV.Add(budConModifyTask);
			}
		}
		this.gvBudget.DataSource = conModifyTaskGV;
		this.gvBudget.DataBind();
	}
}


