using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IFCostDetails : NBasePage, IRequiresSessionState
{
	private BudIndirectDiaryDetailsService IndiDetailSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryDetails indirDetails = new BudIndirectDiaryDetails();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDetails();
		}
		this.hfldAdjunctPath.Value = ConfigHelper.Get("IndirectCost");
	}
	protected void BindDetails()
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.gvDiaryDetails.DataSource = (
				from p in this.IndiDetailSer
				where p.InDiaryId == this.Request["id"]
				orderby p.No
				select p).ToList<BudIndirectDiaryDetails>();
		}
		else
		{
			this.gvDiaryDetails.DataSource = null;
		}
		this.gvDiaryDetails.DataBind();
	}
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["InDiaryDetailsId"].ToString();
		}
	}
	public string CBSName(string CBSCode)
	{
		CostAccounting byCode = CostAccounting.GetByCode(CBSCode);
		if (byCode == null)
		{
			return string.Empty;
		}
		return byCode.Name;
	}
}


