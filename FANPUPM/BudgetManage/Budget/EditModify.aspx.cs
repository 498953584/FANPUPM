using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_EditModify : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private BudModifyService modifySer = new BudModifyService();
	private BudModifyTaskService modifyTaskSer = new BudModifyTaskService();
	private BudModifyTaskResService modifyTaskResSer = new BudModifyTaskResService();
	private string isWBSRelevance = ConfigurationManager.AppSettings["IsWBSRelevance"];

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
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			System.Collections.Generic.List<BudModifyTask> list = new System.Collections.Generic.List<BudModifyTask>();
			System.Collections.Generic.List<BudModifyTask> list2 = new System.Collections.Generic.List<BudModifyTask>();
			if (this.action == "Add")
			{
				this.hfldModifyId.Value = System.Guid.NewGuid().ToString();
			}
			else
			{
				this.hfldModifyId.Value = this.id;
				list = (
					from mt in this.modifyTaskSer
					where mt.ModifyId == this.hfldModifyId.Value && mt.ModifyType.Value == 0
					select mt).ToList<BudModifyTask>();
				list2 = (
					from mt in this.modifyTaskSer
					where mt.ModifyId == this.hfldModifyId.Value && mt.ModifyType.Value == 1
					select mt).ToList<BudModifyTask>();
				this.BindModifyInfo();
				decimal d = 0m;
				d += list.Sum((BudModifyTask omt) => omt.Total);
				d += list2.Sum((BudModifyTask imt) => imt.Total);
				this.txtBudAmount.Value = d.ToString("0.000");
			}
			this.BindGv(list, list2);
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				foreach (BudModifyTask current in list)
				{
					this.BindModifyTaskRes(current.ModifyTaskId);
				}
				using (System.Collections.Generic.List<BudModifyTask>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						BudModifyTask current2 = enumerator2.Current;
						this.BindModifyTaskRes(current2.ModifyTaskId);
					}
					goto IL_444;
				}
			}
			base.Request.Cookies.Remove(this.hfldModifyId.Value);
			System.Collections.Generic.List<BudModifyTaskRes> t = (
				from mtss in this.modifyTaskResSer
				where mtss.ModifyId == this.hfldModifyId.Value
				select mtss).ToList<BudModifyTaskRes>();
			string value = JsonHelper.JsonSerializer<System.Collections.Generic.List<BudModifyTaskRes>>(t);
			HttpCookie httpCookie = new HttpCookie(this.hfldModifyId.Value);
			httpCookie.Value = value;
			base.Response.Cookies.Add(httpCookie);
			IL_444:
			this.hfldPrjId.Value = this.prjId;
			this.uploadModify.RecordCode = this.hfldModifyId.Value;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		int count = (
			from modify in this.modifySer
			where modify.PrjId == this.hfldPrjId.Value.Trim() && modify.ModifyCode == this.txtModifyCode.Text.Trim()
			select modify).ToList<BudModify>().Count;
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
					from modify in this.modifySer
					where modify.ModifyFileCode == this.txtModifyFileCode.Text.Trim()
					select modify).ToList<BudModify>().Count;
				if (count2 > 0)
				{
					base.RegisterScript("top.ui.show('此变更文件编号已经存在!');");
					return;
				}
			}
			try
			{
				BudModify model = this.GetModel(null);
				this.modifySer.Add(model);
				this.SaveOutModifyTask();
				this.SaveInModifyTask();
				if (this.hfldIsWBSRelevance.Value == "0")
				{
					this.SaveModifyTaskResByModifyId(this.hfldModifyId.Value);
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_EditModify' });");
				return;
			}
			catch
			{
				base.RegisterScript("top.ui.show('添加失败!');");
				return;
			}
		}
		BudModify byId = this.modifySer.GetById(this.hfldModifyId.Value);
		if (byId != null && byId.ModifyCode != this.txtModifyCode.Text.Trim() && count > 0)
		{
			base.RegisterScript("top.ui.show('此编码已经存在!');");
			return;
		}
		if (!string.IsNullOrEmpty(this.txtModifyFileCode.Text.Trim()))
		{
			int count3 = (
				from modify in this.modifySer
				where modify.ModifyFileCode == this.txtModifyFileCode.Text.Trim()
				select modify).ToList<BudModify>().Count;
			if (byId.ModifyFileCode != this.txtModifyFileCode.Text.Trim() && count3 > 0)
			{
				base.RegisterScript("top.ui.show('此变更文件编号已经存在!');");
				return;
			}
		}
		try
		{
			this.modifySer.Update(this.GetModel(byId));
			this.modifyTaskSer.DelModifyTask(this.hfldModifyId.Value);
			this.SaveOutModifyTask();
			this.SaveInModifyTask();
			if (this.hfldIsWBSRelevance.Value == "0")
			{
				this.SaveModifyTaskResByModifyId(this.hfldModifyId.Value);
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_EditModify' });");
		}
		catch
		{
			base.RegisterScript("top.ui.show('编辑失败!');");
		}
	}
	protected void btnDelOut_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
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
				foreach (BudModifyTask current2 in outModifyTaskGV)
				{
					if (current2.ModifyTaskId == current)
					{
						outModifyTaskGV.Remove(current2);
						base.Request.Cookies.Remove(current);
						break;
					}
				}
			}
			this.BindGv(outModifyTaskGV, inModifyTaskGV);
			base.RegisterScript("clickWBSType('out');");
			System.Collections.Generic.List<BudModifyTask> outModifyTaskGV2 = this.GetOutModifyTaskGV();
			System.Collections.Generic.List<BudModifyTask> inModifyTaskGV2 = this.GetInModifyTaskGV();
			decimal d = 0m;
			d += outModifyTaskGV2.Sum((BudModifyTask omt) => omt.Total);
			d += inModifyTaskGV2.Sum((BudModifyTask imt) => imt.Total);
			this.txtBudAmount.Value = d.ToString("0.000");
		}
		catch
		{
			base.RegisterScript("clickWBSType('out');top.ui.alert('删除失败！');");
		}
	}
	protected void btnDelIn_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
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
				foreach (BudModifyTask current2 in inModifyTaskGV)
				{
					if (current2.ModifyTaskId == current)
					{
						inModifyTaskGV.Remove(current2);
						base.Request.Cookies.Remove(current);
						break;
					}
				}
			}
			this.BindGv(outModifyTaskGV, inModifyTaskGV);
			base.RegisterScript("clickWBSType('in');");
			System.Collections.Generic.List<BudModifyTask> outModifyTaskGV2 = this.GetOutModifyTaskGV();
			System.Collections.Generic.List<BudModifyTask> inModifyTaskGV2 = this.GetInModifyTaskGV();
			decimal d = 0m;
			d += outModifyTaskGV2.Sum((BudModifyTask omt) => omt.Total);
			d += inModifyTaskGV2.Sum((BudModifyTask imt) => imt.Total);
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
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				TextBox textBox = (TextBox)e.Row.FindControl("txtOutUnitPrice");
				textBox.Attributes.Add("readonly", "readonly");
				textBox.Style.Add("background-color", "#F5F5F5");
				TextBox textBox2 = (TextBox)e.Row.FindControl("txtOutTotal");
				textBox2.Attributes.Add("readonly", "readonly");
				textBox2.Style.Add("background-color", "#F5F5F5");
				TextBox textBox3 = (TextBox)e.Row.FindControl("txtOutQuantity");
				textBox3.Attributes.Add("onBlur", "setValue('" + text + "')");
				return;
			}
			TextBox textBox4 = (TextBox)e.Row.FindControl("txtOutTotal");
			textBox4.Attributes.Add("readonly", "readonly");
			textBox4.Style.Add("background-color", "#F5F5F5");
			TextBox textBox5 = (TextBox)e.Row.FindControl("txtOutUnitPrice");
			TextBox textBox6 = (TextBox)e.Row.FindControl("txtOutQuantity");
			textBox5.Attributes.Add("onBlur", "setValue('" + text + "')");
			textBox6.Attributes.Add("onBlur", "setValue('" + text + "')");
		}
	}
	protected void gvInTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvInTask.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				TextBox textBox = (TextBox)e.Row.FindControl("txtInUnitPrice");
				textBox.Attributes.Add("readonly", "readonly");
				textBox.Style.Add("background-color", "#F5F5F5");
				TextBox textBox2 = (TextBox)e.Row.FindControl("txtInTotal");
				textBox2.Attributes.Add("readonly", "readonly");
				textBox2.Style.Add("background-color", "#F5F5F5");
				TextBox textBox3 = (TextBox)e.Row.FindControl("txtInQuantity");
				textBox3.Attributes.Add("onBlur", "setValue('" + text + "')");
				return;
			}
			TextBox textBox4 = (TextBox)e.Row.FindControl("txtInTotal");
			textBox4.Attributes.Add("readonly", "readonly");
			textBox4.Style.Add("background-color", "#F5F5F5");
			TextBox textBox5 = (TextBox)e.Row.FindControl("txtInUnitPrice");
			TextBox textBox6 = (TextBox)e.Row.FindControl("txtInQuantity");
			textBox5.Attributes.Add("onBlur", "setValue('" + text + "')");
			textBox6.Attributes.Add("onBlur", "setValue('" + text + "')");
		}
	}
	protected void btnAddOut_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		BudModifyTask outEmptyModifyTask = this.GetOutEmptyModifyTask();
		outModifyTaskGV.Add(outEmptyModifyTask);
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
		this.BindGv(outModifyTaskGV, inModifyTaskGV);
		base.RegisterScript("clickWBSType('out')");
	}
	protected void btnAddIn_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
		BudModifyTask inEmptyModifyTask = this.GetInEmptyModifyTask();
		inModifyTaskGV.Add(inEmptyModifyTask);
		this.BindGv(outModifyTaskGV, inModifyTaskGV);
		base.RegisterScript("clickWBSType('in')");
	}
	protected void btnBindRes_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
		this.BindGv(outModifyTaskGV, inModifyTaskGV);
		string text = string.Empty;
		if (this.hfldSpType.Value == "out")
		{
			text = this.hfldCheckedTaskIdOut.Value;
		}
		else
		{
			text = this.hfldCheckedTaskIdIn.Value;
		}
		HttpCookie httpCookie = base.Request.Cookies[text];
		System.Collections.Generic.List<BudModifyTaskRes> list = new System.Collections.Generic.List<BudModifyTaskRes>();
		if (httpCookie != null)
		{
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<System.Collections.Generic.List<BudModifyTaskRes>>(value);
			}
		}
		decimal d = 0m;
		foreach (BudModifyTaskRes current in list)
		{
			d += current.ResourcePrice * current.ResourceQuantity;
		}
		if (this.hfldSpType.Value == "out")
		{
			System.Collections.IEnumerator enumerator2 = this.gvOutTask.Rows.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					GridViewRow gridViewRow = (GridViewRow)enumerator2.Current;
					string value2 = ((HiddenField)gridViewRow.FindControl("hfldOutModifyTaskId")).Value;
					if (!string.IsNullOrEmpty(value2) && value2 == text)
					{
						((TextBox)gridViewRow.FindControl("txtOutTotal")).Text = d.ToString("0.000");
						string value3 = ((TextBox)gridViewRow.FindControl("txtOutQuantity")).Text.Trim();
						if (string.IsNullOrEmpty(value3))
						{
							break;
						}
						decimal num = System.Convert.ToDecimal(value3);
						if (num != 0m)
						{
							((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text = (d / num).ToString("0.000");
							break;
						}
						((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text = "0.000";
						break;
					}
				}
				goto IL_34C;
			}
			finally
			{
				System.IDisposable disposable = enumerator2 as System.IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		foreach (GridViewRow gridViewRow2 in this.gvInTask.Rows)
		{
			string value4 = ((HiddenField)gridViewRow2.FindControl("hfldInModifyTaskId")).Value;
			if (value4 == text)
			{
				((TextBox)gridViewRow2.FindControl("txtInTotal")).Text = d.ToString("0.000");
				string value5 = ((TextBox)gridViewRow2.FindControl("txtInQuantity")).Text.Trim();
				if (string.IsNullOrEmpty(value5))
				{
					((TextBox)gridViewRow2.FindControl("txtInUnitPrice")).Text = "0.000";
					break;
				}
				decimal num2 = System.Convert.ToDecimal(value5);
				if (num2 != 0m)
				{
					((TextBox)gridViewRow2.FindControl("txtInUnitPrice")).Text = (d / num2).ToString("0.000");
					break;
				}
				((TextBox)gridViewRow2.FindControl("txtInUnitPrice")).Text = "0.000";
				break;
			}
		}
		IL_34C:
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV2 = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV2 = this.GetInModifyTaskGV();
		decimal d2 = 0m;
		d2 += outModifyTaskGV2.Sum((BudModifyTask omt) => omt.Total);
		d2 += inModifyTaskGV2.Sum((BudModifyTask imt) => imt.Total);
		this.txtBudAmount.Value = d2.ToString("0.000");
	}
	protected void btnCancel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> outModifyTaskGV = this.GetOutModifyTaskGV();
		System.Collections.Generic.List<BudModifyTask> inModifyTaskGV = this.GetInModifyTaskGV();
		foreach (BudModifyTask current in outModifyTaskGV)
		{
			base.Request.Cookies.Remove(current.ModifyTaskId);
		}
		foreach (BudModifyTask current2 in inModifyTaskGV)
		{
			base.Request.Cookies.Remove(current2.ModifyTaskId);
		}
		base.RegisterScript("winclose('EditModify', 'BudModifyList.aspx', false);");
	}
	private BudModify GetModel(BudModify budModify)
	{
		if (budModify == null)
		{
			budModify = new BudModify();
			budModify.InputDate = System.DateTime.Now;
			budModify.InputUser = base.UserCode;
			budModify.Flowstate = -1;
		}
		budModify.ModifyId = this.hfldModifyId.Value.Trim();
		budModify.PrjId = this.prjId;
		budModify.ModifyCode = this.txtModifyCode.Text.Trim();
		budModify.ModifyContent = this.txtModifyContent.Text.Trim();
		budModify.ModifyFileCode = this.txtModifyFileCode.Text.Trim();
		budModify.BudAmount = 0m;
		decimal reportAmount = 0m;
		if (!string.IsNullOrEmpty(this.txtReportAmount.Text.Trim()))
		{
			reportAmount = System.Convert.ToDecimal(this.txtReportAmount.Text.Trim());
		}
		budModify.ReportAmount = reportAmount;
		decimal approvalAmount = 0m;
		if (!string.IsNullOrEmpty(this.txtApprovalAmount.Text.Trim()))
		{
			approvalAmount = System.Convert.ToDecimal(this.txtApprovalAmount.Text.Trim());
		}
		budModify.ApprovalAmount = approvalAmount;
		if (!string.IsNullOrEmpty(this.txtApprovalDate.Text.Trim()))
		{
			budModify.ApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text.Trim()));
		}
		budModify.Note = this.txtNotes.Text.Trim();
		budModify.LastModifyUser = base.UserCode;
		budModify.LastModifyDate = System.DateTime.Now;
		return budModify;
	}
	private void BindModifyInfo()
	{
		BudModify byId = this.modifySer.GetById(this.hfldModifyId.Value);
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
	private System.Collections.Generic.List<BudModifyTask> GetOutModifyTaskGV()
	{
		System.Collections.Generic.List<BudModifyTask> list = new System.Collections.Generic.List<BudModifyTask>();
		foreach (GridViewRow gridViewRow in this.gvOutTask.Rows)
		{
			BudModifyTask budModifyTask = new BudModifyTask();
			budModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldOutModifyTaskId")).Value;
			budModifyTask.ModifyId = this.hfldModifyId.Value;
			budModifyTask.TaskId = ((HiddenField)gridViewRow.FindControl("hfldOutTaskId")).Value;
			budModifyTask.ModifyTaskCode = ((TextBox)gridViewRow.FindControl("txtOutTaskCode")).Text;
			budModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtOutTaskContent")).Text;
			budModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtOutUnit")).Text;
			budModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutQuantity")).Text);
			budModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text);
			budModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutTotal")).Text);
			string value = ((TextBox)gridViewRow.FindControl("txtOutStartDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				budModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value));
			}
			string value2 = ((TextBox)gridViewRow.FindControl("txtOutEndDate")).Text.ToString();
			if (!string.IsNullOrEmpty(value2))
			{
				budModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value2));
			}
			string text = ((TextBox)gridViewRow.FindControl("txtOutConstructionPeriod")).Text;
			if (!string.IsNullOrEmpty(text))
			{
				budModifyTask.ConstructionPeriod = new int?(System.Convert.ToInt32(text));
			}
			budModifyTask.OrderNumber = ((HiddenField)gridViewRow.FindControl("hfldOutTaskType")).Value;
			budModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtOutNote")).Text;
			budModifyTask.ModifyType = new int?(0);
			budModifyTask.FeatureDescription = ((TextBox)gridViewRow.FindControl("txtOutDescription")).Text;
			list.Add(budModifyTask);
		}
		return list;
	}
	private System.Collections.Generic.List<BudModifyTask> GetInModifyTaskGV()
	{
		System.Collections.Generic.List<BudModifyTask> list = new System.Collections.Generic.List<BudModifyTask>();
		foreach (GridViewRow gridViewRow in this.gvInTask.Rows)
		{
			list.Add(new BudModifyTask
			{
				ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldInModifyTaskId")).Value,
				ModifyId = this.hfldModifyId.Value,
				TaskId = ((HiddenField)gridViewRow.FindControl("hfldInTaskId")).Value,
				ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtInTaskContent")).Text,
				Unit = ((TextBox)gridViewRow.FindControl("txtInUnit")).Text,
				Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInQuantity")).Text),
				UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInUnitPrice")).Text),
				Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInTotal")).Text),
				Note = ((TextBox)gridViewRow.FindControl("txtInNote")).Text,
				ModifyType = new int?(1),
				FeatureDescription = ((TextBox)gridViewRow.FindControl("txtInDescription")).Text
			});
		}
		return list;
	}
	private BudModifyTask GetOutEmptyModifyTask()
	{
		return new BudModifyTask
		{
			ModifyTaskId = System.Guid.NewGuid().ToString(),
			ModifyId = this.id,
			TaskId = string.Empty,
			ModifyTaskCode = string.Empty,
			ModifyTaskContent = string.Empty,
			Quantity = 0m,
			OrderNumber = string.Empty,
			UnitPrice = 0m,
			Total = 0m,
			StartDate = null,
			EndDate = null,
			ConstructionPeriod = null,
			ModifyType = null,
			Note = string.Empty
		};
	}
	private BudModifyTask GetInEmptyModifyTask()
	{
		return new BudModifyTask
		{
			ModifyTaskId = System.Guid.NewGuid().ToString(),
			ModifyId = this.id,
			TaskId = string.Empty,
			ModifyTaskCode = string.Empty,
			ModifyTaskContent = string.Empty,
			Quantity = 0m,
			OrderNumber = string.Empty,
			UnitPrice = 0m,
			Total = 0m,
			ModifyType = null,
			Note = string.Empty
		};
	}
	private void BindGv(System.Collections.Generic.List<BudModifyTask> listOutModifyTask, System.Collections.Generic.List<BudModifyTask> listInModifyTask)
	{
		this.gvOutTask.DataSource = listOutModifyTask;
		this.gvOutTask.DataBind();
		this.gvInTask.DataSource = listInModifyTask;
		this.gvInTask.DataBind();
	}
	private void BindModifyTaskRes(string modifyTaskId)
	{
		base.Request.Cookies.Remove(modifyTaskId);
		System.Collections.Generic.List<BudModifyTaskRes> t = (
			from mtss in this.modifyTaskResSer
			where mtss.ModifyTaskId == modifyTaskId
			select mtss).ToList<BudModifyTaskRes>();
		string value = JsonHelper.JsonSerializer<System.Collections.Generic.List<BudModifyTaskRes>>(t);
		HttpCookie httpCookie = new HttpCookie(modifyTaskId);
		httpCookie.Value = value;
		base.Response.Cookies.Add(httpCookie);
	}
	private void SaveOutModifyTask()
	{
		foreach (GridViewRow gridViewRow in this.gvOutTask.Rows)
		{
			BudModifyTask budModifyTask = new BudModifyTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldOutTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budModifyTask.ModifyTaskId = ((HiddenField)gridViewRow.FindControl("hfldOutModifyTaskId")).Value;
				budModifyTask.ModifyId = this.hfldModifyId.Value;
				budModifyTask.TaskId = value;
				budModifyTask.ParentId = value;
				budModifyTask.ModifyTaskCode = ((TextBox)gridViewRow.FindControl("txtOutTaskCode")).Text;
				budModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtOutTaskContent")).Text;
				budModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtOutUnit")).Text;
				budModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutQuantity")).Text);
				budModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutUnitPrice")).Text);
				budModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtOutTotal")).Text);
				string value2 = ((TextBox)gridViewRow.FindControl("txtOutStartDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value2))
				{
					budModifyTask.StartDate = new System.DateTime?(System.Convert.ToDateTime(value2));
				}
				string value3 = ((TextBox)gridViewRow.FindControl("txtOutEndDate")).Text.ToString();
				if (!string.IsNullOrEmpty(value3))
				{
					budModifyTask.EndDate = new System.DateTime?(System.Convert.ToDateTime(value3));
				}
				string text = ((TextBox)gridViewRow.FindControl("txtOutConstructionPeriod")).Text;
				if (!string.IsNullOrEmpty(text))
				{
					budModifyTask.ConstructionPeriod = new int?(System.Convert.ToInt32(text));
				}
				string value4 = ((HiddenField)gridViewRow.FindControl("hfldOutTaskType")).Value;
				budModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtOutNote")).Text;
				budModifyTask.ModifyType = new int?(0);
				budModifyTask.OrderNumber = this.GetModifyOrderNumber(value4, value);
				budModifyTask.FeatureDescription = ((TextBox)gridViewRow.FindControl("txtOutDescription")).Text;
				budModifyTask.PrjId2 = this.prjId;
				this.modifyTaskSer.Add(budModifyTask);
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					this.SaveModifyTaskRes(budModifyTask.ModifyTaskId);
					this.modifyTaskSer.UpdateTotal2(budModifyTask.ModifyTaskId);
				}
			}
		}
	}
	private void SaveInModifyTask()
	{
		foreach (GridViewRow gridViewRow in this.gvInTask.Rows)
		{
			BudModifyTask budModifyTask = new BudModifyTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldInModifyTaskId")).Value;
			string value2 = ((HiddenField)gridViewRow.FindControl("hfldInTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budModifyTask.ModifyTaskId = value;
				budModifyTask.ModifyId = this.hfldModifyId.Value;
				budModifyTask.TaskId = value2;
				budModifyTask.ParentId = this.GetParentId(value2);
				budModifyTask.ModifyTaskCode = ((TextBox)gridViewRow.FindControl("txtInTaskName")).Text;
				budModifyTask.ModifyTaskContent = ((TextBox)gridViewRow.FindControl("txtInTaskContent")).Text;
				budModifyTask.Unit = ((TextBox)gridViewRow.FindControl("txtInUnit")).Text;
				budModifyTask.Quantity = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInQuantity")).Text);
				budModifyTask.UnitPrice = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInUnitPrice")).Text);
				budModifyTask.Total = System.Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtInTotal")).Text);
				budModifyTask.Total2 = new decimal?(budModifyTask.Quantity * budModifyTask.UnitPrice);
				budModifyTask.Note = ((TextBox)gridViewRow.FindControl("txtInNote")).Text;
				budModifyTask.ModifyType = new int?(1);
				budModifyTask.FeatureDescription = ((TextBox)gridViewRow.FindControl("txtInDescription")).Text;
				budModifyTask.PrjId2 = this.prjId;
				string value3 = ((HiddenField)gridViewRow.FindControl("hfldInTaskType")).Value;
				budModifyTask.OrderNumber = this.GetModifyOrderNumber(value3, value2);
				this.modifyTaskSer.Add(budModifyTask);
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					this.SaveModifyTaskRes(budModifyTask.ModifyTaskId);
					this.modifyTaskSer.UpdateTotal2(budModifyTask.ModifyTaskId);
				}
			}
		}
	}
	private string GetModifyOrderNumber(string orderNumber, string parentId)
	{
		System.Collections.Generic.List<BudModifyTask> list = (
			from mts in this.modifyTaskSer.AsQueryable<BudModifyTask>()
			join ms in this.modifySer.AsQueryable<BudModify>() on mts.ModifyId equals ms.ModifyId
			where ms.PrjId == this.hfldPrjId.Value && mts.OrderNumber == orderNumber
			select mts).ToList<BudModifyTask>();
		if (list.Count > 0)
		{
			orderNumber = this.modifyTaskSer.GetOrderNumber(this.prjId, parentId);
		}
		return orderNumber;
	}
	private void SaveModifyTaskRes(string modifyTaskId)
	{
		this.modifyTaskResSer.DelByModifyTaskId(modifyTaskId);
		HttpCookie httpCookie = base.Request.Cookies[modifyTaskId];
		System.Collections.Generic.List<BudModifyTaskRes> list = new System.Collections.Generic.List<BudModifyTaskRes>();
		if (httpCookie != null)
		{
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<System.Collections.Generic.List<BudModifyTaskRes>>(value);
			}
			foreach (BudModifyTaskRes current in list)
			{
				this.modifyTaskResSer.Add(current);
			}
		}
		base.Request.Cookies.Remove(modifyTaskId);
	}
	private void SaveModifyTaskResByModifyId(string modifyId)
	{
		this.modifyTaskResSer.DelByModifyId(modifyId);
		HttpCookie httpCookie = base.Request.Cookies[modifyId];
		System.Collections.Generic.List<BudModifyTaskRes> list = new System.Collections.Generic.List<BudModifyTaskRes>();
		if (httpCookie != null)
		{
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<System.Collections.Generic.List<BudModifyTaskRes>>(value);
			}
			foreach (BudModifyTaskRes current in list)
			{
				this.modifyTaskResSer.Add(current);
			}
		}
		base.Request.Cookies.Remove(modifyId);
	}
	private string GetParentId(string taskId)
	{
		BudTaskService budTaskService = new BudTaskService();
		cn.justwin.Domain.Entities.BudTask byId = budTaskService.GetById(taskId);
		return byId.ParentId;
	}
	protected string GetTaskCode(string taskId)
	{
		string result = string.Empty;
		cn.justwin.Domain.BudTask byId = cn.justwin.Domain.BudTask.GetById(taskId);
		if (byId != null)
		{
			result = byId.Code;
		}
		else
		{
			BudModifyTask byId2 = this.modifyTaskSer.GetById(taskId);
			if (byId2 != null)
			{
				result = byId2.ModifyTaskCode;
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


