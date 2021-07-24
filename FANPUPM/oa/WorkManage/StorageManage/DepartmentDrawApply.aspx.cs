using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_DepartmentDrawApply : BasePage, IRequiresSessionState
{
	private string strWhere = "";

	public OAOfficeResDepartmentApplicationAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
		this.btnStartWF.Attributes["onclick"] = "javascript:if(!confirm('确定提交审核吗?')) return false;";
		this.btnTransact.Attributes["onclick"] = "javascript:if(!confirm('确定提交办理吗?')) return false;";
		this.btnViewWF.Attributes["onclick"] = "OpenViewWF();";
		this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
		this.BtnView.Attributes["onclick"] = "OpenLock();";
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList(" UserCode = '" + base.UserCode + "'");
		this.GVBook.DataBind();
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
				dataRowView["DARecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"','",
				dataRowView["IsSubmit"].ToString(),
				"','",
				dataRowView["IsAbove"].ToString(),
				"','",
				dataRowView["ApplyDepartment"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetUserName(dataRowView["ApplyMan"].ToString());
			string a;
			if (dataRowView["IsAbove"].ToString() == "0" && (a = dataRowView["IsSubmit"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						e.Row.Cells[3].Text = "已提交";
					}
				}
				else
				{
					e.Row.Cells[3].Text = "未提交";
				}
			}
			if (dataRowView["IsAbove"].ToString() == "1")
			{
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
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.amAction.Delete(new Guid(this.HdnInStorageID.Value)) > 0)
		{
			this.HdnInStorageID.Value = Guid.NewGuid().ToString();
			this.Bind();
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.HdnInStorageID.Value);
		string text = FlowAuditAction.BeginFlow("012", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.JS.Text = "alert('" + text + "!')";
			this.Bind();
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			this.Bind();
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
	protected void btnTransact_Click(object sender, EventArgs e)
	{
		if (this.amAction.UpdateTransact(new Guid(this.HdnInStorageID.Value)) > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('提交办理成功!');</script>");
			this.HdnInStorageID.Value = Guid.NewGuid().ToString();
			this.Bind();
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('提交办理失败!');</script>");
	}
}


