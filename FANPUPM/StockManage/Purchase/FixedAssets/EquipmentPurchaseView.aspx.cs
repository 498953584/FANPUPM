using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Purchase_EquipmentPurchaseView : NBasePage, IRequiresSessionState
{
	private readonly Purchase purchase = new Purchase();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private static readonly string resourceDataSourceName = "Resource";
	private string ic = string.Empty;
	private string pcode = string.Empty;
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ic = base.Request["ic"];
			this.pcode = this.purchase.GetCodeByGuid(this.ic);
		}
		if (!string.IsNullOrEmpty(base.Request["Pcode"]))
		{
			this.pcode = base.Request["Pcode"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			if (!string.IsNullOrEmpty(this.pcode))
			{
				PurchaseModel model = this.purchase.GetModel(this.pcode);
				this.lblPpcode.Text = model.pcode;
				this.DateInTime.Text = model.intime.ToString("yyyy-MM-dd");
				this.txtPerson.Text = model.person;
				this.lblExplain.Text = model.explain;
				if (!string.IsNullOrEmpty(model.contract))
				{
					PayoutContract payoutContract = new PayoutContract();
					PayoutContractModel model2 = payoutContract.GetModel(model.contract);
					if (model2 != null)
					{
						this.lblContract.Text = model2.ContractName;
					}
				}
				this.purchaseStock.GetPrjID(this.pcode);
				DataTable purchaseStockByPscode = this.purchaseStock.GetPurchaseStockByPscode(this.pcode);
				this.ViewState[Equ_Purchase_EquipmentPurchaseView.resourceDataSourceName] = purchaseStockByPscode;
				this.DataBindPurchaseplanStock();
				this.lblBllProducer.Text = model.person;
				this.lblAccessory.Text = StringUtility.FilesBind(model.pid, "EquipmentPurchase");
				this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			}
		}
	}
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState[Equ_Purchase_EquipmentPurchaseView.resourceDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseView.resourceDataSourceName] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				GridViewUtility.DataBind(this.gvwMaterialStock, dataTable);
				string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwMaterialStock, total, 11);
				return;
			}
			this.gvwMaterialStock.DataSource = null;
			this.gvwMaterialStock.DataBind();
		}
	}
}


