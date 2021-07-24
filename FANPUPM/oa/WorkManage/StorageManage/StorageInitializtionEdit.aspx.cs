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
public partial class oa_WorkManage_StorageManage_StorageInitializtionEdit : BasePage, IRequiresSessionState
{
	public ptOfficeResStockAction hrAction
	{
		get
		{
			return new ptOfficeResStockAction();
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
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DepotID"];
			if (obj != null)
			{
				return (int)this.ViewState["DepotID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DepotID"] = value;
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
		if (base.Request["rid"] == null || base.Request["dd"] == null || base.Request["op"] == null)
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
			if (base.Request["dd"].ToString() != "")
			{
				this.DepotID = int.Parse(base.Request["dd"].ToString());
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
		DataTable list = this.hrAction.GetList("a.RecordID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.txtNumber.Text = dataRow["Number"].ToString();
			this.txtPlanFee.Text = dataRow["PlanFee"].ToString();
			this.txtResName.Text = dataRow["ResName"].ToString();
			this.txtUnit.Text = dataRow["Unit"].ToString();
			this.DDLGetType.SelectedValue = dataRow["GetType"].ToString();
			this.DDLUseType.SelectedValue = dataRow["UseType"].ToString();
			this.HdnMatterialID.Value = dataRow["MaterialID"].ToString();
		}
	}
	private ptOfficeResStock GetData()
	{
		return new ptOfficeResStock
		{
			AvgPrice = 0m,
			DepotID = this.DepotID,
			MaterialID = int.Parse(this.HdnMatterialID.Value),
			Number = (this.txtNumber.Text.Trim() == "") ? 0m : Convert.ToDecimal(this.txtNumber.Text.Trim()),
			RecordID = this.RecordID
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResStock data = this.GetData();
		if (this.OperateType == "add")
		{
			if (this.hrAction.Exists(this.DepotID, int.Parse(this.HdnMatterialID.Value)))
			{
				this.Page.RegisterStartupScript("", "<script>alert('相关数据已存在!');</script>");
			}
			else
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


