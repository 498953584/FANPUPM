using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Maintenance_MaintenanceEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;
	private int type;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = System.Convert.ToInt32(base.Request["type"].ToString());
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
			this.BindReport();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.gvMaintenanceStock.Rows.Count == 0)
		{
			base.RegisterScript("top.ui.show('请选择零配件信息！')");
			return;
		}
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("winclose('MaintenanceEdit', 'MaintenanceList.aspx', true)");
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
		this.UpdateMaintenanceStockDataSource();
		this.DeleteResource(listFromJson);
		this.BindGV();
	}
	protected void gvMaintenanceStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvMaintenanceStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void SelectedStock(string resources)
	{
		if (!string.IsNullOrEmpty(resources))
		{
			this.UpdateMaintenanceStockDataSource();
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(resources);
			if (array != null)
			{
				Resource resource = new Resource();
				DataTable dataTable = new DataTable();
				dataTable = resource.GetResourceNew(array);
				DataColumn dataColumn = new DataColumn("Price", typeof(decimal));
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
	private void UpdateMaintenanceStockDataSource()
	{
		if (this.ViewState["Stock"] is DataTable)
		{
			DataTable dataTable = this.ViewState["Stock"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GridViewRow gridViewRow = this.gvMaintenanceStock.Rows[i];
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
						dataRow["Price"] = textBox2.Text.Trim();
					}
					else
					{
						dataRow["Price"] = 0m;
					}
				}
				dataRow["Total"] = System.Convert.ToDecimal(dataRow["Quantity"]) * System.Convert.ToDecimal(dataRow["Price"]);
				if (gridViewRow.FindControl("hfldCorp") is HiddenField)
				{
					HiddenField hiddenField = gridViewRow.FindControl("hfldCorp") as HiddenField;
					dataRow["CorpId"] = hiddenField.Value;
				}
				if (gridViewRow.FindControl("txtCorp") is TextBox)
				{
					TextBox textBox3 = gridViewRow.FindControl("txtCorp") as TextBox;
					dataRow["CorpName"] = textBox3.Text.Trim();
				}
				if (gridViewRow.FindControl("hfldReceivePerson") is HiddenField)
				{
					HiddenField hiddenField2 = gridViewRow.FindControl("hfldReceivePerson") as HiddenField;
					dataRow["ReceivePerson"] = hiddenField2.Value;
				}
				if (gridViewRow.FindControl("txtReceiveDate") is TextBox)
				{
					TextBox textBox4 = gridViewRow.FindControl("txtReceiveDate") as TextBox;
					if (!string.IsNullOrEmpty(textBox4.Text.Trim()))
					{
						dataRow["ReceiveDate"] = System.Convert.ToDateTime(textBox4.Text.Trim());
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
	private void BindReport()
	{
	}
	private void BindGV()
	{
		DataTable dataSource = new DataTable();
		if (this.ViewState["Stock"] != null)
		{
			dataSource = (DataTable)this.ViewState["Stock"];
		}
		this.gvMaintenanceStock.DataSource = dataSource;
		this.gvMaintenanceStock.DataBind();
	}
	private void SaveStocks()
	{
		this.UpdateMaintenanceStockDataSource();
		DataTable dataTable = (DataTable)this.ViewState["Stock"];
		foreach (DataRow arg_35_0 in dataTable.Rows)
		{
		}
	}
}


