using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_System_SystemInfoLock : BasePage, IRequiresSessionState
{

	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}
	private Guid RecordID
	{
		get
		{
			return (Guid)this.ViewState["RECORDID"];
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["ic"] != null)
		{
			this.RecordID = new Guid(base.Request["ic"]);
			this.GetPageData();
			this.hdnRecordId.Value = this.RecordID.ToString();
		}
	}
	protected void GetPageData()
	{
		new userManageDb();
		SystemInfoModel model = this.sia.GetModel(this.RecordID);
		this.LbSystemName.Text = model.SystemName;
		this.LbSignMan.Text = model.SignMan;
		this.LbRemark.Text = model.Remark;
		this.LbSignDate.Text = model.SignDate.ToString("yyyy-MM-dd");
	}
}


