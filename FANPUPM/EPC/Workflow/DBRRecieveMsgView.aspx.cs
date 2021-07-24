using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_DBRRecieveMsgView : BasePage, IRequiresSessionState
{
	protected RecieveMsgAction rma
	{
		get
		{
			return new RecieveMsgAction();
		}
	}
	protected int RecordID
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["RECORDID"].ToString());
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && base.Request["rid"] != null)
		{
			this.RecordID = System.Convert.ToInt32(base.Request["rid"].ToString());
			this.PageBind();
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '014' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				this.RecordID,
				"'"
			}), 1);
		}
	}
	private void PageBind()
	{
		DataTable list = this.rma.GetList(this.RecordID, true);
		if (list.Rows.Count > 0)
		{
			userManageDb userManageDb = new userManageDb();
			this.lbRecieveUserCode.Text = userManageDb.GetUserName(list.Rows[0]["RecieveUserCode"].ToString());
			this.lblv_yhdm.Text = userManageDb.GetUserName(list.Rows[0]["v_yhdm"].ToString());
			this.lblInstanceCode.Text = list.Rows[0]["BusinessClassName"].ToString();
			this.divcontent.InnerHtml = list.Rows[0]["LookUrl"].ToString();
			this.lblTheOrder.Text = list.Rows[0]["TheOrder"].ToString();
		}
	}
}


