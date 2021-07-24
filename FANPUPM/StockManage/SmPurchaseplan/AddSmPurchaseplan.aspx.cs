using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmPurchaseplan_AddSmPurchaseplan : NBasePage, IRequiresSessionState
{
	private class _PurchasePlan
	{
		public string scode;
		public decimal num;
		public string date;
		public string note;
	}
	private MaterialWantPlan materialWantPlan = new MaterialWantPlan();
	private MeterialPlanStock meterialPlanStock = new MeterialPlanStock();
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private PtYhmcBll ptYhmcBll = new PtYhmcBll();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private SmPurchaseplanStockBll smPurchaseplanStockBll = new SmPurchaseplanStockBll();
	private string wpcode = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
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
			this.wpcode = base.Request.QueryString["id"].ToString();
			this.lblTitle.Text = "编辑采购计划单";
			SmPurchaseplanModel model = this.smPurchaseplanBll.GetModel(base.Request.QueryString["id"]);
			this.txtExplain.Text = model.explain;
			this.txtInTime.Text = model.intime.ToString();
			this.txtPeople.Value = model.person;
			this.txtPPCode.Text = model.ppcode;
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(model.Project).PrjName;
			this.hdnProjectCode.Value = model.Project;
			this.hdflowstate.Value = model.flowstate.ToString();
			this.hdGuid.Value = model.ppid;
			this.hdnCodeList.Value = "1";
			string[] arrPpcode = new string[]
			{
				model.ppcode
			};
			this.ViewState["DataTable"] = this.smPurchaseplanStockBll.GetResourceByPpcodes(arrPpcode);
			this.BindGv();
		}
		else
		{
			this.lblTitle.Text = "新增采购计划单";
			string text = base.Request["prjId"].ToString();
			this.hdnProjectCode.Value = text;
			this.txtProjectName.Value = this.pTPrjInfoBll.GetModelByPrjGuid(text).PrjName;
			this.txtPPCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
			this.txtInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			this.hdGuid.Value = Guid.NewGuid().ToString();
			this.txtPeople.Value = PageHelper.QueryUser(this, base.UserCode);
			string[] arrPpcode2 = new string[]
			{
				"''"
			};
			DataTable resourceByPpcodes = this.smPurchaseplanStockBll.GetResourceByPpcodes(arrPpcode2);
			this.ViewState["DataTable"] = resourceByPpcodes;
			this.BindGv();
		}
		this.FileLink1.MID = 1756;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public DataTable GetTable()
	{
		DataTable listArrayByWpcodenew = this.meterialPlanStock.GetListArrayByWpcodenew(this.hdwzId.Value);
		DataColumn column = new DataColumn("arrivalDate", typeof(DateTime));
		DataColumn column2 = new DataColumn("Remark");
		listArrayByWpcodenew.Columns.Add(column);
		listArrayByWpcodenew.Columns.Add(column2);
		for (int i = 0; i < listArrayByWpcodenew.Rows.Count; i++)
		{
			DataRow dataRow = listArrayByWpcodenew.Rows[i];
			dataRow["Remark"] = this.meterialPlanStock.GetRemark(dataRow["Scode"].ToString(), this.hdwzId.Value).ToString();
			try
			{
				dataRow["arrivalDate"] = this.meterialPlanStock.GetMinArrivalDate(dataRow["Scode"].ToString(), this.hdwzId.Value).ToString();
			}
			catch
			{
			}
		}
		return listArrayByWpcodenew;
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
	}
	protected void btnShowGv_Click(object sender, EventArgs e)
	{
		DataTable dataTable = this.GetTable();
		DataTable dataTable2 = this.ViewState["DataTable"] as DataTable;
		this.SetInputValuesIntoResourceTable(dataTable2);
		if (dataTable2 != null)
		{
			dataTable2.PrimaryKey = new DataColumn[]
			{
				dataTable2.Columns["scode"]
			};
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["scode"]
			};
			dataTable2.Merge(dataTable, true);
			dataTable = dataTable2;
		}
		this.ViewState["DataTable"] = dataTable;
		this.BindGv();
		this.hdwzId.Value = string.Empty;
	}
	protected void btnShowList_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldResourceCode.Value))
		{
			string[] arr = JsonHelper.GetListFromJson(this.hfldResourceCode.Value).ToArray();
			string arrayToInStr = StringUtility.GetArrayToInStr(arr);
			this.hdnCodeList.Value = arrayToInStr;
			DataTable dataTable = this.meterialPlanStock.GetListByScode(arrayToInStr);
			DataTable dataTable2 = this.ViewState["DataTable"] as DataTable;
			this.SetInputValuesIntoResourceTable(dataTable2);
			if (dataTable2 != null)
			{
				dataTable2.PrimaryKey = new DataColumn[]
				{
					dataTable2.Columns["scode"]
				};
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns["scode"]
				};
				dataTable2.Merge(dataTable, true);
				dataTable = dataTable2;
			}
			this.ViewState["DataTable"] = dataTable;
			this.BindGv();
		}
	}
	private void SetInputValuesIntoResourceTable(DataTable hasResourceTable)
	{
		StockManage_SmPurchaseplan_AddSmPurchaseplan._PurchasePlan[] array = JsonNetWrap.DeserializeObject<StockManage_SmPurchaseplan_AddSmPurchaseplan._PurchasePlan[]>(this.hdfdInputValues.Value);
		for (int i = 0; i < array.Length; i++)
		{
			DataRow targetRow = this.GetTargetRow(array[i].scode, hasResourceTable);
			if (targetRow != null)
			{
				targetRow["number"] = array[i].num;
				DateTime dateTime;
				if (DateTime.TryParse(array[i].date, out dateTime))
				{
					targetRow["arrivalDate"] = dateTime;
				}
				targetRow["Remark"] = array[i].note;
			}
		}
	}
	private DataRow GetTargetRow(string scode, DataTable hasResourceTable)
	{
		foreach (DataRow dataRow in hasResourceTable.Rows)
		{
			if (scode.Equals(dataRow["scode"]))
			{
				return dataRow;
			}
		}
		return null;
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (checkBox.ToolTip == dataTable.Rows[i]["scode"].ToString())
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
	public decimal GetNumByScode(string scode)
	{
		string value = "";
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TextBox textBox = gridViewRow.FindControl("txtNum") as TextBox;
				if (textBox.ToolTip == scode)
				{
					value = textBox.Text;
				}
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
				SmPurchaseplanModel smPurchaseplanModel = new SmPurchaseplanModel();
				smPurchaseplanModel.acceptstate = 0;
				smPurchaseplanModel.annx = "";
				smPurchaseplanModel.explain = this.txtExplain.Text;
				smPurchaseplanModel.flowstate = Convert.ToInt32(this.hdflowstate.Value);
				smPurchaseplanModel.intime = Convert.ToDateTime(this.txtInTime.Text);
				smPurchaseplanModel.person = this.txtPeople.Value;
				smPurchaseplanModel.ppcode = this.txtPPCode.Text;
				smPurchaseplanModel.ppid = this.hdGuid.Value;
				smPurchaseplanModel.Project = this.hdnProjectCode.Value;
				int num;
				if (base.Request.QueryString["id"] != null)
				{
					num = this.smPurchaseplanBll.Update(sqlTransaction, smPurchaseplanModel);
				}
				else
				{
					num = this.smPurchaseplanBll.Add(sqlTransaction, smPurchaseplanModel);
				}
				if (num != 0)
				{
					this.smPurchaseplanStockBll.DeleteByWhere(sqlTransaction, " where ppcode='" + smPurchaseplanModel.ppcode + "'");
					DataTable dataTable = (DataTable)this.ViewState["DataTable"];
					if (dataTable != null)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							SmPurchaseplanStockModel smPurchaseplanStockModel = new SmPurchaseplanStockModel();
							smPurchaseplanStockModel.scode = dataRow["scode"].ToString();
							smPurchaseplanStockModel.number = this.GetNumByScode(dataRow["scode"].ToString());
							smPurchaseplanStockModel.ppcode = this.txtPPCode.Text;
							smPurchaseplanStockModel.wpsid = Guid.NewGuid().ToString();
							if (this.gvNeedNote.Rows.Count > 0)
							{
								foreach (GridViewRow gridViewRow in this.gvNeedNote.Rows)
								{
									Label label = (Label)gridViewRow.FindControl("lbscode");
									string text = label.Text;
									if (text == dataRow["scode"].ToString())
									{
										TextBox textBox = (TextBox)gridViewRow.FindControl("txtarrivalDate");
										if (!string.IsNullOrEmpty(textBox.Text.Trim()))
										{
											smPurchaseplanStockModel.ArrivalDate = textBox.Text.Trim().ToString();
										}
										else
										{
											smPurchaseplanStockModel.ArrivalDate = "";
										}
										TextBox textBox2 = (TextBox)gridViewRow.FindControl("txtRemark");
										smPurchaseplanStockModel.remark = textBox2.Text.Trim();
										break;
									}
								}
							}
							this.smPurchaseplanStockBll.Add(sqlTransaction, smPurchaseplanStockModel);
						}
					}
				}
				this.UpdateWantplanAState(sqlTransaction, this.hdlfWantplanCodes.Value);
				sqlTransaction.Commit();
				StringBuilder stringBuilder = new StringBuilder();
				string arg_317_0 = string.Empty;
				stringBuilder.Append("top.ui.show('" + this.SetMsg() + "成功！');").Append(Environment.NewLine);
				stringBuilder.Append("winclose('AddSmPurchaseplan','SmPurchaseplanList.aspx?PrjGuid=" + this.hdnProjectCode.Value + "',true)");
				base.RegisterScript(stringBuilder.ToString());
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("top.ui.show('" + this.SetMsg() + "失败！');");
			}
		}
	}
	public void UpdateWantplanAState(SqlTransaction trans, string wpcode)
	{
		if (base.Request.QueryString["id"] == null && string.IsNullOrEmpty(this.hdnCodeList.Value))
		{
			MaterialWantPlan materialWantPlan = new MaterialWantPlan();
			materialWantPlan.UpdateAcceptstate(trans, wpcode);
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
	protected void gvNeedNote_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvNeedNote.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
		}
	}
}


