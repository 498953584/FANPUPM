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
public partial class oa_WorkManage_StorageManage_DepartmentDrawApplyEdit : BasePage, IRequiresSessionState
{
	public OAOfficeResDepartmentApplicationAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationAction();
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
	public Guid InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["INSTORAGEID"];
			}
			return Guid.NewGuid();
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
				this.InStorageID = new Guid(base.Request["id"].ToString());
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
				return;
			}
			this.AddDisplay();
		}
	}
	private void AddDisplay()
	{
		this.txtApplyDate.Text = DateTime.Now.ToShortDateString();
		this.txtApplyPerson.Text = BooksCommonClass.GetUserName(base.UserCode);
		string personInfo = OAOfficeCommonClas.GetPersonInfo(base.UserCode, "i_bmdm");
		this.txtApplyDept.Text = BooksCommonClass.GetDepartmentName(int.Parse(personInfo));
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("DARecordID='" + this.InStorageID + "'");
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtApplyDate.Text = ((dataRow["ApplyDate"].ToString().Trim() == "") ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(dataRow["ApplyDate"].ToString().Trim()).ToShortDateString());
			this.txtApplyPerson.Text = BooksCommonClass.GetUserName(dataRow["ApplyMan"].ToString());
			this.txtApplyDept.Text = BooksCommonClass.GetDepartmentName(int.Parse(dataRow["ApplyDepartment"].ToString()));
			this.txtRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private OAOfficeResDepartmentApplication GetData()
	{
		OAOfficeResDepartmentApplication oAOfficeResDepartmentApplication = new OAOfficeResDepartmentApplication();
		oAOfficeResDepartmentApplication.ACRecordID = Guid.Empty;
		oAOfficeResDepartmentApplication.ApplyDate = ((this.txtApplyDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtApplyDate.Text.Trim()));
		string personInfo = OAOfficeCommonClas.GetPersonInfo(base.UserCode, "i_bmdm");
		oAOfficeResDepartmentApplication.ApplyDepartment = ((personInfo == "") ? 0 : int.Parse(personInfo));
		oAOfficeResDepartmentApplication.ApplyMan = base.UserCode;
		oAOfficeResDepartmentApplication.AuditState = -1;
		oAOfficeResDepartmentApplication.CorpCode = this.Session["CorpCode"].ToString();
		if (this.OperateType == "upd")
		{
			oAOfficeResDepartmentApplication.DARecordID = this.InStorageID;
		}
		else
		{
			oAOfficeResDepartmentApplication.DARecordID = Guid.NewGuid();
		}
		oAOfficeResDepartmentApplication.IsAbove = "0";
		oAOfficeResDepartmentApplication.IsComplete = "0";
		oAOfficeResDepartmentApplication.IsSubmit = "0";
		oAOfficeResDepartmentApplication.RecordDate = DateTime.Now;
		oAOfficeResDepartmentApplication.UserCode = base.UserCode;
		oAOfficeResDepartmentApplication.Remark = this.txtRemark.Text.Trim();
		return oAOfficeResDepartmentApplication;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAOfficeResDepartmentApplication data = this.GetData();
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


