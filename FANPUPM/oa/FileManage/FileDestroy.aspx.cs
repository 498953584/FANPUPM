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
public partial class oa_FileManage_FileDestroy : BasePage, IRequiresSessionState
{

	public OAFileDestroyMainAction mcAction
	{
		get
		{
			return new OAFileDestroyMainAction();
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["lc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.LibraryCode = base.Request["lc"].ToString();
		if (!this.Page.IsPostBack && base.Request["isFirst"] == "true")
		{
			this.btnAdd.Enabled = false;
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add','" + this.LibraryCode + "')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd','" + this.LibraryCode + "')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该条记录吗?')) return false;";
		this.btnStartWF.Attributes["onclick"] = "javascript:if(!confirm('确定是否提交审核申请吗?')) return false;";
		this.btnReportLoss.Attributes["onclick"] = "javascript:if(!confirm('是否确认档案销毁吗?')) return false;";
		this.btnViewWF.Attributes["onclick"] = "OpenViewWF();";
		this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
		this.BtnView.Attributes["onclick"] = "OpenLock();";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
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
			e.Row.Cells[1].Text = BooksCommonClass.GetUserName(dataRowView["UserCode"].ToString());
			if (dataRowView["IsConfirm"].ToString() == "1")
			{
				e.Row.Cells[3].Text = "报损确认";
				return;
			}
			switch (int.Parse(dataRowView["AuditState"].ToString()))
			{
			case -2:
				e.Row.Cells[3].Text = "打回到发起人";
				break;
			case -1:
				e.Row.Cells[3].Text = "未启动流程";
				return;
			case 0:
				e.Row.Cells[3].Text = "流程流转中";
				return;
			case 1:
				e.Row.Cells[3].Text = "审核通过";
				return;
			default:
				return;
			}
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.mcAction.Delete(new Guid(this.HdnRecordCode.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.HdnRecordCode.Value);
		string text = FlowAuditAction.BeginFlow("007", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.JS.Text = "alert('" + text + "!')";
			this.GVBook.DataBind();
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			this.GVBook.DataBind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
	protected void btnReportLoss_Click(object sender, EventArgs e)
	{
		if (this.mcAction.UpdateIsConfirm(new Guid(this.HdnRecordCode.Value)) > 0)
		{
			this.GVBook.DataBind();
			this.JS.Text = "alert('档案销毁申请记录成功!')";
			return;
		}
		this.JS.Text = "alert('档案销毁申请记录失败!')";
	}
}


