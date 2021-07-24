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
public partial class oa_WorkManage_SubCompanyManage_CompanyDrawStatSubTabEdit : BasePage, IRequiresSessionState
{

	public OAOfficeResApplicationCollectDetailAction amAction
	{
		get
		{
			return new OAOfficeResApplicationCollectDetailAction();
		}
	}
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DEPOTID"];
			if (obj != null)
			{
				return (int)this.ViewState["DEPOTID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DEPOTID"] = value;
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
	public Guid ICRecordID
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
		if (base.Request["id"] == null || base.Request["ic"] == null || base.Request["op"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (!this.Page.IsPostBack)
		{
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["id"].ToString() != "")
			{
				this.RecordID = int.Parse(base.Request["id"].ToString());
			}
			if (base.Request["ic"].ToString() != "")
			{
				this.ICRecordID = new Guid(base.Request["ic"].ToString());
			}
			if (this.OperateType == "upd")
			{
				this.imgsel.Disabled = true;
				this.EditDisplay();
			}
		}
		this.txtNumber.Attributes["onblur"] = "javascript:checkDecimal(this);";
	}
	private void EditDisplay()
	{
		DataTable list = this.amAction.GetList("b.ACDetailID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.DDLGetType.SelectedValue = dataRow["GetType"].ToString();
			this.DDLUseType.SelectedValue = dataRow["UseType"].ToString();
			this.HdnMatterialID.Value = dataRow["MaterialID"].ToString();
			this.txtNumber.Text = dataRow["ApplyNumber"].ToString();
			this.txtInStoragePrice.Text = dataRow["PlanFee"].ToString();
		}
	}
	private OAOfficeResApplicationCollectDetail GetData()
	{
		return new OAOfficeResApplicationCollectDetail
		{
			ACDetailID = this.RecordID,
			ACRecordID = this.ICRecordID,
			ApplyNumber = (this.txtNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtNumber.Text.Trim()),
			MaterialID = int.Parse(this.HdnMatterialID.Value)
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAOfficeResApplicationCollectDetail data = this.GetData();
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


