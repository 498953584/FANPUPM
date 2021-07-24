using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Salary2_SalaryMonthReport : NBasePage, IRequiresSessionState
{
	private string DepartmentId = string.Empty;
	private PtYhmcBll ptyhBll = new PtYhmcBll();
	private SASalaryItemService saItemService = new SASalaryItemService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["department"]))
		{
			this.DepartmentId = base.Request["department"].ToString();
		}
		new SASalaryBooksService();
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AddItem();
			this.hfldDepartment.Value = this.DepartmentId;
			this.ComputeTotal();
			this.BindGV();
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		string userCode = this.txtUserCode.Text.Trim();
		string userName = this.txtUserName.Text.Trim();
		int year = System.Convert.ToInt32(this.ddlYear.SelectedValue.Trim());
		int month = System.Convert.ToInt32(this.ddlMonth.SelectedValue.Trim());
		this.hfldYear.Value = year.ToString();
		this.hfldMonth.Value = month.ToString();
		this.AspNetPager1.RecordCount = this.ptyhBll.GetSaMonthCountReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim());
		DataTable dataTable = this.ptyhBll.GetSaMonthInfoReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim(), 0, 0);
		System.Collections.Generic.List<SASalaryItem> list = (
			from sis in this.saItemService
			orderby sis.No
			select sis).ToList<SASalaryItem>();
		string[] array = new string[list.Count];
		if (dataTable.Rows.Count != 0)
		{
			dataTable = this.formattingData(dataTable, false);
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = System.Convert.ToDecimal(dataTable.Compute("sum([" + list[i].Name + "])", "1>0")).ToString("0.000");
			}
		}
		else
		{
			for (int j = 0; j < list.Count; j++)
			{
				array[j] = "0.000";
			}
		}
		this.ViewState["Total"] = array;
	}
	private void BindGV()
	{
		string userCode = this.txtUserCode.Text.Trim();
		string userName = this.txtUserName.Text.Trim();
		int year = System.Convert.ToInt32(this.ddlYear.SelectedValue.Trim());
		int month = System.Convert.ToInt32(this.ddlMonth.SelectedValue.Trim());
		this.hfldYear.Value = year.ToString();
		this.hfldMonth.Value = month.ToString();
		this.AspNetPager1.RecordCount = this.ptyhBll.GetSaMonthCountReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim());
		DataTable saMonthInfoReport = this.ptyhBll.GetSaMonthInfoReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		System.Collections.Generic.List<SASalaryItem> list = (
			from sis in this.saItemService
			orderby sis.No
			select sis).ToList<SASalaryItem>();
		if (this.gvwSaMonth.Columns.Count < 3 + list.Count)
		{
			foreach (SASalaryItem current in list)
			{
				BoundField boundField = new BoundField();
				boundField.DataField = current.Name.Trim();
				boundField.HeaderText = current.Name.Trim();
				boundField.HeaderStyle.Width = 80;
				boundField.ItemStyle.CssClass = "decimal_input";
				this.gvwSaMonth.Columns.Add(boundField);
			}
		}
		this.gvwSaMonth.DataSource = saMonthInfoReport;
		this.gvwSaMonth.DataBind();
	}
	protected void gvwSaMonth_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwSaMonth.DataKeys[e.Row.RowIndex].Value.ToString();
			for (int i = 4; i < e.Row.Cells.Count; i++)
			{
				e.Row.Cells[i].Attributes.Add("align", "right");
				e.Row.Cells[i].Attributes.Add("class", "decimal_input");
				string text = e.Row.Cells[i].Text;
				if (text == "&nbsp;")
				{
					e.Row.Cells[i].Text = "0.000";
				}
			}
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			for (int j = 4; j < e.Row.Cells.Count; j++)
			{
				e.Row.Cells[0].Text = "合计";
				string[] array = (string[])this.ViewState["Total"];
				e.Row.Cells[j].Text = array[j - 4];
				e.Row.Cells[j].Style.Add("text-align", "right");
				e.Row.Cells[j].CssClass = "decimal_input";
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
		this.ComputeTotal();
		this.BindGV();
	}
	private void AddItem()
	{
		for (int i = 1; i <= 12; i++)
		{
			this.ddlMonth.Items.Add(new ListItem(i.ToString() + "月", i.ToString("00")));
		}
		for (int j = System.DateTime.Now.Year - 4; j <= System.DateTime.Now.Year; j++)
		{
			this.ddlYear.Items.Add(new ListItem(j.ToString() + "年", j.ToString()));
		}
		if (System.DateTime.Now.Month.ToString("00") == "01")
		{
			string selectedValue = System.DateTime.Now.AddYears(-1).Year.ToString("00");
			this.ddlYear.SelectedValue = selectedValue;
			this.ddlMonth.SelectedValue = "12";
			return;
		}
		this.ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
		string selectedValue2 = System.DateTime.Now.AddMonths(-1).Month.ToString("00");
		this.ddlMonth.SelectedValue = selectedValue2;
	}
	private DataTable formattingData(DataTable dt, bool isExport)
	{
		foreach (DataRow dataRow in dt.Rows)
		{
			for (int i = 8; i < dt.Columns.Count; i++)
			{
				if (dataRow[i] == System.DBNull.Value)
				{
					dataRow[i] = decimal.Parse("0.000");
				}
			}
		}
		if (isExport && dt.Rows.Count > 0)
		{
			if (dt.Columns["Num"] != null)
			{
				dt.Columns["Num"].ColumnName = "序号";
			}
			if (dt.Columns["UserCode"] != null)
			{
				dt.Columns["UserCode"].ColumnName = "员工编号";
			}
			if (dt.Columns["v_xm"] != null)
			{
				dt.Columns["v_xm"].ColumnName = "员工姓名";
			}
			if (dt.Columns["IsPayoffName"] != null)
			{
				dt.Columns["IsPayoffName"].ColumnName = "是否发放";
			}
			if (dt.Columns["IsPayoff"] != null)
			{
				dt.Columns.Remove(dt.Columns["IsPayoff"]);
			}
			if (dt.Columns["v_yhdm"] != null)
			{
				dt.Columns.Remove(dt.Columns["v_yhdm"]);
			}
			if (dt.Columns["Month"] != null)
			{
				dt.Columns.Remove(dt.Columns["Month"]);
			}
			if (dt.Columns["year"] != null)
			{
				dt.Columns.Remove(dt.Columns["year"]);
			}
		}
		return dt;
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		string userCode = this.txtUserCode.Text.Trim();
		string userName = this.txtUserName.Text.Trim();
		int year = System.Convert.ToInt32(this.ddlYear.SelectedValue.Trim());
		int month = System.Convert.ToInt32(this.ddlMonth.SelectedValue.Trim());
		this.hfldYear.Value = year.ToString();
		this.hfldMonth.Value = month.ToString();
		this.AspNetPager1.RecordCount = this.ptyhBll.GetSaMonthCountReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim());
		DataTable dataTable = this.ptyhBll.GetSaMonthInfoReport(this.DepartmentId, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim(), 0, 0);
		System.Collections.Generic.List<SASalaryItem> list = (
			from sis in this.saItemService
			orderby sis.No
			select sis).ToList<SASalaryItem>();
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["UserCode"] = "合计";
			dataTable = this.formattingData(dataTable, false);
			for (int i = 0; i < list.Count; i++)
			{
				dataRow[list[i].Name] = System.Convert.ToDecimal(dataTable.Compute("sum([" + list[i].Name + "])", "1>0")).ToString("0.000");
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.formattingData(dataTable, true);
		ExcelHelper.ExportExcel(dataTable, "工资报表", "工资报表", "工资报表.xls", null, null, 3, base.Request.Browser.Browser);
	}
}


