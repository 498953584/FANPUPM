using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutBalance_BalanceQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PayoutBalance balance = new PayoutBalance();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private ConConfigContractService configSer = new ConConfigContractService();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ViewState["BalanceId"] = base.Request["ic"].ToString();
		}
		else
		{
			this.trSate.Visible = false;
		}
		if (!string.IsNullOrEmpty(base.Request["showAudit"]))
		{
			new DataTable();
		}
		if (!string.IsNullOrEmpty(base.Request["BalanceId"]))
		{
			this.ViewState["BalanceId"] = base.Request["BalanceId"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.contractId = base.Request["ContractId"];
		}
		DataTable balanceStockByContractId = this.purchaseStock.GetBalanceStockByContractId(this.contractId, this.ViewState["BalanceId"].ToString());
		this.ViewState["resource"] = balanceStockByContractId;
		this.DataBindBalanceStock();
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(this.contractId) && !string.IsNullOrEmpty(this.ViewState["BalanceId"].ToString()))
		{
			PayoutBalanceModel model = this.balance.GetModel(this.ViewState["BalanceId"].ToString());
			if (model != null)
			{
				this.contractId = model.ContractID;
			}
		}
		if (!base.IsPostBack)
		{
			this.InintUpdateState();
			this.Literal1.Text = this.FilesBind(1903, this.ViewState["BalanceId"].ToString());
			this.ShowGuideLine();
		}
		DataTable balanceStockByContractId = this.purchaseStock.GetBalanceStockByContractId(this.contractId, this.ViewState["BalanceId"].ToString());
		this.ViewState["resource"] = balanceStockByContractId;
		this.DataBindBalanceStock();
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		string text = "";
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<a href='",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"' target=_blank>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
		}
		return text;
	}
	private void DataBindBalanceStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.lblTitalPurchase.Text = "采购单";
				GridViewUtility.DataBind(this.gvwPurchaseplanStock, dataTable);
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
			this.lblTitalPurchase.Text = "";
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
	private void InitContractInfo(string conId)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(conId);
		if (model != null)
		{
			this.lblContractCode.Text = model.ContractCode;
			this.lblContractName.Text = model.ContractName;
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			string userCode = this.Session["yhdm"].ToString();
			DataTable dataTable = PersonnelAction.QueryPersonnelById(userCode);
			if (dataTable != null && dataTable.Rows.Count == 1)
			{
				this.lblBllProducer.Text = dataTable.Rows[0]["v_xm"].ToString();
			}
			this.lblContractMoney.Text = model.ModifiedMoney.ToString();
			this.lblSignedDate.Text = model.SignDate.Value.ToShortDateString();
			this.LblAPart.Text = model.AName;
			this.LblContractType.Text = model.TypeName;
			this.lblBName.Text = model.CorpName;
			this.lblAddress.Text = model.Address;
		}
	}
	private void InintUpdateState()
	{
		PayoutBalanceModel model = this.balance.GetModel(this.ViewState["BalanceId"].ToString());
		if (model != null)
		{
			this.contractId = model.ContractID;
			this.lblBalanceCode.Text = model.BalanceCode;
			this.lblBalanceMoney.Text = model.BalanceMoney.ToString();
			this.lblBalanceDate.Text = model.BalanceDate.Value.ToShortDateString();
			this.lblBalancePerson.Text = model.BalancePerson;
			this.lblInputPerson.Text = model.InputPerson;
			this.lblInputDate.Text = model.InputDate.Value.ToShortDateString();
			this.lblNotes.Text = model.Notes;
			if (model.BalanceMoney.ToString() != "")
			{
				Convert.ToDecimal(model.BalanceMoney);
			}
			this.lblDiffAmount.Text = this.GetDiffAmount(this.contractId, model.ContainPending).ToString();
			this.lblBalanceObj.Text = model.BalanceObject;
			DataTable table = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=27 and ParentCodeID=0");
			DataTable table2 = Common2.GetTable("dbo.XPM_Basic_CodeList", "where typeId=25 and ParentCodeID=0");
			if (table.Rows.Count > 0)
			{
				foreach (DataRow dataRow in table.Rows)
				{
					if (dataRow["NoteID"].ToString() == model.BalanceMode)
					{
						this.lblBalanceMode.Text = dataRow["CodeName"].ToString();
					}
				}
			}
			if (table2.Rows.Count > 0)
			{
				foreach (DataRow dataRow2 in table2.Rows)
				{
					if (dataRow2["NoteID"].ToString() == model.PayMode)
					{
						this.lblPayMode.Text = dataRow2["CodeName"].ToString();
					}
				}
			}
			this.InitContractInfo(this.contractId);
		}
	}
	private decimal GetDiffAmount(string contractId, bool containPending)
	{
		PayoutContractModel model = new PayoutContract().GetModel(contractId);
		decimal balancedAmount = this.balance.GetBalancedAmount(contractId, containPending);
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
	protected void gvwPurchaseplanStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void ShowGuideLine()
	{
		if (!this.configSer.IsExist(this.contractId) || !(
			from p in this.configSer
			where p.ContractId == this.contractId
			select p).FirstOrDefault<ConConfigContract>().IsBalanceAlarm)
		{
			this.trSate.Visible = false;
			return;
		}
		string text = this.ViewState["BalanceId"].ToString();
		if (!string.IsNullOrEmpty(text))
		{
			PayoutBalanceModel model = this.balance.GetModel(text);
			PayoutContractModel model2 = new PayoutContract().GetModel(model.ContractID);
			decimal balancedAmount = this.balance.GetBalancedAmount(model.ContractID, model.ContainPending);
			this.lblBalancedAmount.Text = balancedAmount.ToString("0.000");
			this.lblModifedAmount.Text = (model2.ModifiedMoney.HasValue ? model2.ModifiedMoney.ToString() : "0.000");
			this.lblBalanceAmount.Text = (model.BalanceMoney.HasValue ? model.BalanceMoney.ToString() : "0.000");
			decimal d = 0m;
			if (model2.ModifiedMoney.HasValue && model.BalanceMoney.HasValue)
			{
				d = model2.ModifiedMoney.Value - balancedAmount;
			}
			decimal num = 0m;
			if (model2.ModifiedMoney.HasValue && model2.ModifiedMoney.Value != 0m)
			{
				num = d / model2.ModifiedMoney.Value;
			}
			this.lblRate.Text = string.Format("{0:P}", num);
			string balanceAmountState = this.balance.GetBalanceAmountState(num, this.contractId);
			this.lblState.Text = balanceAmountState;
			this.SetLableColor(Common2.GetColorByState(balanceAmountState));
		}
	}
	private void SetLableColor(Color c)
	{
		this.lblState.ForeColor = c;
		this.lblBalancedAmount.ForeColor = c;
		this.lblModifedAmount.ForeColor = c;
		this.lblBalanceAmount.ForeColor = c;
		this.lblRate.ForeColor = c;
	}
}


