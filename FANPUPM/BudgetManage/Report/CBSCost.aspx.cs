using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Report_CBSCost : NBasePage, IRequiresSessionState
{
	private string outSourceCBSCode = string.Empty;
	private string prjId = string.Empty;
	private string year = string.Empty;
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string isWBSRelevance = ConfigurationManager.AppSettings["IsWBSRelevance"];

	protected override void OnInit(System.EventArgs e)
	{
		this.outSourceCBSCode = ConfigHelper.Get("outSourceCBSCode");
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
		PrjInfoModel prjInfoModel = new PrjInfoModel();
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			prjInfoModel = pTPrjInfoBll.GetModelByPrjGuid(this.prjId);
			this.year = prjInfoModel.StartDate.Year.ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	private DataTable GetCBSCost()
	{
		DataTable dataTable = new DataTable();
		dataTable.Clear();
		DataTable cBSCost = ConstructReport.GetCBSCost(this.prjId);
		if (cBSCost != null)
		{
			dataTable = cBSCost.Clone();
			DataRow dataRow = dataTable.NewRow();
			dataRow = this.GetDataRow("all", dataRow, cBSCost);
			dataTable.Rows.Add(dataRow);
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2 = this.GetDataRow("self", dataRow2, cBSCost);
			dataTable.Rows.Add(dataRow2);
			DataRow[] array = cBSCost.Select("CBSCode<>'" + this.outSourceCBSCode + "' AND Type='D'", "CBSCode");
			int num = 0;
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow3 = array2[i];
				num++;
				DataRow dataRow4 = dataTable.NewRow();
				dataRow4.ItemArray = dataRow3.ItemArray;
				dataRow4["Num"] = num.ToString();
				dataTable.Rows.Add(dataRow4);
			}
			DataRow dataRow5 = dataTable.NewRow();
			dataRow5 = this.GetDataRow("indInfo", dataRow5, cBSCost);
			dataTable.Rows.Add(dataRow5);
			DataRow[] array3 = cBSCost.Select("Type='I'", "CBSCode");
			int num2 = 0;
			num++;
			DataRow[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				DataRow dataRow6 = array4[j];
				num2++;
				string value = num.ToString() + "." + num2.ToString();
				DataRow dataRow7 = dataTable.NewRow();
				dataRow7.ItemArray = dataRow6.ItemArray;
				dataRow7["Num"] = value;
				dataTable.Rows.Add(dataRow7);
			}
			DataRow[] array5 = cBSCost.Select("CBSCode='" + this.outSourceCBSCode + "'");
			DataRow[] array6 = array5;
			for (int k = 0; k < array6.Length; k++)
			{
				DataRow dataRow8 = array6[k];
				DataRow dataRow9 = dataTable.NewRow();
				dataRow9.ItemArray = dataRow8.ItemArray;
				dataRow9["Num"] = "（二）";
				dataTable.Rows.Add(dataRow9);
			}
		}
		return dataTable;
	}
	private DataRow GetDataRow(string type, DataRow dr, DataTable dtCBSCode)
	{
		string filter = string.Empty;
		string value = string.Empty;
		string value2 = string.Empty;
		if (type == "all")
		{
			value2 = "一";
			filter = "1>0";
			value = "合同预计总成本";
		}
		else
		{
			if (type == "self")
			{
				value2 = "（一）";
				filter = "CBSCode<>'" + this.outSourceCBSCode + "'";
				value = "自营成本";
			}
			else
			{
				if (type == "indInfo")
				{
					DataRow[] array = dtCBSCode.Select("CBSCode<>'" + this.outSourceCBSCode + "' AND Type='D'", "CBSCode");
					int num = array.Length;
					value2 = (num + 1).ToString();
					filter = "Type='I'";
					value = "间接费用";
				}
			}
		}
		decimal num2 = (decimal)dtCBSCode.Compute("sum(BudTotal)", filter);
		decimal num3 = (decimal)dtCBSCode.Compute("sum(LastRealityTotal)", filter);
		decimal num4 = (decimal)dtCBSCode.Compute("sum(CurrentRealityTotal)", filter);
		decimal num5 = (decimal)dtCBSCode.Compute("sum(RealityTotal)", filter);
		dr["Num"] = value2;
		dr["CBSName"] = value;
		dr["BudTotal"] = num2;
		dr["LastRealityTotal"] = num3;
		dr["CurrentRealityTotal"] = num4;
		dr["RealityTotal"] = num5;
		return dr;
	}
	private void BindGv()
	{
		DataTable dataSource = new DataTable();
		dataSource = this.GetCBSCost();
		this.gvCBSCost.DataSource = dataSource;
		this.gvCBSCost.DataBind();
	}
	protected void gvCBSCost_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable sourceTable = new DataTable();
		DataTable cBSCost = this.GetCBSCost();
		sourceTable = this.GetTitleByTable(cBSCost);
		ExcelHelper.ExportExcel(sourceTable, "项目成本计划分项分类表", "项目成本计划分项分类表", "项目成本计划分项分类表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["CBSCode"] != null)
		{
			dt.Columns.Remove("CBSCode");
		}
		if (dt.Columns["Type"] != null)
		{
			dt.Columns.Remove("Type");
		}
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["CBSName"] != null)
		{
			dt.Columns["CBSName"].ColumnName = "成本计划内容 ";
		}
		if (dt.Columns["BudTotal"] != null)
		{
			dt.Columns["BudTotal"].ColumnName = "首次计划金额";
		}
		if (dt.Columns["LastRealityTotal"] != null)
		{
			dt.Columns["LastRealityTotal"].ColumnName = "开工至上年末累计实际发生额";
		}
		if (dt.Columns["CurrentRealityTotal"] != null)
		{
			dt.Columns["CurrentRealityTotal"].ColumnName = "本年度累计实际发生额";
		}
		if (dt.Columns["RealityTotal"] != null)
		{
			dt.Columns["RealityTotal"].ColumnName = "开工至调整日前实际累计发生额";
		}
		dt.AcceptChanges();
		return dt;
	}
}


