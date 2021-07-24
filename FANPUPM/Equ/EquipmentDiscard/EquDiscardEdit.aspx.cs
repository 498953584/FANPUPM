using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquipmentDiscard_EquDiscardEdit : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equSer = new EquEquipmentService();
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"];
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindInfo();
		}
	}
	private void BindInfo()
	{
		
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
	}
}


