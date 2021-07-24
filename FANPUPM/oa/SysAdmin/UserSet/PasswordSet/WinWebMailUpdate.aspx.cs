using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_WinWebMailUpdate : BasePage, IRequiresSessionState
{

	protected dzyjwbyjAction da
	{
		get
		{
			return new dzyjwbyjAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.PageBind();
		}
	}
	protected void btMod_Click(object sender, EventArgs e)
	{
		if (this.da.Update(this.GetModel()) == 1)
		{
			this.js.Text = "alert('修改成功！');";
			return;
		}
		this.js.Text = "alert('修改失败！');";
	}
	protected void PageBind()
	{
		DataTable list = this.da.GetList(" v_yhdm = '" + this.Session["yhdm"].ToString() + "'");
		if (list.Rows.Count > 0)
		{
			this.txtwbyjmc.Text = list.Rows[0]["v_wbyjmc"].ToString();
			this.txtpassword.Text = list.Rows[0]["v_password"].ToString();
			return;
		}
		userManageDb userManageDb = new userManageDb();
		dzyjwbyjModel model = this.da.GetModel(this.Session["yhdm"].ToString());
		model.v_yhdm = this.Session["yhdm"].ToString();
		model.v_xm = userManageDb.GetUserName(this.Session["yhdm"].ToString());
		model.v_password = "";
		model.v_wbyjmc = "";
		this.da.Add(model);
	}
	protected dzyjwbyjModel GetModel()
	{
		dzyjwbyjModel model = this.da.GetModel(this.Session["yhdm"].ToString());
		model.v_wbyjmc = this.txtwbyjmc.Text.ToString();
		model.v_password = this.txtpassword.Text.ToString();
		return model;
	}
}


