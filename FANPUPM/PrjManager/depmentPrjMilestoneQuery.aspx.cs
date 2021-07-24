using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_depmentPrjMilestoneQuery : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindPrjInfo();
		}
	}
	protected void BindPrjInfo()
	{
		string where = this.strwhere();
		this.AspNetPager1.RecordCount = this.prjMileSer.getdepmentCount(where);
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
		DataTable dataTable = this.prjMileSer.GetdepmentprjMilestone("", where, this.AspNetPager1.RecordCount, 1);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			d += System.Convert.ToDecimal(dataTable.Rows[i]["StoreAmount"]);
			d2 += System.Convert.ToDecimal(dataTable.Rows[i]["ForeCast"]);
			d3 += System.Convert.ToDecimal(dataTable.Rows[i]["NextForeCast"]);
			num += System.Convert.ToInt32(dataTable.Rows[i]["Stone1"]);
			num2 += System.Convert.ToInt32(dataTable.Rows[i]["Stone2"]);
			num3 += System.Convert.ToInt32(dataTable.Rows[i]["Stone3"]);
			num4 += System.Convert.ToInt32(dataTable.Rows[i]["Stone4"]);
			num5 += System.Convert.ToInt32(dataTable.Rows[i]["Stone5"]);
			num6 += System.Convert.ToInt32(dataTable.Rows[i]["Stone6"]);
			num7 += System.Convert.ToInt32(dataTable.Rows[i]["Stone7"]);
			num8 += System.Convert.ToInt32(dataTable.Rows[i]["Stone8"]);
			num9 += System.Convert.ToInt32(dataTable.Rows[i]["Stone9"]);
		}
		this.hfldForeCast.Value = d2.ToString();
		this.hfldStoreAmount.Value = d.ToString();
		this.hfldNextForeCast.Value = d3.ToString();
		this.hfldStone1.Value = num.ToString();
		this.hfldStone2.Value = num2.ToString();
		this.hfldStone3.Value = num3.ToString();
		this.hfldStone4.Value = num4.ToString();
		this.hfldStone5.Value = num5.ToString();
		this.hfldStone6.Value = num6.ToString();
		this.hfldStone7.Value = num7.ToString();
		this.hfldStone8.Value = num8.ToString();
		this.hfldStone9.Value = num9.ToString();
		DataTable dataSource = this.prjMileSer.GetdepmentprjMilestone("", where, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.depmentPrjMilestoneQuery.DataSource = dataSource;
		this.depmentPrjMilestoneQuery.DataBind();
	}
	protected string strwhere()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		string text = this.txtdepmentName.Text.Trim();
		if (!string.IsNullOrEmpty(text))
		{
			stringBuilder.AppendFormat(" AND v_BMMC LIKE'%{0}%' ", text);
		}
		return stringBuilder.ToString();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPrjInfo();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindPrjInfo();
	}
	public string FormatRate(object ForeCast, object StoreAmount)
	{
		string result;
		try
		{
			result = Common2.FormatRate(System.Convert.ToDecimal(ForeCast) / System.Convert.ToDecimal(StoreAmount));
		}
		catch
		{
			result = "0.00%";
		}
		return result;
	}
	protected void depmentPrjMilestoneQuery_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.depmentPrjMilestoneQuery.DataKeys[e.Row.RowIndex]["NUM"].ToString();
			e.Row.Attributes["id"] = value;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			e.Row.Cells[2].Text = this.hfldStoreAmount.Value;
			e.Row.Cells[2].CssClass = "decimal_input";
			e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[3].Text = this.hfldForeCast.Value;
			e.Row.Cells[3].CssClass = "decimal_input";
			e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
			if (System.Convert.ToDecimal(this.hfldStoreAmount.Value) != 0m)
			{
				e.Row.Cells[4].Text = Common2.FormatRate(decimal.Divide(System.Convert.ToDecimal(this.hfldForeCast.Value), System.Convert.ToDecimal(this.hfldStoreAmount.Value))).ToString();
				e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
			}
			e.Row.Cells[5].Text = this.hfldNextForeCast.Value;
			e.Row.Cells[5].CssClass = "decimal_input";
			e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
			e.Row.Cells[6].Text = this.hfldStone1.Value;
			e.Row.Cells[7].Text = this.hfldStone2.Value;
			e.Row.Cells[8].Text = this.hfldStone3.Value;
			e.Row.Cells[9].Text = this.hfldStone4.Value;
			e.Row.Cells[10].Text = this.hfldStone5.Value;
			e.Row.Cells[11].Text = this.hfldStone6.Value;
			e.Row.Cells[12].Text = this.hfldStone7.Value;
			e.Row.Cells[13].Text = this.hfldStone8.Value;
			e.Row.Cells[14].Text = this.hfldStone9.Value;
			e.Row.Cells[15].Text = (System.Convert.ToInt32(this.hfldStone1.Value) + System.Convert.ToInt32(this.hfldStone2.Value) + System.Convert.ToInt32(this.hfldStone3.Value) + System.Convert.ToInt32(this.hfldStone4.Value) + System.Convert.ToInt32(this.hfldStone5.Value) + System.Convert.ToInt32(this.hfldStone6.Value) + System.Convert.ToInt32(this.hfldStone7.Value) + System.Convert.ToInt32(this.hfldStone8.Value) + System.Convert.ToInt32(this.hfldStone9.Value)).ToString();
		}
	}
	protected void btnImput_Click(object sender, System.EventArgs e)
	{
		ExcelExporter excelExporter = new ExcelExporter();
		System.Collections.Generic.List<DataTable> data = this.GetData();
		HttpContext current = HttpContext.Current;
		excelExporter.ExportAll(data, current, "部门汇总.xls");
	}
	public System.Collections.Generic.List<DataTable> GetData()
	{
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		DataTable dataTable = this.prjMileSer.GetdepmentprjMilestone("", "", this.AspNetPager1.RecordCount, 1);
		DataTable dataTable2 = new DataTable();
		dataTable2.Columns.Add("序号");
		dataTable2.Columns.Add("部门名称");
		dataTable2.Columns.Add("项目储备额");
		dataTable2.Columns.Add("当年预测");
		dataTable2.Columns.Add("储备转换率");
		dataTable2.Columns.Add("明年预测");
		dataTable2.Columns.Add("初步接洽");
		dataTable2.Columns.Add("提供样品");
		dataTable2.Columns.Add("样品质量被接纳");
		dataTable2.Columns.Add("投标");
		dataTable2.Columns.Add("中标");
		dataTable2.Columns.Add("下订单");
		dataTable2.Columns.Add("交货");
		dataTable2.Columns.Add("销售确认");
		dataTable2.Columns.Add("项目失败");
		dataTable2.Columns.Add("工程总数");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataRow dataRow = dataTable2.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["部门名称"] = dataTable.Rows[i][1].ToString();
			dataRow["项目储备额"] = dataTable.Rows[i][2].ToString();
			dataRow["当年预测"] = dataTable.Rows[i][3].ToString();
			if (System.Convert.ToDecimal(dataTable.Rows[i][2]) != 0m)
			{
				dataRow["储备转换率"] = Common2.FormatRate(System.Convert.ToDecimal(dataTable.Rows[i][3]) / System.Convert.ToDecimal(dataTable.Rows[i][2]));
			}
			else
			{
				dataRow["储备转换率"] = Common2.FormatRate("0");
			}
			dataRow["明年预测"] = dataTable.Rows[i][4].ToString();
			dataRow["初步接洽"] = dataTable.Rows[i][5].ToString();
			dataRow["提供样品"] = dataTable.Rows[i][6].ToString();
			dataRow["样品质量被接纳"] = dataTable.Rows[i][7].ToString();
			dataRow["投标"] = dataTable.Rows[i][8].ToString();
			dataRow["中标"] = dataTable.Rows[i][9].ToString();
			dataRow["下订单"] = dataTable.Rows[i][10].ToString();
			dataRow["交货"] = dataTable.Rows[i][11].ToString();
			dataRow["销售确认"] = dataTable.Rows[i][12].ToString();
			dataRow["项目失败"] = dataTable.Rows[i][13].ToString();
			dataRow["工程总数"] = System.Convert.ToInt32(dataTable.Rows[i][13]) + System.Convert.ToInt32(dataTable.Rows[i][5]) + System.Convert.ToInt32(dataTable.Rows[i][6]) + System.Convert.ToInt32(dataTable.Rows[i][7]) + System.Convert.ToInt32(dataTable.Rows[i][8]) + System.Convert.ToInt32(dataTable.Rows[i][9]) + System.Convert.ToInt32(dataTable.Rows[i][10]) + System.Convert.ToInt32(dataTable.Rows[i][11]) + System.Convert.ToInt32(dataTable.Rows[i][12]);
			dataTable2.Rows.Add(dataRow);
		}
		string[] values = new string[]
		{
			"合计",
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
		dataTable2.Rows.Add(values);
		list.Add(dataTable2);
		return list;
	}
}


