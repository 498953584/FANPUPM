using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
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
public partial class StockManage_SmOutReserve_AddReserve : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private Treasury treasury = new Treasury();
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStockBll = new OutStockBll();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
			this.hfldPrjId.Value = base.Request["prjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.txtTrea.Attributes.Add("ReadOnly", "true");
		this.txtEquCode.Attributes.Add("ReadOnly", "true");
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.Request.QueryString["t"] != null)
		{
			this.btnDel.Enabled = false;
			this.btnSave.Enabled = false;
			this.btnSelectByd.Disabled = true;
		}
		if (base.Request.QueryString["id"] != null)
		{
			this.lblTitle.Text = "编辑出库单";
			OutReserveModel model = this.outReserveBll.GetModel(base.Request.QueryString["id"]);
			this.hdTsid.Value = "1";
			this.txtExplain.Text = model.explain;
			this.txtInTime.Text = model.intime.ToString();
			this.txtPeople.Value = model.person;
			this.txtPPCode.Text = model.orcode;
			this.txtPickingPeople.Text = model.PickingPeople;
			this.txtPickingSector.Text = model.PickingSector;
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(model.procode).PrjName;
			this.hdGuid.Value = model.orid;
			this.hdflowstate.Value = model.flowstate.ToString();
			this.hfldTrea.Value = model.tcode;
			this.txtTrea.Text = this.treasury.GetModel(model.tcode).tname;
			string equipmentId = model.EquipmentId;
			if (!string.IsNullOrEmpty(equipmentId))
			{
				this.hfldEquId.Value = equipmentId;
				EquEquipmentService equEquipmentService = new EquEquipmentService();
				EquEquipment byId = equEquipmentService.GetById(equipmentId);
				if (byId != null)
				{
					this.txtEquCode.Text = byId.EquCode;
				}
			}
			List<OutStockModel> listArray = this.outStockBll.GetListArray(" where orcode='" + model.orcode + "'");
			string text = "";
			foreach (OutStockModel current in listArray)
			{
				text = text + "'" + current.scode + "',";
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
			}
			this.ViewState["DataTable"] = this.outStockBll.GetTableByOrcode(model.orcode, model.tcode);
			this.BindGv();
		}
		else
		{
			this.lblTitle.Text = "新增出库单";
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(this.prjId).PrjName;
			this.txtPPCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
			this.txtInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.txtPeople.Value = PageHelper.QueryUser(this, base.UserCode);
			this.ViewState["DataTable"] = this.outStockBll.GetTableByOrcode("''", "");
			this.BindGv();
		}
		this.FileLink1.MID = 1804;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public string GetNumByScodeAndOrcode(string scode)
	{
		OutStockModel modelByWhere = this.outStockBll.GetModelByWhere(string.Concat(new string[]
		{
			" where orcode='",
			this.txtPPCode.Text,
			"' and scode='",
			scode,
			"'"
		}));
		if (modelByWhere != null)
		{
			return modelByWhere.number.ToString();
		}
		return string.Concat(0);
	}
	public DataTable GetTable()
	{
		return this.treasuryStockBll.GetListByTsid(this.hdTsid.Value, this.hfldTrea.Value);
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			this.gvNeedNote.DataSource = dataTable;
			this.gvNeedNote.DataBind();
			return;
		}
		this.gvNeedNote.DataSource = dataTable;
		this.gvNeedNote.DataBind();
		string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
		GridViewUtility.AddTotalRow(this.gvNeedNote, total, 9);
	}
	protected void btnShowGv_Click(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = this.ViewState["DataTable"] as DataTable;
		DataTable table = this.GetTable();
		DataColumn dataColumn = new DataColumn();
		dataColumn.ColumnName = "TaskId";
		dataColumn.DataType = Type.GetType("System.String");
		dataColumn.DefaultValue = string.Empty;
		table.Columns.Add(dataColumn);
		if (dataTable != null && table != null)
		{
			if (dataTable.Rows.Count == 1 && string.IsNullOrEmpty(dataTable.Rows[0]["scode"].ToString()))
			{
				dataTable.Rows.RemoveAt(0);
			}
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["scode"],
				dataTable.Columns["CorpId"],
				dataTable.Columns["sprice"],
				dataTable.Columns["TaskId"]
			};
			table.PrimaryKey = new DataColumn[]
			{
				table.Columns["scode"],
				table.Columns["CorpId"],
				table.Columns["sprice"],
				table.Columns["TaskId"]
			};
			dataTable.Merge(table, true);
			this.ViewState["DataTable"] = dataTable;
		}
		this.BindGv();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			HiddenField hiddenField = gridViewRow.FindControl("hdSprice") as HiddenField;
			HiddenField hiddenField2 = gridViewRow.FindControl("hdCorp") as HiddenField;
			if (checkBox != null && checkBox.Checked)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (checkBox.ToolTip == dataTable.Rows[i]["scode"].ToString() && hiddenField.Value == dataTable.Rows[i]["Sprice"].ToString() && hiddenField2.Value == dataTable.Rows[i]["CorpId"].ToString())
					{
						((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[i]);
					}
				}
			}
		}
		if (dataTable.Rows.Count == 1 && dataTable.Rows[0]["scode"].ToString() == "")
		{
			((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataTable.Rows[0]);
		}
		this.BindGv();
	}
	public decimal GetNumByOrsid(string scode, string sprice, string corp)
	{
		string value = "";
		DataTable arg_1B_0 = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
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
		return Convert.ToDecimal(value);
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				OutReserveModel outReserveModel = new OutReserveModel();
				outReserveModel.annx = "";
				outReserveModel.explain = this.txtExplain.Text;
				outReserveModel.flowstate = Convert.ToInt32(this.hdflowstate.Value);
				outReserveModel.intime = Convert.ToDateTime(this.txtInTime.Text);
				outReserveModel.isout = false;
				outReserveModel.orcode = this.txtPPCode.Text;
				outReserveModel.orid = this.hdGuid.Value;
				outReserveModel.person = this.txtPeople.Value;
				outReserveModel.procode = this.prjId;
				outReserveModel.tcode = this.hfldTrea.Value;
				outReserveModel.PickingSector = this.txtPickingSector.Text;
				outReserveModel.PickingPeople = this.txtPickingPeople.Text;
				outReserveModel.EquipmentId = this.hfldEquId.Value;
				int num;
				if (base.Request.QueryString["id"] != null)
				{
					num = this.outReserveBll.Update(sqlTransaction, outReserveModel);
				}
				else
				{
					num = this.outReserveBll.Add(sqlTransaction, outReserveModel);
				}
				if (num != 0)
				{
					this.outStockBll.DeleteByWhere(sqlTransaction, " where orcode='" + outReserveModel.orcode + "'");
					this.UpdateDataSource();
					DataTable dataTable = (DataTable)this.ViewState["DataTable"];
					if (dataTable != null)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							OutStockModel outStockModel = new OutStockModel();
							outStockModel.corp = dataRow["CorpId"].ToString();
							outStockModel.number = this.GetNumByOrsid(dataRow["scode"].ToString(), dataRow["sprice"].ToString(), dataRow["corpId"].ToString());
							outStockModel.orcode = this.txtPPCode.Text;
							outStockModel.orsid = Guid.NewGuid().ToString();
							outStockModel.scode = dataRow["scode"].ToString();
							outStockModel.sprice = Convert.ToDecimal(dataRow["sprice"]);
							outStockModel.TaskId = dataRow["TaskId"].ToString();
							this.outStockBll.Add(sqlTransaction, outStockModel);
						}
					}
				}
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
				stringBuilder.Append("winclose('AddReserve','SmOutReserveList.aspx?prjGuid=" + this.prjId + "',true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('" + this.SetMsg() + "失败！');");
			}
		}
	}
	protected void gvNeedNote_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvNeedNote.DataKeys[e.Row.RowIndex].Value.ToString();
		}
		TextBox textBox = e.Row.FindControl("txtSnumber") as TextBox;
		TextBox textBox2 = e.Row.FindControl("txtCNum") as TextBox;
		if (textBox != null)
		{
			textBox2.Attributes["onblur"] = "chkNum(this.No,this," + textBox.ClientID + ")";
		}
	}
	public string SetMsg()
	{
		if (base.Request.QueryString["id"] != null)
		{
			return "更新";
		}
		return "添加";
	}
	private void UpdateDataSource()
	{
		if (this.ViewState["DataTable"] is DataTable)
		{
			DataTable dataTable = this.ViewState["DataTable"] as DataTable;
			string value = string.Empty;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.gvNeedNote.Rows[i];
				Control control = gridViewRow.FindControl("txtCNum");
				if (control is TextBox)
				{
					value = (control as TextBox).Text.Trim();
					if (!string.IsNullOrEmpty(value))
					{
						dataRow["number"] = Convert.ToDecimal(value);
					}
					else
					{
						dataRow["number"] = 0m;
					}
					decimal d = Convert.ToDecimal(dataRow["sprice"]);
					dataRow["Total"] = Convert.ToDecimal(value) * d;
					TextBox textBox = gridViewRow.FindControl("txtTask") as TextBox;
					HiddenField hiddenField = gridViewRow.FindControl("hfldTask") as HiddenField;
					if (!string.IsNullOrEmpty(textBox.Text.Trim()))
					{
						dataRow["TaskId"] = hiddenField.Value.Trim();
					}
					else
					{
						dataRow["TaskId"] = string.Empty;
					}
				}
			}
			this.ViewState["DataTable"] = dataTable;
		}
	}
}


