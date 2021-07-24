using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_basicset_SelectByBud : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private MeterialPlanStock materialStock = new MeterialPlanStock();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.Bind();
		}
	}
	public void Bind()
	{
		this.AspNetPager1.RecordCount = this.materialStock.GetResCountByBud(this.txtResCode.Text.Trim(), this.txtResName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, 1, this.hfldIsWBSRelevance.Value);
		DataTable resByBud = this.materialStock.GetResByBud(this.txtResCode.Text.Trim(), this.txtResName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, 1, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, this.hfldIsWBSRelevance.Value, this.txtTaskCode.Text.Trim());
		this.gvResourceBud.DataSource = resByBud;
		this.gvResourceBud.DataBind();
		if (this.hfldIsWBSRelevance.Value == "0")
		{
			this.gvResourceBud.Columns[2].Visible = false;
			this.gvResourceBud.Columns[3].Visible = false;
		}
	}
	protected void gvResourceBud_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = dataRowView["ResourceId"].ToString();
			e.Row.Attributes["resCode"] = dataRowView["ResourceCode"].ToString();
			e.Row.Attributes["price"] = dataRowView["ResourcePrice"].ToString();
			e.Row.Attributes["number"] = dataRowView["ResourceQuantity"].ToString();
			e.Row.Attributes["TaskId"] = dataRowView["TaskId"].ToString();
			e.Row.Attributes["TaskName"] = dataRowView["TaskName"].ToString();
			e.Row.Cells[0].Attributes["id"] = dataRowView["ResourceCode"].ToString();
		}
	}
	protected void btnSertch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.Bind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.Bind();
	}
}


