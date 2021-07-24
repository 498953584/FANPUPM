using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_MaterialBack_MaterialOutList : NBasePage, IRequiresSessionState
{
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStock = new OutStockBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	public void BindGv()
	{
		this.gvOutReserve.PageSize = NBasePage.pagesize;
		this.gvOutReserve.DataSource = this.outReserveBll.GetListArray("");
		this.gvOutReserve.DataBind();
	}
	protected void cbBox_CheckedChanged(object sender, EventArgs e)
	{
	}
	protected void getMaterialList(string code)
	{
		string strWhere = "where orcode='" + code + "'";
		this.gvSmMaterialBack.DataSource = this.outStock.GetListArray(strWhere);
		this.gvSmMaterialBack.DataBind();
	}
	protected void chkBox_CheckedChanged(object sender, EventArgs e)
	{
	}
}


