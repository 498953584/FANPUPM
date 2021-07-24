using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_PrjSummary : NBasePage, IRequiresSessionState
{
	private ConvertChineseNum conChineseNum = new ConvertChineseNum();
	private Common2 cm = new Common2();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string prjName = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			if (byId != null)
			{
				this.prjName = byId.PrjName;
			}
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
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldYear.Value = this.year;
			this.txtDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.hfldDate.Value = this.txtDate.Text.Trim();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
			this.hfldPrjName.Value = this.prjName;
		}
	}
	protected void BindGv()
	{
		DataTable prjSummary = EReport.GetPrjSummary(this.prjId, 1, System.Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyy-MM-dd"), this.hfldIsWBSRelevance.Value);
		this.gvConstruct.DataSource = this.formatData(prjSummary, false);
		this.gvConstruct.DataBind();
		this.hfldMonth.Value = System.Convert.ToDateTime(this.txtDate.Text.Trim()).Month.ToString();
		this.hfldDate.Value = this.txtDate.Text.Trim();
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["budLevel"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["taskName"] = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable prjSummary = EReport.GetPrjSummary(this.prjId, 1, System.Convert.ToDateTime(this.hfldDate.Value.Trim()).ToString("yyyy-MM-dd"), this.hfldIsWBSRelevance.Value);
		int month = System.Convert.ToDateTime(this.hfldDate.Value.Trim()).Month;
		string text = this.prjName + "工程" + month.ToString() + "月工程汇总表";
		ExcelHelper.ExportExcel(this.formatData(prjSummary, true), text, text, text + ".xls", null, null, 0, base.Request.Browser.Browser);
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected DataTable formatData(DataTable dt, bool IsExport)
	{
		int num = 0;
		int num2 = 0;
		foreach (DataRow dataRow in dt.Rows)
		{
			if (dataRow["BudLevel"].ToString() == "1")
			{
				dataRow["Num"] = this.conChineseNum.Convert(dataRow["Num"].ToString());
				num2 = 0;
				num++;
			}
			else
			{
				dataRow["Num"] = num2.ToString();
			}
			num2++;
		}
		if (dt.Rows.Count > 0)
		{
			DataRow dataRow2 = dt.NewRow();
			dataRow2["Num"] = this.conChineseNum.Convert((num + 1).ToString());
			dataRow2["TaskId"] = "合计";
			dataRow2["TaskName"] = "合计";
			dataRow2["Total"] = System.Convert.ToDecimal(dt.Compute("sum(Total)", "BudLevel=1")).ToString("0.000");
			dataRow2["LastMonthConsTotal"] = System.Convert.ToDecimal(dt.Compute("sum(LastMonthConsTotal)", "BudLevel=1")).ToString("0.000");
			dataRow2["CurrentMonthConsTotal"] = System.Convert.ToDecimal(dt.Compute("sum(CurrentMonthConsTotal)", "BudLevel=1")).ToString("0.000");
			dataRow2["SumCons"] = System.Convert.ToDecimal(dt.Compute("sum(SumCons)", "BudLevel=1")).ToString("0.000");
			dt.Rows.Add(dataRow2);
		}
		if (IsExport)
		{
			if (dt.Columns["Num"] != null)
			{
				dt.Columns["Num"].ColumnName = "序号";
			}
			if (dt.Columns["TaskName"] != null)
			{
				dt.Columns["TaskName"].ColumnName = "工程项目名称";
			}
			if (dt.Columns["Total"] != null)
			{
				dt.Columns["Total"].ColumnName = "清单金额";
			}
			if (dt.Columns["LastMonthConsTotal"] != null)
			{
				dt.Columns["LastMonthConsTotal"].ColumnName = "上期末完成金额";
			}
			if (dt.Columns["CurrentMonthConsTotal"] != null)
			{
				dt.Columns["CurrentMonthConsTotal"].ColumnName = "本月完成金额";
			}
			if (dt.Columns["SumCons"] != null)
			{
				dt.Columns["SumCons"].ColumnName = "本期末完成金额";
			}
			if (dt.Columns["TaskId"] != null)
			{
				dt.Columns.Remove(dt.Columns["TaskId"]);
			}
			if (dt.Columns["OrderNumber"] != null)
			{
				dt.Columns.Remove(dt.Columns["OrderNumber"]);
			}
			if (dt.Columns["TaskCode"] != null)
			{
				dt.Columns.Remove(dt.Columns["TaskCode"]);
			}
			if (dt.Columns["BudLevel"] != null)
			{
				dt.Columns.Remove(dt.Columns["BudLevel"]);
			}
			dt.AcceptChanges();
		}
		return dt;
	}
}


