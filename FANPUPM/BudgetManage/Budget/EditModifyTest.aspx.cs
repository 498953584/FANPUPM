using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_EditModifyTest : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private BudModifyService modifySer = new BudModifyService();
	private BudModifyTaskService modifyTaskSer = new BudModifyTaskService();
	private BudModifyTaskResService modifyTaskResSer = new BudModifyTaskResService();
	private BudTaskService budTaskSer = new BudTaskService();
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
			if (this.action == "Add")
			{
				this.hfldModifyId.Value = System.Guid.NewGuid().ToString();
			}
			else
			{
				this.hfldModifyId.Value = this.id;
				list = (
					from mt in this.modifyTaskSer
					where mt.ModifyId == this.hfldModifyId.Value
					select mt).ToList<BudModifyTask>();
				this.BindModifyInfo();
				decimal d = 0m;
				d += list.Sum((BudModifyTask imt) => imt.Total);
				this.txtBudAmount.Value = d.ToString("0.000");
				this.hfldAllModifyTaskJson.Value = JsonNetWrap.SerializeObject(list);
			}
			this.BindGv(list);
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				using (System.Collections.Generic.List<BudModifyTask>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BudModifyTask current = enumerator.Current;
						this.BindModifyTaskRes(current.ModifyTaskId);
					}
					goto IL_2AE;
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
			IL_2AE:
			this.hfldPrjId.Value = this.prjId;
			this.uploadModify.RecordCode = this.hfldModifyId.Value;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> modifyTaskGV = this.GetModifyTaskGV();
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
				foreach (BudModifyTask budModifyTask in modifyTaskGV)
				{
					if (budModifyTask.ModifyTaskId == current)
					{
						modifyTaskGV.Remove(budModifyTask);
						base.Request.Cookies.Remove(current);
						System.Collections.Generic.List<BudModifyTaskRes> list2 = (
							from r in this.modifyTaskResSer
							where r.ModifyTaskId == budModifyTask.ModifyTaskId
							select r).ToList<BudModifyTaskRes>();
						using (System.Collections.Generic.List<BudModifyTaskRes>.Enumerator enumerator3 = list2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								BudModifyTaskRes current2 = enumerator3.Current;
								this.modifyTaskResSer.Delete(current2);
							}
							break;
						}
					}
				}
			}
			this.BindGv(modifyTaskGV);
			this.hfldAllModifyTaskJson.Value = JsonNetWrap.SerializeObject(modifyTaskGV);
			System.Collections.Generic.List<BudModifyTask> modifyTaskGV2 = this.GetModifyTaskGV();
			decimal d = 0m;
			d += modifyTaskGV2.Sum((BudModifyTask imt) => imt.Total);
			this.txtBudAmount.Value = d.ToString("0.000");
		}
		catch
		{
			base.RegisterScript("top.ui.alert('删除失败！');");
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
				this.SaveModifyTask();
				this.SaveTask();
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
			this.SaveModifyTask();
			this.SaveTask();
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
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["ModifyTaskId"].ToString();
			e.Row.Attributes["targetId"] = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["layer"] = num.ToString();
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			e.Row.Attributes["modifyType"] = text2;
			if (text2 == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
	}
	protected void btnBindRes_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> modifyTaskGV = this.GetModifyTaskGV();
		string text = string.Empty;
		text = this.hfldEditModifyTaskId.Value;
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
		decimal num = 0m;
		foreach (BudModifyTaskRes current in list)
		{
			num += current.ResourcePrice * current.ResourceQuantity;
		}
		int i = 0;
		while (i < this.gvBudget.Rows.Count)
		{
			string value2 = ((HiddenField)this.gvBudget.Rows[i].FindControl("hfldModifyTaskId")).Value;
			if (!string.IsNullOrEmpty(value2) && value2 == text)
			{
				((Label)this.gvBudget.Rows[i].FindControl("lblTotal")).Text = num.ToString("0.000");
				modifyTaskGV[i].Total = num;
				modifyTaskGV[i].Total2 = new decimal?(num);
				string value3 = ((Label)this.gvBudget.Rows[i].FindControl("lblQuantity")).Text.Trim();
				if (string.IsNullOrEmpty(value3))
				{
					break;
				}
				decimal num2 = System.Convert.ToDecimal(value3);
				modifyTaskGV[i].Quantity = num2;
				if (num2 != 0m)
				{
					((Label)this.gvBudget.Rows[i].FindControl("lblUnitPrice")).Text = (num / num2).ToString("0.000");
					modifyTaskGV[i].UnitPrice = num / num2;
					break;
				}
				((Label)this.gvBudget.Rows[i].FindControl("lblUnitPrice")).Text = "0.000";
				modifyTaskGV[i].UnitPrice = 0m;
				break;
			}
			else
			{
				i++;
			}
		}
		decimal d = 0m;
		d += modifyTaskGV.Sum((BudModifyTask imt) => imt.Total);
		this.txtBudAmount.Value = d.ToString("0.000");
		this.BindGv(modifyTaskGV);
		this.hfldAllModifyTaskJson.Value = JsonNetWrap.SerializeObject(modifyTaskGV);
	}
	protected void btnCancel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<BudModifyTask> modifyTaskGV = this.GetModifyTaskGV();
		foreach (BudModifyTask current in modifyTaskGV)
		{
			base.Request.Cookies.Remove(current.ModifyTaskId);
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
	private System.Collections.Generic.List<BudModifyTask> GetModifyTaskGV()
	{
		System.Collections.Generic.List<BudModifyTask> list = new System.Collections.Generic.List<BudModifyTask>();
		return JsonNetWrap.DeserializeObject<System.Collections.Generic.List<BudModifyTask>>(this.hfldAllModifyTaskJson.Value);
	}
	private void BindGv(System.Collections.Generic.List<BudModifyTask> listModifyTask)
	{
		this.gvBudget.DataSource = listModifyTask;
		this.gvBudget.DataBind();
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
	private void SaveModifyTask()
	{
		System.Collections.Generic.List<BudModifyTask> modifyTaskGV = this.GetModifyTaskGV();
		for (int i = 0; i < this.gvBudget.Rows.Count; i++)
		{
			BudModifyTask budModifyTask = new BudModifyTask();
			string value = ((HiddenField)this.gvBudget.Rows[i].FindControl("hfldModifyTaskId")).Value;
			string value2 = ((HiddenField)this.gvBudget.Rows[i].FindControl("hfldTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budModifyTask.ModifyTaskId = value;
				budModifyTask.ModifyId = this.hfldModifyId.Value;
				budModifyTask.ModifyTaskCode = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyTaskCode")).Text;
				budModifyTask.ModifyTaskContent = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyTaskContent")).Text;
				budModifyTask.Unit = ((Label)this.gvBudget.Rows[i].FindControl("lblUnit")).Text;
				budModifyTask.Quantity = System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblQuantity")).Text);
				budModifyTask.UnitPrice = System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblUnitPrice")).Text);
				budModifyTask.Total = System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblTotal")).Text);
				string text = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyType")).Text;
				string value3 = ((HiddenField)this.gvBudget.Rows[i].FindControl("hfldParentId")).Value;
				string orderNumber = modifyTaskGV[i].OrderNumber;
				budModifyTask.OrderNumber = this.GetModifyOrderNumber(orderNumber, value3);
				if (text == "清单内")
				{
					budModifyTask.TaskId = value2;
					budModifyTask.Total2 = new decimal?(budModifyTask.Quantity * budModifyTask.UnitPrice);
					budModifyTask.ModifyType = new int?(1);
					budModifyTask.ParentId = this.GetParentId(value2);
				}
				else
				{
					budModifyTask.TaskId = value3 + "-" + budModifyTask.OrderNumber;
					budModifyTask.Total2 = new decimal?(budModifyTask.Total);
					budModifyTask.ModifyType = new int?(0);
					budModifyTask.ParentId = value3;
				}
				budModifyTask.PrjId2 = this.prjId;
				budModifyTask.StartDate = modifyTaskGV[i].StartDate;
				budModifyTask.EndDate = modifyTaskGV[i].EndDate;
				budModifyTask.ConstructionPeriod = modifyTaskGV[i].ConstructionPeriod;
				budModifyTask.FeatureDescription = modifyTaskGV[i].FeatureDescription;
				budModifyTask.Note = ((Label)this.gvBudget.Rows[i].FindControl("hlinkShowNote")).Text;
				this.modifyTaskSer.Add(budModifyTask);
				if (this.hfldIsWBSRelevance.Value == "1")
				{
					this.SaveModifyTaskRes(budModifyTask.ModifyTaskId);
				}
			}
		}
	}
	private void SaveTask()
	{
		System.Collections.Generic.List<BudModifyTask> list = (
			from r in this.modifyTaskSer
			where r.ModifyId == this.hfldModifyId.Value
			select r).ToList<BudModifyTask>();
		for (int i = 0; i < this.gvBudget.Rows.Count; i++)
		{
			string taskId = list[i].TaskId;
			cn.justwin.Domain.Entities.BudTask budTask = (
				from p in this.budTaskSer
				where p.TaskId == taskId && p.ModifyId == this.hfldModifyId.Value
				select p).FirstOrDefault<cn.justwin.Domain.Entities.BudTask>();
			BudModifyTask budModifyTask = (
				from p in this.modifyTaskSer
				where p.TaskId == taskId
				select p).FirstOrDefault<BudModifyTask>();
			if (budTask != null)
			{
				budTask.Total2 = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblTotal")).Text));
				budTask.Quantity = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblQuantity")).Text));
				budTask.UnitPrice = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblUnitPrice")).Text));
				this.budTaskSer.Update(budTask);
			}
			if (budTask == null && budModifyTask.ModifyType == 0)
			{
				budTask = new cn.justwin.Domain.Entities.BudTask();
				budTask.ModifyId = this.hfldModifyId.Value;
				budTask.TaskCode = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyTaskCode")).Text;
				budTask.TaskName = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyTaskContent")).Text;
				budTask.Unit = ((Label)this.gvBudget.Rows[i].FindControl("lblUnit")).Text;
				budTask.Quantity = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblQuantity")).Text));
				budTask.UnitPrice = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblUnitPrice")).Text));
				budTask.Total2 = new decimal?(System.Convert.ToDecimal(((Label)this.gvBudget.Rows[i].FindControl("lblTotal")).Text));
				string text = ((Label)this.gvBudget.Rows[i].FindControl("lblModifyType")).Text;
				string value = ((HiddenField)this.gvBudget.Rows[i].FindControl("hfldParentId")).Value;
				budTask.ParentId = value;
				if (text == "清单内")
				{
					budTask.TaskId = taskId;
					budTask.ModifyType = "1";
				}
				else
				{
					budTask.TaskId = value + "-" + budModifyTask.OrderNumber;
					budTask.ModifyType = "0";
				}
				budTask.PrjId = this.prjId;
				budTask.StartDate = list[i].StartDate;
				budTask.EndDate = list[i].EndDate;
				budTask.ConstructionPeriod = list[i].ConstructionPeriod;
				budTask.FeatureDescription = list[i].FeatureDescription;
				budTask.Note = ((Label)this.gvBudget.Rows[i].FindControl("hlinkShowNote")).Text;
				budTask.OrderNumber = budModifyTask.OrderNumber;
				budTask.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budTask.InputDate = System.DateTime.Now;
				budTask.TaskType = string.Empty;
				budTask.Version = new int?(1);
				budTask.IsValid = new bool?(false);
				this.budTaskSer.Add(budTask);
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
	private void BindBudget()
	{
		System.Collections.Generic.List<BudModifyTask> dataSource = JsonNetWrap.DeserializeObject<System.Collections.Generic.List<BudModifyTask>>(this.hfldAllModifyTaskJson.Value);
		this.gvBudget.DataSource = dataSource;
		this.gvBudget.DataBind();
	}
	protected void btnRefresh_Click(object sender, System.EventArgs e)
	{
		this.BindBudget();
	}
}


