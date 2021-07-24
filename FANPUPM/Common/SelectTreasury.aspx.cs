using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.EasyUI;
using cn.justwin.stockBLL;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_SelectTreasury : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			cn.justwin.Domain.EasyUI.Treasury treasury = new cn.justwin.Domain.EasyUI.Treasury();
			cn.justwin.Domain.EasyUI.Treasury[] t = treasury.GetTreasury().ToArray<cn.justwin.Domain.EasyUI.Treasury>();
			this.hfldTreasuryJson.Value = JsonHelper.JsonSerializer<cn.justwin.Domain.EasyUI.Treasury[]>(t);
			this.hfldDepotType.Value = StockParameter.DepotType;
		}
	}
}


