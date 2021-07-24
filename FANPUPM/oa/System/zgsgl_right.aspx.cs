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
public partial class oa_System_zgsgl_right : BasePage, IRequiresSessionState
{
	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["cid"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		if (base.Request["cid"].ToString() != "")
		{
			this.Label1.Value = base.Request["cid"].ToString();
		}
		if (!base.IsPostBack)
		{
			this.btnAdd.Attributes["onclick"] = "JavaScript:return showEditWin('add');";
			this.btnEdit.Attributes["onclick"] = "JavaScript:return showEditWin('edit');";
			this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除该制度吗?')){return false;}else{return true;}";
			this.SystemInfoBind();
		}
	}
	protected void SystemInfoBind()
	{
		string strWhere;
		if (base.Request["cid"] != "")
		{
			strWhere = "a.SystemType=2 and a.ClassID=" + base.Request["cid"].ToString();
		}
		else
		{
			strWhere = "SystemType=2";
		}
		DataTable usreNameList = this.sia.GetUsreNameList(strWhere);
		this.DataGrid1.DataSource = usreNameList;
		this.DataGrid1.DataBind();
	}
	protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
	{
		if (e.Item.ItemIndex != -1)
		{
			LinkButton linkButton = (LinkButton)e.Item.Cells[1].Controls[0];
			e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Item.Attributes["onclick"] = string.Concat(new string[]
			{
				"getRecordID('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"'); OnRecord(this);"
			});
			int num = Convert.ToInt32(dataRowView["AuditState"].ToString());
			if (num == -1)
			{
				e.Item.Cells[4].Text = "未提交";
			}
			else
			{
				if (num == 0)
				{
					e.Item.Cells[4].Text = "审核中";
				}
				else
				{
					if (num == 1)
					{
						e.Item.Cells[4].Text = "已审核";
					}
					else
					{
						if (num == -2)
						{
							e.Item.Cells[4].Text = "已驳回";
						}
					}
				}
			}
			linkButton.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);getRecordID('",
				((DataRowView)e.Item.DataItem)["RecordID"].ToString(),
				"','",
				dataRowView["AuditState"].ToString(),
				"');javascript:showEditWin('see')"
			});
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.sia.Delete(new Guid(this.hfRecord.Value)) > 0)
		{
			this.JS.Text = "alert('删除成功!');";
			JavaScriptControl expr_34 = this.JS;
			expr_34.Text = expr_34.Text + "window.location.href='" + base.Request.Url.ToString() + "';";
			return;
		}
		this.JS.Text = "alert('该制度已被使用，目前不能删除!');";
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		Guid recordID = new Guid(this.hfRecord.Value);
		string text = FlowAuditAction.BeginFlow("006", "001", recordID, "", base.UserCode);
		if (text == "工作流程已成功启动")
		{
			this.JS.Text = "alert('" + text + "!');";
		}
		else
		{
			if (text == "请先设置当前模块的审核流程")
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('" + text + "!');", true);
			}
			else
			{
				this.JS.Text = "if(window.showModalDialog('" + text + "',window,'dialogHeight:180px;dialogWidth:450px;center:1;help:0;status:0;')=='1'){}; ";
			}
		}
		this.SystemInfoBind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.SystemInfoBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.SystemInfoBind();
	}
	protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
	{
		this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
		this.SystemInfoBind();
	}
}


