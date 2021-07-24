using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_FirstStorageEdit : NBasePage, IRequiresSessionState
{
	private readonly Treasury treasury = new Treasury();
	private string action = string.Empty;
	private string storageCode = string.Empty;
	private SmEnum.SmSetValue manageMode;
	private Storage storage = new Storage();
	private StorageStock storageStock = new StorageStock();
	private Purchase purchase = new Purchase();
	private PurchaseStock purhaseStock = new PurchaseStock();
	private PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private string stockDataSourceName = "Stock";

	protected override void OnInit(System.EventArgs e)
	{
		this.manageMode = this.treasury.GetManageMode();
		if (base.Request["Action"] != null)
		{
			this.action = base.Request["Action"];
		}
		if (base.Request["StorageCode"] != null)
		{
			if (base.Request["StorageCode"].Contains("["))
			{
				this.storageCode = JsonHelper.GetListFromJson(base.Request["StorageCode"])[0];
			}
			else
			{
				this.storageCode = base.Request["StorageCode"];
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.action.Equals("Add"))
			{
				this.hfldPid.Value = System.Guid.NewGuid().ToString();
				this.txtScode.Text = System.DateTime.Now.ToString("yyyyMMddHHmmss");
				this.txtInputDate.Text = System.DateTime.Today.ToShortDateString();
				this.txtPerson.Text = PageHelper.QueryUser(this, base.UserCode);
				DataTable storageStockDataSource = this.storageStock.GetStorageStockDataSource(string.Empty);
				this.ViewState[this.stockDataSourceName] = storageStockDataSource;
				this.gvwStorageStock.DataSource = storageStockDataSource;
				this.gvwStorageStock.DataBind();
				if (this.manageMode != SmEnum.SmSetValue.ParallelMode)
				{
					string totalCode = this.treasury.GetTotalCode();
					this.hfldTrea.Value = totalCode;
					this.txtTrea.Text = this.GetTreasuryNameByCode(totalCode);
				}
			}
			else
			{
				StorageModel model = this.storage.GetModel(this.storageCode);
				this.hfldPid.Value = model.sid;
				this.txtScode.Text = model.scode;
				this.txtInputDate.Text = model.intime.ToShortDateString();
				this.txtPerson.Text = model.person;
				this.hfldTrea.Value = model.tcode;
				this.txtTrea.Text = this.GetTreasuryNameByCode(model.tcode);
				this.txtExplain.Text = model.explain;
				this.txttrustee.Text = model.Trustee;
				this.txtsupervisor.Text = model.Supervisor;
				this.hfldIsFirst.Value = (model.isfirst ? "1" : "0");
				if (!string.IsNullOrEmpty(model.project))
				{
					this.hfldProject.Value = model.project;
					PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(model.project);
					if (modelByPrjGuid != null)
					{
						this.txtProject.Value = modelByPrjGuid.PrjName;
					}
				}
				DataTable storageStockDataSource2 = this.storageStock.GetStorageStockDataSource(this.storageCode);
				this.ViewState[this.stockDataSourceName] = storageStockDataSource2;
				this.gvwStorageStock.DataSource = storageStockDataSource2;
				this.gvwStorageStock.DataBind();
				if (storageStockDataSource2.Rows.Count > 0)
				{
					this.hfldPurchaseCode.Value = storageStockDataSource2.Rows[0]["linkcode"].ToString();
				}
			}
		}
		this.flAnnx.MID = 1806;
		this.flAnnx.FID = this.hfldPid.Value;
		this.flAnnx.Type = 1;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.action.Equals("Add"))
		{
			this.Add();
			return;
		}
		this.Update();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldResourceCode.Value);
		this.UpdateDataSource();
		this.DeleteResource(listFromJson);
		this.DataBindStorageStock();
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
		dataTable.PrimaryKey = new DataColumn[]
		{
			dataTable.Columns["ResourceId"]
		};
		if (dataTable != null && !string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(this.hfldResourceId.Value);
			if (array != null)
			{
				Resource resource = new Resource();
				DataTable resource2 = resource.GetResource(array);
				DataColumn dataColumn = new DataColumn("sprice", typeof(decimal));
				dataColumn.DefaultValue = 0m;
				DataColumn dataColumn2 = new DataColumn("number", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				DataColumn column = new DataColumn("Total", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				DataColumn column2 = new DataColumn("corp", typeof(string));
				resource2.Columns.Add(dataColumn);
				resource2.Columns.Add(dataColumn2);
				resource2.Columns.Add(column);
				resource2.Columns.Add(column2);
				foreach (DataRow dataRow in resource2.Rows)
				{
					dataRow["corp"] = dataRow["corpId"];
				}
				resource2.PrimaryKey = new DataColumn[]
				{
					resource2.Columns["ResourceId"]
				};
				dataTable.Merge(resource2, true);
				this.DataBindStorageStock();
			}
		}
	}
	protected void gvwStorageStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwStorageStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void DataBindStorageStock()
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			DataTable dataSource = this.ViewState[this.stockDataSourceName] as DataTable;
			this.gvwStorageStock.DataSource = dataSource;
			this.gvwStorageStock.DataBind();
		}
	}
	private void Add()
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.AddStorage(sqlTransaction);
				this.AddStorageStock(sqlTransaction);
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("alert('系统提示：\\n\\n添加成功！');").Append(System.Environment.NewLine);
				stringBuilder.Append("window.close();").Append(System.Environment.NewLine);
				stringBuilder.Append("winclose('FirstStorageEdit', 'FirstStorage.aspx', true)");
				base.RegisterScript(stringBuilder.ToString());
				sqlTransaction.Commit();
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n添加失败！');");
			}
		}
	}
	private void AddStorage(SqlTransaction trans)
	{
		StorageModel storageModel = new StorageModel();
		storageModel.sid = this.hfldPid.Value;
		storageModel.scode = this.txtScode.Text.Trim();
		storageModel.tcode = this.hfldTrea.Value;
		storageModel.flowstate = -1;
		storageModel.person = this.txtPerson.Text.Trim();
		storageModel.intime = System.DateTime.Now;
		storageModel.inflag = false;
		storageModel.annx = string.Empty;
		storageModel.explain = this.txtExplain.Text.Trim();
		storageModel.project = this.hfldProject.Value;
		storageModel.isfirst = true;
		storageModel.Supervisor = this.txtsupervisor.Text.Trim().ToString();
		storageModel.Trustee = this.txttrustee.Text.Trim().ToString();
		this.storage.Add(trans, storageModel);
	}
	private void AddStorageStock(SqlTransaction trans)
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			this.UpdateDataSource();
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			System.Collections.Generic.List<StorageStockModel> list = new System.Collections.Generic.List<StorageStockModel>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				list.Add(new StorageStockModel
				{
					ssid = System.Guid.NewGuid().ToString(),
					scode = dataRow["ResourceCode"].ToString(),
					stcode = this.txtScode.Text.Trim(),
					number = System.Convert.ToDecimal(dataRow["number"].ToString()),
					sprice = System.Convert.ToDecimal(dataRow["sprice"].ToString()),
					corp = dataRow["corp"].ToString(),
					intype = string.Empty,
					linkcode = this.hfldPurchaseCode.Value,
					CheckCondition = dataRow["checkCondition"].ToString()
				});
			}
			this.storageStock.Add(trans, list);
		}
	}
	private void Update()
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.UpdateStorage(sqlTransaction);
				this.UpdateStorageStock(sqlTransaction);
				sqlTransaction.Commit();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("alert('系统提示：\\n\\n修改成功！');").Append(System.Environment.NewLine);
				stringBuilder.Append("window.close();").Append(System.Environment.NewLine);
				stringBuilder.Append("winclose('FirstStorageEdit', 'FirstStorage.aspx', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (System.Exception)
			{
				base.RegisterScript("alert('系统提示：\\n\\n修改失败！');");
			}
		}
	}
	private void UpdateStorage(SqlTransaction trans)
	{
		StorageModel model = this.storage.GetModel(this.storageCode);
		model.tcode = this.hfldTrea.Value;
		model.person = this.txtPerson.Text.Trim();
		model.explain = this.txtExplain.Text.Trim();
		model.project = this.hfldProject.Value;
		model.isfirst = !(this.hfldIsFirst.Value == "0");
		model.Trustee = this.txttrustee.Text.Trim().ToString();
		model.Supervisor = this.txtsupervisor.Text.Trim().ToString();
		this.storage.Update(trans, model);
	}
	private void UpdateStorageStock(SqlTransaction trans)
	{
		this.storageStock.DeleteByStorageCode(trans, this.storageCode);
		this.AddStorageStock(trans);
	}
	private void UpdateDataSource()
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
					dataRow["number"] = (string.IsNullOrEmpty(textBox.Text.Trim()) ? "0.0" : textBox.Text.Trim());
				}
				if (gridViewRow.FindControl("txtPrice") is TextBox)
				{
					TextBox textBox2 = gridViewRow.FindControl("txtPrice") as TextBox;
					dataRow["sprice"] = (string.IsNullOrEmpty(textBox2.Text.Trim()) ? "0.0" : textBox2.Text.Trim());
				}
				dataRow["Total"] = System.Convert.ToDecimal(dataRow["number"]) * System.Convert.ToDecimal(dataRow["sprice"]);
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
				if (gridViewRow.FindControl("txtcheckCondition") is TextBox)
				{
					TextBox textBox4 = gridViewRow.FindControl("txtcheckCondition") as TextBox;
					dataRow["checkCondition"] = textBox4.Text.Trim().ToString();
				}
			}
			this.ViewState[this.stockDataSourceName] = dataTable;
		}
	}
	private void DeleteResource(System.Collections.Generic.List<string> lstResourceID)
	{
		if (this.ViewState[this.stockDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[this.stockDataSourceName] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (lstResourceID.Contains(dataTable.Rows[i]["ResourceCode"].ToString()))
				{
					dataTable.Rows.RemoveAt(i);
					i--;
				}
			}
			dataTable.AcceptChanges();
			this.ViewState[this.stockDataSourceName] = dataTable;
		}
	}
	private string GetTreasuryNameByCode(string code)
	{
		string result = string.Empty;
		SmTreasuryService smTreasuryService = new SmTreasuryService();
		SmTreasury byCode = smTreasuryService.GetByCode(code);
		if (byCode != null)
		{
			result = byCode.tname;
		}
		return result;
	}
}


