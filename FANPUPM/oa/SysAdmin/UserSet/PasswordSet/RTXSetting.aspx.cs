using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_RTXSetting : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			PtYhmcBll ptYhmcBll = new PtYhmcBll();
			this.txtRTXID.Text = ptYhmcBll.GetRTXID(base.UserCode);
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(this.txtRTXID.Text))
		{
			string text = this.txtRTXID.Text;
			string sql = string.Format("UPDATE PT_yhmc SET RTXID = '{0}' WHERE v_yhdm = '{1}'", text, base.UserCode);
			Common2.ExecuteNoQuery(sql);
			string message = "alert('系统提示：\\n\\n设置成功.');";
			base.RegisterScript(message);
		}
	}
}


