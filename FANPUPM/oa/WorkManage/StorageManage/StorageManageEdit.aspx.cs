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
public partial class oa_WorkManage_StorageManage_StorageManageEdit : BasePage, IRequiresSessionState
{
	public ptOfficeResDepotAction mcAction
	{
		get
		{
			return new ptOfficeResDepotAction();
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
		if (base.Request["lc"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.DepotID = int.Parse(base.Request["lc"].ToString().Trim());
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack && this.OperateType == "upd")
		{
			this.ClassDisplay();
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " DepotID='" + this.DepotID + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtBookLibraryName.Text = list.Rows[0]["DepotName"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
			this.DDLCorp.SelectedValue = list.Rows[0]["CorpCode"].ToString();
		}
	}
	private ptOfficeResDepot GetData()
	{
		return new ptOfficeResDepot
		{
			CorpCode = this.DDLCorp.SelectedValue,
			DepotName = this.txtBookLibraryName.Text,
			IsValid = "1",
			DepotID = this.DepotID,
			Remark = this.txtRemark.Text.Trim()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResDepot data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.mcAction.Add(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
			}
			else
			{
				this.JS.Text = "没有相关数据可添加!";
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.mcAction.Update(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "没有相关数据可更新!";
		}
	}
}


