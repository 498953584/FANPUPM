using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_WorkPlanMange : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(base.Request["planType"]))
			{
				string a = base.Request["planType"].ToString();
				if (a == "0")
				{
					this.WF1.BusiCode = "130";
				}
				else
				{
					this.WF1.BusiCode = "131";
				}
			}
			this.btnDel.Attributes.Add("onclick", "if(!confirm('确定删除该计划吗?')) return false;");
			this.AllData_bind();
		}
	}
	protected void GvShowPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DateTime date = DateTime.Now.Date;
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			TimeSpan timeSpan = date - DateTime.Parse(dataRowView["WkpRecordDate"].ToString());
			if (timeSpan.ToString().IndexOf(".") > 0)
			{
				string value = timeSpan.ToString().Substring(0, timeSpan.ToString().IndexOf("."));
				this.hdnTimeSpan.Value = value;
			}
			else
			{
				this.hdnTimeSpan.Value = "0";
			}
			if (dataRowView["WkpIsTrue"].ToString() == "1")
			{
				string sqlString = "select IsValid from pm_workplan_plansummary where wkpid='" + dataRowView["WkpId"] + "'";
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				string a = dataTable.Rows[0][0].ToString();
				string text = string.Empty;
				if (a == "False")
				{
					text = "0";
				}
				else
				{
					text = "1";
				}
				e.Row.Attributes.Add("onclick", string.Concat(new string[]
				{
					"if(window.oldtr!=null){window.oldtr.style.cssText='';}this.style.cssText='background-color:#EAF4FD';window.oldtr=this;rowClick('",
					dataRowView["WkpId"].ToString(),
					"','",
					dataRowView["WkpIsTrue"].ToString(),
					"','",
					text,
					"');"
				}));
			}
			else
			{
				e.Row.Attributes.Add("onclick", string.Concat(new string[]
				{
					"if(window.oldtr!=null){window.oldtr.style.cssText='';}this.style.cssText='background-color:#EAF4FD';window.oldtr=this;rowClick('",
					dataRowView["WkpId"].ToString(),
					"','",
					dataRowView["WkpIsTrue"].ToString(),
					"','-1');"
				}));
			}
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				string value2 = this.GvShowPlan.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["id"] = value2;
				e.Row.Attributes["guid"] = value2;
			}
			HyperLink hyperLink = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
			string str = "&planType=" + dataRowView["WkpReportType"].ToString();
			string text2 = "/oa/WorkPlanAndSummary/ExplainPlan_1.aspx?Action=View&wkpid=";
			text2 += this.GvShowPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			text2 = text2 + "&ic=" + this.GvShowPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			text2 += str;
			text2 = "javascript:toolbox_oncommand('" + text2 + "','计划与总结查看');";
			hyperLink.NavigateUrl = text2;
			hyperLink.ForeColor = Color.Blue;
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		string text = "begin";
		object obj = text;
		text = string.Concat(new object[]
		{
			obj,
			" delete from Pm_WorkPlan_WeekPlanDetails where WkpId='",
			new Guid(this.hdnwkpId.Value),
			"'"
		});
		object obj2 = text;
		text = string.Concat(new object[]
		{
			obj2,
			" delete from Pm_workplan_plansummary where wkpid='",
			new Guid(this.hdnwkpId.Value),
			"'"
		});
		object obj3 = text;
		text = string.Concat(new object[]
		{
			obj3,
			" delete from Pm_WorkPlan_WeekPlan where WkpId='",
			new Guid(this.hdnwkpId.Value),
			"'"
		});
		text += " end";
		if (publicDbOpClass.ExecSqlString(text) > 0)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('删除成功!')", true);
			this.AllData_bind();
		}
	}
	protected void GvShowPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GvShowPlan.PageIndex = e.NewPageIndex;
		this.AllData_bind();
	}
	protected void AllData_bind()
	{
		string sqlString = string.Concat(new string[]
		{
			"select * from pm_workplan_weekplan where wkpreporttype=",
			base.Request.QueryString["planType"].ToString(),
			" and wkpreportuser='",
			base.UserCode,
			"' order by InputTime desc"
		});
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GvShowPlan.DataSource = dataSource;
		this.GvShowPlan.DataBind();
	}
	protected string BackDept(string WkpDeptId)
	{
		string sqlString = "select v_bmmc from pt_d_bm where i_bmdm='" + WkpDeptId + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			return dataTable.Rows[0]["v_bmmc"].ToString();
		}
		return "";
	}
	protected string BackUserName(string usercode)
	{
		string sqlString = "select v_xm from pt_yhmc where v_yhdm='" + usercode + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			return dataTable.Rows[0]["v_xm"].ToString();
		}
		return "";
	}
	public string ReNames(string WkpCheckerUser)
	{
		string[] array = WkpCheckerUser.Split(new char[]
		{
			','
		});
		string text = "";
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (array[i] != "")
			{
				text = text + new MainPlanAndAction().BackUserName(array[i]) + " ";
			}
		}
		return text;
	}
}


