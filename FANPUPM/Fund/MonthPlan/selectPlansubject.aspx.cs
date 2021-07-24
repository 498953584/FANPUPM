using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.MonthPlan;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_selectPlansubject : NBasePage, IRequiresSessionState
{
	private MonthDetailLogic bll = new MonthDetailLogic();
	private string _pageType = string.Empty;

	public string _PageType
	{
		get
		{
			return this._pageType;
		}
		set
		{
			this._pageType = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["plantype"] != null)
			{
				this._pageType = base.Request.QueryString["plantype"].ToString();
			}
			else
			{
				this._pageType = "payout";
			}
			this.bindData();
		}
	}
	private void bindData()
	{
		DataTable planDetailobject = this.bll.GetPlanDetailobject(this._pageType);
		this.GridView1.DataSource = planDetailobject;
		this.GridView1.DataBind();
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GridView1.PageIndex = e.NewPageIndex;
		this.bindData();
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = (e.Row.RowIndex + 1).ToString();
			e.Row.Attributes["ondblclick "] = "dbClickRow('" + dataRowView["Plansubject"].ToString() + "')";
		}
	}
}


