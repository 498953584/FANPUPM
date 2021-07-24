using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_CompanyPrjMilestoneQuery : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();
	private PrjMilestoneDetailService PrjMilestoneDetailSer = new PrjMilestoneDetailService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
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
		System.DateTime value = System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day));
		System.DateTime value2 = System.DateTime.Today.AddDays((double)(1 - System.DateTime.Today.Day + 14));
		System.DateTime now = System.DateTime.Now;
		System.DateTime rptTime = default(System.DateTime);
		if (now.Day < value2.Day)
		{
			rptTime = System.Convert.ToDateTime(value);
		}
		else
		{
			rptTime = System.Convert.ToDateTime(value2);
		}
		string userName = this.txtuserName.Text.Trim();
		string depName = this.txtdepmentName.Text.Trim();
		IQueryable<PrjMilestoneDetail> queryable =
			from r in this.PrjMilestoneDetailSer.AsQueryable<PrjMilestoneDetail>()
			where r.RptDate == rptTime
			select r;
		if (!string.IsNullOrEmpty(userName))
		{
			queryable =
				from p in queryable
				where p.UserName.Contains(userName)
				select p;
		}
		if (!string.IsNullOrEmpty(depName))
		{
			queryable =
				from p in queryable
				where p.DepName.Contains(depName)
				select p;
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
		foreach (PrjMilestoneDetail current in queryable)
		{
			d += current.ForeCast;
			d2 += current.StoreAmount;
			d3 += current.NextForeCast;
			num += current.Stone1;
			num2 += current.Stone2;
			num3 += current.Stone3;
			num4 += current.Stone4;
			num5 += current.Stone5;
			num6 += current.Stone6;
			num7 += current.Stone7;
			num8 += current.Stone8;
			num9 += current.Stone9;
		}
		this.hfldForeCast.Value = d.ToString();
		this.hfldStoreAmount.Value = d2.ToString();
		this.hfldNextForeCast.Value = d3.ToString();
		this.hfldPrjCount.Value = queryable.Count<PrjMilestoneDetail>().ToString();
		this.hfldStone1.Value = num.ToString();
		this.hfldStone2.Value = num2.ToString();
		this.hfldStone3.Value = num3.ToString();
		this.hfldStone4.Value = num4.ToString();
		this.hfldStone5.Value = num5.ToString();
		this.hfldStone6.Value = num6.ToString();
		this.hfldStone7.Value = num7.ToString();
		this.hfldStone8.Value = num8.ToString();
		this.hfldStone9.Value = num9.ToString();
		this.AspNetPager1.RecordCount = queryable.Count<PrjMilestoneDetail>();
		IQueryable<PrjMilestoneDetail> dataSource = queryable.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
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
			string value = this.gvPrjMilestoneDetail.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = value;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			e.Row.Cells[7].Text = this.hfldStoreAmount.Value;
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[8].Text = this.hfldForeCast.Value;
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
			if (System.Convert.ToDecimal(this.hfldStoreAmount.Value) != 0m)
			{
				e.Row.Cells[9].Text = Common2.FormatRate(decimal.Divide(System.Convert.ToDecimal(this.hfldForeCast.Value), System.Convert.ToDecimal(this.hfldStoreAmount.Value))).ToString();
				e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
			}
			e.Row.Cells[10].Text = this.hfldNextForeCast.Value;
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[11].Text = this.hfldStone1.Value;
			e.Row.Cells[12].Text = this.hfldStone2.Value;
			e.Row.Cells[13].Text = this.hfldStone3.Value;
			e.Row.Cells[14].Text = this.hfldStone4.Value;
			e.Row.Cells[15].Text = this.hfldStone5.Value;
			e.Row.Cells[16].Text = this.hfldStone6.Value;
			e.Row.Cells[17].Text = this.hfldStone7.Value;
			e.Row.Cells[18].Text = this.hfldStone8.Value;
			e.Row.Cells[19].Text = this.hfldStone9.Value;
			e.Row.Cells[20].Text = this.hfldPrjCount.Value;
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
		excelExporter.ExportAll(data, current, "公司汇总.xls");
	}
	public System.Collections.Generic.List<DataTable> GetData()
	{
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		DataTable comList = this.PrjMilestoneDetailSer.GetComList();
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("上报时间");
		dataTable.Columns.Add("部门名称");
		dataTable.Columns.Add("项目负责人");
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
		for (int i = 0; i < comList.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["项目编号"] = comList.Rows[i][1].ToString();
			dataRow["项目名称"] = comList.Rows[i][2].ToString();
			dataRow["上报时间"] = comList.Rows[i][6].ToString();
			dataRow["部门名称"] = comList.Rows[i][5].ToString();
			dataRow["项目负责人"] = comList.Rows[i][4].ToString();
			dataRow["项目储备额"] = comList.Rows[i][8].ToString();
			dataRow["当年预测"] = comList.Rows[i][9].ToString();
			dataRow["储备转换率"] = Common2.FormatRate(comList.Rows[i][10]);
			dataRow["明年预测"] = comList.Rows[i][11].ToString();
			dataRow["初步接洽"] = comList.Rows[i][12].ToString();
			dataRow["提供样品"] = comList.Rows[i][13].ToString();
			dataRow["样品质量被接纳"] = comList.Rows[i][14].ToString();
			dataRow["投标"] = comList.Rows[i][15].ToString();
			dataRow["中标"] = comList.Rows[i][16].ToString();
			dataRow["下订单"] = comList.Rows[i][17].ToString();
			dataRow["交货"] = comList.Rows[i][18].ToString();
			dataRow["销售确认"] = comList.Rows[i][19].ToString();
			dataRow["项目失败"] = comList.Rows[i][20].ToString();
			dataRow["工程总数"] = System.Convert.ToInt32(comList.Rows[i][13]) + System.Convert.ToInt32(comList.Rows[i][14]) + System.Convert.ToInt32(comList.Rows[i][15]) + System.Convert.ToInt32(comList.Rows[i][16]) + System.Convert.ToInt32(comList.Rows[i][17]) + System.Convert.ToInt32(comList.Rows[i][18]) + System.Convert.ToInt32(comList.Rows[i][19]) + System.Convert.ToInt32(comList.Rows[i][20]) + System.Convert.ToInt32(comList.Rows[i][12]);
			dataTable.Rows.Add(dataRow);
		}
		string[] values = new string[]
		{
			"合计",
			"",
			"",
			"",
			"",
			"",
			this.hfldStoreAmount.Value,
			this.hfldForeCast.Value,
			Common2.FormatRate(decimal.Divide(System.Convert.ToDecimal(this.hfldForeCast.Value), System.Convert.ToDecimal(this.hfldStoreAmount.Value))).ToString(),
			this.hfldNextForeCast.Value,
			this.hfldStone1.Value,
			this.hfldStone2.Value,
			this.hfldStone3.Value,
			this.hfldStone4.Value,
			this.hfldStone5.Value,
			this.hfldStone6.Value,
			this.hfldStone7.Value,
			this.hfldStone8.Value,
			this.hfldStone9.Value,
			(System.Convert.ToInt32(this.hfldStone1.Value) + System.Convert.ToInt32(this.hfldStone2.Value) + System.Convert.ToInt32(this.hfldStone3.Value) + System.Convert.ToInt32(this.hfldStone4.Value) + System.Convert.ToInt32(this.hfldStone5.Value) + System.Convert.ToInt32(this.hfldStone6.Value) + System.Convert.ToInt32(this.hfldStone7.Value) + System.Convert.ToInt32(this.hfldStone8.Value) + System.Convert.ToInt32(this.hfldStone9.Value)).ToString()
		};
		dataTable.Rows.Add(values);
		list.Add(dataTable);
		return list;
	}
}


