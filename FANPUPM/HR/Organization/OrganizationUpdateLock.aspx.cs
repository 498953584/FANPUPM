using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_OrganizationUpdateLock : BasePage, IRequiresSessionState
{

	public HROrgAdjustAction hrAction
	{
		get
		{
			return new HROrgAdjustAction();
		}
	}
	public Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public string DepartmentCode
	{
		get
		{
			object obj = this.ViewState["DepartmentCode"];
			if (obj != null)
			{
				return (string)this.ViewState["DepartmentCode"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["DepartmentCode"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ic"] != null)
		{
			base.Response.Cache.SetNoStore();
			this.RecordID = new Guid(base.Request["ic"].ToString());
			this.EditDisplay();
		}
	}
	private void EditDisplay()
	{
		DataTable list = this.hrAction.GetList("RecordID='" + this.RecordID.ToString() + "'");
		if (list.Rows.Count > 0)
		{
			userManageDb userManageDb = new userManageDb();
			this.LbUserName.Text = userManageDb.GetUserName(list.Rows[0]["UserCode"].ToString());
			this.LbAdjustContent.Text = list.Rows[0]["AdjustContent"].ToString();
			this.LbAdjustReason.Text = list.Rows[0]["AdjustReason"].ToString();
			this.LbRecordDate.Text = Convert.ToDateTime(list.Rows[0]["RecordDate"].ToString()).ToShortDateString();
			this.LbRemark.Text = list.Rows[0]["Remark"].ToString();
			this.LbCorpCode.Text = BooksCommonClass.GetDepartmentName(list.Rows[0]["CorpCode"].ToString());
		}
	}
}


