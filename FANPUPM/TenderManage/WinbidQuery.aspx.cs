using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_WinbidQuery : NBasePage, IRequiresSessionState
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
	private void bindGv()
	{
		System.Collections.Generic.List<int> prjState = this.GetPrjState();
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "BidFlowState");
		this.AspNetPager1.RecordCount = count;
		this.gvwProject.DataSource = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, false, 3, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, "", "BidFlowState");
		this.gvwProject.DataBind();
	}
	protected void gvwProject_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwProject.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
			e.Row.Attributes["state"] = this.gvwProject.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			System.Collections.Generic.List<int> prjState = this.GetPrjState();
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "BidFlowState");
			e.Row.Cells[6].Text = sumTotal.ToString("#0.000");
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected System.Collections.Generic.List<int> GetPrjState()
	{
		return new System.Collections.Generic.List<int>
		{
			5
		};
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<int> prjState = this.GetPrjState();
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.hfldTenderPrjManager.Value, false, 3, this.AspNetPager1.CurrentPageIndex, 2147483647, "", "BidFlowState");
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		foreach (DataControlField dataControlField in this.gvwProject.Columns)
		{
			string headerText = dataControlField.HeaderText;
			if (headerText != "")
			{
				list.Add(headerText);
			}
		}
		string[] source = new string[]
		{
			"No",
			"StateText",
			"Person",
			"PrjCode",
			"PrjName",
			"WorkUnitName",
			"PrjCost",
			"Duration",
			"InputDate"
		};
		string[] totalField = new string[]
		{
			"PrjCost"
		};
		ExcelHelper.ExportExcel(all, list.ToArray(), source.ToArray<string>(), totalField, base.Title.Trim() + ".xls", base.Request.Browser.Browser);
	}
}


