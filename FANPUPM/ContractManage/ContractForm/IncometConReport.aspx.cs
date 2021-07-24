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
public partial class ContractManage_ContractForm_IncometConReport : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ComputeTotal();
			this.DataBindIncometCon();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable incometPayoutConInfo = formBLL.GetIncometPayoutConInfo(this.prjId, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), 0, 0);
		string[] array = new string[11];
		if (incometPayoutConInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(ContractPrice)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(InCometContractMoney)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(IncometBalanceMoney)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(IncometPaymentMoney)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(IncometApplyMoney)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(PayoutModifiedMoney)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(PayoutBalanceMoney)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(PayoutPaymentMoney)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(SpreadConMoney)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(SpreadBalanceMoney)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(incometPayoutConInfo.Compute("sum(SpreadPaymentMoney)", "1>0")).ToString("0.000");
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
			array[10] = "0.000";
		}
		int[] value = new int[]
		{
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	private void DataBindIncometCon()
	{
		string conCode = this.txtContractCode.Text.Trim();
		string conName = this.txtContractName.Text.Trim();
		string conType = this.txtConType.Text.Trim();
		this.AspNetPager1.RecordCount = formBLL.GetIncometPayoutConCount(this.prjId, conCode, conName, conType);
		DataTable incometPayoutConInfo = formBLL.GetIncometPayoutConInfo(this.prjId, conCode, conName, conType, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwIncometCon.DataSource = incometPayoutConInfo;
		this.gvwIncometCon.DataBind();
	}
	protected void gvwIncometCon_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwIncometCon.DataKeys[e.Row.RowIndex].Value.ToString();
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
				num++;
			}
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.DataBindIncometCon();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable incometPayoutConInfo = formBLL.GetIncometPayoutConInfo(this.prjId, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), 0, 0);
		if (incometPayoutConInfo.Rows.Count > 0)
		{
			DataRow dataRow = incometPayoutConInfo.NewRow();
			dataRow["Num"] = "合计";
			dataRow["ContractPrice"] = incometPayoutConInfo.Compute("sum(ContractPrice)", "1>0");
			dataRow["InCometContractMoney"] = incometPayoutConInfo.Compute("sum(InCometContractMoney)", "1>0");
			dataRow["IncometBalanceMoney"] = incometPayoutConInfo.Compute("sum(IncometBalanceMoney)", "1>0");
			dataRow["IncometPaymentMoney"] = incometPayoutConInfo.Compute("sum(IncometPaymentMoney)", "1>0");
			dataRow["IncometApplyMoney"] = incometPayoutConInfo.Compute("sum(IncometApplyMoney)", "1>0");
			dataRow["PayoutModifiedMoney"] = incometPayoutConInfo.Compute("sum(PayoutModifiedMoney)", "1>0");
			dataRow["PayoutBalanceMoney"] = incometPayoutConInfo.Compute("sum(PayoutBalanceMoney)", "1>0");
			dataRow["PayoutPaymentMoney"] = incometPayoutConInfo.Compute("sum(PayoutPaymentMoney)", "1>0");
			dataRow["SpreadConMoney"] = incometPayoutConInfo.Compute("sum(SpreadConMoney)", "1>0");
			dataRow["SpreadBalanceMoney"] = incometPayoutConInfo.Compute("sum(SpreadBalanceMoney)", "1>0");
			dataRow["SpreadPaymentMoney"] = incometPayoutConInfo.Compute("sum(SpreadPaymentMoney)", "1>0");
			incometPayoutConInfo.Rows.Add(dataRow);
		}
		IExportable exporter = new ExcelExporter();
		FileExport fileExport = new FileExport(exporter, this.FormatData(incometPayoutConInfo), "收入合同明细.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindIncometCon();
	}
	private DataTable FormatData(DataTable dtIncometCon)
	{
		if (dtIncometCon.Columns["Num"] != null)
		{
			dtIncometCon.Columns["Num"].ColumnName = "序号";
		}
		if (dtIncometCon.Columns["ContractId"] != null)
		{
			dtIncometCon.Columns.Remove(dtIncometCon.Columns["ContractId"]);
		}
		if (dtIncometCon.Columns["Date"] != null)
		{
			dtIncometCon.Columns.Remove(dtIncometCon.Columns["Date"]);
		}
		if (dtIncometCon.Columns["ContractCode"] != null)
		{
			dtIncometCon.Columns["ContractCode"].ColumnName = "合同编号";
		}
		if (dtIncometCon.Columns["ContractName"] != null)
		{
			dtIncometCon.Columns["ContractName"].ColumnName = "合同名称";
		}
		if (dtIncometCon.Columns["TypeName"] != null)
		{
			dtIncometCon.Columns["TypeName"].ColumnName = "合同类型";
		}
		if (dtIncometCon.Columns["ContractPrice"] != null)
		{
			dtIncometCon.Columns["ContractPrice"].ColumnName = "原始金额";
		}
		if (dtIncometCon.Columns["InCometContractMoney"] != null)
		{
			dtIncometCon.Columns["InCometContractMoney"].ColumnName = "最终金额";
		}
		if (dtIncometCon.Columns["IncometBalanceMoney"] != null)
		{
			dtIncometCon.Columns["IncometBalanceMoney"].ColumnName = "开累结算";
		}
		if (dtIncometCon.Columns["IncometPaymentMoney"] != null)
		{
			dtIncometCon.Columns["IncometPaymentMoney"].ColumnName = "开累回款";
		}
		if (dtIncometCon.Columns["IncometApplyMoney"] != null)
		{
			dtIncometCon.Columns["IncometApplyMoney"].ColumnName = "挂靠项目开累付款";
		}
		if (dtIncometCon.Columns["PayoutModifiedMoney"] != null)
		{
			dtIncometCon.Columns["PayoutModifiedMoney"].ColumnName = "支出合同最终金额";
		}
		if (dtIncometCon.Columns["PayoutBalanceMoney"] != null)
		{
			dtIncometCon.Columns["PayoutBalanceMoney"].ColumnName = "支出合同开累结算";
		}
		if (dtIncometCon.Columns["PayoutPaymentMoney"] != null)
		{
			dtIncometCon.Columns["PayoutPaymentMoney"].ColumnName = "开累支付";
		}
		if (dtIncometCon.Columns["SpreadConMoney"] != null)
		{
			dtIncometCon.Columns["SpreadConMoney"].ColumnName = "合同差额";
		}
		if (dtIncometCon.Columns["SpreadBalanceMoney"] != null)
		{
			dtIncometCon.Columns["SpreadBalanceMoney"].ColumnName = "结算差额";
		}
		if (dtIncometCon.Columns["SpreadPaymentMoney"] != null)
		{
			dtIncometCon.Columns["SpreadPaymentMoney"].ColumnName = "支付差额";
		}
		dtIncometCon.AcceptChanges();
		return dtIncometCon;
	}
}


