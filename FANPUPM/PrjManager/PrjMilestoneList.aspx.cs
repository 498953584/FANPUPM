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
public partial class PrjManager_PrjMilestoneList : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();
	private PrjMilestoneDetailService prjMilestoneDetailSer = new PrjMilestoneDetailService();

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
		int num10 = 0;
		IQueryable<PrjMilestone> queryable = this.QueryAble();
		this.AspNetPager1.RecordCount = queryable.Count<PrjMilestone>();
		foreach (PrjMilestone current in queryable)
		{
			d += current.StoreAmount;
			d2 += current.ForeCast;
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
			num10 += this.GetPrjCount(current.Id);
		}
		this.hfldForeCast.Value = d2.ToString();
		this.hfldStoreAmount.Value = d.ToString();
		this.hfldNextForeCast.Value = d3.ToString();
		this.hfldPrjCount.Value = num10.ToString();
		this.hfldStone1.Value = num.ToString();
		this.hfldStone2.Value = num2.ToString();
		this.hfldStone3.Value = num3.ToString();
		this.hfldStone4.Value = num4.ToString();
		this.hfldStone5.Value = num5.ToString();
		this.hfldStone6.Value = num6.ToString();
		this.hfldStone7.Value = num7.ToString();
		this.hfldStone8.Value = num8.ToString();
		this.hfldStone9.Value = num9.ToString();
		queryable =
			from p in queryable
			orderby p.RptDate descending
			select p;
		queryable = queryable.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvPrjMilestone.DataSource = queryable;
		this.gvPrjMilestone.DataBind();
	}
	protected IQueryable<PrjMilestone> QueryAble()
	{
		string userName = this.txtuserName.Text.Trim();
		string rptDate = this.txtRptDate.Text.Trim();
		IQueryable<PrjMilestone> queryable = this.prjMileSer.AsQueryable<PrjMilestone>();
		if (!string.IsNullOrEmpty(userName))
		{
			queryable =
				from p in queryable
				where p.YHProjectManager.v_xm.Contains(userName)
				select p;
		}
		if (!string.IsNullOrEmpty(rptDate))
		{
			queryable =
				from p in queryable
				where p.RptDate == System.Convert.ToDateTime(rptDate)
				select p;
		}
		return queryable;
	}
	protected void gvPrjMilestone_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvPrjMilestone.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
		}
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
	protected int GetPrjCount(string Id)
	{
		int result = 0;
		if (!string.IsNullOrEmpty(Id))
		{
			PrjMilestone byId = this.prjMileSer.GetById(Id);
			result = byId.Stone1 + byId.Stone2 + byId.Stone3 + byId.Stone4 + byId.Stone5 + byId.Stone6 + byId.Stone7 + byId.Stone8 + byId.Stone9;
		}
		return result;
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
		excelExporter.ExportAll(data, current, "个人明细.xls");
	}
	public System.Collections.Generic.List<DataTable> GetData()
	{
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		DataTable perList = this.prjMilestoneDetailSer.GetPerList();
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目负责人");
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
		for (int i = 0; i < perList.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["项目负责人"] = perList.Rows[i][0].ToString();
			dataRow["上报时间"] = this.GetRptDate(perList.Rows[i][1].ToString());
			dataRow["项目储备额"] = perList.Rows[i][2].ToString();
			dataRow["当年预测"] = perList.Rows[i][3].ToString();
			dataRow["储备转换率"] = Common2.FormatRate(perList.Rows[i][4].ToString());
			dataRow["明年预测"] = perList.Rows[i][5].ToString();
			dataRow["初步接洽"] = perList.Rows[i][6].ToString();
			dataRow["提供样品"] = perList.Rows[i][7].ToString();
			dataRow["样品质量被接纳"] = perList.Rows[i][8].ToString();
			dataRow["投标"] = perList.Rows[i][9].ToString();
			dataRow["中标"] = perList.Rows[i][10].ToString();
			dataRow["下订单"] = perList.Rows[i][11].ToString();
			dataRow["交货"] = perList.Rows[i][12].ToString();
			dataRow["销售确认"] = perList.Rows[i][13].ToString();
			dataRow["项目失败"] = perList.Rows[i][14].ToString();
			dataRow["工程总数"] = perList.Rows[i][15].ToString();
			dataTable.Rows.Add(dataRow);
		}
		list.Add(dataTable);
		return list;
	}
}


