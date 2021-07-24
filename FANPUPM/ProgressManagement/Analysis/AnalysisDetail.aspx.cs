using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Analysis_AnalysisDetail : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["region"]))
			{
				this.hfldRegion.Value = base.Request["region"].ToString();
				this.BindDrop();
				this.ShowColumn();
				this.UpdateAttrib();
			}
		}
		catch
		{
		}
	}
	public void ShowColumn()
	{
		DataTable analysisDetailSource = Progress.GetAnalysisDetailSource(base.Request["region"].ToString(), this.dropLevel.SelectedValue);
		int num = 30 * analysisDetailSource.Rows.Count;
		if (num <= 800)
		{
			this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 30.0;
			num = 800;
		}
		else
		{
			this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (double)(analysisDetailSource.Rows.Count + 20);
		}
		this.Chart1.Width = num;
		this.Chart1.Titles["headerShow"].Text = base.Request["region"].ToString();
		for (int i = 0; i < analysisDetailSource.Rows.Count; i++)
		{
			this.Chart1.Series["Default"].Points.AddXY(this.getSpace(int.Parse(analysisDetailSource.Rows[i]["loclevel"].ToString())) + analysisDetailSource.Rows[i]["NAME_"].ToString(), new object[]
			{
				double.Parse(analysisDetailSource.Rows[i]["PERCENTCOMPLETE_"].ToString()) / 100.0
			});
		}
	}
	public string getSpace(int n)
	{
		string text = string.Empty;
		for (int i = 0; i < n; i++)
		{
			text += " ";
		}
		return text;
	}
	public string removeSpace(string str)
	{
		if (str.Contains(" "))
		{
			int num = str.LastIndexOf(" ");
			return str.Substring(num + 3);
		}
		return str;
	}
	public void UpdateAttrib()
	{
		foreach (Series current in this.Chart1.Series)
		{
			for (int i = 0; i < current.Points.Count; i++)
			{
				string str = this.removeSpace(current.Points[i].AxisLabel) + "的进度完成量为" + (current.Points[i].YValues[0] * 100.0).ToString() + "%";
				current.Points[i].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + str + "');\" onmouseout=\"DisplayTooltip('');\" ";
			}
		}
	}
	public void BindDrop()
	{
		int maxLevel = Progress.GetMaxLevel(base.Request["region"]);
		for (int i = 1; i <= maxLevel; i++)
		{
			this.dropLevel.Items.Add(new ListItem(i.ToString() + "级", i.ToString()));
		}
		string selectedValue = ConfigurationManager.AppSettings["ProgressChartLevel"].ToString();
		this.dropLevel.SelectedValue = selectedValue;
	}
	protected void dropLevel_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.ShowColumn();
		this.UpdateAttrib();
	}
}


