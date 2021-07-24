using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryItemList : BasePage, IRequiresSessionState
{

	private SalaryItemAction sia
	{
		get
		{
			return new SalaryItemAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除薪酬科目吗？');";
		}
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVSalaryItem.DataBind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVSalaryItem.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sia.Delete(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
		}
		else
		{
			this.JS.Text = "alert('删除失败!');";
		}
		this.GVSalaryItem.DataBind();
	}
	protected void GVSalaryItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["ItemID"].ToString() + "');";
			if (dataRowView["ItemType"].ToString() == "0")
			{
				e.Row.Cells[2].Text = "标准工资项";
			}
			else
			{
				e.Row.Cells[2].Text = "可维护项";
			}
			if (dataRowView["IsValid"].ToString() == "0")
			{
				e.Row.Cells[3].Text = "无效";
				return;
			}
			e.Row.Cells[3].Text = "有效";
		}
	}
}


