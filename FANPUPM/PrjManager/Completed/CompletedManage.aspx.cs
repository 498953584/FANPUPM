using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_Completed_CompletedManage : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjState, true, "所有", null, new System.Collections.Generic.List<int>
			{
				7,
				8,
				9,
				10,
				11,
				12,
				13
			});
			this.isCompletedApprove.Value = ConfigurationManager.AppSettings["IsCompletedApprove"].ToString();
			this.bindGv();
			int[] prjTypeCodes = new int[]
			{
				11,
				12,
				13
			};
			TypeList.BindDrop(this.dropModifyPrjState, prjTypeCodes, null, null);
			this.dropModifyPrjState.Items.RemoveAt(0);
		}
	}
	public void bindGv()
	{
		int recordCount = 0;
		DataTable completedManageList = PrjCompleted.GetCompletedManageList(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtPrjManager.Text.Trim(), this.dropPrjState.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtCompletedTime.Text.Trim(), base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, ref recordCount);
		this.AspNetPager1.RecordCount = recordCount;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvwPrjInfo.DataSource = completedManageList;
		this.gvwPrjInfo.DataBind();
		if (this.isCompletedApprove.Value == "1")
		{
			this.gvwPrjInfo.Columns[7].Visible = true;
			return;
		}
		this.gvwPrjInfo.Columns[7].Visible = false;
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void gvwPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
			if (text.Length == 5)
			{
				e.Row.Attributes["isMainContract"] = "True";
			}
			else
			{
				e.Row.Attributes["isMainContract"] = "False";
			}
			e.Row.Attributes["isPrimit"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["wfState"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[3].ToString();
			e.Row.Attributes["prjState"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[4].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		try
		{
			PrjCompleted.UpdateCompleted(this.hfldCheckedIds.Value, this.txtCompletedDate.Text.Trim(), this.txtCompletedNote.Value);
			base.RegisterScript("top.ui.show('执行成功');window.location = window.location;");
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('执行失败')");
		}
	}
}


