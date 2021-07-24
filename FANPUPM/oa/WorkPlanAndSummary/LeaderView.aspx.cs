using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_LeaderView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AllData_bind();
		}
	}
	protected void GvShowPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GvShowPlan.PageIndex = e.NewPageIndex;
		this.AllData_bind();
	}
	protected void GvShowPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string sqlString = string.Concat(new string[]
			{
				"select * from pm_workplan_checkinfo where wkpid='",
				dataRowView["WkpId"].ToString(),
				"' and wkpcheckercode='",
				base.UserCode,
				"'"
			});
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			string text;
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["WkpCheckResult"].ToString();
			}
			else
			{
				text = "-1";
			}
			e.Row.Attributes["onmouseover"] = "currentColor=this.style.backgroundColor;this.style.backgroundColor='#EEF2F5';this.style.cursor='hand';";
			e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentColor;");
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText='background-color:#EAF4FD';window.oldtr=this;rowClick('",
				text,
				"','",
				dataRowView["WkpId"].ToString(),
				"')"
			});
			string text2 = dataRowView["WkpIsTrue"].ToString();
			string a;
			if ((a = text2) != null)
			{
				if (!(a == "1"))
				{
					if (!(a == "0"))
					{
						if (a == "-1")
						{
							e.Row.Cells[5].Text = "驳回修改";
						}
					}
					else
					{
						e.Row.Cells[5].Text = "未审核";
					}
				}
				else
				{
					e.Row.Cells[5].Text = "合格";
				}
			}
			e.Row.Cells[6].Text = DateTime.Parse(dataRowView["WkpRecordDate"].ToString()).ToShortDateString();
		}
	}
	protected void AllData_bind()
	{
		string sqlString = "select * from pm_workplan_weekplan where wkpisreport=1";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (!dataTable.Rows[i]["WkpCheckerUser"].ToString().Contains(base.UserCode + ","))
			{
				dataTable.Rows.Remove(dataTable.Rows[i]);
			}
		}
		this.GvShowPlan.DataSource = dataTable;
		this.GvShowPlan.DataBind();
	}
}


