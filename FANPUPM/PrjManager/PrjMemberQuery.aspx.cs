using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjMemberQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "所有", null, true);
			TypeList.BindDrop(this.dropPrjState, true, "所有", null);
			this.bindGv();
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void bindGv()
	{
		string arg_0B_0 = this.txtPrjCode.Text;
		string arg_17_0 = this.txtPrjName.Text;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text))
		{
			new System.DateTime?(System.DateTime.Parse(this.txtStartTime.Text));
		}
		if (!string.IsNullOrEmpty(this.txtEndTime.Text))
		{
			new System.DateTime?(System.DateTime.Parse(this.txtEndTime.Text));
		}
		if (!string.IsNullOrEmpty(this.dropWFState.SelectedValue))
		{
			new int?(int.Parse(this.dropWFState.SelectedValue));
		}
		if (!string.IsNullOrEmpty(this.dropPrjState.SelectedValue))
		{
			new int?(int.Parse(this.dropPrjState.SelectedValue));
		}
	}
	protected void gvwPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
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


