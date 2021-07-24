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
public partial class HR_Salary_SalaryTempletMain : BasePage, IRequiresSessionState
{
	private SalaryTAction sta
	{
		get
		{
			return new SalaryTAction();
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
			this.btnUse.Attributes["onclick"] = "return confirm('启动后将无法进行薪金项目修改？');";
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVSalaryTemplet.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVSalaryTemplet.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sta.Delete(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
			this.GVSalaryTemplet.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void GVSalaryTemplet_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["TempletID"].ToString(),
				"','",
				dataRowView["UseState"].ToString(),
				"');"
			});
			e.Row.Cells[2].Text = this.sta.strClassName(dataRowView["ClassCode"].ToString());
		}
	}
	protected void btnUse_Click(object sender, EventArgs e)
	{
		if (this.sta.ExecTempletTable(Convert.ToInt32(this.HdnRecoreId.Value)) > 0)
		{
			this.JS.Text = "alert('当前模板以经启用！');";
			return;
		}
		if (this.sta.TempletUse(Convert.ToInt32(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('启用成功！');";
			this.GVSalaryTemplet.DataBind();
			return;
		}
		if (this.sta.TempletUse(Convert.ToInt32(this.HdnRecoreId.Value)) == 2)
		{
			this.JS.Text = "alert('启用失败！请先设置工资模板子项后启用');";
			return;
		}
		this.JS.Text = "alert('启用失败')";
	}
}


