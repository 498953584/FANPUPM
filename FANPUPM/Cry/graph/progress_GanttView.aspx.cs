using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Steema.TeeChart.Styles;
using Steema.TeeChart.Tools;
using Steema.TeeChart.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Cry_graph_progress_GanttView : PageBase, IRequiresSessionState
{
	protected ConstructWBSTaskAction bta
	{
		get
		{
			return new ConstructWBSTaskAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ddlBind();
			this.teeChartBind();
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
	public void teeChartBind()
	{
		Gantt gantt = (Gantt)this.gantChart.Chart.Series[0];
		gantt.Clear();
		gantt.Marks.Visible = true;
		gantt.Marks.Transparency = 45;
		SeriesHotspot seriesHotspot = new SeriesHotspot(this.gantChart.Chart);
		seriesHotspot.MapAction = MapAction.Mark;
		gantt.Active = true;
		gantt.Chart.Aspect.View3D = false;
		gantt.ColorEach = true;
		gantt.Chart.Zoom.Zoomed = true;
		gantt.Chart.Axes.Bottom.Labels.Angle = 90;
		gantt.Chart.Legend.Visible = false;
		gantt.Chart.Legend.Inverted = true;
		this.gantChart.Chart.Series.AllActive = true;
		gantt.Chart.Series.AllActive = true;
		gantt.Pointer.VertSize = 6;
		string selectedValue = this.ddlPrjname.SelectedValue;
		DataTable dataTable = this.bta.getallsubProject(selectedValue);
		int count = dataTable.Rows.Count;
		if (dataTable != null || count != 0)
		{
			for (int i = 0; i < count - 1; i++)
			{
				System.DateTime start = System.DateTime.Parse(dataTable.Rows[i]["strdata"].ToString());
				System.DateTime endDate = System.DateTime.Parse(dataTable.Rows[i]["enddata"].ToString());
				string text = dataTable.Rows[i]["TaskName"].ToString();
				gantt.Add(start, endDate, double.Parse(i.ToString()), text);
			}
		}
	}
	protected void ddlPrjname_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.teeChartBind();
	}
}


