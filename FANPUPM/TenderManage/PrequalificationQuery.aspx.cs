using ASP;
using cn.justwin.BLL;
using cn.justwin.Project;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_PrequalificationQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, false, "", null, new System.Collections.Generic.List<int>
			{
				int.Parse(ProjectParameter.Initiate),
				int.Parse(ProjectParameter.QualificationPass),
				int.Parse(ProjectParameter.QualificationFail),
				int.Parse(ProjectParameter.GiveUpState)
			});
			this.bindGv();
		}
	}
	private void bindGv()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
		if (string.IsNullOrEmpty(this.dropPrjState.SelectedValue))
		{
			list = new System.Collections.Generic.List<int>
			{
				int.Parse(ProjectParameter.Initiate),
				int.Parse(ProjectParameter.Prequalification),
				int.Parse(ProjectParameter.QualificationPass),
				int.Parse(ProjectParameter.QualificationFail)
			};
		}
		else
		{
			list.Add(System.Convert.ToInt32(this.dropPrjState.SelectedValue));
		}
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
		{
			1
		};
		string text = this.txtName.Text;
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, 4, ProjectParameter.Initiate, "InitiateFlowState");
		this.AspNetPager1.RecordCount = count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, true, 4, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, ProjectParameter.Initiate, "InitiateFlowState");
		this.gvwProject.DataSource = all;
		this.gvwProject.DataBind();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < all.Rows.Count; i++)
		{
			if (all.Rows[i]["PrjState"].ToString() == ProjectParameter.Prequalification)
			{
				num++;
			}
			else
			{
				if (all.Rows[i]["PrjState"].ToString() == ProjectParameter.QualificationPass)
				{
					num2++;
				}
				else
				{
					if (all.Rows[i]["PrjState"].ToString() == ProjectParameter.QualificationFail)
					{
						num3++;
					}
					else
					{
						if (all.Rows[i]["PrjState"].ToString() == ProjectParameter.GiveUpState)
						{
							num4++;
						}
					}
				}
			}
		}
		string text2 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text3 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：正在预审",
			text2,
			num,
			text3,
			"项，预审通过",
			text2,
			num2,
			text3,
			"项，预审失败",
			text2,
			num3,
			text3,
			"项，放弃",
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
			System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
			if (string.IsNullOrEmpty(this.dropPrjState.SelectedValue))
			{
				list = new System.Collections.Generic.List<int>
				{
					int.Parse(ProjectParameter.Initiate),
					int.Parse(ProjectParameter.Prequalification),
					int.Parse(ProjectParameter.QualificationPass),
					int.Parse(ProjectParameter.QualificationFail)
				};
			}
			else
			{
				list.Add(System.Convert.ToInt32(this.dropPrjState.SelectedValue));
			}
			System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
			{
				1
			};
			decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, this.txtName.Text, 4, ProjectParameter.Initiate, "InitiateFlowState");
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
		ExcelHelper.ExportFooterExcel(reportData, "资格预审一览", "资格预审一览", "资格预审一览.xls", null, null, 0, value, base.Request.Browser.Browser);
	}
	public DataTable GetReportData()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
		if (string.IsNullOrEmpty(this.dropPrjState.SelectedValue))
		{
			list = new System.Collections.Generic.List<int>
			{
				int.Parse(ProjectParameter.Prequalification),
				int.Parse(ProjectParameter.QualificationPass),
				int.Parse(ProjectParameter.QualificationFail)
			};
		}
		else
		{
			list.Add(System.Convert.ToInt32(this.dropPrjState.SelectedValue));
		}
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
		{
			1
		};
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, this.txtName.Text, true, 4, this.AspNetPager1.CurrentPageIndex, 2147483647, ProjectParameter.Initiate, "InitiateFlowState");
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目状态");
		dataTable.Columns.Add("项目跟踪人");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("建设单位");
		dataTable.Columns.Add("工程造价");
		dataTable.Columns.Add("工程工期");
		dataTable.Columns.Add("立项申请日期");
		if (all.Rows.Count > 0)
		{
			for (int i = 0; i < all.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["序号"] = (i + 1).ToString();
				dataRow["项目状态"] = all.Rows[i]["StateText"].ToString();
				dataRow["项目跟踪人"] = all.Rows[i]["Person"].ToString();
				dataRow["项目编号"] = all.Rows[i]["PrjCode"].ToString();
				dataRow["项目名称"] = all.Rows[i]["PrjName"].ToString();
				dataRow["建设单位"] = all.Rows[i]["WorkUnitName"].ToString();
				dataRow["工程造价"] = all.Rows[i]["PrjCost"].ToString();
				dataRow["工程工期"] = all.Rows[i]["Duration"].ToString();
				dataRow["立项申请日期"] = all.Rows[i]["InputDate"].ToString();
				dataTable.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = "合计";
			dataRow2["项目状态"] = "";
			dataRow2["项目跟踪人"] = "";
			dataRow2["项目编号"] = "";
			dataRow2["项目名称"] = "";
			dataRow2["建设单位"] = "";
			dataRow2["工程造价"] = all.Compute("SUM(PrjCost)", string.Empty).ToString();
			dataRow2["工程工期"] = "";
			dataRow2["立项申请日期"] = "";
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	public string GetState(string state)
	{
		string result = "";
		if (state == ProjectParameter.Prequalification)
		{
			result = "<span style='color:green; font-size:20px;'>●</span>";
		}
		else
		{
			if (state == ProjectParameter.Initiate)
			{
				result = "<span style='color:green; font-size:20px;'>#</span>";
			}
			else
			{
				if (state == ProjectParameter.QualificationPass)
				{
					result = "<span style='color:green;font-size:20px; font-weight:bold;'>√</span>";
				}
				else
				{
					if (state == ProjectParameter.QualificationFail)
					{
						result = "<span style='color:green;font-size:20px;font-weight:bold;'>×</span>";
					}
					else
					{
						if (state == ProjectParameter.GiveUpState)
						{
							result = "<span style='color:green;font-size:20px;font-weight:bold;'>▼</span>";
						}
					}
				}
			}
		}
		return result;
	}
}


