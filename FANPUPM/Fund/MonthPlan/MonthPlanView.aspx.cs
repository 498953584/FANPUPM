using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_MonthPlanView : NBasePage, IRequiresSessionState
{
	private string Plantype;
	private Fund_Plan_MonthMainAction FA = new Fund_Plan_MonthMainAction();
	private DesktopBLL deskTop = new DesktopBLL();
	public string dtrow;
	protected string plantype
	{
		get
		{
			return this.Plantype;
		}
		set
		{
			this.Plantype = value;
		}
	}
	protected string MonthPlanID
	{
		get
		{
			object obj = this.ViewState["MonthPlanID"];
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}
		set
		{
			this.ViewState["MonthPlanID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindPlanDetail();
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"].ToString() != "")
		{
			this.MonthPlanID = base.Request.QueryString["ic"].ToString();
		}
		base.OnInit(e);
	}
	public string GetParty(string party)
	{
		if (party.Contains(','))
		{
			party = party.Split(new char[]
			{
				','
			})[1];
		}
		return party;
	}
	public void BindPlanDetail()
	{
		if (this.MonthPlanID != "" && this.MonthPlanID.Length == 36)
		{
			DataTable dt = new DataTable();
			Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
			fund_Plan_MonthMainInfo = this.FA.GetMainInfoByMonthPlanID(this.MonthPlanID);
			this.ltFlowState.Text = Common2.GetState(fund_Plan_MonthMainInfo.FlowState.ToString());
			this.ltadduser.Text = fund_Plan_MonthMainInfo.OperatorName.ToString();
			this.ltadddate.Text = fund_Plan_MonthMainInfo.OperateTime.ToShortDateString();
			if (fund_Plan_MonthMainInfo.PrjGuid.ToString() != "")
			{
				this.ltprjname.Text = fund_Plan_MonthMainInfo.PrjName.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(fund_Plan_MonthMainInfo.PlanYear.ToString()).Append("年");
			stringBuilder.Append(fund_Plan_MonthMainInfo.PlanMonth.ToString()).Append("月");
			stringBuilder.Append("资金");
			this.plantype = fund_Plan_MonthMainInfo.PlanType;
			int num = fund_Plan_MonthMainInfo.PlanYear;
			int num2 = fund_Plan_MonthMainInfo.PlanMonth;
			num2--;
			if (num2 < 1)
			{
				num2 = 12;
				num--;
			}
			if (!string.IsNullOrEmpty(fund_Plan_MonthMainInfo.Remark))
			{
				this.Literal2.Text = fund_Plan_MonthMainInfo.Remark.ToString();
			}
			string text = string.Empty;
			if (fund_Plan_MonthMainInfo.PlanType == "payout")
			{
				this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanPayOut");
				text = ConfigHelper.Get("MonthPlanPayOut");
				this.showaudit1.BusiCode = "091";
				dt = this.getLastPlan("payout", num2, num, fund_Plan_MonthMainInfo.PrjGuid.ToString());
				stringBuilder.Append("支出");
			}
			else
			{
				text = ConfigHelper.Get("MonthPlanIncome");
				this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanIncome");
				this.showaudit1.BusiCode = "090";
				dt = this.getLastPlan("income", num2, num, fund_Plan_MonthMainInfo.PrjGuid.ToString());
				stringBuilder.Append("收入");
			}
			text += this.MonthPlanID.ToString();
			string value = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				value = text;
				text = HttpContext.Current.Server.MapPath(text);
			}
			DirectoryUtility directoryUtility = new DirectoryUtility(text);
			List<Annex> annex = directoryUtility.GetAnnex();
			ISerializable serializable = new JsonSerializer();
			string a = serializable.Serialize<List<Annex>>(annex);
			StringBuilder stringBuilder2 = new StringBuilder();
			if (a != "[]")
			{
				for (int i = 0; i < annex.Count; i++)
				{
					if (annex[i].Name != null && annex[i].Name.ToString() != "")
					{
						stringBuilder2.Append("<a class=\"link\" target=\"_blank\" href=\"../../Common/DownLoad.aspx?path=").Append(value).Append("/").Append(annex[i].Name.ToString()).Append("\" title=\"" + HttpContext.Current.Server.UrlDecode(annex[i].Name.ToString()) + "\" >");
						stringBuilder2.Append(HttpContext.Current.Server.UrlDecode(annex[i].Name.ToString()));
						stringBuilder2.Append("</a>&nbsp;&nbsp;&nbsp;&nbsp;");
					}
				}
			}
			this.Literal1.Text = stringBuilder2.ToString();
			stringBuilder.Append("计划");
			this.lblTitle.Text = stringBuilder.ToString();
			DataTable planDetailList = this.FA.GetPlanDetailList(this.MonthPlanID, fund_Plan_MonthMainInfo.PlanType);
			DataTable dataTable = this.UniteDataTable(planDetailList, new DataTable
			{
				Columns =
				{

					{
						"sumtotal",
						Type.GetType("System.String")
					}
				},
				Rows =
				{
					new object[]
					{
						""
					}
				}
			}, "ce");
			dataTable = this.JoinDataTable(dataTable, dt, "eee");
			this.ViewState["sourcesTable"] = dataTable;
			this.gvwWebLineList.DataSource = dataTable;
			this.gvwWebLineList.DataBind();
			string[] value2 = new string[]
			{
				dataTable.Compute("SUM(PlanMoney)", string.Empty).ToString()
			};
			int[] index = new int[]
			{
				9
			};
			GridViewUtility.AddTotalRow(this.gvwWebLineList, value2, index);
		}
	}
	private DataTable getContpaid(string guid)
	{
		string sqlString = "select [BName],[contMoney],[PaymentMoney],[BalanceMoney] FROM cont_sum_paid a WHERE a.[ContractID]='" + guid + "'";
		return publicDbOpClass.DataTableQuary(sqlString);
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			if (dataRowView["ContractID"].ToString() != "")
			{
				e.Row.Cells[1].Text = this.GetContractNameByContractID(dataRowView["ContractID"].ToString());
			}
			double num = 0.0;
			double num2 = 0.0;
			if (dataRowView["EarlierMoney"] != null && dataRowView["EarlierMoney"].ToString() != "")
			{
				num2 = double.Parse(dataRowView["EarlierMoney"].ToString());
				e.Row.Cells[6].Text = string.Format("{0:0.000}", num2);
			}
			else
			{
				e.Row.Cells[6].Text = "0.000";
			}
			if (dataRowView["ActuallyMoney"] != null && dataRowView["ActuallyMoney"].ToString() != "")
			{
				num = double.Parse(dataRowView["ActuallyMoney"].ToString());
				e.Row.Cells[7].Text = string.Format("{0:0.000}", num);
			}
			else
			{
				e.Row.Cells[7].Text = "0.000";
			}
			if (num > 0.0 && num2 > 0.0)
			{
				e.Row.Cells[8].Text = string.Format("{0:P}", num / num2 * 100.0 / 100.0);
			}
			else
			{
				e.Row.Cells[8].Text = "0.00%";
			}
			string text = dataRowView["ReMark"].ToString();
			if (text != "" && text.Length > 20)
			{
				e.Row.Cells[10].Text = text.Substring(0, 19) + "......";
			}
			if (dataRowView["BalanceMoney"].ToString() == "")
			{
				e.Row.Cells[4].Text = "0.000";
			}
			if (dataRowView["PaymentMoney"].ToString() == "")
			{
				e.Row.Cells[5].Text = "0.000";
			}
			if (dataRowView["PlanMoney"].ToString() == "")
			{
				e.Row.Cells[9].Text = "0.000";
			}
			string text2 = string.Empty;
			if (this.plantype == "payout")
			{
				text2 = ConfigHelper.Get("MonthPlanPayOut");
			}
			else
			{
				text2 = ConfigHelper.Get("MonthPlanIncome");
			}
			this.hfldAdjunctPath.Value = text2;
			text2 += this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString();
			DirectoryUtility directoryUtility = new DirectoryUtility(text2);
			List<Annex> annex = directoryUtility.GetAnnex();
			ISerializable serializable = new JsonSerializer();
			string a = serializable.Serialize<List<Annex>>(annex);
			StringBuilder stringBuilder = new StringBuilder();
			if (a != "[]")
			{
				stringBuilder.Append("<span class=\"link\" onclick=\"queryAdjunct('" + this.gvwWebLineList.DataKeys[e.Row.RowIndex].Value.ToString() + "')\">");
				stringBuilder.Append("<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />");
				stringBuilder.Append("</span>");
			}
			e.Row.Cells[11].Text = stringBuilder.ToString();
			return;
		}
		if (e.Row.RowType != DataControlRowType.Header)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				e.Row.Cells[0].Text = "aaa";
			}
			return;
		}
		if (this.plantype == "payout")
		{
			e.Row.Cells[2].Text = "乙方";
			e.Row.Cells[5].Text = "合同已付总额";
			e.Row.Cells[6].Text = "上期计划金额";
			e.Row.Cells[7].Text = "<div>上期实际发生金额<img title=\"=合同上期计划已通过资金支付申请审核金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" /></div>";
			e.Row.Cells[8].Text = "<div>上期计划执行完成情况 <img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" /></div>";
			return;
		}
		e.Row.Cells[2].Text = "甲方";
		e.Row.Cells[5].Text = "合同已收总额";
		e.Row.Cells[6].Text = "上期计划金额";
		e.Row.Cells[7].Text = "<div>上期实际发生金额<img title=\"=合同上期计划已回款金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" /></div>";
		e.Row.Cells[8].Text = "<div>上期计划执行完成情况 <img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" /></div>";
	}
	protected void btnClose_Click(object sender, EventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
		base.RegisterScript(stringBuilder.ToString());
	}
	private string GetContractNameByContractID(string _ContractID)
	{
		string result = string.Empty;
		if (_ContractID.Length == 36)
		{
			if (this.plantype != "" && this.plantype == "payout")
			{
				PayoutContract payoutContract = new PayoutContract();
				result = payoutContract.GetModel(_ContractID).ContractName;
			}
			else
			{
				if (this.plantype != "" && this.plantype == "income")
				{
					DataTable table = Common2.GetTable("Con_Incomet_Contract", " where ContractID='" + _ContractID + "'");
					if (table.Rows.Count > 0 && table.Rows[0]["ContractName"] != null)
					{
						result = table.Rows[0]["ContractName"].ToString();
					}
				}
			}
		}
		return result;
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_53_0 = dataTable.Rows.Count;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = string.Empty;
			text = dataRow["OriginalName"].ToString();
			text = string.Concat(new object[]
			{
				"<span class=\"link\" onclick=\"javascript:return openannexpage('",
				dataRow["RecordCode"],
				"',",
				dataRow["ModuleID"],
				")\" title=\"",
				text,
				"\">",
				text,
				"</span>"
			});
			stringBuilder.Append(text);
			stringBuilder.Append(", ");
		}
		string result = string.Empty;
		if (stringBuilder.Length > 2)
		{
			result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
		}
		return result;
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = this.ViewState["sourcesTable"] as DataTable;
		dataTable.Columns["OrderID"].ColumnName = "排列序号";
		dataTable.Columns["ContractID"].ColumnName = "依据合同";
		dataTable.Columns["Plansubject"].ColumnName = "所属科目";
		dataTable.Columns["OldBalance"].ColumnName = "上期结余";
		dataTable.Columns["PlanMoney"].ColumnName = "本期计划";
		dataTable.Columns["ReMark"].ColumnName = "备注";
		dataTable.Columns.Remove("UID");
		dataTable.Columns.Remove("MonthPlanID");
		dataTable.Columns.Remove("ThisBalance");
		return dataTable;
	}
	protected void leadingout_Click(object sender, EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				8
			}
		}, reportData, this.lblTitle.Text.ToString() + "报表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
	{
		DataTable dataTable = dt1.Clone();
		for (int i = 0; i < dt2.Columns.Count; i++)
		{
			dataTable.Columns.Add(dt2.Columns[i].ColumnName);
		}
		object[] array = new object[dataTable.Columns.Count];
		for (int j = 0; j < dt1.Rows.Count; j++)
		{
			dt1.Rows[j].ItemArray.CopyTo(array, 0);
			dataTable.Rows.Add(array);
		}
		if (dt1.Rows.Count >= dt2.Rows.Count)
		{
			for (int k = 0; k < dt2.Rows.Count; k++)
			{
				for (int l = 0; l < dt2.Columns.Count; l++)
				{
					dataTable.Rows[k][l + dt1.Columns.Count] = dt2.Rows[k][l].ToString();
				}
			}
		}
		else
		{
			for (int m = 0; m < dt2.Rows.Count - dt1.Rows.Count; m++)
			{
				DataRow row = dataTable.NewRow();
				dataTable.Rows.Add(row);
			}
			for (int n = 0; n < dt2.Rows.Count; n++)
			{
				for (int num = 0; num < dt2.Columns.Count; num++)
				{
					dataTable.Rows[n][num + dt1.Columns.Count] = dt2.Rows[n][num].ToString();
				}
			}
		}
		for (int num2 = 0; num2 < dataTable.Rows.Count; num2++)
		{
			try
			{
				double num3 = double.Parse(dt1.Rows[num2]["PlanMoney"].ToString());
				double num4 = double.Parse(dt1.Rows[num2]["OldBalance"].ToString());
				dataTable.Rows[num2]["sumtotal"] = num3 + num4;
			}
			catch (Exception)
			{
			}
		}
		for (int num5 = dataTable.Rows.Count - 1; num5 > 0; num5--)
		{
			string value = dataTable.Rows[num5]["UID"].ToString();
			if (string.IsNullOrEmpty(value))
			{
				dataTable.Rows.RemoveAt(num5);
			}
		}
		dataTable.TableName = DTName;
		return dataTable;
	}
	private DataTable JoinDataTable(DataTable dt1, DataTable dt2, string DTName)
	{
		DataTable dataTable = dt1.Clone();
		int count = dt1.Columns.Count;
		int arg_1E_0 = dt2.Columns.Count;
		int count2 = dataTable.Columns.Count;
		for (int i = 1; i < dt2.Columns.Count; i++)
		{
			dataTable.Columns.Add(dt2.Columns[i].ColumnName);
		}
		object[] array = new object[count];
		for (int j = 0; j < dt1.Rows.Count; j++)
		{
			dt1.Rows[j].ItemArray.CopyTo(array, 0);
			dataTable.Rows.Add(array);
		}
		for (int k = 0; k < dt1.Rows.Count; k++)
		{
			for (int l = 0; l < dt2.Rows.Count; l++)
			{
				if (dataTable.Rows[k]["UID"].Equals(dt2.Rows[l]["UID"]))
				{
					for (int m = 1; m < dt2.Columns.Count; m++)
					{
						dt2.Rows[l][m].ToString();
						dataTable.Rows[k][count2 + m - 1] = dt2.Rows[l][m];
					}
				}
			}
		}
		dataTable.TableName = DTName;
		return dataTable;
	}
	private DataTable getLastPlan(string plantype, int _PlanMonth, int _PlanYear, string PrjGuid)
	{
		DataTable result = new DataTable();
		if (plantype == "payout")
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" WITH CTE as(");
			stringBuilder.Append(" SELECT  fpmd.PlanMoney AS EarlierMoney,fpmd.MonthPlanID,fpmm.PlanMonth,fpmm.PlanYear,fpmm.PrjGuid,fpmd.ContractID,fpmd.UID FROM   Fund_Plan_MonthDetail fpmd");
			stringBuilder.Append(" LEFT JOIN Fund_Plan_MonthMain fpmm ");
			stringBuilder.Append(" ON  fpmd.MonthPlanID=fpmm.MonthPlanID ");
			stringBuilder.Append(" WHERE fpmm.PrjGuid ='").Append(PrjGuid);
			stringBuilder.Append("' AND fpmm.PlanYear =").Append(_PlanYear);
			stringBuilder.Append(" AND PlanType = 'payout')");
			stringBuilder.Append(" SELECT  CTE.UID,CTE1.EarlierMoney, cic.ContractName AS sqcname ,");
			stringBuilder.Append(" SUM (cip.PaymentMoney) AS ActuallyMoney");
			stringBuilder.Append(" FROM CTE");
			stringBuilder.Append(" LEFT JOIN (Select CTE.ContractID,CTE.EarlierMoney,CTE.UID FROM CTE where CTE.PlanMonth=").Append(_PlanMonth);
			stringBuilder.Append(" )AS CTE1 ON CTE.ContractID=CTE1.ContractID");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Contract cic ON cic.ContractID = CTE1.ContractID");
			stringBuilder.Append(" LEFT JOIN Con_Payout_Payment cip ON cip.MonthPlanUID = CTE1.UID AND cip.FlowState=1");
			stringBuilder.Append(" WHERE CTE.ContractID=CTE1.ContractID AND CTE.PlanMonth=").Append(_PlanMonth + 1);
			stringBuilder.Append(" GROUP BY cic.ContractName,CTE1.EarlierMoney,CTE.UID ");
			result = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		}
		else
		{
			if (plantype == "income")
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("WITH CTE as(");
				stringBuilder2.Append(" SELECT  fpmd.PlanMoney AS EarlierMoney,fpmd.MonthPlanID,fpmm.PlanMonth,fpmm.PlanYear,fpmm.PrjGuid,fpmd.ContractID,fpmd.UID FROM   Fund_Plan_MonthDetail fpmd");
				stringBuilder2.Append("  LEFT JOIN Fund_Plan_MonthMain fpmm");
				stringBuilder2.Append(" ON  fpmd.MonthPlanID=fpmm.MonthPlanID ");
				stringBuilder2.Append(" WHERE fpmm.PrjGuid = '").Append(PrjGuid).Append("' ");
				stringBuilder2.Append(" AND fpmm.PlanYear =").Append(_PlanYear);
				stringBuilder2.Append(" AND fpmm.PlanType='income')SELECT  CTE.UID,CTE1.EarlierMoney, cic.ContractName AS sqcname ,");
				stringBuilder2.Append(" SUM (cip.CllectionPrice) AS ActuallyMoney ");
				stringBuilder2.Append(" FROM CTE");
				stringBuilder2.Append(" LEFT JOIN (Select CTE.ContractID,CTE.EarlierMoney,CTE.UID FROM CTE where CTE.PlanMonth=").Append(_PlanMonth);
				stringBuilder2.Append(" )AS CTE1 ON CTE.ContractID=CTE1.ContractID");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Contract cic ON cic.ContractID = CTE1.ContractID");
				stringBuilder2.Append(" LEFT JOIN Con_Incomet_Payment cip ON cip.MonthPlanUID = CTE1.UID");
				stringBuilder2.Append(" WHERE CTE.ContractID=CTE1.ContractID AND CTE.PlanMonth=").Append(_PlanMonth + 1);
				stringBuilder2.Append(" GROUP BY cic.ContractName,CTE1.EarlierMoney,CTE.UID");
				result = publicDbOpClass.DataTableQuary(stringBuilder2.ToString());
			}
		}
		return result;
	}
}


