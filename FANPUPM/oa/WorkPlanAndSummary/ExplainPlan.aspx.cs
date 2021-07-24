using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkPlanAndSummary_ExplainPlan : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			MainPlan model = new MainPlanAndAction().GetModel(new Guid(base.Request.QueryString["wkpid"].ToString()));
			this.lblCode.Text = model.WkpUserCode;
			this.lblChecker.Text = this.GetNames(model);
			this.lblDate.Text = model.WkpRecordDate.ToShortDateString();
			this.lblPart.Text = new MainPlanAndAction().BackDept(model.WkpDeptId);
			this.lblReportName.Text = new MainPlanAndAction().BackUserName(model.WkpReportUser);
			string sqlString = "select * from pm_workplan_plansummary where wkpid='" + new Guid(base.Request.QueryString["wkpid"].ToString()) + "'";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				this.lblPlanSumm.Text = dataTable.Rows[0]["wkpremarks"].ToString();
				this.lblScore.Text = dataTable.Rows[0]["wkpselfscore"].ToString();
				this.lblSumm.Text = dataTable.Rows[0]["wkpsummary"].ToString();
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
		string sqlString = "select * from pm_workplan_weekplandetails where wkpid='" + new Guid(base.Request.QueryString["wkpid"].ToString()) + "'";
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.dlData.DataSource = dataSource;
		this.dlData.DataBind();
	}
	protected void dlData_ItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			string sqlString = "select *from pm_workplan_weeksummary where wkpdetailsId='" + new Guid(dataRowView["wkpdetailsid"].ToString()) + "'";
			DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
			DataList dataList = (DataList)e.Item.FindControl("dlSumm");
			dataList.DataSource = dataSource;
			dataList.DataBind();
		}
	}
	protected string GetNames(MainPlan mplan)
	{
		if (mplan.WkpCheckerUser != "")
		{
			string[] array = mplan.WkpCheckerUser.Split(new char[]
			{
				','
			});
			string text = "";
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (array[i] != "")
				{
					text = text + new MainPlanAndAction().BackUserName(array[i]) + ",";
				}
			}
			return text;
		}
		return "";
	}
}


