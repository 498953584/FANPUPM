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
public partial class oa_WorkManage_StorageManage_InStoreroomEdit : BasePage, IRequiresSessionState
{
	public ptOfficeResInStorageAction hrAction
	{
		get
		{
			return new ptOfficeResInStorageAction();
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["InStorageID"];
			if (obj != null)
			{
				return (int)this.ViewState["InStorageID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["InStorageID"] = value;
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
			base.Response.Cache.SetNoStore();
			this.OperateType = base.Request["op"].ToString().Trim();
			if (base.Request["id"].ToString() != "")
			{
				this.InStorageID = int.Parse(base.Request["id"].ToString());
			}
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
		}
	}
	private void EditDisplay()
	{
		ptOfficeResInStorage model = this.hrAction.GetModel(this.InStorageID);
		if (model != null)
		{
			this.txtBillCode.Text = model.BillCode;
			this.txtInDate.Text = model.InDate.ToShortDateString();
			this.txtRemark.Text = model.Remark;
			this.txtTransactor.Text = model.Transactor;
			this.DDLStorage.SelectedValue = model.DepotID.ToString();
		}
	}
	private ptOfficeResInStorage GetData()
	{
		return new ptOfficeResInStorage
		{
			BillCode = this.txtBillCode.Text,
			DepotID = int.Parse(this.DDLStorage.SelectedValue),
			InDate = (this.txtInDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtInDate.Text.Trim()),
			InStorageID = this.InStorageID,
			RecordDate = DateTime.Now,
			Remark = this.txtRemark.Text,
			Transactor = this.txtTransactor.Text,
			UserCode = base.UserCode
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResInStorage data = this.GetData();
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


