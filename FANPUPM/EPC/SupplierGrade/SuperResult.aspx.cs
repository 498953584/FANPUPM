using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_SupplierGrade_SuperResult : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvbind(0);
		}
	}
	protected void btnOK_Click(object sender, EventArgs e)
	{
		if (this.radls.Checked)
		{
			this.gvbind(0);
			return;
		}
		int y = int.Parse(this.dply.SelectedItem.Text.ToString().Trim());
		this.gvbind(y);
	}
	public void gvbind(int y)
	{
		string sqlString = " select a.superid,count(a.id) as khnum,sum(a.fileisall+a.numisover+a.lookisgood+a.tpyeisright+a.timeisquk+a.smallisok) AS allpr,sum(a.fileisall+a.numisover+a.lookisgood+a.tpyeisright+a.timeisquk+a.smallisok)/count(a.id) as pj,b.CorpName from Res_SuperGradeTab a  INNER JOIN XPM_Basic_ContactCorp b ON a.superid=b.CorpID   GROUP BY a.superid,b.CorpName";
		if (y != 0)
		{
			sqlString = " select a.superid,count(a.id) as khnum,sum(a.fileisall+a.numisover+a.lookisgood+a.tpyeisright+a.timeisquk+a.smallisok) AS allpr,sum(a.fileisall+a.numisover+a.lookisgood+a.tpyeisright+a.timeisquk+a.smallisok)/count(a.id) as pj,b.CorpName from Res_SuperGradeTab a  INNER JOIN XPM_Basic_ContactCorp b ON a.superid=b.CorpID  WHERE DATEPART(YEAR,gradeTime)='" + y + "'  GROUP BY a.superid,b.CorpName";
		}
		DataTable dataSource = new DataTable();
		dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.gvpf.DataSource = dataSource;
		this.gvpf.DataBind();
	}
	public void chartbind(int supid)
	{
		string sqlString = " select superid,gradeTime,sum(fileisall+numisover+lookisgood+tpyeisright+timeisquk+smallisok) AS allpr from Res_SuperGradeTab  WHERE  superid='" + supid + "' GROUP BY superid,gradeTime";
		DataTable dataTable = new DataTable();
		dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int count = dataTable.Rows.Count;
		int[] array = new int[count];
		DateTime[] array2 = new DateTime[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = int.Parse(dataTable.Rows[i]["allpr"].ToString());
			array2[i] = DateTime.Parse(dataTable.Rows[i]["gradeTime"].ToString());
		}
		this.Chart1.Width = 900;
		this.Chart1.Height = 400;
		this.Chart1.BackColor = Color.Azure;
		Series series = new Series("供应商信誉走势图");
		series.ChartType = SeriesChartType.Spline;
		series.Color = Color.Green;
		series.BorderWidth = 2;
		series.ShadowOffset = 1;
		series.IsVisibleInLegend = false;
		series.IsValueShownAsLabel = true;
		series.MarkerStyle = MarkerStyle.Diamond;
		series.MarkerSize = 10;
		DateTime arg_13B_0 = DateTime.Now.Date;
		for (int j = 0; j < array.Length; j++)
		{
			series.Points.AddXY(array2[j], new object[]
			{
				array[j]
			});
		}
		this.Chart1.Series.Add(series);
	}
	protected void btnview_Click(object sender, EventArgs e)
	{
		if (this.hidysid.Value.Trim().ToString() != "")
		{
			this.chartbind(int.Parse(this.hidysid.Value.Trim()));
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("alert('系统提示：\\n\\n请选择供应商！');").Append(Environment.NewLine);
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void gvpf_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvpf.PageIndex = e.NewPageIndex;
		this.gvbind(0);
	}
	protected void gvpf_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvpf.DataKeys[e.Row.RowIndex].Value.ToString();
			string str = ((DataRowView)e.Row.DataItem)["superid"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
		}
	}
}


