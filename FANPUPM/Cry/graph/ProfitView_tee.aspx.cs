using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using Steema.TeeChart.Tools;
using Steema.TeeChart.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Cry_graph_ProfitView : PageBase, IRequiresSessionState
{
	public System.Guid ProjectCode;
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
			Chart chart = this.webchart.Chart;
			chart.Aspect.View3D = true;
			Bubble bubble = new Bubble(chart);
			bubble.FillSampleValues();
			SeriesHotspot seriesHotspot = new SeriesHotspot(chart);
			seriesHotspot.MapAction = MapAction.Mark;
			this.ddlBind();
			this.DrawBar();
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
	private void DrawBar()
	{
		this.webchart.Chart.Header.Text = "工程成本消耗比例";
		this.webchart.Dispose();
		Bar bar = new Bar(this.webchart.Chart);
		bar.ColorEach = true;
		System.DateTime dtStartDate = System.Convert.ToDateTime(System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-1");
		System.DateTime dtEndDate = System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
		if (this.ddlPrjname.Items.Count != 0)
		{
			this.ProjectCode = new System.Guid(this.ddlPrjname.SelectedValue);
		}
		DataTable monthDirectCostProportion = this.arAction.GetMonthDirectCostProportion(this.ProjectCode, dtStartDate, dtEndDate);
		if (monthDirectCostProportion != null)
		{
			bar.DataSource = monthDirectCostProportion;
			bar.YValues.DataMember = monthDirectCostProportion.Columns[2].ToString();
			bar.LabelMember = monthDirectCostProportion.Columns[1].ToString();
			this.webchart.DataBind();
			this.gv.DataSource = monthDirectCostProportion;
			this.gv.DataBind();
		}
	}
	protected void ddlPrjname_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.DrawBar();
	}
}


