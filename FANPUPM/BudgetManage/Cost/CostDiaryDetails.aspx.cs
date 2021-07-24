using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Cost_CostDiaryDetails : NBasePage, IRequiresSessionState
{
	private BudIndirectDiaryCostService cSer = new BudIndirectDiaryCostService();
	private BudIndirectDiaryCost indirCost = new BudIndirectDiaryCost();
	private BudIndirectDiaryDetailsService DSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryDetails indirDetail = new BudIndirectDiaryDetails();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		this.AspNetPager1.RecordCount = this.cSer.GetTableCount(this.GetDiaryCostQueryable()).Rows.Count;
		DataTable table = this.cSer.GetTable(this.GetDiaryCostQueryable(), NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwCostVerify.DataSource = table;
		this.gvwCostVerify.DataBind();
	}
	public string GetDiaryCostQueryable()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		if (!string.IsNullOrEmpty(this.txtDepmentName.Value))
		{
			stringBuilder.Append(" AND V_BMMC like '%" + this.txtDepmentName.Value + "%' ");
		}
		if (!string.IsNullOrEmpty(this.ddlFlowState.SelectedValue))
		{
			stringBuilder.Append(" AND FlowState =" + this.ddlFlowState.SelectedValue + " ");
		}
		if (!string.IsNullOrEmpty(this.txtUserName.Value))
		{
			stringBuilder.Append(" AND IssuedBy like '%" + this.txtUserName.Value + "%' ");
		}
		if (!string.IsNullOrEmpty(this.txtIDNumber.Text.Trim()))
		{
			stringBuilder.Append(" AND IndireCode like '%" + this.txtIDNumber.Text.Trim() + "%' \n");
		}
		if (!string.IsNullOrEmpty(this.txtPojectNum.Text.Trim()))
		{
			stringBuilder.Append(" AND PrjCode like '%" + this.txtPojectNum.Text.Trim() + "%' \n");
		}
		System.DateTime? dateTime = DateTimeHelper.GetDateTime(this.txtStartDate.Text);
		System.DateTime? dateTime2 = DateTimeHelper.GetDateTime(this.txtEndDate.Text);
		if (dateTime.HasValue)
		{
			stringBuilder.Append(" AND InputDate2 > '" + dateTime.Value + "' \n");
		}
		if (dateTime2.HasValue)
		{
			stringBuilder.Append(" AND InputDate2 < '" + dateTime2.Value.AddDays(1.0) + "' \n");
		}
		return stringBuilder.ToString();
	}
	public string GetPrjOrOrg(object inDiaryIdObj)
	{
		string inDiaryId = inDiaryIdObj.ToString();
		BudIndirectDiaryCost byId = this.cSer.GetById(inDiaryId);
		if (byId == null)
		{
			return string.Empty;
		}
		string result = string.Empty;
		string arg_28_0 = string.Empty;
		if (byId.CostType == "P")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.ProjectId);
			if (byId2 != null)
			{
				result = byId2.PrjName;
			}
			string arg_62_0 = byId2.PrjCode;
		}
		else
		{
			PTdbmService pTdbmService = new PTdbmService();
			PTdbm byId3 = pTdbmService.GetById(byId.ProjectId);
			if (byId3 != null)
			{
				result = byId3.V_bmqc;
			}
		}
		return result;
	}
	public string GetPrjCode(object inDiaryIdObj)
	{
		string inDiaryId = inDiaryIdObj.ToString();
		BudIndirectDiaryCost byId = this.cSer.GetById(inDiaryId);
		if (byId == null)
		{
			return string.Empty;
		}
		string result = string.Empty;
		if (byId.CostType == "P")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.ProjectId);
			if (byId2 != null)
			{
				result = byId2.PrjCode;
			}
		}
		return result;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwCostVerify_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		DataControlRowType arg_0D_0 = e.Row.RowType;
	}
	public string CBSName(string CBSCode)
	{
		CostAccounting byCode = CostAccounting.GetByCode(CBSCode);
		if (byCode == null)
		{
			return string.Empty;
		}
		return byCode.Name;
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		ExcelExporter excelExporter = new ExcelExporter();
		System.Collections.Generic.List<DataTable> data = this.GetData();
		HttpContext current = HttpContext.Current;
		excelExporter.ExportAll(data, current, "费用报销明细.xls");
	}
	public System.Collections.Generic.List<DataTable> GetData()
	{
		System.Collections.Generic.List<DataTable> list = new System.Collections.Generic.List<DataTable>();
		DataTable table = this.cSer.GetTable(this.GetDiaryCostQueryable(), this.AspNetPager1.RecordCount, this.AspNetPager1.CurrentPageIndex);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("报销日期");
		dataTable.Columns.Add("ID号(流水号)");
		dataTable.Columns.Add("姓名");
		dataTable.Columns.Add("成本科目");
		dataTable.Columns.Add("费用名称");
		dataTable.Columns.Add("金额");
		dataTable.Columns.Add("流程状态");
		dataTable.Columns.Add("备注");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("部门");
		for (int i = 0; i < table.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = i + 1;
			dataRow["报销日期"] = table.Rows[i][1].ToString();
			dataRow["ID号(流水号)"] = table.Rows[i][2].ToString();
			dataRow["姓名"] = table.Rows[i][3].ToString();
			dataRow["成本科目"] = table.Rows[i][4].ToString();
			dataRow["费用名称"] = table.Rows[i][5].ToString();
			dataRow["金额"] = table.Rows[i][7].ToString();
			dataRow["流程状态"] = Common2.GetStateNoColor(table.Rows[i][8].ToString());
			dataRow["备注"] = table.Rows[i][9].ToString();
			dataRow["项目编号"] = table.Rows[i][10].ToString();
			dataRow["项目名称"] = table.Rows[i][11].ToString();
			dataRow["部门"] = table.Rows[i][14].ToString();
			dataTable.Rows.Add(dataRow);
		}
		list.Add(dataTable);
		return list;
	}
}


