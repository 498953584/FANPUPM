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
public partial class StockManage_Purchase_PurchaseView : NBasePage, IRequiresSessionState
{
	private readonly Purchase purchase = new Purchase();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private static readonly string resourceDataSourceName = "Resource";
	private string ic = string.Empty;
	private string pcode = string.Empty;
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();

	protected override void OnInit(System.EventArgs e)
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
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			if (!string.IsNullOrEmpty(this.pcode))
			{
				PurchaseModel model;
				if (this.pcode.Contains("["))
				{
					model = this.purchase.GetModel(JsonHelper.GetListFromJson(this.pcode)[0]);
				}
				else
				{
					model = this.purchase.GetModel(this.pcode);
				}
				this.lblPpcode.Text = model.pcode;
				this.DateInTime.Text = model.intime.ToString("yyyy-MM-dd");
				this.txtPerson.Text = model.person;
				this.lblExplain.Text = model.explain;
				if (!string.IsNullOrEmpty(model.Project))
				{
					PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(model.Project);
					if (modelByPrjGuid != null)
					{
						this.lblProject.Text = modelByPrjGuid.PrjName;
					}
					else
					{
						DataTable tableByPrjGuid = this.ptPrjInfo.GetTableByPrjGuid(model.Project);
						if (tableByPrjGuid.Rows.Count > 0)
						{
							this.lblProject.Text = tableByPrjGuid.Rows[0]["prjName"].ToString().Trim();
						}
					}
				}
				if (!string.IsNullOrEmpty(model.contract))
				{
					PayoutContract payoutContract = new PayoutContract();
					PayoutContractModel model2 = payoutContract.GetModel(model.contract);
					if (model2 != null)
					{
						this.lblContract.Text = model2.ContractName;
					}
				}
				DataTable prjID = this.purchaseStock.GetPrjID(this.pcode);
				string strPrjId = prjID.Rows[0][0].ToString();
				DataTable purchaseStockByPscode = this.purchaseStock.GetPurchaseStockByPscode(this.pcode, strPrjId, this.hfldIsWBSRelevance.Value);
				this.ViewState[StockManage_Purchase_PurchaseView.resourceDataSourceName] = purchaseStockByPscode;
				this.DataBindPurchaseplanStock();
				this.DataBindDiff(prjID.Rows[0][1].ToString(), this.pcode);
				this.flAnnx.MID = 1801;
				this.flAnnx.FID = model.pid;
				this.flAnnx.Type = 1;
				this.lblBllProducer.Text = model.person;
				this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			}
		}
	}
	private void DataBindPurchaseplanStock()
	{
		if (this.ViewState[StockManage_Purchase_PurchaseView.resourceDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[StockManage_Purchase_PurchaseView.resourceDataSourceName] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				GridViewUtility.DataBind(this.gvwPurchaseplanStock, dataTable);
				string total = System.Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwPurchaseplanStock, total, 11);
				return;
			}
			this.gvwPurchaseplanStock.DataSource = null;
			this.gvwPurchaseplanStock.DataBind();
		}
	}
	private void DataBindDiff(string contractID, string strpcode)
	{
		DataTable diff = this.purchaseStock.GetDiff(contractID, strpcode, this.hfldIsWBSRelevance.Value);
		this.gvwDiff.DataSource = diff;
		this.gvwDiff.DataBind();
		if (diff.Rows.Count == 0)
		{
			this.diffTitle.Visible = false;
		}
	}
}


