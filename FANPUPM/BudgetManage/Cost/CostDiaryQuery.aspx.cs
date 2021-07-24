using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Cost_CostDiaryQuery : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private PCPettyCashService pcSer = new PCPettyCashService();
	private BudIndirectDiaryDetails indirDetail = new BudIndirectDiaryDetails();
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryDetailsService indirDetailSer = new BudIndirectDiaryDetailsService();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string costType = "P";

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		if (this.year == "zzjg")
		{
			this.costType = "O";
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindPrjName();
		this.BindGv();
	}
	public void BindGv()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		string person = this.txtPerson.Text.Trim();
		string empty = string.Empty;
		string text = this.txtStartDate.Text;
		string text2 = this.txtEndDate.Text;
		string deparment = this.txtDeparment.Text.Trim();
		decimal? totalAmount = null;
		decimal? endCash = null;
		string name = this.txtName.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtTotalAmount.Text))
		{
			totalAmount = new decimal?(System.Convert.ToDecimal(this.txtTotalAmount.Text));
		}
		if (!string.IsNullOrEmpty(this.txtEndCash.Text))
		{
			endCash = new decimal?(System.Convert.ToDecimal(this.txtEndCash.Text));
		}
		int count = CostDiary.GetCount(this.prjId, person, text, text2, empty, deparment, this.txtName.Text, this.ddlFlowState.SelectedValue, totalAmount, endCash, this.costType);
		DataTable diaries = CostDiary.GetDiaries(this.prjId, person, text, text2, empty, deparment, this.txtName.Text, this.ddlFlowState.SelectedValue, totalAmount, endCash, this.costType, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.AspNetPager1.RecordCount = count;
		this.ViewState["Diaries"] = diaries;
		this.gvBudget.DataSource = diaries;
		this.gvBudget.DataBind();
		this.hfldPurchaseChecked.Value = string.Empty;
		if (this.gvBudget.Rows.Count > 0)
		{
			base.RegisterScript("fillTotalAmount('" + CostDiary.GetDiariesTotal(this.prjId, person, empty, name, this.ddlFlowState.SelectedValue, text, text2, deparment, this.costType, totalAmount, endCash).ToString("#,##0.000") + "');showDetails();");
			return;
		}
		base.RegisterScript("showDetails();");
	}
	protected void ClearSearchCondition()
	{
		this.txtPerson.Text = string.Empty;
		this.txtStartDate.Text = string.Empty;
		this.txtEndDate.Text = string.Empty;
		this.txtDeparment.Text = string.Empty;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["InDiaryId"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable formatDataTable = this.GetFormatDataTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in formatDataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(formatDataTable, this.hfldPrjName.Value + "费用查询(财务)", "sheet1", "费用查询(财务).xls", list, null, 3, base.Request.Browser.Browser);
	}
	private DataTable GetFormatDataTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("费用名称");
		dataTable.Columns.Add("发生单位");
		dataTable.Columns.Add("发生时间");
		dataTable.Columns.Add("填报人");
		dataTable.Columns.Add("经手人");
		dataTable.Columns.Add("流程状态");
		dataTable.Columns.Add("金额  ");
		dataTable.Columns.Add("月度差额比例");
		dataTable.Columns.Add("所属项目");
		dataTable.Columns.Add("核销金额");
		dataTable.Columns.Add("是否核销");
		int num = 0;
		DataTable dataTable2 = this.ViewState["Diaries"] as DataTable;
		if (dataTable2 != null)
		{
			foreach (DataRow dataRow in dataTable2.Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["费用名称"] = dataRow["Name"];
				dataRow2["发生单位"] = dataRow["Department"];
				dataRow2["发生时间"] = Common2.GetTime(dataRow["InputDate"]);
				dataRow2["填报人"] = dataRow["InputUser"];
				dataRow2["经手人"] = dataRow["IssuedBy"];
				dataRow2["流程状态"] = Common2.GetStateNoColor(dataRow["FlowState"].ToString());
				dataRow2["金额  "] = decimal.Parse(dataRow["TotalAmount"].ToString()).ToString("#,##0.000");
				dataRow2["月度差额比例"] = ((decimal.Parse(dataRow["MonthBudgetAmount"].ToString()) > 0m) ? ((decimal.Parse(dataRow["MonthBudgetAmount"].ToString()) - decimal.Parse(dataRow["MothDiaryAlreadyAmount"].ToString())) / decimal.Parse(dataRow["MonthBudgetAmount"].ToString())).ToString("P2") : 0m.ToString("P2"));
				dataRow2["所属项目"] = this.hfldPrjName.Value;
				dataRow2["核销金额"] = this.GetAuditAmountSum(dataRow["InDiaryId"].ToString()).ToString("#,##0.000");
				dataRow2["是否核销"] = this.IsAudit(dataRow["InDiaryId"].ToString());
				dataTable.Rows.Add(dataRow2);
			}
			if (dataTable2.Rows.Count > 0)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["序号"] = "合 计";
				dataRow3["金额  "] = System.Convert.ToDecimal(dataTable2.Compute("SUM(TotalAmount)", "1>0")).ToString("#,##0.000");
				dataTable.Rows.Add(dataRow3);
			}
		}
		return dataTable;
	}
	private void BindPrjName()
	{
		if (this.year != "zzjg")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			if (byId != null)
			{
				this.hfldPrjName.Value = byId.PrjName;
				return;
			}
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(this.prjId);
			if (byId2 != null)
			{
				this.hfldPrjName.Value = byId2.PrjName;
			}
		}
	}
	public IQueryable<BudIndirectDiaryCost> GetIndiCostSouce(string year)
	{
		string costType = (year == "zzjg") ? "O" : "P";
		return
			from p in this.budInCostSer
			where p.CostType == costType && p.ProjectId == this.prjId && p.InputUserCode == this.UserCode
			select p;
	}
	public decimal GetAuditAmountSum(string Id)
	{
		return (
			from p in this.indirDetailSer
			where p.InDiaryId == Id
			select p.AuditAmount).ToList<decimal>().Sum();
	}
	public string IsAudit(string Id)
	{
		BudIndirectDiaryCost byId = this.budInCostSer.GetById(Id);
		if (byId == null)
		{
			return string.Empty;
		}
		if (!byId.IsAudit)
		{
			return "未核销";
		}
		return "已核销";
	}
}


