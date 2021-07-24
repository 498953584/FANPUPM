using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
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
public partial class Equ_Purchase_EquipmentPurchaseEdit : NBasePage, IRequiresSessionState
{
	private readonly Purchase purchase = new Purchase();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private readonly ContractMain contractMain = new ContractMain();
	private static readonly string resourceDataSourceName = "Resource";
	private string action = string.Empty;
	private string pcode = string.Empty;
	private string pid = string.Empty;
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private SmPurchaseplanStockBll purchaseplanStockBll = new SmPurchaseplanStockBll();
	private int alreadyNumberCount;
	public static string strBName = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["pid"]))
		{
			this.pid = base.Request["pid"];
			this.pcode = this.purchase.GetCodeByGuid(this.pid);
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.action.Equals("Add"))
			{
				this.txtPpcode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.txtInputDate.Text = DateTime.Today.ToShortDateString();
				this.txtPerson.Text = PageHelper.QueryUser(this, base.UserCode);
				this.hfldPid.Value = Guid.NewGuid().ToString();
				this.gvwMaterialStock.DataSource = null;
				this.gvwMaterialStock.DataBind();
			}
			else
			{
				if (!string.IsNullOrEmpty(this.pcode))
				{
					PurchaseModel model = this.purchase.GetModel(this.pcode);
					this.hfldPid.Value = model.pid;
					this.txtPpcode.Text = model.pcode;
					this.txtInputDate.Text = model.intime.ToShortDateString();
					this.txtPerson.Text = model.person;
					if (!string.IsNullOrEmpty(model.contract))
					{
						this.hfldContract.Value = model.contract;
						this.txtContract.Value = this.GetContractName(model.contract);
					}
					this.txtExplain.Text = model.explain;
					DataTable purchaseStockByPscode = this.purchaseStock.GetPurchaseStockByPscode(this.pcode);
					this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = purchaseStockByPscode;
					if (purchaseStockByPscode.Rows.Count > 0)
					{
						this.hfldBName.Value = purchaseStockByPscode.Rows[0]["corp"].ToString();
					}
					this.DataBindGvwMaterialStock();
				}
			}
		}
		this.FileUpload1.RecordCode = this.hfldPid.Value;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.action.Equals("Add"))
		{
			this.Add();
		}
		if (this.action.Equals("Update"))
		{
			this.Update();
		}
	}
	protected void gvwMaterialStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwMaterialStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldMaterialChecked.Value);
		this.UpdateGvwMaterialStockDataSource();
		this.DeleteResource(listFromJson);
		this.DataBindGvwMaterialStock();
	}
	private void DataBindGvwMaterialStock()
	{
		if (this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
			if (dataTable.Rows.Count > 0)
			{
				this.gvwMaterialStock.DataSource = dataTable;
				this.gvwMaterialStock.DataBind();
				string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
				GridViewUtility.AddTotalRow(this.gvwMaterialStock, total, 12);
				return;
			}
			this.gvwMaterialStock.DataSource = dataTable;
			this.gvwMaterialStock.DataBind();
		}
	}
	protected void btnBindResource_Click(object sender, EventArgs e)
	{
		this.InitResource(this.hfldResourceId.Value);
	}
	private void DeleteResource(List<string> lstResourceID)
	{
		if (this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (lstResourceID.Contains(dataTable.Rows[i]["ResourceCode"].ToString()))
				{
					dataTable.Rows.RemoveAt(i);
					i--;
				}
			}
			dataTable.AcceptChanges();
			this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = dataTable;
		}
	}
	private void AddPurchase(SqlTransaction trans)
	{
		PurchaseModel purchaseModel = new PurchaseModel();
		purchaseModel.pid = this.hfldPid.Value;
		purchaseModel.pcode = this.txtPpcode.Text.Trim();
		purchaseModel.contract = this.hfldContract.Value;
		purchaseModel.flowstate = -1;
		purchaseModel.person = this.txtPerson.Text.Trim();
		purchaseModel.intime = DateTime.Now;
		purchaseModel.acceptstate = 0;
		purchaseModel.annx = string.Empty;
		purchaseModel.explain = this.txtExplain.Text.Trim();
		purchaseModel.PurchaseType = 1;
		this.purchase.Add(trans, purchaseModel);
	}
	private void UpdatePurchase(SqlTransaction trans)
	{
		PurchaseModel model = this.purchase.GetModel(this.txtPpcode.Text.Trim());
		model.contract = this.hfldContract.Value;
		model.person = this.txtPerson.Text.Trim();
		model.annx = string.Empty;
		model.explain = this.txtExplain.Text.Trim();
		this.purchase.Update(trans, model);
	}
	private void AddPurchaseStock(SqlTransaction trans)
	{
		this.UpdateGvwMaterialStockDataSource();
		DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
		List<PurchaseStockModel> list = new List<PurchaseStockModel>();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.Rows[i];
			list.Add(new PurchaseStockModel
			{
				psid = Guid.NewGuid().ToString(),
				scode = dataRow["ResourceCode"].ToString(),
				pscode = this.txtPpcode.Text.Trim(),
				number = Convert.ToDecimal(dataRow["number"]),
				sprice = Convert.ToDecimal(dataRow["sprice"]),
				corp = dataRow["corp"].ToString(),
				ArrivalDate = dataRow["arrivalDate"].ToString()
			});
		}
		this.purchaseStock.Add(trans, list);
	}
	private void InitResource(string resources)
	{
		if (!string.IsNullOrEmpty(resources))
		{
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(resources);
			if (array != null)
			{
				string value = this.hidenppcode.Value;
				Resource resource = new Resource();
				DataTable dataTable = new DataTable();
				if (!string.IsNullOrEmpty(value) && value.Length > 2)
				{
					dataTable = resource.GetResourceByPpcodes(array, value);
					this.hidenppcode.Value = "";
				}
				else
				{
					dataTable = resource.GetResourceNew(array);
				}
				DataColumn dataColumn = new DataColumn("sprice", typeof(decimal));
				dataColumn.DefaultValue = 0m;
				DataColumn dataColumn2 = new DataColumn("number", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				DataColumn dataColumn3 = new DataColumn("Total", typeof(decimal));
				dataColumn3.DefaultValue = 0m;
				dataTable.Columns.Add(dataColumn2);
				DataColumn column = new DataColumn("arrivalDate", typeof(DateTime));
				dataTable.Columns.Add(column);
				List<string> resourceNumber = this.GetResourceNumber();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					DataRow dataRow = dataTable.Rows[i];
					try
					{
						dataRow["number"] = Convert.ToDecimal(resourceNumber[i]);
						dataRow["arrivalDate"] = this.purchaseplanStockBll.GetMinArrivalDate(dataRow["ResourceCode"].ToString(), value).ToString();
					}
					catch
					{
					}
				}
				dataTable.Columns.Add(dataColumn);
				dataTable.Columns.Add(dataColumn3);
				DataColumn dataColumn4 = new DataColumn("corp", typeof(string));
				dataColumn4.DefaultValue = this.GetCorpID(this.hfldContract.Value);
				dataTable.Columns.Add(dataColumn4);
				DataColumn dataColumn5 = new DataColumn("CorpName", typeof(string));
				dataColumn5.DefaultValue = this.GetCorpName(this.hfldContract.Value);
				dataTable.Columns.Add(dataColumn5);
				if (this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] == null)
				{
					this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = dataTable;
				}
				else
				{
					DataTable dataTable2 = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
					for (int j = 0; j < dataTable2.Rows.Count; j++)
					{
						DataRow dataRow2 = dataTable2.Rows[j];
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
					dataTable2.PrimaryKey = new DataColumn[]
					{
						dataTable2.Columns["ResourceId"]
					};
					dataTable.PrimaryKey = new DataColumn[]
					{
						dataTable.Columns["ResourceId"]
					};
					object arg_340_0 = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName];
					dataTable2.Merge(dataTable, true);
				}
				DataTable dataTable3 = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
				if (dataTable3.Rows.Count > 0)
				{
					bool flag = true;
					try
					{
						Convert.ToInt32(this.hfldBName.Value);
						flag = true;
					}
					catch
					{
						flag = false;
					}
					if (flag)
					{
						if (this.GetID(this.hfldBName.Value.Trim()) != "")
						{
							Equ_Purchase_EquipmentPurchaseEdit.strBName = this.GetID(this.hfldBName.Value.Trim());
						}
						else
						{
							Equ_Purchase_EquipmentPurchaseEdit.strBName = this.hfldBName.Value.Trim();
						}
						for (int l = 0; l < dataTable3.Rows.Count; l++)
						{
							dataTable3.Rows[l]["corp"] = this.hfldBName.Value;
							dataTable3.Rows[l]["CorpName"] = Equ_Purchase_EquipmentPurchaseEdit.strBName;
						}
					}
					else
					{
						if (this.GetName(this.hfldBName.Value.Trim()) != "")
						{
							Equ_Purchase_EquipmentPurchaseEdit.strBName = this.GetName(this.hfldBName.Value.Trim());
						}
						else
						{
							Equ_Purchase_EquipmentPurchaseEdit.strBName = this.hfldBName.Value.Trim();
						}
						for (int m = 0; m < dataTable3.Rows.Count; m++)
						{
							dataTable3.Rows[m]["corp"] = Equ_Purchase_EquipmentPurchaseEdit.strBName;
							dataTable3.Rows[m]["CorpName"] = this.hfldBName.Value;
						}
					}
				}
				this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = dataTable3;
				this.DataBindGvwMaterialStock();
			}
		}
	}
	public void BindGvw()
	{
		DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			bool flag = true;
			try
			{
				Convert.ToInt32(this.hfldBName.Value);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			if (flag)
			{
				if (this.GetID(this.hfldBName.Value.Trim()) != "")
				{
					Equ_Purchase_EquipmentPurchaseEdit.strBName = this.GetID(this.hfldBName.Value.Trim());
				}
				else
				{
					Equ_Purchase_EquipmentPurchaseEdit.strBName = this.hfldBName.Value.Trim();
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					dataTable.Rows[i]["corp"] = this.hfldBName.Value;
					dataTable.Rows[i]["CorpName"] = Equ_Purchase_EquipmentPurchaseEdit.strBName;
				}
			}
			else
			{
				if (this.GetName(this.hfldBName.Value.Trim()) != "")
				{
					Equ_Purchase_EquipmentPurchaseEdit.strBName = this.GetName(this.hfldBName.Value.Trim());
				}
				else
				{
					Equ_Purchase_EquipmentPurchaseEdit.strBName = this.hfldBName.Value.Trim();
				}
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					dataTable.Rows[j]["corp"] = Equ_Purchase_EquipmentPurchaseEdit.strBName;
					dataTable.Rows[j]["CorpName"] = this.hfldBName.Value;
				}
			}
		}
		this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = dataTable;
		this.DataBindGvwMaterialStock();
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
	private void UpdatePurchaseStock(SqlTransaction trans)
	{
		this.purchaseStock.DeleteByPscode(trans, this.pcode);
		this.AddPurchaseStock(trans);
	}
	public void Add()
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.AddPurchase(sqlTransaction);
				this.AddPurchaseStock(sqlTransaction);
				this.UpdateGvwMaterialStock(sqlTransaction);
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("top.ui.show('添加成功');").Append(Environment.NewLine);
				stringBuilder.Append("winclose('EquipmentPurchaseEdit', 'EquipmentPurchase.aspx?PrjGuid=" + this.hfldProject.Value + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				base.Trace.Write(ex.Message);
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('添加失败');");
			}
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
				this.UpdatePurchase(sqlTransaction);
				this.UpdatePurchaseStock(sqlTransaction);
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("top.ui.show('修改成功');").Append(Environment.NewLine);
				stringBuilder.Append("winclose('EquipmentPurchaseEdit', 'EquipmentPurchase.aspx', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('修改失败');");
			}
		}
	}
	private void UpdateGvwMaterialStock(SqlTransaction trans)
	{
		if (!string.IsNullOrEmpty(this.hfldPurchaseplan.Value))
		{
			string[] array = JsonHelper.GetListFromJson(this.hfldPurchaseplan.Value).ToArray();
			SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string ppcode = array2[i];
				smPurchaseplanBll.UpdateAcceptState(trans, ppcode);
			}
		}
	}
	private void UpdateGvwMaterialStockDataSource()
	{
		if (this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] is DataTable)
		{
			DataTable dataTable = this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] as DataTable;
			new List<PurchaseStockModel>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvwMaterialStock.Rows[i];
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
				if (gridViewRow.FindControl("txtarrivalDate") is TextBox)
				{
					TextBox textBox4 = gridViewRow.FindControl("txtarrivalDate") as TextBox;
					if (textBox4.Text != "" && textBox4.Text.ToString().Length > 1)
					{
						dataRow["arrivalDate"] = textBox4.Text;
					}
					else
					{
						dataRow["arrivalDate"] = DBNull.Value;
					}
				}
			}
			this.ViewState[Equ_Purchase_EquipmentPurchaseEdit.resourceDataSourceName] = dataTable;
		}
	}
	private string GetContractName(string contractID)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(contractID);
		if (model == null)
		{
			return string.Empty;
		}
		return model.ContractName;
	}
	private string GetCorpID(string contractID)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CAST(CorpId AS nvarchar(200))=(select BName from Con_Payout_Contract where ContractID='" + contractID + "')");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpID"].ToString();
		}
		return string.Empty;
	}
	private string GetCorpName(string contractID)
	{
		string corpID = this.GetCorpID(contractID);
		if (corpID == string.Empty)
		{
			return string.Empty;
		}
		return Common2.GetTable("XPM_Basic_ContactCorp", "where CorpId='" + corpID + "'").Rows[0]["CorpName"].ToString();
	}
	private string GetID(string corpName)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpID='" + corpName + "'");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpName"].ToString();
		}
		return string.Empty;
	}
	private string GetName(string corpName)
	{
		DataTable table = Common2.GetTable("XPM_Basic_ContactCorp", "where CorpName='" + corpName + "'");
		if (table.Rows.Count > 0)
		{
			return table.Rows[0]["CorpID"].ToString();
		}
		return string.Empty;
	}
}


