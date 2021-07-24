using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_CheckPlan : NBasePage, IRequiresSessionState
{
	

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SetHdnvalue();
			this.btnSave.Attributes["onclick"] = "if(!confirm('确定保存该计划的审核结果?')) return false;";
			this.Data_Bind();
		}
	}
	protected void GvShowPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GvShowPlan.PageIndex = e.NewPageIndex;
		this.Data_Bind();
	}
	protected void GvShowPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Cells[1].Style.Add("word-break", " break-all");
			e.Row.Cells[1].Style.Add("word-wrap", "break-word");
			e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text;
			if (e.Row.Cells[1].Text.Length > 28)
			{
				e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 27) + "...";
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string sqlString = "select * from pm_workplan_checkinfo where Wkpid='" + base.Request.QueryString["wkpid"].ToString() + "' order by wkpcheckdate desc";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_3C_0 = dataTable.Rows.Count;
		if (dataTable.Rows.Count < 1)
		{
			string text = string.Concat(new object[]
			{
				"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
				base.Request.QueryString["wkpid"].ToString(),
				"','",
				base.UserCode,
				"','",
				DateTime.Now,
				"',",
				this.rbtnState.SelectedValue,
				")"
			});
			if (this.rbtnState.SelectedValue == "-1")
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" update pm_workplan_weekplan set wkpistrue=",
					this.rbtnState.SelectedValue,
					",wkpisreport=0 where wkpid='",
					base.Request.QueryString["wkpid"].ToString(),
					"' end"
				});
			}
			else
			{
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					" update pm_workplan_weekplan set wkpistrue=",
					this.rbtnState.SelectedValue,
					",wkpisreport=1 where wkpid='",
					base.Request.QueryString["wkpid"].ToString(),
					"' end"
				});
			}
			bool flag = publicDbOpClass.NonQuerySqlString(text);
			if (flag)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');winclose('CheckPlan','SummPlanView.aspx',true)", true);
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
				string text4 = string.Concat(new object[]
				{
					"begin update pm_workplan_checkinfo set wkpcheckdate='",
					DateTime.Now,
					"',wkpcheckresult=",
					this.rbtnState.SelectedValue,
					" where wkpid='",
					base.Request.QueryString["wkpid"],
					"' and wkpcheckercode='",
					base.UserCode,
					"'"
				});
				if (this.rbtnState.SelectedValue == "-1")
				{
					string text5 = text4;
					text4 = string.Concat(new string[]
					{
						text5,
						" update pm_workplan_weekplan set wkpistrue=",
						this.rbtnState.SelectedValue,
						",wkpisreport=0 where wkpid='",
						base.Request.QueryString["wkpid"].ToString(),
						"' end"
					});
				}
				else
				{
					string text6 = text4;
					text4 = string.Concat(new string[]
					{
						text6,
						" update pm_workplan_weekplan set wkpistrue=",
						this.rbtnState.SelectedValue,
						",wkpisreport=1 where wkpid='",
						base.Request.QueryString["wkpid"].ToString(),
						"' end"
					});
				}
				bool flag2 = publicDbOpClass.NonQuerySqlString(text4);
				if (flag2)
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');winclose('CheckPlan','SummPlanView.aspx',true)", true);
					return;
				}
			}
			else
			{
				DataRow[] array = dataTable.Select("wkpcheckercode='" + base.UserCode + "'");
				if (array.Length > 0)
				{
					string text7 = string.Concat(new object[]
					{
						"begin update pm_workplan_checkinfo set wkpcheckdate='",
						DateTime.Now,
						"',wkpcheckresult=",
						this.rbtnState.SelectedValue,
						" where wkpid='",
						base.Request.QueryString["wkpid"],
						"' and wkpcheckercode='",
						base.UserCode,
						"'"
					});
					if (this.rbtnState.SelectedValue == "-1")
					{
						string text8 = text7;
						text7 = string.Concat(new string[]
						{
							text8,
							" update pm_workplan_weekplan set wkpistrue=",
							this.rbtnState.SelectedValue,
							",wkpisreport=0 where wkpid='",
							base.Request.QueryString["wkpid"].ToString(),
							"' end"
						});
					}
					else
					{
						string text9 = text7;
						text7 = string.Concat(new string[]
						{
							text9,
							" update pm_workplan_weekplan set wkpistrue=",
							this.rbtnState.SelectedValue,
							",wkpisreport=1 where wkpid='",
							base.Request.QueryString["wkpid"].ToString(),
							"' end"
						});
					}
					bool flag2 = publicDbOpClass.NonQuerySqlString(text7);
					if (flag2)
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');winclose('CheckPlan','SummPlanView.aspx',true)", true);
						return;
					}
				}
				else
				{
					string text10 = string.Concat(new object[]
					{
						"begin insert into pm_workplan_checkinfo (wkpid,wkpcheckercode,wkpcheckdate,wkpcheckresult)values('",
						base.Request.QueryString["wkpid"].ToString(),
						"','",
						base.UserCode,
						"','",
						DateTime.Now,
						"',",
						this.rbtnState.SelectedValue,
						")"
					});
					if (this.rbtnState.SelectedValue == "-1")
					{
						string text11 = text10;
						text10 = string.Concat(new string[]
						{
							text11,
							" update pm_workplan_weekplan set wkpistrue=",
							this.rbtnState.SelectedValue,
							",wkpisreport=0 where wkpid='",
							base.Request.QueryString["wkpid"].ToString(),
							"' end"
						});
					}
					else
					{
						string text12 = text10;
						text10 = string.Concat(new string[]
						{
							text12,
							" update pm_workplan_weekplan set wkpistrue=",
							this.rbtnState.SelectedValue,
							",wkpisreport=1 where wkpid='",
							base.Request.QueryString["wkpid"].ToString(),
							"' end"
						});
					}
					bool flag3 = publicDbOpClass.NonQuerySqlString(text10);
					if (flag3)
					{
						this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('审核成功!');winclose('CheckPlan','SummPlanView',true)", true);
					}
				}
			}
		}
	}
	protected void Data_Bind()
	{
		string sqlString = "select * from pm_workplan_weekplandetails where wkpid='" + new Guid(base.Request.QueryString["wkpid"]) + "'";
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GvShowPlan.DataSource = dataSource;
		this.GvShowPlan.DataBind();
	}
	public void SetHdnvalue()
	{
		string sqlString = "select * from pm_workplan_checkinfo where Wkpid='" + base.Request.QueryString["wkpid"].ToString() + "' and wkpcheckresult=-1";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			this.hdnname.Value = dataTable.Rows[0]["wkpcheckercode"].ToString();
			return;
		}
		this.hdnname.Value = "";
	}
}


