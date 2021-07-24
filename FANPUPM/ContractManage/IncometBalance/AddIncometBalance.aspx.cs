using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometBalance_AddIncometBalance : NBasePage, IRequiresSessionState
{
	private IncometBalanceBll incometBalanceBll = new IncometBalanceBll();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private BudContractConsReportService budContractRptSev = new BudContractConsReportService();
	private BudContractConsTaskService consTaskSev = new BudContractConsTaskService();
	private ConBalanceItemService conBalItemSev = new ConBalanceItemService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindDdl();
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑收入合同结算";
			IncometBalanceModel model = this.incometBalanceBll.GetModel(base.Request.QueryString["id"]);
			this.txtClearingNumber.Text = model.ClearingNumber;
			this.hdClearingNumber.Value = model.ClearingNumber;
			this.txtClearingPrice.Text = model.ClearingPrice.ToString();
			this.txtClearingTime.Text = Common2.GetTime(model.ClearingTime.ToString());
			this.txtClearingUser.Text = model.ClearingUser;
			this.txtInputDate.Text = Common2.GetTime(model.InputDate.ToString());
			this.txtInputPerson.Text = model.InputPerson;
			this.txtRemark.Text = model.Remark;
			this.hdGuid.Value = model.ID;
			this.ddlBalanceMode.SelectedValue = model.BalanceMode;
			this.ddlPayMode.SelectedValue = model.PayMode;
			this.txtBalanceObj.Text = model.BalanceObject;
			List<ConBalanceItem> listByBalanceId = this.conBalItemSev.GetListByBalanceId(this.hdGuid.Value);
			this.SaveBanlanceItemToViewState(listByBalanceId);
			this.BindBalanceItem();
		}
		else
		{
			this.lblTitle.Text = "新增收入合同结算";
			this.txtInputDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.BindBalanceItem();
		}
		this.hfldContractId.Value = base.Request.QueryString["ContractID"];
		IncometContractModel model2 = this.incometContractBll.GetModel(base.Request.QueryString["ContractID"]);
		this.txtContractCode.Text = model2.ContractCode;
		this.txtContractName.Text = model2.ContractName;
		this.txtContractPrice.Text = WebUtil.GetEnPrice(model2.ContractPrice.ToString(), model2.ContractID);
		this.txtSignedTime.Text = Common2.GetTime(model2.SignedTime.ToString());
		this.txtSumClearing.Text = WebUtil.GetPaymentSum(model2.ContractID, "Con_Incomet_Balance", "ClearingPrice");
		this.txtDiffAmount.Text = string.Concat(Convert.ToDecimal(this.txtContractPrice.Text) - Convert.ToDecimal(this.txtSumClearing.Text));
		this.FileLink1.MID = 1906;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
		IncometContractModel model3 = this.incometContractBll.GetModel(base.Request["ContractID"]);
		this.hfldPrjid.Value = model3.Project.PrjGuid.ToString();
		this.initMeasureBindGV();
	}
	protected void hfldBtnConBalanceItem_Click(object sender, EventArgs e)
	{
		this.hfldconBalanceItem.Value = base.Request.Cookies["balanceJson"].Value;
		ConBalanceItem conBalanceItem = JsonConvert.DeserializeObject<ConBalanceItem>(this.hfldconBalanceItem.Value);
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		if (conBalanceItem != null)
		{
			ConBalanceItem conBalanceItem2 = null;
			foreach (ConBalanceItem current in conBalanceItemFromViewState)
			{
				if (current.Id == conBalanceItem.Id)
				{
					conBalanceItem2 = current;
					break;
				}
			}
			if (conBalanceItem2 != null)
			{
				for (int i = 0; i < conBalanceItemFromViewState.Count; i++)
				{
					if (conBalanceItemFromViewState[i].Id == conBalanceItem.Id)
					{
						conBalanceItemFromViewState[i] = conBalanceItem;
						break;
					}
				}
			}
			else
			{
				conBalanceItemFromViewState.Add(conBalanceItem);
			}
		}
		this.SaveBanlanceItemToViewState(conBalanceItemFromViewState);
		this.BindBalanceItem();
	}
	protected void btnEdit_click(object sender, EventArgs e)
	{
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		string value = this.hfldInItemIds.Value;
		foreach (ConBalanceItem current in conBalanceItemFromViewState)
		{
			if (current.Id == value)
			{
				string value2 = JsonConvert.SerializeObject(current);
				base.Response.Cookies["balanceJson"].Value = value2;
				base.RegisterScript("EditItem();");
				return;
			}
		}
		this.BindBalanceItem();
	}
	protected void btnDelItem_Click(object sender, EventArgs e)
	{
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldInItemIds.Value);
		foreach (string itemId in listFromJson)
		{
			ConBalanceItem conBalanceItem = (
				from cb in conBalanceItemFromViewState
				where cb.Id == itemId
				select cb).FirstOrDefault<ConBalanceItem>();
			if (conBalanceItem != null)
			{
				conBalanceItemFromViewState.Remove(conBalanceItem);
			}
		}
		this.SaveBanlanceItemToViewState(conBalanceItemFromViewState);
		this.BindBalanceItem();
	}
	protected void gvgvIncometItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvIncometItem.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			this.gvIncometItem.DataKeys[e.Row.RowIndex]["Type"].ToString();
			string value2 = this.gvIncometItem.DataKeys[e.Row.RowIndex]["Qty"].ToString();
			string value3 = this.gvIncometItem.DataKeys[e.Row.RowIndex]["UnitPrice"].ToString();
			if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(value3))
			{
				e.Row.Cells[5].Text = (Convert.ToDecimal(value2) * Convert.ToDecimal(value3)).ToString();
			}
		}
	}
	private void BindBalanceItem()
	{
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		this.gvIncometItem.DataSource = conBalanceItemFromViewState;
		this.gvIncometItem.DataBind();
	}
	private void SaveBanlanceItemToViewState(List<ConBalanceItem> conBalanceItemLst)
	{
		this.ViewState["conBalanceItemLst"] = conBalanceItemLst;
	}
	private List<ConBalanceItem> GetConBalanceItemFromViewState()
	{
		List<ConBalanceItem> list = this.ViewState["conBalanceItemLst"] as List<ConBalanceItem>;
		if (list == null)
		{
			list = new List<ConBalanceItem>();
		}
		return list;
	}
	private void initMeasureBindGV()
	{
		List<BudContractConsReport> contractReportByConIDAndBalID = this.budContractRptSev.GetContractReportByConIDAndBalID(this.hfldContractId.Value, this.hdGuid.Value);
		this.SaveToViewState(contractReportByConIDAndBalID);
		this.gvContractRpt.DataSource = contractReportByConIDAndBalID;
		this.gvContractRpt.DataBind();
	}
	private void BindConsTask(IList<BudContractConsReport> consRpt)
	{
		this.gvContractRpt.DataSource =
			from ct in consRpt
			orderby ct.InputDate descending
			select ct;
		this.gvContractRpt.DataBind();
	}
	private void SaveToViewState(IList<BudContractConsReport> consRpt)
	{
		this.ViewState["ContractConsTask"] = consRpt;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (BudContractConsReport current in consRpt)
		{
			stringBuilder.Append("'" + current.RptId + "',");
		}
		if (stringBuilder.ToString().Length > 0)
		{
			this.hfldRptIDs.Value = stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1);
		}
		else
		{
			this.hfldRptIDs.Value = "''";
		}
		this.GVconsTaskBind();
	}
	private List<BudContractConsReport> GetFromViewState()
	{
		List<BudContractConsReport> list = this.ViewState["ContractConsTask"] as List<BudContractConsReport>;
		if (list == null)
		{
			list = new List<BudContractConsReport>();
		}
		return list;
	}
	public void BindDdl()
	{
		this.ddlBalanceMode.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and ParentCodeID=0 AND IsValid=1 ");
		this.ddlBalanceMode.DataBind();
		this.ddlPayMode.DataSource = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=25 and ParentCodeID=0 AND IsValid=1 ");
		this.ddlPayMode.DataBind();
		WebUtil.AddItem(this.ddlBalanceMode, "结算类型");
		WebUtil.AddItem(this.ddlPayMode, "付款方式");
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		int num;
		if (base.Request.QueryString["id"] != null)
		{
			if (this.hdClearingNumber.Value != this.txtClearingNumber.Text && this.incometBalanceBll.GetListArray(string.Concat(new string[]
			{
				" where ClearingNumber='",
				this.txtClearingNumber.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("top.ui.alert('结算编号已存在')");
				return;
			}
			num = this.UpdateModel();
		}
		else
		{
			if (this.incometBalanceBll.GetListArray(string.Concat(new string[]
			{
				" where ClearingNumber='",
				this.txtClearingNumber.Text,
				"' and ContractID='",
				base.Request.QueryString["ContractID"],
				"'"
			})).Count > 0)
			{
				base.RegisterScript("top.ui.alert('结算编号已存在')");
				return;
			}
			num = this.AddModel();
		}
		this.UpdateBudConstractConsRpt();
		this.UpdateConBalanceItem();
		if (num != 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('");
			if (ContractParameter.IsBalanceAlarm && this.isBalancePrice(this.txtContractPrice.Text))
			{
				stringBuilder.Append("警告:结算金额大于合同金额\\n");
			}
			stringBuilder.Append(this.SetMsg() + "成功！');").Append(Environment.NewLine);
			stringBuilder.Append("winclose('AddIncometBalance','ShowBalanceList.aspx?ContractID=" + base.Request.QueryString["ContractID"] + "',true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		else
		{
			base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败');");
		}
		this.ConvertGridToList();
	}
	public void UpdateConBalanceItem()
	{
		this.conBalItemSev.ExcuteSql(" delete Con_BalanceItem where BalanceId='" + this.hdGuid.Value + "'");
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		foreach (ConBalanceItem current in conBalanceItemFromViewState)
		{
			this.conBalItemSev.Add(current);
		}
	}
	public bool isBalancePrice(string endPrice)
	{
		string paymentSum = WebUtil.GetPaymentSum(base.Request.QueryString["ContractID"], "Con_Incomet_Balance", "ClearingPrice");
		return !string.IsNullOrEmpty(endPrice) && !string.IsNullOrEmpty(paymentSum) && Convert.ToDecimal(paymentSum) > Convert.ToDecimal(endPrice);
	}
	public int AddModel()
	{
		return this.incometBalanceBll.Add(this.GetModel());
	}
	public int UpdateModel()
	{
		return this.incometBalanceBll.Update(this.GetModel());
	}
	public IncometBalanceModel GetModel()
	{
		return new IncometBalanceModel
		{
			Annex = "",
			ClearingNumber = this.txtClearingNumber.Text,
			ClearingPrice = new decimal?(Convert.ToDecimal(this.txtClearingPrice.Text)),
			ClearingTime = new DateTime?(Convert.ToDateTime(this.txtClearingTime.Text)),
			ClearingUser = this.txtClearingUser.Text,
			ContractID = base.Request.QueryString["ContractID"],
			ID = this.hdGuid.Value,
			InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text)),
			InputPerson = this.txtInputPerson.Text,
			Remark = this.txtRemark.Text,
			BalanceMode = this.ddlBalanceMode.SelectedValue,
			PayMode = this.ddlPayMode.SelectedValue,
			BalanceObject = this.txtBalanceObj.Text
		};
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	protected void gvContractRpt_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvContractRpt.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["Prjguid"] = this.gvContractRpt.DataKeys[e.Row.RowIndex]["PrjId"].ToString();
			e.Row.Attributes["state"] = this.gvContractRpt.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
			e.Row.Attributes["ContractId"] = this.gvContractRpt.DataKeys[e.Row.RowIndex]["ContractId"].ToString();
		}
	}
	protected void delContractConsReport(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldBtnDelMeasureIds.Value);
		List<BudContractConsReport> fromViewState = this.GetFromViewState();
		foreach (string rptId in listFromJson)
		{
			BudContractConsReport budContractConsReport = (
				from t in fromViewState
				where t.RptId == rptId
				select t).FirstOrDefault<BudContractConsReport>();
			if (budContractConsReport != null)
			{
				fromViewState.Remove(budContractConsReport);
			}
		}
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	public void hfldBtnMeasure_Click(object sender, EventArgs e)
	{
		this.UpdateGvwToViewState();
		List<BudContractConsReport> fromViewState = this.GetFromViewState();
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	private void UpdateGvwToViewState()
	{
		List<BudContractConsReport> fromViewState = this.GetFromViewState();
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldMeasureIds.Value);
		BudContractConsReport byBalanceIdAndContractIDAndType = this.budContractRptSev.getByBalanceIdAndContractIDAndType(this.hdGuid.Value, this.hfldContractId.Value, "1");
		if (byBalanceIdAndContractIDAndType != null && !listFromJson.Contains(byBalanceIdAndContractIDAndType.RptId))
		{
			listFromJson.Add(byBalanceIdAndContractIDAndType.RptId);
		}
		foreach (string rptId in listFromJson)
		{
			if ((
				from t in fromViewState
				where t.RptId == rptId
				select t).FirstOrDefault<BudContractConsReport>() == null)
			{
				BudContractConsReport byId = this.budContractRptSev.GetById(rptId);
				fromViewState.Add(byId);
			}
		}
		this.SaveToViewState(fromViewState);
	}
	protected void gvwConsTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConsTask.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			e.Row.Attributes["guid"] = this.gvConsTask.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
		}
	}
	protected void GVconsTaskBind()
	{
		this.gvConsTask.DataSource = cn.justwin.Domain.BudContractTask.GetDtblByPrjidAndContractIDAndRptIds(this.hfldPrjid.Value, this.hfldContractId.Value, this.hfldRptIDs.Value);
		this.gvConsTask.DataBind();
	}
	protected List<BudContractConsTask> ConvertGridToList()
	{
		List<BudContractConsTask> list = new List<BudContractConsTask>();
		foreach (GridViewRow gridViewRow in this.gvConsTask.Rows)
		{
			bool flag = false;
			BudContractConsTask budContractConsTask = new BudContractConsTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budContractConsTask.TaskId = value;
				decimal d = Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtCompleteAmount")).Text);
				decimal num = Convert.ToDecimal(((HiddenField)gridViewRow.FindControl("hfldCompleteAmount")).Value);
				decimal d2 = Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtApproveAmount")).Text);
				decimal num2 = Convert.ToDecimal(((HiddenField)gridViewRow.FindControl("hfldApproveAmount")).Value);
				budContractConsTask.Amount = 0m;
				if (!d.Equals(num))
				{
					budContractConsTask.Amount = d - num;
					flag = true;
				}
				budContractConsTask.ApproveAmount = 0m;
				if (!d2.Equals(num2))
				{
					budContractConsTask.ApproveAmount = d2 - num2;
					flag = true;
				}
				if (flag)
				{
					list.Add(budContractConsTask);
				}
			}
		}
		return list;
	}
	public void UpdateBudConstractConsRpt()
	{
		StringBuilder stringBuilder = new StringBuilder();
		this.SaveBudConstractConsRpt();
		if (this.hfldRptIDs.Value == "''")
		{
			BudContractConsReport byBalanceIdAndContractIDAndType = this.budContractRptSev.getByBalanceIdAndContractIDAndType(this.hdGuid.Value, this.hfldContractId.Value, "1");
			if (byBalanceIdAndContractIDAndType != null)
			{
				stringBuilder.Append("DELETE Bud_ContractConsTask WHERE rptId='" + byBalanceIdAndContractIDAndType.RptId + "' ");
				this.budContractRptSev.Delete(byBalanceIdAndContractIDAndType);
			}
		}
		stringBuilder.Append(string.Concat(new string[]
		{
			"Update Bud_ContractConsReport SET BalanceId=NULL WHERE BalanceId='",
			this.hdGuid.Value,
			"' AND contractId='",
			this.hfldContractId.Value,
			"' AND Type='0' "
		}));
		stringBuilder.Append(string.Concat(new string[]
		{
			"Update Bud_ContractConsReport SET BalanceId='",
			this.hdGuid.Value,
			"' WHERE RptId in(",
			this.hfldRptIDs.Value,
			") AND contractId='",
			this.hfldContractId.Value,
			"' AND Type='0' "
		}));
		this.budContractRptSev.ExcuteSql(stringBuilder.ToString());
	}
	public void SaveBudConstractConsRpt()
	{
		List<BudContractConsTask> list = this.ConvertGridToList();
		string rptId = Guid.NewGuid().ToString();
		if (list.Count > 0)
		{
			BudContractConsReport budContractConsReport = this.budContractRptSev.getByBalanceIdAndContractIDAndType(this.hdGuid.Value, this.hfldContractId.Value, "1");
			if (budContractConsReport != null)
			{
				rptId = budContractConsReport.RptId;
				using (List<BudContractConsTask>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BudContractConsTask current = enumerator.Current;
						BudContractConsTask budContractConsTask = this.consTaskSev.GetByRptIdAndTaskId(rptId, current.TaskId);
						if (budContractConsTask != null)
						{
							budContractConsTask.Amount += current.Amount;
							budContractConsTask.ApproveAmount += current.ApproveAmount;
							this.consTaskSev.Update(budContractConsTask);
						}
						else
						{
							budContractConsTask = new BudContractConsTask();
							budContractConsTask.ConsTaskId = Guid.NewGuid().ToString();
							budContractConsTask.RptId = rptId;
							budContractConsTask.TaskId = current.TaskId;
							budContractConsTask.Amount = current.Amount;
							budContractConsTask.Note = "";
							budContractConsTask.ApproveAmount = current.ApproveAmount;
							this.consTaskSev.Add(budContractConsTask);
						}
					}
					return;
				}
			}
			budContractConsReport = new BudContractConsReport();
			budContractConsReport.RptId = rptId;
			budContractConsReport.InputDate = DateTime.Now;
			budContractConsReport.InputUser = PageHelper.QueryUser(this, base.UserCode);
			budContractConsReport.IsValid = true;
			budContractConsReport.PrjId = this.hfldPrjid.Value;
			budContractConsReport.Type = "1";
			budContractConsReport.FlowState = 1;
			budContractConsReport.Note = "";
			budContractConsReport.ContractId = this.hfldContractId.Value;
			budContractConsReport.BalanceId = this.hdGuid.Value;
			this.budContractRptSev.Add(budContractConsReport);
			foreach (BudContractConsTask current2 in list)
			{
				BudContractConsTask budContractConsTask2 = new BudContractConsTask();
				budContractConsTask2.ConsTaskId = Guid.NewGuid().ToString();
				budContractConsTask2.RptId = rptId;
				budContractConsTask2.TaskId = current2.TaskId;
				budContractConsTask2.Amount = current2.Amount;
				budContractConsTask2.Note = "";
				budContractConsTask2.ApproveAmount = current2.ApproveAmount;
				this.consTaskSev.Add(budContractConsTask2);
			}
		}
	}
	public string getTypeName(string type)
	{
		string result;
		if (type != null)
		{
			if (type == "1")
			{
				result = "管理扣项";
				return result;
			}
			if (type == "2")
			{
				result = "结算增减项";
				return result;
			}
			if (type == "3")
			{
				result = "代扣代缴税金";
				return result;
			}
		}
		result = "管理扣项";
		return result;
	}
}


