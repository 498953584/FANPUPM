using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Salary2_SaMonthSalary : NBasePage, IRequiresSessionState
{
	private string DepartmentId = string.Empty;
	private PtYhmcBll ptyhBll = new PtYhmcBll();
	private System.Collections.Generic.List<SASalaryBooks> saBooks = new System.Collections.Generic.List<SASalaryBooks>();
	private SASalaryBooksItemService saBooksItemService = new SASalaryBooksItemService();
	private SAMonthSalaryService saMonthService = new SAMonthSalaryService();
	private SASalaryItemService saItemService = new SASalaryItemService();
	private SAPersonalTaxService saTaxService = new SAPersonalTaxService();
	private SAPayoffService saPayoffService = new SAPayoffService();
	private DataTable dtMonth = new DataTable();

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
			if (!string.IsNullOrEmpty(this.DepartmentId))
			{
				this.btnGenerate.Enabled = false;
			}
			else
			{
				this.btnGenerate.Enabled = true;
			}
		}
		this.BindGV();
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
		this.AspNetPager1.RecordCount = this.ptyhBll.GetSaMonthCount(this.DepartmentId, text, userCode, userName, year, month, this.ddlIsGenerate.SelectedValue.Trim());
		this.dtMonth = this.ptyhBll.GetSaMonthInfo(this.DepartmentId, text, userCode, userName, year, month, this.ddlIsGenerate.SelectedValue.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		new System.Collections.Generic.List<SASalaryItem>();
		DataTable saItems = this.ptyhBll.GetSaItems(text, year, month, this.DepartmentId);
		foreach (DataRow dataRow in this.dtMonth.Rows)
		{
			string lastBookIdByUserCode = this.GetLastBookIdByUserCode(dataRow["v_yhdm"].ToString());
			if (lastBookIdByUserCode == this.ddlSaBooks.SelectedValue.Trim())
			{
				System.Collections.IEnumerator enumerator2 = saItems.Rows.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						DataRow dataRow2 = (DataRow)enumerator2.Current;
						decimal? lastCost = this.GetLastCost(dataRow["v_yhdm"].ToString(), dataRow2["Id"].ToString());
						if (lastCost.HasValue)
						{
							dataRow[dataRow2["Name"].ToString()] = lastCost;
						}
					}
					continue;
				}
				finally
				{
					System.IDisposable disposable = enumerator2 as System.IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!string.IsNullOrEmpty(lastBookIdByUserCode))
			{
				string columnName = string.Empty;
				System.Collections.Generic.List<string> items = this.saBooksItemService.GetItems(lastBookIdByUserCode);
				System.Collections.Generic.List<string> items2 = this.saBooksItemService.GetItems(text);
				foreach (string current in items)
				{
					if (items2.Contains(current))
					{
						foreach (DataRow dataRow3 in saItems.Rows)
						{
							if (dataRow3["Id"].ToString() == current)
							{
								columnName = dataRow3["Name"].ToString();
								dataRow[columnName] = this.GetLastCost(dataRow["v_yhdm"].ToString(), current);
							}
						}
					}
				}
			}
		}
		BoundField boundField = new BoundField();
		boundField.DataField = "Num";
		boundField.HeaderText = "序号";
		boundField.HeaderStyle.Width = 25;
		this.gvwSaMonth.Columns.Add(boundField);
		BoundField boundField2 = new BoundField();
		boundField2.DataField = "UserCode";
		boundField2.HeaderText = "员工编号";
		boundField2.HeaderStyle.Width = 80;
		this.gvwSaMonth.Columns.Add(boundField2);
		BoundField boundField3 = new BoundField();
		boundField3.DataField = "v_xm";
		boundField3.HeaderText = "员工姓名";
		boundField3.HeaderStyle.Width = 150;
		this.gvwSaMonth.Columns.Add(boundField3);
		BoundField boundField4 = new BoundField();
		boundField4.DataField = "BooksName";
		boundField4.HeaderText = "帐套名称";
		boundField4.HeaderStyle.Width = 150;
		this.gvwSaMonth.Columns.Add(boundField4);
		BoundField boundField5 = new BoundField();
		boundField5.DataField = "IsGenerate";
		boundField5.HeaderText = "是否生成";
		boundField5.HeaderStyle.Width = 80;
		this.gvwSaMonth.Columns.Add(boundField5);
		BoundField boundField6 = new BoundField();
		boundField6.DataField = "IsPayoffName";
		boundField6.HeaderText = "是否发放";
		boundField6.HeaderStyle.Width = 80;
		this.gvwSaMonth.Columns.Add(boundField6);
		foreach (DataRow dataRow4 in saItems.Rows)
		{
			BoundField boundField7 = new BoundField();
			boundField7.DataField = dataRow4["Name"].ToString();
			boundField7.HeaderText = dataRow4["Name"].ToString() + "⊙" + dataRow4["Id"].ToString();
			boundField7.HeaderStyle.Width = 80;
			this.gvwSaMonth.Columns.Add(boundField7);
		}
		this.gvwSaMonth.DataSource = this.dtMonth;
		this.gvwSaMonth.DataBind();
	}
	protected void gvwSaMonth_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			for (int i = 6; i < e.Row.Cells.Count; i++)
			{
				string text = e.Row.Cells[i].Text;
				string[] array = text.Split(new char[]
				{
					'⊙'
				});
				e.Row.Cells[i].Text = array[0];
				list.Add(array[1]);
			}
			if (list.Count > 0)
			{
				this.ViewState["colItemId"] = list;
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[0].Attributes.Add("align", "right");
			System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
			list2 = (System.Collections.Generic.List<string>)this.ViewState["colItemId"];
			e.Row.Attributes["id"] = this.gvwSaMonth.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Cells[4].Attributes.Add("align", "center");
			e.Row.Cells[5].Attributes.Add("align", "center");
			string text2 = e.Row.Cells[4].Text;
			string text3 = e.Row.Cells[5].Text;
			for (int j = 6; j < e.Row.Cells.Count; j++)
			{
				string text4 = e.Row.Cells[j].Text;
				TextBox textBox = new TextBox();
				HiddenField hiddenField = new HiddenField();
				if (text4 == "&nbsp;")
				{
					textBox.Text = "0.000";
					hiddenField.Value = "0.000";
				}
				else
				{
					textBox.Text = text4;
					hiddenField.Value = text4;
				}
				textBox.Width = 80;
				textBox.CssClass = "easyui-validatebox easyui-numberbox";
				textBox.Style.Add("text-align", "right");
				textBox.Attributes.Add("onblur", "computeValue(this);");
				string itemId = list2[j - 6];
				textBox.Attributes["itemId"] = itemId;
				hiddenField.ID = "hfld" + itemId;
				string booksId = this.ddlSaBooks.SelectedValue.Trim();
				SASalaryBooksItem sASalaryBooksItem = (
					from sbi in this.saBooksItemService
					where sbi.BooksId == booksId && sbi.ItemId == itemId
					select sbi).FirstOrDefault<SASalaryBooksItem>();
				if (sASalaryBooksItem != null)
				{
					if (sASalaryBooksItem.IsFormula)
					{
						textBox.Attributes.Add("disabled", "disabled");
						textBox.Attributes.Add("data-options", "min:-9999999999,max:9999999999999999,precision:3,groupSeparator:','");
					}
					else
					{
						SASalaryItem byId = this.saItemService.GetById(itemId);
						if (byId != null)
						{
							if (byId.Code == "TaxRate" || byId.Code == "Deduct")
							{
								textBox.Attributes.Add("disabled", "disabled");
								textBox.Attributes.Add("data-options", "min:-9999999999,max:9999999999999999,precision:3,groupSeparator:','");
							}
							else
							{
								if (text3 == "是")
								{
									textBox.Attributes.Add("disabled", "disabled");
								}
								if (text2 == "否")
								{
									textBox.Attributes.Add("disabled", "disabled");
								}
								textBox.Attributes.Add("data-options", "min:0,max:9999999,precision:3,groupSeparator:','");
							}
						}
					}
				}
				e.Row.Cells[j].Controls.Add(textBox);
				e.Row.Cells[j].Controls.Add(hiddenField);
			}
		}
	}
	private void BindSaBooks(DropDownList ddl)
	{
		ddl.DataSource = this.saBooks;
		ddl.DataTextField = "Name";
		ddl.DataValueField = "Id";
		ddl.DataBind();
		ddl.Items.Insert(0, new ListItem("", ""));
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		int year = System.Convert.ToInt32(this.hfldYear.Value.Trim());
		int month = System.Convert.ToInt32(this.hfldMonth.Value.Trim());
		System.Collections.IEnumerator enumerator = this.gvwSaMonth.Rows.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
				string id = gridViewRow.Attributes["id"].ToString();
				for (int i = 6; i < gridViewRow.Cells.Count; i++)
				{
					TextBox textBox = (TextBox)gridViewRow.Cells[i].Controls[0];
					if (textBox != null)
					{
						string ItemId = textBox.Attributes["itemId"];
						string id2 = "hfld" + ItemId;
						decimal cost = 0m;
						string value = ((HiddenField)gridViewRow.Cells[i].FindControl(id2)).Value;
						if (!string.IsNullOrEmpty(value.Trim()))
						{
							cost = System.Convert.ToDecimal(value.Trim());
						}
						SAMonthSalary sAMonthSalary = (
							from sms in this.saMonthService
							where sms.UserCode == id && sms.ItemId == ItemId && sms.Year == year && sms.Month == month
							select sms).FirstOrDefault<SAMonthSalary>();
						if (sAMonthSalary != null)
						{
							sAMonthSalary.ItemId = ItemId;
							sAMonthSalary.UserCode = id;
							sAMonthSalary.Cost = cost;
							this.saMonthService.Update(sAMonthSalary);
						}
					}
				}
			}
		}
		finally
		{
			System.IDisposable disposable = enumerator as System.IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		base.RegisterShow("系统提示", "保存成功!");
		this.BindGV();
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
		this.BindGV();
	}
	private void AddData()
	{
		int month = System.Convert.ToInt32(this.ddlMonth.SelectedValue);
		int year = System.Convert.ToInt32(this.ddlYear.SelectedValue);
		string saBooksId = this.ddlSaBooks.SelectedValue;
		string arg_52_0 = string.Empty;
		try
		{
			DataTable userInfoByDepartBooks = this.ptyhBll.GetUserInfoByDepartBooks(this.DepartmentId, saBooksId, year, month);
			System.Collections.Generic.List<SASalaryBooksItem> list = (
				from sbi in this.saBooksItemService
				where sbi.BooksId == saBooksId
				select sbi).ToList<SASalaryBooksItem>();
			if (list.Count == 0)
			{
				base.RegisterShow("系统提示", "生成失败，该帐套下没有编辑帐套明细！");
			}
			else
			{
				System.Collections.IEnumerator enumerator = userInfoByDepartBooks.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataRow dataRow = (DataRow)enumerator.Current;
						string userCode = dataRow["v_yhdm"].ToString();
						string arg_158_0 = userCode;
						System.Collections.Generic.List<SASalaryBooksItem> list2 = (
							from sbi in list
							where sbi.BooksId == saBooksId && !sbi.IsFormula
							select sbi).ToList<SASalaryBooksItem>();
						foreach (SASalaryBooksItem saBooksItemNotFormula in list2)
						{
							if ((
								from sm in this.saMonthService
								where sm.Month == month && sm.Year == year && sm.UserCode == userCode && sm.ItemId == saBooksItemNotFormula.ItemId
								select sm).FirstOrDefault<SAMonthSalary>() == null)
							{
								SASalaryItem byId = this.saItemService.GetById(saBooksItemNotFormula.ItemId);
								if (byId.Code == "TaxRate" || byId.Code == "Deduct")
								{
									decimal? cost = null;
									cost = this.GetLastCost(userCode, saBooksItemNotFormula.ItemId);
									if (!cost.HasValue)
									{
										cost = new decimal?(this.GetTaxrateDeduct(byId.Code, 30));
									}
									this.AddSaMonth(userCode, year, month, saBooksItemNotFormula.ItemId, cost);
								}
								else
								{
									decimal? cost2 = this.GetLastCost(userCode, saBooksItemNotFormula.ItemId);
									if (!cost2.HasValue)
									{
										cost2 = saBooksItemNotFormula.DefaultValue;
									}
									this.AddSaMonth(userCode, year, month, saBooksItemNotFormula.ItemId, cost2);
								}
							}
						}
						System.Collections.Generic.List<SASalaryBooksItem> list3 = (
							from sbi in list
							where sbi.BooksId == saBooksId && sbi.IsFormula
							select sbi).ToList<SASalaryBooksItem>();
						foreach (SASalaryBooksItem saBooksItemFormula in list3)
						{
							if ((
								from sm in this.saMonthService
								where sm.Month == month && sm.Year == year && sm.UserCode == userCode && sm.ItemId == saBooksItemFormula.ItemId
								select sm).FirstOrDefault<SAMonthSalary>() == null)
							{
								string formula = saBooksItemFormula.Formula;
								decimal? cost3 = null;
								if (!string.IsNullOrEmpty(formula))
								{
									string expression = this.RepalceFormula(formula, 30);
									try
									{
										cost3 = this.GetLastCost(userCode, saBooksItemFormula.ItemId);
										if (!cost3.HasValue)
										{
											cost3 = new decimal?(CalculatorHelper.Calc(expression));
										}
									}
									catch (System.Exception ex)
									{
										if (ex.Message == "计算错误")
										{
											cost3 = new decimal?(0m);
										}
									}
								}
								this.AddSaMonth(userCode, year, month, saBooksItemFormula.ItemId, cost3);
							}
						}
					}
				}
				finally
				{
					System.IDisposable disposable = enumerator as System.IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		catch (System.Exception)
		{
			base.RegisterShow("系统提示", "生成失败，请确认公式逻辑是否正确或者计算额度是否过大！");
		}
	}
	private void AddSaMonth(string userCode, int year, int month, string itemId, decimal? cost)
	{
		SAMonthSalary sAMonthSalary = new SAMonthSalary();
		sAMonthSalary.Id = System.Guid.NewGuid().ToString();
		sAMonthSalary.UserCode = userCode;
		sAMonthSalary.Year = year;
		sAMonthSalary.Month = month;
		sAMonthSalary.ItemId = itemId;
		sAMonthSalary.Cost = ((!cost.HasValue) ? 0m : cost.Value);
		this.saMonthService.Add(sAMonthSalary);
	}
	private string RepalceFormula(string formula, int level)
	{
		if (level < 0)
		{
			throw new System.Exception("生成失败，请确认公式逻辑是否正确或者计算额度是否过大！");
		}
		if (!string.IsNullOrEmpty(formula))
		{
			string[] array = formula.Split(new char[]
			{
				'['
			});
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (text.Contains(']'))
				{
					string item = text.Substring(0, text.IndexOf(']'));
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			foreach (string current in list)
			{
				SASalaryBooksItem byId = this.saBooksItemService.GetById(current);
				if (byId.IsFormula)
				{
					if (string.IsNullOrEmpty(byId.Formula))
					{
						formula = formula.Replace("[" + byId.Id + "]", "(0.000)");
					}
					else
					{
						formula = formula.Replace("[" + byId.Id + "]", "(" + byId.Formula.ToString() + ")");
					}
				}
				else
				{
					SASalaryItem byId2 = this.saItemService.GetById(byId.ItemId);
					if (byId2.Code == "TaxRate" || byId2.Code == "Deduct")
					{
						decimal taxrateDeduct = this.GetTaxrateDeduct(byId2.Code, level);
						formula = formula.Replace("[" + byId.Id + "]", taxrateDeduct.ToString("0.000"));
					}
					else
					{
						formula = formula.Replace("[" + byId.Id + "]", (!byId.DefaultValue.HasValue) ? "0.000" : byId.DefaultValue.Value.ToString("0.000"));
					}
				}
			}
			if (formula.Contains("["))
			{
				formula = this.RepalceFormula(formula, --level);
			}
		}
		return formula;
	}
	private decimal GetTaxrateDeduct(string saItemCode, int level)
	{
		SASalaryItem saItem = (
			from sis in this.saItemService
			where sis.Code == "Taxable"
			select sis).FirstOrDefault<SASalaryItem>();
		SASalaryBooksItem sASalaryBooksItem = (
			from sbi in this.saBooksItemService
			where sbi.BooksId == this.ddlSaBooks.SelectedValue && sbi.ItemId == saItem.Id
			select sbi).FirstOrDefault<SASalaryBooksItem>();
		decimal taxrateDeduct;
		if (sASalaryBooksItem != null)
		{
			if (sASalaryBooksItem.IsFormula)
			{
				if (!string.IsNullOrEmpty(sASalaryBooksItem.Formula))
				{
					string expression = this.RepalceFormula(sASalaryBooksItem.Formula, --level);
					decimal value = 0m;
					try
					{
						value = CalculatorHelper.Calc(expression);
					}
					catch (System.Exception ex)
					{
						if (ex.Message == "计算错误")
						{
							value = 0m;
						}
					}
					taxrateDeduct = this.GetTaxrateDeduct(value, saItemCode);
				}
				else
				{
					taxrateDeduct = this.GetTaxrateDeduct(0m, saItemCode);
				}
			}
			else
			{
				decimal value2 = (!sASalaryBooksItem.DefaultValue.HasValue) ? 0m : sASalaryBooksItem.DefaultValue.Value;
				taxrateDeduct = this.GetTaxrateDeduct(value2, saItemCode);
			}
		}
		else
		{
			taxrateDeduct = this.GetTaxrateDeduct(0m, saItemCode);
		}
		return taxrateDeduct;
	}
	private decimal GetTaxrateDeduct(decimal value, string saItemCode)
	{
		decimal result = 0m;
		if (saItemCode == "TaxRate")
		{
			result = this.saTaxService.GetTaxRate(value);
		}
		else
		{
			if (saItemCode == "Deduct")
			{
				result = this.saTaxService.GetDeduct(value);
			}
		}
		return result;
	}
	protected void btnGenerate_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.ddlSaBooks.SelectedValue.Trim()))
		{
			this.AddData();
			this.AspNetPager1.CurrentPageIndex = 0;
			this.BindGV();
			return;
		}
		base.RegisterShow("系统提示", "生成失败，请选择帐套之后生成工资！");
	}
	private string GetLastBookIdByUserCode(string userCode)
	{
		string result = string.Empty;
		SAPayoff sAPayoff = (
			from p in this.saPayoffService
			where p.IsPayoff == true && p.UserCode == userCode
			orderby p.Year descending
			orderby p.Month descending
			select p).FirstOrDefault<SAPayoff>();
		if (sAPayoff != null)
		{
			result = sAPayoff.BooksId;
		}
		return result;
	}
	private decimal? GetLastCost(string userCode, string itemId)
	{
		decimal? result = null;
		SAPayoff payOff = (
			from p in this.saPayoffService
			where p.UserCode == userCode && p.IsPayoff == true
			orderby p.Year descending
			orderby p.Month descending
			select p).FirstOrDefault<SAPayoff>();
		if (payOff != null)
		{
			SAMonthSalary sAMonthSalary = (
				from s in this.saMonthService
				where s.UserCode == userCode && s.Year == payOff.Year && s.Month == payOff.Month && s.ItemId == itemId
				select s).FirstOrDefault<SAMonthSalary>();
			if (sAMonthSalary != null)
			{
				result = new decimal?(sAMonthSalary.Cost);
			}
		}
		return result;
	}
}


