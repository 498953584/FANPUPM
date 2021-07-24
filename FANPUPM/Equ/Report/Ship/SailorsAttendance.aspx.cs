using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_SailorsAttendance : NBasePage, IRequiresSessionState
{
	private ApplicationAction action = new ApplicationAction();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private string[] total = new string[32];

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTvw();
			this.BindDrop();
			this.ComputeTotal();
			this.BindGvw();
		}
	}
	private void BindTvw()
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Selected = true;
				this.tvwDep.Nodes.Add(treeNode);
				this.BindSubDep(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	private void BindSubDep(TreeNode parentNode, string strBMDM)
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				parentNode.ChildNodes.Add(treeNode);
				this.BindSubDep(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected void tvwDep_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGvw();
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		int? depId = null;
		if (!string.IsNullOrEmpty(this.tvwDep.SelectedValue.Trim()))
		{
			depId = new int?(System.Convert.ToInt32(this.tvwDep.SelectedValue.Trim()));
		}
		DataTable table = this.GetTable(depId, 0, 0);
		if (table.Rows.Count > 0)
		{
			for (int i = 0; i <= 30; i++)
			{
				this.total[i] = System.Convert.ToDecimal(table.Compute("sum([" + (i + 1).ToString() + "])", "1>0")).ToString("0");
			}
			this.total[31] = System.Convert.ToDecimal(table.Compute("sum(TotalLeaveDays)", "1>0")).ToString("0");
			return;
		}
		for (int j = 0; j < 32; j++)
		{
			this.total[j] = "0";
		}
	}
	private void BindDrop()
	{
		int year = System.DateTime.Now.Year;
		for (int i = year - 4; i <= year; i++)
		{
			this.dropYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
		}
		for (int j = 1; j <= 12; j++)
		{
			this.dropMonth.Items.Add(new ListItem(j.ToString() + "月", j.ToString("00")));
		}
		this.dropYear.SelectedValue = year.ToString();
		this.dropMonth.SelectedIndex = System.DateTime.Now.Month - 2;
	}
	private void BindGvw()
	{
		this.gvwAttendance.Columns.Clear();
		int? depId = null;
		if (!string.IsNullOrEmpty(this.tvwDep.SelectedValue.Trim()))
		{
			depId = new int?(System.Convert.ToInt32(this.tvwDep.SelectedValue.Trim()));
		}
		DataTable table = this.GetTable(depId, NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwAttendance.DataSource = table;
		BoundField boundField = new BoundField();
		boundField.DataField = "Num";
		boundField.HeaderText = "序号";
		boundField.HeaderStyle.Width = 25;
		this.gvwAttendance.Columns.Add(boundField);
		BoundField boundField2 = new BoundField();
		boundField2.DataField = "v_xm";
		boundField2.HeaderText = "姓名";
		this.gvwAttendance.Columns.Add(boundField2);
		for (int i = 3; i < 35; i++)
		{
			BoundField boundField3 = new BoundField();
			if (i <= 33)
			{
				boundField3.DataField = table.Columns[i].ColumnName;
				boundField3.HeaderText = table.Columns[i].Caption;
			}
			if (i == 34)
			{
				boundField3.DataField = table.Columns[i].ColumnName;
				boundField3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				boundField3.HeaderText = "请假总天数";
			}
			this.gvwAttendance.Columns.Add(boundField3);
		}
		BoundField boundField4 = new BoundField();
		boundField4.HeaderText = "签名";
		this.gvwAttendance.Columns.Add(boundField4);
		this.gvwAttendance.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.gvwAttendance.DataSource = null;
		this.BindGvw();
	}
	protected void gvwAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			int days = this.GetDays();
			string a = string.Empty;
			for (int i = 2; i <= days + 1; i++)
			{
				a = e.Row.Cells[i].Text;
				if (a == "1")
				{
					e.Row.Cells[i].Text = "╳";
				}
				if (a == "0")
				{
					e.Row.Cells[i].Text = "√";
				}
			}
			if (days < 31)
			{
				for (int j = days + 1; j <= 31; j++)
				{
					e.Row.Cells[j + 1].Text = "";
				}
			}
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			int days2 = this.GetDays();
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			for (int k = 1; k <= days2; k++)
			{
				e.Row.Cells[k + 1].Style.Add("text-align", "right");
				e.Row.Cells[k + 1].Text = this.total[k - 1].ToString();
			}
			e.Row.Cells[33].Style.Add("text-align", "right");
			e.Row.Cells[33].Text = this.total[31];
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		int? depId = null;
		if (!string.IsNullOrEmpty(this.tvwDep.SelectedValue.Trim()))
		{
			depId = new int?(System.Convert.ToInt32(this.tvwDep.SelectedValue.Trim()));
		}
		DataTable table = this.GetTable(depId, 0, 0);
		DataTable dataTable = this.Replace(table);
		if (table.Rows.Count > 0)
		{
			int days = this.GetDays();
			DataRow dataRow = dataTable.NewRow();
			dataRow["v_xm"] = "合计";
			for (int i = 1; i <= days; i++)
			{
				dataRow[i + 2] = table.Compute("sum([" + i.ToString() + "])", "1>0").ToString();
			}
			dataRow["TotalLeaveDays"] = table.Compute("sum(TotalLeaveDays)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(dataTable, "人员请假统计表", "人员请假统计表", "人员请假统计表.xls", list, null, 0, base.Request.Browser.Browser);
	}
	private DataTable Replace(DataTable dt)
	{
		int days = this.GetDays();
		DataTable dataTable = new DataTable();
		foreach (DataColumn dataColumn in dt.Columns)
		{
			DataColumn dataColumn2 = new DataColumn();
			dataColumn2.ColumnName = dataColumn.ColumnName;
			dataColumn2.DataType = System.Type.GetType("System.String");
			dataColumn2.ReadOnly = false;
			dataTable.Columns.Add(dataColumn2);
		}
		foreach (DataRow arg_99_0 in dt.Rows)
		{
			DataRow row = dataTable.NewRow();
			dataTable.Rows.Add(row);
		}
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			for (int j = 0; j < dt.Columns.Count; j++)
			{
				if (j >= 3 && j <= days + 3)
				{
					if (dt.Rows[i][j].ToString() == "0")
					{
						dataTable.Rows[i][j] = "√";
					}
					if (dt.Rows[i][j].ToString() == "1")
					{
						dataTable.Rows[i][j] = "╳";
					}
				}
				else
				{
					if (days < 31)
					{
						this.GetDays();
						for (int k = days + 1; k < 31; k++)
						{
							dataTable.Rows[i][k + 3] = "";
						}
					}
					dataTable.Rows[i][j] = dt.Rows[i][j];
				}
			}
		}
		return dataTable;
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		int days = this.GetDays();
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["UserCode"] != null)
		{
			dt.Columns.Remove(dt.Columns["UserCode"].ColumnName);
		}
		if (dt.Columns["v_xm"] != null)
		{
			dt.Columns["v_xm"].ColumnName = "姓名";
		}
		for (int i = days + 1; i <= 31; i++)
		{
			if (dt.Columns[i.ToString()] != null)
			{
				dt.Columns.Remove(i.ToString());
			}
		}
		if (dt.Columns["TotalLeaveDays"] != null)
		{
			dt.Columns["TotalLeaveDays"].ColumnName = "请假总天数";
		}
		dt.Columns.Add("签名");
		dt.AcceptChanges();
		return dt;
	}
	private DataTable GetTable(int? depId, int pageSize, int pageNo)
	{
		string selectedValue = this.dropYear.SelectedValue;
		string selectedValue2 = this.dropMonth.SelectedValue;
		string text = selectedValue + "-" + selectedValue2 + "-01";
		string endDate = System.Convert.ToDateTime(text).AddMonths(1).ToString("yyyy-MM-dd");
		return this.action.GetAttendanceTable(text, endDate, depId, pageSize, pageNo);
	}
	private int GetCount(int? depId)
	{
		string selectedValue = this.dropYear.SelectedValue;
		string selectedValue2 = this.dropMonth.SelectedValue;
		string text = selectedValue + "-" + selectedValue2 + "-01";
		string endDate = System.Convert.ToDateTime(text).AddMonths(1).ToString("yyyy-MM-dd");
		return this.action.GetAttendanceCount(text, endDate, depId);
	}
	private int GetDays()
	{
		int num = System.Convert.ToInt32(this.dropYear.SelectedValue);
		int num2 = this.dropMonth.SelectedIndex + 1;
		int result;
		if (num2 == 2)
		{
			if (num % 400 == 0 || (num % 4 == 0 && num % 100 != 0))
			{
				result = 29;
			}
			else
			{
				result = 28;
			}
		}
		else
		{
			int num3 = num2;
			switch (num3)
			{
			case 4:
			case 6:
				break;
			case 5:
				goto IL_75;
			default:
				switch (num3)
				{
				case 9:
				case 11:
					break;
				case 10:
					goto IL_75;
				default:
					goto IL_75;
				}
				break;
			}
			result = 30;
			return result;
			IL_75:
			result = 31;
		}
		return result;
	}
}


