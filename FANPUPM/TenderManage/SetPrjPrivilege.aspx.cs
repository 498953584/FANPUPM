using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_SetPrjPrivilege : NBasePage, IRequiresSessionState
{
	private PTPrjInfoZTBUserService ztbUserSev = new PTPrjInfoZTBUserService();
	private PrivBusiDataRoleService DataRoleSev = new PrivBusiDataRoleService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			TypeList.BindDrop(this.dropPrjState, false, "", null);
			TypeList.BindDrop(this.dropProperty, "ProjectProperty", "", null, true);
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string text = this.txtEndTime.Text.Trim();
		if (!string.IsNullOrEmpty(text))
		{
			text = System.Convert.ToDateTime(text).AddDays(1.0).ToString("yyyyMMdd");
		}
		this.AspNetPager1.RecordCount = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
		DataTable tenderPrivilege = TenderInfo.GetTenderPrivilege(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvDataInfo.DataSource = tenderPrivilege;
		this.gvDataInfo.DataBind();
		string text2 = string.Empty;
		if (this.dropPrjState.SelectedValue != string.Empty)
		{
			text2 = this.dropPrjState.SelectedValue;
		}
		if (string.IsNullOrEmpty(text2))
		{
			string prjState = "5";
			int tenderPrivilegeCount = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "6";
			int tenderPrivilegeCount2 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "4";
			int tenderPrivilegeCount3 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "1";
			int tenderPrivilegeCount4 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "2";
			int tenderPrivilegeCount5 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "3";
			int tenderPrivilegeCount6 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "14";
			int tenderPrivilegeCount7 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "15";
			int tenderPrivilegeCount8 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			prjState = "16";
			int tenderPrivilegeCount9 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
			string text3 = "<span style='margin-left:3px;margin-right:3px;'>";
			string text4 = "</span>";
			this.lblTotal.Text = string.Concat(new object[]
			{
				"汇总：中标",
				text3,
				tenderPrivilegeCount,
				text4,
				"项，落标",
				text3,
				tenderPrivilegeCount2,
				text4,
				"项，投标",
				text3,
				tenderPrivilegeCount3,
				text4,
				"项, 信息预立项",
				text3,
				tenderPrivilegeCount4,
				text3,
				"项,信息立项",
				text3,
				tenderPrivilegeCount5,
				text4,
				"项，启动",
				text3,
				tenderPrivilegeCount6,
				text4,
				"项, 资格预审",
				text3,
				tenderPrivilegeCount7,
				text4,
				"项, 预审通过",
				text3,
				tenderPrivilegeCount8,
				text4,
				"项，预审失败",
				text3,
				tenderPrivilegeCount9,
				text4,
				"项"
			});
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		string prjState2 = text2;
		int tenderPrivilegeCount10 = TenderInfo.GetTenderPrivilegeCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtStartTime.Text.Trim(), text, this.txtTenderPrjManager.Text.Trim(), prjState2, base.UserCode, this.dropPrjKindClass.SelectedValue, this.txtProjPeopleDep.Text.Trim(), this.dropProperty.SelectedValue.Trim());
		switch (System.Convert.ToInt32(text2))
		{
		case 1:
			num4 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 2:
			num5 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 3:
			num6 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 4:
			num3 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 5:
			num = tenderPrivilegeCount10;
			goto IL_8BC;
		case 6:
			num2 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 14:
			num7 = tenderPrivilegeCount10;
			goto IL_8BC;
		case 15:
			num8 = tenderPrivilegeCount10;
			goto IL_8BC;
		}
		num9 = tenderPrivilegeCount10;
		IL_8BC:
		string text5 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text6 = "</span>";
		this.lblTotal.Text = string.Concat(new object[]
		{
			"汇总：中标",
			text5,
			num,
			text6,
			"项，落标",
			text5,
			num2,
			text6,
			"项，投标",
			text5,
			num3,
			text6,
			"项, 信息预立项",
			text5,
			num4,
			text5,
			"项,信息立项",
			text5,
			num5,
			text6,
			"项，启动",
			text5,
			num6,
			text6,
			"项, 资格预审",
			text5,
			num7,
			text6,
			"项, 预审通过",
			text5,
			num8,
			text6,
			"项，预审失败",
			text5,
			num9,
			text6,
			"项"
		});
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
			e.Row.Attributes["id"] = text;
			string userNamesByGuid = this.ztbUserSev.GetUserNamesByGuid(text);
			string roleNamesByBusiDataRole = this.DataRoleSev.GetRoleNamesByBusiDataRole(text);
			e.Row.Cells[6].Text = StringUtility.GetStr(userNamesByGuid, 25);
			e.Row.Cells[6].ToolTip = userNamesByGuid;
			e.Row.Cells[7].Text = StringUtility.GetStr(roleNamesByBusiDataRole, 25);
			e.Row.Cells[7].ToolTip = roleNamesByBusiDataRole;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


