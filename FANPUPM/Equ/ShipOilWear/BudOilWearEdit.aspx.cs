using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipOilWear_BudOilWearEdit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["Id"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindBudEquOilWear();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.txtOilWearCode.Text.Trim();
		if (string.IsNullOrEmpty(value))
		{
			base.RegisterScript("top.ui.show('编码不能为空！')");
			return;
		}
	}
	private void BindBudEquOilWear()
	{
        
	}
}


