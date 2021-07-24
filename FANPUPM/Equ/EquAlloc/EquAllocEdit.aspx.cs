using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquAlloc_EquAllocEdit : NBasePage, IRequiresSessionState
{
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
			this.BindState();
			this.BindInfo();
		}
	}
	private void BindState()
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		this.dropState.DataSource = (
			from cs in basicCodeListService.GetByType("EquState")
			orderby cs.ItemCode
			select cs).ToList<BasicCodeList>();
		this.dropState.DataTextField = "ItemName";
		this.dropState.DataValueField = "ItemCode";
		this.dropState.DataBind();
	}
	private void BindInfo()
	{
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
	}
}


