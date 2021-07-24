using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_ExplainPlan : NBasePage, IRequiresSessionState
{
	private string wkpid = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["wkpid"]))
		{
			this.wkpid = base.Request["wkpid"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.wkpid = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(base.Request["planType"]))
			{
				string a = base.Request["planType"].ToString();
				if (a == "0")
				{
					this.ShowAudit1.BusiCode = "130";
				}
				else
				{
					this.ShowAudit1.BusiCode = "131";
				}
			}
			DataTable dataTable = PersonnelAction.QueryPersonnelById(base.UserCode);
			if (dataTable != null && dataTable.Rows.Count == 1)
			{
				this.lblBllProducer.Text = dataTable.Rows[0]["v_xm"].ToString();
			}
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			if (base.Request.QueryString["type"] == "0")
			{
				PTDbsjBll pTDbsjBll = new PTDbsjBll();
				PTDbsjModel modelByGID = pTDbsjBll.GetModelByGID(this.wkpid);
				pTDbsjBll.Delete(modelByGID.I_DBSJ_ID);
			}
			MainPlan model = new MainPlanAndAction().GetModel(new Guid(this.wkpid));
			this.lblCode.Text = model.WkpUserCode;
			this.lblDate.Text = model.WkpRecordDate.ToShortDateString();
			this.lblPart.Text = new MainPlanAndAction().BackDept(model.WkpDeptId);
			this.lblReportName.Text = new MainPlanAndAction().BackUserName(model.WkpReportUser);
			string sqlString = "select * from pm_workplan_plansummary where wkpid='" + new Guid(this.wkpid) + "'";
			DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable2.Rows.Count > 0)
			{
				this.lblPlanSumm.Text = dataTable2.Rows[0]["wkpremarks"].ToString();
				this.lblScore.Text = dataTable2.Rows[0]["wkpselfscore"].ToString();
				this.lblSumm.Text = dataTable2.Rows[0]["wkpsummary"].ToString();
			}
			else
			{
				this.lblPlanSumm.Text = "";
				this.lblScore.Text = "";
				this.lblSumm.Text = "";
			}
			this.dl_Bind();
		}
	}
	protected void dl_Bind()
	{
		string sqlString = "select 1 from pm_workplan_weeksummary where wkpid='" + new Guid(this.wkpid) + "'";
		if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
		{
			string sqlString2 = "select pm_workplan_weekplandetails.wkpdetailsid,pm_workplan_weekplandetails.wkpid,\r\n                pm_workplan_weekplandetails.wkpcontents,pm_workplan_weekplandetails.wkpstarttime,pm_workplan_weekplandetails.wkpendtime,\r\n                Pm_WorkPlan_WeekPlanDetails.wkpstandard,Pm_WorkPlan_WeekPlanDetails.wkppersons,Pm_WorkPlan_WeekPlanDetails.wkpchief,\r\n                pm_workplan_weeksummary.wkpsmcontents,pm_workplan_weeksummary.wkppercent,pm_workplan_weeksummary.wkprecorddate from \r\n                Pm_WorkPlan_WeekPlanDetails join pm_workplan_weeksummary on Pm_WorkPlan_WeekPlanDetails.wkpdetailsid=pm_workplan_weeksummary.wkpdetailsid \r\n                where Pm_WorkPlan_WeekPlanDetails.wkpid='" + new Guid(this.wkpid) + "'";
			DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString2);
			this.dlData.DataSource = dataSource;
			this.dlData.DataBind();
			return;
		}
		string sqlString3 = "select *,'' as wkpsmcontents,'' as wkppercent,'' as wkprecorddate \r\n                    from pm_workplan_weekplandetails where wkpid='" + new Guid(this.wkpid) + "'";
		DataTable dataSource2 = publicDbOpClass.DataTableQuary(sqlString3);
		this.dlData.DataSource = dataSource2;
		this.dlData.DataBind();
	}
}


