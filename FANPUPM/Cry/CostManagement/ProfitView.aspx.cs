using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.web;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProfitView : PageBase, IRequiresSessionState
	{
		private int _RowCount = 5;
		private Guid _ProjectCode = Guid.Empty;
		public string ProjectName = "";

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["pc"] == null || base.Request["pn"] == null)
				{
					this.js.Text = "alert('参数错误!');";
					return;
				}
				this._ProjectCode = new Guid(base.Request["pc"]);
				this.ProjectName = base.Request["pn"];
				DataTable dataTable = new DataTable();
				dataTable = ProductValueAction.EarnValueAnalyse(this._ProjectCode.ToString());
				this._RowCount = ((dataTable.Rows.Count > 5) ? dataTable.Rows.Count : 5);
				this.tblData_Draw(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					this.InitData(dataTable);
				}
			}
		}
		private void InitData(DataTable dt)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (i == dt.Rows.Count - 1)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][3].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', false, '计划预算费用');"
					}));
					stringBuilder2.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][4].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', false, '完成预算费用');"
					}));
					stringBuilder3.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][5].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', false, '实际完成费用');"
					}));
				}
				else
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][3].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', true, '计划预算费用');"
					}));
					stringBuilder2.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][4].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', true, '完成预算费用');"
					}));
					stringBuilder3.Append(string.Concat(new string[]
					{
						"SvgFile.addChartValue(",
						dt.Rows[i][5].ToString(),
						", '",
						dt.Rows[i][0].ToString().Substring(2),
						"', true, '实际完成费用');"
					}));
				}
			}
			StringBuilder stringBuilder4 = new StringBuilder();
			stringBuilder4.Append("\t<script language=\"Javascript\">");
			stringBuilder4.Append("\t\tfunction CreateChart(){");
			stringBuilder4.Append("try{");
			stringBuilder4.Append(stringBuilder.ToString());
			stringBuilder4.Append(stringBuilder2.ToString());
			stringBuilder4.Append(stringBuilder3.ToString());
			stringBuilder4.Append("\t\tSvgFile.createEnd();");
			stringBuilder4.Append("\t\tSvgFile.setAxes('单位：元','月份');");
			stringBuilder4.Append("}");
			stringBuilder4.Append("catch(e){GetSVGViewer();}");
			stringBuilder4.Append("}");
			stringBuilder4.Append("function GetSVGViewer(){");
			stringBuilder4.Append("\tif(confirm(\"要浏览本页图形你的浏览器需要安装SVG Viewer插件，您是否现在安装？\")){");
			stringBuilder4.Append("\t\tdocument.location = \"/aspnet_client/CX/Chart1_0/SVGView3_0.exe\";}");
			stringBuilder4.Append("\telse{");
			stringBuilder4.Append("var htmlContent = \"<hr style='height:1px;'>对不起，您目前的浏览器不支持SVG,要浏览图形需要安装Svg Viewer插件。<br><a href='/aspnet_client/CX/Chart1_0/SVGView3_0.exe'><b>下载插件</b></a>，如果需要更多关于Svg的信息，<a target='_blank' href='http://www.chinese-s.adobe.com/enterprise/svg.html'><b>了解信息</b></a>。<hr style='height:1px;'>\";");
			stringBuilder4.Append("document.getElementById(\"CXChart1_0_NoSVGViewer\").innerHTML = htmlContent;");
			stringBuilder4.Append("document.getElementById(\"CXChart1_0_NoSVGViewer\").style.display = \"\";}");
			stringBuilder4.Append("}");
			stringBuilder4.Append("document.body.onload = CreateChart;");
			stringBuilder4.Append("</script>");
			this.Page.RegisterStartupScript("chart", stringBuilder4.ToString());
		}
		private void tblData_Draw(DataTable dt)
		{
			for (int i = 0; i < 4; i++)
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				switch (i)
				{
				case 0:
					tableCell.Text = "&nbsp;";
					break;
				case 1:
					tableCell.Text = "计划预算费用";
					break;
				case 2:
					tableCell.Text = "完成预算费用";
					break;
				case 3:
					tableCell.Text = "实际完成费用";
					break;
				}
				tableCell.Width = Unit.Pixel(90);
				tableCell.Wrap = false;
				tableRow.Cells.Add(tableCell);
				for (int j = 0; j < dt.Rows.Count; j++)
				{
					tableCell = new TableCell();
					if (i == 0)
					{
						tableCell.Attributes["align"] = "center";
						tableCell.Text = dt.Rows[j][i].ToString();
					}
					else
					{
						tableCell.Attributes["align"] = "right";
						tableCell.Text = dt.Rows[j][i + 2].ToString();
					}
					tableCell.Wrap = false;
					tableRow.Cells.Add(tableCell);
				}
				tableRow.Height = Unit.Pixel(22);
				if (i == 0)
				{
					tableRow.CssClass = "grid_head";
				}
				this.tblData.Rows.Add(tableRow);
				this.tblData.Style["border-collapse"] = "collapse";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
	}


