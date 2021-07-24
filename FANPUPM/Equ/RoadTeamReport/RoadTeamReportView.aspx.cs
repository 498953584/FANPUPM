using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_RoadTeamReport_RoadTeamReportView : NBasePage, IRequiresSessionState
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
	protected string GetRoadTypeName(object typeId)
	{
		string result = string.Empty;
		string a;
		if ((a = typeId.ToString()) != null)
		{
			if (!(a == "G"))
			{
				if (!(a == "L"))
				{
					if (a == "E")
					{
						result = "其他设备";
					}
				}
				else
				{
					result = "装载机";
				}
			}
			else
			{
				result = "挖掘机";
			}
		}
		return result;
	}
}


