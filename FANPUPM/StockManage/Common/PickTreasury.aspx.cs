using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Purchase_PickTreasury : NBasePage, IRequiresSessionState
{
	private Treasury treasury = new Treasury();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvwTreasury.DataSource = this.treasury.GetList("tcode <> '0'");
			this.gvwTreasury.DataBind();
		}
	}
	protected void gvTreasury_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwTreasury.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


