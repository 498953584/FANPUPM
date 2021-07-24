using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjMember : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
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
			TypeList.BindDrop(this.dropPrjState, prjTypeCodes, "", null);
			this.bindGv();
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void bindGv()
	{
		string prjCode = this.txtPrjCode.Text.Trim();
		string text = this.txtPrjName.Text;
		System.DateTime? start = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			start = new System.DateTime?(System.DateTime.Parse(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? end = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			end = new System.DateTime?(System.DateTime.Parse(this.txtEndTime.Text.Trim()));
		}
		int? memberFowState = null;
		if (!string.IsNullOrEmpty(this.dropWFState.SelectedValue))
		{
			memberFowState = new int?(int.Parse(this.dropWFState.SelectedValue));
		}
		int? prjState = null;
		if (!string.IsNullOrEmpty(this.dropPrjState.SelectedValue))
		{
			prjState = new int?(int.Parse(this.dropPrjState.SelectedValue));
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		DataTable all = PrjMember.GetAll(text, prjCode, start, end, memberFowState, prjState, base.UserCode, this.txtTenderPrjManager.Text.Trim(), this.AspNetPager1.CurrentPageIndex, NBasePage.pagesize);
		this.AspNetPager1.RecordCount = PrjMember.GetCount(text, prjCode, start, end, memberFowState, prjState, base.UserCode, this.txtTenderPrjManager.Text.Trim());
		this.gvwPrjInfo.DataSource = all;
		this.gvwPrjInfo.DataBind();
	}
	protected void gvwPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
			e.Row.Attributes["guid"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
			e.Row.Attributes["primit"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["Primit"].ToString();
			e.Row.Attributes["flowState"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["MemberFlowState"].ToString();
			string text = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["TypeCode"].ToString();
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


