using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Calendar_CalendarView : BasePage, IRequiresSessionState
{
	protected int RecordID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDID"].ToString());
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	private CalendarInfoAction cia
	{
		get
		{
			return new CalendarInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request["rid"] != null)
		{
			this.RecordID = Convert.ToInt32(base.Request["rid"]);
			this.PageBind();
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '001' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				this.RecordID,
				"'"
			}), 1);
		}
	}
	private void PageBind()
	{
		OACalendarInfo model = this.cia.GetModel(this.RecordID);
		if (model != null)
		{
			this.lbRecordDate.Text = Convert.ToDateTime(model.RecordDate).ToString("yyyy-MM-dd");
			this.lbContent.Text = model.Content;
			this.lbTitle.Text = model.Title;
			userManageDb userManageDb = new userManageDb();
			this.lbUserName.Text = userManageDb.GetUserName(model.UserCode);
			this.CBIsRemind.Checked = (model.IsRemind == "1");
		}
	}
}


