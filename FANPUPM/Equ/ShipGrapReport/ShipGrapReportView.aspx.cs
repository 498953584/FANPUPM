using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipProductionReport_ShipProductionReportView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private PTPrjInfoService prjInfoSer = new PTPrjInfoService();
	private EquEquipmentService equipmentSer = new EquEquipmentService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
}


