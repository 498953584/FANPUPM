using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractForm_OperateSituation : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ComputeTotal();
			this.DataBindPrjCon();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable prjConInfo = formBLL.GetPrjConInfo(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), 0, 0);
		string[] array = new string[10];
		if (prjConInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(prjConInfo.Compute("sum(InCometContractMoney)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(prjConInfo.Compute("sum(IncometBalanceMoney)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(prjConInfo.Compute("sum(IncometPaymentMoney)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(prjConInfo.Compute("sum(IncometApplyMoney)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(prjConInfo.Compute("sum(PayoutModifiedMoney)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(prjConInfo.Compute("sum(PayoutBalanceMoney)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(prjConInfo.Compute("sum(PayoutPaymentMoney)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(prjConInfo.Compute("sum(SpreadConMoney)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(prjConInfo.Compute("sum(SpreadBalanceMoney)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(prjConInfo.Compute("sum(SpreadPaymentMoney)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
		}
		int[] value = new int[]
		{
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	private void DataBindPrjCon()
	{
		string prjCode = this.txtPrjCode.Text.Trim();
		string prjName = this.txtPrjName.Text.Trim();
		this.AspNetPager1.RecordCount = formBLL.GetPrjConCount(prjCode, prjName);
		DataTable prjConInfo = formBLL.GetPrjConInfo(prjCode, prjName, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwPrjCon.DataSource = prjConInfo;
		this.gvwPrjCon.DataBind();
	}
	protected void gvwPrjCon_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrjCon.DataKeys[e.Row.RowIndex].Value.ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			int[] array2 = (int[])this.ViewState["index"];
			int num = 0;
			int[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				int index = array3[i];
				e.Row.Cells[index].Text = array[num];
				e.Row.Cells[index].Style.Add("text-align", "right");
				e.Row.Cells[index].CssClass = "decimal_input";
				num++;
			}
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.DataBindPrjCon();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable prjConInfo = formBLL.GetPrjConInfo(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), 0, 0);
		if (prjConInfo.Rows.Count > 0)
		{
			DataRow dataRow = prjConInfo.NewRow();
			dataRow["Num"] = "合计";
			dataRow["InCometContractMoney"] = prjConInfo.Compute("sum(InCometContractMoney)", "1>0");
			dataRow["IncometBalanceMoney"] = prjConInfo.Compute("sum(IncometBalanceMoney)", "1>0");
			dataRow["IncometPaymentMoney"] = prjConInfo.Compute("sum(IncometPaymentMoney)", "1>0");
			dataRow["IncometApplyMoney"] = prjConInfo.Compute("sum(IncometApplyMoney)", "1>0");
			dataRow["PayoutModifiedMoney"] = prjConInfo.Compute("sum(PayoutModifiedMoney)", "1>0");
			dataRow["PayoutBalanceMoney"] = prjConInfo.Compute("sum(PayoutBalanceMoney)", "1>0");
			dataRow["PayoutPaymentMoney"] = prjConInfo.Compute("sum(PayoutPaymentMoney)", "1>0");
			dataRow["SpreadConMoney"] = prjConInfo.Compute("sum(SpreadConMoney)", "1>0");
			dataRow["SpreadBalanceMoney"] = prjConInfo.Compute("sum(SpreadBalanceMoney)", "1>0");
			dataRow["SpreadPaymentMoney"] = prjConInfo.Compute("sum(SpreadPaymentMoney)", "1>0");
			prjConInfo.Rows.Add(dataRow);
		}
		IExportable exporter = new ExcelExporter();
		FileExport fileExport = new FileExport(exporter, this.FormatData(prjConInfo), "经营情况分析总表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindPrjCon();
	}
	private DataTable FormatData(DataTable dtPrjCon)
	{
		if (dtPrjCon.Columns["Num"] != null)
		{
			dtPrjCon.Columns["Num"].ColumnName = "序号";
		}
		if (dtPrjCon.Columns["prjGuid"] != null)
		{
			dtPrjCon.Columns.Remove(dtPrjCon.Columns["prjGuid"]);
		}
		if (dtPrjCon.Columns["TypeCode"] != null)
		{
			dtPrjCon.Columns.Remove(dtPrjCon.Columns["TypeCode"]);
		}
		if (dtPrjCon.Columns["userDefined_Date"] != null)
		{
			dtPrjCon.Columns.Remove(dtPrjCon.Columns["userDefined_Date"]);
		}
		if (dtPrjCon.Columns["PrjCode"] != null)
		{
			dtPrjCon.Columns["PrjCode"].ColumnName = "项目编号";
		}
		if (dtPrjCon.Columns["PrjName"] != null)
		{
			dtPrjCon.Columns["PrjName"].ColumnName = "项目名称";
		}
		if (dtPrjCon.Columns["InCometContractMoney"] != null)
		{
			dtPrjCon.Columns["InCometContractMoney"].ColumnName = "收入合同最终金额";
		}
		if (dtPrjCon.Columns["IncometBalanceMoney"] != null)
		{
			dtPrjCon.Columns["IncometBalanceMoney"].ColumnName = "收入合同开累结算";
		}
		if (dtPrjCon.Columns["IncometPaymentMoney"] != null)
		{
			dtPrjCon.Columns["IncometPaymentMoney"].ColumnName = "开累回款";
		}
		if (dtPrjCon.Columns["IncometApplyMoney"] != null)
		{
			dtPrjCon.Columns["IncometApplyMoney"].ColumnName = "挂靠项目开累付款";
		}
		if (dtPrjCon.Columns["PayoutModifiedMoney"] != null)
		{
			dtPrjCon.Columns["PayoutModifiedMoney"].ColumnName = "支出合同最终金额";
		}
		if (dtPrjCon.Columns["PayoutBalanceMoney"] != null)
		{
			dtPrjCon.Columns["PayoutBalanceMoney"].ColumnName = "支出合同开累结算";
		}
		if (dtPrjCon.Columns["PayoutPaymentMoney"] != null)
		{
			dtPrjCon.Columns["PayoutPaymentMoney"].ColumnName = "开累支付";
		}
		if (dtPrjCon.Columns["SpreadConMoney"] != null)
		{
			dtPrjCon.Columns["SpreadConMoney"].ColumnName = "合同差额";
		}
		if (dtPrjCon.Columns["SpreadBalanceMoney"] != null)
		{
			dtPrjCon.Columns["SpreadBalanceMoney"].ColumnName = "结算差额";
		}
		if (dtPrjCon.Columns["SpreadPaymentMoney"] != null)
		{
			dtPrjCon.Columns["SpreadPaymentMoney"].ColumnName = "支付差额";
		}
		dtPrjCon.AcceptChanges();
		return dtPrjCon;
	}
}


