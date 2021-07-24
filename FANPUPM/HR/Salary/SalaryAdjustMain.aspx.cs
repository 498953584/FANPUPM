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
public partial class HR_Salary_SalaryAdjustMain : BasePage, IRequiresSessionState
{
	private SalarySAAction sia
	{
		get
		{
			return new SalarySAAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定删除吗？');";
			this.BtnView.Attributes["onclick"] = " OpenLock();return false;";
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF('021','001')";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF('021','001');";
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVSalaryAdjust.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVSalaryAdjust.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sia.Delete(new Guid(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
			this.GVSalaryAdjust.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void GVSalaryAdjust_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				dataRowView["IsConfirm"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[1].Text = userManageDb.GetUserName(dataRowView["EmployeeCode"].ToString());
			switch (int.Parse(dataRowView["AuditState"].ToString()))
			{
			case -2:
				e.Row.Cells[7].Text = "打回到发起人";
				break;
			case -1:
				e.Row.Cells[7].Text = "未启动流程";
				return;
			case 0:
				e.Row.Cells[7].Text = "流程流转中";
				return;
			case 1:
				e.Row.Cells[7].Text = "审核通过";
				return;
			default:
				return;
			}
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		this.Page.SmartNavigation = false;
		string text = FlowAuditAction.BeginFlow("021", "001", new Guid(this.HdnRecoreId.Value), "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
	protected void BtnIsConfirm_Click(object sender, EventArgs e)
	{
	}
}


