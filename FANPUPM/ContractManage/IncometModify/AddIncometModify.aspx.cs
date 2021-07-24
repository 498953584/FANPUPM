using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using com.jwsoft.pm.entpm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometModify_AddIncometModify : NBasePage, IRequiresSessionState
{
	private IncometModifyBll incometModifyBll = new IncometModifyBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private PTPrjInfoBll prjInfoBll = new PTPrjInfoBll();
	public BudConModifyService budModifySev = new BudConModifyService();
	private ConIncometContractService conInConSev = new ConIncometContractService();
	private BudConModifyTaskService conModifyTaskSer = new BudConModifyTaskService();
	private BudContractTaskService conTaskSev = new BudContractTaskService();
	private BudContractConsTaskService consTaskSev = new BudContractConsTaskService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.InitHiddenField();
			this.GvbudModifyBind();
			return;
		}
		this.GVModifyBind();
	}
	public void GvbudModifyBind()
	{
		List<BudConModifyTask> list = (
			from budModT in this.conModifyTaskSer
			join budmf in this.budModifySev on budModT.ModifyId equals budmf.ModifyId
			where budmf.ConInModifyID == this.hfldconModifyID.Value
			select budModT).ToList<BudConModifyTask>();
		this.SaveBudModifyToViewState(list);
		this.GvbudModify.DataSource = list;
		this.GvbudModify.DataBind();
	}
	protected void GVModifyBind()
	{
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		this.GvbudModify.DataSource = budModifyListFromViewState;
		this.GvbudModify.DataBind();
	}
	public void SaveBudModifyToViewState(List<BudConModifyTask> budConTaskLst)
	{
		this.ViewState["budConTaskLst"] = budConTaskLst;
	}
	public List<BudConModifyTask> getBudModifyListFromViewState()
	{
		List<BudConModifyTask> list = this.ViewState["budConTaskLst"] as List<BudConModifyTask>;
		if (list == null)
		{
			list = new List<BudConModifyTask>();
		}
		return list;
	}
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		base.RegisterScript("winclose('AddIncometModify','PayoutModifyEdit.aspx',false)");
	}
	public void btnEdit_Click(object sender, EventArgs e)
	{
		string value = this.hfldMoidyTaskId.Value;
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		foreach (BudConModifyTask current in budModifyListFromViewState)
		{
			if (current.ModifyTaskId == value)
			{
				string value2 = JsonConvert.SerializeObject(current);
				base.Response.Cookies["budConMidifyTaskJson"].Value = value2;
				this.GVModifyBind();
				base.RegisterScript("editBud();");
				break;
			}
		}
	}
	public void hfldBtnMidifyTask_Click(object sender, EventArgs e)
	{
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		string value = base.Request.Cookies["budConMidifyTaskJson"].Value;
		BudConModifyTask budConModifyTask = JsonConvert.DeserializeObject<BudConModifyTask>(value);
		if (budConModifyTask != null)
		{
			BudConModifyTask budConModifyTask2 = null;
			foreach (BudConModifyTask current in budModifyListFromViewState)
			{
				if (current.ModifyTaskId == budConModifyTask.ModifyTaskId)
				{
					budConModifyTask2 = current;
					break;
				}
			}
			if (budConModifyTask2 != null)
			{
				for (int i = 0; i < budModifyListFromViewState.Count; i++)
				{
					if (budModifyListFromViewState[i].ModifyTaskId == budConModifyTask.ModifyTaskId)
					{
						budModifyListFromViewState[i] = budConModifyTask;
						break;
					}
				}
			}
			else
			{
				budModifyListFromViewState.Add(budConModifyTask);
			}
		}
		this.SaveBudModifyToViewState(budModifyListFromViewState);
		this.GVModifyBind();
	}
	public void UpdateDetail()
	{
		BudConModify conModify = this.budModifySev.GetByConInModifyID(this.hfldconModifyID.Value);
		List<BudConModifyTask> list = (
			from r in this.conModifyTaskSer
			where r.ModifyId == conModify.ModifyId
			select r).ToList<BudConModifyTask>();
		foreach (BudConModifyTask modifyTask in list)
		{
			BudContractTask budContractTask = (
				from r in this.conTaskSev
				where r.TaskId == modifyTask.TaskId
				select r).FirstOrDefault<BudContractTask>();
			if (budContractTask.ModifyType == "0")
			{
				this.conTaskSev.Delete(budContractTask);
			}
			else
			{
				budContractTask.EndDate = modifyTask.EndDate;
				budContractTask.StartDate = modifyTask.StartDate;
				budContractTask.TaskCode = modifyTask.ModifyTaskCode;
				budContractTask.TaskName = modifyTask.ModifyTaskContent;
				budContractTask.Unit = modifyTask.Unit;
				budContractTask.Total = new decimal?(Convert.ToDecimal(budContractTask.Total) - modifyTask.Total);
				budContractTask.InputDate = DateTime.Now;
				budContractTask.IsValid = new bool?(false);
				budContractTask.Note = modifyTask.Note;
				budContractTask.OrderNumber = modifyTask.OrderNumber;
				budContractTask.ParentId = modifyTask.ParentId;
				budContractTask.PrjId = modifyTask.PrjId2;
				budContractTask.Quantity = Convert.ToDecimal(budContractTask.Quantity) - modifyTask.Quantity;
				budContractTask.TaskId = modifyTask.TaskId;
				budContractTask.ModifyId = null;
				budContractTask.ModifyType = "1";
				if (budContractTask.Quantity != 0m)
				{
					budContractTask.UnitPrice = budContractTask.Total / budContractTask.Quantity;
				}
				budContractTask.TaskType = string.Empty;
				budContractTask.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budContractTask.FeatureDescription = modifyTask.FeatureDescription;
				budContractTask.ConstructionPeriod = modifyTask.ConstructionPeriod;
				budContractTask.MainMaterial = new decimal?(Convert.ToDecimal(budContractTask.MainMaterial) - Convert.ToDecimal(modifyTask.MainMaterial));
				budContractTask.Labor = new decimal?(Convert.ToDecimal(budContractTask.Labor) - Convert.ToDecimal(modifyTask.Labor));
				budContractTask.SubMaterial = new decimal?(Convert.ToDecimal(budContractTask.SubMaterial) - Convert.ToDecimal(modifyTask.SubMaterial));
				this.conTaskSev.Update(budContractTask);
			}
		}
	}
	public void GvbudModify_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		BudContractTask budContractTask = new BudContractTask();
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.GvbudModify.DataKeys[e.Row.RowIndex]["ModifyTaskId"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			string TaskID = this.GvbudModify.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			e.Row.Attributes["TaskId"] = TaskID;
			string text = this.GvbudModify.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			string text2 = this.GvbudModify.DataKeys[e.Row.RowIndex]["Quantity"].ToString();
			string text3 = this.GvbudModify.DataKeys[e.Row.RowIndex]["Total"].ToString();
			e.Row.Attributes["ModifyType"] = text;
			if (text == "1")
			{
				BudConModify conModify = this.budModifySev.GetByConInModifyID(this.hfldconModifyID.Value);
				BudConModifyTask budConModifyTask = null;
				if (conModify != null)
				{
					budConModifyTask = (
						from r in this.conModifyTaskSer
						where r.ModifyId == conModify.ModifyId && r.TaskId == TaskID
						select r).FirstOrDefault<BudConModifyTask>();
				}
				budContractTask = (
					from r in this.conTaskSev
					where r.TaskId == TaskID
					select r).FirstOrDefault<BudContractTask>();
				if (budContractTask != null)
				{
					e.Row.Cells[2].Text = budContractTask.TaskCode;
					e.Row.Cells[10].Text = budContractTask.UnitPrice.ToString();
					if (!string.IsNullOrEmpty(text2) && Convert.ToDecimal(text2) != 0m)
					{
						e.Row.Cells[9].Style.Add("color", "red");
						e.Row.Cells[11].Style.Add("color", "red");
					}
					if (!string.IsNullOrEmpty(text2))
					{
						if (budConModifyTask != null)
						{
							e.Row.Cells[12].Text = (budContractTask.Quantity - budConModifyTask.Quantity + Convert.ToDecimal(text2)).ToString();
						}
						else
						{
							e.Row.Cells[12].Text = (budContractTask.Quantity + Convert.ToDecimal(text2)).ToString();
						}
					}
					else
					{
						e.Row.Cells[12].Text = budContractTask.Quantity.ToString();
					}
					if (string.IsNullOrEmpty(text3))
					{
						e.Row.Cells[13].Text = budContractTask.Total.ToString();
						return;
					}
					if (budConModifyTask != null)
					{
						e.Row.Cells[13].Text = (budContractTask.Total - budConModifyTask.Total + Convert.ToDecimal(text3)).ToString();
						return;
					}
					e.Row.Cells[13].Text = (budContractTask.Total + Convert.ToDecimal(text3)).ToString();
					return;
				}
			}
			else
			{
				e.Row.Style.Add("color", "red");
				e.Row.Cells[12].Text = text2;
				e.Row.Cells[13].Text = text3;
			}
		}
	}
	protected void InitHiddenField()
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			string text = base.Request["id"];
			this.hfldconModifyID.Value = text;
			BudConModify byConInModifyID = this.budModifySev.GetByConInModifyID(text);
			if (byConInModifyID != null)
			{
				this.hfldconBudModifyID.Value = byConInModifyID.ModifyId;
			}
			else
			{
				this.hfldconBudModifyID.Value = Guid.NewGuid().ToString();
			}
		}
		else
		{
			this.hfldconModifyID.Value = this.hdGuid.Value;
			this.hfldconBudModifyID.Value = Guid.NewGuid().ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			string text2 = base.Request["ContractID"];
			this.hfldConIncometID.Value = text2;
			ConIncometContract byContractId = this.conInConSev.GetByContractId(text2);
			this.hfldPrjGuid.Value = byContractId.Project;
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑收入合同变更";
			IncometModifyModel model = this.incometModifyBll.GetModel(base.Request.QueryString["id"]);
			this.txtChangeCode.Text = model.ChangeCode;
			this.hdChangeCode.Value = model.ChangeCode;
			this.txtChangePrice.Text = model.ChangePrice.ToString();
			this.txtChangeReason.Text = model.ChangeReason;
			this.txtChangeTime.Text = Common2.GetTime(model.ChangeTime.ToString());
			this.txtInputDate.Text = Common2.GetTime(model.InputDate.ToString());
			this.txtInputPerson.Text = model.InputPerson;
			this.txtRemark.Text = model.Remark;
			this.txtTransactor.Text = model.Transactor;
			this.hdGuid.Value = model.ID;
			this.DataBindSheet(model.ID);
		}
		else
		{
			this.lblTitle.Text = "新增收入合同变更";
			this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hdGuid.Value = Guid.NewGuid().ToString();
		}
		IncometContractModel model2 = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
		this.txtContractCode.Text = model2.ContractCode;
		this.txtContractName.Text = model2.ContractName;
		this.txtContractPrice.Text = model2.ContractPrice.ToString();
		this.txtEndPrice.Text = WebUtil.GetEnPrice(model2.ContractPrice.ToString(), model2.ContractID);
		this.txtSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
		if (model2.Project != null)
		{
			this.hfldPrjGuid.Value = model2.Project.PrjGuid.ToString();
		}
		else
		{
			List<string> prjInfoZTBIncoment = this.prjInfoBll.GetPrjInfoZTBIncoment(base.Request.QueryString["ContractID"]);
			if (prjInfoZTBIncoment.Count > 0)
			{
				this.hfldPrjGuid.Value = prjInfoZTBIncoment[4].ToString();
			}
		}
		this.FileLink1.MID = 1907;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		int num = 0;
		if (base.Request.QueryString["id"] != null)
		{
			if (this.hdChangeCode.Value != this.txtChangeCode.Text && this.incometModifyBll.GetListArray(string.Concat(new string[]
			{
				" where ChangeCode='",
				this.txtChangeCode.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("top.ui.alert('变更编号已存在')");
				return;
			}
			num = this.UpdateModel();
		}
		else
		{
			if (this.incometModifyBll.GetListArray(string.Concat(new string[]
			{
				" where ChangeCode='",
				this.txtChangeCode.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("top.ui.alert('变更编号已存在')");
				return;
			}
			num = this.AddModel();
		}
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		decimal num2 = budModifyListFromViewState.Sum((BudConModifyTask conmodify) => conmodify.Total);
		this.hfldTatolAmount.Value = num2.ToString("0.00");
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			try
			{
				this.UpdateDetail();
				List<BudConModifyTask> budModifyListFromViewState2 = this.getBudModifyListFromViewState();
				List<string> list = new List<string>();
				foreach (BudConModifyTask current in budModifyListFromViewState2)
				{
					list.Add(current.TaskId);
				}
				BudConModify budConModify = this.budModifySev.GetByConInModifyID(this.hfldconModifyID.Value);
				if (budConModify != null)
				{
					budConModify.BudAmount = Convert.ToDecimal(this.hfldTatolAmount.Value);
					budConModify.LastModifyDate = DateTime.Now;
					budConModify.LastModifyUser = base.UserCode;
					this.budModifySev.Update(budConModify);
					this.conModifyTaskSer.DelModifyTask(this.hfldconBudModifyID.Value);
				}
				else
				{
					budConModify = this.GetModel(null);
					this.budModifySev.Add(budConModify);
				}
				this.SaveModifyTask();
				this.SaveTask();
				goto IL_2B1;
			}
			catch
			{
				base.RegisterScript("top.ui.alert('编辑失败！')");
				goto IL_2B1;
			}
		}
		try
		{
			BudConModify model = this.GetModel(null);
			this.budModifySev.Add(model);
			this.SaveModifyTask();
			this.SaveTask();
		}
		catch
		{
			base.RegisterScript("top.ui.alert('编辑失败！')");
		}
		IL_2B1:
		if (num != 0)
		{
			if (!string.IsNullOrEmpty(this.hfldSheetId.Value))
			{
				this.UpdateSheet();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("winclose('AddIncometModify','ShowModifyList.aspx?ContractID=" + base.Request.QueryString["ContractID"] + "',true)");
			base.RegisterScript(stringBuilder.ToString());
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.SetMsg() + " 失败！');");
	}
	protected void SaveTask()
	{
		List<BudConModifyTask> list = (
			from r in this.conModifyTaskSer
			where r.ModifyId == this.hfldconBudModifyID.Value
			select r).ToList<BudConModifyTask>();
		for (int i = 0; i < list.Count; i++)
		{
			BudConModifyTask entity = list[i];
			BudContractTask budContractTask = (
				from r in this.conTaskSev
				where r.TaskId == entity.TaskId
				select r).FirstOrDefault<BudContractTask>();
			if (budContractTask != null && entity.ModifyType == 0)
			{
				BudContractTask budContractTask2 = new BudContractTask();
				budContractTask2.EndDate = entity.EndDate;
				budContractTask2.StartDate = entity.StartDate;
				budContractTask2.TaskCode = entity.ModifyTaskCode;
				budContractTask2.TaskName = entity.ModifyTaskContent;
				budContractTask2.Unit = entity.Unit;
				budContractTask2.Total = new decimal?(entity.Total);
				budContractTask2.InputDate = DateTime.Now;
				budContractTask2.IsValid = new bool?(false);
				budContractTask2.Note = entity.Note;
				budContractTask2.OrderNumber = entity.OrderNumber;
				budContractTask2.ParentId = entity.ParentId;
				budContractTask2.PrjId = entity.PrjId2;
				budContractTask2.Quantity = entity.Quantity;
				budContractTask2.TaskId = entity.TaskId;
				budContractTask2.ModifyId = entity.ModifyId;
				budContractTask2.ModifyType = entity.ModifyType.ToString();
				if (budContractTask2.Quantity != 0m)
				{
					budContractTask2.UnitPrice = budContractTask2.Total / budContractTask2.Quantity;
				}
				budContractTask2.TaskType = string.Empty;
				budContractTask2.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budContractTask2.FeatureDescription = entity.FeatureDescription;
				budContractTask2.ConstructionPeriod = entity.ConstructionPeriod;
				budContractTask2.SubMaterial = entity.SubMaterial;
				budContractTask2.MainMaterial = entity.MainMaterial;
				budContractTask2.Labor = entity.Labor;
				this.conTaskSev.Delete(budContractTask);
				this.conTaskSev.Add(budContractTask2);
			}
			if (budContractTask == null && entity.ModifyType == 0)
			{
				BudContractTask budContractTask3 = new BudContractTask();
				budContractTask3.EndDate = entity.EndDate;
				budContractTask3.StartDate = entity.StartDate;
				budContractTask3.TaskCode = entity.ModifyTaskCode;
				budContractTask3.TaskName = entity.ModifyTaskContent;
				budContractTask3.Unit = entity.Unit;
				budContractTask3.Total = new decimal?(entity.Total);
				budContractTask3.InputDate = DateTime.Now;
				budContractTask3.IsValid = new bool?(false);
				budContractTask3.Note = entity.Note;
				budContractTask3.OrderNumber = this.GetModifyOrderNumber(entity.OrderNumber, entity.ParentId);
				budContractTask3.ParentId = entity.ParentId;
				budContractTask3.PrjId = entity.PrjId2;
				budContractTask3.Quantity = entity.Quantity;
				budContractTask3.TaskId = entity.TaskId;
				budContractTask3.ModifyId = entity.ModifyId;
				budContractTask3.ModifyType = entity.ModifyType.ToString();
				if (budContractTask3.Quantity != 0m)
				{
					budContractTask3.UnitPrice = budContractTask3.Total / budContractTask3.Quantity;
				}
				budContractTask3.TaskType = string.Empty;
				budContractTask3.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budContractTask3.FeatureDescription = entity.FeatureDescription;
				budContractTask3.ConstructionPeriod = entity.ConstructionPeriod;
				budContractTask3.SubMaterial = entity.SubMaterial;
				budContractTask3.MainMaterial = entity.MainMaterial;
				budContractTask3.Labor = entity.Labor;
				this.conTaskSev.Add(budContractTask3);
			}
			if (entity.ModifyType == 1)
			{
				budContractTask.EndDate = entity.EndDate;
				budContractTask.StartDate = entity.StartDate;
				budContractTask.Total = new decimal?(Convert.ToDecimal(budContractTask.Total) + entity.Total);
				budContractTask.InputDate = DateTime.Now;
				budContractTask.IsValid = new bool?(false);
				budContractTask.Note = entity.Note;
				budContractTask.OrderNumber = entity.OrderNumber;
				budContractTask.ParentId = entity.ParentId;
				budContractTask.PrjId = entity.PrjId2;
				budContractTask.Quantity = Convert.ToDecimal(budContractTask.Quantity) + entity.Quantity;
				budContractTask.TaskId = entity.TaskId;
				budContractTask.ModifyId = entity.ModifyId;
				budContractTask.ModifyType = entity.ModifyType.ToString();
				if (budContractTask.Quantity != 0m)
				{
					budContractTask.UnitPrice = budContractTask.Total / budContractTask.Quantity;
				}
				budContractTask.TaskType = string.Empty;
				budContractTask.InputUser = PageHelper.QueryUser(this, base.UserCode);
				budContractTask.FeatureDescription = entity.FeatureDescription;
				budContractTask.ConstructionPeriod = entity.ConstructionPeriod;
				budContractTask.MainMaterial = new decimal?(Convert.ToDecimal(budContractTask.MainMaterial) + Convert.ToDecimal(entity.MainMaterial));
				budContractTask.Labor = new decimal?(Convert.ToDecimal(budContractTask.Labor) + Convert.ToDecimal(entity.Labor));
				budContractTask.SubMaterial = new decimal?(Convert.ToDecimal(budContractTask.SubMaterial) + Convert.ToDecimal(entity.SubMaterial));
				this.conTaskSev.Update(budContractTask);
			}
		}
	}
	protected void SaveModifyTask()
	{
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		for (int i = 0; i < budModifyListFromViewState.Count; i++)
		{
			BudConModifyTask budConModifyTask = budModifyListFromViewState[i];
			if (budModifyListFromViewState[i].ModifyType == 0)
			{
				budConModifyTask.OrderNumber = this.GetModifyOrderNumber(budModifyListFromViewState[i].OrderNumber, budModifyListFromViewState[i].ParentId);
				budConModifyTask.TaskId = budModifyListFromViewState[i].TaskId.Substring(0, 37) + budConModifyTask.OrderNumber;
			}
			this.conModifyTaskSer.Add(budConModifyTask);
		}
		List<string> modifyTaskFromViewState = this.GetModifyTaskFromViewState();
		foreach (string current in modifyTaskFromViewState)
		{
			this.DeleteTaskAndMeasure(current);
		}
	}
	public void BtnDel_Click(object sender, EventArgs e)
	{
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldMoidyTaskId.Value);
		foreach (string taskId in listFromJson)
		{
			BudConModifyTask budConModifyTask = (
				from mt in budModifyListFromViewState
				where mt.ModifyTaskId == taskId
				select mt).FirstOrDefault<BudConModifyTask>();
			if (budConModifyTask != null)
			{
				if (this.IsCanDeleteModifyTask(budConModifyTask))
				{
					if (budConModifyTask.ModifyType == 0)
					{
						this.SaveModifyTaskIdToViewState(budConModifyTask);
					}
					budModifyListFromViewState.Remove(budConModifyTask);
				}
				else
				{
					base.RegisterScript("top.ui.alert('该变更节点不能删除，被合同计量或其他节点引用！');");
				}
			}
		}
		this.SaveBudModifyToViewState(budModifyListFromViewState);
		this.GVModifyBind();
	}
	public void SaveModifyTaskIdToViewState(BudConModifyTask modifyTask)
	{
		List<string> modifyTaskFromViewState = this.GetModifyTaskFromViewState();
		string modifyTaskId = modifyTask.ModifyTaskId;
		if (!modifyTaskFromViewState.Contains(modifyTaskId))
		{
			modifyTaskFromViewState.Add(modifyTaskId);
		}
		this.ViewState["ModifyTaskDel"] = modifyTaskFromViewState;
	}
	public List<string> GetModifyTaskFromViewState()
	{
		List<string> list = this.ViewState["ModifyTaskDel"] as List<string>;
		if (list == null)
		{
			list = new List<string>();
		}
		return list;
	}
	public void DeleteTaskAndMeasure(string taskId)
	{
		SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE bud_conModifyTask WHERE TaskId='" + taskId + "'", null);
		SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE Bud_ContractConsTask WHERE TaskId='" + taskId + "'", null);
	}
	public bool IsCanDeleteModifyTask(BudConModifyTask modifyTask)
	{
		if (modifyTask.ModifyType == 1)
		{
			return true;
		}
		List<BudConModifyTask> budModifyListFromViewState = this.getBudModifyListFromViewState();
		BudConModifyTask budConModifyTask = (
			from cm in budModifyListFromViewState
			where cm.TaskId == modifyTask.ModifyTaskId
			select cm).FirstOrDefault<BudConModifyTask>();
		if (budConModifyTask != null)
		{
			return false;
		}
		BudConModifyTask budConModifyTask2 = (
			from cm in this.conModifyTaskSer
			where cm.TaskId == modifyTask.ModifyTaskId
			select cm).FirstOrDefault<BudConModifyTask>();
		if (budConModifyTask2 != null)
		{
			return false;
		}
		BudContractConsTask budContractConsTask = (
			from ct in this.consTaskSev
			where ct.TaskId == modifyTask.ModifyTaskId
			select ct).FirstOrDefault<BudContractConsTask>();
		return budContractConsTask == null;
	}
	private string GetModifyOrderNumber(string orderNumber, string parentId)
	{
		List<BudConModifyTask> list = (
			from mts in this.conModifyTaskSer.AsQueryable<BudConModifyTask>()
			join ms in this.budModifySev.AsQueryable<BudConModify>() on mts.ModifyId equals ms.ModifyId
			where ms.PrjId == this.hfldPrjGuid.Value && mts.OrderNumber == orderNumber
			select mts).ToList<BudConModifyTask>();
		if (list.Count > 0)
		{
			orderNumber = this.conModifyTaskSer.GetOrderNumber(this.hfldPrjGuid.Value, parentId);
		}
		return orderNumber;
	}
	private BudConModify GetModel(BudConModify budConModify)
	{
		if (budConModify == null)
		{
			budConModify = new BudConModify();
			budConModify.InputDate = DateTime.Now;
			budConModify.InputUser = base.UserCode;
			budConModify.Flowstate = 1;
		}
		budConModify.ModifyId = this.hfldconBudModifyID.Value.Trim();
		budConModify.PrjId = this.hfldPrjGuid.Value;
		budConModify.ModifyCode = this.txtChangeCode.Text.Trim();
		budConModify.ModifyContent = this.txtChangeCode.Text.Trim();
		budConModify.ModifyFileCode = string.Empty;
		budConModify.BudAmount = 0m;
		if (!string.IsNullOrEmpty(this.hfldTatolAmount.Value))
		{
			budConModify.BudAmount = Convert.ToDecimal(this.hfldTatolAmount.Value);
		}
		decimal reportAmount = 0m;
		budConModify.ReportAmount = reportAmount;
		decimal approvalAmount = 0m;
		budConModify.ApprovalAmount = approvalAmount;
		budConModify.Note = string.Empty;
		budConModify.LastModifyUser = base.UserCode;
		budConModify.LastModifyDate = DateTime.Now;
		budConModify.ConInModifyID = this.hfldconModifyID.Value;
		return budConModify;
	}
	public int AddModel()
	{
		return this.incometModifyBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.incometModifyBll.Update(this.GetModel());
	}
	public IncometModifyModel GetModel()
	{
		return new IncometModifyModel
		{
			Annex = "",
			ChangeCode = this.txtChangeCode.Text,
			ChangePrice = new decimal?(Convert.ToDecimal(this.txtChangePrice.Text)),
			ChangeReason = this.txtChangeReason.Text,
			ChangeTime = new DateTime?(Convert.ToDateTime(this.txtChangeTime.Text)),
			ContractID = base.Request.QueryString["ContractID"],
			ID = this.hdGuid.Value,
			InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text)),
			InputPerson = this.txtInputPerson.Text,
			Remark = this.txtRemark.Text,
			Transactor = this.txtTransactor.Text
		};
	}
	private void DataBindSheet(string modifyId)
	{
		List<int> list = new List<int>();
		StringBuilder stringBuilder = new StringBuilder();
		DataTable sheet = this.incometModifyBll.GetSheet(modifyId);
		foreach (DataRow dataRow in sheet.Rows)
		{
			int num = Convert.ToInt32(dataRow["TechnologyId"]);
			list.Add(num);
			string sheetNameById = this.incometModifyBll.GetSheetNameById(num);
			stringBuilder.Append(sheetNameById).Append(",");
		}
		ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
		this.hfldSheetId.Value = serializable.Serialize<int[]>(list.ToArray());
		this.txtSheetName.Value = stringBuilder.ToString();
	}
	private void UpdateSheet()
	{
		IList<string> list = new List<string>();
		if (this.hfldSheetId.Value.Contains("["))
		{
			ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldSheetId.Value);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string item = array2[i];
				list.Add(item);
			}
		}
		else
		{
			list.Add(this.hfldSheetId.Value);
		}
		this.incometModifyBll.DeleteSheet(this.hdGuid.Value);
		if (list.Count > 0)
		{
			foreach (string current in list)
			{
				this.incometModifyBll.InsertSheet(this.hdGuid.Value, current);
			}
		}
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
		IList<XpmCode> bySignCode = xpmCodeServices.GetBySignCode("taskType");
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
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
}


