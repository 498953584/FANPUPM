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
public partial class HR_Organization_OrganizationUpdateEdit : BasePage, IRequiresSessionState
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
	public string DepartmentName
	{
		get
		{
			object obj = this.ViewState["DepartmentName"];
			if (obj != null)
			{
				return (string)this.ViewState["DepartmentName"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["DepartmentName"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["t"] == null || base.Request["dc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["t"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = new Guid(base.Request["rid"].ToString());
			}
			else
			{
				this.RecordID = Guid.NewGuid();
			}
			this.DepartmentCode = base.Request["dc"].ToString();
			this.DepartmentName = BooksCommonClass.GetDepartmentName(this.DepartmentCode);
			if (this.OperateType == "add")
			{
				this.AddDisplay();
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
	}
	private void AddDisplay()
	{
		this.txtRecordDate.Text = DateTime.Now.ToShortDateString();
		this.txtCorpCode.Text = this.DepartmentName;
	}
	private void EditDisplay()
	{
		DataTable list = this.hrAction.GetList("RecordID='" + this.RecordID.ToString() + "'");
		if (list.Rows.Count > 0)
		{
			this.txtAdjustContent.Text = list.Rows[0]["AdjustContent"].ToString();
			this.txtAdjustReason.Text = list.Rows[0]["AdjustReason"].ToString();
			this.txtRecordDate.Text = Convert.ToDateTime(list.Rows[0]["RecordDate"].ToString()).ToShortDateString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
		this.txtCorpCode.Text = this.DepartmentName;
	}
	private HROrgAdjust GetData()
	{
		return new HROrgAdjust
		{
			AdjustContent = this.txtAdjustContent.Text.Trim(),
			AdjustReason = this.txtAdjustReason.Text.Trim(),
			RecordID = this.RecordID,
			AuditState = -1,
			CorpCode = this.DepartmentCode,
			RecordDate = (this.txtRecordDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtRecordDate.Text),
			UserCode = this.Session["yhdm"].ToString(),
			Remark = this.txtRemark.Text.Trim()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		HROrgAdjust data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.hrAction.Add(data);
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
			int num = this.hrAction.Update(data);
			if (num > 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('修改成功!');returnValue=true;window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可更新!');</script>");
		}
	}
}


