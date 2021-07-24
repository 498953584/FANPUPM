using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_FlowQuestion : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
	}
	protected void btnDeal_Click(object sender, System.EventArgs e)
	{
		string a = FlowChartAction.FlowChartDeal();
		if (a == "true")
		{
			base.RegisterScript("alert('系统提示：\\n执行成功')");
			return;
		}
		base.RegisterScript("alert('系统提示：\\n执行失败')");
	}
}


