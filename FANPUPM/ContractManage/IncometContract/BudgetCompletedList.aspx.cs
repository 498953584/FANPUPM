using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_IncometContract_BudgetCompletedList : NBasePage, IRequiresSessionState
{
	private BudConModifyService budModifySev = new BudConModifyService();
	private BudConModifyTaskService conModifyTaskSer = new BudConModifyTaskService();
	private BudContractTaskService conTaskSev = new BudContractTaskService();
	private IncometContract dal = new IncometContract();
	private decimal preTotal;
	private decimal curTotal;
	private decimal changedTotal;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBandList();
		}
	}
	private void DataBandList()
	{
		string prjId = base.Request.QueryString["PrjCode"];
		string text = string.Empty;
		if (this.txtCode.Text.Trim() != "")
		{
			text = text + " AND TaskCode Like '%" + this.txtCode.Text.Trim() + "%'";
		}
		if (this.txtName.Text.Trim() != "")
		{
			text = text + " AND  TaskName Like '%" + this.txtName.Text.Trim() + "%'";
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.AspNetPager1.RecordCount = this.dal.GetModifyDetail(prjId, text, 1, NBasePage.pagesize * this.AspNetPager1.PageCount).Rows.Count;
		DataTable modifyDetail = this.dal.GetModifyDetail(prjId, text, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		foreach (DataRow dataRow in modifyDetail.Rows)
		{
			this.preTotal += System.Convert.ToDecimal(dataRow["oTotal"]);
			this.curTotal += System.Convert.ToDecimal(dataRow["CTotal"]);
			this.changedTotal += System.Convert.ToDecimal(dataRow["Total"]);
		}
		this.hfldPreTotal.Value = this.preTotal.ToString();
		this.hfldCurTotal.Value = this.curTotal.ToString();
		this.hfldChangedTotal.Value = this.changedTotal.ToString();
		this.ViewState["dt"] = modifyDetail;
		this.gvwBudModify.PageSize = NBasePage.pagesize;
		this.gvwBudModify.DataSource = modifyDetail;
		this.gvwBudModify.DataBind();
	}
	public void gvwBudModify_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvwBudModify.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string value2 = this.gvwBudModify.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			decimal d = System.Convert.ToDecimal(this.gvwBudModify.DataKeys[e.Row.RowIndex]["Total"]);
			decimal d2 = System.Convert.ToDecimal(this.gvwBudModify.DataKeys[e.Row.RowIndex]["Quantity"]);
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["ModifyType"] = value2;
			if (d2 > 0m)
			{
				e.Row.Cells[9].Text = "+" + d2.ToString();
			}
			if (d > 0m)
			{
				e.Row.Cells[10].Text = "+" + d.ToString();
				return;
			}
		}
		else
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				e.Row.Cells[0].Text = "合计";
				e.Row.Cells[6].Text = this.hfldPreTotal.Value;
				e.Row.Cells[6].Style.Add("text-align", "right");
				e.Row.Cells[6].CssClass = "decimal_input";
				e.Row.Cells[8].Text = this.hfldCurTotal.Value;
				e.Row.Cells[8].Style.Add("text-align", "right");
				e.Row.Cells[8].CssClass = "decimal_input";
				if (System.Convert.ToDecimal(this.hfldChangedTotal.Value) > 0m)
				{
					e.Row.Cells[10].Text = "+" + this.hfldChangedTotal.Value;
				}
				else
				{
					e.Row.Cells[10].Text = this.hfldChangedTotal.Value;
				}
				e.Row.Cells[10].Style.Add("text-align", "right");
				e.Row.Cells[10].CssClass = "decimal_input";
			}
		}
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.ViewState["dt"] as DataTable;
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		DataTable dataTable2 = new DataTable();
		for (int i = 0; i < this.gvwBudModify.Columns.Count; i++)
		{
			dataTable2.Columns.Add(this.gvwBudModify.HeaderRow.Cells[i].Text);
		}
		string[] values = new string[10];
		int num = 1;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = num++.ToString();
			string text2 = dataRow["TaskCode"].ToString();
			string text3 = dataRow["TaskName"].ToString();
			string text4 = dataRow["Unit"].ToString();
			string text5 = dataRow["UnitPrice"].ToString();
			string text6 = dataRow["oQty"].ToString();
			string text7 = dataRow["oTotal"].ToString();
			string text8 = dataRow["CQuantity"].ToString();
			string text9 = dataRow["CTotal"].ToString();
			string text10 = dataRow["Quantity"].ToString();
			string text11 = dataRow["Total"].ToString();
			values = new string[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7,
				text8,
				text9,
				text10,
				text11
			};
			dataTable2.Rows.Add(values);
		}
		string[] values2 = new string[]
		{
			"合计",
			"",
			"",
			"",
			"",
			"",
			this.hfldPreTotal.Value,
			"",
			this.hfldCurTotal.Value,
			"",
			this.hfldChangedTotal.Value
		};
		dataTable2.Rows.Add(values2);
		string text12 = "合同预算变更工程量清单.xls";
		if (base.Request.Browser.Browser != "Safari")
		{
			text12 = HttpUtility.UrlEncode(text12, System.Text.Encoding.UTF8);
		}
		ExcelHelper.ExportDataTableToExcel(dataTable2, text12, "sheet1");
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.DataBandList();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBandList();
	}
}


