using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using System;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjTreeMemberQuery : NBasePage, IRequiresSessionState
{
	private PTPrjInfoService prjInfoSer = new PTPrjInfoService();
	private PTPrjInfoZTBService ztbSer = new PTPrjInfoZTBService();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.bindGv();
			this.hfldAdjunctPath.Value = WebConfigurationManager.AppSettings["ProjectFile"];
		}
	}
	protected void bindGv()
	{
		try
		{
			if (PrjMember.IsPrimit(this.prjId, base.UserCode) && PrjMember.GetFlowState(this.prjId) == "1")
			{
				string memberName = this.txtName.Text.Trim();
				string postName = this.txtPost.Text.Trim();
				string educationalBackground = this.txtEducationalBackground.Text.Trim();
				string technical = this.txtTechnical.Text.Trim();
				this.AspNetPager1.PageSize = NBasePage.pagesize;
				this.AspNetPager1.RecordCount = PrjMember.GetMembersCount(this.prjId, memberName, postName, educationalBackground, string.Empty, technical);
				int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
				this.gvwPrjMembers.DataSource = PrjMember.GetMembers(this.prjId, memberName, postName, educationalBackground, string.Empty, technical, currentPageIndex, this.AspNetPager1.PageSize);
			}
		}
		catch
		{
			this.gvwPrjMembers.DataSource = null;
		}
		this.gvwPrjMembers.DataBind();
	}
	protected void gvwPrjMembers_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvwPrjMembers.DataKeys[e.Row.RowIndex]["PrjMemberId"].ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	protected void btnQueryInfo_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


