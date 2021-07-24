using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjMilestoneDetailList : NBasePage, IRequiresSessionState
{
	private PrjMilestoneService prjMileSer = new PrjMilestoneService();
	private PrjMilestoneDetailService PrjMilestoneDetailSer = new PrjMilestoneDetailService();
	private string name = string.Empty;
	private string time = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["name"]))
		{
			this.name = base.Request["name"];
		}
		if (!string.IsNullOrEmpty(base.Request["time"]))
		{
			this.time = base.Request["time"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.CurrentPageIndex = 1;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		System.Collections.Generic.List<PrjMilestoneDetail> list = (
			from r in this.PrjMilestoneDetailSer
			where r.UserName == this.name.Trim() && r.RptDate == System.Convert.ToDateTime(this.time)
			select r).ToList<PrjMilestoneDetail>();
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		foreach (PrjMilestoneDetail current in list)
		{
			d += current.ForeCast;
			d2 += current.StoreAmount;
			d3 += current.NextForeCast;
			num += current.Stone1;
			num2 += current.Stone2;
			num3 += current.Stone3;
			num4 += current.Stone4;
			num5 += current.Stone5;
			num6 += current.Stone6;
			num7 += current.Stone7;
			num8 += current.Stone8;
			num9 += current.Stone9;
		}
		this.hfldForeCast.Value = d.ToString();
		this.hfldStoreAmount.Value = d2.ToString();
		this.hfldNextForeCast.Value = d3.ToString();
		this.hfldPrjCount.Value = list.Count<PrjMilestoneDetail>().ToString();
		this.hfldStone1.Value = num.ToString();
		this.hfldStone2.Value = num2.ToString();
		this.hfldStone3.Value = num3.ToString();
		this.hfldStone4.Value = num4.ToString();
		this.hfldStone5.Value = num5.ToString();
		this.hfldStone6.Value = num6.ToString();
		this.hfldStone7.Value = num7.ToString();
		this.hfldStone8.Value = num8.ToString();
		this.hfldStone9.Value = num9.ToString();
		this.AspNetPager1.RecordCount = list.Count<PrjMilestoneDetail>();
		System.Collections.Generic.IEnumerable<PrjMilestoneDetail> dataSource = list.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvPrjMilestoneDetail.DataSource = dataSource;
		this.gvPrjMilestoneDetail.DataBind();
	}
	protected void gvPrjMilestoneDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		new System.Collections.Generic.List<string>();
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvPrjMilestoneDetail.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = value;
		}
	}
}


