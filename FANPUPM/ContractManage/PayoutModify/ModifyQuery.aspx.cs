using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutModify_ModifyQuery : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractId = string.Empty;
	private PayoutModify payoutModify = new PayoutModify();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private readonly ModifyStockBll modifyStock = new ModifyStockBll();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ViewState["ModifyId"] = base.Request["ic"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["showAudit"]))
		{
			new DataTable();
		}
		if (!string.IsNullOrEmpty(base.Request["ModifyId"]))
		{
			this.ViewState["ModifyId"] = base.Request["ModifyId"];
		}
		if (!string.IsNullOrEmpty(base.Request["ContractId"]))
		{
			this.contractId = base.Request["ContractId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InintUpdateState();
			this.InitProjectInfo(this.ViewState["ModifyId"].ToString());
			DataTable value = new DataTable();
			value = this.purchaseStock.GetModifyStockByContractId(this.contractId, this.ViewState["ModifyId"].ToString());
			this.ViewState["resource"] = value;
			this.DataBindPurchaseplanStock();
			this.Literal1.Text = this.FilesBind(1902, this.ViewState["ModifyId"].ToString());
		}
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
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState["resource"] is DataTable)
		{
			DataTable dataTable = this.ViewState["resource"] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.lblTitalPurchase.Text = "采购单";
				GridViewUtility.DataBind(this.gvwPurchaseplanStock, dataTable);
				string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwPurchaseplanStock, total, 8);
				return;
			}
			this.lblTitalPurchase.Text = "";
		}
	}
	protected void InitProjectInfo(string modifyId)
	{
		PayoutModifyModel payoutModifyModel = new PayoutModifyModel();
		payoutModifyModel = this.payoutModify.GetModel(modifyId);
		if (payoutModifyModel != null)
		{
			PayoutContractModel payoutContractModel = new PayoutContractModel();
			PayoutContract payoutContract = new PayoutContract();
			payoutContractModel = payoutContract.GetModel(payoutModifyModel.ContractID);
			if (payoutContractModel.PrjGuid != "")
			{
				string sqlString = " select PrjCode,PrjName,PrjGuid,TypeCode from pt_prjinfo where prjGuid='" + payoutContractModel.PrjGuid + "' ";
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					this.LblProjectCode.Text = dataTable.Rows[0]["PrjCode"].ToString();
					this.LblProjectName.Text = dataTable.Rows[0]["PrjName"].ToString();
				}
			}
		}
	}
	private void InitContractInfo(string conId)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(conId);
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
	}
	private void InintUpdateState()
	{
		PayoutModifyModel model = this.payoutModify.GetModel(this.ViewState["ModifyId"].ToString());
		if (model != null)
		{
			this.contractId = model.ContractID;
			this.lblModifyCode.Text = model.ModifyCode;
			this.lblModifyMoney.Text = model.ModifyMoney.ToString();
			this.lblModifyDate.Text = model.ModifyDate.Value.ToShortDateString();
			this.lblModifyPerson.Text = model.ModifyPerson;
			this.lblInputPerson.Text = model.InputPerson;
			this.lblInputDate.Text = model.InputDate.Value.ToShortDateString();
			this.lblModifyReason.Text = model.Reason;
			this.lblNotes.Text = model.Notes;
			if (model.ModifyMoney.ToString() != "")
			{
				Convert.ToDecimal(model.ModifyMoney);
			}
			this.InitContractInfo(this.contractId);
		}
	}
}


