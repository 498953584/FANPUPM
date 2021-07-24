using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Excel;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Budget_ExcelImportContract : NBasePage, IRequiresSessionState
{
	private const int start = 2;
	private string excelName = string.Empty;
	private string taskId = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private int version = 1;
	private System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
	private DataTable dtWBS = new DataTable();
	private DataTable dtRes = new DataTable();
	private string[] taskArray;
	private string[] resourceArray;
	private string inputUser = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["taskId"]))
		{
			this.taskId = base.Request["taskId"];
		}
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (base.IsPostBack)
		{
			string value = this.hfldCurrentTabIndex.Value;
			base.RegisterScript("setTem('" + value + "')");
			this.BindResData();
			this.BindWBSData();
		}
	}
	protected void btnConnectExcel_Click(object sender, System.EventArgs e)
	{
		if (!this.ValidateExcel(this.fupExcel))
		{
			return;
		}
		string text = base.Server.MapPath(ConfigHelper.Get("TempDirectory"));
		if (!System.IO.Directory.Exists(text))
		{
			System.IO.Directory.CreateDirectory(text);
		}
		this.excelName = text + this.fupExcel.FileName;
		this.ViewState["ExcelName"] = this.excelName;
		this.hfldExcelName.Value = this.excelName;
		if (System.IO.File.Exists(this.excelName))
		{
			System.IO.File.SetAttributes(this.excelName, System.IO.FileAttributes.Normal);
			System.IO.File.Delete(this.excelName);
		}
		this.fupExcel.SaveAs(this.excelName);
		this.ClearDataAndInitialize();
	}
	protected void btnImport_Click(object sender, System.EventArgs e)
	{
		this.taskArray = this.hfldExcelColumns.Value.Split(new char[]
		{
			','
		});
		this.resourceArray = this.hfldResource.Value.Split(new char[]
		{
			','
		});
		this.hfldExcelColumns.Value = string.Empty;
		this.hfldResource.Value = string.Empty;
		if (this.ViewState["ExcelName"] != null && !string.IsNullOrEmpty(this.ViewState["ExcelName"].ToString()))
		{
			bool flag = true;
			if (string.IsNullOrEmpty(this.taskId))
			{
				this.dtWBS = (this.ViewState["dtwbs"] as DataTable);
				if (this.dtWBS.Rows.Count > 0)
				{
					BudContractTask.ClearByPrjId(this.prjId);
				}
			}
			else
			{
				flag = (BudContractTask.GetById(this.taskId) != null);
			}
			if (flag)
			{
				this.dtWBS = (this.ViewState["dtwbs"] as DataTable);
				BudTaskServices budTaskServices = new BudTaskServices(this.taskId, this.prjId, this.version, this.dtWBS);
				bool flag2 = false;
				System.Collections.Generic.IDictionary<string, int> relation = ExcelUtility.GetRelation(this.taskArray);
				int num = -1;
				int num2 = -1;
				string str = string.Empty;
				foreach (System.Collections.Generic.KeyValuePair<string, int> current in relation)
				{
					string key;
					if ((key = current.Key) != null)
					{
						if (!(key == "TaskCode"))
						{
							if (key == "SerialNo")
							{
								num = current.Value;
							}
						}
						else
						{
							num2 = current.Value;
						}
					}
				}
				if (num != -1)
				{
					str = this.dtWBS.Columns[num].ColumnName;
					this.dtWBS.DefaultView.Sort = str + " DESC";
					for (int i = 1; i < this.dtWBS.Rows.Count; i++)
					{
						if (this.dtWBS.Rows[i][num].ToString().Trim() == this.dtWBS.Rows[i - 1][num].ToString().Trim())
						{
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					if (num2 != -1)
					{
						str = this.dtWBS.Columns[num2].ColumnName;
						this.dtWBS.DefaultView.Sort = str + " DESC";
						for (int j = 1; j < this.dtWBS.Rows.Count; j++)
						{
							if (this.dtWBS.Rows[j][num2].ToString().Trim() == this.dtWBS.Rows[j - 1][num2].ToString().Trim())
							{
								flag2 = true;
								break;
							}
						}
					}
				}
				if (flag2)
				{
					base.RegisterScript("top.ui.alert('导入失败!\\n导入的Excel中有节点的序号出现重复，请修改！');");
					return;
				}
				if (num == -1 && num2 != -1)
				{
					foreach (DataRow dataRow in this.dtWBS.Rows)
					{
						string code = (dataRow[num2] == null) ? "" : dataRow[num2].ToString();
						flag2 = BudContractTask.CheckCode(code, this.prjId);
						if (flag2)
						{
							break;
						}
					}
				}
				if (!flag2)
				{
					this.inputUser = PageHelper.QueryUser(this, base.UserCode);
					int num3 = budTaskServices.ConverConTaskList(this.taskArray, this.inputUser, this.errors);
					this.AddResource();
					string text = "成功";
					if (num3 == 0)
					{
						text = "失败";
					}
					string text2 = string.Empty;
					if (this.errors.Count > 0)
					{
						this.ViewState["errors"] = this.errors;
						this.ShowErrors();
						text2 = "\\n部分节点导入失败,详见“警告信息”！";
					}
					else
					{
						this.CloseSkip();
					}
					base.RegisterScript(string.Concat(new string[]
					{
						"top.ui.alert('Excel导入",
						text,
						"！",
						text2,
						"');"
					}));
					base.RegisterScript("top.ui._contractTask.location.href = top.ui._contractTask.location.href;");
					return;
				}
				base.RegisterScript("top.ui.alert('导入失败!\\n导入的Excel中有节点的任务编码与已有的节点出现重复，请修改！');");
				base.RegisterScript("top.ui._contractTask.location.href = top.ui._contractTask.location.href;");
				return;
			}
			else
			{
				base.RegisterScript("top.ui.alert('Excel导入失败！\\n你选择要导入的上级节点已不存在！');");
				base.RegisterScript("top.ui._contractTask.location.href = top.ui._contractTask.location.href;");
			}
		}
	}
	protected void AspNetPagerWBS_PageChanged(object sender, System.EventArgs e)
	{
		this.BindWBSData();
	}
	protected void AspNetPagerRes_PageChanged(object sender, System.EventArgs e)
	{
		this.BindResData();
	}
	protected void AspNetPagerError_PageChanged(object sender, System.EventArgs e)
	{
		this.BindErrorData();
	}
	protected void btnClose_Click(object sender, System.EventArgs e)
	{
		this.CloseSkip();
	}
	protected void gvwWBSData_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = (e.Row.RowIndex + (this.AspNetPagerWBS.CurrentPageIndex - 1) * NBasePage.pagesize).ToString();
		}
	}
	protected void gvwResData_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = (e.Row.RowIndex + (this.AspNetPagerRes.CurrentPageIndex - 1) * NBasePage.pagesize).ToString();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldCurrentTabIndex.Value;
		if (value == "1")
		{
			this.dtRes = (this.ViewState["dtres"] as DataTable);
			this.DelDataRow(this.hfldResChecked.Value, this.dtRes, value);
			this.BindResData();
			return;
		}
		if (value == "" || value == "0")
		{
			this.dtWBS = (this.ViewState["dtwbs"] as DataTable);
			this.DelDataRow(this.hfldWBSChecked.Value, this.dtWBS, value);
			this.BindWBSData();
		}
	}
	protected void DelDataRow(string rowIndexs, DataTable dt, string tabIndex)
	{
		if (!string.IsNullOrEmpty(rowIndexs))
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (rowIndexs.Contains("["))
			{
				list = JsonHelper.GetListFromJson(rowIndexs);
			}
			else
			{
				list.Add(rowIndexs);
			}
			System.Collections.Generic.List<DataRow> list2 = new System.Collections.Generic.List<DataRow>();
			foreach (string current in list)
			{
				list2.Add(dt.Rows[System.Convert.ToInt32(current)]);
			}
			foreach (DataRow current2 in list2)
			{
				dt.Rows.Remove(current2);
			}
			if (tabIndex == "1")
			{
				this.ViewState["dtres"] = dt;
				return;
			}
			this.ViewState["dtwbs"] = dt;
		}
	}
	private void ClearDataAndInitialize()
	{
		this.errors.Clear();
		this.dtWBS.Clear();
		this.dtRes.Clear();
		this.hfldExcelColumns.Value = string.Empty;
		this.hfldResource.Value = string.Empty;
		this.AspNetPagerRes.CurrentPageIndex = 1;
		this.AspNetPagerWBS.CurrentPageIndex = 1;
		this.AspNetPagerError.CurrentPageIndex = 1;
		this.btnClose.Visible = false;
		this.ViewState["errors"] = this.errors;
		this.ViewState["dtwbs"] = this.errors;
		this.ViewState["dtres"] = this.dtRes;
		this.GetExcelData();
		this.BindWBSData();
		this.BindResData();
		this.BindErrorData();
		if (this.errors.Count > 0 && this.dtWBS.Rows.Count == 0)
		{
			base.RegisterScript("setTem('2')");
		}
	}
	private bool ValidateExcel(System.Web.UI.WebControls.FileUpload fup)
	{
		if (!fup.HasFile)
		{
			base.RegisterScript("top.ui.alert('请选择文件！')");
			return false;
		}
		string extension = System.IO.Path.GetExtension(fup.FileName);
		if (string.Compare(extension, ".xls", true) != 0 && string.Compare(extension, ".xls", true) != 0)
		{
			base.RegisterScript("top.ui.alert('请选择Excel文件！')");
			return false;
		}
		return true;
	}
	private void FormatString(DataTable dt)
	{
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			for (int j = 0; j < dt.Columns.Count; j++)
			{
				if (dt.Rows[i][j].ToString().Length > 30)
				{
					dt.Rows[i][j] = StringUtility.GetStr(dt.Rows[i][j].ToString());
				}
			}
		}
	}
	private void AddDrop(DataTable dt, int sheetIndxe, GridView gv)
	{
		GridViewRow gridViewRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
		Bud_Task bud_Task = new Bud_Task();
		DataTable dataTable = new DataTable();
		if (sheetIndxe == 0)
		{
			dataTable = bud_Task.GetDescription("Bud_ContractTask");
			DataRow dataRow = dataTable.NewRow();
			dataRow["name"] = "Rank";
			dataRow["value"] = "级别";
			dataTable.Rows.Add(dataRow);
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["name"] = "TaskCode";
			dataRow2["value"] = "任务编码";
			dataTable.Rows.Add(dataRow2);
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["name"] = "TaskName";
			dataRow3["value"] = "任务名称";
			dataTable.Rows.Add(dataRow3);
			DataRow dataRow4 = dataTable.NewRow();
			dataRow4["name"] = "Total";
			dataRow4["value"] = "小计";
			dataTable.Rows.Add(dataRow4);
			DataRow dataRow5 = dataTable.NewRow();
			dataRow5["name"] = "ConstructionPeriod";
			dataRow5["value"] = "工期";
			dataTable.Rows.Add(dataRow5);
		}
		else
		{
			if (sheetIndxe == 1)
			{
				dataTable = bud_Task.GetResource();
				DataRow dataRow6 = dataTable.NewRow();
				dataRow6["name"] = "UnitPrice";
				dataRow6["value"] = "单价";
				dataTable.Rows.Add(dataRow6);
				DataRow dataRow7 = dataTable.NewRow();
				dataRow7["name"] = "Quantity";
				dataRow7["value"] = "数量";
				dataTable.Rows.Add(dataRow7);
				DataRow dataRow8 = dataTable.NewRow();
				dataRow8["name"] = "Amount";
				dataRow8["value"] = "合计金额";
				dataTable.Rows.Add(dataRow8);
				DataRow dataRow9 = dataTable.NewRow();
				dataRow9["name"] = "TaskCode";
				dataRow9["value"] = "清单编码";
				dataTable.Rows.Add(dataRow9);
			}
		}
		DataRow dataRow10 = dataTable.NewRow();
		dataRow10["name"] = "Invalid";
		dataRow10["value"] = "无效列";
		dataTable.Rows.Add(dataRow10);
		DataRow dataRow11 = dataTable.NewRow();
		dataRow11["name"] = "SerialNo";
		dataRow11["value"] = "序号";
		dataTable.Rows.Add(dataRow11);
		DataRow dataRow12 = dataTable.NewRow();
		dataRow12["value"] = "选择列";
		dataTable.Rows.InsertAt(dataRow12, 0);
		for (int i = 0; i < dt.Columns.Count + 2; i++)
		{
			TableCell tableCell = new TableCell();
			tableCell.Attributes["align"] = "left";
			tableCell.Attributes["colspan"] = "1";
			DropDownList dropDownList = new DropDownList
			{
				ID = "drop" + sheetIndxe.ToString() + i,
				DataSource = dataTable,
				DataTextField = "value",
				DataValueField = "name"
			};
			dropDownList.DataBind();
			dropDownList.Items[0].Attributes["style"] = "color:Red";
			if (sheetIndxe == 0)
			{
				dropDownList.Items[1].Text = dropDownList.Items[1].Text + "*";
				dropDownList.Items[2].Text = dropDownList.Items[2].Text + "*";
				dropDownList.Items[3].Text = dropDownList.Items[3].Text + "*";
			}
			else
			{
				if (sheetIndxe == 1)
				{
					dropDownList.Items[2].Text = dropDownList.Items[2].Text + "*";
					dropDownList.Items[5].Text = dropDownList.Items[5].Text + "*";
					dropDownList.Items[12].Text = dropDownList.Items[12].Text + "*";
					dropDownList.Items[14].Text = dropDownList.Items[14].Text + "*";
					dropDownList.Items[15].Text = dropDownList.Items[15].Text + "*";
					dropDownList.Items[16].Text = dropDownList.Items[16].Text + "*";
					dropDownList.Items[17].Text = dropDownList.Items[17].Text + "*";
				}
			}
			if (i == 0 || i == 1)
			{
				dropDownList.Style.Add("display", "none");
			}
			if (i >= 2)
			{
				DataColumn dataColumn = dt.Columns[i - 2];
				ListItem listItem = dropDownList.Items.FindByText(dataColumn.ColumnName.Trim());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				else
				{
					listItem = dropDownList.Items.FindByText(dataColumn.ColumnName.Trim() + "*");
					if (listItem != null)
					{
						listItem.Selected = true;
					}
				}
			}
			tableCell.Controls.Add(dropDownList);
			gridViewRow.Cells.Add(tableCell);
			this.SaveDropDownListState(sheetIndxe, i, dropDownList);
		}
		if (dt.Rows.Count > 0)
		{
			gv.Controls[0].Controls.AddAt(1, gridViewRow);
		}
	}
	private void SaveDropDownListState(int sheetIndex, int arrayIndex, DropDownList ddl)
	{
		string[] array = null;
		if (!string.IsNullOrEmpty(this.hfldExcelColumns.Value) && sheetIndex == 0)
		{
			array = this.hfldExcelColumns.Value.Split(new char[]
			{
				','
			});
		}
		else
		{
			if (!string.IsNullOrEmpty(this.hfldResource.Value))
			{
				array = this.hfldResource.Value.Split(new char[]
				{
					','
				});
			}
		}
		if (array != null)
		{
			ddl.SelectedValue = array[arrayIndex];
		}
	}
	private void AddResource()
	{
		if (this.ViewState["ExcelName"] != null && !string.IsNullOrEmpty(this.ViewState["ExcelName"].ToString()))
		{
			this.dtRes = (this.ViewState["dtres"] as DataTable);
			if (this.dtRes.Rows.Count > 0)
			{
				BudTaskResourceServices budTaskResourceServices = new BudTaskResourceServices(this.dtRes, WebUtil.GetUserNames(base.UserCode));
				budTaskResourceServices.AddConResource(this.resourceArray, this.inputUser, this.errors, this.prjId);
			}
		}
	}
	private void ShowErrors()
	{
		if (this.ViewState["errors"] != null)
		{
			this.errors = (this.ViewState["errors"] as System.Collections.Generic.List<string>);
		}
		if (this.errors.Count > 0)
		{
			base.RegisterScript("setTem('2')");
			this.AspNetPagerError.CurrentPageIndex = 1;
			this.BindErrorData();
		}
	}
	private void GetExcelData()
	{
		this.dtWBS = ExcelHelper.ImportDataTableFromExcel(this.ViewState["ExcelName"].ToString(), 0, this.errors);
		this.ViewState["dtwbs"] = this.dtWBS;
		int sheetCounts = ExcelHelper.GetSheetCounts(this.excelName);
		this.ViewState["sheetcount"] = sheetCounts;
		if (sheetCounts >= 2)
		{
			this.dtRes = ExcelHelper.ImportDataTableFromExcel(this.ViewState["ExcelName"].ToString(), 1, this.errors);
			this.ViewState["dtres"] = this.dtRes;
		}
	}
	private void BindWBSData()
	{
		if (this.ViewState["dtwbs"] == null)
		{
			return;
		}
		this.dtWBS = (this.ViewState["dtwbs"] as DataTable);
		this.AspNetPagerWBS.RecordCount = this.dtWBS.Rows.Count;
		this.AspNetPagerWBS.PageSize = NBasePage.pagesize;
		DataTable pageDataTable = BudgetManage_Budget_ExcelImportContract.GetPageDataTable(this.dtWBS, this.AspNetPagerWBS.CurrentPageIndex, this.AspNetPagerWBS.PageSize);
		this.gvwWBSData.DataSource = pageDataTable;
		this.FormatString(pageDataTable);
		this.gvwWBSData.DataBind();
		this.AddDrop(pageDataTable, 0, this.gvwWBSData);
		if (this.gvwWBSData.Rows.Count > 0)
		{
			this.btmImport.Enabled = true;
			return;
		}
		this.btmImport.Enabled = false;
	}
	private void BindResData()
	{
		if (this.ViewState["sheetcount"] == null)
		{
			return;
		}
		int num = 0;
		int num2 = System.Convert.ToInt32(this.ViewState["sheetcount"]);
		if (num2 >= 2)
		{
			if (this.ViewState["dtres"] == null)
			{
				return;
			}
			this.dtRes = (this.ViewState["dtres"] as DataTable);
			this.AspNetPagerRes.RecordCount = this.dtRes.Rows.Count;
			this.AspNetPagerRes.PageSize = NBasePage.pagesize;
			DataTable pageDataTable = BudgetManage_Budget_ExcelImportContract.GetPageDataTable(this.dtRes, this.AspNetPagerRes.CurrentPageIndex, this.AspNetPagerRes.PageSize);
			this.gvwResData.DataSource = pageDataTable;
			this.FormatString(pageDataTable);
			this.gvwResData.DataBind();
			this.AddDrop(pageDataTable, 1, this.gvwResData);
			num = this.dtRes.Rows.Count;
		}
		else
		{
			this.gvwResData.DataSource = null;
			this.gvwResData.DataBind();
		}
		this.hfldValidRes.Value = num.ToString();
	}
	private void BindErrorData()
	{
		DataTable dataTable = new DataTable();
		if (this.ViewState["errors"] != null)
		{
			this.errors = (this.ViewState["errors"] as System.Collections.Generic.List<string>);
		}
		if (this.errors.Count > 0)
		{
			this.btnClose.Visible = true;
			dataTable = this.ConvertListToDataTable(this.errors);
			this.AspNetPagerError.PageSize = NBasePage.pagesize;
			this.AspNetPagerError.RecordCount = dataTable.Rows.Count;
			this.gvwError.DataSource = BudgetManage_Budget_ExcelImportContract.GetPageDataTable(dataTable, this.AspNetPagerError.CurrentPageIndex, this.AspNetPagerError.PageSize);
			this.gvwError.DataBind();
			return;
		}
		this.gvwError.DataSource = null;
		this.gvwError.DataBind();
	}
	private void CloseSkip()
	{
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_contractTask' });");
	}
	private DataTable ConvertListToDataTable(System.Collections.Generic.List<string> errorList)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("错误信息");
		foreach (string current in errorList)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["错误信息"] = current;
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
	private static DataTable GetPageDataTable(DataTable dtAll, int pageIndex, int pageSize)
	{
		DataTable dataTable = dtAll.Clone();
		int num = (pageIndex - 1) * pageSize;
		int num2 = num;
		while (num2 < dtAll.Rows.Count && num2 != num + pageSize)
		{
			DataRow dataRow = dtAll.Rows[num2];
			dataTable.Rows.Add(dataRow.ItemArray);
			num2++;
		}
		return dataTable;
	}
}


