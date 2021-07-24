using ASP;
using cn.justwin.stockBLL;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class StockManage_UserControl_TreasuryControl : System.Web.UI.UserControl
{

	public TreeView TreeView
	{
		get
		{
			return this.tvTreasury;
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			Treasury treasury = new Treasury();
			treasury.ParseTreasuryList(this.tvTreasury.Nodes, string.Empty, string.Empty);
		}
	}
}


