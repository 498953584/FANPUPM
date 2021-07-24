using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_Completed_PrjCompletedList : NBasePage, IRequiresSessionState
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
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "所有", null, true);
			this.bindGv();
		}
	}
	public void bindGv()
	{
		int recordCount = 0;
		DataTable allPrj = PrjCompleted.GetAllPrj(this.txtPrjCode.Text, this.txtPrjName.Text, this.txtPrjManager.Text, this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.txtOwner.Text, this.dropPrjState.SelectedValue, this.dropWFState.SelectedValue, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, ref recordCount);
		this.AspNetPager1.RecordCount = recordCount;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvwPrjInfo.DataSource = allPrj;
		this.gvwPrjInfo.DataBind();
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
			e.Row.Attributes["guid"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["limit"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["state"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[3].ToString();
			e.Row.Attributes["prjstate"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[4].ToString();
			string text = this.gvwPrjInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
			if (text.Length == 5)
			{
				e.Row.Attributes["isMainContract"] = "True";
				return;
			}
			e.Row.Attributes["isMainContract"] = "False";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


