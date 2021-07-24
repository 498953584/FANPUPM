using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_SetPrjPrivilege : NBasePage, IRequiresSessionState
{
	private PTPrjInfoZTBUserService ztbUserSev = new PTPrjInfoZTBUserService();
	private PrivBusiDataRoleService DataRoleSev = new PrivBusiDataRoleService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			int[] prjTypeCodes = new int[]
			{
				5,
				17,
				7,
				8,
				9,
				10,
				11,
				12,
				13
			};
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, prjTypeCodes, "", null);
			TypeList.BindDrop(this.dropProperty, "ProjectProperty", "", null, true);
			this.BindGv();
		}
	}
	public void BindGv()
	{
		int recordCount = 0;
		DataTable prjPrivilege = ProjectInfo.GetPrjPrivilege(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtTenderPrjManager.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, this.dropPrjSource.SelectedValue, this.dropProperty.SelectedValue.Trim(), this.txtProjPeopleDep.Text.Trim(), this.dropPrjKindClass.SelectedValue.Trim(), ref recordCount);
		this.AspNetPager1.RecordCount = recordCount;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvDataInfo.DataSource = prjPrivilege;
		this.gvDataInfo.DataBind();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["limit"] = this.gvDataInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
			string text2 = this.gvDataInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["typeCode"] = text2;
			if (text2.Length == 5)
			{
				e.Row.Attributes["isMainContract"] = "True";
			}
			else
			{
				e.Row.Attributes["isMainContract"] = "False";
			}
			string userNamesByGuid = this.ztbUserSev.GetUserNamesByGuid(text);
			e.Row.Cells[6].Text = StringUtility.GetStr(userNamesByGuid, 25);
			e.Row.Cells[6].ToolTip = userNamesByGuid;
			string roleNamesByBusiDataRole = this.DataRoleSev.GetRoleNamesByBusiDataRole(text);
			e.Row.Cells[7].Text = StringUtility.GetStr(roleNamesByBusiDataRole, 25);
			e.Row.Cells[7].ToolTip = roleNamesByBusiDataRole;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected string ViewInfo(string prjGuid, bool isTender)
	{
		if (isTender)
		{
			return string.Format("viewopen('/TenderManage/InfoView.aspx?ic={0}')", prjGuid);
		}
		return string.Format("viewopen('/PrjManager/PrjInfoView.aspx?ic={0}')", prjGuid);
	}
}


