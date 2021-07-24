using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_DirectStorageEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string storageCode = string.Empty;
	private cn.justwin.stockBLL.Storage storage = new cn.justwin.stockBLL.Storage();
	private cn.justwin.stockBLL.StorageStock storageStock = new cn.justwin.stockBLL.StorageStock();
	private cn.justwin.stockBLL.Purchase purchase = new cn.justwin.stockBLL.Purchase();
	private cn.justwin.stockBLL.PurchaseStock purhaseStock = new cn.justwin.stockBLL.PurchaseStock();
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private Resource resource = new Resource();
	private readonly string stockDataSourceName = "Stock";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldPid.Value = Guid.NewGuid().ToString();
			string[] resources = new string[]
			{
				""
			};
			DataTable resourceInitialize = this.resource.GetResourceInitialize(resources);
			this.ViewState[this.stockDataSourceName] = resourceInitialize;
			this.gvwStorageStock.DataSource = resourceInitialize;
			this.gvwStorageStock.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		this.UpdateStorageStockDataSource();
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			for (int i = this.gvwStorageStock.Rows.Count - 1; i >= 0; i--)
			{
				GridViewRow gridViewRow = this.gvwStorageStock.Rows[i];
				CheckBox checkBox = gridViewRow.FindControl("chkSelectSingle") as CheckBox;
				if (checkBox.Checked)
				{
					dataTable.Rows.RemoveAt(i);
				}
			}
			dataTable.AcceptChanges();
			this.ViewState[this.stockDataSourceName] = dataTable;
			this.DataBindStorageStock();
		}
	}
	protected void btnBindResource_Click(object sender, EventArgs e)
	{
		this.UpdateStorageStockDataSource();
		DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
		if (dataTable != null)
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			if (array != null)
			{
				DataTable resourceInitialize = this.resource.GetResourceInitialize(array);
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns["ResourceId"]
				};
				resourceInitialize.PrimaryKey = new DataColumn[]
				{
					resourceInitialize.Columns["ResourceId"]
				};
				dataTable.Merge(resourceInitialize, true);
				this.gvwStorageStock.DataSource = dataTable;
				this.gvwStorageStock.DataBind();
				if (dataTable.Rows.Count > 0)
				{
					string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
					GridViewUtility.AddTotalRow(this.gvwStorageStock, total, 11);
				}
				this.DisabledBtnSave();
			}
		}
	}
	private bool IsExists(DataTable target, DataRow row, string primarykey)
	{
		foreach (DataRow dataRow in target.Rows)
		{
			if (dataRow[primarykey].ToString() == row[primarykey].ToString())
			{
				return true;
			}
		}
		return false;
	}
	protected void gvwStorageStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStorageStock.PageIndex = e.NewPageIndex;
		this.DataBindStorageStock();
	}
	protected void gvwStorageStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwStorageStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		this.Add();
	}
	private void DataBindStorageStock()
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			this.gvwStorageStock.DataSource = dataTable;
			this.gvwStorageStock.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwStorageStock, total, 11);
			}
		}
		else
		{
			this.gvwStorageStock.DataSource = null;
			this.gvwStorageStock.DataBind();
		}
		this.DisabledBtnSave();
	}
	protected void DisabledBtnSave()
	{
		if (this.gvwStorageStock.Rows.Count == 0)
		{
			this.btnSave.Enabled = false;
			return;
		}
		this.btnSave.Enabled = true;
	}
	private void Add()
	{
		string tcode = base.Request["tcode"];
		foreach (GridViewRow gridViewRow in this.gvwStorageStock.Rows)
		{
			string text = gridViewRow.Cells[2].Text;
			decimal snumber = 0m;
			decimal sprice = 0m;
			string corp = string.Empty;
			if (gridViewRow.FindControl("txtNumber") is TextBox)
			{
				TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
				snumber = decimal.Parse(textBox.Text.Trim());
			}
			if (gridViewRow.FindControl("txtPrice") is TextBox)
			{
				TextBox textBox2 = gridViewRow.FindControl("txtPrice") as TextBox;
				sprice = Convert.ToDecimal(textBox2.Text.Trim());
			}
			if (gridViewRow.FindControl("hfldCorp") is HiddenField)
			{
				HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
				corp = hiddenField.Value;
			}
			TreasuryStock treasuryStock = new TreasuryStock();
			treasuryStock.Add(new TreasuryStockModel
			{
				tsid = Guid.NewGuid().ToString(),
				scode = text,
				snumber = snumber,
				sprice = sprice,
				tcode = tcode,
				corp = corp,
				isfirst = false,
				intime = DateTime.Now,
				incode = string.Empty,
				Type = "I"
			});
		}
		this.Back();
	}
	protected void Back()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("window.close();").Append(Environment.NewLine);
		stringBuilder.Append("winclose('DirectStorageEdit', 'InitializeStorage.aspx?tcode=" + base.Request["tcode"] + "', true)");
		base.RegisterScript(stringBuilder.ToString());
	}
	private void AddStorage(SqlTransaction trans)
	{
		StorageModel storageModel = new StorageModel();
		storageModel.sid = this.hfldPid.Value;
		storageModel.flowstate = -1;
		storageModel.intime = DateTime.Now;
		storageModel.inflag = false;
		storageModel.annx = string.Empty;
		storageModel.project = this.hfldProject.Value;
		storageModel.isfirst = false;
		storageModel.Supervisor = "";
		storageModel.Trustee = "";
		this.storage.Add(trans, storageModel);
	}
	private void AddStorageStock(SqlTransaction trans)
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			this.UpdateStorageStockDataSource();
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			List<StorageStockModel> list = new List<StorageStockModel>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				list.Add(new StorageStockModel
				{
					ssid = Guid.NewGuid().ToString(),
					scode = dataRow["ResourceCode"].ToString(),
					number = Convert.ToDecimal(dataRow["number"].ToString()),
					sprice = Convert.ToDecimal(dataRow["sprice"].ToString()),
					corp = dataRow["corp"].ToString(),
					intype = string.Empty,
					linkcode = dataRow["linkcode"].ToString(),
					CheckCondition = ""
				});
			}
			this.storageStock.Add(trans, list);
		}
	}
	private void Update()
	{
	}
	private void UpdateStorage(SqlTransaction trans)
	{
		StorageModel model = this.storage.GetModel(this.storageCode);
		model.project = this.hfldProject.Value;
		this.storage.Update(trans, model);
	}
	private void UpdateStorageStock(SqlTransaction trans)
	{
		this.storageStock.DeleteByStorageCode(trans, this.storageCode);
		this.AddStorageStock(trans);
	}
	private void UpdateStorageStockDataSource()
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvwStorageStock.Rows[i];
				if (gridViewRow.FindControl("txtNumber") is TextBox)
				{
					TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
					decimal num = Convert.ToDecimal(textBox.Text.Trim());
					dataRow["number"] = num;
					TextBox textBox2 = gridViewRow.FindControl("txtPrice") as TextBox;
					decimal num2 = Convert.ToDecimal(textBox2.Text.Trim());
					dataRow["price"] = num2;
					dataRow["Total"] = num * num2;
				}
				if (gridViewRow.FindControl("txtCorp") is TextBox)
				{
					TextBox textBox3 = gridViewRow.FindControl("txtCorp") as TextBox;
					dataRow["CorpName"] = textBox3.Text;
				}
				if (gridViewRow.FindControl("hfldCorp") is HiddenField)
				{
					HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
					if (!string.IsNullOrEmpty(hiddenField.Value))
					{
						dataRow["corpId"] = Convert.ToInt32(hiddenField.Value);
					}
				}
			}
			this.ViewState[this.stockDataSourceName] = dataTable;
		}
	}
	private void UpdatePurchaseAccepteState(SqlTransaction trans)
	{
	}
}


