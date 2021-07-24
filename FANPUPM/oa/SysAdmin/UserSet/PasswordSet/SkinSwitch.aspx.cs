using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_SkinSwitch : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DropDownList1.SelectedValue = this.Session["SkinID"].ToString();
		}
	}
	protected void btnConfirm_Click(object sender, EventArgs e)
	{
		if (userManageDb.updateUserSkin(this.Session["yhdm"].ToString(), this.DropDownList1.SelectedValue))
		{
			this.JavaScriptControl1.Text = "alert('设置成功!');";
			if (this.cbUseNow.Checked)
			{
				JavaScriptControl expr_4A = this.JavaScriptControl1;
				expr_4A.Text += "top.history.go(0);";
				this.Session["SkinID"] = this.DropDownList1.SelectedValue;
				return;
			}
		}
		else
		{
			this.JavaScriptControl1.Text = "alert('设置失败!');";
		}
	}
}


