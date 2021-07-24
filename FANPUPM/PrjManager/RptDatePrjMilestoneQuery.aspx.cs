using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_RptDatePrjMilestoneQuery : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindPrjInfo();
		}
	}
	protected void BindPrjInfo()
	{
		string where = this.strwhere();
		this.AspNetPager1.RecordCount = this.prjMileSer.GetRptDateMileCount(where);
		DataTable rptDateMileSouce = this.prjMileSer.GetRptDateMileSouce(where, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.RptDatePrjMilestoneQuery.DataSource = rptDateMileSouce;
		this.RptDatePrjMilestoneQuery.DataBind();
	}
	protected string strwhere()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		string text = this.txtRptDateStart.Text.Trim();
		string text2 = this.txtRptDateEnd.Text.Trim();
		if (string.IsNullOrEmpty(text))
		{
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(" AND RptDate<='{0}'", text2);
			}
		}
		else
		{
			if (string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(" AND RptDate>='{0}'", text);
			}
			else
			{
				stringBuilder.AppendFormat(" AND RptDate BETWEEN '{0}' AND '{1}'", text, text2);
			}
		}
		return stringBuilder.ToString();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPrjInfo();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindPrjInfo();
	}
	public string FormatRate(object ForeCast, object StoreAmount)
	{
		string result;
		try
		{
			result = Common2.FormatRate(System.Convert.ToDecimal(ForeCast) / System.Convert.ToDecimal(StoreAmount));
		}
		catch
		{
			result = "0.00%";
		}
		return result;
	}
}


