using ASP;
using cn.justwin.BLL;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipElseReport_ShipElseReportEdit : NBasePage, IRequiresSessionState
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
			this.BindReport();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			
			stringBuilder.Append("winclose('ShipElseReportEdit', 'ShipElseReportList.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	private void BindReport()
	{
        
	}
}


