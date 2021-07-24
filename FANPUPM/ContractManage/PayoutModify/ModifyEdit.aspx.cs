using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutModify_ModifyEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private string modifyId = string.Empty;
	private string modifyStockAction = string.Empty;
	private readonly Purchase purchase = new Purchase();
	private PayoutModify payoutModify = new PayoutModify();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private readonly ModifyStockBll modifyStock = new ModifyStockBll();
	private readonly BalanceStockBll balanceStock = new BalanceStockBll();
	private BudModifyTaskResService modifyTaskResSer = new BudModifyTaskResService();
	private BudModifyTaskService modifyTaskSer = new BudModifyTaskService();
	private BudModifyService modifySer = new BudModifyService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.contractId = base.Request["ContractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.modifyId = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["ModifyId"]))
		{
			if (base.Request["ModifyId"].Contains("["))
			{
				this.modifyId = JsonHelper.GetListFromJson(base.Request["ModifyId"])[0];
			}
			else
			{
				this.modifyId = base.Request["ModifyId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			DataTable dataTable = new DataTable();
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InintAddState();
				this.modifyStockAction = "add";
				dataTable = this.purchaseStock.GetPurchaseStockByContractId(this.contractId, false);
				DataColumn dataColumn = new DataColumn("ModifyStockId", typeof(string));
				dataColumn.DefaultValue = string.Empty;
				dataTable.Columns.Add(dataColumn);
				this.hfldBudModifyId.Value = Guid.NewGuid().ToString();
			}
			else
			{
				this.InitUpdateState();
				this.modifyStockAction = "edit";
				dataTable = this.purchaseStock.GetModifyStockByContractId(this.contractId, this.modifyId);
				ConPayoutModifyService conPayoutModifyService = new ConPayoutModifyService();
				ConPayoutModify byId = conPayoutModifyService.GetById(this.modifyId);
				if (byId != null && !string.IsNullOrEmpty(byId.BudModifyId))
				{
					this.hfldBudModifyId.Value = byId.BudModifyId;
				}
				else
				{
					this.hfldBudModifyId.Value = Guid.NewGuid().ToString();
				}
				this.InitBudTask();
			}
			this.ViewState["modifyStockAction"] = this.modifyStockAction;
			this.ViewState["resource"] = dataTable;
			this.DataBindPurchaseplanStock();
			string conPurchasePcode = this.purchase.GetConPurchasePcode(this.contractId);
			if (string.IsNullOrEmpty(conPurchasePcode))
			{
				this.divPurchase.Style.Add("display", "none");
			}
			this.FileLink1.MID = 1902;
			this.FileLink1.FID = this.hfldModifyId.Value;
			this.FileLink1.Type = 1;
			this.hfldIsWBSRelevance.Value = ConfigurationManager.AppSettings["IsWBSRelevance"];
			List<BudModifyTask> list = (
				from mt in this.modifyTaskSer
				where mt.ModifyId == this.hfldBudModifyId.Value
				select mt).ToList<BudModifyTask>();
			if (this.hfldIsWBSRelevance.Value == "1")
			{
				foreach (BudModifyTask current in list)
				{
					this.BindModifyTaskRes(current.ModifyTaskId);
				}
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddContractModify();
			ConPayoutModifyService conPayoutModifyService = new ConPayoutModifyService();
			ConPayoutModify byId = conPayoutModifyService.GetById(this.hfldModifyId.Value);
			byId.BudModifyId = this.hfldBudModifyId.Value;
			conPayoutModifyService.Update(byId);
		}
		else
		{
			if (string.Compare(this.action, "Update", true) == 0)
			{
				this.UpdateContractModify();
			}
		}
		this.UpdateModifyTask();
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
	protected void btnBindResource_Click(object sender, EventArgs e)
	{
		this.UpdatePurchaseplanStockDataSource();
		this.AddBugModify();
		this.UpdateModifyTask();
		this.InitResource(this.hfldResourceId.Value);
	}
	private void UpdateModifyTask()
	{
		HttpCookie httpCookie = base.Request.Cookies[this.hfldEditModifyTaskId.Value];
		List<BudModifyTaskRes> list = new List<BudModifyTaskRes>();
		if (httpCookie != null)
		{
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<List<BudModifyTaskRes>>(value);
			}
		}
		decimal num = 0m;
		foreach (BudModifyTaskRes current in list)
		{
			num += current.ResourcePrice * current.ResourceQuantity;
		}
		BudModifyTask budModifyTask = (
			from r in this.modifyTaskSer
			where r.ModifyTaskId == this.hfldEditModifyTaskId.Value
			select r).FirstOrDefault<BudModifyTask>();
		List<BudModifyTask> list2 = JsonNetWrap.DeserializeObject<List<BudModifyTask>>(this.hfldAllModifyTaskJson.Value);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i].ModifyTaskId == this.hfldEditModifyTaskId.Value)
			{
				list2[i].Total = num;
				list2[i].Total2 = new decimal?(num);
				if (list2[i].Quantity != 0m)
				{
					list2[i].UnitPrice = num / Convert.ToDecimal(list2[i].Quantity);
				}
			}
		}
		this.modifyTaskSer.DelByModifyTaskId(this.hfldEditModifyTaskId.Value);
		foreach (BudModifyTask current2 in list2)
		{
			if (current2.ModifyTaskId == this.hfldEditModifyTaskId.Value)
			{
				this.modifyTaskSer.Add(current2);
				this.SaveModifyTaskRes(this.hfldEditModifyTaskId.Value);
				this.modifyTaskSer.UpdateTotal2(current2.ModifyTaskId);
			}
		}
		if (!list2.Contains(budModifyTask) && budModifyTask != null)
		{
			this.modifyTaskSer.Delete(budModifyTask);
		}
		this.gvBudget.DataSource = list2;
		this.gvBudget.DataBind();
	}
	private void SaveModifyTaskRes(string modifyTaskId)
	{
		this.modifyTaskResSer.DelByModifyTaskId(modifyTaskId);
		HttpCookie httpCookie = base.Request.Cookies[modifyTaskId];
		List<BudModifyTaskRes> list = new List<BudModifyTaskRes>();
		if (httpCookie != null)
		{
			string value = httpCookie.Value;
			if (!string.IsNullOrEmpty(value))
			{
				list = JsonConvert.DeserializeObject<List<BudModifyTaskRes>>(value);
			}
			foreach (BudModifyTaskRes current in list)
			{
				this.modifyTaskResSer.Add(current);
			}
		}
		base.Request.Cookies.Remove(modifyTaskId);
	}
	private void BindModifyTaskRes(string modifyTaskId)
	{
		base.Request.Cookies.Remove(modifyTaskId);
		List<BudModifyTaskRes> t = (
			from mtss in this.modifyTaskResSer
			where mtss.ModifyTaskId == modifyTaskId
			select mtss).ToList<BudModifyTaskRes>();
		string value = JsonHelper.JsonSerializer<List<BudModifyTaskRes>>(t);
		HttpCookie httpCookie = new HttpCookie(modifyTaskId);
		httpCookie.Value = value;
		base.Response.Cookies.Add(httpCookie);
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseplanChecked.Value);
		if (this.ViewState["modifyStockAction"].ToString() == "edit")
		{
			List<string> list = new List<string>();
			if (this.ViewState["modifyStockIds"] != null)
			{
				list = (List<string>)this.ViewState["modifyStockIds"];
			}
			List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldModifyStockId.Value);
			foreach (string current in listFromJson2)
			{
				if (!string.IsNullOrEmpty(current))
				{
					list.Add(current);
				}
			}
			this.ViewState["modifyStockIds"] = list;
		}
		List<string> listFromJson3 = JsonHelper.GetListFromJson(this.hfldPurchaseIds.Value);
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		foreach (string current2 in listFromJson3)
		{
			DataTable infoByPurchaseId = this.balanceStock.GetInfoByPurchaseId(current2);
			if (infoByPurchaseId.Rows.Count > 0)
			{
				PurchaseStockModel model = this.purchaseStock.GetModel(current2);
				if (model != null)
				{
					list3.Add(model.scode);
					listFromJson.Remove(model.scode);
				}
				if (this.ViewState["modifyStockIds"] != null)
				{
					list2 = (List<string>)this.ViewState["modifyStockIds"];
					DataTable infoByModifyId = this.modifyStock.GetInfoByModifyId(this.hfldModifyId.Value);
					if (infoByModifyId.Rows.Count > 0)
					{
						DataRow[] array = infoByModifyId.Select("PurchaseId='" + current2 + "'");
						DataRow[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							DataRow dataRow = array2[i];
							string item = dataRow["ModifyStockId"].ToString();
							list2.Remove(item);
						}
					}
				}
				this.ViewState["modifyStockIds"] = list2;
			}
		}
		this.UpdatePurchaseplanStockDataSource();
		this.DeleteResource(listFromJson);
		if (list3.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("alert('编号为");
			foreach (string current3 in list3)
			{
				stringBuilder.Append(current3.Trim() + ",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append("的物资已经被结算，不能进行删除操作!');");
			base.RegisterScript(stringBuilder.ToString());
		}
		this.DataBindPurchaseplanStock();
	}
	protected void gvwPurchaseplanStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["modifyStockId"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["purchaseId"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
	private void AddContractModify()
	{
		PayoutModifyModel payoutModifyModel = new PayoutModifyModel();
		payoutModifyModel.ModifyID = this.hfldModifyId.Value;
		payoutModifyModel.ContractID = this.contractId;
		payoutModifyModel.ModifyCode = this.txtModifyCode.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtModifyMoney.Text.Trim()))
		{
			payoutModifyModel.ModifyMoney = new decimal?(Convert.ToDecimal(this.txtModifyMoney.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtModifyDate.Text.Trim()))
		{
			payoutModifyModel.ModifyDate = new DateTime?(Convert.ToDateTime(this.txtModifyDate.Text.Trim()));
		}
		payoutModifyModel.ModifyPerson = this.txtModifyPerson.Text.Trim();
		payoutModifyModel.Annex = string.Empty;
		payoutModifyModel.FlowState = new int?(1);
		payoutModifyModel.InputPerson = this.txtInputPerson.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtInputDate.Text.Trim()))
		{
			payoutModifyModel.InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text.Trim()));
		}
		payoutModifyModel.Reason = this.txtReason.Text.Trim();
		payoutModifyModel.Notes = this.txtNotes.Text.Trim();
		try
		{
			if (this.payoutModify.IsExists(payoutModifyModel.ModifyCode, payoutModifyModel.ContractID))
			{
				base.RegisterScript("top.ui.alert('此变更编号已经存在')");
			}
			else
			{
				this.payoutModify.Add(payoutModifyModel);
				this.SavePurchase();
				base.RegisterScript("top.ui.show('添加成功!'); winclose('ModifyEdit','PayoutModifyEdit.aspx?ContractID=" + this.contractId + "',true);");
			}
		}
		catch (Exception)
		{
			base.RegisterScript("top.ui.alert('添加失败');");
		}
	}
	private void UpdateContractModify()
	{
		PayoutModifyModel model = this.payoutModify.GetModel(this.modifyId);
		decimal? modifyMoney = model.ModifyMoney;
		if (!string.IsNullOrEmpty(this.txtModifyMoney.Text.Trim()))
		{
			model.ModifyMoney = new decimal?(Convert.ToDecimal(this.txtModifyMoney.Text.Trim()));
		}
		if (!string.IsNullOrEmpty(this.txtModifyDate.Text.Trim()))
		{
			model.ModifyDate = new DateTime?(Convert.ToDateTime(this.txtModifyDate.Text.Trim()));
		}
		model.ModifyPerson = this.txtModifyPerson.Text.Trim();
		model.Reason = this.txtReason.Text.Trim();
		model.Notes = this.txtNotes.Text.Trim();
		try
		{
			this.payoutModify.Update(model, modifyMoney);
			this.SavePurchase();
			if (model.FlowState == 1)
			{
				PayoutModifyAudit payoutModifyAudit = new PayoutModifyAudit();
				payoutModifyAudit.CommitEvent(model.ModifyID);
			}
			base.RegisterScript("top.ui.show('更新成功'); winclose('ModifyEdit','PayoutModifyEdit.aspx?ContractID=" + model.ContractID + "',true);");
		}
		catch (Exception)
		{
			base.RegisterScript("top.ui.alert('更新失败');");
		}
	}
	private void InitContractInfo(string id)
	{
		PayoutContractModel model = new PayoutContract().GetModel(id);
		this.txtContractCode.Text = model.ContractCode;
		this.txtContractName.Text = model.ContractName;
		this.hfldBId.Value = model.BName;
		this.txtContractMoney.Text = model.ContractMoney.ToString();
		this.txtFinalMoney.Text = model.ModifiedMoney.ToString();
		this.txtSignDate.Text = model.SignDate.Value.ToShortDateString();
		this.hdnProjectCode.Value = model.PrjGuid;
	}
	private void InintAddState()
	{
		this.hfldModifyId.Value = Guid.NewGuid().ToString();
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = DateTime.Now.ToShortDateString();
		this.InitContractInfo(this.contractId);
	}
	private void InitUpdateState()
	{
		PayoutModifyModel model = this.payoutModify.GetModel(this.modifyId);
		this.hfldModifyId.Value = model.ModifyID;
		this.txtModifyCode.Text = model.ModifyCode;
		this.txtModifyCode.Enabled = false;
		this.txtModifyMoney.Text = model.ModifyMoney.ToString();
		this.txtModifyPerson.Text = model.ModifyPerson.ToString();
		this.txtModifyDate.Text = model.ModifyDate.Value.ToShortDateString();
		this.txtReason.Text = model.Reason;
		this.txtNotes.Text = model.Notes;
		this.txtInputPerson.Text = model.InputPerson;
		this.txtInputDate.Text = model.InputDate.Value.ToShortDateString();
		this.InitContractInfo(model.ContractID);
	}
	private void UpdatePurchaseplanStockDataSource()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvwPurchaseplanStock.Rows[i];
				DataRow dataRow = dataTable.Rows[i];
				if (gridViewRow.FindControl("txtNumber") is TextBox)
				{
					TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
					dataRow["number"] = textBox.Text.Trim();
				}
				if (gridViewRow.FindControl("txtSprice") is TextBox)
				{
					TextBox textBox2 = gridViewRow.FindControl("txtSprice") as TextBox;
					dataRow["sprice"] = textBox2.Text.Trim();
				}
				dataRow["Total"] = Convert.ToDecimal(dataRow["number"]) * Convert.ToDecimal(dataRow["sprice"]);
				if (gridViewRow.FindControl("hfldCorp") is HiddenField)
				{
					HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
					dataRow["corp"] = hiddenField.Value;
				}
				if (gridViewRow.FindControl("txtCorp") is TextBox)
				{
					TextBox textBox3 = gridViewRow.FindControl("txtCorp") as TextBox;
					dataRow["CorpName"] = textBox3.Text;
				}
			}
			this.ViewState["resource"] = dataTable;
		}
	}
	private List<string> GetResourceNumber()
	{
		List<string> result = new List<string>();
		string value = this.hfldppcode.Value;
		if (!string.IsNullOrEmpty(value) && value.Length > 2)
		{
			result = JsonHelper.GetListFromJson(value);
		}
		return result;
	}
	private void InitResource(string resources)
	{
		if (!string.IsNullOrEmpty(resources))
		{
			ISerializable serializable = new cn.justwin.Serialize.JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(resources);
			if (array != null)
			{
				Resource resource = new Resource();
				DataTable resource2 = resource.GetResource(array);
				DataColumn dataColumn = new DataColumn("sprice", typeof(decimal));
				dataColumn.DefaultValue = 0.000m;
				DataColumn dataColumn2 = new DataColumn("number", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				DataColumn dataColumn3 = new DataColumn("Total", typeof(decimal));
				dataColumn3.DefaultValue = 0m;
				DataColumn dataColumn4 = new DataColumn("pscode", typeof(string));
				dataColumn4.DefaultValue = string.Empty;
				resource2.Columns.Add(dataColumn4);
				DataColumn dataColumn5 = new DataColumn("ModifyStockId", typeof(string));
				dataColumn5.DefaultValue = string.Empty;
				resource2.Columns.Add(dataColumn5);
				resource2.Columns.Add(dataColumn2);
				resource2.Columns.Add(dataColumn);
				resource2.Columns.Add(dataColumn3);
				string conPurchasePcode = this.purchase.GetConPurchasePcode(this.contractId);
				List<string> resourceNumber = this.GetResourceNumber();
				for (int i = 0; i < resource2.Rows.Count; i++)
				{
					DataRow dataRow = resource2.Rows[i];
					dataRow["pscode"] = conPurchasePcode;
					try
					{
						dataRow["number"] = Convert.ToDecimal(resourceNumber[i]);
					}
					catch
					{
					}
				}
				DataColumn dataColumn6 = new DataColumn("corp", typeof(string));
				dataColumn6.DefaultValue = this.hfldBId.Value;
				resource2.Columns.Add(dataColumn6);
				DataColumn dataColumn7 = new DataColumn("CorpName", typeof(string));
				dataColumn7.DefaultValue = this.GetCorpName(this.hfldBId.Value);
				resource2.Columns.Add(dataColumn7);
				if (this.ViewState["resource"] == null)
				{
					this.ViewState["resource"] = resource2;
				}
				else
				{
					DataTable dataTable = this.ViewState["resource"] as DataTable;
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						DataRow dataRow2 = dataTable.Rows[j];
						for (int k = 0; k < array.Length; k++)
						{
							if (dataRow2["ResourceId"].ToString() == array[k])
							{
								try
								{
									dataRow2["number"] = resourceNumber[k];
								}
								catch
								{
								}
							}
						}
					}
					dataTable = this.MergeDataTable(dataTable, resource2);
				}
				this.DataBindPurchaseplanStock();
			}
		}
	}
	private DataTable MergeDataTable(DataTable dtMaster, DataTable dtMerge)
	{
		foreach (DataRow dataRow in dtMerge.Rows)
		{
			string text = dataRow["ResourceId"].ToString();
			string text2 = dataRow["corp"].ToString();
			decimal num = Convert.ToDecimal(dataRow["sprice"]);
			string text3 = dataRow["pscode"].ToString();
			string filterExpression = string.Concat(new string[]
			{
				"ResourceId='",
				text,
				"' and corp='",
				text2,
				"' and sprice=",
				num.ToString("0.000"),
				" and pscode='",
				text3,
				"'"
			});
			DataRow[] array = dtMaster.Select(filterExpression);
			if (array.Length == 0)
			{
				DataRow dataRow2 = dtMaster.NewRow();
				dataRow2["ResourceId"] = text;
				dataRow2["corp"] = text2;
				dataRow2["sprice"] = num;
				dataRow2["pscode"] = text3;
				dataRow2["number"] = Convert.ToDecimal(dataRow["number"]);
				dataRow2["ResourceCode"] = dataRow["ResourceCode"].ToString();
				dataRow2["ResourceName"] = dataRow["ResourceName"].ToString();
				dataRow2["Specification"] = dataRow["Specification"].ToString();
				dataRow2["Name"] = dataRow["Name"].ToString();
				dataRow2["CorpName"] = dataRow["CorpName"].ToString();
				dataRow2["Brand"] = dataRow["Brand"].ToString();
				dataRow2["ModelNumber"] = dataRow["ModelNumber"].ToString();
				dataRow2["TechnicalParameter"] = dataRow["TechnicalParameter"].ToString();
				dataRow2["Total"] = Convert.ToDecimal(dataRow["Total"]);
				dtMaster.Rows.Add(dataRow2);
			}
		}
		return dtMaster;
	}
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.gvwPurchaseplanStock.DataSource = dataTable;
				this.gvwPurchaseplanStock.DataBind();
				string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwPurchaseplanStock, total, 9);
				return;
			}
			this.gvwPurchaseplanStock.DataSource = dataTable;
			this.gvwPurchaseplanStock.DataBind();
		}
	}
	private string GetCorpName(string corpId)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpId='" + corpId + "'");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpName"].ToString();
		}
		return string.Empty;
	}
	private void DeleteResource(List<string> lstResourceID)
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (lstResourceID.Contains(dataTable.Rows[i]["ResourceCode"].ToString()))
				{
					dataTable.Rows.RemoveAt(i);
					i--;
				}
			}
			dataTable.AcceptChanges();
			this.ViewState["resource"] = dataTable;
		}
	}
	protected void SavePurchase()
	{
		this.UpdatePurchaseplanStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		if (dataTable != null)
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				string condition = "contract = '" + this.contractId + "'";
				List<PurchaseModel> list = this.purchase.GetList(condition);
				foreach (PurchaseModel current in list)
				{
					this.AddPurchaseStock(sqlTransaction, current, this.ViewState["modifyStockAction"].ToString());
				}
				sqlTransaction.Commit();
			}
		}
	}
	private PurchaseModel GetNewPurchase()
	{
		return new PurchaseModel
		{
			pid = Guid.NewGuid().ToString(),
			pcode = DateTime.Now.ToString("yyyyMMddHHmmss"),
			contract = this.contractId,
			flowstate = -1,
			person = PageHelper.QueryUser(this, base.UserCode),
			intime = DateTime.Now,
			acceptstate = 0,
			Project = this.hdnProjectCode.Value,
			annx = string.Empty,
			explain = string.Empty,
			IsConPurchase = true
		};
	}
	private void AddPurchaseStock(SqlTransaction trans, PurchaseModel model, string modifyStockAction)
	{
		this.UpdatePurchaseplanStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		if (modifyStockAction == "add")
		{
			DataRow[] array = dataTable.Select("pscode=" + model.pcode);
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = array[i];
				ModifyStockModel modifyStockModel = new ModifyStockModel();
				modifyStockModel.Id = Guid.NewGuid().ToString();
				modifyStockModel.ModifyId = this.hfldModifyId.Value.Trim();
				if (!string.IsNullOrEmpty(dataRow["psid"].ToString()))
				{
					modifyStockModel.PurchaseId = dataRow["psid"].ToString();
				}
				else
				{
					modifyStockModel.PurchaseId = Guid.NewGuid().ToString();
				}
				modifyStockModel.Scode = dataRow["ResourceCode"].ToString();
				modifyStockModel.Pscode = dataRow["pscode"].ToString();
				modifyStockModel.Quantity = Convert.ToDecimal(dataRow["number"]);
				modifyStockModel.Sprice = Convert.ToDecimal(dataRow["sprice"]);
				modifyStockModel.Corp = dataRow["corp"].ToString();
				if (dataRow["ArrivalDate"] != DBNull.Value)
				{
					modifyStockModel.ArrivalDate = new DateTime?(Convert.ToDateTime(dataRow["ArrivalDate"]));
				}
				else
				{
					modifyStockModel.ArrivalDate = null;
				}
				this.modifyStock.Add(trans, modifyStockModel);
			}
			return;
		}
		if (modifyStockAction == "edit")
		{
			DataRow[] array2 = dataTable.Select("pscode=" + model.pcode + "and ModifyStockId<>''");
			for (int j = 0; j < array2.Length; j++)
			{
				DataRow dataRow2 = array2[j];
				ModifyStockModel modifyStockModel2 = new ModifyStockModel();
				modifyStockModel2.Id = dataRow2["ModifyStockId"].ToString();
				modifyStockModel2.ModifyId = this.hfldModifyId.Value.Trim();
				modifyStockModel2.PurchaseId = dataRow2["psid"].ToString();
				modifyStockModel2.Pscode = dataRow2["pscode"].ToString();
				modifyStockModel2.Quantity = Convert.ToDecimal(dataRow2["number"]);
				modifyStockModel2.Sprice = Convert.ToDecimal(dataRow2["sprice"]);
				modifyStockModel2.Corp = dataRow2["corp"].ToString();
				this.modifyStock.Update(trans, modifyStockModel2);
			}
			if (this.ViewState["modifyStockIds"] != null)
			{
				List<string> ids = (List<string>)this.ViewState["modifyStockIds"];
				this.modifyStock.Delete(trans, ids);
			}
			DataRow[] array3 = dataTable.Select("pscode=" + model.pcode);
			for (int k = 0; k < array3.Length; k++)
			{
				DataRow dataRow3 = array3[k];
				if (string.IsNullOrEmpty(dataRow3["ModifyStockId"].ToString()))
				{
					ModifyStockModel modifyStockModel3 = new ModifyStockModel();
					modifyStockModel3.Id = Guid.NewGuid().ToString();
					modifyStockModel3.ModifyId = this.hfldModifyId.Value.Trim();
					modifyStockModel3.PurchaseId = Guid.NewGuid().ToString();
					modifyStockModel3.Scode = dataRow3["ResourceCode"].ToString();
					modifyStockModel3.Pscode = dataRow3["pscode"].ToString();
					modifyStockModel3.Quantity = Convert.ToDecimal(dataRow3["number"]);
					modifyStockModel3.Sprice = Convert.ToDecimal(dataRow3["sprice"]);
					modifyStockModel3.Corp = dataRow3["corp"].ToString();
					if (dataRow3["ArrivalDate"] != DBNull.Value)
					{
						modifyStockModel3.ArrivalDate = new DateTime?(Convert.ToDateTime(dataRow3["ArrivalDate"]));
					}
					else
					{
						modifyStockModel3.ArrivalDate = null;
					}
					this.modifyStock.Add(trans, modifyStockModel3);
				}
			}
		}
	}
	private void BindBudget()
	{
		List<BudModifyTask> dataSource = JsonNetWrap.DeserializeObject<List<BudModifyTask>>(this.hfldAllModifyTaskJson.Value);
		this.gvBudget.DataSource = dataSource;
		this.gvBudget.DataBind();
	}
	private void InitBudTask()
	{
		BudModifyTaskService budModifyTaskService = new BudModifyTaskService();
		List<BudModifyTask> byModifyId = budModifyTaskService.GetByModifyId(this.hfldBudModifyId.Value);
		this.hfldAllModifyTaskJson.Value = JsonNetWrap.SerializeObject(byModifyId);
		this.gvBudget.DataSource = byModifyId;
		this.gvBudget.DataBind();
	}
	private void AddBugModify()
	{
		BudModifyService budModifyService = new BudModifyService();
		string value = this.hfldBudModifyId.Value;
		if (budModifyService.GetById(value) == null)
		{
			budModifyService.Add(new BudModify
			{
				ModifyId = value,
				PrjId = this.hdnProjectCode.Value,
				ModifyCode = this.txtModifyCode.Text.Trim(),
				ModifyContent = this.txtModifyCode.Text.Trim(),
				ModifyFileCode = this.txtModifyCode.Text.Trim(),
				BudAmount = 0m,
				ReportAmount = 0m,
				ApprovalAmount = 0m,
				ApprovalDate = new DateTime?(DateTime.Now),
				Note = string.Empty,
				Flowstate = -1,
				InputUser = base.UserCode,
				InputDate = DateTime.Now,
				LastModifyUser = base.UserCode,
				LastModifyDate = DateTime.Now
			});
			return;
		}
		budModifyService.Update(new BudModify
		{
			ModifyId = value,
			PrjId = this.hdnProjectCode.Value,
			ModifyCode = this.txtModifyCode.Text.Trim(),
			ModifyContent = this.txtModifyCode.Text.Trim(),
			ModifyFileCode = this.txtModifyCode.Text.Trim(),
			ApprovalDate = new DateTime?(DateTime.Now),
			Note = string.Empty,
			InputUser = base.UserCode,
			InputDate = DateTime.Now,
			LastModifyUser = base.UserCode,
			LastModifyDate = DateTime.Now,
			Flowstate = -1
		});
	}
	public void AddBudModifTask()
	{
		List<BudModifyTask> list = JsonNetWrap.DeserializeObject<List<BudModifyTask>>(this.hfldAllModifyTaskJson.Value);
		if (list == null)
		{
			return;
		}
		BudModifyTaskService budModifyTaskService = new BudModifyTaskService();
		budModifyTaskService.DelModifyTask(this.hfldBudModifyId.Value);
		foreach (BudModifyTask current in list)
		{
			budModifyTaskService.Add(current);
			budModifyTaskService.UpdateTotal2(current.ModifyTaskId);
		}
	}
	protected void btnRefresh_Click(object sender, EventArgs e)
	{
		this.BindBudget();
	}
}


