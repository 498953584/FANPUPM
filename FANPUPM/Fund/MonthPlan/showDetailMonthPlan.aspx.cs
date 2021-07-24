using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_showDetailMonthPlan : NBasePage, IRequiresSessionState
{
	private string _mpid;
	private int _PlanMonth;
	private int _PlanYear;
	private string _plantype;
	private Fund_Plan_MonthMainAction FA = new Fund_Plan_MonthMainAction();

	public string Mpid
	{
		get
		{
			return this._mpid;
		}
		set
		{
			this._mpid = value;
		}
	}
	public int PlanMonth
	{
		get
		{
			return this._PlanMonth;
		}
		set
		{
			this._PlanMonth = value;
		}
	}
	public int PlanYear
	{
		get
		{
			return this._PlanYear;
		}
		set
		{
			this._PlanYear = value;
		}
	}
	public string plantype
	{
		get
		{
			return this._plantype;
		}
		set
		{
			this._plantype = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["mpid"] != null)
		{
			this.Mpid = base.Request.QueryString["mpid"].ToString();
		}
		if (!base.IsPostBack)
		{
			this.initParameterByMpid();
		}
	}
	private void initParameterByMpid()
	{
		if (!string.IsNullOrEmpty(this.Mpid))
		{
			Fund_Plan_MonthMainInfo fund_Plan_MonthMainInfo = new Fund_Plan_MonthMainInfo();
			fund_Plan_MonthMainInfo = this.FA.GetMainInfoByMonthPlanID(this.Mpid);
			if (fund_Plan_MonthMainInfo != null)
			{
				new StringBuilder();
				DataTable dataSource = new DataTable();
				int num = fund_Plan_MonthMainInfo.PlanYear;
				int num2 = fund_Plan_MonthMainInfo.PlanMonth - 1;
				if (num2 < 1)
				{
					num--;
				}
				this._plantype = fund_Plan_MonthMainInfo.PlanType;
				StringBuilder stringBuilder = new StringBuilder();
				if (fund_Plan_MonthMainInfo.PlanType == "payout")
				{
					this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanPayOut");
					stringBuilder.Append("  SELECT tab.*, b.CorpName  ,ISNULL((shijifasheng/nullif(Shangqi,0))*100,0) AS BL FROM ( SELECT Plandetail.*,planMain.PlanType, (SELECT isnull(SUM(PlanMoney),0) FROM Fund_Plan_MonthDetail AS d ");
					stringBuilder.Append(" WHERE d.ContractID=plandetail.ContractID AND  d.MonthPlanID= (SELECT m.MonthPlanID FROM Fund_Plan_MonthMain AS m  ");
					stringBuilder.Append(" WHERE m.PlanDate= DATEADD(MONTH,-1,planMain.PlanDate) AND m.PlanType=PlanMain.PlanType AND m.FlowState=1 AND m.PrjGuid=planMain.PrjGuid) ) AS Shangqi, ");
					stringBuilder.Append("  (SELECT isnull(SUM(pay.paymentMoney),0) FROM dbo.Con_Payout_Payment AS pay WHERE pay.FlowState=1 AND pay.MonthPlanUID IN ");
					stringBuilder.Append(" (SELECT pd.UID FROM Fund_Plan_MonthDetail AS pd WHERE pd.ContractID=plandetail.ContractID AND pd.MonthPlanID=(SELECT m.MonthPlanID ");
					stringBuilder.Append(" FROM Fund_Plan_MonthMain AS m WHERE m.PlanDate= DATEADD(MONTH,-1,planMain.PlanDate) AND m.PlanType=PlanMain.PlanType AND m.FlowState=1 AND m.PrjGuid=planMain.PrjGuid) ) ) AS shijifasheng, ");
					stringBuilder.Append(" (SELECT ContractName FROM Con_Payout_Contract AS cont WHERE cont.ContractID=plandetail.ContractID )AS ContractName");
					stringBuilder.Append("  FROM Fund_Plan_MonthDetail AS PlanDetail LEFT JOIN Fund_Plan_MonthMain AS PlanMain ON plandetail.MonthPlanID=PlanMain.MonthPlanID) AS tab ");
					stringBuilder.Append(" LEFT JOIN  Con_Payout_Contract con  ON con.contractid=tab.contractid");
					stringBuilder.Append(" LEFT JOIN XPM_Basic_ContactCorp b ON b.corpid=con.bName");
				}
				else
				{
					if (fund_Plan_MonthMainInfo.PlanType == "income")
					{
						this.hfldAdjunctPath.Value = ConfigHelper.Get("MonthPlanIncome");
						stringBuilder.Append("  SELECT tab.*, CON.Second AS CorpName ,ISNULL((shijifasheng/nullif(Shangqi,0))*100,0) AS BL FROM ( SELECT Plandetail.*,planMain.PlanType, (SELECT isnull(SUM(PlanMoney),0) FROM Fund_Plan_MonthDetail AS d ");
						stringBuilder.Append(" WHERE d.ContractID=plandetail.ContractID AND  d.MonthPlanID= (SELECT m.MonthPlanID FROM Fund_Plan_MonthMain AS m  ");
						stringBuilder.Append(" WHERE m.PlanDate= DATEADD(MONTH,-1,planMain.PlanDate) AND m.PlanType=PlanMain.PlanType AND m.FlowState=1 AND m.PrjGuid=planMain.PrjGuid) ) AS Shangqi, ");
						stringBuilder.Append("  (SELECT isnull(SUM(pay.CllectionPrice),0) FROM dbo.Con_Incomet_Payment AS pay WHERE  pay.MonthPlanUID IN ");
						stringBuilder.Append(" (SELECT pd.UID FROM Fund_Plan_MonthDetail AS pd WHERE pd.ContractID=plandetail.ContractID AND pd.MonthPlanID=(SELECT m.MonthPlanID ");
						stringBuilder.Append(" FROM Fund_Plan_MonthMain AS m WHERE m.PlanDate= DATEADD(MONTH,-1,planMain.PlanDate) AND m.PlanType=PlanMain.PlanType AND m.FlowState=1 AND m.PrjGuid=planMain.PrjGuid) ) ) AS shijifasheng, ");
						stringBuilder.Append(" (SELECT ContractName FROM Con_Incomet_Contract AS cont WHERE cont.ContractID=plandetail.ContractID )AS ContractName ");
						stringBuilder.Append("  FROM Fund_Plan_MonthDetail AS PlanDetail LEFT JOIN Fund_Plan_MonthMain AS PlanMain ON plandetail.MonthPlanID=PlanMain.MonthPlanID) AS tab ");
						stringBuilder.Append("\tLEFT JOIN Con_Incomet_Contract AS CON ON CON.contractid=tab.contractid ");
					}
				}
				stringBuilder.Append(string.Concat(new object[]
				{
					"  where PlanType='",
					fund_Plan_MonthMainInfo.PlanType,
					"' and MonthPlanID='",
					fund_Plan_MonthMainInfo.MonthPlanID,
					"' "
				}));
				dataSource = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
				this.gvMonthPlanList.DataSource = dataSource;
				this.gvMonthPlanList.DataBind();
			}
			this.Literal1.Text = "";
			return;
		}
		this.showGV.Visible = false;
		string text = string.Empty;
		text += "<table class=\"gvdata\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"emptyMonthPlanType\" style=\"width: 100%; border-collapse: collapse;\">";
		text += "<tr class=\"header\">";
		text += "<th scope=\"col\" style=\"width: 20px;\">";
		text += "序号";
		text += "</th>   ";
		text += "<th scope=\"col\" style=\"width:100px;\">";
		text += "合同名称";
		text += "</th>          ";
		text += "<th scope=\"col\" style=\"width: 80px;\">";
		text += "上期计划金额";
		text += "</th>";
		if (this.plantype == "payout")
		{
			text += "<th scope=\"col\" style=\"width: 80px;\">";
			text += "上期实际发生金额<img title=\"＝合同上期计划已通过资金支付申请审核金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			text += "</th>";
			text += "<th scope=\"col\" style=\"width: 80px;\">";
			text += "上期计划执行完成情况<img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			text += "</th>";
		}
		else
		{
			text += "<th scope=\"col\" style=\"width: 80px;\">";
			text += "上期实际发生金额<img title=\"＝合同上期计划已回款金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			text += "</th>";
			text += "<th scope=\"col\" style=\"width: 80px;\">";
			text += "上期计划执行完成情况<img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			text += "</th>";
		}
		text += "<th scope=\"col\" style=\"width: 80px;\">";
		text += "本期计划金额";
		text += "</th>                  ";
		text += "<th scope=\"col\" style=\"width: 30px;\">";
		text += "附件";
		text += "</th>";
		text += "<th scope=\"col\">";
		text += "备注";
		text += "</th>";
		text += "</tr>";
		text += "</table>";
		this.Literal1.Text = text;
	}
	protected void gvMonthPlanList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = string.Empty;
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvMonthPlanList.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["guid"] = this.gvMonthPlanList.DataKeys[e.Row.RowIndex]["UID"].ToString();
			string text2 = this.gvMonthPlanList.DataKeys[e.Row.RowIndex]["UID"].ToString();
			if (text2 != "")
			{
				if (this._plantype == "payout")
				{
					text = ConfigHelper.Get("MonthPlanPayOut");
				}
				else
				{
					text = ConfigHelper.Get("MonthPlanIncome");
				}
				text += text2.ToString();
				DirectoryUtility directoryUtility = new DirectoryUtility(text);
				List<Annex> annex = directoryUtility.GetAnnex();
				ISerializable serializable = new JsonSerializer();
				string a = serializable.Serialize<List<Annex>>(annex);
				StringBuilder stringBuilder = new StringBuilder();
				if (a != "[]")
				{
					stringBuilder.Append("<span class=\"link\" onclick=\"queryAdjunct('" + text2.ToString() + "')\">");
					stringBuilder.Append("<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />");
					stringBuilder.Append("</span>");
				}
				e.Row.Cells[7].Text = stringBuilder.ToString();
			}
			string text3 = dataRowView["ReMark"].ToString();
			if (text3 != "" && text3.Length > 38)
			{
				e.Row.Cells[8].Text = text3.Substring(0, 37) + "......";
				return;
			}
		}
		else
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				if (this.plantype == "payout")
				{
					e.Row.Cells[3].Text = "上期实际发生金额<img title=\"=合同上期计划已通过资金支付申请审核金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
					e.Row.Cells[4].Text = "上期计划执行完成情况 <img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
					return;
				}
				e.Row.Cells[3].Text = "上期实际发生金额<img title=\"=合同上期计划已回款金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
				e.Row.Cells[4].Text = "上期计划执行完成情况<img title=\"＝上期实际发生金额／上期计划金额\" style=\"cursor: pointer;\" src=\"/images/help.jpg\" />";
			}
		}
	}
	protected void gvMonthPlanList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvMonthPlanList.PageIndex = e.NewPageIndex;
		this.initParameterByMpid();
	}
	private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
	{
		if (dt1.Rows.Count > 0)
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
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				if (dataTable.Rows[k]["ContractID"].ToString() != "")
				{
					DataRow[] array2 = dt2.Select("NEWConID='" + dataTable.Rows[k]["ContractID"].ToString() + "'");
					if (array2.Length > 0)
					{
						dataTable.Rows[k]["sqcname"] = array2[0]["sqcname"].ToString();
						dataTable.Rows[k]["NEWConID"] = array2[0]["NEWConID"].ToString();
						dataTable.Rows[k]["ActuallyMoney"] = array2[0]["ActuallyMoney"].ToString();
						dataTable.Rows[k]["EarlierMoney"] = array2[0]["EarlierMoney"].ToString();
					}
				}
			}
			for (int l = dataTable.Rows.Count - 1; l > 0; l--)
			{
				string value = dataTable.Rows[l]["UID"].ToString();
				if (string.IsNullOrEmpty(value))
				{
					dataTable.Rows.RemoveAt(l);
				}
			}
			dataTable.TableName = DTName;
			return dataTable;
		}
		return dt1;
	}
}


