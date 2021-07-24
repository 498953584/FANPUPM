using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Repair_RepairReportEdit : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();
	private EquRepairStockService stockSer = new EquRepairStockService();
	private string action = string.Empty;
	private string id = string.Empty;
	private string equipmentType = "0";

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["equipmentType"]))
		{
			this.equipmentType = base.Request["equipmentType"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldEquipmentType.Value = this.equipmentType;
			this.BindReport();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if (this.action == "add")
			{
				this.reportSer.Add(this.GetModel(null));
				this.SaveStocks();
				stringBuilder.Append("top.ui.show('添加成功');").Append(System.Environment.NewLine);
			}
			else
			{
				EquRepairReport byId = this.reportSer.GetById(this.id);
				this.reportSer.Update(this.GetModel(byId));
				this.SaveStocks();
				stringBuilder.Append("top.ui.show('编辑成功');").Append(System.Environment.NewLine);
			}
			stringBuilder.Append("winclose('RepairReportEdit', 'RepairReportList.aspx?equipmentType=" + this.equipmentType + "', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	protected void btnBindResource_Click(object sender, System.EventArgs e)
	{
		this.SelectedStock(this.hfldResourceId.Value);
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldStockIdChecked.Value);
		this.UpdateRepairStockDataSource();
		this.DeleteResource(listFromJson);
		this.BindGV();
	}
	protected void gvRepairStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRepairStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void SelectedStock(string resources)
	{
		if (!string.IsNullOrEmpty(resources))
		{
			this.UpdateRepairStockDataSource();
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(resources);
			if (array != null)
			{
				Resource resource = new Resource();
				DataTable dataTable = new DataTable();
				dataTable = resource.GetResourceNew(array);
				DataColumn dataColumn = new DataColumn("UnitPrice", typeof(decimal));
				dataColumn.DefaultValue = 0m;
				dataTable.Columns.Add(dataColumn);
				DataColumn dataColumn2 = new DataColumn("Quantity", typeof(decimal));
				dataColumn2.DefaultValue = 0m;
				dataTable.Columns.Add(dataColumn2);
				DataColumn dataColumn3 = new DataColumn("Total", typeof(decimal));
				dataColumn3.DefaultValue = 0m;
				dataTable.Columns.Add(dataColumn3);
				DataColumn dataColumn4 = new DataColumn("corpId", typeof(string));
				dataColumn4.DefaultValue = "";
				dataTable.Columns.Add(dataColumn4);
				DataColumn dataColumn5 = new DataColumn("CorpName", typeof(string));
				dataColumn5.DefaultValue = "";
				dataTable.Columns.Add(dataColumn5);
				DataColumn dataColumn6 = new DataColumn("ReceivePerson", typeof(string));
				dataColumn6.DefaultValue = "";
				dataTable.Columns.Add(dataColumn6);
				DataColumn column = new DataColumn("ReceiveDate", typeof(System.DateTime));
				dataTable.Columns.Add(column);
				if (this.ViewState["Stock"] == null)
				{
					this.ViewState["Stock"] = dataTable;
				}
				else
				{
					DataTable dataTable2 = this.ViewState["Stock"] as DataTable;
					dataTable2.PrimaryKey = new DataColumn[]
					{
						dataTable2.Columns["ResourceId"]
					};
					dataTable.PrimaryKey = new DataColumn[]
					{
						dataTable.Columns["ResourceId"]
					};
					object arg_21E_0 = this.ViewState["Stock"];
					dataTable2.Merge(dataTable, true);
				}
				DataTable value = this.ViewState["Stock"] as DataTable;
				this.ViewState["Stock"] = value;
				this.BindGV();
			}
		}
	}
	private void UpdateRepairStockDataSource()
	{
		if (this.ViewState["Stock"] is DataTable)
		{
			DataTable dataTable = this.ViewState["Stock"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvRepairStock.Rows[i];
				DataRow dataRow = dataTable.Rows[i];
				if (gridViewRow.FindControl("txtNumber") is TextBox)
				{
					TextBox textBox = gridViewRow.FindControl("txtNumber") as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text.Trim()))
					{
						dataRow["Quantity"] = System.Convert.ToDecimal(textBox.Text.Trim());
					}
					else
					{
						dataRow["Quantity"] = 0m;
					}
				}
				if (gridViewRow.FindControl("txtSprice") is TextBox)
				{
					TextBox textBox2 = gridViewRow.FindControl("txtSprice") as TextBox;
					if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
					{
						dataRow["UnitPrice"] = textBox2.Text.Trim();
					}
					else
					{
						dataRow["UnitPrice"] = 0m;
					}
				}
				dataRow["Total"] = System.Convert.ToDecimal(dataRow["Quantity"]) * System.Convert.ToDecimal(dataRow["UnitPrice"]);
				if (gridViewRow.FindControl("hfldCorp") is HiddenField)
				{
					HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
					dataRow["CorpId"] = hiddenField.Value;
				}
				if (gridViewRow.FindControl("txtCorp") is HtmlInputText)
				{
					HtmlInputText htmlInputText = gridViewRow.FindControl("txtCorp") as HtmlInputText;
					dataRow["CorpName"] = htmlInputText.Value.Trim();
				}
				if (gridViewRow.FindControl("hfldReceivePerson") is HiddenField)
				{
					HiddenField hiddenField2 = gridViewRow.FindControl("hfldReceivePerson") as HiddenField;
					dataRow["ReceivePerson"] = hiddenField2.Value;
				}
				if (gridViewRow.FindControl("txtReceiveDate") is TextBox)
				{
					TextBox textBox3 = gridViewRow.FindControl("txtReceiveDate") as TextBox;
					if (!string.IsNullOrEmpty(textBox3.Text.Trim()))
					{
						dataRow["ReceiveDate"] = System.Convert.ToDateTime(textBox3.Text.Trim());
					}
					else
					{
						dataRow["ReceiveDate"] = System.DBNull.Value;
					}
				}
			}
			this.ViewState["Stock"] = dataTable;
		}
	}
	private void DeleteResource(System.Collections.Generic.List<string> lstResourceID)
	{
		if (this.ViewState["Stock"] is DataTable)
		{
			DataTable dataTable = this.ViewState["Stock"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (lstResourceID.Contains(dataTable.Rows[i]["ResourceId"].ToString()))
				{
					dataTable.Rows.RemoveAt(i);
					i--;
				}
			}
			dataTable.AcceptChanges();
			this.ViewState["Stock"] = dataTable;
		}
	}
	private EquRepairReport GetModel(EquRepairReport model)
	{
		return model;
	}
	private void BindReport()
	{
		if (this.action == "edit")
		{
			EquRepairReport byId = this.reportSer.GetById(this.id);
			if (byId != null)
			{
			}
		}
		else
		{
			this.hfldRepairId.Value = System.Guid.NewGuid().ToString();
		}
		DataTable repairStock = this.stockSer.GetRepairStock(this.id, string.Empty, string.Empty, 0, 0);
		this.ViewState["Stock"] = repairStock;
		this.FileUpload1.RecordCode = this.hfldRepairId.Value;
		this.BindGV();
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
	private void BindGV()
	{
		DataTable dataSource = new DataTable();
		if (this.ViewState["Stock"] != null)
		{
			dataSource = (DataTable)this.ViewState["Stock"];
		}
		this.gvRepairStock.DataSource = dataSource;
		this.gvRepairStock.DataBind();
	}
	private void SaveStocks()
	{
		this.UpdateRepairStockDataSource();
		this.stockSer.DelStockByReportId(this.id);
		DataTable dataTable = (DataTable)this.ViewState["Stock"];
		foreach (DataRow dataRow in dataTable.Rows)
		{
			EquRepairStock equRepairStock = new EquRepairStock();
			equRepairStock.ReportId = this.hfldRepairId.Value;
			equRepairStock.ResourceId = dataRow["ResourceId"].ToString();
			equRepairStock.Quantity = System.Convert.ToDecimal(dataRow["Quantity"]);
			equRepairStock.UnitPrice = System.Convert.ToDecimal(dataRow["UnitPrice"]);
			equRepairStock.CorpId = dataRow["CorpId"].ToString();
			equRepairStock.ReceivePerson = dataRow["ReceivePerson"].ToString();
			equRepairStock.ReceiveDate = System.Convert.ToDateTime(dataRow["ReceiveDate"]);
			this.stockSer.Add(equRepairStock);
		}
	}
}


