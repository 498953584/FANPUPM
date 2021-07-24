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
public partial class oa_WorkManage_StorageManage_DepartmentDrawApplyAssTabEdit : BasePage, IRequiresSessionState
{
	public OAOfficeResDepartmentApplicationDetailAction amAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationDetailAction();
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
	public int DepartmentID
	{
		get
		{
			object obj = this.ViewState["DEPARTMENTID"];
			if (obj != null)
			{
				return (int)this.ViewState["DEPARTMENTID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DEPARTMENTID"] = value;
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
		if (base.Request["rid"] == null || base.Request["id"] == null || base.Request["dm"] == null || base.Request["op"] == null)
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
			if (base.Request["id"].ToString() != "")
			{
				this.InStorageID = new Guid(base.Request["id"].ToString());
			}
			if (base.Request["dm"].ToString() != "")
			{
				this.DepartmentID = int.Parse(base.Request["dm"].ToString());
			}
			this.DisplayDepartmentRation();
			if (this.OperateType == "upd")
			{
				this.imgsel.Disabled = true;
				this.EditDisplay();
			}
		}
		this.txtNumber.Attributes["onblur"] = "javascript:IsInteger(this);";
	}
	private void DisplayDepartmentRation()
	{
		decimal d = OAOfficeCommonClas.GetDepartmentRation(this.DepartmentID.ToString());
		d *= 12m;
		this.txtPersonRation.Text = d.ToString("f2");
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("a.DADRecordID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.DDLUseType.SelectedValue = dataRow["UseType"].ToString();
			this.HdnMatterialID.Value = dataRow["MaterialID"].ToString();
			this.txtNumber.Text = dataRow["ApplyNumber"].ToString();
			this.txtInStoragePrice.Text = dataRow["PlanFee"].ToString();
			this.HdnPrice.Value = dataRow["PlanFee"].ToString();
		}
	}
	private OAOfficeResDepartmentApplicationDetail GetData()
	{
		return new OAOfficeResDepartmentApplicationDetail
		{
			IsPass = "1",
			MaterialID = int.Parse(this.HdnMatterialID.Value),
			DADRecordID = this.RecordID,
			DARecordID = this.InStorageID,
			ApplyNumber = (this.txtNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtNumber.Text.Trim())
		};
	}
	private bool IsPass()
	{
		decimal d = (this.txtPersonRation.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtPersonRation.Text.Trim());
		string text = string.Concat(new object[]
		{
			"datediff(yyyy,'",
			DateTime.Now,
			"',a.ApplyDate)=0 and ApplyDepartment='",
			this.DepartmentID.ToString(),
			"'"
		});
		if (this.OperateType == "upd")
		{
			text = text + " and b.DADRecordID<>" + this.RecordID;
		}
		decimal d2 = this.amAction.GetApplyStat(text) + ((this.HdnPrice.Value.Trim() == "") ? 0m : Convert.ToDecimal(this.HdnPrice.Value.Trim())) * ((this.txtNumber.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtNumber.Text.Trim()));
		return d >= d2;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (this.IsPass())
		{
			this.amAction.UpdateAudit(this.InStorageID, "0");
		}
		else
		{
			this.amAction.UpdateAudit(this.InStorageID, "1");
		}
		OAOfficeResDepartmentApplicationDetail data = this.GetData();
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


