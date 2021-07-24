using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ItemProgReport : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BindGridData();
			}
		}
		private void BindGridData()
		{
			DateTime dateTime = DateTime.Parse((this.txtBeginDate.Text.Trim() == "") ? "1900-1-1" : this.txtBeginDate.Text);
			DateTime dateTime2 = DateTime.Parse((this.txtEndDate.Text.Trim() == "") ? "6000-1-1" : this.txtEndDate.Text);
			string sqlString = string.Format("WITH cte_ItemProg AS(\r\n\t               SELECT PS.ProgSortName, P.PrjName,IP.ProgMoney\r\n\t               FROM Prj_ItemProg AS IP\r\n\t               JOIN PT_PrjInfo AS P ON IP.PrjCode = P.PrjGuid\r\n\t               JOIN Prj_ProgSort AS PS ON IP.ProgSortCode = PS.ProgSortCode\r\n\t               WHERE IP.FlowState = 1 AND IP.ProgSign=0 and  ProgDate>'{0}' and ProgDate<'{1}'\r\n                    ), cte_ItemProg2 AS\r\n                   ( SELECT ProgSortName, ISNULL(PrjName, '小计') AS PrjName,SUM(ProgMoney) ProgMoney\r\n\t               FROM  cte_ItemProg\r\n\t               GROUP BY ProgSortName,PrjName\r\n\t               WITH ROLLUP)SELECT * FROM cte_ItemProg2 WHERE ProgSortName IS NOT NULL", dateTime, dateTime2);
			DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
			this.tbReport.DataSource = dataSource;
			this.tbReport.DataBind();
			this.GroupRows(this.tbReport);
		}
		private void GroupRows(GridView tbReport)
		{
			for (int i = 0; i < tbReport.Rows.Count; i++)
			{
				if (tbReport.Rows[i].Cells[1].Text == "小计")
				{
					TableCell tableCell = tbReport.Rows[i].Cells[0];
					TableCell tableCell2 = tbReport.Rows[i].Cells[1];
					tableCell.Visible = false;
					tableCell2.ColumnSpan = 2;
					tableCell2.ApplyStyle(new Style
					{
						CssClass = "text",
						ForeColor = Color.Red
					});
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.BindGridData();
		}
		protected void tbReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.tbReport.PageIndex = e.NewPageIndex;
			this.BindGridData();
		}
	}


