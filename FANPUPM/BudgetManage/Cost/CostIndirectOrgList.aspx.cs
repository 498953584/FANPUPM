using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostIndirectOrgList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtInputDate.Text = System.DateTime.Now.ToString("yyyy-MM");
			this.BindgvSouce();
		}
	}
	protected void BindgvSouce()
	{
		string depment = this.txtDepmentName.Value.Trim();
		string userName = this.txtUserName.Value.Trim();
		System.DateTime inputDate = string.IsNullOrEmpty(this.txtInputDate.Text.Trim()) ? System.DateTime.Now : System.Convert.ToDateTime(this.txtInputDate.Text.Trim());
		DataTable indirectOrgCosts = EReport.GetIndirectOrgCosts(depment, userName, inputDate);
		this.gvCostInOrgList.DataSource = indirectOrgCosts;
		this.gvCostInOrgList.DataBind();
		this.RowSpanTalbe(1, 3, this.gvCostInOrgList);
		this.ViewState["gvwdataSouce"] = indirectOrgCosts;
		this.ViewState["gvwCostIndirectOrg"] = this.Formatdata(indirectOrgCosts);
		string[] sumAmount = this.GetSumAmount(indirectOrgCosts);
		int[] index = new int[]
		{
			7,
			9,
			12,
			14
		};
		GridViewUtility.AddTotalRow(this.gvCostInOrgList, sumAmount, index);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindgvSouce();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		this.ExportToExcel("application/ms-excel", "间接成本汇总表.xls");
	}
	public void ExportToExcel(string FileType, string FileName)
	{
		GridView gridView = new GridView();
		DataTable dataSource = this.ViewState["gvwCostIndirectOrg"] as DataTable;
		base.Response.Charset = "GB2312";
		base.Response.ContentEncoding = System.Text.Encoding.UTF8;
		base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
		base.Response.ContentType = FileType;
		this.EnableViewState = false;
		System.IO.StringWriter stringWriter = new System.IO.StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		gridView.DataSource = dataSource;
		gridView.DataBind();
		this.RowSpanTalbe(1, 3, gridView);
		string[] sumAmount = this.GetSumAmount(this.ViewState["gvwdataSouce"] as DataTable);
		int[] index = new int[]
		{
			7,
			9,
			12,
			14
		};
		GridViewUtility.AddTotalRow(gridView, sumAmount, index);
		gridView.RenderControl(writer);
		base.Response.Write(stringWriter.ToString());
		base.Response.End();
	}
	public string[] GetSumAmount(DataTable sourceTable)
	{
		return new string[]
		{
			sourceTable.Compute("SUM(SumItemMonthAmount)", "1>0").ToString(),
			sourceTable.Compute("SUM(SumItemMonthAuditAmount)", "1>0").ToString(),
			sourceTable.Compute("SUM(SumItemYearAmount)", "1>0").ToString(),
			sourceTable.Compute("SUM(SumItemYearAuditAmount)", "1>0").ToString()
		};
	}
	private DataTable Formatdata(DataTable ds)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("部门");
		dataTable.Columns.Add("姓名");
		dataTable.Columns.Add("岗位");
		dataTable.Columns.Add("成本编码");
		dataTable.Columns.Add("成本名称");
		dataTable.Columns.Add("选中年月");
		dataTable.Columns.Add("月预报销金额");
		dataTable.Columns.Add("月预报销合计");
		dataTable.Columns.Add("月核销金额");
		dataTable.Columns.Add("月核销合计");
		dataTable.Columns.Add("选中年");
		dataTable.Columns.Add("年预报销金额");
		dataTable.Columns.Add("年预报销合计");
		dataTable.Columns.Add("年核销金额");
		dataTable.Columns.Add("年核销合计");
		foreach (DataRow dataRow in ds.Rows)
		{
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = dataRow["Num"];
			dataRow2["部门"] = ((dataRow["V_BMMC"] == System.DBNull.Value) ? string.Empty : dataRow["V_BMMC"].ToString());
			dataRow2["姓名"] = ((dataRow["IssuedBy"] == System.DBNull.Value) ? string.Empty : dataRow["IssuedBy"]);
			dataRow2["岗位"] = ((dataRow["DutyName"] == System.DBNull.Value) ? string.Empty : dataRow["DutyName"]);
			dataRow2["成本编码"] = ((dataRow["CBSCode"] == System.DBNull.Value) ? string.Empty : dataRow["CBSCode"]);
			dataRow2["成本名称"] = ((dataRow["CBSName"] == System.DBNull.Value) ? string.Empty : dataRow["CBSName"]);
			dataRow2["选中年月"] = ((dataRow["YearMonthdate"] == System.DBNull.Value) ? string.Empty : this.GetTime(dataRow["YearMonthdate"]));
			dataRow2["月预报销金额"] = ((dataRow["SumItemMonthAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumItemMonthAmount"]));
			dataRow2["月预报销合计"] = ((dataRow["SumMonthAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumMonthAmount"]));
			dataRow2["月核销金额"] = ((dataRow["SumItemMonthAuditAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumItemMonthAuditAmount"]));
			dataRow2["月核销合计"] = ((dataRow["SumMonthAuditAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumMonthAuditAmount"]));
			dataRow2["选中年"] = ((dataRow["Yeardate"] == System.DBNull.Value) ? string.Empty : dataRow["Yeardate"]);
			dataRow2["年预报销金额"] = ((dataRow["SumItemYearAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumItemYearAmount"]));
			dataRow2["年预报销合计"] = ((dataRow["SumYearAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumYearAmount"]));
			dataRow2["年核销金额"] = ((dataRow["SumItemYearAuditAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumItemYearAuditAmount"]));
			dataRow2["年核销合计"] = ((dataRow["SumYearAuditAmount"] == System.DBNull.Value) ? string.Empty : this.FormAmount(dataRow["SumYearAuditAmount"]));
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	protected string GetTime(object datetime)
	{
		if (datetime != null && datetime.ToString() != "")
		{
			System.DateTime dateTime = System.Convert.ToDateTime(datetime);
			return string.Concat(new object[]
			{
				dateTime.Year,
				"年",
				dateTime.Month,
				"月"
			});
		}
		return string.Empty;
	}
	protected string FormAmount(object Amount)
	{
		if (Amount != null && Amount.ToString() != "")
		{
			return string.Format("{0:f3}", Amount);
		}
		return string.Empty;
	}
	private void RowSpanTalbe(int cellstart, int colLength, GridView gvwPayInContable)
	{
		if (gvwPayInContable.Rows == null || gvwPayInContable.Rows.Count <= 1)
		{
			return;
		}
		int[] array = new int[colLength];
		for (int i = 1; i < gvwPayInContable.Rows.Count; i++)
		{
			for (int j = cellstart; j < cellstart + colLength; j++)
			{
				if (gvwPayInContable.Rows[array[j - cellstart]].Cells[j].RowSpan == 0)
				{
					gvwPayInContable.Rows[array[j - cellstart]].Cells[j].RowSpan++;
				}
				if (!gvwPayInContable.Rows[i].Cells[j].Text.Equals(gvwPayInContable.Rows[i - 1].Cells[j].Text))
				{
					while (j < cellstart + colLength)
					{
						array[j - cellstart] = i;
						j++;
					}
					break;
				}
				gvwPayInContable.Rows[array[j - cellstart]].Cells[j].RowSpan++;
				gvwPayInContable.Rows[i].Cells[j].Visible = false;
			}
		}
	}
}


