using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_SummPlanView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnPass.Attributes["onclick"] = "if(!confirm('确定通过该计划?')) return false;";
			this.btnBack.Attributes["onclick"] = "if(!confirm('确定驳回该计划?')) return false;";
			if (!string.IsNullOrEmpty(base.Request.QueryString["audit"]))
			{
				this.AllData_bind();
			}
		}
	}
	protected void AllData_bind()
	{
		string text = "select WeekPlan.* from pm_workplan_weekplan WeekPlan LEFT JOIN PT_yhmc yhmc ON WeekPlan.WkpReportUser=yhmc.v_yhdm where 1=1 ";
		if (base.Request.QueryString["audit"] == "no")
		{
			text += " and WkpIsTrue=1 ";
			if (!string.IsNullOrEmpty(this.ddlPlanType.SelectedValue.Trim()))
			{
				text = text + " and WkpReportType=" + this.ddlPlanType.SelectedValue.Trim();
			}
			if (!string.IsNullOrEmpty(this.txtWkpRecordDate.Text.Trim()))
			{
				text = text + " and WkpRecordDate= '" + this.txtWkpRecordDate.Text.Trim() + " 0:00:00' ";
			}
			if (!string.IsNullOrEmpty(this.txtWkpReportUser.Text.Trim()))
			{
				text = text + " and v_xm LIKE '%" + this.txtWkpReportUser.Text.Trim() + "%' ";
			}
			this.tr_audit.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.LblHeader.Text = "计划查看";
		}
		else
		{
			if (base.Request.QueryString["audit"] == "yes")
			{
				if (!string.IsNullOrEmpty(this.ddlPlanType.SelectedValue.Trim()))
				{
					text = text + " and WkpReportType=" + this.ddlPlanType.SelectedValue.Trim();
				}
				if (!string.IsNullOrEmpty(this.txtWkpRecordDate.Text.Trim()))
				{
					text = text + " and WkpRecordDate= '" + this.txtWkpRecordDate.Text.Trim() + " 0:00:00' ";
				}
				if (!string.IsNullOrEmpty(this.txtWkpReportUser.Text.Trim()))
				{
					text = text + " and v_xm LIKE '%" + this.txtWkpReportUser.Text.Trim() + "%' ";
				}
				text += " and (WkpIsTrue=0 or WkpIsTrue=-1) ";
				this.LblHeader.Text = "计划审核";
			}
		}
		text += " order by wkprecorddate desc ";
		DataTable dataSource = publicDbOpClass.DataTableQuary(text);
		this.GvShowPlan.DataSource = dataSource;
		this.GvShowPlan.DataBind();
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
			string value = this.GvShowPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			HyperLink hyperLink = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
			string str = "&planType=" + dataRowView["WkpReportType"].ToString();
			string text = "/oa/WorkPlanAndSummary/ExplainPlan_1.aspx?Action=View&wkpid=";
			text += this.GvShowPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			text = text + "&ic=" + this.GvShowPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			text += str;
			text = "javascript:toolbox_oncommand('" + text + "','查看与审核')";
			hyperLink.NavigateUrl = text;
			hyperLink.ForeColor = Color.Blue;
		}
	}
	protected void btnPass_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hdnWkpId.Value))
		{
			string sqlString = "select * from pm_workplan_checkinfo where Wkpid='" + this.hdnWkpId.Value + "' order by wkpcheckdate desc";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			int arg_42_0 = dataTable.Rows.Count;
			if (dataTable.Rows.Count < 1)
			{
				string text = string.Concat(new object[]
				{
					"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
					this.hdnWkpId.Value,
					"','",
					base.UserCode,
					"','",
					DateTime.Now,
					"',1)"
				});
				text = text + " update pm_workplan_weekplan set wkpistrue=1,wkpisreport=1 where wkpid='" + this.hdnWkpId.Value + "' end";
				bool flag = publicDbOpClass.NonQuerySqlString(text);
				if (flag)
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
					this.AllData_bind();
					return;
				}
			}
			else
			{
				if (dataTable.Rows[0]["wkpcheckresult"].ToString() == "-1")
				{
					if (!(dataTable.Rows[0]["wkpcheckercode"].ToString() == base.UserCode))
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('对不起,该计划已被'+document.getElementById('hdnname').value+'审核为驳回修改,您暂无审核权限!')", true);
						return;
					}
					string text2 = string.Concat(new object[]
					{
						"begin update pm_workplan_checkinfo set wkpcheckdate='",
						DateTime.Now,
						"',wkpcheckresult=1 where wkpid='",
						this.hdnWkpId.Value,
						"' and wkpcheckercode='",
						base.UserCode,
						"'"
					});
					text2 = text2 + " update pm_workplan_weekplan set wkpistrue=1,wkpisreport=1 where wkpid='" + this.hdnWkpId.Value + "' end";
					bool flag2 = publicDbOpClass.NonQuerySqlString(text2);
					if (flag2)
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
						this.AllData_bind();
						return;
					}
				}
				else
				{
					DataRow[] array = dataTable.Select("wkpcheckercode='" + base.UserCode + "'");
					if (array.Length > 0)
					{
						string text3 = string.Concat(new object[]
						{
							"begin update pm_workplan_checkinfo set wkpcheckdate='",
							DateTime.Now,
							"',wkpcheckresult=1 where wkpid='",
							this.hdnWkpId.Value,
							"' and wkpcheckercode='",
							base.UserCode,
							"'"
						});
						text3 = text3 + " update pm_workplan_weekplan set wkpistrue=1,wkpisreport=1 where wkpid='" + this.hdnWkpId.Value + "' end";
						bool flag2 = publicDbOpClass.NonQuerySqlString(text3);
						if (flag2)
						{
							this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
							this.AllData_bind();
							return;
						}
					}
					else
					{
						string text4 = string.Concat(new object[]
						{
							"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
							this.hdnWkpId.Value,
							"','",
							base.UserCode,
							"','",
							DateTime.Now,
							"',1)"
						});
						text4 = text4 + " update pm_workplan_weekplan set wkpistrue=1,wkpisreport=1 where wkpid='" + this.hdnWkpId.Value + "' end";
						bool flag3 = publicDbOpClass.NonQuerySqlString(text4);
						if (flag3)
						{
							this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
							this.AllData_bind();
							return;
						}
					}
				}
			}
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('您并没有选中任何计划!');", true);
		}
	}
	protected void btnSelect_Click(object sender, EventArgs e)
	{
		this.AllData_bind();
	}
	protected void btnBack_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hdnWkpId.Value))
		{
			string sqlString = "select * from pm_workplan_checkinfo where Wkpid='" + this.hdnWkpId.Value + "' order by wkpcheckdate desc";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			int arg_42_0 = dataTable.Rows.Count;
			if (dataTable.Rows.Count < 1)
			{
				string text = string.Concat(new object[]
				{
					"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
					this.hdnWkpId.Value,
					"','",
					base.UserCode,
					"','",
					DateTime.Now,
					"',0)"
				});
				text = text + " update pm_workplan_weekplan set wkpistrue=-2,wkpisreport=0 where wkpid='" + this.hdnWkpId.Value + "' end";
				bool flag = publicDbOpClass.NonQuerySqlString(text);
				if (flag)
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
					this.AllData_bind();
					return;
				}
			}
			else
			{
				if (dataTable.Rows[0]["wkpcheckresult"].ToString() == "-1")
				{
					if (!(dataTable.Rows[0]["wkpcheckercode"].ToString() == base.UserCode))
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('对不起,该计划已被'+document.getElementById('hdnname').value+'审核为驳回修改,您暂无审核权限!')", true);
						return;
					}
					string text2 = string.Concat(new object[]
					{
						"begin update pm_workplan_checkinfo set wkpcheckdate='",
						DateTime.Now,
						"',wkpcheckresult=0 where wkpid='",
						this.hdnWkpId.Value,
						"' and wkpcheckercode='",
						base.UserCode,
						"'"
					});
					text2 = text2 + " update pm_workplan_weekplan set wkpistrue=-2,wkpisreport=0 where wkpid='" + this.hdnWkpId.Value + "' end";
					bool flag2 = publicDbOpClass.NonQuerySqlString(text2);
					if (flag2)
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
						this.AllData_bind();
						return;
					}
				}
				else
				{
					DataRow[] array = dataTable.Select("wkpcheckercode='" + base.UserCode + "'");
					if (array.Length > 0)
					{
						string text3 = string.Concat(new object[]
						{
							"begin update pm_workplan_checkinfo set wkpcheckdate='",
							DateTime.Now,
							"',wkpcheckresult=0 where wkpid='",
							this.hdnWkpId.Value,
							"' and wkpcheckercode='",
							base.UserCode,
							"'"
						});
						text3 = text3 + " update pm_workplan_weekplan set wkpistrue=-2,wkpisreport=0 where wkpid='" + this.hdnWkpId.Value + "' end";
						bool flag2 = publicDbOpClass.NonQuerySqlString(text3);
						if (flag2)
						{
							this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
							this.AllData_bind();
							return;
						}
					}
					else
					{
						string text4 = string.Concat(new object[]
						{
							"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
							this.hdnWkpId.Value,
							"','",
							base.UserCode,
							"','",
							DateTime.Now,
							"',0)"
						});
						text4 = text4 + " update pm_workplan_weekplan set wkpistrue=-2,wkpisreport=0 where wkpid='" + this.hdnWkpId.Value + "' end";
						bool flag3 = publicDbOpClass.NonQuerySqlString(text4);
						if (flag3)
						{
							this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');", true);
							this.AllData_bind();
							return;
						}
					}
				}
			}
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('您并没有选中任何计划!');", true);
		}
	}
	public void SetHdnvalue()
	{
		string sqlString = "select * from pm_workplan_checkinfo where Wkpid='" + this.hdnWkpId.Value + "' and wkpcheckresult=0";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			this.hdnname.Value = dataTable.Rows[0]["wkpcheckercode"].ToString();
			return;
		}
		this.hdnname.Value = "";
	}
}


