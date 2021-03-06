using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_DepYearMileStoneDetail : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();
	private PrjMilestoneDetailService PrjMilestoneDetailSer = new PrjMilestoneDetailService();
	private string name = string.Empty;
	private string month = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["name"]))
		{
			this.name = base.Request["name"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		string sWhere = " AND YEAR(RptDate)=" + System.Convert.ToInt32(this.year);
		DataTable dataSource = new DataTable();
		string value = this.txtRptDate.Text.Trim();
		if (this.year != string.Empty)
		{
			if (!string.IsNullOrEmpty(value))
			{
				sWhere = string.Concat(new object[]
				{
					" AND YEAR(RptDate)=",
					System.Convert.ToInt32(this.year),
					" AND RptDate='",
					System.Convert.ToDateTime(value),
					"'"
				});
			}
			this.AspNetPager1.RecordCount = this.PrjMilestoneDetailSer.GetDepYearCount(sWhere, this.name).Rows.Count;
			dataSource = this.PrjMilestoneDetailSer.GetDepYearDetail(sWhere, this.name, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		}
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		DataTable depYearCount = this.PrjMilestoneDetailSer.GetDepYearCount(sWhere, this.name);
		for (int i = 0; i < depYearCount.Rows.Count; i++)
		{
			d += System.Convert.ToDecimal(depYearCount.Rows[i]["ForeCast"]);
			d2 += System.Convert.ToDecimal(depYearCount.Rows[i]["StoreAmount"]);
			d3 += System.Convert.ToDecimal(depYearCount.Rows[i]["NextForeCast"]);
			num += System.Convert.ToInt32(depYearCount.Rows[i]["Stone1"]);
			num2 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone2"]);
			num3 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone3"]);
			num4 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone4"]);
			num5 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone5"]);
			num6 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone6"]);
			num7 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone7"]);
			num8 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone8"]);
			num9 += System.Convert.ToInt32(depYearCount.Rows[i]["Stone9"]);
		}
		this.hfldForeCast.Value = d.ToString();
		this.hfldStoreAmount.Value = d2.ToString();
		this.hfldNextForeCast.Value = d3.ToString();
		this.hfldPrjCount.Value = depYearCount.Rows.Count.ToString();
		this.hfldStone1.Value = num.ToString();
		this.hfldStone2.Value = num2.ToString();
		this.hfldStone3.Value = num3.ToString();
		this.hfldStone4.Value = num4.ToString();
		this.hfldStone5.Value = num5.ToString();
		this.hfldStone6.Value = num6.ToString();
		this.hfldStone7.Value = num7.ToString();
		this.hfldStone8.Value = num8.ToString();
		this.hfldStone9.Value = num9.ToString();
		this.gvPrjMilestoneDetail.DataSource = dataSource;
		this.gvPrjMilestoneDetail.DataBind();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void gvPrjMilestoneDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		new System.Collections.Generic.List<string>();
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvPrjMilestoneDetail.DataKeys[e.Row.RowIndex]["NUM"].ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	protected string GetRptDate(object obj)
	{
		string result = string.Empty;
		System.DateTime dateTime = System.Convert.ToDateTime(obj);
		if (dateTime.Day > System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day + 13)).Day)
		{
			result = string.Concat(new object[]
			{
				dateTime.Year,
				"年-",
				dateTime.Month,
				"月-下半月"
			});
		}
		else
		{
			result = string.Concat(new object[]
			{
				dateTime.Year,
				"年-",
				dateTime.Month,
				"月-上半月"
			});
		}
		return result;
	}
	protected void btnImput_Click(object sender, System.EventArgs e)
	{
		ExcelExporter excelExporter = new ExcelExporter();
		System.Collections.Generic.List<DataTable> data = this.GetData();
		HttpContext current = HttpContext.Current;
		excelExporter.ExportAll(data, current, "部门年度.xls");
	}
	public System.Collections.Generic.List<DataTable> GetData()
	{
		string sWhere = " AND YEAR(RptDate)=" + System.Convert.ToInt32(this.year);
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		DataTable depYearCount = this.PrjMilestoneDetailSer.GetDepYearCount(sWhere, this.name);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("上报时间");
		dataTable.Columns.Add("项目储备额");
		dataTable.Columns.Add("当年预测");
		dataTable.Columns.Add("储备转换率");
		dataTable.Columns.Add("明年预测");
		dataTable.Columns.Add("初步接洽");
		dataTable.Columns.Add("提供样品");
		dataTable.Columns.Add("样品质量被接纳");
		dataTable.Columns.Add("投标");
		dataTable.Columns.Add("中标");
		dataTable.Columns.Add("下订单");
		dataTable.Columns.Add("交货");
		dataTable.Columns.Add("销售确认");
		dataTable.Columns.Add("项目失败");
		dataTable.Columns.Add("工程总数");
		for (int i = 0; i < depYearCount.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["上报时间"] = depYearCount.Rows[i][1].ToString();
			dataRow["项目储备额"] = depYearCount.Rows[i][3].ToString();
			dataRow["当年预测"] = depYearCount.Rows[i][4].ToString();
			if (System.Convert.ToDecimal(depYearCount.Rows[i][3]) != 0m)
			{
				dataRow["储备转换率"] = Common2.FormatRate(System.Convert.ToDecimal(depYearCount.Rows[i][4]) / System.Convert.ToDecimal(depYearCount.Rows[i][3]));
			}
			else
			{
				dataRow["储备转换率"] = Common2.FormatRate("0");
			}
			dataRow["明年预测"] = depYearCount.Rows[i][6].ToString();
			dataRow["初步接洽"] = depYearCount.Rows[i][7].ToString();
			dataRow["提供样品"] = depYearCount.Rows[i][8].ToString();
			dataRow["样品质量被接纳"] = depYearCount.Rows[i][9].ToString();
			dataRow["投标"] = depYearCount.Rows[i][10].ToString();
			dataRow["中标"] = depYearCount.Rows[i][11].ToString();
			dataRow["下订单"] = depYearCount.Rows[i][12].ToString();
			dataRow["交货"] = depYearCount.Rows[i][13].ToString();
			dataRow["销售确认"] = depYearCount.Rows[i][14].ToString();
			dataRow["项目失败"] = depYearCount.Rows[i][15].ToString();
			dataRow["工程总数"] = System.Convert.ToInt32(depYearCount.Rows[i][13]) + System.Convert.ToInt32(depYearCount.Rows[i][14]) + System.Convert.ToInt32(depYearCount.Rows[i][15]) + System.Convert.ToInt32(depYearCount.Rows[i][7]) + System.Convert.ToInt32(depYearCount.Rows[i][8]) + System.Convert.ToInt32(depYearCount.Rows[i][9]) + System.Convert.ToInt32(depYearCount.Rows[i][10]) + System.Convert.ToInt32(depYearCount.Rows[i][11]) + System.Convert.ToInt32(depYearCount.Rows[i][12]);
			dataTable.Rows.Add(dataRow);
		}
		list.Add(dataTable);
		return list;
	}
}


