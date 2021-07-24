using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_HRLayoutEdit : BasePage, IRequiresSessionState
{
	public HROrgManpowerPlanAction hrAction
	{
		get
		{
			return new HROrgManpowerPlanAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
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

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["t"] == null)
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
			this.HdnRecordCode.Value = this.RecordID.ToString();
			if (this.OperateType == "add")
			{
				this.AddDisplay();
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
				this.FileUp_Bind();
			}
			if (this.OperateType == "view")
			{
				this.btnAdd.Enabled = false;
				this.FileUp_Bind();
			}
		}
		this.btnAnnex.Attributes["onclick"] = "javascript:if(!UpFile('" + this.OperateType + "')) return false;";
	}
	private void AddDisplay()
	{
		this.lblAddPerson.Text = BooksCommonClass.GetUserName(this.Session["yhdm"].ToString());
		this.txtLayoutDate.Text = DateTime.Now.ToShortDateString();
	}
	private void EditDisplay()
	{
		HROrgManpowerPlan model = this.hrAction.GetModel(this.RecordID);
		if (model != null)
		{
			this.txtLayoutName.Text = model.PlanName;
			this.txtLayoutDate.Text = model.RecordDate.ToShortDateString();
			this.lblAddPerson.Text = BooksCommonClass.GetUserName(model.UserCode);
			this.txtRemark.Text = model.Remark;
		}
	}
	private void FileUp_Bind()
	{
		string text = "";
		ArrayList annexList = this._AnnexAction.GetAnnexList(this.RecordID.ToString(), 0, 28);
		if (annexList.Count > 0)
		{
			for (int i = 0; i < annexList.Count; i++)
			{
				text = text + ((AnnexInfo)annexList[i]).OriginalName + ",";
			}
			this.lblAnnex.Text = text.Trim(new char[]
			{
				','
			});
		}
	}
	private HROrgManpowerPlan GetData()
	{
		return new HROrgManpowerPlan
		{
			PlanName = this.txtLayoutName.Text,
			RecordDate = (this.txtLayoutDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtLayoutDate.Text.Trim()),
			RecordID = this.RecordID,
			Remark = this.txtRemark.Text,
			UserCode = this.Session["yhdm"].ToString(),
			CorpCode = this.Session["CorpCode"].ToString(),
			AuditState = -1
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		HROrgManpowerPlan data = this.GetData();
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
	protected void btnAnnex_Click(object sender, EventArgs e)
	{
		this.FileUp_Bind();
	}
}


