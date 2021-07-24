using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_BudGetOutPutReport : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.prjId = base.Request["prjId"];
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GetTable(15, 1);
			this.BindGvw();
		}
	}
	private void GetTable(int pageSize, int pageIndex)
	{
		DataTable budGetOutPut = EReport.GetBudGetOutPut(this.prjId, pageSize, pageIndex);
		budGetOutPut.Columns.Add("LossCoefficient", System.Type.GetType("System.Decimal"));
		budGetOutPut.Columns.Add("ResourcePrice", System.Type.GetType("System.Decimal"));
		budGetOutPut.Columns["LossCoefficient"].SetOrdinal(11);
		budGetOutPut.Columns["ResourcePrice"].SetOrdinal(12);
		DataTable dataTable = new DataTable();
		string text = string.Empty;
		foreach (DataRow dataRow in budGetOutPut.Rows)
		{
			text = text + "'" + dataRow["TaskId"].ToString() + "',";
		}
		if (text.Length >= 1)
		{
			text = text.Remove(text.Length - 1);
			dataTable = BudTask.GetTaskRes(text);
			dataTable.Columns.Add(new DataColumn("OrderNumber", System.Type.GetType("System.String")));
			foreach (DataRow dataRow2 in budGetOutPut.Rows)
			{
				int num = 1;
				foreach (DataRow dataRow3 in dataTable.Rows)
				{
					if (dataRow3["TaskId"].ToString() == dataRow2["TaskId"].ToString())
					{
						dataRow3["OrderNumber"] = dataRow2["OrderNumber"].ToString() + num.ToString();
						num++;
					}
				}
			}
			System.Collections.IEnumerator enumerator4 = dataTable.Rows.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					DataRow dataRow4 = (DataRow)enumerator4.Current;
					DataRow dataRow5 = budGetOutPut.NewRow();
					dataRow5["TaskCode"] = string.Empty;
					dataRow5["OrderNumber"] = dataRow4["OrderNumber"];
					dataRow5["TaskName"] = string.Empty;
					dataRow5["FeatureDescription"] = dataRow4["ResourceName"].ToString();
					dataRow5["TypeName"] = dataRow4["ResourceTypeName"].ToString();
					dataRow5["Unit"] = dataRow4["UnitName"].ToString();
					dataRow5["Quantity"] = dataRow4["ResourceQuantity"].ToString();
					dataRow5["ConUnitPrice"] = System.DBNull.Value;
					dataRow5["ConTotal"] = System.DBNull.Value;
					dataRow5["LossCoefficient"] = dataRow4["LossCoefficient"];
					dataRow5["ResourcePrice"] = dataRow4["ResourcePrice"];
					dataRow5["BudTotal"] = dataRow4["ResTotal"];
					dataRow5["BudUnitPrice"] = System.DBNull.Value;
					dataRow5["Profit"] = System.DBNull.Value;
					dataRow5["ProfitRate"] = string.Empty;
					budGetOutPut.Rows.Add(dataRow5);
				}
				goto IL_39C;
			}
			finally
			{
				System.IDisposable disposable4 = enumerator4 as System.IDisposable;
				if (disposable4 != null)
				{
					disposable4.Dispose();
				}
			}
		}
		dataTable = null;
		IL_39C:
		this.ViewState["dtReport"] = budGetOutPut;
	}
	private void BindGvw()
	{
		this.AspNetPager1.RecordCount = EReport.GetBudGetOutPut(this.prjId, 0, 0).Rows.Count;
		DataTable dataTable = (DataTable)this.ViewState["dtReport"];
		DataView dataView = dataTable.AsDataView();
		dataView.Sort = "OrderNumber";
		dataTable = dataView.ToTable();
		this.ViewState["dtReport"] = dataTable;
		this.gvwBudGetOutPut.DataSource = dataTable;
		this.gvwBudGetOutPut.DataBind();
	}
	protected void gvwBudGetOutPut_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataTable dataTable = (DataTable)this.ViewState["dtReport"];
			if (string.IsNullOrEmpty(dataTable.Rows[e.Row.RowIndex].ItemArray[3].ToString()))
			{
				for (int i = 3; i < e.Row.Cells.Count; i++)
				{
					e.Row.Cells[i].CssClass = "redColor";
				}
			}
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "清单编码";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "项目特征描述";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "类型";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "单位";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "工程量";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 2;
			cells[7].Text = "综合单价(元)";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "合价(元)";
			cells.Add(new TableHeaderCell());
			cells[9].ColumnSpan = 3;
			cells[9].Text = "目标成本组成";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 2;
			cells[10].Text = "成本单价(元)";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 2;
			cells[11].Text = "利润(元)";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 2;
			cells[12].Text = "利润率</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "损耗系数";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "单价(元)";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "合价(元)";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.GetTable(NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		this.GetTable(0, 0);
		DataTable dataTable = (DataTable)this.ViewState["dtReport"];
		DataView dataView = dataTable.AsDataView();
		dataView.Sort = "OrderNumber";
		dataTable = dataView.ToTable();
		int arg_44_0 = dataTable.Rows.Count;
		dataTable = this.GetTitleByTable(dataTable);
		if (dataTable.Columns.Contains("TaskId"))
		{
			dataTable.Columns.Remove("TaskId");
		}
		if (dataTable.Columns.Contains("OrderNumber"))
		{
			dataTable.Columns.Remove("OrderNumber");
		}
		dataTable.AcceptChanges();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("目标成本组成", 1, 3, 9, 0));
		new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal <= 8 || dataColumn.Ordinal > 11)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "目标成本产出", "目标成本产出", "目标成本产出.xls", list, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["No"] != null)
		{
			dt.Columns["No"].ColumnName = "序号";
		}
		if (dt.Columns["TaskCode"] != null)
		{
			dt.Columns["TaskCode"].ColumnName = "清单编码";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "项目名称";
		}
		if (dt.Columns["FeatureDescription"] != null)
		{
			dt.Columns["FeatureDescription"].ColumnName = "项目特征描述";
		}
		if (dt.Columns["TypeName"] != null)
		{
			dt.Columns["TypeName"].ColumnName = "类型";
		}
		if (dt.Columns["Unit"] != null)
		{
			dt.Columns["Unit"].ColumnName = "单位";
		}
		if (dt.Columns["Quantity"] != null)
		{
			dt.Columns["Quantity"].ColumnName = "工程量";
		}
		if (dt.Columns["ConUnitPrice"] != null)
		{
			dt.Columns["ConUnitPrice"].ColumnName = "综合单价(元)";
		}
		if (dt.Columns["ConTotal"] != null)
		{
			dt.Columns["ConTotal"].ColumnName = "合价(元)";
		}
		if (dt.Columns["LossCoefficient"] != null)
		{
			dt.Columns["LossCoefficient"].ColumnName = "损耗系数";
		}
		if (dt.Columns["ResourcePrice"] != null)
		{
			dt.Columns["ResourcePrice"].ColumnName = "单价(元)";
		}
		if (dt.Columns["BudTotal"] != null)
		{
			dt.Columns["BudTotal"].ColumnName = "合价(元) ";
		}
		if (dt.Columns["BudUnitPrice"] != null)
		{
			dt.Columns["BudUnitPrice"].ColumnName = "成本单价(元)";
		}
		if (dt.Columns["Profit"] != null)
		{
			dt.Columns["Profit"].ColumnName = "利润(元)";
		}
		if (dt.Columns["ProfitRate"] != null)
		{
			dt.Columns["ProfitRate"].ColumnName = "利润率";
		}
		dt.AcceptChanges();
		return dt;
	}
}


