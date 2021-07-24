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
public partial class Salary2_PayoffSalary : NBasePage, IRequiresSessionState
{
	private string DepartmentId = string.Empty;
	private PtYhmcBll ptyhBll = new PtYhmcBll();
	private System.Collections.Generic.List<SASalaryBooks> saBooks = new System.Collections.Generic.List<SASalaryBooks>();
	private SAPayoffService saPayoffService = new SAPayoffService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["department"]))
		{
			this.DepartmentId = base.Request["department"].ToString();
		}
		SASalaryBooksService source = new SASalaryBooksService();
		this.saBooks = (
			from sbs in source
			where sbs.IsValid == true
			orderby sbs.InputDate
			select sbs).ToList<SASalaryBooks>();
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AddItem();
			this.BindSaBooks(this.ddlSaBooks);
			this.hfldDepartment.Value = this.DepartmentId;
			this.hfldSaBooksId.Value = this.ddlSaBooks.SelectedValue.Trim();
			this.BindGV();
		}
	}
	private void BindGV()
	{
		this.gvwSaMonth.Columns.Clear();
		string userCode = this.txtUserCode.Text.Trim();
		string userName = this.txtUserName.Text.Trim();
		string text = this.ddlSaBooks.SelectedValue.Trim();
		int year = System.Convert.ToInt32(this.ddlYear.SelectedValue.Trim());
		int month = System.Convert.ToInt32(this.ddlMonth.SelectedValue.Trim());
		this.hfldSaBooksId.Value = text;
		this.hfldYear.Value = year.ToString();
		this.hfldMonth.Value = month.ToString();
		this.AspNetPager1.RecordCount = this.ptyhBll.GetSaMonthCountPayoff(this.DepartmentId, text, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim());
		DataTable saMonthInfoPayoff = this.ptyhBll.GetSaMonthInfoPayoff(this.DepartmentId, text, userCode, userName, year, month, this.ddlIsPayoff.SelectedValue.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		new System.Collections.Generic.List<SASalaryItem>();
		DataTable saItems = this.ptyhBll.GetSaItems(text, year, month);
		BoundField boundField = new BoundField();
		boundField.HeaderStyle.Width = 20;
		this.gvwSaMonth.Columns.Add(boundField);
		BoundField boundField2 = new BoundField();
		boundField2.DataField = "Num";
		boundField2.HeaderText = "序号";
		boundField2.HeaderStyle.Width = 25;
		this.gvwSaMonth.Columns.Add(boundField2);
		BoundField boundField3 = new BoundField();
		boundField3.DataField = "UserCode";
		boundField3.HeaderText = "员工编号";
		boundField3.HeaderStyle.Width = 80;
		this.gvwSaMonth.Columns.Add(boundField3);
		BoundField boundField4 = new BoundField();
		boundField4.DataField = "v_xm";
		boundField4.HeaderText = "员工姓名";
		boundField4.HeaderStyle.Width = 150;
		this.gvwSaMonth.Columns.Add(boundField4);
		BoundField boundField5 = new BoundField();
		boundField5.DataField = "IsPayoffName";
		boundField5.HeaderText = "是否发放";
		boundField5.HeaderStyle.Width = 50;
		this.gvwSaMonth.Columns.Add(boundField5);
		foreach (DataRow dataRow in saItems.Rows)
		{
			BoundField boundField6 = new BoundField();
			boundField6.DataField = dataRow["Name"].ToString();
			boundField6.HeaderText = dataRow["Name"].ToString();
			boundField6.HeaderStyle.Width = 80;
			boundField6.ItemStyle.CssClass = "decimal_input";
			this.gvwSaMonth.Columns.Add(boundField6);
		}
		this.gvwSaMonth.DataSource = saMonthInfoPayoff;
		this.gvwSaMonth.DataBind();
	}
	protected void gvwSaMonth_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			CheckBox checkBox = new CheckBox();
			checkBox.ID = "chbAll";
			e.Row.Cells[0].Controls.Add(checkBox);
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwSaMonth.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isPayoff"] = this.gvwSaMonth.DataKeys[e.Row.RowIndex].Values[1].ToString();
			CheckBox checkBox2 = new CheckBox();
			checkBox2.ID = "chbSingle";
			e.Row.Cells[0].Controls.Add(checkBox2);
			e.Row.Cells[0].Attributes.Add("align", "center");
			e.Row.Cells[1].Attributes.Add("align", "right");
			e.Row.Cells[4].Attributes.Add("align", "center");
			for (int i = 5; i < e.Row.Cells.Count; i++)
			{
				e.Row.Cells[i].Attributes.Add("align", "right");
				e.Row.Cells[i].Attributes.Add("class", "decimal_input");
				string text = e.Row.Cells[i].Text;
				if (text == "&nbsp;")
				{
					e.Row.Cells[i].Text = "0.000";
				}
			}
		}
	}
	private void BindSaBooks(DropDownList ddl)
	{
		ddl.DataSource = this.saBooks;
		ddl.DataTextField = "Name";
		ddl.DataValueField = "Id";
		ddl.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
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
	protected void btnPayoff_Click(object sender, System.EventArgs e)
	{
		int year = System.Convert.ToInt32(this.hfldYear.Value.Trim());
		int month = System.Convert.ToInt32(this.hfldMonth.Value.Trim());
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldCheckedId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldCheckedId.Value);
		}
		else
		{
			list.Add(this.hfldCheckedId.Value);
		}
		foreach (string userCode in list)
		{
			if ((
				from sps in this.saPayoffService
				where sps.UserCode == userCode && sps.Year == year && sps.Month == month
				select sps).FirstOrDefault<SAPayoff>() == null)
			{
				SAPayoff sAPayoff = new SAPayoff();
				sAPayoff.Id = System.Guid.NewGuid().ToString();
				sAPayoff.UserCode = userCode;
				sAPayoff.Year = year;
				sAPayoff.Month = month;
				sAPayoff.IsPayoff = true;
				sAPayoff.BooksId = this.ddlSaBooks.SelectedValue.Trim();
				this.saPayoffService.Add(sAPayoff);
			}
		}
		this.BindGV();
	}
}


