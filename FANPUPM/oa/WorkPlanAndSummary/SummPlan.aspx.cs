using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_SummPlan : NBasePage, IRequiresSessionState
{
	

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			MainPlan model = new MainPlanAndAction().GetModel(new Guid(base.Request.QueryString["wkpId"]));
			MainPlanAndAction mainPlanAndAction = new MainPlanAndAction();
			this.txtWPcode.ReadOnly = true;
			this.txtWPcode.ToolTip = "单号是不允许编辑的";
			this.txtWPcode.Text = model.WkpUserCode;
			this.txtPart.Text = mainPlanAndAction.BackDept(model.WkpDeptId);
			this.txtReportUser.Text = mainPlanAndAction.BackUserName(model.WkpReportUser);
			string[] array = model.WkpCheckerUser.Split(new char[]
			{
				','
			});
			string str = "";
			for (int i = 0; i < array.Length - 1; i++)
			{
				str = str + new MainPlanAndAction().BackUserName(array[i]) + ",";
			}
			this.DateInTime.Text = model.WkpRecordDate.ToShortDateString();
			string sqlString = "select wkpremarks from pm_workplan_plansummary where wkpid='" + new Guid(base.Request.QueryString["wkpid"].ToString()) + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			this.txtRemarks.Text = ((dataTable.Rows.Count > 0) ? dataTable.Rows[0][0].ToString() : "");
			if (base.Request.QueryString["Action"].ToString() == "edit")
			{
				string sqlString2 = "select * from pm_workplan_plansummary where wkpid='" + base.Request.QueryString["wkpid"].ToString() + "'";
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
				this.txtRemarks.Text = PlanDetailAction.DeCodeStr(dataTable2.Rows[0]["wkpremarks"].ToString());
				this.txtScore.Text = dataTable2.Rows[0]["wkpselfscore"].ToString();
				this.txtSummPro.Text = dataTable2.Rows[0]["wkpsummary"].ToString();
			}
			string sqlString3 = "select * from pm_workplan_weekplandetails where WkpId='" + base.Request.QueryString["wkpId"].ToString() + "'";
			DataTable dataTable3 = publicDbOpClass.DataTableQuary(sqlString3);
			int count = dataTable3.Rows.Count;
			int count2 = dataTable3.Columns.Count;
			this.hdnDataCount.Value = count.ToString();
			new PlanDetailAction().GetPlanDemos(new Guid(base.Request.QueryString["wkpId"]));
			if (count > 0)
			{
				string sqlString4 = "select wkpsmcontents,wkppercent,wkpdetailsid from pm_workplan_weeksummary where wkpid='" + new Guid(base.Request.QueryString["wkpid"].ToString()) + "'";
				DataTable dataTable4 = publicDbOpClass.DataTableQuary(sqlString4);
				int count3 = dataTable4.Rows.Count;
				int count4 = dataTable4.Columns.Count;
				for (int j = 0; j < count3; j++)
				{
					for (int k = 0; k < count4; k++)
					{
						if (k == count4 - 1)
						{
							HtmlInputHidden expr_34D = this.hdnTextSum;
							expr_34D.Value = expr_34D.Value + dataTable4.Rows[j][k].ToString() + "?";
						}
						else
						{
							HtmlInputHidden expr_384 = this.hdnTextSum;
							expr_384.Value = expr_384.Value + dataTable4.Rows[j][k].ToString() + "|";
						}
					}
				}
				for (int l = 0; l < count; l++)
				{
					for (int m = 0; m < count2; m++)
					{
						if (m == count2 - 1)
						{
							HtmlInputHidden expr_3EC = this.hdnvalue;
							expr_3EC.Value = expr_3EC.Value + dataTable3.Rows[l][m].ToString() + "?";
						}
						else
						{
							if (m == 3 || m == 4)
							{
								HtmlInputHidden expr_430 = this.hdnvalue;
								expr_430.Value = expr_430.Value + DateTime.Parse(dataTable3.Rows[l][m].ToString()) + "|";
							}
							else
							{
								if (m == 2)
								{
									HtmlInputHidden expr_476 = this.hdnvalue;
									expr_476.Value = expr_476.Value + PlanDetailAction.DeCodeStr(dataTable3.Rows[l][m].ToString()) + "|";
								}
								else
								{
									HtmlInputHidden expr_4B2 = this.hdnvalue;
									expr_4B2.Value = expr_4B2.Value + dataTable3.Rows[l][m].ToString() + "|";
								}
							}
						}
					}
				}
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (base.Request.QueryString["Action"] == "write")
		{
			string sqlString = "select IsValid from pm_workplan_plansummary where wkpid='" + base.Request.QueryString["wkpid"].ToString() + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows[0][0].ToString() == "true")
			{
				base.RegisterScript("top.ui.alert('对不起,该计划已有相应总结,一个计划只能对应一个总结.')");
				return;
			}
			string value = this.hdnNums.Value;
			int num = value.Split(new char[]
			{
				'?'
			}).Length - 1;
			string text = "begin ";
			SummModel summModel = new SummModel();
			for (int i = 0; i < num; i++)
			{
				summModel.WkpDetailsId = new Guid(base.Request.Form["txtNormer" + i.ToString()]);
				summModel.WkpPercent = base.Request.Form["txtpecent" + i.ToString()];
				summModel.WkpSmContents = base.Request.Form["txtsummer" + i.ToString()];
				summModel.WkpRecordDate = DateTime.Now.Date;
				summModel.WkpId = new Guid(base.Request.QueryString["wkpid"].ToString());
				text = text + SummModelAcion.getInertStr(summModel) + " ";
			}
			decimal num2 = 0m;
			if (!string.IsNullOrEmpty(this.txtScore.Text.Trim()))
			{
				num2 = Convert.ToDecimal(this.txtScore.Text.Trim());
			}
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"update pm_workplan_plansummary set WkpSummary='",
				this.txtSummPro.Text.Trim(),
				"',WkpSelfScore='",
				num2,
				"',WkpSummaryDate='",
				DateTime.Now.Date,
				"',IsValid=1 where wkpid='",
				new Guid(base.Request.QueryString["wkpid"]),
				"' end"
			});
			if (new MainPlanAndAction().ExecuteResult(text))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('保存失败!');", true);
			}
		}
		if (base.Request.QueryString["Action"] == "edit")
		{
			string value2 = this.hdnNums.Value;
			int num3 = value2.Split(new char[]
			{
				'?'
			}).Length - 1;
			string text2 = "begin ";
			SummModel summModel2 = new SummModel();
			for (int j = 0; j < num3; j++)
			{
				string g = base.Request.Form["txtNormer" + j.ToString()];
				summModel2.WkpPercent = base.Request.Form["txtpecent" + j.ToString()];
				summModel2.WkpSmContents = base.Request.Form["txtsummer" + j.ToString()];
				summModel2.WkpRecordDate = DateTime.Now.Date;
				summModel2.WkpId = new Guid(base.Request.QueryString["wkpid"].ToString());
				string sqlString2 = "select 1 from pm_workplan_weeksummary where wkpdetailsid='" + new Guid(g) + "'";
				if (publicDbOpClass.DataTableQuary(sqlString2).Rows.Count > 0)
				{
					text2 = text2 + SummModelAcion.getUpdateStr(summModel2, new Guid(g)) + " ";
				}
				else
				{
					summModel2.WkpDetailsId = new Guid(g);
					text2 += SummModelAcion.getInertStr(summModel2);
				}
			}
			decimal num4 = 0m;
			if (!string.IsNullOrEmpty(this.txtScore.Text.Trim()))
			{
				num4 = Convert.ToDecimal(this.txtScore.Text.Trim());
			}
			object obj2 = text2;
			text2 = string.Concat(new object[]
			{
				obj2,
				"update pm_workplan_plansummary set WkpSummary='",
				this.txtSummPro.Text,
				"',WkpSelfScore='",
				num4,
				"',WkpSummaryDate='",
				DateTime.Now.Date,
				"' where wkpid='",
				base.Request.QueryString["wkpid"].ToString(),
				"' end"
			});
			if (new MainPlanAndAction().ExecuteResult(text2))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
				this.hdnNums.Value = "";
				return;
			}
			base.RegisterScript("top.ui.show(保存失败!)");
			this.hdnNums.Value = "";
		}
	}
}


