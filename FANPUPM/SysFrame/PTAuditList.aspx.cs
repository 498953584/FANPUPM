using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame_PTAuditList : BasePage, IRequiresSessionState
{

	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.gvAuditingList.DataBind();
	}
	protected void gvAuditingList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string text = dataRowView["InstanceCode"].ToString();
			string text2 = dataRowView["NoteID"].ToString();
			string text3 = dataRowView["IsAllPass"].ToString();
			string text4 = dataRowView["NodeID"].ToString();
			string text5 = dataRowView["BusinessCode"].ToString();
			string text6 = dataRowView["BusinessClass"].ToString();
			Convert.ToInt32(dataRowView["During"]);
			decimal d = 0m;
			try
			{
				d = Convert.ToDecimal(dataRowView["cs"]);
			}
			catch
			{
			}
			LinkButton linkButton = (LinkButton)e.Row.Cells[1].Controls[0];
			if (dataRowView["BusinessCode"].ToString() == "999")
			{
				OAWFApplyItem model = this.hrAction.GetModel((Guid)dataRowView["InstanceCode"]);
				if (model != null)
				{
					if (model.Title.Length > 20)
					{
						linkButton.Text = model.Title.Substring(0, 19) + "..&nbsp;";
						linkButton.Attributes["title"] = model.Title;
					}
					else
					{
						linkButton.Text = model.Title + "&nbsp;";
						linkButton.Attributes["title"] = model.Title;
					}
				}
			}
			linkButton.Attributes["class"] = "firstpage";
			if (d > 0m)
			{
				string str = "超时";
				linkButton.Text = "[<font color=\"red\">" + str + "</font>]" + linkButton.Text;
			}
			string str2 = string.Concat(new string[]
			{
				"../EPC/Workflow/WorkflowAuditFrame.aspx?ic=",
				text,
				"&id=",
				text2,
				"&pass=",
				text3,
				"&nid=",
				text4,
				"&bc=",
				text5,
				"&bcl=",
				text6
			});
			linkButton.Attributes["onclick"] = "javascript:if(window.showModalDialog('" + str2 + "', window, 'dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;')){window.window.location.reload(true)}else{return false;};";
		}
	}
}


