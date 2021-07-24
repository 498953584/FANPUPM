using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_RejectQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			this.bindGv();
		}
	}
	protected void bindGv()
	{
		System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
		{
			1
		};
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
		{
			-2
		};
		string text = this.txtName.Text;
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 1, "0", "");
		this.AspNetPager1.RecordCount = count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, false, 1, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, "0", "");
		this.gvDataInfo.DataSource = all;
		this.gvDataInfo.DataBind();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void btnExport_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
		{
			1
		};
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
		{
			-2
		};
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, this.txtName.Text, false, 1, this.AspNetPager1.CurrentPageIndex, 2147483647, "0", "");
		string[] headerName = new string[]
		{
			"序号",
			"项目状态",
			"立项申请人",
			"项目编号",
			"项目名称",
			"建设单位",
			"项目类型",
			"工程造价",
			"工程工期",
			"立项申请日期"
		};
		string[] fieldName = new string[]
		{
			"",
			"StateText",
			"Person",
			"PrjCode",
			"PrjName",
			"WorkUnitName",
			"PrjTypeName",
			"PrjCost",
			"Duration",
			"InputDate"
		};
		string[] totalField = new string[]
		{
			"PrjCost"
		};
		ExcelHelper.ExportExcel(all, headerName, fieldName, totalField, "驳回信息一览.xls", base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				1
			};
			System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
			{
				-2
			};
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, this.txtName.Text, 1, "0", "");
			e.Row.Cells[6].Text = sumTotal.ToString("#0.000");
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
		}
	}
}


