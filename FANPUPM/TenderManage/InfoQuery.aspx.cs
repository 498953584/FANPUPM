using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_InfoQuery : NBasePage, IRequiresSessionState
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
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 1, null, null);
		this.AspNetPager1.RecordCount = count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, true, 1, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, null, null);
		this.gvwProject.DataSource = all;
		this.gvwProject.DataBind();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		DataTable flowStateSummarizingInfo = TenderInfo.GetFlowStateSummarizingInfo(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, null, null);
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
	protected void gvwProject_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwProject.DataKeys[e.Row.RowIndex].Value.ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
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
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, this.txtName.Text, 1, null, null);
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
	protected void btnExport_Click(object sender, System.EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		string value = this.hfldSummarizingInfo.Value;
		ExcelHelper.ExportFooterExcel(reportData, "立项申请一览", "立项申请一览", "立项申请一览.xls", null, null, 0, value, base.Request.Browser.Browser);
	}
	public DataTable GetReportData()
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
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, this.txtName.Text, true, 1, this.AspNetPager1.CurrentPageIndex, 2147483647, null, null);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目状态");
		dataTable.Columns.Add("立项申请人");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("建设单位");
		dataTable.Columns.Add("项目类型");
		dataTable.Columns.Add("工程造价");
		dataTable.Columns.Add("工程工期");
		dataTable.Columns.Add("立项申请日期");
		dataTable.Columns.Add("流程状态");
		if (all.Rows.Count > 0)
		{
			for (int i = 0; i < all.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["序号"] = (i + 1).ToString();
				dataRow["项目状态"] = all.Rows[i]["StateText"].ToString();
				dataRow["立项申请人"] = all.Rows[i]["Person"].ToString();
				dataRow["项目编号"] = all.Rows[i]["PrjCode"].ToString();
				dataRow["项目名称"] = all.Rows[i]["PrjName"].ToString();
				dataRow["建设单位"] = all.Rows[i]["WorkUnitName"].ToString();
				dataRow["项目类型"] = all.Rows[i]["PrjTypeName"].ToString();
				dataRow["工程造价"] = all.Rows[i]["PrjCost"].ToString();
				dataRow["工程工期"] = all.Rows[i]["Duration"].ToString();
				dataRow["立项申请日期"] = all.Rows[i]["InputDate"].ToString();
				dataRow["流程状态"] = this.GetStateStr(all.Rows[i]["ProjFlowSate"].ToString());
				dataTable.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = "合计";
			dataRow2["项目状态"] = "";
			dataRow2["立项申请人"] = "";
			dataRow2["项目编号"] = "";
			dataRow2["项目名称"] = "";
			dataRow2["建设单位"] = "";
			dataRow2["项目类型"] = "";
			dataRow2["工程造价"] = all.Compute("SUM(PrjCost)", string.Empty).ToString();
			dataRow2["工程工期"] = "";
			dataRow2["立项申请日期"] = "";
			dataRow2["流程状态"] = "";
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	private string GetStateStr(string state)
	{
		string result;
		if (state == "-1")
		{
			result = "未提交";
		}
		else
		{
			if (state == "-2")
			{
				result = "驳回";
			}
			else
			{
				if (state == "-3")
				{
					result = "重报";
				}
				else
				{
					if (state == "0")
					{
						result = "审核中";
					}
					else
					{
						if (state == "1")
						{
							result = "已审核";
						}
						else
						{
							result = "未审核";
						}
					}
				}
			}
		}
		return result;
	}
}


