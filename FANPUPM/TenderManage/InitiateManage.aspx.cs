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
public partial class TenderManage_InitiateManage : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, false, "", null, new System.Collections.Generic.List<int>
			{
				2,
				3,
				18,
				19
			});
			this.hfldUserCode.Value = base.UserCode;
			this.bindGv();
		}
	}
	protected void bindGv()
	{
		System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>
		{
			2,
			3,
			18,
			19
		};
		if (this.dropPrjState.SelectedValue != "")
		{
			list.Clear();
			list.Add(int.Parse(this.dropPrjState.SelectedValue));
		}
		System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>(1);
		string text = this.txtName.Text;
		int count = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
		this.AspNetPager1.RecordCount = count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = TenderInfo.GetAll(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, list, flowState, base.UserCode, text, false, 4, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, ProjectParameter.Approval, null);
		this.gvDataInfo.DataSource = all;
		this.gvDataInfo.DataBind();
		string value = string.Empty;
		if (this.dropPrjState.SelectedValue != string.Empty)
		{
			value = this.dropPrjState.SelectedValue;
		}
		if (string.IsNullOrEmpty(value))
		{
			System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
			{
				2
			};
			int count2 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
			prjState = new System.Collections.Generic.List<int>
			{
				3
			};
			int count3 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
			prjState = new System.Collections.Generic.List<int>
			{
				19
			};
			int count4 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
			prjState = new System.Collections.Generic.List<int>
			{
				18
			};
			int count5 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
			string text2 = "<span style='margin-left:3px;margin-right:3px;'>";
			string text3 = "</span>";
			this.lblTotal.Text = string.Concat(new object[]
			{
				"汇总：信息立项",
				text2,
				count2,
				text3,
				"项，报名通过",
				text2,
				count3,
				text3,
				"项，报名不通过",
				text2,
				count4,
				text3,
				"项，放弃",
				text2,
				count5,
				text3,
				"项"
			});
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		System.Collections.Generic.List<int> prjState2 = new System.Collections.Generic.List<int>
		{
			System.Convert.ToInt32(value)
		};
		int count6 = TenderInfo.GetCount(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState2, flowState, base.UserCode, text, 4, ProjectParameter.Approval, null);
		if (System.Convert.ToInt32(value) == 2)
		{
			num = count6;
		}
		else
		{
			if (System.Convert.ToInt32(value) == 3)
			{
				num2 = count6;
			}
			else
			{
				if (System.Convert.ToInt32(value) == 19)
				{
					num4 = count6;
				}
				else
				{
					if (System.Convert.ToInt32(value) == 18)
					{
						num3 = count6;
					}
				}
			}
		}
		string text4 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text5 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：信息立项",
			text4,
			num,
			text5,
			"项，报名通过",
			text4,
			num2,
			text5,
			"项, 报名不通过",
			text4,
			num4,
			text5,
			"项，放弃",
			text4,
			num3,
			text5,
			"项"
		});
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["state"] = text;
			e.Row.Attributes["OldState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["OldState"].ToString();
			if (text == "18")
			{
				e.Row.Attributes["FlowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["GiveUpFlowState"].ToString();
			}
			else
			{
				e.Row.Attributes["FlowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["InitiateFlowState"].ToString();
			}
			if (text == ProjectParameter.Approval)
			{
				e.Row.Cells[9].Text = "";
				return;
			}
		}
		else
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				e.Row.Cells[0].Text = "合计";
				System.Collections.Generic.List<int> prjState = new System.Collections.Generic.List<int>
				{
					2,
					3
				};
				System.Collections.Generic.List<int> flowState = new System.Collections.Generic.List<int>
				{
					1
				};
				decimal sumTotal = TenderInfo.GetSumTotal(this.txtPrjName.Text, this.txtPrjCode.Text, this.txtOwner.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, prjState, flowState, base.UserCode, this.txtName.Text, 4, ProjectParameter.Approval, null);
				e.Row.Cells[6].Text = sumTotal.ToString("#0.000");
				e.Row.Cells[6].Style.Add("text-align", "right");
				e.Row.Cells[6].CssClass = "decimal_input";
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


