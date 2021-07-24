using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_MatterBillEdit : BasePage, IRequiresSessionState
{
	public ptOfficeResResourcesAction hrAction
	{
		get
		{
			return new ptOfficeResResourcesAction();
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
	public string TypeCode
	{
		get
		{
			object obj = this.ViewState["TypeCode"];
			if (obj != null)
			{
				return (string)this.ViewState["TypeCode"];
			}
			return "";
		}
		set
		{
			this.ViewState["TypeCode"] = value;
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
		if (base.Request["rid"] == null || base.Request["cc"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["rid"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["rid"].ToString());
			}
			this.TypeCode = base.Request["cc"].ToString();
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
		this.txtPlanFee.Attributes["onblur"] = "javascript:checkDecimal(this);";
	}
	private void EditDisplay()
	{
		ptOfficeResResources model = this.hrAction.GetModel(this.RecordID);
		if (model != null)
		{
			this.txtPlanFee.Text = model.PlanFee.ToString("f2");
			this.txtUnit.Text = model.Unit;
			this.txtResName.Text = model.ResName;
			this.DDLUseType.SelectedValue = model.UseType;
			this.DDLGetType.SelectedValue = model.GetType;
		}
	}
	private ptOfficeResResources GetData()
	{
		return new ptOfficeResResources
		{
			GetType = this.DDLGetType.SelectedValue,
			PlanFee = (this.txtPlanFee.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtPlanFee.Text.Trim()),
			RecordID = this.RecordID,
			ResCode = OfficeCommonClass.GetNewSameLevelCode("OA_OfficeRes_Resources", "ResCode", "TypeCode='" + this.TypeCode + "'", 10),
			ResName = this.txtResName.Text,
			TypeCode = this.TypeCode,
			Unit = this.txtUnit.Text,
			UseType = this.DDLUseType.SelectedValue
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResResources data = this.GetData();
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


