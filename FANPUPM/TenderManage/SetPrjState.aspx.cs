using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_SetPrjState : NBasePage, IRequiresSessionState
{
	private string SignUpWarnDay = ConfigurationManager.AppSettings["SignUpWarnDay"];

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.hfldUserCode.Value = base.UserCode;
			this.bindDrop();
			this.bindGv();
		}
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				string selectedValue = this.dropPrjType.SelectedValue;
				decimal sumTotalAtChgState = TenderInfo.GetSumTotalAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.GetState(2), this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
				e.Row.Cells[1].Text = "合计";
				e.Row.Cells[8].Text = sumTotalAtChgState.ToString("#0.000");
				e.Row.Cells[8].Style.Add("text-align", "right");
				e.Row.Cells[8].CssClass = "decimal_input";
			}
			return;
		}
		string text = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
		e.Row.Attributes["id"] = text;
		e.Row.Attributes["guid"] = text;
		e.Row.Attributes["PrjState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
		e.Row.Attributes["prjInfoState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjInfoState"].ToString();
		e.Row.Attributes["flowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["ChangeFlowSate"].ToString();
		PTPrjInfoStateChange byPrjIdByOrder = new PTPrjInfoStateChangeService().GetByPrjIdByOrder(text, -1);
		if (byPrjIdByOrder != null)
		{
			e.Row.Attributes["changed"] = "1";
			return;
		}
		e.Row.Attributes["changed"] = "0";
	}
	protected void bindGv()
	{
		string selectedValue = this.dropPrjType.SelectedValue;
		int countAtChgState = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.GetState(2), this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
		this.AspNetPager1.RecordCount = countAtChgState;
		DataTable allAtChgState = TenderInfo.GetAllAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.GetState(2), this.GetState(1), base.UserCode, string.Empty, true, 3, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, "ChangeFlowSate");
		this.gvDataInfo.DataSource = allAtChgState;
		this.gvDataInfo.DataBind();
		string value = string.Empty;
		if (this.dropPrjState.SelectedValue != string.Empty)
		{
			value = this.dropPrjState.SelectedValue;
		}
		if (string.IsNullOrEmpty(value))
		{
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				6
			};
			int countAtChgState2 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				4
			};
			int countAtChgState3 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				1
			};
			int countAtChgState4 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				2
			};
			int countAtChgState5 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				3
			};
			int countAtChgState6 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				14
			};
			int countAtChgState7 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				15
			};
			int countAtChgState8 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				16
			};
			int countAtChgState9 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			prjState = new System.Collections.Generic.List<int>
			{
				19
			};
			int countAtChgState10 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
			string text = "<span style='margin-left:3px;margin-right:3px;'>";
			string text2 = "</span>";
			this.lblTotal.Text = string.Concat(new object[]
			{
				"汇总：落标",
				text,
				countAtChgState2,
				text2,
				"项，投标",
				text,
				countAtChgState3,
				text2,
				"项, 信息预立项",
				text,
				countAtChgState4,
				text,
				"项,信息立项",
				text,
				countAtChgState5,
				text2,
				"项，报名通过",
				text,
				countAtChgState6,
				text2,
				"项，报名不通过",
				text,
				countAtChgState10,
				text2,
				"项, 资格预审",
				text,
				countAtChgState7,
				text2,
				"项, 预审通过",
				text,
				countAtChgState8,
				text2,
				"项，预审失败",
				text,
				countAtChgState9,
				text2,
				"项"
			});
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		System.Collections.Generic.List<int> prjState2 = new System.Collections.Generic.List<int>
		{
			System.Convert.ToInt32(value)
		};
		int countAtChgState11 = TenderInfo.GetCountAtChgState(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtBuildUnit.Text, selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState2, this.GetState(1), base.UserCode, string.Empty, 3, "ChangeFlowSate");
		int num11 = System.Convert.ToInt32(value);
		switch (num11)
		{
		case 1:
			num4 = countAtChgState11;
			goto IL_791;
		case 2:
			num5 = countAtChgState11;
			goto IL_791;
		case 3:
			num6 = countAtChgState11;
			goto IL_791;
		case 4:
			num3 = countAtChgState11;
			goto IL_791;
		case 5:
			num = countAtChgState11;
			goto IL_791;
		case 6:
			num2 = countAtChgState11;
			goto IL_791;
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
			break;
		case 14:
			num7 = countAtChgState11;
			goto IL_791;
		case 15:
			num8 = countAtChgState11;
			goto IL_791;
		default:
			if (num11 == 19)
			{
				num10 = countAtChgState11;
				goto IL_791;
			}
			break;
		}
		num9 = countAtChgState11;
		IL_791:
		string text3 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text4 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：中标",
			text3,
			num,
			text4,
			"项，落标",
			text3,
			num2,
			text4,
			"项，投标",
			text3,
			num3,
			text4,
			"项, 信息预立项",
			text3,
			num4,
			text3,
			"项,信息立项",
			text3,
			num5,
			text4,
			"项，报名通过",
			text3,
			num6,
			text4,
			"项，报名不通过",
			text3,
			num10,
			text4,
			"项, 资格预审",
			text3,
			num7,
			text4,
			"项, 预审通过",
			text3,
			num8,
			text4,
			"项，预审失败",
			text3,
			num9,
			text4,
			"项"
		});
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	private void TransplantProject()
	{
		try
		{
			new ProjectInfo();
			string arg_11_0 = this.hfldCheckedIds.Value;
		}
		catch
		{
			base.RegisterShow("系统提示", "更新失败");
		}
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void bindDrop()
	{
		TypeList.BindDrop(this.dropPrjState, false, "", null, new System.Collections.Generic.List<int>
		{
			1,
			2,
			3,
			4,
			6,
			14,
			15,
			16,
			19
		});
		TypeList.BindDrop(this.dropPrjType, "ProjectType", "", null, true);
	}
	protected System.Collections.Generic.List<int> GetState(int stateType)
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
		string text = string.Empty;
		if (stateType == 2)
		{
			if (this.dropPrjState.SelectedValue != "")
			{
				text = this.dropPrjState.SelectedValue;
			}
		}
		else
		{
			if (this.dropWFState.SelectedValue != "")
			{
				text = this.dropWFState.SelectedValue;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			list.Clear();
			int item = int.Parse(text);
			list.Add(item);
		}
		return list;
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Guid guid = new System.Guid(this.hfldCheckedIds.Value);
			string text = "089";
			string text2 = "001";
			string sqlString = "select LinkTable,KeyWord,StateField,BusinessName from WF_BusinessCode  where BusinessCode=" + text;
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			string text3 = dataTable.Rows[0]["LinkTable"].ToString();
			string text4 = dataTable.Rows[0]["KeyWord"].ToString();
			dataTable.Rows[0]["StateField"].ToString();
			string text5 = dataTable.Rows[0]["BusinessName"].ToString();
			string text6 = " Begin ";
			object obj = text6;
			text6 = string.Concat(new object[]
			{
				obj,
				" delete from WF_Instance where ID IN (SELECT ID FROM WF_Instance_Main WHERE BusinessCode=",
				text,
				" AND BusinessClass=",
				text2,
				" AND InstanceCode='",
				guid,
				"')"
			});
			object obj2 = text6;
			text6 = string.Concat(new object[]
			{
				obj2,
				" DELETE  FROM WF_Instance_Main WHERE BusinessCode=",
				text,
				" AND BusinessClass=",
				text2,
				" AND InstanceCode='",
				guid,
				"'"
			});
			string sqlString2 = "select FatherId, TableName, line, linkLine,linkTable from WF_supperDelete where BusinessCode=" + text + " and  BussinessClass=" + text2;
			DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
			if (dataTable2.Rows.Count > 0)
			{
				if (dataTable2.Rows.Count == 1)
				{
					string text7 = dataTable2.Rows[0]["TableName"].ToString();
					string text8 = dataTable2.Rows[0]["line"].ToString();
					string text9 = dataTable2.Rows[0]["linkLine"].ToString();
					string text10 = dataTable2.Rows[0]["linkTable"].ToString();
					object obj3 = text6;
					text6 = string.Concat(new object[]
					{
						obj3,
						" delete from ",
						text7,
						" where ",
						text8,
						" in (select ",
						text9,
						" from ",
						text10,
						" where ",
						text4,
						"= '",
						guid,
						"')"
					});
				}
				else
				{
					int arg_2A7_0 = dataTable2.Rows.Count;
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						string text11 = dataTable2.Rows[i]["TableName"].ToString();
						string text12 = dataTable2.Rows[i]["line"].ToString();
						string text13 = dataTable2.Rows[i]["linkLine"].ToString();
						string text14 = dataTable2.Rows[0]["linkTable"].ToString();
						string a = dataTable2.Rows[0]["Fatherid"].ToString();
						if (a == "1")
						{
							object obj4 = text6;
							text6 = string.Concat(new object[]
							{
								obj4,
								" delete from ",
								text11,
								" where ",
								text12,
								" in (select ",
								text13,
								" from ",
								text14,
								" where ",
								text4,
								"= '",
								guid,
								"')"
							});
						}
						text6 += " ";
					}
				}
			}
			object obj5 = text6;
			text6 = string.Concat(new object[]
			{
				obj5,
				" DELETE  FROM ",
				text3,
				" where ",
				text4,
				"= '",
				guid,
				"' "
			});
			text6 += " ";
			text6 += "end";
			string str = "删除失败！";
			if (publicDbOpClass.ExecuteSQL(text6) >= 1)
			{
				str = "删除成功";
			}
			myxml.SetTwoPWDlog(this.Session["yhdm"].ToString(), this.Page.Request.UserHostAddress.ToString(), text5.ToString(), guid.ToString(), "");
			base.Response.Redirect(base.Request.Url.ToString());
			this.Page.RegisterStartupScript("", "<script>alert('" + str + "');</script>");
		}
		catch
		{
			this.Page.RegisterStartupScript("", "<script>alert('删除失败！');</script>");
		}
	}
	protected string GetPrjStateName(string state)
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		if (string.IsNullOrEmpty(state))
		{
			return "";
		}
		if (state == "1")
		{
			stringBuilder.Append("信息预立项");
		}
		else
		{
			if (state == "2")
			{
				stringBuilder.Append("信息立项");
			}
			else
			{
				if (state == "3")
				{
					stringBuilder.Append("报名通过");
				}
				else
				{
					if (state == "4")
					{
						stringBuilder.Append("投标");
					}
					else
					{
						if (state == "5")
						{
							stringBuilder.Append("中标");
						}
						else
						{
							if (state == "6")
							{
								stringBuilder.Append("落标");
							}
							else
							{
								if (state == "14")
								{
									stringBuilder.Append("资格预审");
								}
								else
								{
									if (state == "15")
									{
										stringBuilder.Append("预审通过");
									}
									else
									{
										if (state == "16")
										{
											stringBuilder.Append("预审失败");
										}
										else
										{
											if (state == "18")
											{
												stringBuilder.Append("放弃");
											}
											else
											{
												if (state == "19")
												{
													stringBuilder.Append("报名不通过");
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return stringBuilder.ToString();
	}
}


