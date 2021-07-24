using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Budget_ExcelImportSpecial : NBasePage, IRequiresSessionState
{
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
	private string resTypeCloumnName = "resType";

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
		if (!string.IsNullOrEmpty(base.Request["version"]))
		{
			this.version = System.Convert.ToInt32(base.Request["version"]);
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
			this.BindWBSData();
			this.BindResData();
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
			new System.IO.FileInfo(this.excelName)
			{
				IsReadOnly = false
			}.Delete();
		}
		this.fupExcel.SaveAs(this.excelName);
		this.ClearDataAndInitialize();
	}
	protected void ClearDataAndInitialize()
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
		this.ViewState["dtwbs"] = this.dtWBS;
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
			base.RegisterScript("alert('系统提示：\\n请选择文件！')");
			return false;
		}
		string extension = System.IO.Path.GetExtension(fup.FileName);
		if (string.Compare(extension, ".xls", true) != 0 && string.Compare(extension, ".xls", true) != 0)
		{
			base.RegisterScript("alert('系统提示：\\n请选择Excel文件！')");
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
					Project.CoverVersion(this.prjId);
				}
			}
			else
			{
				flag = (BudTask.GetById(this.taskId) != null);
			}
			if (flag)
			{
				this.inputUser = PageHelper.QueryUser(this, base.UserCode);
				BudTaskSpecial budTaskSpecial = new BudTaskSpecial(this.taskId, this.prjId, this.inputUser, this.dtWBS);
				int num = budTaskSpecial.ConverBudTaskList(this.errors);
				string text = "成功";
				if (num == 0)
				{
					text = "失败";
				}
				string text2 = string.Empty;
				if (this.errors.Count > 0)
				{
					this.ViewState["errors"] = this.errors;
					this.ShowErrors();
					text2 = "\\n详见“警告信息”!";
				}
				else
				{
					this.CloseSkip();
				}
				base.RegisterScript(string.Concat(new string[]
				{
					"alert('系统提示：\\nExcel导入",
					text,
					"！",
					text2,
					"');"
				}));
				return;
			}
			base.RegisterScript("alert('系统提示：\\nExcel导入失败！\\n你选择要导入的上级节点已不存在！');");
		}
	}
	protected void ShowErrors()
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
	protected void GetExcelData()
	{
		int num = 4;
		string s = ConfigHelper.Get("WBSFirstLine");
		int.TryParse(s, out num);
		this.dtWBS = ExcelHelper.ImportDataTableFromExcel(this.ViewState["ExcelName"].ToString(), num - 1, 0, this.errors);
		if (this.dtWBS.Columns.IndexOf(this.resTypeCloumnName) == -1)
		{
			this.dtWBS.Columns.Add(new DataColumn(this.resTypeCloumnName));
		}
		this.CheckRes(this.dtWBS);
		this.ViewState["dtwbs"] = this.dtWBS;
	}
	protected void BindWBSData()
	{
		if (this.ViewState["dtwbs"] == null)
		{
			return;
		}
		this.dtWBS = (this.ViewState["dtwbs"] as DataTable);
		this.AspNetPagerWBS.RecordCount = this.dtWBS.Rows.Count;
		this.AspNetPagerWBS.PageSize = NBasePage.pagesize;
		DataTable pageDataTable = BudgetManage_Budget_ExcelImportSpecial.GetPageDataTable(this.dtWBS, this.AspNetPagerWBS.CurrentPageIndex, this.AspNetPagerWBS.PageSize);
		this.gvwWBSData.DataSource = pageDataTable;
		this.FormatString(pageDataTable);
		this.gvwWBSData.DataBind();
		if (this.gvwWBSData.Rows.Count > 0)
		{
			this.btmImport.Enabled = true;
			return;
		}
		this.btmImport.Enabled = false;
	}
	protected void CheckRes(DataTable dtWBS)
	{
		if (this.dtRes.Columns.IndexOf(this.resTypeCloumnName) == -1)
		{
			foreach (DataColumn dataColumn in dtWBS.Columns)
			{
				this.dtRes.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
		}
		foreach (DataRow dataRow in dtWBS.Rows)
		{
			int columnIndex = 0;
			string input = dataRow[columnIndex].ToString();
			Regex regex = new Regex("^[0-9]+.[0-9]+.[0-9]+$");
			if (regex.IsMatch(input))
			{
				int num = 1;
				string text = dataRow[num].ToString();
				string value = cn.justwin.Domain.Resource.GetResourceId(text);
				if (string.IsNullOrEmpty(value))
				{
					value = "0";
					DataRow[] array = this.dtRes.Select(this.dtRes.Columns[num].ColumnName + "='" + text + "'");
					if (array.Length == 0)
					{
						this.dtRes.ImportRow(dataRow);
					}
				}
				dataRow[dtWBS.Columns.Count - 1] = value;
			}
		}
		this.ViewState["dtres"] = this.dtRes;
	}
	protected void BindResData()
	{
		DataTable dataTable = this.ViewState["dtres"] as DataTable;
		if (dataTable != null)
		{
			this.dtRes = dataTable;
			this.AspNetPagerRes.RecordCount = this.dtRes.Rows.Count;
			this.AspNetPagerRes.PageSize = NBasePage.pagesize;
			DataTable pageDataTable = BudgetManage_Budget_ExcelImportSpecial.GetPageDataTable(this.dtRes, this.AspNetPagerRes.CurrentPageIndex, this.AspNetPagerRes.PageSize);
			this.gvwResData.DataSource = pageDataTable;
			this.FormatString(pageDataTable);
			this.gvwResData.DataBind();
		}
	}
	protected void BindErrorData()
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
			this.AspNetPagerError.RecordCount = dataTable.Rows.Count;
			this.AspNetPagerError.PageSize = NBasePage.pagesize;
			this.gvwError.DataSource = BudgetManage_Budget_ExcelImportSpecial.GetPageDataTable(dataTable, this.AspNetPagerError.CurrentPageIndex, this.AspNetPagerError.PageSize);
			this.gvwError.DataBind();
			return;
		}
		this.gvwError.DataSource = null;
		this.gvwError.DataBind();
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
	protected void CloseSkip()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.Append("parent.desktop.BudgetPlaitList.location='/BudgetManage/Budget/BudgetPlaitList.aspx");
		stringBuilder.Append(string.Concat(new string[]
		{
			"?prjId=",
			this.prjId,
			"&year=",
			this.year,
			"';"
		}));
		stringBuilder.Append("parent.desktop.BudgetPlaitList=null;");
		stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
		base.RegisterScript(stringBuilder.ToString());
	}
	protected DataTable ConvertListToDataTable(System.Collections.Generic.List<string> errorList)
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
	public static DataTable GetPageDataTable(DataTable dtAll, int pageIndex, int pageSize)
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
	protected void gvwWBSData_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = (e.Row.RowIndex + (this.AspNetPagerWBS.CurrentPageIndex - 1) * NBasePage.pagesize).ToString();
			string text = e.Row.Cells[e.Row.Cells.Count - 1].Text;
			if (text == "0")
			{
				e.Row.Style.Add("color", "red");
				e.Row.ToolTip = "此资源在资源库中不存在！";
			}
		}
	}
	protected void gvwResData_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
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
			System.Collections.Generic.List<string> list = this.CovnertToList(rowIndexs);
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
	protected void btnAddRes_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldSelResType.Value;
		string value2 = this.hfldResChecked.Value;
		DataTable dataTable = this.ViewState["dtres"] as DataTable;
		System.Collections.Generic.List<string> list = this.CovnertToList(value2);
		System.Collections.Generic.List<DataRow> list2 = new System.Collections.Generic.List<DataRow>();
		foreach (string current in list)
		{
			DataRow dataRow = dataTable.Rows[System.Convert.ToInt32(current)];
			this.AddResToDB(dataRow, value);
			list2.Add(dataRow);
		}
		foreach (DataRow current2 in list2)
		{
			dataTable.Rows.Remove(current2);
		}
		this.ViewState["dtres"] = dataTable;
		this.BindResData();
	}
	protected System.Collections.Generic.List<string> CovnertToList(string rowIndexs)
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
		return list;
	}
	protected void AddResToDB(DataRow row, string resTypeId)
	{
		if (row != null)
		{
			string id = System.Guid.NewGuid().ToString();
			ResType byId = ResType.GetById(resTypeId);
			string code = row[1].ToString();
			string name = row[2].ToString();
			row[3].ToString();
			string s = row[7].ToString();
			decimal value;
			decimal.TryParse(s, out value);
			string id2 = ConfigHelper.Get("BudgetPriceType");
			System.Collections.Generic.Dictionary<ResPriceType, decimal?> dictionary = new System.Collections.Generic.Dictionary<ResPriceType, decimal?>();
			ResPriceType byId2 = ResPriceType.GetById(id2);
			dictionary.Add(byId2, new decimal?(value));
			try
			{
				cn.justwin.Domain.Resource resource = cn.justwin.Domain.Resource.Create(id, byId, code, name, string.Empty, null, string.Empty, string.Empty, string.Empty, null, null, string.Empty, dictionary, this.inputUser, new System.DateTime?(System.DateTime.Now), null);
				cn.justwin.Domain.Resource.Add(resource);
			}
			catch (System.Exception)
			{
			}
		}
	}
}


