using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Tender;
using cn.justwin.Web;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractReport_IncometPaymentReport : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometPaymentBll incometPaymentBll = new IncometPaymentBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			this.BindGv();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		if (this.txtSignPeople.Value.Trim() != "")
		{
			stringBuilder.AppendFormat(" AND v_xm LIKE '%{0}%'", this.txtSignPeople.Value.Trim());
		}
		if (this.hldfIsExamineApprove.Value == "1")
		{
			stringBuilder.Append(" AND p1.FlowState=1 ");
		}
		this.BindGv(this.GetTable(stringBuilder.ToString()));
	}
	public DataTable GetTable(string strWhere)
	{
		strWhere = strWhere + " and p1.UserCodes like '%" + base.UserCode + "%'";
		this.contractCount = this.incometContractBll.GetReportTb(this.txtConType.Text.Trim(), this.txtPrjName.Value, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtParty.Value.Trim(), "Payment", strWhere, this.dropPrjKindClass.SelectedValue);
		this.ViewState["contract"] = this.contractCount;
		this.AspNetPager1.RecordCount = this.contractCount.Rows.Count;
		return this.incometContractBll.GetReportTb(this.txtConType.Text.Trim(), this.txtPrjName.Value, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtParty.Value.Trim(), "Payment", strWhere, this.dropPrjKindClass.SelectedValue, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
		string[] value = new string[]
		{
			this.contractCount.Compute("sum(EndPrice)", "").ToString(),
			this.contractCount.Compute("sum(CllectionPrice)", "").ToString()
		};
		int[] index = new int[]
		{
			3,
			10
		};
		GridViewUtility.AddTotalRow(this.gvConract, value, index);
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["isMainContract"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			}
			catch
			{
			}
		}
	}
	public string GetParty(string party)
	{
		PayoutContract payoutContract = new PayoutContract();
		DataTable dataTable = new DataTable();
		string result = string.Empty;
		if (!string.IsNullOrEmpty(party))
		{
			dataTable = payoutContract.GetBName(party.Split(new char[]
			{
				','
			})[0]);
		}
		if (dataTable.Rows.Count > 0)
		{
			result = dataTable.Rows[0]["CorpName"].ToString();
		}
		return result;
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable dt = new DataTable();
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		if (this.txtSignPeople.Value.Trim() != "")
		{
			stringBuilder.AppendFormat(" AND v_xm LIKE '%{0}%'", this.txtSignPeople.Value.Trim());
		}
		if (this.hldfIsExamineApprove.Value == "1")
		{
			stringBuilder.Append(" AND p1.FlowState=1 ");
		}
		dt = (this.ViewState["contract"] as DataTable);
		new Common2().ExportToExecelAll(this.GetTitleByTable(dt), "收入合同收款报表.xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt = WebUtil.UpdateDataTable(dt, "CllectionPrice", "Party");
		if (dt.Rows.Count > 0)
		{
			string[] array = new string[]
			{
				dt.Compute("sum(EndPrice)", "").ToString(),
				dt.Compute("sum(CllectionPrice)", "").ToString()
			};
			DataRow dataRow = dt.NewRow();
			dataRow["ContractCode"] = "合计";
			dataRow["EndPrice"] = array[0];
			dataRow["CllectionPrice"] = array[1];
			dt.Rows.Add(dataRow);
		}
		dt.Columns["Num"].ColumnName = "序号";
		dt.Columns["ContractCode"].ColumnName = "合同编号";
		dt.Columns["ContractName"].ColumnName = "合同名称";
		dt.Columns["EndPrice"].ColumnName = "合同金额";
		dt.Columns["TypeName"].ColumnName = "合同类型";
		dt.Columns["prjName"].ColumnName = "项目";
		dt.Columns["SignedTime"].ColumnName = "签约时间";
		dt.Columns["Party"].ColumnName = "甲方";
		dt.Columns["CllectionPrice"].ColumnName = "收款累计";
		dt.Columns["SignPeopleName"].ColumnName = "签订人";
		dt.Columns["CodeName"].ColumnName = "项目类型";
		dt.Columns.Add(new DataColumn("合同状态", typeof(string)));
		dt.Columns.Remove(dt.Columns["isFContract"]);
		dt.Columns.Remove(dt.Columns["ContractId"]);
		dt.Columns.Remove(dt.Columns["FCode"]);
		dt.Columns.Remove(dt.Columns["Version"]);
		foreach (DataRow dataRow2 in dt.Rows)
		{
			dataRow2["合同状态"] = WebUtil.GetConState(dataRow2["conState"].ToString());
		}
		dt.Columns.Remove(dt.Columns["conState"]);
		dt.Columns["合同名称"].SetOrdinal(2);
		dt.Columns["合同金额"].SetOrdinal(3);
		dt.Columns["合同类型"].SetOrdinal(4);
		dt.Columns["项目"].SetOrdinal(5);
		dt.Columns["项目类型"].SetOrdinal(6);
		dt.Columns["签订人"].SetOrdinal(7);
		dt.Columns["签约时间"].SetOrdinal(8);
		dt.Columns["甲方"].SetOrdinal(9);
		dt.Columns["收款累计"].SetOrdinal(10);
		dt.Columns["合同收款比率"].SetOrdinal(11);
		dt.Columns["合同状态"].SetOrdinal(12);
		return dt;
	}
}


