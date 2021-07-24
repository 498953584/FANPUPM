using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Analysis_Analysis : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ShowColumn();
			this.UpdateAttrib();
		}
	}
	public void ShowColumn()
	{
		DataTable analysisSource = Progress.GetAnalysisSource();
		int num = 30 * analysisSource.Rows.Count;
		if (num <= 800)
		{
			this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 30.0;
			num = 800;
		}
		else
		{
			this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (double)(analysisSource.Rows.Count + 20);
		}
		this.Chart1.Width = num;
		for (int i = 0; i < analysisSource.Rows.Count; i++)
		{
			this.Chart1.Series["Default"].Points.AddXY(analysisSource.Rows[i]["PrjName"].ToString(), new object[]
			{
				double.Parse(analysisSource.Rows[i]["PERCENTCOMPLETE_"].ToString()) / 100.0
			});
		}
		this.gvProgress.Width = num;
		this.gvProgress.DataSource = analysisSource;
		this.gvProgress.DataBind();
	}
	public void UpdateAttrib()
	{
		foreach (Series current in this.Chart1.Series)
		{
			for (int i = 0; i < current.Points.Count; i++)
			{
				string text = current.Points[i].AxisLabel + "的进度完成量为" + (current.Points[i].YValues[0] * 100.0).ToString() + "%";
				current.Points[i].Url = "#";
				current.Points[i].MapAreaAttributes = string.Concat(new string[]
				{
					"href=\"\" onmouseover=\" DisplayTooltip('",
					text,
					"');\" onmouseout=\"DisplayTooltip('');\" onclick=\"query('",
					current.Points[i].AxisLabel,
					"');\""
				});
			}
		}
	}
	protected void gvProgress_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
}


