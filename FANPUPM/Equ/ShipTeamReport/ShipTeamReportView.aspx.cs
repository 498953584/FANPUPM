using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipTeamReport_ShipTeamReportView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
		}
	}
	private string GetShipTypeName(object typeId)
	{
		string result = string.Empty;
		string a;
		if ((a = typeId.ToString()) != null)
		{
			if (!(a == "D"))
			{
				if (a == "E")
				{
					result = "其他船";
				}
			}
			else
			{
				result = "耙吸船";
			}
		}
		return result;
	}
}


