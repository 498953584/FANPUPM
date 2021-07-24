using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_EditConModify : NBasePage, IRequiresSessionState
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
			System.Collections.Generic.List<BudConModifyTask> list2 = new System.Collections.Generic.List<BudConModifyTask>();
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
					where mt.ModifyId == this.hfldModifyId.Value && mt.ModifyType.Value == 0
					select mt).ToList<BudConModifyTask>();
				list2 = (
					from mt in this.conModifyTaskSer
					where mt.ModifyId == this.hfldModifyId.Value && mt.ModifyType.Value == 1
					select mt).ToList<BudConModifyTask>();
				decimal d = 0m;
				d += list.Sum((BudConModifyTask omt) => omt.Total);
				d += list2.Sum((BudConModifyTask imt) => imt.Total);
				this.txtBudAmount.Value = d.ToString("0.000");
			}
			this.BindGv(list, list2);
			this.hfldPrjId.Value = this.prjId;
			this.uploadModify.RecordCode = this.hfldModifyId.Value;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.GetOutConModifyTaskGV();
		this.GetInConModifyTaskGV();
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
				this.SaveOutModifyTask();
				this.SaveInModifyTask();
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
			this.SaveOutModifyTask();
			this.SaveInModifyTask();
			System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
			stringBuilder2.Append("top.ui.tabSuccess({ parentName: '_EditConModify' });").Append(System.Environment.NewLine);
			base.RegisterScript(stringBuilder2.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('编辑失败!');");
		}
	}
	protected void btnDelOut_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV = this.GetOutConModifyTaskGV();
		System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV = this.GetInConModifyTaskGV();
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldCheckedTaskIdOut.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldCheckedTaskIdOut.Value);
		}
		else
		{
			list.Add(this.hfldCheckedTaskIdOut.Value);
		}
		try
		{
			foreach (string current in list)
			{
				foreach (BudConModifyTask current2 in outConModifyTaskGV)
				{
					if (current2.ModifyTaskId == current)
					{
						outConModifyTaskGV.Remove(current2);
						base.Request.Cookies.Remove(current);
						break;
					}
				}
			}
			this.BindGv(outConModifyTaskGV, inConModifyTaskGV);
			base.RegisterScript("clickWBSType('out');");
			System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV2 = this.GetOutConModifyTaskGV();
			System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV2 = this.GetInConModifyTaskGV();
			decimal d = 0m;
			d += outConModifyTaskGV2.Sum((BudConModifyTask omt) => omt.Total);
			d += inConModifyTaskGV2.Sum((BudConModifyTask imt) => imt.Total);
			this.txtBudAmount.Value = d.ToString("0.000");
		}
		catch
		{
			base.RegisterScript("clickWBSType('out');top.ui.alert('删除失败！');");
		}
	}
	protected void btnDelIn_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV = this.GetOutConModifyTaskGV();
		System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV = this.GetInConModifyTaskGV();
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldCheckedTaskIdIn.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldCheckedTaskIdIn.Value);
		}
		else
		{
			list.Add(this.hfldCheckedTaskIdIn.Value);
		}
		try
		{
			foreach (string current in list)
			{
				foreach (BudConModifyTask current2 in inConModifyTaskGV)
				{
					if (current2.ModifyTaskId == current)
					{
						inConModifyTaskGV.Remove(current2);
						base.Request.Cookies.Remove(current);
						break;
					}
				}
			}
			this.BindGv(outConModifyTaskGV, inConModifyTaskGV);
			base.RegisterScript("clickWBSType('in');");
			System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV2 = this.GetOutConModifyTaskGV();
			System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV2 = this.GetInConModifyTaskGV();
			decimal d = 0m;
			d += outConModifyTaskGV2.Sum((BudConModifyTask omt) => omt.Total);
			d += inConModifyTaskGV2.Sum((BudConModifyTask imt) => imt.Total);
			this.txtBudAmount.Value = d.ToString("0.000");
		}
		catch
		{
			base.RegisterScript("clickWBSType('in');top.ui.alert('删除失败！');");
		}
	}
	protected void gvOutTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvOutTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			TextBox textBox = (TextBox)e.Row.FindControl("txtOutTotal");
			textBox.Attributes.Add("readonly", "readonly");
			textBox.Style.Add("background-color", "#F5F5F5");
			TextBox textBox2 = (TextBox)e.Row.FindControl("txtOutUnitPrice");
			TextBox textBox3 = (TextBox)e.Row.FindControl("txtOutQuantity");
			textBox2.Attributes.Add("onBlur", "setValue('" + text + "')");
			textBox3.Attributes.Add("onBlur", "setValue('" + text + "')");
		}
	}
	protected void gvInTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvInTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			TextBox textBox = (TextBox)e.Row.FindControl("txtInTotal");
			textBox.Attributes.Add("readonly", "readonly");
			textBox.Style.Add("background-color", "#F5F5F5");
			TextBox textBox2 = (TextBox)e.Row.FindControl("txtInUnitPrice");
			TextBox textBox3 = (TextBox)e.Row.FindControl("txtInQuantity");
			textBox2.Attributes.Add("onBlur", "setValue('" + text + "')");
			textBox3.Attributes.Add("onBlur", "setValue('" + text + "')");
		}
	}
	protected void btnAddOut_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV = this.GetOutConModifyTaskGV();
		BudConModifyTask outEmptyConModifyTask = this.GetOutEmptyConModifyTask();
		outConModifyTaskGV.Add(outEmptyConModifyTask);
		System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV = this.GetInConModifyTaskGV();
		this.BindGv(outConModifyTaskGV, inConModifyTaskGV);
		base.RegisterScript("clickWBSType('out')");
	}
	protected void btnAddIn_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudConModifyTask> outConModifyTaskGV = this.GetOutConModifyTaskGV();
		System.Collections.Generic.List<BudConModifyTask> inConModifyTaskGV = this.GetInConModifyTaskGV();
		BudConModifyTask inEmptyConModifyTask = this.GetInEmptyConModifyTask();
		inConModifyTaskGV.Add(inEmptyConModifyTask);
		this.BindGv(outConModifyTaskGV, inConModifyTaskGV);
		base.RegisterScript("clickWBSType('in')");
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
	private System.Collections.Generic.List<BudConModifyTask> GetOutConModifyTaskGV()
	{
		System.Collections.Generic.List<BudConModifyTask> list = new System.Collections.Generic.List<BudConModifyTask>();
		foreach (GridViewRow gridViewRow in this.gvOutTask.Rows)
		{
			BudConModifyTask budConModifyTask = new BudConModifyTask();
			budConModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldOutModifyTaskId")).Value;
			budConModifyTask.ModifyId = this.hfldModifyId.Value;
			budConModifyTask.TaskId = ((HiddenField)gridViewRow.FindControl("hfldOutTaskId")).Value;
			budConModifyTask.ModifyTaskCode = ((TextBox)gridViewRow.FindControl("txtOutTaskCode")).Text;
			budConModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtOutTaskContent")).Text;
			budConModifyTask.FeatureDescription = ((TextBox)gridViewRow.FindControl("txtDescription")).Text;
			budConModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtOutUnit")).Text;
			budConModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutQuantity")).Text);
			budConModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text);
			budConModifyTask.MainMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtMainMaterial")).Text));
			budConModifyTask.SubMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtSubMaterial")).Text));
			budConModifyTask.Labor = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtLabor")).Text));
			budConModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutTotal")).Text);
			string value = ((TextBox)gridViewRow.FindControl("txtOutStartDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				budConModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value));
			}
			string value2 = ((TextBox)gridViewRow.FindControl("txtOutEndDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value2))
			{
				budConModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value2));
			}
			string text = ((TextBox)gridViewRow.FindControl("txtOutConstructionPeriod")).Text;
			if (!string.IsNullOrEmpty(text))
			{
				budConModifyTask.ConstructionPeriod = new int?(System.Convert.ToInt32(text));
			}
			budConModifyTask.OrderNumber = ((HiddenField)gridViewRow.FindControl("hfldOutTaskType")).Value;
			budConModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtOutNote")).Text;
			budConModifyTask.ModifyType = new int?(0);
			list.Add(budConModifyTask);
		}
		return list;
	}
	private System.Collections.Generic.List<BudConModifyTask> GetInConModifyTaskGV()
	{
		System.Collections.Generic.List<BudConModifyTask> list = new System.Collections.Generic.List<BudConModifyTask>();
		foreach (GridViewRow gridViewRow in this.gvInTask.Rows)
		{
			list.Add(new BudConModifyTask
			{
				ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldInModifyTaskId")).Value,
				ModifyId = this.hfldModifyId.Value,
				TaskId = ((HiddenField)gridViewRow.FindControl("hfldInTaskId")).Value,
				ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtInTaskContent")).Text,
				Unit = ((TextBox)gridViewRow.FindControl("txtInUnit")).Text,
				Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInQuantity")).Text),
				UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInUnitPrice")).Text),
				MainMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtMainMaterial")).Text)),
				SubMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtSubMaterial")).Text)),
				Labor = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtLabor")).Text)),
				Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInTotal")).Text),
				Note = ((TextBox)gridViewRow.FindControl("txtInNote")).Text,
				ModifyType = new int?(1)
			});
		}
		return list;
	}
	private BudConModifyTask GetOutEmptyConModifyTask()
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
	private BudConModifyTask GetInEmptyConModifyTask()
	{
		return new BudConModifyTask
		{
			ModifyTaskId = System.Guid.NewGuid().ToString(),
			ModifyId = this.id,
			TaskId = string.Empty,
			ModifyTaskCode = string.Empty,
			ModifyTaskContent = string.Empty,
			Quantity = 0m,
			OrderNumber = string.Empty,
			UnitPrice = 0m,
			MainMaterial = new decimal?(0m),
			SubMaterial = new decimal?(0m),
			Labor = new decimal?(0m),
			Total = 0m,
			ModifyType = null,
			Note = string.Empty
		};
	}
	private void BindGv(System.Collections.Generic.List<BudConModifyTask> listOutConModifyTask, System.Collections.Generic.List<BudConModifyTask> listInConModifyTask)
	{
		foreach (BudConModifyTask current in listInConModifyTask)
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
		foreach (BudConModifyTask current2 in listOutConModifyTask)
		{
			if (!current2.MainMaterial.HasValue)
			{
				current2.MainMaterial = new decimal?(0m);
			}
			if (!current2.SubMaterial.HasValue)
			{
				current2.SubMaterial = new decimal?(0m);
			}
			if (!current2.Labor.HasValue)
			{
				current2.Labor = new decimal?(0m);
			}
		}
		this.gvOutTask.DataSource = listOutConModifyTask;
		this.gvOutTask.DataBind();
		this.gvInTask.DataSource = listInConModifyTask;
		this.gvInTask.DataBind();
	}
	private void SaveOutModifyTask()
	{
		foreach (GridViewRow gridViewRow in this.gvOutTask.Rows)
		{
			BudConModifyTask budConModifyTask = new BudConModifyTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldOutTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budConModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldOutModifyTaskId")).Value;
				budConModifyTask.ModifyId = this.hfldModifyId.Value;
				budConModifyTask.TaskId = value;
				budConModifyTask.ModifyTaskCode = ((TextBox)gridViewRow.FindControl("txtOutTaskCode")).Text;
				budConModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtOutTaskContent")).Text;
				budConModifyTask.FeatureDescription = ((TextBox)gridViewRow.FindControl("txtDescription")).Text;
				budConModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtOutUnit")).Text;
				budConModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutQuantity")).Text);
				budConModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text);
				budConModifyTask.MainMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtMainMaterial")).Text));
				budConModifyTask.SubMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtSubMaterial")).Text));
				budConModifyTask.Labor = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtLabor")).Text));
				budConModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutTotal")).Text);
				string value2 = ((TextBox)gridViewRow.FindControl("txtOutStartDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value2))
				{
					budConModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value2));
				}
				string value3 = ((TextBox)gridViewRow.FindControl("txtOutEndDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value3))
				{
					budConModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value3));
				}
				string text = ((TextBox)gridViewRow.FindControl("txtOutConstructionPeriod")).Text;
				if (!string.IsNullOrEmpty(text))
				{
					budConModifyTask.ConstructionPeriod = new int?(System.Convert.ToInt32(text));
				}
				string value4 = ((HiddenField)gridViewRow.FindControl("hfldOutTaskType")).Value;
				budConModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtOutNote")).Text;
				budConModifyTask.ModifyType = new int?(0);
				budConModifyTask.OrderNumber = this.GetModifyOrderNumber(value4, value);
				budConModifyTask.PrjId2 = this.prjId;
				budConModifyTask.ParentId = value;
				this.conModifyTaskSer.Add(budConModifyTask);
			}
		}
	}
	private void SaveInModifyTask()
	{
		foreach (GridViewRow gridViewRow in this.gvInTask.Rows)
		{
			BudConModifyTask budConModifyTask = new BudConModifyTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldInModifyTaskId")).Value;
			string value2 = ((HiddenField)gridViewRow.FindControl("hfldInTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budConModifyTask.ModifyTaskId = value;
				budConModifyTask.ModifyId = this.hfldModifyId.Value;
				budConModifyTask.TaskId = value2;
				budConModifyTask.ModifyTaskCode = string.Empty;
				budConModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtInTaskContent")).Text;
				budConModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtInUnit")).Text;
				budConModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInQuantity")).Text);
				budConModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInUnitPrice")).Text);
				budConModifyTask.MainMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtMainMaterial")).Text));
				budConModifyTask.SubMaterial = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtSubMaterial")).Text));
				budConModifyTask.Labor = new decimal?(System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtLabor")).Text));
				budConModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInTotal")).Text);
				budConModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtInNote")).Text;
				budConModifyTask.ModifyType = new int?(1);
				budConModifyTask.PrjId2 = this.prjId;
				budConModifyTask.ParentId = this.GetParentId(value2);
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
	protected string GetTypeName(string orderNum)
	{
		XpmCodeServices xpmCodeServices = new XpmCodeServices();
		System.Collections.Generic.IList<XpmCode> bySignCode = xpmCodeServices.GetBySignCode("taskType");
		string result = string.Empty;
		foreach (XpmCode current in bySignCode)
		{
			if (current.CodeID == orderNum.Length / 3)
			{
				result = current.CodeName;
				break;
			}
		}
		return result;
	}
}


