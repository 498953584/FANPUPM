using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_WorkPlanAllList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.bindSouce();
		}
	}
	protected void bindSouce()
	{
		string text = string.Empty;
		DateTime dateTime = DateTime.Now;
		if (!string.IsNullOrEmpty(this.txtWkplanDate.Text))
		{
			dateTime = Convert.ToDateTime(this.txtWkplanDate.Text.Trim());
			if (dateTime.DayOfWeek >= DayOfWeek.Friday)
			{
				text = Common2.GetTime(dateTime.AddDays((double)(5 - Convert.ToInt32(dateTime.DayOfWeek))));
			}
			else
			{
				text = Common2.GetTime(dateTime.AddDays((double)(-(double)(Convert.ToInt32(dateTime.DayOfWeek) + 2))));
			}
		}
		else
		{
			if (DateTime.Now.DayOfWeek >= DayOfWeek.Friday)
			{
				text = Common2.GetTime(DateTime.Now.AddDays((double)(5 - Convert.ToInt32(DateTime.Now.DayOfWeek))));
			}
			else
			{
				text = Common2.GetTime(DateTime.Now.AddDays((double)(-(double)(Convert.ToInt32(DateTime.Now.DayOfWeek) + 2))));
			}
		}
		DateTime dateTime2 = Convert.ToDateTime(dateTime.ToString("yyyy-MM") + "-01");
		DateTime monthLastDate = Convert.ToDateTime(dateTime2).AddMonths(1).AddDays(-1.0);
		DateTime monthFirstDate = dateTime2.AddMonths(-2);
		int num = this.WeekCount(dateTime2, monthLastDate);
		int num2 = this.WeekCount(monthFirstDate, monthLastDate);
		string text2 = string.IsNullOrEmpty(this.txtDepmentName.Value.Trim()) ? string.Empty : ("  AND i_bmdm='" + this.hflDepCode.Value.Trim() + "'");
		string sqlString = string.Format("WITH CRT_Work AS(\r\n                        --获取最新的一条周计划数据\r\n\t                        SELECT ROW_NUMBER() OVER(PARTITION BY WkpDeptId,WkpReportUser ORDER BY \r\n                            WkpRecordDate DESC)AS Number, * FROM pm_workplan_weekplan\tWHERE WkpRecordDate='{0}'\r\n                        ),CRT_Ptyh AS(\r\n                        --获取相关部门的人员信息\r\n\t                        SELECT Pt_bm.i_bmdm,Pt_bm.V_BMMC,pt_bm.v_bmqc,Pt_yh.v_yhdm,pt_yh.v_xm,Pt_dy.DutyName \r\n\t                        FROM PT_d_bm AS Pt_bm\r\n\t                        LEFT JOIN PT_yhmc AS Pt_yh ON Pt_bm.i_bmdm=pt_yh.i_bmdm\r\n\t                        LEFT JOIN PT_DUTY AS Pt_dy ON Pt_dy.I_DUTYID=pt_yh.I_DUTYID\r\n                        ),CRT_Plan AS(\r\n                        --获取相关部门人员对应的周计划信息\r\n\t                        SELECT * FROM CRT_Ptyh \r\n\t                        LEFT JOIN (SELECT * FROM CRT_Work WHERE Number=1)AS Works\r\n\t                        ON Works.WkpDeptId=CRT_Ptyh.i_bmdm AND CRT_Ptyh.v_yhdm=Works.WkpReportUser\r\n                        ),CRT_WorkPlan AS(\r\n                        --根据录入时间，周计划开始时间，来获取周计划未提交，晚提交，提交的信息\r\n\t                        SELECT *,(CASE  WHEN  WkpId IS NOT NULL AND WkpIsTrue IN(1,0) AND DATEDIFF(day,WkpRecordDate,InputTime)>=0  THEN '' ELSE '1' END )AS NoCommit,\r\n\t                        (CASE WHEN WkpId IS NOT NULL AND WkpIsTrue IN(1,0) AND DATEDIFF(day,WkpRecordDate,InputTime)>0 THEN '1' ELSE '' END)AS LateCommit,\r\n\t                        (CASE WHEN WkpId IS NOT NULL AND WkpIsTrue IN(1,0) AND DATEDIFF(day,WkpRecordDate,InputTime)=0 THEN '1' ELSE '' END )AS IsCommit  \r\n\t                        FROM CRT_Plan\r\n                        ),CRT_CountNoCommit AS(\r\n                        --获取未提交的数据个数\r\n\t                        SELECT i_bmdm,ISNULL(COUNT(NoCommit),0) AS CountCommit \r\n\t                        FROM CRT_WorkPlan WHERE NoCommit!='' GROUP BY i_bmdm\r\n                        ),CRT_CountCommit AS(\r\n                        --获取已提交的数据个数\r\n\t                        SELECT i_bmdm,ISNULL(COUNT(IsCommit),0) AS CountIsCommit \r\n\t                        FROM CRT_WorkPlan WHERE IsCommit!='' GROUP BY i_bmdm\r\n                        ),CRT_CountLate AS(\r\n                        --获取晚提交的数据个数\r\n\t                        SELECT i_bmdm,ISNULL(COUNT(LateCommit),0)AS CountLateCommit \r\n\t                        FROM CRT_WorkPlan WHERE LateCommit!='' GROUP BY i_bmdm\r\n                        ),CRT_Countdbm AS(\r\n                        --相关部门人员个数\r\n\t                        SELECT i_bmdm, V_BMMC+'  计数' AS V_BMMC ,ISNULL(Count(v_yhdm),0) AS Countbm \r\n\t                        FROM CRT_Ptyh GROUP BY i_bmdm,V_BMMC\r\n                        ),CRT_Month AS(\r\n                        --获取该月的数据\r\n\t                        SELECT * FROM  pm_workplan_weekplan WHERE datediff(MM,'{0}',WkpRecordDate)=0\r\n                        ),CRT_MonthCount AS(\r\n                        --获取该月提交，晚交的数据\r\n\t                        SELECT AA.*,ISNULL(BB.MonthLate,0)AS MonthLate FROM \r\n\t\t                        (SELECT WkpDeptId,ISNULL(COUNT(WkpId),0)AS MonthCommit \r\n\t\t                        FROM CRT_Month WHERE datediff(day,WkpRecordDate,InputTime)=0  \r\n\t                        GROUP BY WkpDeptId)AS AA\r\n                        LEFT JOIN (SELECT WkpDeptId,COUNT(WkpId)AS MonthLate \r\n\t\t                        FROM CRT_Month WHERE datediff(day,WkpRecordDate,InputTime)>0 \r\n\t\t                        GROUP BY WkpDeptId )AS BB\r\n\t                        ON AA.WkpDeptId=BB.WkpDeptId\r\n                        ),CRT_TreeMonth AS(\r\n                        --获取该月的前三个月的数据\r\n\t                        SELECT * FROM  pm_workplan_weekplan WHERE CONVERT(VARCHAR(7),WkpRecordDate,120)\r\n                            BETWEEN CONVERT(VARCHAR(7),dateadd(mm,-3,'{0}'),120) \r\n\t                        AND CONVERT(VARCHAR(7),'{0}',120)\r\n                        ),CRT_TreeMonthCount AS(\r\n                        --获取该月前三个月提交，晚交的数据\r\n\t                        SELECT AA.*,ISNULL(BB.TreeMonthLate,0)AS TreeMonthLate FROM \r\n\t\t                        (SELECT WkpDeptId,ISNULL(COUNT(WkpId),0)AS TreeMonthCommit \r\n\t\t                        FROM CRT_TreeMonth WHERE datediff(day,WkpRecordDate,InputTime)=0  \r\n\t                        GROUP BY WkpDeptId)AS AA\r\n                        LEFT JOIN (SELECT WkpDeptId,COUNT(WkpId)AS TreeMonthLate \r\n\t\t                        FROM CRT_TreeMonth WHERE datediff(day,WkpRecordDate,InputTime)>0 \r\n\t\t                        GROUP BY WkpDeptId )AS BB\r\n\t                        ON AA.WkpDeptId=BB.WkpDeptId\r\n                        ),CRT_WorkPlanAll AS(\r\n                        --周计划数据与已提交，未提交，晚提交的数据合并\r\n\t                        SELECT *,NULL AS CommitRate,NULL AS LateCommitRate,NULL AS IsCommitRate,\r\n\t                        CONVERT(VARCHAR(7),WkpRecordDate,120)AS WkpMonth,NULL AS MonthCommit ,NULL AS MonthLate,NULL AS MonthRate,\r\n\t                        CONVERT(VARCHAR(7),DATEADD(mm,-3,'{0}'),120)+'~~'+CONVERT(VARCHAR(7),'{0}',120) AS DuringMonths,\r\n\t                        NULL AS TreeMonthCommit,NULL AS TreeMonthLate,NULL AS TreeMonthRate  FROM CRT_WorkPlan\r\n                        UNION \r\n\t                        SELECT CRT_Countdbm.i_bmdm,CRT_Countdbm.V_BMMC,NULL,NULL,CAST(CRT_Countdbm.Countbm AS VARCHAR(200)),NULL,NULL,NULL,NULL,NULL,NULL,\r\n\t                        NULL,NULL,NULL,NULL,NULL,NULL,NULL,ISNULL(CountCommit,0),ISNULL(CountLateCommit,0),ISNULL(CountIsCommit,0),\r\n                            CountCommit*1.0/NULLIF(CRT_Countdbm.Countbm,0),CountLateCommit*1.0/NULLIF(CRT_Countdbm.Countbm,0),CountIsCommit*1.0/NULLIF(CRT_Countdbm.Countbm,0),\r\n                            CONVERT(VARCHAR(200),{1}*CRT_Countdbm.Countbm),ISNULL(CRT_MonthCount.MonthCommit,0),\r\n\t                        ISNULL(CRT_MonthCount.MonthLate,0),(CRT_MonthCount.MonthCommit+CRT_MonthCount.MonthLate)*1.0/({1}*CRT_Countdbm.Countbm),CONVERT(VARCHAR(32),{2}*CRT_Countdbm.Countbm),\r\n                            ISNULL(CRT_TreeMonthCount.TreeMonthCommit,0)AS TreeMonthCommit,ISNULL(CRT_TreeMonthCount.TreeMonthLate,0)AS TreeMonthLate,\r\n                            CRT_TreeMonthCount.TreeMonthCommit*1.0/({2}*CRT_Countdbm.Countbm) FROM CRT_Countdbm\r\n\t                        LEFT JOIN CRT_CountNoCommit ON CRT_Countdbm.i_bmdm=CRT_CountNoCommit.i_bmdm\r\n\t                        LEFT JOIN CRT_CountCommit ON CRT_Countdbm.i_bmdm=CRT_CountCommit.i_bmdm\r\n\t                        LEFT JOIN CRT_CountLate ON CRT_Countdbm.i_bmdm=CRT_CountLate.i_bmdm\r\n                            LEFT JOIN CRT_MonthCount ON CRT_Countdbm.i_bmdm=CRT_MonthCount.WkpDeptId\r\n\t                        LEFT JOIN CRT_TreeMonthCount ON CRT_Countdbm.i_bmdm=CRT_TreeMonthCount.WkpDeptId\r\n                        ),CRT_WorkPlans AS(\r\n                            SELECT ROW_NUMBER() OVER(PARTITION BY i_bmdm,V_BMMC ORDER BY WkpRecordDate DESC)AS OrderNo,\r\n                            * FROM CRT_WorkPlanAll WHERE 1=1 {3}\r\n                        )SELECT ROW_NUMBER() OVER(ORDER BY I_BMDM ASC ) AS Num, * FROM CRT_WorkPlans ", new object[]
		{
			text,
			num,
			num2,
			text2
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		this.ViewState["weekPlanSouce"] = dataTable;
		this.gvWorkPlanList.DataSource = dataTable;
		this.gvWorkPlanList.DataBind();
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.bindSouce();
	}
	protected void btnExcel_Click(object sender, EventArgs e)
	{
		DataTable dateSouce = this.getDateSouce();
		IExportable exporter = new ExcelExporter();
		FileExport fileExport = new FileExport(exporter, dateSouce, "周计划报表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected DataTable getDateSouce()
	{
		DataTable dataTable = this.ViewState["weekPlanSouce"] as DataTable;
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("序号");
		dataTable2.Columns.Add("部门");
		dataTable2.Columns.Add("人员");
		dataTable2.Columns.Add("岗位");
		dataTable2.Columns.Add("开始时间");
		dataTable2.Columns.Add("提交计数");
		dataTable2.Columns.Add("提交比率");
		dataTable2.Columns.Add("晚交计数");
		dataTable2.Columns.Add("晚交比率");
		dataTable2.Columns.Add("未提交计数");
		dataTable2.Columns.Add("未提交比率");
		dataTable2.Columns.Add("月份");
		dataTable2.Columns.Add("该月按时提交");
		dataTable2.Columns.Add("该月晚交");
		dataTable2.Columns.Add("该月份总比率");
		dataTable2.Columns.Add("该月前三个月");
		dataTable2.Columns.Add("按时提交");
		dataTable2.Columns.Add("晚交");
		dataTable2.Columns.Add("该月前三个月提交比率");
		foreach (DataRow dataRow in dataTable.Rows)
		{
			DataRow dataRow2 = dataTable2.NewRow();
			dataRow2["序号"] = dataRow["Num"].ToString();
			dataRow2["部门"] = dataRow["V_BMMC"].ToString();
			dataRow2["人员"] = dataRow["v_xm"].ToString();
			dataRow2["岗位"] = dataRow["DutyName"].ToString();
			dataRow2["开始时间"] = Common2.GetTime(dataRow["WkpRecordDate"]);
			dataRow2["提交计数"] = ((dataRow["IsCommit"].ToString() == "0") ? string.Empty : dataRow["IsCommit"].ToString());
			dataRow2["提交比率"] = ((dataRow["IsCommitRate"] != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(dataRow["IsCommitRate"]), 5)) : string.Empty);
			dataRow2["晚交计数"] = ((dataRow["LateCommit"].ToString() == "0") ? string.Empty : dataRow["LateCommit"].ToString());
			dataRow2["晚交比率"] = ((dataRow["LateCommitRate"] != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(dataRow["LateCommitRate"]), 5)) : string.Empty);
			dataRow2["未提交计数"] = ((dataRow["NoCommit"].ToString() == "0") ? string.Empty : dataRow["NoCommit"].ToString());
			dataRow2["未提交比率"] = ((dataRow["CommitRate"] != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(dataRow["CommitRate"]), 5)) : string.Empty);
			dataRow2["月份"] = dataRow["WkpMonth"].ToString();
			dataRow2["该月按时提交"] = dataRow["MonthCommit"].ToString();
			dataRow2["该月晚交"] = dataRow["MonthLate"].ToString();
			dataRow2["该月份总比率"] = ((dataRow["MonthRate"] != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(dataRow["MonthRate"]), 5)) : string.Empty);
			dataRow2["该月前三个月"] = dataRow["DuringMonths"].ToString();
			dataRow2["按时提交"] = dataRow["TreeMonthCommit"].ToString();
			dataRow2["晚交"] = dataRow["TreeMonthLate"].ToString();
			dataRow2["该月前三个月提交比率"] = ((dataRow["TreeMonthRate"] != DBNull.Value) ? string.Format("{0:P}", decimal.Round(Convert.ToDecimal(dataRow["TreeMonthRate"]), 5)) : string.Empty);
			dataTable2.Rows.Add(dataRow2);
		}
		return dataTable2;
	}
	protected int WeekCount(DateTime monthFirstDate, DateTime monthLastDate)
	{
		int year = monthFirstDate.Year;
		int year2 = monthLastDate.Year;
		int num = 365;
		if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
		{
			num = 366;
		}
		DateTime dateTime = monthFirstDate.AddDays((double)(7 - Convert.ToInt32(monthFirstDate.DayOfWeek)));
		DateTime dateTime2 = monthLastDate.AddDays((double)(1 - Convert.ToInt32(monthLastDate.DayOfWeek)));
		return (year == year2) ? ((dateTime2.DayOfYear - dateTime.DayOfYear) / 7 + 1) : ((dateTime2.DayOfYear + (num - dateTime.DayOfYear)) / 7 + 1);
	}
	protected void gvWorkPlanList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvWorkPlanList.DataKeys[e.Row.RowIndex]["Num"].ToString();
		}
	}
}


