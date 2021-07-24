using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_EnableList : NBasePage, IRequiresSessionState
{
	private PTPrjInfoZTBService prjZTbSer = new PTPrjInfoZTBService();
	private PTPrjInfoService prjInfoSer = new PTPrjInfoService();
	private PTPrjInfoZTBDetailService prjZTbDetailSer = new PTPrjInfoZTBDetailService();

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
			1,
			2,
			3,
			4,
			14,
			15,
			16,
			18,
			19
		};
		string text = this.txtName.Text;
		int countAtGiveUp = TenderInfo.GetCountAtGiveUp(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, text, 4);
		this.AspNetPager1.RecordCount = countAtGiveUp;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable allAtGiveUp = TenderInfo.GetAllAtGiveUp(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, text, false, 4, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvDataInfo.DataSource = allAtGiveUp;
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
			1,
			2,
			3,
			4,
			14,
			15,
			16,
			18,
			19
		};
		DataTable allAtGiveUp = TenderInfo.GetAllAtGiveUp(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtName.Text, false, 4, this.AspNetPager1.CurrentPageIndex, 2147483647);
		string[] headerName = new string[]
		{
			"序号",
			"项目状态",
			"项目跟踪人",
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
		ExcelHelper.ExportExcel(allAtGiveUp, headerName, fieldName, totalField, "启动项目一览.xls", base.Request.Browser.Browser);
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["flowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["GiveUpFlowState"].ToString();
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["PrjState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			e.Row.Attributes["IsGiveUp"] = System.Convert.ToInt32(this.gvDataInfo.DataKeys[e.Row.RowIndex]["IsGiveUp"]).ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				1,
				2,
				3,
				4,
				14,
				15,
				16,
				18,
				19
			};
			decimal sumTotalAtGiveUp = TenderInfo.GetSumTotalAtGiveUp(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtName.Text, 4);
			e.Row.Cells[6].Text = sumTotalAtGiveUp.ToString("#0.000");
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


