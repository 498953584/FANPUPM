using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Steema.TeeChart.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Cry_graph_ProjectPayoffAnalyse : PageBase, IRequiresSessionState
{
	public System.Guid ProjectCode;
	public DataTable dt;
	public AccountReportAction arAction
	{
		get
		{
			return new AccountReportAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.webchart.Chart.Header.Text = "项目赢得值分析图";
			this.ddlBind();
			this.drawingTeeChart();
		}
	}
	public void ddlBind()
	{
		CryReport cryReport = new CryReport();
		this.ddlPrjname.DataSource = cryReport.GetWorkCostpcList();
		this.ddlPrjname.DataTextField = "pn";
		this.ddlPrjname.DataValueField = "pc";
		this.ddlPrjname.DataBind();
	}
	protected void ddlPrjname_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.webchart.Chart.Header.Text = "项目赢得值分析图";
		this.dt = ProductValueAction.EarnValueAnalyse(this.ddlPrjname.SelectedValue.ToString());
		this.drawingTeeChart();
	}
	public void drawingTeeChart()
	{
		this.dt = ProductValueAction.EarnValueAnalyse(this.ddlPrjname.SelectedValue.ToString());
		if (this.dt.Rows.Count > 5)
		{
			int arg_3E_0 = this.dt.Rows.Count;
		}
		this.webchart.Chart.Series[0].DataSource = this.dt;
		this.webchart.Chart.Series[1].DataSource = this.dt;
		this.webchart.Chart.Series[2].DataSource = this.dt;
		if (this.dt == null || this.dt.Rows.Count == 0)
		{
			this.webchart.Chart.Series[0].Add(System.DateTime.Parse(System.DateTime.Now.Year.ToString() + "-" + (int.Parse(System.DateTime.Now.Month.ToString()) - 1).ToString()), double.Parse("0"));
			this.webchart.Chart.Series[0].Add(System.DateTime.Parse(System.DateTime.Now.ToShortDateString()), double.Parse("0"));
		}
		for (int i = 0; i < this.dt.Rows.Count - 1; i++)
		{
			this.webchart.Chart.Series[0].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["BudegetCost"].ToString()));
			this.webchart.Chart.Series[0].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["BudegetCost"].ToString()));
			this.webchart.Chart.Series[1].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["PlanCost"].ToString()));
			this.webchart.Chart.Series[1].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["PlanCost"].ToString()));
			this.webchart.Chart.Series[1].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["FactCost"].ToString()));
			this.webchart.Chart.Series[1].Add(System.DateTime.Parse(this.dt.Rows[i]["DatePoint"].ToString()), double.Parse(this.dt.Rows[i]["FactCost"].ToString()));
		}
		this.gv.DataSource = this.dt;
		this.gv.DataBind();
	}
}


