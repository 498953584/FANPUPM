using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmWastage_SmWastageEdit : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private Treasury treasury = new Treasury();
	private SmWastageBll smWastageBll = new SmWastageBll();
	private SmWastageStockBll smWastageStockBll = new SmWastageStockBll();
	private string action = string.Empty;
	private string wastageId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Id"]))
		{
			this.wastageId = base.Request["Id"].ToString();
			this.action = "update";
		}
		else
		{
			this.action = "add";
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initialize();
			this.FileUpload1.RecordCode = this.hdGuid.Value;
		}
	}
	protected void Initialize()
	{
		if (this.action == "add")
		{
			this.lblTitle.Text = "新报损增出库单";
			this.txtWastageCode.Text = System.DateTime.Now.ToString("yyyyMMddHHmmss");
			this.txtInTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.hdGuid.Value = System.Guid.NewGuid().ToString();
			this.txtPeople.Value = PageHelper.QueryUser(this, base.UserCode);
			this.ViewState["DataTable"] = this.smWastageStockBll.GetTableByWastageCode("''", "");
			this.BindGv();
			return;
		}
		this.lblTitle.Text = "编辑报损出库单";
		SmWastageModel model = this.smWastageBll.GetModel(this.wastageId);
		this.hdTsid.Value = "1";
		this.txtExplain.Text = model.Explain;
		this.txtInTime.Text = model.InputDate.ToString();
		this.txtPeople.Value = model.InputPerson;
		this.txtWastageCode.Text = model.Code;
		this.hdGuid.Value = model.Id;
		this.hdflowstate.Value = model.Flowstate.ToString();
		this.hfldTrea.Value = model.Treasurycode;
		this.txtTrea.Text = this.treasury.GetModel(model.Treasurycode).tname;
		System.Collections.Generic.List<SmWastageStockModel> listArray = this.smWastageStockBll.GetListArray(" WHERE WastageCode='" + model.Code + "' ");
		string text = "";
		foreach (SmWastageStockModel current in listArray)
		{
			text = text + "'" + current.ResourceCode + "',";
		}
		if (text != "")
		{
			text = text.Substring(0, text.Length - 1);
		}
		this.ViewState["DataTable"] = this.smWastageStockBll.GetTableByWastageCode(model.Code, model.Treasurycode);
		this.BindGv();
	}
	protected void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			this.gvWastageStock.DataSource = dataTable;
			this.gvWastageStock.DataBind();
			return;
		}
		this.gvWastageStock.DataSource = dataTable;
		this.gvWastageStock.DataBind();
		string total = System.Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
		GridViewUtility.AddTotalRow(this.gvWastageStock, total, 9);
	}
	protected void btnShowGv_Click(object sender, System.EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = this.ViewState["DataTable"] as DataTable;
		DataTable table = this.GetTable();
		DataColumn dataColumn = new DataColumn("ResourceCode", typeof(string));
		dataColumn.DefaultValue = "";
		table.Columns.Add(dataColumn);
		for (int i = 0; i < table.Rows.Count; i++)
		{
			DataRow dataRow = table.Rows[i];
			try
			{
				dataRow["ResourceCode"] = dataRow["scode"].ToString();
			}
			catch
			{
			}
		}
		if (dataTable != null && table != null)
		{
			if (dataTable.Rows.Count == 1 && string.IsNullOrEmpty(dataTable.Rows[0]["ResourceCode"].ToString()))
			{
				dataTable.Rows.RemoveAt(0);
			}
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ResourceCode"],
				dataTable.Columns["CorpId"],
				dataTable.Columns["sprice"]
			};
			table.PrimaryKey = new DataColumn[]
			{
				table.Columns["ResourceCode"],
				table.Columns["CorpId"],
				table.Columns["sprice"]
			};
			dataTable = this.MergeDataTable(dataTable, table);
			this.ViewState["DataTable"] = dataTable;
		}
		this.BindGv();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvWastageStock.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			HiddenField hiddenField = gridViewRow.FindControl("hdSprice") as HiddenField;
			HiddenField hiddenField2 = gridViewRow.FindControl("hdCorp") as HiddenField;
			if (checkBox != null && checkBox.Checked)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (checkBox.ToolTip == dataTable.Rows[i]["ResourceCode"].ToString() && hiddenField.Value == dataTable.Rows[i]["Sprice"].ToString() && hiddenField2.Value == dataTable.Rows[i]["CorpId"].ToString())
					{
						((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[i]);
					}
				}
			}
		}
		if (dataTable.Rows.Count == 1 && dataTable.Rows[0]["ResourceCode"].ToString() == "")
		{
			((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[0]);
		}
		this.BindGv();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.UpdateDataSource();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				SmWastageModel smWastageModel = new SmWastageModel();
				smWastageModel.Explain = this.txtExplain.Text;
				smWastageModel.Flowstate = System.Convert.ToInt32(this.hdflowstate.Value);
				smWastageModel.InputDate = System.Convert.ToDateTime(this.txtInTime.Text);
				smWastageModel.Isout = false;
				smWastageModel.Code = this.txtWastageCode.Text;
				smWastageModel.Id = this.hdGuid.Value;
				smWastageModel.InputPerson = this.txtPeople.Value;
				smWastageModel.Treasurycode = this.hfldTrea.Value;
				int num;
				if (this.action == "update")
				{
					num = this.smWastageBll.Update(sqlTransaction, smWastageModel);
				}
				else
				{
					num = this.smWastageBll.Add(sqlTransaction, smWastageModel);
				}
				if (num != 0)
				{
					this.smWastageStockBll.DeleteByWhere(sqlTransaction, " where wastageCode='" + smWastageModel.Code + "' ");
					DataTable dataTable = (DataTable)this.ViewState["DataTable"];
					if (dataTable != null)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							SmWastageStockModel smWastageStockModel = new SmWastageStockModel();
							smWastageStockModel.Corp = dataRow["CorpId"].ToString();
							smWastageStockModel.Number = System.Convert.ToDecimal(dataRow["number"]);
							smWastageStockModel.WastageCode = this.txtWastageCode.Text;
							smWastageStockModel.Id = System.Guid.NewGuid().ToString();
							smWastageStockModel.ResourceCode = dataRow["ResourceCode"].ToString();
							smWastageStockModel.Sprice = System.Convert.ToDecimal(dataRow["sprice"]);
							this.smWastageStockBll.Add(sqlTransaction, smWastageStockModel);
						}
					}
				}
				sqlTransaction.Commit();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(System.Environment.NewLine);
				stringBuilder.Append("winclose('AddReserve','SmWastageList.aspx',true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (System.Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('系统提示：\\n\\n对不起" + this.SetMsg() + "失败！');");
			}
		}
	}
	protected void gvWastageStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvWastageStock.DataKeys[e.Row.RowIndex].Value.ToString();
		}
		TextBox textBox = e.Row.FindControl("txtSnumber") as TextBox;
		TextBox textBox2 = e.Row.FindControl("txtCNum") as TextBox;
		if (textBox != null)
		{
			textBox2.Attributes["onblur"] = "chkNum(this.No,this," + textBox.ClientID + ")";
		}
	}
	protected void btnModifyTreasury_Click(object sender, System.EventArgs e)
	{
		this.ViewState["DataTable"] = this.smWastageStockBll.GetTableByWastageCode("''", "");
		this.BindGv();
	}
	private void UpdateDataSource()
	{
		if (this.ViewState["DataTable"] is DataTable)
		{
			DataTable dataTable = this.ViewState["DataTable"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvWastageStock.Rows[i];
				Control control = gridViewRow.FindControl("txtCNum");
				if (control is TextBox)
				{
					TextBox textBox = control as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text.Trim()))
					{
						dataRow["number"] = System.Convert.ToDecimal(textBox.Text.Trim());
					}
					else
					{
						dataRow["number"] = 0m;
					}
					decimal d = System.Convert.ToDecimal(dataRow["sprice"]);
					dataRow["Total"] = System.Convert.ToDecimal(dataRow["number"]) * d;
				}
			}
			this.ViewState["DataTable"] = dataTable;
		}
	}
	public DataTable GetTable()
	{
		return this.treasuryStockBll.GetListByTsid(this.hdTsid.Value, this.hfldTrea.Value);
	}
	public decimal GetNumByResourceInfo(string scode, string sprice, string corp)
	{
		string value = "";
		DataTable arg_1B_0 = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvWastageStock.Rows)
		{
			TextBox textBox = gridViewRow.FindControl("txtCNum") as TextBox;
			HiddenField hiddenField = gridViewRow.FindControl("hdScode") as HiddenField;
			HiddenField hiddenField2 = gridViewRow.FindControl("hdSprice") as HiddenField;
			HiddenField hiddenField3 = gridViewRow.FindControl("hdCorp") as HiddenField;
			if (hiddenField.Value == scode && hiddenField2.Value == sprice && hiddenField3.Value == corp)
			{
				value = textBox.Text;
			}
		}
		return System.Convert.ToDecimal(value);
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	private DataTable MergeDataTable(DataTable dtMaster, DataTable dtMerge)
	{
		foreach (DataRow dataRow in dtMerge.Rows)
		{
			string text = dataRow["ResourceCode"].ToString();
			string text2 = dataRow["corpId"].ToString();
			decimal num = System.Convert.ToDecimal(dataRow["sprice"]);
			string filterExpression = string.Concat(new string[]
			{
				"ResourceCode='",
				text,
				"' and corpId='",
				text2,
				"' and sprice=",
				num.ToString("0.000")
			});
			DataRow[] array = dtMaster.Select(filterExpression);
			if (array.Length == 0)
			{
				DataRow dataRow2 = dtMaster.NewRow();
				dataRow2["ResourceCode"] = text;
				dataRow2["corpId"] = text2;
				dataRow2["sprice"] = num;
				dataRow2["number"] = System.Convert.ToDecimal(dataRow["number"]);
				dataRow2["CorpName"] = dataRow["Corp"].ToString();
				dataRow2["ResourceName"] = dataRow["ResourceName"].ToString();
				dataRow2["Specification"] = dataRow["Specification"].ToString();
				dataRow2["Name"] = dataRow["Name"].ToString();
				dataRow2["Total"] = System.Convert.ToDecimal(dataRow["Total"]);
				dataRow2["snumber"] = System.Convert.ToDecimal(dataRow["snumber"]);
				dataRow2["Brand"] = dataRow["Brand"].ToString();
				dataRow2["ModelNumber"] = dataRow["ModelNumber"].ToString();
				dataRow2["TechnicalParameter"] = dataRow["TechnicalParameter"].ToString();
				dtMaster.Rows.Add(dataRow2);
			}
		}
		return dtMaster;
	}
}


