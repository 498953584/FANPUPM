using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutBalance_BalanceEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PayoutBalance payoutBalance = new PayoutBalance();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private readonly BalanceStockBll balanceStockBll = new BalanceStockBll();
	private ConConfigContractService configSer = new ConConfigContractService();
	private PayoutContract payoutContract = new PayoutContract();
	private ConBalanceItemService conBalItemSev = new ConBalanceItemService();
	private BudConsReportService budConReportSev = new BudConsReportService();
	private BudConsTaskService budConsTaskSev = new BudConsTaskService();

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
		if (!string.IsNullOrEmpty(base.Request["BalanceId"]))
		{
			if (base.Request["BalanceId"].Contains("["))
			{
				this.ViewState["BalanceId"] = JsonHelper.GetListFromJson(base.Request["BalanceId"])[0];
			}
			else
			{
				this.ViewState["BalanceId"] = base.Request["BalanceId"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(this.contractId) && !string.IsNullOrEmpty(this.ViewState["BalanceId"].ToString()))
		{
			PayoutBalanceModel model = this.payoutBalance.GetModel(this.ViewState["BalanceId"].ToString());
			this.contractId = model.ContractID;
		}
		if (!base.IsPostBack)
		{
			WebUtil.DataBindBalanceMode(this.dropBalanceMode);
			WebUtil.DataBindPayMode(this.dropPayMode);
			if (string.Compare(this.action, "Add", true) == 0)
			{
				this.InitAddState();
				this.BindBalanceItem();
			}
			else
			{
				this.InitUpdateState();
				List<ConBalanceItem> listByBalanceId = this.conBalItemSev.GetListByBalanceId(this.hdGuid.Value);
				this.SaveBanlanceItemToViewState(listByBalanceId);
				this.BindBalanceItem();
			}
			DataTable balanceStockByContractId = this.purchaseStock.GetBalanceStockByContractId(this.contractId, this.ViewState["BalanceId"].ToString());
			this.ViewState["resource"] = balanceStockByContractId;
			this.DataBindBalanceStock();
			if (balanceStockByContractId.Rows.Count == 0)
			{
				this.divPurchase.Style.Add("display", "none");
			}
			this.FileLink1.MID = 1903;
			this.FileLink1.FID = this.ViewState["BalanceId"].ToString();
			this.FileLink1.Type = 1;
			PayoutContractModel model2 = this.payoutContract.GetModel(this.contractId);
			this.hfldPrjid.Value = model2.PrjGuid.ToString();
			this.InitContractReport();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			this.AddBalance();
		}
		else
		{
			this.UpdateBalance();
		}
		this.UpdateConBalanceItem();
		this.UpdateBudConstractConsRpt();
	}
	private void DataBindBalanceStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.gvwPurchaseplanStock.DataSource = dataTable;
				this.gvwPurchaseplanStock.DataBind();
				string[] value = new string[]
				{
					dataTable.Compute("SUM(ThisTimeTotal)", string.Empty).ToString(),
					dataTable.Compute("SUM(AlreadyTotal)", string.Empty).ToString()
				};
				int[] index = new int[]
				{
					9,
					11
				};
				this.AddTotalRow(value, index);
				return;
			}
			this.gvwPurchaseplanStock.DataSource = dataTable;
			this.gvwPurchaseplanStock.DataBind();
		}
	}
	private void AddTotalRow(string[] value, int[] index)
	{
		if (value.Length == 0 || value.Length != index.Length)
		{
			return;
		}
		GridViewRow gridViewRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
		TableCell tableCell = new TableCell();
		tableCell.Attributes["colspan"] = index[0].ToString();
		tableCell.Attributes["align"] = "center";
		tableCell.Text = "总 计";
		gridViewRow.Cells.Add(tableCell);
		for (int i = 0; i < index.Length; i++)
		{
			TableCell tableCell2 = new TableCell();
			tableCell2.Text = value[i];
			tableCell2.Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign.Right.ToString());
			tableCell2.CssClass = "decimal_input";
			gridViewRow.Cells.Add(tableCell2);
			if (i < index.Length - 1)
			{
				int num = index[i + 1] - index[i] - 1;
				if (num > 0)
				{
					TableCell tableCell3 = new TableCell();
					tableCell3.Attributes["colspan"] = num.ToString();
					gridViewRow.Cells.Add(tableCell3);
				}
			}
		}
		if (this.gvwPurchaseplanStock.Columns.Count - index[index.Length - 1] - 1 > 0)
		{
			TableCell tableCell4 = new TableCell();
			tableCell4.Attributes["colspan"] = Convert.ToString(this.gvwPurchaseplanStock.Columns.Count - index[index.Length - 1] - 1);
			gridViewRow.Cells.Add(tableCell4);
		}
		if (this.gvwPurchaseplanStock.Rows.Count > 0)
		{
			this.gvwPurchaseplanStock.Controls[0].Controls.AddAt(this.gvwPurchaseplanStock.Rows.Count + 1, gridViewRow);
		}
	}
	protected void gvwPurchaseplanStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void chkContainPendint_CheckedChanged(object sender, EventArgs e)
	{
		bool @checked = this.chkContainPending.Checked;
		this.txtDiffAmount.Text = this.GetDiffAmount(this.contractId, @checked).ToString();
	}
	private void InitAddState()
	{
		this.ViewState["BalanceId"] = Guid.NewGuid().ToString();
		this.InitContractInfo();
		this.txtInputPerson.Text = PageHelper.QueryUser(this, base.UserCode);
		this.txtInputDate.Text = DateTime.Now.ToShortDateString();
		this.txtDiffAmount.Text = this.GetDiffAmount(this.contractId, false).ToString();
		this.hdGuid.Value = this.ViewState["BalanceId"].ToString();
	}
	private void InitUpdateState()
	{
		PayoutBalanceModel model = this.payoutBalance.GetModel(this.ViewState["BalanceId"].ToString());
		if (model != null)
		{
			this.contractId = model.ContractID;
			this.InitContractInfo();
			this.txtBalanceCode.Text = model.BalanceCode;
			this.txtBalanceCode.ReadOnly = true;
			this.txtBalanceMoney.Text = model.BalanceMoney.ToString();
			this.txtBalanceDate.Text = model.BalanceDate.Value.ToShortDateString();
			this.txtBalancePerson.Text = model.BalancePerson;
			this.dropBalanceMode.SelectedValue = model.BalanceMode;
			this.dropPayMode.SelectedValue = model.PayMode;
			this.txtBalanceObj.Text = model.BalanceObject;
			this.chkContainPending.Checked = model.ContainPending;
			this.txtInputPerson.Text = model.InputPerson;
			this.txtInputDate.Text = model.InputDate.Value.ToShortDateString();
			this.txtNotes.Text = model.Notes;
			this.hdGuid.Value = model.BalanceID;
			this.txtDiffAmount.Text = this.GetDiffAmount(this.contractId, model.ContainPending).ToString();
		}
	}
	private decimal GetDiffAmount(string contractId, bool containPending)
	{
		PayoutContractModel model = new PayoutContract().GetModel(contractId);
		decimal balancedAmount = this.payoutBalance.GetBalancedAmount(contractId, containPending);
		decimal result;
		if (model.ModifiedMoney.HasValue)
		{
			result = model.ModifiedMoney.Value - balancedAmount;
		}
		else
		{
			result = 0m - balancedAmount;
		}
		return result;
	}
	private void AddBalance()
	{
		PayoutBalanceModel payoutBalanceModel = new PayoutBalanceModel();
		payoutBalanceModel.BalanceID = this.ViewState["BalanceId"].ToString();
		payoutBalanceModel.BalanceCode = this.txtBalanceCode.Text.Trim();
		payoutBalanceModel.ContractID = this.contractId;
		if (!string.IsNullOrEmpty(this.txtBalanceMoney.Text.Trim()))
		{
			payoutBalanceModel.BalanceMoney = new decimal?(Convert.ToDecimal(this.txtBalanceMoney.Text.Trim()));
		}
		payoutBalanceModel.BalancePerson = this.txtBalancePerson.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtBalanceDate.Text.Trim()))
		{
			payoutBalanceModel.BalanceDate = new DateTime?(Convert.ToDateTime(this.txtBalanceDate.Text.Trim()));
		}
		payoutBalanceModel.Annex = string.Empty;
		payoutBalanceModel.FlowState = new int?(-1);
		payoutBalanceModel.BalanceMode = this.dropBalanceMode.SelectedValue;
		payoutBalanceModel.PayMode = this.dropPayMode.SelectedValue;
		payoutBalanceModel.BalanceObject = this.txtBalanceObj.Text;
		payoutBalanceModel.ContainPending = this.chkContainPending.Checked;
		payoutBalanceModel.Notes = this.txtNotes.Text.Trim();
		payoutBalanceModel.InputPerson = this.txtInputPerson.Text.Trim();
		payoutBalanceModel.InputDate = new DateTime?(Convert.ToDateTime(this.txtInputDate.Text.Trim()));
		try
		{
			if (this.payoutBalance.IsExists(payoutBalanceModel.BalanceCode, payoutBalanceModel.ContractID))
			{
				base.RegisterScript("top.ui.alert('此结算编号已经存在')");
			}
			else
			{
				this.payoutBalance.Add(payoutBalanceModel);
				this.SaveBalanceStock();
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				ConConfigContract conConfigContract = (
					from p in this.configSer
					where p.ContractId == this.contractId
					select p).FirstOrDefault<ConConfigContract>();
				if (conConfigContract != null)
				{
					flag = conConfigContract.IsBalanceAlarm;
				}
				if (flag && this.payoutBalance.GreaterModifiedMoney(payoutBalanceModel))
				{
					stringBuilder.Append("top.ui.alert('结算金额大于合同金额\\n添加成功')");
				}
				else
				{
					stringBuilder.Append("top.ui.show('添加成功')");
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("winclose('BalanceEdit', 'PayoutBalanceEdit.aspx?ContractID=" + this.contractId + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch (Exception)
		{
			base.RegisterHintScript("Add", false, string.Empty);
		}
	}
	private void UpdateBalance()
	{
		PayoutBalanceModel model = this.payoutBalance.GetModel(this.ViewState["BalanceId"].ToString());
		if (!string.IsNullOrEmpty(this.txtBalanceMoney.Text.Trim()))
		{
			model.BalanceMoney = new decimal?(Convert.ToDecimal(this.txtBalanceMoney.Text.Trim()));
		}
		model.BalancePerson = this.txtBalancePerson.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtBalanceDate.Text.Trim()))
		{
			model.BalanceDate = new DateTime?(Convert.ToDateTime(this.txtBalanceDate.Text.Trim()));
		}
		model.BalanceMode = this.dropBalanceMode.SelectedValue;
		model.PayMode = this.dropPayMode.SelectedValue;
		model.BalanceObject = this.txtBalanceObj.Text;
		model.ContainPending = this.chkContainPending.Checked;
		model.Notes = this.txtNotes.Text.Trim();
		try
		{
			this.payoutBalance.Update(model);
			this.SaveBalanceStock();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show('修改成功')");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("winclose('BalanceEdit', 'PayoutBalanceEdit.aspx?ContractID=" + model.ContractID + "', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (Exception)
		{
			base.RegisterHintScript("Update", false, string.Empty);
		}
	}
	private void InitContractInfo()
	{
		if (!string.IsNullOrEmpty(this.contractId))
		{
			PayoutContractModel model = new PayoutContract().GetModel(this.contractId);
			if (model != null)
			{
				this.txtContractCode.Text = model.ContractCode;
				this.txtContractName.Text = model.ContractName;
				this.txtContractMoney.Text = model.ModifiedMoney.ToString();
				this.txtSignedDate.Text = model.SignDate.Value.ToShortDateString();
				this.txtContractType.Text = model.TypeName;
				this.txtAName.Text = model.AName;
				this.txtBname.Text = model.CorpName;
				this.txtAddress.Text = model.Address;
				this.hfldContractId.Value = model.ContractID;
			}
		}
	}
	private void InitContractReport()
	{
		List<BudConsReport> list = (
			from r in this.budConReportSev
			where r.ConstractId == this.hfldContractId.Value && r.FlowState == 1 && r.IsValid == (bool?)true && r.BalanceId == this.ViewState["BalanceId"].ToString()
			select r).ToList<BudConsReport>();
		this.SaveToViewState(list);
		this.gvConract.DataSource = list;
		this.gvConract.DataBind();
	}
	public void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex]["ConsReportId"].ToString();
			e.Row.Attributes["guid"] = this.gvConract.DataKeys[e.Row.RowIndex]["ConsReportId"].ToString();
			e.Row.Attributes["state"] = this.gvConract.DataKeys[e.Row.RowIndex]["State"].ToString();
		}
	}
	private void BindConsTask(IList<BudConsReport> consRpt)
	{
		this.gvConract.DataSource =
			from ct in consRpt
			orderby ct.InputDate descending
			select ct;
		this.gvConract.DataBind();
	}
	public void hfldBtnMeasure_Click(object sender, EventArgs e)
	{
		this.UpdateGvwToViewState();
		List<BudConsReport> fromViewState = this.GetFromViewState();
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	private List<BudConsReport> GetFromViewState()
	{
		List<BudConsReport> list = this.ViewState["ContractConsTask"] as List<BudConsReport>;
		if (list == null)
		{
			list = new List<BudConsReport>();
		}
		return list;
	}
	private void SaveToViewState(IList<BudConsReport> consRpt)
	{
		this.ViewState["ContractConsTask"] = consRpt;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (BudConsReport current in consRpt)
		{
			stringBuilder.Append("'" + current.ConsReportId + "',");
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
	private void UpdateGvwToViewState()
	{
		List<BudConsReport> fromViewState = this.GetFromViewState();
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldMeasureIds.Value);
		List<BudConsReport> list = (
			from r in this.budConReportSev
			where r.ConstractId == this.hfldContractId.Value && r.FlowState == 1 && r.IsValid == (bool?)true && r.BalanceId == this.ViewState["BalanceId"].ToString()
			select r).ToList<BudConsReport>();
		if (list != null)
		{
			foreach (BudConsReport current in list)
			{
				if (!listFromJson.Contains(current.ConsReportId))
				{
					listFromJson.Add(current.ConsReportId);
				}
			}
		}
		foreach (string rptId in listFromJson)
		{
			if ((
				from t in fromViewState
				where t.ConsReportId == rptId
				select t).FirstOrDefault<BudConsReport>() == null)
			{
				BudConsReport byId = this.budConReportSev.GetById(rptId);
				byId.Type = "0";
				fromViewState.Add(byId);
			}
		}
		this.SaveToViewState(fromViewState);
	}
	protected void gvTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string value = this.gvTask.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	protected void GVconsTaskBind()
	{
		DataTable dataTable = new DataTable();
		string text = this.hfldRptIDs.Value.ToString().Replace("'", "");
		string[] array = text.Split(new char[]
		{
			','
		});
		bool flag = false;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string reportId = array2[i];
			List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(reportId);
			string text2 = "";
			foreach (string current in taskIdsByReportId)
			{
				if (current != "")
				{
					text2 += "'";
					text2 += current;
					text2 += "',";
				}
			}
			if (text2 != "")
			{
				text2 = text2.Substring(0, text2.Length - 1);
			}
			else
			{
				text2 = "''";
			}
			DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text2, reportId, true, false);
			if (!flag)
			{
				dataTable = allByTaskIds.Clone();
				flag = true;
			}
			object[] array3 = new object[dataTable.Columns.Count];
			for (int j = 0; j < allByTaskIds.Rows.Count; j++)
			{
				allByTaskIds.Rows[j].ItemArray.CopyTo(array3, 0);
				dataTable.Rows.Add(array3);
			}
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			for (int l = k + 1; l < dataTable.Rows.Count; l++)
			{
				if (dataTable.Rows[k][1].ToString() == dataTable.Rows[l][1].ToString() && k != l)
				{
					decimal num = Convert.ToDecimal(dataTable.Rows[k][9]) + Convert.ToDecimal(dataTable.Rows[l][9]);
					dataTable.Rows[k][9] = num;
					dataTable.Rows.RemoveAt(l);
					l--;
				}
			}
		}
		this.gvTask.DataSource = dataTable;
		this.gvTask.DataBind();
	}
	protected void delContractConsReport(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldBtnDelMeasureIds.Value);
		List<BudConsReport> fromViewState = this.GetFromViewState();
		foreach (string rptId in listFromJson)
		{
			BudConsReport budConsReport = (
				from t in fromViewState
				where t.ConsReportId == rptId
				select t).FirstOrDefault<BudConsReport>();
			if (budConsReport != null)
			{
				fromViewState.Remove(budConsReport);
				budConsReport.BalanceId = null;
				this.budConReportSev.Update(budConsReport);
			}
		}
		this.SaveToViewState(fromViewState);
		this.BindConsTask(fromViewState);
	}
	public void UpdateBudConstractConsRpt()
	{
		this.SaveBudConstractConsRpt();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new string[]
		{
			"Update Bud_ConsReport SET BalanceId='",
			this.hdGuid.Value,
			"' WHERE ConsReportId in(",
			this.hfldRptIDs.Value,
			") AND ConstractId='",
			this.hfldContractId.Value,
			"'"
		}));
		this.budConReportSev.ExcuteSql(stringBuilder.ToString());
	}
	public void SaveBudConstractConsRpt()
	{
		List<BudConsTask> list = this.ConvertGridToList();
		string rptId = Guid.NewGuid().ToString();
		if (list.Count > 0)
		{
			BudConsReport budConsReport = (
				from r in this.budConReportSev
				where r.BalanceId == this.hdGuid.Value.ToString() && r.Type == "1" && r.ConstractId == this.hfldContractId.Value.ToString()
				select r).FirstOrDefault<BudConsReport>();
			if (budConsReport != null)
			{
				rptId = budConsReport.ConsReportId;
				using (List<BudConsTask>.Enumerator enumerator = list.GetEnumerator())
				{
					BudConsTask budConsTask;
					while (enumerator.MoveNext())
					{
						budConsTask = enumerator.Current;
						BudConsTask budConsTask3 = (
							from r in this.budConsTaskSev
							where r.ConsReportId == rptId && r.TaskId == budConsTask.TaskId
							select r).FirstOrDefault<BudConsTask>();
						if (budConsTask3 != null)
						{
							budConsTask3.CompleteQuantity += budConsTask.CompleteQuantity;
							budConsTask3.AccountingQuantity = new decimal?(budConsTask3.CompleteQuantity);
							this.budConsTaskSev.Update(budConsTask3);
						}
						else
						{
							budConsTask3 = new BudConsTask();
							budConsTask3.ConsTaskId = Guid.NewGuid().ToString();
							budConsTask3.ConsReportId = rptId;
							budConsTask3.TaskId = budConsTask.TaskId;
							budConsTask3.CompleteQuantity = budConsTask.CompleteQuantity;
							budConsTask3.Note = "";
							budConsTask3.AccountingQuantity = budConsTask.AccountingQuantity;
							this.budConsTaskSev.Add(budConsTask3);
						}
					}
					return;
				}
			}
			BudConsReport budConsReport2 = new BudConsReport();
			budConsReport2.ConsReportId = rptId;
			budConsReport2.InputDate = DateTime.Now;
			budConsReport2.InputUser = PageHelper.QueryUser(this, base.UserCode);
			budConsReport2.IsValid = new bool?(true);
			budConsReport2.PrjId = this.hfldPrjid.Value;
			budConsReport2.FlowState = 1;
			budConsReport2.IsValid = new bool?(true);
			budConsReport2.State = "0";
			budConsReport2.ConstractId = this.hfldContractId.Value;
			budConsReport2.BalanceId = this.hdGuid.Value;
			budConsReport2.WorkCard = string.Empty;
			budConsReport2.Type = "1";
			this.budConReportSev.Add(budConsReport2);
			foreach (BudConsTask current in list)
			{
				BudConsTask budConsTask2 = new BudConsTask();
				budConsTask2.ConsTaskId = Guid.NewGuid().ToString();
				budConsTask2.ConsReportId = rptId;
				budConsTask2.TaskId = current.TaskId;
				budConsTask2.CompleteQuantity = current.CompleteQuantity;
				budConsTask2.Note = "";
				budConsTask2.AccountingQuantity = current.AccountingQuantity;
				this.budConsTaskSev.Add(budConsTask2);
			}
		}
	}
	protected List<BudConsTask> ConvertGridToList()
	{
		List<BudConsTask> list = new List<BudConsTask>();
		foreach (GridViewRow gridViewRow in this.gvTask.Rows)
		{
			bool flag = false;
			BudConsTask budConsTask = new BudConsTask();
			string value = ((HiddenField)gridViewRow.FindControl("hfldTaskId")).Value;
			if (!string.IsNullOrEmpty(value))
			{
				budConsTask.TaskId = value;
				decimal d = Convert.ToDecimal(((TextBox)gridViewRow.FindControl("txtCompleteQuantity")).Text);
				decimal num = Convert.ToDecimal(((HiddenField)gridViewRow.FindControl("hfldCompleteQuantity")).Value);
				budConsTask.CompleteQuantity = 0m;
				if (!d.Equals(num))
				{
					budConsTask.CompleteQuantity = d - num;
					budConsTask.AccountingQuantity = new decimal?(budConsTask.CompleteQuantity);
					flag = true;
				}
				if (flag)
				{
					list.Add(budConsTask);
				}
			}
		}
		return list;
	}
	protected void SaveBalanceStock()
	{
		this.UpdateBalanceStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				this.balanceStockBll.Delete(sqlTransaction, this.ViewState["BalanceId"].ToString());
				this.AddBalanceStock(sqlTransaction);
				sqlTransaction.Commit();
			}
		}
	}
	private void UpdateBalanceStockDataSource()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gvRow = this.gvwPurchaseplanStock.Rows[i];
				DataRow dataRow = dataTable.Rows[i];
				this.UpdateBalanceQuantity(dataRow, gvRow, "ThisTimeArrivaledQuantity", "txtThisTimeArrivaledQuantity");
				decimal d = Convert.ToDecimal(dataRow["sprice"]);
				dataRow["ThisTimeTotal"] = d * Convert.ToDecimal(dataRow["ThisTimeArrivaledQuantity"]);
			}
			this.ViewState["resource"] = dataTable;
		}
	}
	private void UpdateBalanceQuantity(DataRow vsRow, GridViewRow gvRow, string columnName, string controlId)
	{
		Control control = gvRow.FindControl(controlId);
		if (control is TextBox)
		{
			TextBox textBox = control as TextBox;
			if (!string.IsNullOrEmpty(textBox.Text.Trim()))
			{
				vsRow[columnName] = Convert.ToDecimal(textBox.Text.Trim());
				return;
			}
			vsRow[columnName] = 0m;
		}
	}
	private void AddBalanceStock(SqlTransaction trans)
	{
		this.UpdateBalanceStockDataSource();
		DataTable dataTable = this.ViewState["resource"] as DataTable;
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.Rows[i];
			BalanceStockModel balanceStockModel = new BalanceStockModel();
			balanceStockModel.Id = Guid.NewGuid().ToString();
			balanceStockModel.BalanceId = this.ViewState["BalanceId"].ToString();
			balanceStockModel.PurchaseId = dataRow["psid"].ToString();
			balanceStockModel.ArrivaledQuantity = Convert.ToDecimal(dataRow["ThisTimeArrivaledQuantity"].ToString());
			this.balanceStockBll.Add(trans, balanceStockModel);
		}
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
	public void UpdateConBalanceItem()
	{
		this.conBalItemSev.ExcuteSql(" delete Con_BalanceItem where BalanceId='" + this.hdGuid.Value + "'");
		List<ConBalanceItem> conBalanceItemFromViewState = this.GetConBalanceItemFromViewState();
		foreach (ConBalanceItem current in conBalanceItemFromViewState)
		{
			this.conBalItemSev.Add(current);
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


