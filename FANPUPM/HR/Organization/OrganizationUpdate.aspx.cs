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
public partial class HR_Organization_OrganizationUpdate : BasePage, IRequiresSessionState
{
	public HROrgAdjustAction hrAction
	{
		get
		{
			return new HROrgAdjustAction();
		}
	}
	public HROrgManpowerPlanAction hrmpAction
	{
		get
		{
			return new HROrgManpowerPlanAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.Tree_Bind();
		}
		this.btnAdd.Attributes["onclick"] = "javascript:if(!OpenWin('add')) return false;";
		this.btnEdit.Attributes["onclick"] = "javascript:if(!OpenWin('upd')) return false;";
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项吗?')) return false;";
		this.btnStartWF.Attributes["onclick"] = "javascript:if(!confirm('确定提交审核吗?')) return false;";
		this.btnViewWF.Attributes["onclick"] = "OpenViewWF();";
		this.btnWFPrint.Attributes["onclick"] = "OpenPrintWF();";
		this.BtnView.Attributes["onclick"] = "OpenLock();";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"');"
			});
			e.Row.Cells[1].Text = BooksCommonClass.GetDepartmentName(dataRowView["CorpCode"].ToString());
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
		if (this.hrAction.Delete(new Guid(this.HdnRecordID.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.HdnRecordID.Value);
		string text = FlowAuditAction.BeginFlow("009", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.GVBook.DataBind();
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "err", "alert('" + text + "!');", true);
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			return;
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
	}
	private void Tree_Bind()
	{
		bool isAll = false;
		string text = this.Session["CorpCode"].ToString().Trim();
		if (text == "00")
		{
			isAll = true;
		}
		this.TVDept.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "华夏幸福基业";
		treeNode.Value = "";
		this.TVDept.Nodes.Add(treeNode);
		DataTable corpDepartment = this.hrmpAction.GetCorpDepartment(text, isAll);
		if (corpDepartment.Rows.Count > 0)
		{
			foreach (DataRow dataRow in corpDepartment.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["CorpName"].ToString();
				treeNode2.Value = dataRow["CorpCode"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
			}
		}
	}
	protected void TVDept_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.btnAdd.Enabled = true;
		this.LblMsg.Text = this.TVDept.SelectedNode.Text;
		this.HdnDeptCode.Value = this.TVDept.SelectedValue;
		this.GVBook.DataBind();
	}
}


