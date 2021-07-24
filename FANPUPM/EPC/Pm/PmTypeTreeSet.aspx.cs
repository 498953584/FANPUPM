using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PmTypeTreeSet : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			try
			{
				this.DropDownList1.SelectedValue = this.Session["PmSet"].ToString();
			}
			catch
			{
				this.DropDownList1.SelectedValue = "0";
			}
		}
	}
	protected void btnConfirm_Click(object sender, EventArgs e)
	{
		if (PmTypeAction.SetPmType(this.Session["yhdm"].ToString(), this.DropDownList1.SelectedValue))
		{
			this.JavaScriptControl1.Text = "top.ui.alert('设置成功!');";
			if (this.cbUseNow.Checked)
			{
				this.Session["PmSet"] = this.DropDownList1.SelectedValue;
				return;
			}
		}
		else
		{
			this.JavaScriptControl1.Text = "top.ui.alert('设置失败!');";
		}
	}
}


