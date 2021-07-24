using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IndirectMonthBudget : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
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
		this.hfldYear.Value = this.year;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (this.year == "zzjg")
		{
			this.gvBudget.DataSource = OrganizationBudget.GetAllPass(this.prjId);
		}
		else
		{
			this.gvBudget.DataSource = IndirectBudget.GetAllPass(this.prjId);
		}
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			int index = 3;
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
			HtmlAnchor htmlAnchor = e.Row.Cells[index].FindControl("moreMonthBudgets") as HtmlAnchor;
			if (htmlAnchor != null)
			{
				if (e.Row.RowIndex == 0)
				{
					htmlAnchor.Visible = false;
					return;
				}
				string text = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
				string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["AccountAmount"].ToString();
				htmlAnchor.Attributes["onclick"] = string.Concat(new string[]
				{
					"openWindow('",
					text,
					"','",
					text2,
					"')"
				});
			}
		}
	}
}


