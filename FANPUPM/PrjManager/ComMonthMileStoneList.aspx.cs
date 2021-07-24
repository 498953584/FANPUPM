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
public partial class PrjManager_ComMonthMileStoneList : NBasePage, IRequiresSessionState
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
		string sWhere = this.strwhere();
		string where = this.where();
		this.AspNetPager1.RecordCount = this.prjMileSer.getdepmentCount(where);
		DataTable dataSource = this.prjMileSer.GetdepmentprjMilestone(sWhere, where, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.depmentPrjMilestoneQuery.DataSource = dataSource;
		this.depmentPrjMilestoneQuery.DataBind();
	}
	protected string where()
	{
		int arg_0D_0 = System.DateTime.Now.Month;
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		string text = this.txtdepmentName.Text.Trim();
		if (!string.IsNullOrEmpty(text))
		{
			stringBuilder.AppendFormat(" AND v_BMMC LIKE'%{0}%' ", text);
		}
		return stringBuilder.ToString();
	}
	protected string strwhere()
	{
		int month = System.DateTime.Now.Month;
		int year = System.DateTime.Now.Year;
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		this.txtdepmentName.Text.Trim();
		stringBuilder.AppendFormat(" AND MONTH(RptDate)={0} AND YEAR(RptDate)={1} ", month, year);
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


