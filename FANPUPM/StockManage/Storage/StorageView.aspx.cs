using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_StorageView : NBasePage, IRequiresSessionState
{
	private string ic = string.Empty;
	private string storageCode = string.Empty;
	private Storage storage = new Storage();
	private StorageStock storageStock = new StorageStock();
	private Purchase purchase = new Purchase();
	private PurchaseStock purhaseStock = new PurchaseStock();
	private Treasury treasury = new Treasury();
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private readonly string stockDataSourceName = "Stock";

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request["ic"] != null)
		{
			this.ic = base.Request["ic"];
			this.storageCode = this.storage.GetCodeFromGuid(this.ic);
		}
		if (base.Request["storageCode"] != null)
		{
			this.storageCode = base.Request["storageCode"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			StorageModel model = this.storage.GetModel(this.storageCode);
			this.lblTitle.Text = (model.isfirst ? "甲供入库" : "入库单");
			this.hfldPid.Value = model.sid;
			this.lblScode.Text = model.scode;
			this.DateInTime.Text = model.intime.ToShortDateString();
			this.lblPerson.Text = model.person;
			this.Literal1.Text = model.Trustee;
			this.Literal2.Text = model.Supervisor;
			this.lblBllProducer.Text = model.person;
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			TreasuryModel model2 = this.treasury.GetModel(model.tcode);
			if (model2 != null)
			{
				this.lblTreasury.Text = model2.tname;
			}
			this.lblExplain.Text = model.explain;
			if (!string.IsNullOrEmpty(model.project))
			{
				this.hfldProject.Value = model.project;
				PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(model.project);
				if (modelByPrjGuid != null)
				{
					this.lblProject.Text = modelByPrjGuid.PrjName;
				}
				else
				{
					DataTable tableByPrjGuid = this.ptPrjInfo.GetTableByPrjGuid(model.project);
					if (tableByPrjGuid.Rows.Count > 0)
					{
						this.lblProject.Text = tableByPrjGuid.Rows[0]["prjName"].ToString().Trim();
					}
				}
			}
			DataTable storageStockDataSource = this.storageStock.GetStorageStockDataSource(this.storageCode);
			this.ViewState[this.stockDataSourceName] = storageStockDataSource;
			this.gvwStorageStock.DataSource = storageStockDataSource;
			this.gvwStorageStock.DataBind();
			if (this.lblTitle.Text.Trim() == "入库单")
			{
				this.trd.Visible = false;
				if (storageStockDataSource.Rows.Count > 0)
				{
					string total = System.Convert.ToDecimal(storageStockDataSource.Compute("SUM(Total)", string.Empty)).ToString("0.000");
					GridViewUtility.AddTotalRow(this.gvwStorageStock, total, 12);
				}
				else
				{
					this.gvwStorageStock.DataSource = null;
					this.gvwStorageStock.DataBind();
				}
			}
			else
			{
				if (this.lblTitle.Text.Trim() == "甲供入库")
				{
					this.trd.Visible = true;
					this.gvwStorageStock.Columns[8].Visible = false;
					this.gvwStorageStock.Columns[9].Visible = false;
					this.gvwStorageStock.Columns[11].Visible = false;
					this.gvwStorageStock.Columns[12].Visible = false;
				}
			}
			if (storageStockDataSource.Rows.Count > 0)
			{
				this.hfldPurchaseCode.Value = storageStockDataSource.Rows[0]["linkcode"].ToString();
			}
		}
		this.flAnnx.MID = 1802;
		this.flAnnx.FID = this.hfldPid.Value;
		this.flAnnx.Type = 1;
	}
}


