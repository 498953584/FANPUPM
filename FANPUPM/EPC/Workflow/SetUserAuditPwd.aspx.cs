using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_SetUserAuditPwd : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	protected void InitPage()
	{
		string text = base.Request["hid"];
		if (!string.IsNullOrEmpty(text))
		{
			this.hiddenuser.Value = text;
			string[] array = text.Split(new char[44]);
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (i < array.Length - 1)
					{
						TextBox expr_4B = this.TbLoginName;
						expr_4B.Text = expr_4B.Text + PageHelper.QueryUser(this, array[i]) + ",";
					}
					else
					{
						TextBox expr_71 = this.TbLoginName;
						expr_71.Text += PageHelper.QueryUser(this, array[i]);
					}
				}
			}
		}
	}
	protected void btMod_Click(object sender, System.EventArgs e)
	{
		string text = this.tbNewPwd1.Text;
		string arg_17_0 = this.tbNewPwd2.Text;
		userManageDb userManageDb = new userManageDb();
		string[] array = this.hiddenuser.Value.Split(new char[44]);
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				userManageDb.updateUserAuditPwd(array[i], text.ToString());
			}
		}
		this.js.Text = "alert('审核密码修改成功，下次审核请使用新密码！');window.close();";
	}
}


