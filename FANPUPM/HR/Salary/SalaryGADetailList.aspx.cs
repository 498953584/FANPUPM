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
public partial class HR_Salary_SalaryGADetailList : BasePage, IRequiresSessionState
{

	private SalaryGADAction sgad
	{
		get
		{
			return new SalaryGADAction();
		}
	}
	private Guid rid
	{
		get
		{
			return (Guid)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["rid"] != null)
		{
			this.rid = new Guid(base.Request["rid"].ToString());
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add','" + this.rid + "');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit','" + this.rid + "');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除吗？');";
			SqlDataSource expr_CE = this.SqGroupAdjustDetail;
			object selectCommand = expr_CE.SelectCommand;
			expr_CE.SelectCommand = string.Concat(new object[]
			{
				selectCommand,
				" WHERE [RecordID] = '",
				this.rid,
				"' "
			});
			this.GVGroupAdjustDetail.DataBind();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		SqlDataSource expr_06 = this.SqGroupAdjustDetail;
		object selectCommand = expr_06.SelectCommand;
		expr_06.SelectCommand = string.Concat(new object[]
		{
			selectCommand,
			" WHERE [RecordID] = '",
			this.rid,
			"' "
		});
		this.GVGroupAdjustDetail.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		SqlDataSource expr_06 = this.SqGroupAdjustDetail;
		object selectCommand = expr_06.SelectCommand;
		expr_06.SelectCommand = string.Concat(new object[]
		{
			selectCommand,
			" WHERE [RecordID] = '",
			this.rid,
			"' "
		});
		this.GVGroupAdjustDetail.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sgad.Delete(Convert.ToInt32(this.HdnDetailID.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
			SqlDataSource expr_34 = this.SqGroupAdjustDetail;
			object selectCommand = expr_34.SelectCommand;
			expr_34.SelectCommand = string.Concat(new object[]
			{
				selectCommand,
				" WHERE [RecordID] = '",
				this.rid,
				"' "
			});
			this.GVGroupAdjustDetail.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void GVGroupAdjustDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["DetailID"].ToString() + "');";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[0].Text = userManageDb.GetUserName(dataRowView["EmployeeCode"].ToString());
		}
	}
}


