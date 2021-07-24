using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Allocation_TransferringOrder : NBasePage, IRequiresSessionState
{
	private ResourceLogicEdit resLogicEdit = new ResourceLogicEdit();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Clear();
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			if (base.Request.QueryString["op"] == null && base.Request.QueryString["ac"] == null)
			{
				base.RegisterScript("top.ui.alert('页面错误,请联系技术人员')");
				return;
			}
			if (base.Request.QueryString["op"] != "")
			{
				this.HdnOperater.Value = base.Request.QueryString["op"];
				if (base.Request.QueryString["op"] == "Add")
				{
					this.lblAllocationNo.Text = DateTime.Now.ToString("yyyyMMddhhmmss");
					this.hdGuid.Value = Guid.NewGuid().ToString();
					this.HdnSelectOutPer.Value = base.UserCode;
					AllocationBllAction allocationBllAction = new AllocationBllAction();
					this.txtOutAllocationPerson.Text = allocationBllAction.GetUserNameByCode(base.UserCode);
				}
				cn.justwin.stockBLL.Treasury treasury = new cn.justwin.stockBLL.Treasury();
				if (treasury.GetManageMode() == SmEnum.SmSetValue.ParallelMode)
				{
					this.HdnStockModel.Value = "0";
				}
				if (Convert.ToInt32(treasury.GetManageMode()) == 1)
				{
					this.HdnStockModel.Value = "1";
				}
			}
			this.BindGVMaterialList();
			this.btnSave.Attributes["onclick"] = "return checkData();";
			this.txtOutDepository.Attributes["readonly"] = "readonly";
			this.txtInDepository.Attributes["readonly"] = "readonly";
			this.txtOutAllocationPerson.Attributes["readonly"] = "readonly";
			this.txtInAllocationPerson.Attributes["readonly"] = "readonly";
			this.lblAllocationNo.Attributes["readonly"] = "readonly";
			this.FileLink1.MID = 89;
			this.FileLink1.FID = this.hdGuid.Value;
			this.FileLink1.Type = 1;
		}
	}
	protected void BindGVMaterialList()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		if (this.HdnOperater.Value == "Add")
		{
			DataTable allocationStockList = allocationBllAction.GetAllocationStockList("", "acode=''");
			this.ViewState["DataTable"] = allocationStockList;
			this.GVMaterialList.DataSource = allocationStockList;
			this.GVMaterialList.DataBind();
		}
		if (this.HdnOperater.Value == "Edit")
		{
			this.ShowAllocationData();
		}
		if (this.HdnOperater.Value == "View")
		{
			this.ShowAllocationData();
			this.btnSave.Visible = false;
			this.BtnDel.Visible = false;
			this.btnSelectMaterial.Visible = false;
			this.txtInDate.Enabled = false;
			this.txtOutAllocationPerson.Enabled = false;
			this.txtInAllocationPerson.Enabled = false;
			this.txtOutDepository.Enabled = false;
			this.txtInDepository.Enabled = false;
		}
	}
	protected void GVMaterialList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.GVMaterialList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["sprice"] = this.GVMaterialList.DataKeys[e.Row.RowIndex]["sprice"].ToString();
			e.Row.Attributes["corp"] = this.GVMaterialList.DataKeys[e.Row.RowIndex]["corp"].ToString();
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			Label label = (Label)e.Row.Cells[1].FindControl("lblNo");
			label.Text = (this.GVMaterialList.PageIndex * this.GVMaterialList.PageSize + (e.Row.RowIndex + 1)).ToString();
			((Label)e.Row.Cells[8].FindControl("lblUnit")).Text = this.resLogicEdit.GetUnitNameList(this.resLogicEdit.GetUnitId(dataRowView["resourcecode"].ToString()));
			if (this.HdnOperater.Value == "Edit" || this.HdnOperater.Value == "View")
			{
				((TextBox)e.Row.Cells[11].FindControl("txtAllocationOutNum")).Text = ((dataRowView["number"].ToString() == "") ? "0.000" : Convert.ToDecimal(dataRowView["number"]).ToString("0.000"));
				e.Row.Attributes["onclick"] = "rowClick('" + this.GVMaterialList.DataKeys[e.Row.RowIndex].Value.ToString() + "')";
			}
			if (this.HdnOperater.Value == "Add")
			{
				string str = Guid.NewGuid().ToString();
				e.Row.Attributes["onclick"] = "rowClick('" + str + "')";
			}
		}
	}
	protected void ShowAllocationData()
	{
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		AllocationModel allocationModel = new AllocationModel();
		allocationModel = allocationBllAction.ReturnAllocatonModel("acode='" + ((base.Request.QueryString["ac"] == "") ? "0" : base.Request.QueryString["ac"]) + "' ");
		this.lblAllocationNo.Text = allocationModel.Acode;
		this.txtOutDepository.Text = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodea).Rows[0][0].ToString();
		this.HdnSelectOutDepo.Value = allocationModel.TCodea;
		this.txtInDepository.Text = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodeb).Rows[0][0].ToString();
		this.HdnIsTotal.Value = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodea).Rows[0][1].ToString();
		this.HdnSecTotal.Value = allocationBllAction.GetTreasuryNameByCode(allocationModel.TCodea).Rows[0][1].ToString();
		this.HdnSelectInDepo.Value = allocationModel.TCodeb;
		this.txtInDate.Text = allocationModel.InTime;
		this.txtOutAllocationPerson.Text = allocationBllAction.GetUserNameByCode(allocationModel.OutAllocationPerson);
		this.HdnSelectOutPer.Value = allocationModel.OutAllocationPerson;
		this.txtInAllocationPerson.Text = allocationBllAction.GetUserNameByCode(allocationModel.InAllocationPerson);
		this.HdnSelectInPer.Value = allocationModel.InAllocationPerson;
		this.txtRemark.Text = allocationModel.Explain;
		this.hdGuid.Value = allocationModel.Aid;
		DataTable allocationStockList = allocationBllAction.GetAllocationStockList(allocationModel.TCodea, "acode='" + allocationModel.Acode + "'");
		if (allocationStockList != null && allocationStockList.Rows.Count > NBasePage.pagesize)
		{
			this.HdnIsPage.Value = "1";
		}
		this.ViewState["DataTable"] = allocationStockList;
		if (allocationStockList.Rows.Count > 0)
		{
			Common2.BindGvTable(allocationStockList, this.GVMaterialList, false);
			string total = Convert.ToDecimal(allocationStockList.Compute("SUM(Total)", string.Empty)).ToString("0.000");
			GridViewUtility.AddTotalRow(this.GVMaterialList, total, 12);
			return;
		}
		this.GVMaterialList.DataSource = allocationStockList;
		this.GVMaterialList.DataBind();
	}
	protected AllocationModel GetAllocationData()
	{
		return new AllocationModel
		{
			Aid = this.hdGuid.Value,
			Acode = this.lblAllocationNo.Text.Trim(),
			Annex = "",
			Explain = this.txtRemark.Text,
			FlowState = -1,
			InAllocationPerson = this.HdnSelectInPer.Value,
			OutAllocationPerson = this.HdnSelectOutPer.Value,
			Person = this.Session["yhdm"].ToString(),
			TCodea = this.HdnSelectOutDepo.Value,
			TCodeb = this.HdnSelectInDepo.Value,
			InTime = this.txtInDate.Text.Trim(),
			IsOutA = false,
			IsInB = false
		};
	}
	protected AllocationStockModel GetStockModel(DataRow row)
	{
		return new AllocationStockModel
		{
			Number = Convert.ToDecimal(row["number"]),
			Sprice = Convert.ToDecimal(row["sprice"]),
			Corp = row["corpID"].ToString(),
			ACode = this.lblAllocationNo.Text.Trim(),
			SCode = row["ResourceCode"].ToString()
		};
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		AllocationBllAction allocationBllAction = new AllocationBllAction();
		if (this.HdnOperater.Value == "Add")
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				try
				{
					allocationBllAction.Insert(sqlTransaction, this.GetAllocationData());
					DataTable dataTable = this.ViewState["DataTable"] as DataTable;
					if (dataTable != null)
					{
						foreach (DataRow row in dataTable.Rows)
						{
							allocationBllAction.Insert(sqlTransaction, this.GetStockModel(row));
						}
					}
					sqlTransaction.Commit();
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_transferringOrdre' });");
				}
				catch
				{
					sqlTransaction.Rollback();
					base.RegisterScript("top.ui.show('修改失败')");
				}
				return;
			}
		}
		if (this.HdnOperater.Value == "Edit")
		{
			using (SqlConnection sqlConnection2 = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection2.Open();
				SqlTransaction sqlTransaction2 = sqlConnection2.BeginTransaction();
				try
				{
					AllocationModel allocationData = this.GetAllocationData();
					allocationBllAction.Update(sqlTransaction2, allocationData);
					allocationBllAction.DelAllocationStockByAcode(sqlTransaction2, allocationData.Acode);
					DataTable dataTable2 = this.ViewState["DataTable"] as DataTable;
					if (dataTable2 != null)
					{
						foreach (DataRow row2 in dataTable2.Rows)
						{
							allocationBllAction.Insert(sqlTransaction2, this.GetStockModel(row2));
						}
					}
					sqlTransaction2.Commit();
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_transferringOrdre' });");
				}
				catch
				{
					sqlTransaction2.Rollback();
					base.RegisterScript("top.ui.show('修改失败')");
				}
			}
		}
	}
	protected void btnBind_ServerClick(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = this.ViewState["DataTable"] as DataTable;
		DataTable materialOfDepositoryList = AllocationAction.GetMaterialOfDepositoryList(this.HdnSelectOutDepo.Value, this.HdnViewCodeList.Value);
		DataColumn dataColumn = new DataColumn("Total", typeof(decimal));
		dataColumn.DefaultValue = 0m;
		materialOfDepositoryList.Columns.Add(dataColumn);
		if (dataTable != null && materialOfDepositoryList != null)
		{
			if (dataTable.Rows.Count == 1 && string.IsNullOrEmpty(dataTable.Rows[0]["scode"].ToString()))
			{
				dataTable.Rows.RemoveAt(0);
			}
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["scode"],
				dataTable.Columns["sprice"],
				dataTable.Columns["corp"]
			};
			materialOfDepositoryList.PrimaryKey = new DataColumn[]
			{
				materialOfDepositoryList.Columns["scode"],
				materialOfDepositoryList.Columns["sprice"],
				materialOfDepositoryList.Columns["corp"]
			};
			dataTable.Merge(materialOfDepositoryList, true);
			this.ViewState["DataTable"] = dataTable;
		}
		if (dataTable.Rows.Count > 0)
		{
			this.GVMaterialList.DataSource = dataTable;
			this.GVMaterialList.DataBind();
			string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
			GridViewUtility.AddTotalRow(this.GVMaterialList, total, 12);
			return;
		}
		this.GVMaterialList.DataSource = dataTable;
		this.GVMaterialList.DataBind();
	}
	protected void BtnDel_ServerClick(object sender, EventArgs e)
	{
		this.UpdateDataSource();
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		foreach (GridViewRow gridViewRow in this.GVMaterialList.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			string text = gridViewRow.Attributes["id"].ToString();
			string text2 = gridViewRow.Attributes["sprice"].ToString();
			string text3 = gridViewRow.Attributes["corp"].ToString();
			if (checkBox != null && checkBox.Checked)
			{
				DataRow dataRow = dataTable.Select(string.Concat(new string[]
				{
					"scode='",
					text,
					"' and sprice='",
					text2,
					"' and corp='",
					text3,
					"'"
				}))[0];
				if (dataRow != null)
				{
					((DataTable)this.ViewState["DataTable"]).Rows.Remove(dataRow);
				}
			}
		}
		this.HdnViewCodeList.Value = "";
		dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count > 0)
		{
			this.GVMaterialList.DataSource = dataTable;
			this.GVMaterialList.DataBind();
			string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
			GridViewUtility.AddTotalRow(this.GVMaterialList, total, 12);
			return;
		}
		this.GVMaterialList.DataSource = dataTable;
		this.GVMaterialList.DataBind();
	}
	protected void btnAllDel_ServerClick(object sender, EventArgs e)
	{
		AllocationAction.DelAllocationStockBill(this.lblAllocationNo.Text);
		DataTable dataTable = this.ViewState["DataTable"] as DataTable;
		dataTable.Rows.Clear();
		this.ViewState["DataTable"] = dataTable;
		this.GVMaterialList.DataSource = null;
		this.GVMaterialList.DataBind();
	}
	protected void GVMaterialList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVMaterialList.PageIndex = e.NewPageIndex;
		this.BindGVMaterialList();
	}
	private void UpdateDataSource()
	{
		if (this.ViewState["DataTable"] is DataTable)
		{
			DataTable dataTable = this.ViewState["DataTable"] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				GridViewRow gridViewRow = this.GVMaterialList.Rows[i];
				Control control = gridViewRow.FindControl("txtAllocationOutNum");
				if (control is TextBox)
				{
					TextBox textBox = control as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text.Trim()))
					{
						dataRow["number"] = Convert.ToDecimal(textBox.Text.Trim());
					}
					else
					{
						dataRow["number"] = 0m;
					}
					decimal d = Convert.ToDecimal(dataRow["sprice"]);
					dataRow["Total"] = Convert.ToDecimal(dataRow["number"]) * d;
				}
			}
			this.ViewState["DataTable"] = dataTable;
		}
	}
}


