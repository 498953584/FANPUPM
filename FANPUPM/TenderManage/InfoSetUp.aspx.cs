using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_InfoSetUp : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjProperty, "ProjectProperty", "", null, true);
			this.bindGv();
			string text = base.Request.UrlReferrer.ToString();
			if (text.Contains("/TenderManage/InfoAdd.aspx?Action=Add") && !string.IsNullOrEmpty(base.Request["returnMsg"]))
			{
				string msg = base.Request["returnMsg"].ToString();
				base.RegisterShow("系统提示", msg);
			}
		}
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["WFState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["ProjFlowSate"].ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				1,
				2
			};
			System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>();
			if (this.dropWFState.SelectedValue != "")
			{
				flowState = new System.Collections.Generic.List<int>
				{
					int.Parse(this.dropWFState.SelectedValue)
				};
			}
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text.Trim(), this.txtPrjCode.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), prjState, flowState, base.UserCode, this.txtName.Text.Trim(), 1, null, null);
			e.Row.Cells[8].Text = sumTotal.ToString("#0.000");
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
		}
	}
	protected void bindGv()
	{
		System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
		{
			1,
			2
		};
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>();
		if (this.dropWFState.SelectedValue != "")
		{
			flowState = new System.Collections.Generic.List<int>
			{
				int.Parse(this.dropWFState.SelectedValue)
			};
		}
		string text = this.txtName.Text;
		int tendersCount = TenderInfo.GetTendersCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.txtProjPeopleDep.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropWFState.SelectedValue, this.dropPrjProperty.SelectedValue, base.UserCode);
		this.AspNetPager1.RecordCount = tendersCount;
		DataTable tenders = TenderInfo.GetTenders(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.txtProjPeopleDep.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropWFState.SelectedValue, this.dropPrjProperty.SelectedValue, base.UserCode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvDataInfo.DataSource = tenders;
		this.gvDataInfo.DataBind();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		DataTable flowStateSummarizingInfo = TenderInfo.GetFlowStateSummarizingInfo(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, "", "");
		if (flowStateSummarizingInfo != null && flowStateSummarizingInfo.Rows.Count > 0)
		{
			DataRow dataRow = flowStateSummarizingInfo.Rows[0];
			num = DBHelper.GetInt(dataRow["100"]);
			num += DBHelper.GetInt(dataRow["-1"]);
			num += DBHelper.GetInt(dataRow["0"]);
			num2 = DBHelper.GetInt(dataRow["1"]);
			num3 = DBHelper.GetInt(dataRow["-2"]);
			num4 = DBHelper.GetInt(dataRow["-3"]);
		}
		string text2 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text3 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：正在申请",
			text2,
			num,
			text3,
			"项，已批准(立项)",
			text2,
			num2,
			text3,
			"项，已驳回",
			text2,
			num3,
			text3,
			"项，重报",
			text2,
			num4,
			text3,
			"项"
		});
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			System.Collections.Generic.IList<string> list = CsvHelper.ToList(this.hfldCheckedIds.Value);
			for (int i = 0; i < list.Count; i++)
			{
				string id = list[i];
				pTPrjInfoZTBService.Delete(id);
				pTPrjInfoService.Delete(id);
			}
			this.bindGv();
			base.RegisterScript("top.ui.show('删除成功');");
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.show('删除失败');");
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


