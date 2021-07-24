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
public partial class oa_WorkManage_StorageManage_IndividualDrawApplyEdit : BasePage, IRequiresSessionState
{
	public OAOfficeResPersonalApplicationAction amAction
	{
		get
		{
			return new OAOfficeResPersonalApplicationAction();
		}
	}
	public int RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)this.ViewState["RECORDID"];
			}
			return 0;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPERATETYPE"];
			if (obj != null)
			{
				return (string)this.ViewState["OPERATETYPE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["OPERATETYPE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (base.Request["id"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["id"].ToString() != "")
			{
				this.InStorageID = int.Parse(base.Request["id"].ToString());
				this.HdnApplyPerson.Value = base.UserCode;
				this.txtApplyPerson.Text = userManageDb.GetCurrentUserInfo().UserName;
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("PARecordID=" + this.InStorageID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtApplyDate.Text = ((dataRow["ApplyDate"].ToString().Trim() == "") ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(dataRow["ApplyDate"].ToString().Trim()).ToShortDateString());
			this.txtApplyPerson.Text = BooksCommonClass.GetUserName(dataRow["ApplyMan"].ToString());
			this.HdnApplyPerson.Value = dataRow["ApplyMan"].ToString();
			this.txtUserPerson.Text = BooksCommonClass.GetUserName(dataRow["UseMan"].ToString());
			this.HdnUserPerson.Value = dataRow["UseMan"].ToString();
		}
	}
	private OAOfficeResPersonalApplication GetData()
	{
		return new OAOfficeResPersonalApplication
		{
			ACRecordID = Guid.Empty,
			ApplyDate = (this.txtApplyDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtApplyDate.Text.Trim()),
			ApplyMan = this.HdnApplyPerson.Value,
			CorpCode = userManageDb.GetCorpCode(this.HdnUserPerson.Value),
			IsComplete = "0",
			IsSubmit = "0",
			PARecordID = this.InStorageID,
			RecordDate = DateTime.Now,
			UseMan = this.HdnUserPerson.Value,
			UserCode = base.UserCode
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAOfficeResPersonalApplication data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.amAction.Add(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('添加成功!');returnValue=true;window.close();</script>");
			}
			else
			{
				this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可添加!');</script>");
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.amAction.Update(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('修改成功!');returnValue=true;window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可更新!');</script>");
		}
	}
}


