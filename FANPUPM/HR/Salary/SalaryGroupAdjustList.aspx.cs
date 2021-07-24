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
public partial class HR_Salary_SalaryGroupAdjustList : BasePage, IRequiresSessionState
{
	private SalaryGAAction sga
	{
		get
		{
			return new SalaryGAAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "return openEdit('Add');";
			this.btnEdit.Attributes["onclick"] = "return openEdit('Edit');";
			this.btnDel.Attributes["onclick"] = " return confirm('确定工资调整申请数据删除吗？');";
			this.BtnView.Attributes["onclick"] = " OpenLock();return false;";
			this.btnViewWF.Attributes["onclick"] = "OpenViewWF('25','001')";
			this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF('25','001');";
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVGroupAdjust.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVGroupAdjust.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sga.Delete(new Guid(this.HdnRecoreId.Value)) == 1)
		{
			this.JS.Text = "alert('删除成功!');";
			this.GVGroupAdjust.DataBind();
			return;
		}
		this.JS.Text = "alert('删除失败!');";
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		this.Page.SmartNavigation = false;
		string text = FlowAuditAction.BeginFlow("25", "001", new Guid(this.HdnRecoreId.Value), "", base.UserCode);
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
	protected void GVGroupAdjust_RowDataBound(object sender, GridViewRowEventArgs e)
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
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[0].Text = userManageDb.GetUserName(dataRowView["UserCode"].ToString());
			switch (int.Parse(dataRowView["UserCode"].ToString()))
			{
			case -2:
				e.Row.Cells[2].Text = "打回到发起人";
				break;
			case -1:
				e.Row.Cells[2].Text = "未启动流程";
				return;
			case 0:
				e.Row.Cells[2].Text = "流程流转中";
				return;
			case 1:
				e.Row.Cells[2].Text = "审核通过";
				return;
			default:
				return;
			}
		}
	}
}


