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
public partial class HR_Salary_SalaryTIList : BasePage, IRequiresSessionState
{

	private SalaryTIAction stia
	{
		get
		{
			return new SalaryTIAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.GVSalaryTempletItem.DataBind();
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add','" + base.Request["tid"].ToString() + "');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit','" + base.Request["tid"].ToString() + "');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除薪金子项吗？');";
			if (base.Request["us"].ToString() == "1")
			{
				this.btnAdd.Enabled = false;
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVSalaryTempletItem.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVSalaryTempletItem.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.stia.Delete(Convert.ToInt32(base.Request["tid"].ToString()), Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功！');";
			this.GVSalaryTempletItem.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败！');";
	}
	protected void GVSalaryTempletItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["ItemID"].ToString() + "');";
			TableCell expr_81 = e.Row.Cells[0];
			expr_81.Text = expr_81.Text + "(F" + dataRowView["ItemID"].ToString() + ")";
			CheckBox checkBox = (CheckBox)e.Row.Cells[1].FindControl("CBIsEdit");
			if (dataRowView["IsEdit"].ToString() == "1")
			{
				checkBox.Checked = true;
			}
			else
			{
				checkBox.Checked = false;
			}
			e.Row.Cells[3].Text = this.stia.StrItemName(Convert.ToInt32(dataRowView["ItemID"]));
		}
	}
}


