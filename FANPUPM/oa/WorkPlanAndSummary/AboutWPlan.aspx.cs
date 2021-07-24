using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_AboutWPlan : NBasePage, IRequiresSessionState
{
	

	protected List<string> DynamicLoad
	{
		get
		{
			return ((List<string>)this.ViewState["DynamicLoad"]) ?? new List<string>();
		}
		set
		{
			this.ViewState["DynamicLoad"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnSave.Attributes["onclick"] = "return CheckEmpty()";
			if (DateTime.Now.DayOfWeek >= DayOfWeek.Friday)
			{
				this.txtWPlanDate.Text = Common2.GetTime(DateTime.Now.AddDays((double)(5 - Convert.ToInt32(DateTime.Now.DayOfWeek))));
			}
			else
			{
				this.txtWPlanDate.Text = Common2.GetTime(DateTime.Now.AddDays((double)(-(double)(Convert.ToInt32(DateTime.Now.DayOfWeek) + 2))));
			}
			if (base.Request.QueryString["Action"] == "Add")
			{
				this.txtReportUser.Text = new MainPlanAndAction().BackUserName(base.UserCode);
				this.txtPart.Text = MainPlanAndAction.BackDeptOrID(base.UserCode)[1];
				this.txtDuty.Text = new MainPlanAndAction().BackUserName(base.UserCode);
				string text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
				string sqlString = string.Concat(new string[]
				{
					"select max(WkpUserCode) from pm_workplan_weekplan where wkpreporttype=",
					base.Request.QueryString["planType"].ToString(),
					"and wkpreportuser='",
					base.UserCode,
					"'and WkpUserCode LIKE '%",
					text,
					"%'"
				});
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					if (string.IsNullOrEmpty(dataTable.Rows[0][0].ToString()))
					{
						this.txtWPcode.Text = text + "001";
					}
					else
					{
						int num = Convert.ToInt32(dataTable.Rows[0][0].ToString().Substring(8, 3)) + 1;
						if (num < 10)
						{
							this.txtWPcode.Text = text + "00" + num.ToString();
						}
						else
						{
							if (num < 100)
							{
								this.txtWPcode.Text = text + "0" + num.ToString();
							}
							else
							{
								this.txtWPcode.Text = text + num.ToString();
							}
						}
					}
				}
				else
				{
					this.txtWPcode.Text = text + "001";
				}
			}
			if (base.Request["action"] == null)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('参数错误,本页无法正常显示')", true);
				return;
			}
			if (base.Request["action"] == "Edit")
			{
				MainPlan model = new MainPlanAndAction().GetModel(new Guid(base.Request.QueryString["wkpId"]));
				MainPlanAndAction mainPlanAndAction = new MainPlanAndAction();
				this.txtWPcode.ReadOnly = true;
				this.txtWPcode.ToolTip = "单号是不允许编辑的";
				this.txtWPcode.Text = model.WkpUserCode;
				this.txtPart.Text = mainPlanAndAction.BackDept(model.WkpDeptId);
				this.txtReportUser.Text = mainPlanAndAction.BackUserName(model.WkpReportUser);
				string sqlString2 = "select wkpremarks from pm_workplan_plansummary where wkpid='" + new Guid(base.Request.QueryString["wkpid"].ToString()) + "'";
				DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
				this.txtProduce.Text = ((dataTable2.Rows.Count > 0) ? PlanDetailAction.DeCodeStr(dataTable2.Rows[0][0].ToString()) : "");
				this.txtWPlanDate.Text = Common2.GetTime(model.WkpRecordDate);
				string sqlString3 = "select * from pm_workplan_weekplandetails where WkpId='" + base.Request.QueryString["wkpId"].ToString() + "'";
				DataTable dataTable3 = publicDbOpClass.DataTableQuary(sqlString3);
				int count = dataTable3.Rows.Count;
				int count2 = dataTable3.Columns.Count;
				this.hdnDataCount.Value = count.ToString();
				List<PlanDetail> planDemos = new PlanDetailAction().GetPlanDemos(new Guid(base.Request.QueryString["wkpId"]));
				if (count > 0)
				{
					this.txtContent.Text = PlanDetailAction.DeCodeStr(planDemos[0].WkpContents);
					this.txtDuty.Text = planDemos[0].WkpChief;
					this.txtResponsibility.Text = planDemos[0].WkpPersons;
					this.BeginDate.Text = planDemos[0].WkpStartTime.ToShortDateString();
					this.EndDate.Text = planDemos[0].WkpEndTime.ToShortDateString();
					for (int i = 1; i < count; i++)
					{
						for (int j = 0; j < count2; j++)
						{
							if (j == count2 - 1)
							{
								HtmlInputHidden expr_581 = this.hdnvalue;
								expr_581.Value = expr_581.Value + dataTable3.Rows[i][j].ToString() + "?";
							}
							else
							{
								if (j == 3 || j == 4)
								{
									HtmlInputHidden expr_5C5 = this.hdnvalue;
									expr_5C5.Value = expr_5C5.Value + DateTime.Parse(dataTable3.Rows[i][j].ToString()) + "|";
								}
								else
								{
									if (j == 2)
									{
										HtmlInputHidden expr_60B = this.hdnvalue;
										expr_60B.Value = expr_60B.Value + PlanDetailAction.DeCodeStr(dataTable3.Rows[i][j].ToString()) + "|";
									}
									else
									{
										HtmlInputHidden expr_647 = this.hdnvalue;
										expr_647.Value = expr_647.Value + dataTable3.Rows[i][j].ToString() + "|";
									}
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
		bool flag = false;
		if (base.Request.QueryString["Action"] == "Add")
		{
			string sqlString = string.Concat(new string[]
			{
				"select 1 from pm_workplan_weekplan where wkpreporttype=",
				base.Request.QueryString["planType"].ToString(),
				" and WkpUserCode='",
				this.txtWPcode.Text,
				"'and wkpreportuser='",
				base.UserCode,
				"'"
			});
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				base.RegisterScript("top.ui.alert('该单号已经被使用!请更换填写!')");
				return;
			}
			MainPlan mainPlan = new MainPlan();
			mainPlan.WkpId = Guid.NewGuid();
			mainPlan.WkpDeptId = ((this.thisDeptId.Value == "") ? MainPlanAndAction.BackDeptOrID(base.UserCode)[0] : this.thisDeptId.Value);
			mainPlan.WkpIsReport = 0;
			mainPlan.WkpIstrue = 0;
			mainPlan.WkpRecordDate = Convert.ToDateTime(this.txtWPlanDate.Text.Trim());
			mainPlan.WkpRegisterUser = base.UserCode;
			mainPlan.WkpReportType = int.Parse(base.Request.QueryString["planType"]);
			mainPlan.WkpReportUser = ((this.hdnReportUser.Value == "") ? base.UserCode : this.hdnReportUser.Value);
			mainPlan.WkpUserCode = this.txtWPcode.Text.Trim();
			mainPlan.WkpIstrue = -1;
			mainPlan.InputTime = DateTime.Now;
			bool flag2 = new MainPlanAndAction().InsertIntoBase(mainPlan);
			string text = "insert into pm_workplan_plansummary (wkppsid,wkpid,wkpremarks,wkpsummary,wkpselfscore,wkpsummarydate)";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" values ('",
				Guid.NewGuid(),
				"','",
				mainPlan.WkpId,
				"','",
				PlanDetailAction.EncodeStr(this.txtProduce.Text.Trim()),
				"','',NULL,'",
				DateTime.Now.Date,
				"')"
			});
			publicDbOpClass.ExecSqlString(text);
			PlanDetail planDetail = new PlanDetail();
			if (this.txtDuty.Text.Trim().Length > 10)
			{
				planDetail.WkpChief = this.txtDuty.Text.Trim().Substring(0, 10);
			}
			else
			{
				planDetail.WkpChief = this.txtDuty.Text.Trim();
			}
			planDetail.WkpContents = PlanDetailAction.EncodeStr(this.txtContent.Text);
			planDetail.WkpDetailsId = Guid.NewGuid();
			planDetail.WkpEndTime = DateTime.Parse(this.EndDate.Text + " 0:00:00");
			planDetail.WkpId = mainPlan.WkpId;
			if (this.txtResponsibility.Text.Trim().Length > 10)
			{
				planDetail.WkpPersons = this.txtResponsibility.Text.Trim().Substring(0, 10);
			}
			else
			{
				planDetail.WkpPersons = this.txtResponsibility.Text.Trim();
			}
			planDetail.WkpStartTime = DateTime.Parse(this.BeginDate.Text + " 0:00:00");
			bool flag3 = new PlanDetailAction().AddModelIntoBaseData(planDetail);
			string value = this.hdnNums.Value;
			if (value != "")
			{
				string[] array = value.Split(new char[]
				{
					'?'
				});
				int num = array.Length;
				for (int i = 0; i < num - 1; i++)
				{
					string str = array[i].Substring(2).ToString();
					PlanDetail planDetail2 = new PlanDetail();
					planDetail2.WkpDetailsId = Guid.NewGuid();
					planDetail2.WkpEndTime = DateTime.Parse(base.Request.Form["endedDate" + str] + " 0:00:00");
					planDetail2.WkpContents = PlanDetailAction.EncodeStr(base.Request.Form["txtConten" + str]);
					planDetail2.WkpId = mainPlan.WkpId;
					if (base.Request.Form["txtPerson" + str].Length > 10)
					{
						planDetail2.WkpPersons = base.Request.Form["txtPerson" + str].Substring(0, 10);
					}
					else
					{
						planDetail2.WkpPersons = base.Request.Form["txtPerson" + str];
					}
					planDetail2.WkpStartTime = DateTime.Parse(base.Request.Form["beginDate" + str] + " 0:00:00");
					if (base.Request.Form["txtCheify" + str].Length > 10)
					{
						planDetail2.WkpChief = base.Request.Form["txtCheify" + str].Substring(0, 10);
					}
					else
					{
						planDetail2.WkpChief = base.Request.Form["txtCheify" + str];
					}
					flag = new PlanDetailAction().AddModelIntoBaseData(planDetail2);
				}
				if (flag2 && flag3 && flag)
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
				}
			}
			else
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
			}
		}
		if (base.Request.QueryString["Action"] == "Edit")
		{
			string sqlString2 = string.Concat(new string[]
			{
				"update pm_workplan_plansummary set wkpremarks='",
				PlanDetailAction.EncodeStr(this.txtProduce.Text.Trim()),
				"' where wkpid='",
				base.Request.QueryString["wkpid"],
				"'"
			});
			publicDbOpClass.NonQuerySqlString(sqlString2);
			MainPlan model = new MainPlanAndAction().GetModel(new Guid(base.Request.QueryString["wkpId"]));
			model.WkpDeptId = ((this.thisDeptId.Value == "") ? new MainPlanAndAction().BackDeptAndID(this.txtReportUser.Text)[0] : this.thisDeptId.Value);
			model.WkpReportType = int.Parse(base.Request.QueryString["planType"]);
			model.WkpReportUser = ((this.hdnReportUser.Value == "") ? new MainPlanAndAction().BackUserCode(this.txtReportUser.Text) : this.hdnReportUser.Value);
			model.WkpRecordDate = Convert.ToDateTime(this.txtWPlanDate.Text.Trim());
			model.InputTime = DateTime.Now;
			string text2 = "begin ";
			string text3 = "begin ";
			text2 += MainPlanAndAction.GetUpdateStr(model.WkpId.ToString(), model);
			string sqlString3 = "select 1 from pm_workplan_weekplandetails where WkpId='" + base.Request.QueryString["wkpid"].ToString() + "'";
			int count = publicDbOpClass.DataTableQuary(sqlString3).Rows.Count;
			if (count > 0)
			{
				List<PlanDetail> planDemos = new PlanDetailAction().GetPlanDemos(new Guid(base.Request.QueryString["wkpId"]));
				if (this.txtDuty.Text.Trim().Length > 10)
				{
					planDemos[0].WkpChief = this.txtDuty.Text.Trim().Substring(0, 10);
				}
				else
				{
					planDemos[0].WkpChief = this.txtDuty.Text.Trim();
				}
				planDemos[0].WkpContents = PlanDetailAction.EncodeStr(this.txtContent.Text.Trim());
				planDemos[0].WkpEndTime = DateTime.Parse(this.EndDate.Text + " 0:00:00");
				if (this.txtResponsibility.Text.Trim().Length > 10)
				{
					planDemos[0].WkpPersons = this.txtResponsibility.Text.Trim().Substring(0, 10);
				}
				else
				{
					planDemos[0].WkpPersons = this.txtResponsibility.Text.Trim();
				}
				planDemos[0].WkpStartTime = DateTime.Parse(this.BeginDate.Text.Trim() + " 0:00:00");
				text2 = text2 + " " + PlanDetailAction.GetUpdateStr(planDemos[0], planDemos[0].WkpDetailsId);
				string text4 = this.hdnNums.Value;
				if (text4 != "")
				{
					string value2 = this.hdnDelPlanId.Value;
					if (value2 != "")
					{
						string[] array2 = value2.Split(new char[]
						{
							'?'
						});
						for (int j = 0; j < array2.Length - 1; j++)
						{
							text3 = text3 + "delete from Pm_WorkPlan_WeekPlanDetails where WkpDetailsId='" + array2[j] + "' ";
						}
						text3 += " end";
						string value3 = this.hdnDelTableId.Value;
						string[] array3 = value3.Split(new char[]
						{
							','
						});
						for (int k = 0; k < array3.Length - 1; k++)
						{
							text4 = text4.Replace(array3[k], "");
						}
					}
					else
					{
						text3 = " ";
					}
					if (text4 != "")
					{
						string[] array4 = text4.Split(new char[]
						{
							'?'
						});
						int num2 = array4.Length;
						for (int l = 1; l < count + 1 - value2.Split(new char[]
						{
							'?'
						}).Length; l++)
						{
							string str2 = array4[l - 1].Substring(2).ToString();
							planDemos[l].WkpEndTime = DateTime.Parse(base.Request.Form["endedDate" + str2] + " 0:00:00");
							planDemos[l].WkpContents = PlanDetailAction.EncodeStr(base.Request.Form["txtConten" + str2]);
							if (base.Request.Form["txtPerson" + str2].Length > 10)
							{
								planDemos[l].WkpPersons = base.Request.Form["txtPerson" + str2].Substring(0, 10);
							}
							else
							{
								planDemos[l].WkpPersons = base.Request.Form["txtPerson" + str2];
							}
							planDemos[l].WkpStartTime = DateTime.Parse(base.Request.Form["beginDate" + str2].ToString() + " 0:00:00");
							if (base.Request.Form["txtCheify" + str2].Length > 10)
							{
								planDemos[l].WkpChief = base.Request.Form["txtCheify" + str2].Substring(0, 10);
							}
							else
							{
								planDemos[l].WkpChief = base.Request.Form["txtCheify" + str2];
							}
							text2 = text2 + " " + PlanDetailAction.GetUpdateStr(planDemos[l], planDemos[l].WkpDetailsId);
						}
						if (num2 - 1 < count)
						{
							text2 += " end";
						}
						else
						{
							for (int m = count; m <= num2 - 1; m++)
							{
								string str3 = array4[m - 1].Substring(2).ToString();
								PlanDetail planDetail3 = new PlanDetail();
								planDetail3.WkpDetailsId = Guid.NewGuid();
								planDetail3.WkpEndTime = DateTime.Parse(base.Request.Form["endedDate" + str3] + " 0:00:00");
								planDetail3.WkpContents = PlanDetailAction.EncodeStr(base.Request.Form["txtConten" + str3]);
								planDetail3.WkpId = model.WkpId;
								if (base.Request.Form["txtPerson" + str3].Length > 10)
								{
									planDetail3.WkpPersons = base.Request.Form["txtPerson" + str3].Substring(0, 10);
								}
								else
								{
									planDetail3.WkpPersons = base.Request.Form["txtPerson" + str3];
								}
								planDetail3.WkpStartTime = Convert.ToDateTime(base.Request.Form["beginDate" + str3]);
								if (base.Request.Form["txtChiefy" + str3] != null && base.Request.Form["txtChiefy" + str3].Length > 10)
								{
									planDetail3.WkpChief = base.Request.Form["txtChiefy" + str3].Substring(0, 10);
								}
								else
								{
									planDetail3.WkpChief = ((base.Request.Form["txtChiefy" + str3] == null) ? new MainPlanAndAction().BackUserName(base.UserCode) : base.Request.Form["txtChiefy" + str3]);
								}
								text2 = text2 + " " + PlanDetailAction.GetInsertStr(planDetail3);
							}
							text2 += " end";
						}
					}
					else
					{
						text2 += " end";
					}
				}
				else
				{
					text3 = " ";
					text2 += " end";
				}
			}
			else
			{
				PlanDetail planDetail4 = new PlanDetail();
				if (this.txtResponsibility.Text.Trim().Length > 10)
				{
					planDetail4.WkpChief = this.txtResponsibility.Text.Trim().Substring(0, 10);
				}
				else
				{
					planDetail4.WkpChief = this.txtResponsibility.Text.Trim();
				}
				planDetail4.WkpContents = PlanDetailAction.EncodeStr(this.txtContent.Text.Trim());
				planDetail4.WkpDetailsId = Guid.NewGuid();
				planDetail4.WkpEndTime = DateTime.Parse(this.EndDate.Text + " 0:00:00");
				planDetail4.WkpId = model.WkpId;
				if (this.txtDuty.Text.Trim().Length > 10)
				{
					planDetail4.WkpPersons = this.txtDuty.Text.Trim().Substring(0, 10);
				}
				else
				{
					planDetail4.WkpPersons = this.txtDuty.Text.Trim();
				}
				planDetail4.WkpStartTime = DateTime.Parse(this.BeginDate.Text + " 0:00:00");
				bool flag3 = new PlanDetailAction().AddModelIntoBaseData(planDetail4);
				string value4 = this.hdnNums.Value;
				if (value4 != "")
				{
					string[] array5 = value4.Split(new char[]
					{
						'?'
					});
					int num3 = array5.Length;
					for (int n = 0; n < num3 - 1; n++)
					{
						string str4 = array5[n].Substring(2).ToString();
						PlanDetail planDetail5 = new PlanDetail();
						planDetail5.WkpDetailsId = Guid.NewGuid();
						planDetail5.WkpEndTime = DateTime.Parse(base.Request.Form["endedDate" + str4] + " 0:00:00");
						planDetail5.WkpContents = PlanDetailAction.EncodeStr(base.Request.Form["txtConten" + str4]);
						planDetail5.WkpId = model.WkpId;
						if (base.Request.Form["txtPerson" + str4].Length > 10)
						{
							planDetail5.WkpPersons = base.Request.Form["txtPerson" + str4].Substring(0, 10);
						}
						else
						{
							planDetail5.WkpPersons = base.Request.Form["txtPerson" + str4];
						}
						planDetail5.WkpStartTime = DateTime.Parse(base.Request.Form["beginDate" + str4] + " 0:00:00");
						if (base.Request.Form["txtCheify" + str4].Length > 10)
						{
							planDetail5.WkpChief = base.Request.Form["txtCheify" + str4].Substring(0, 10);
						}
						else
						{
							planDetail5.WkpChief = base.Request.Form["txtCheify" + str4];
						}
						flag = new PlanDetailAction().AddModelIntoBaseData(planDetail5);
					}
					if (flag3 && flag)
					{
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
					}
				}
				text2 = " ";
				text3 = " ";
			}
			if (PlanDetailAction.ExecuteResult(text2) && PlanDetailAction.ExecuteResult(text3))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_WorkPlanMange'})");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败!')");
		}
	}
	public string GetCurrentName()
	{
		return new MainPlanAndAction().BackUserName(base.UserCode);
	}
	public string GetCheckUserCodes(string text)
	{
		if (text != "")
		{
			string[] array = text.Split(new char[]
			{
				','
			});
			int num = array.Length - 1;
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 = text2 + new MainPlanAndAction().BackUserCode(array[i]) + ",";
			}
			return text2;
		}
		return "";
	}
}


