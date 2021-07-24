using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Conference_DBViewApply : BasePage, IRequiresSessionState
{

	public int RecordId
	{
		get
		{
			object obj = this.ViewState["RecordId"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["RecordId"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.RecordId = Convert.ToInt32(base.Request.QueryString["rid"]);
			this.setData(this.RecordId);
			PTDBSJAction pTDBSJAction = new PTDBSJAction();
			pTDBSJAction.Delete(string.Concat(new object[]
			{
				" v_lxbm = '009' and v_YHDM = '",
				this.Session["yhdm"].ToString(),
				"' and i_XGID = '",
				this.RecordId,
				"'"
			}), 1);
		}
	}
	protected void setData(int recordId)
	{
		DataTable dataTable = ConferenceManage.QueryApplyInfo(recordId);
		if (dataTable.Rows.Count == 1)
		{
			this.LbMeetingRoom.Text = dataTable.Rows[0]["MeetingRoom"].ToString();
			this.LbHumanNumber.Text = dataTable.Rows[0]["HumanNumber"].ToString();
			this.LbTitle.Text = dataTable.Rows[0]["Title"].ToString();
			this.LbUserDate.Text = dataTable.Rows[0]["UserDate"].ToString();
			this.LBBeginDate.Text = dataTable.Rows[0]["BeginHour"].ToString() + ":" + dataTable.Rows[0]["BeginMinute"].ToString();
			this.LbEndDate.Text = dataTable.Rows[0]["EndHour"].ToString() + ":" + dataTable.Rows[0]["EndMinute"].ToString();
			this.LbContent.Text = dataTable.Rows[0]["Content"].ToString();
			userManageDb userManageDb = new userManageDb();
			this.LbUserName.Text = userManageDb.GetUserName(dataTable.Rows[0]["UserCode"].ToString());
		}
	}
}


