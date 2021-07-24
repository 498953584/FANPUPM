using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_receiveGoods_ReciveEditList : NBasePage, IRequiresSessionState
{
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private SmPurchaseplanStock smPurchaseplanStock = new SmPurchaseplanStock();
	private sendNoteBll sendNote = new sendNoteBll();
	private sendGoodsBLL sendGoods = new sendGoodsBLL();
	private PtYhmcBll yhmc = new PtYhmcBll();
	private receiveNoteBLL receiveNote = new receiveNoteBLL();
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		this.gvPurchaseplan.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjGuid"]))
		{
			this.prjId = base.Request["prjGuid"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			this.lblProject.Text = pTPrjInfoService.GetById(this.prjId).PrjName;
		}
	}
	public void BindGv()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" where s.limits like '%" + base.UserCode + "%'");
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim().ToString()))
		{
			stringBuilder.Append(" and r.rnTime >='" + this.txtStartTime.Text.Trim() + "'");
		}
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim().ToString()))
		{
			stringBuilder.Append(" and r.rnTime < '" + Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0).ToString() + "'");
		}
		if (this.txtsnCode.Text.Trim().ToString() != "")
		{
			stringBuilder.Append(" and r.rnCode like '%" + this.txtsnCode.Text.Trim() + "%' ");
		}
		if (!string.IsNullOrEmpty(this.prjId))
		{
			stringBuilder.Append(" and s.prjCode='" + this.prjId + "' ");
		}
		else
		{
			stringBuilder.Append(" and s.prjCode is null ");
		}
		if (!string.IsNullOrEmpty(this.txtPeople.Value.Trim()))
		{
			stringBuilder.Append(" and PT_yhmc.v_xm like '%" + this.txtPeople.Value.Trim() + "%' ");
		}
		DataTable dataSource = new DataTable();
		dataSource = this.receiveNote.GetNodeInfo(stringBuilder.ToString());
		this.gvPurchaseplan.DataSource = dataSource;
		this.gvPurchaseplan.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPurchaseplan.PageIndex = e.NewPageIndex;
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString(),
				"','",
				text,
				"');"
			});
			try
			{
				e.Row.Attributes["id"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvPurchaseplan.DataKeys[e.Row.RowIndex].Values[0].ToString();
				Label label = e.Row.FindControl("labUser") as Label;
				label.Text = this.yhmc.GetModelById(label.Text.ToString()).v_xm;
			}
			catch
			{
			}
		}
	}
}


