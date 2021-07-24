using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryParameterMain : BasePage, IRequiresSessionState
{
	private SalaryITAction sit
	{
		get
		{
			return new SalaryITAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除个人所得税设置吗？');";
			this.btnSave.Attributes["onclick"] = " return confirm('确定修改个税起征点吗？');";
			string sqlString = "select isnull(TaxLine,0) as TaxLine from HR_Salary_IncomeTaxLine ";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				this.txtTaxLine.Text = Convert.ToDecimal(dataTable.Rows[0]["TaxLine"]).ToString("f2");
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVSalaryIncomeTax.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVSalaryIncomeTax.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sit.Delete(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void GVSalaryIncomeTax_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["TaxRateID"].ToString() + "');";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.txtTaxLine.Text.Trim().Length > 0)
		{
			string sqlString = " update HR_Salary_IncomeTaxLine set TaxLine = '" + this.txtTaxLine.Text + "'";
			if (publicDbOpClass.NonQuerySqlString(sqlString))
			{
				this.JS.Text = "alert('个税起征点保存成功！');";
				return;
			}
			this.JS.Text = "alert('个税起征点保存成功！');";
		}
	}
}


