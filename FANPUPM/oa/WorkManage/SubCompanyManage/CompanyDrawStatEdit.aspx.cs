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
public partial class oa_WorkManage_SubCompanyManage_CompanyDrawStatEdit : BasePage, IRequiresSessionState
{
	public OAOfficeResApplicationCollectAction amAction
	{
		get
		{
			return new OAOfficeResApplicationCollectAction();
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
				this.RecordID = new Guid(base.Request["id"].ToString());
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
		this.txtYear.Text = DateTime.Now.Year.ToString();
		this.DDLApplyType.SelectedValue = "D";
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("ACRecordID='" + this.RecordID + "'");
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.DDLApplyType.SelectedValue = dataRow["ApplyType"].ToString();
			this.txtYear.Text = dataRow["iYear"].ToString();
			this.txtMonth.Text = dataRow["iMonth"].ToString();
			this.txtRemark.Text = dataRow["Remark"].ToString();
		}
	}
	private OAOfficeResApplicationCollect GetData()
	{
		OAOfficeResApplicationCollect oAOfficeResApplicationCollect = new OAOfficeResApplicationCollect();
		if (this.OperateType == "upd")
		{
			oAOfficeResApplicationCollect.ACRecordID = this.RecordID;
		}
		else
		{
			oAOfficeResApplicationCollect.ACRecordID = Guid.NewGuid();
		}
		oAOfficeResApplicationCollect.ApplyType = this.DDLApplyType.SelectedValue;
		oAOfficeResApplicationCollect.AuditState = -1;
		oAOfficeResApplicationCollect.CorpCode = this.Session["CorpCode"].ToString();
		oAOfficeResApplicationCollect.iMonth = ((this.txtMonth.Text.Trim() == "") ? DateTime.Now.Month : int.Parse(this.txtMonth.Text.Trim()));
		oAOfficeResApplicationCollect.IsSubmit = "0";
		oAOfficeResApplicationCollect.iYear = ((this.txtYear.Text.Trim() == "") ? DateTime.Now.Year : int.Parse(this.txtYear.Text.Trim()));
		oAOfficeResApplicationCollect.RecordDate = DateTime.Now;
		oAOfficeResApplicationCollect.SumMoney = 0m;
		oAOfficeResApplicationCollect.UserCode = base.UserCode;
		oAOfficeResApplicationCollect.Remark = this.txtRemark.Text.Trim();
		return oAOfficeResApplicationCollect;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAOfficeResApplicationCollect data = this.GetData();
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


