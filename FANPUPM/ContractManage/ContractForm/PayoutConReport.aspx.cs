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
public partial class ContractManage_ContractForm_PayoutConReport : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();
	private string IncometConId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["IncometConId"]))
		{
			this.IncometConId = base.Request["IncometConId"].ToString();
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
		DataTable payoutConInfo = formBLL.GetPayoutConInfo(this.IncometConId, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), 0, 0);
		string[] array = new string[4];
		if (payoutConInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(payoutConInfo.Compute("sum(ContractMoney)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(payoutConInfo.Compute("sum(PayoutModifiedMoney)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(payoutConInfo.Compute("sum(PayoutBalanceMoney)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(payoutConInfo.Compute("sum(PayoutPaymentMoney)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
		}
		int[] value = new int[]
		{
			4,
			5,
			6,
			7
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
	}
	private void DataBindIncometCon()
	{
		string conCode = this.txtContractCode.Text.Trim();
		string conName = this.txtContractName.Text.Trim();
		string conType = this.txtConType.Text.Trim();
		this.AspNetPager1.RecordCount = formBLL.GetPayoutConCount(this.IncometConId, conCode, conName, conType);
		DataTable payoutConInfo = formBLL.GetPayoutConInfo(this.IncometConId, conCode, conName, conType, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwPayoutCon.DataSource = payoutConInfo;
		this.gvwPayoutCon.DataBind();
	}
	protected void gvwPayoutCon_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPayoutCon.DataKeys[e.Row.RowIndex].Value.ToString();
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
		DataTable payoutConInfo = formBLL.GetPayoutConInfo(this.IncometConId, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), 0, 0);
		if (payoutConInfo.Rows.Count > 0)
		{
			DataRow dataRow = payoutConInfo.NewRow();
			dataRow["Num"] = "合计";
			dataRow["ContractMoney"] = payoutConInfo.Compute("sum(ContractMoney)", "1>0");
			dataRow["PayoutModifiedMoney"] = payoutConInfo.Compute("sum(PayoutModifiedMoney)", "1>0");
			dataRow["PayoutBalanceMoney"] = payoutConInfo.Compute("sum(PayoutBalanceMoney)", "1>0");
			dataRow["PayoutPaymentMoney"] = payoutConInfo.Compute("sum(PayoutPaymentMoney)", "1>0");
			payoutConInfo.Rows.Add(dataRow);
		}
		IExportable exporter = new ExcelExporter();
		FileExport fileExport = new FileExport(exporter, this.FormatData(payoutConInfo), "支出合同明细.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindIncometCon();
	}
	private DataTable FormatData(DataTable dtPayoutCon)
	{
		if (dtPayoutCon.Columns["Num"] != null)
		{
			dtPayoutCon.Columns["Num"].ColumnName = "序号";
		}
		if (dtPayoutCon.Columns["PayoutConId"] != null)
		{
			dtPayoutCon.Columns.Remove(dtPayoutCon.Columns["PayoutConId"]);
		}
		if (dtPayoutCon.Columns["Date"] != null)
		{
			dtPayoutCon.Columns.Remove(dtPayoutCon.Columns["Date"]);
		}
		if (dtPayoutCon.Columns["ContractCode"] != null)
		{
			dtPayoutCon.Columns["ContractCode"].ColumnName = "合同编号";
		}
		if (dtPayoutCon.Columns["ContractName"] != null)
		{
			dtPayoutCon.Columns["ContractName"].ColumnName = "合同名称";
		}
		if (dtPayoutCon.Columns["TypeName"] != null)
		{
			dtPayoutCon.Columns["TypeName"].ColumnName = "合同类型";
		}
		if (dtPayoutCon.Columns["ContractMoney"] != null)
		{
			dtPayoutCon.Columns["ContractMoney"].ColumnName = "原始金额";
		}
		if (dtPayoutCon.Columns["PayoutModifiedMoney"] != null)
		{
			dtPayoutCon.Columns["PayoutModifiedMoney"].ColumnName = "最终金额";
		}
		if (dtPayoutCon.Columns["PayoutBalanceMoney"] != null)
		{
			dtPayoutCon.Columns["PayoutBalanceMoney"].ColumnName = "开累结算";
		}
		if (dtPayoutCon.Columns["PayoutPaymentMoney"] != null)
		{
			dtPayoutCon.Columns["PayoutPaymentMoney"].ColumnName = "开累支付";
		}
		dtPayoutCon.AcceptChanges();
		return dtPayoutCon;
	}
}


