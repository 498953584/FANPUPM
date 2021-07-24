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
public partial class TenderManage_AllbidQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, false, "", null, new System.Collections.Generic.List<int>
			{
				5,
				6
			});
			this.bindGv();
		}
	}
	private void bindGv()
	{
		System.Collections.Generic.List<int> prjState = this.GetPrjState();
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "");
		this.AspNetPager1.RecordCount = count;
		this.gvwProject.DataSource = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, false, 3, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, "", "");
		this.gvwProject.DataBind();
		string value = string.Empty;
		if (this.dropPrjState.SelectedValue != string.Empty)
		{
			value = this.dropPrjState.SelectedValue;
		}
		if (string.IsNullOrEmpty(value))
		{
			System.Collections.Generic.List<int> prjState2 = new System.Collections.Generic.List<int>
			{
				5
			};
			int count2 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState2, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "");
			prjState2 = new System.Collections.Generic.List<int>
			{
				6
			};
			int count3 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState2, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "");
			string text = "<span style='margin-left:3px;margin-right:3px;'>";
			string text2 = "</span>";
			this.lblTotal.Text = string.Concat(new object[]
			{
				"汇总：中标",
				text,
				count2,
				text2,
				"项，落标",
				text,
				count3,
				text2,
				"项"
			});
			return;
		}
		int num = 0;
		int num2 = 0;
		System.Collections.Generic.List<int> prjState3 = new System.Collections.Generic.List<int>
		{
			System.Convert.ToInt32(value)
		};
		int count4 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState3, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "");
		if (System.Convert.ToInt32(value) == 5)
		{
			num = count4;
		}
		else
		{
			num2 = count4;
		}
		string text3 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text4 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：中标",
			text3,
			num,
			text4,
			"项，落标",
			text3,
			num2,
			text4,
			"项"
		});
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
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, 3, "", "");
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
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>
		{
			5,
			6
		};
		if (this.dropPrjState.SelectedValue != "")
		{
			int item = int.Parse(this.dropPrjState.SelectedValue);
			list.Clear();
			list.Add(item);
		}
		return list;
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<int> prjState = this.GetPrjState();
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtName.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, null, base.UserCode, this.txtTenderPrjManager.Text, false, 3, this.AspNetPager1.CurrentPageIndex, 2147483647, "", "");
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


