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
public partial class oa_WorkManage_SubCompanyManage_CompanyDrawStat : BasePage, IRequiresSessionState
{
	public OAOfficeResApplicationCollectAction amAction
	{
		get
		{
			return new OAOfficeResApplicationCollectAction();
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)this.ViewState["STRWHERE"];
			}
			return "";
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.strWhere = "ApplyType='D'";
			this.Bind(this.strWhere);
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除吗?')) return false;";
		this.btnStartWF.Attributes["onclick"] = "javascript:if(!confirm('确定提交审核吗?')) return false;";
		this.btnViewWF.Attributes["onclick"] = "OpenViewWF();";
		this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
		this.BtnView.Attributes["onclick"] = "OpenLock();";
	}
	private void Bind(string strWhere)
	{
		this.GVBook.DataSource = this.amAction.GetList(strWhere);
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
				dataRowView["ACRecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = dataRowView["iYear"].ToString() + "年";
			e.Row.Cells[1].Text = dataRowView["iMonth"].ToString() + "月";
			e.Row.Cells[2].Text = BooksCommonClass.GetDepartmentName(dataRowView["CorpCode"].ToString());
			switch (int.Parse(dataRowView["AuditState"].ToString()))
			{
			case -2:
				e.Row.Cells[4].Text = "打回到发起人";
				break;
			case -1:
				e.Row.Cells[4].Text = "未启动流程";
				return;
			case 0:
				e.Row.Cells[4].Text = "流程流转中";
				return;
			case 1:
				e.Row.Cells[4].Text = "审核通过";
				return;
			default:
				return;
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind(this.strWhere);
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind(this.strWhere);
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind(this.strWhere);
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.amAction.Delete(new Guid(this.HdnACRecordID.Value)) > 0)
		{
			this.Bind(this.strWhere);
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.HdnACRecordID.Value);
		string text = FlowAuditAction.BeginFlow("014", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.JS.Text = "alert('" + text + "!')";
			this.Bind(this.strWhere);
			return;
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			this.Bind(this.strWhere);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.strWhere = " ApplyType='D' and iYear=" + ((this.txtYear.Text.Trim() == "") ? DateTime.Now.Year.ToString() : this.txtYear.Text.Trim());
		this.Bind(this.strWhere);
	}
}


