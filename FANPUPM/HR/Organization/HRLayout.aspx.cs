using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_HRLayout : BasePage, IRequiresSessionState
{

	public HROrgManpowerPlanAction hrAction
	{
		get
		{
			return new HROrgManpowerPlanAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
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
		this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定删除该项人力资源规划吗?')) return false;";
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
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			LinkButton linkButton = new LinkButton();
			linkButton.Text = this.Display_File(dataRowView["RecordID"].ToString());
			string str = "../../../commonWindow/Annex/AnnexList.aspx?mid=28&rc=" + dataRowView["RecordID"].ToString() + "&at=0&type=1";
			linkButton.Attributes["onclick"] = "javascript:window.showModalDialog('" + str + "', window, 'dialogHeight:300px;dialogWidth:500px;center:1;help:0;status:0;');return false;";
			e.Row.Cells[3].Controls.Add(linkButton);
		}
	}
	private string Display_File(string RecordID)
	{
		string text = "";
		ArrayList annexList = this._AnnexAction.GetAnnexList(RecordID, 0, 28);
		if (annexList.Count > 0)
		{
			for (int i = 0; i < annexList.Count; i++)
			{
				text = text + ((AnnexInfo)annexList[i]).OriginalName + ",";
			}
		}
		return text.Trim(new char[]
		{
			','
		});
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
		string text = FlowAuditAction.BeginFlow("008", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.GVBook.DataBind();
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "err", "alert('" + text + "!');", true);
		}
		if (text == "请先设置当前模块的审核流程")
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){window.location.href=window.location.href};", true);
		}
		this.GVBook.DataBind();
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
		DataTable corpDepartment = this.hrAction.GetCorpDepartment(text, isAll);
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
		this.GVBook.DataBind();
	}
}


