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
public partial class oa_WorkManage_StorageManage_MatterClassManageEdit : BasePage, IRequiresSessionState
{
	public ptOfficeResClassAction mcAction
	{
		get
		{
			return new ptOfficeResClassAction();
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LibraryCode"];
			if (obj != null)
			{
				return (string)this.ViewState["LibraryCode"];
			}
			return "";
		}
		set
		{
			this.ViewState["LibraryCode"] = value;
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
		this.LibraryCode = base.Request["lc"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack && this.OperateType == "upd")
		{
			this.ClassDisplay();
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " TypeCode='" + this.LibraryCode + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtBookLibraryName.Text = list.Rows[0]["TypeName"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
	}
	private ptOfficeResClass GetData()
	{
		ptOfficeResClass ptOfficeResClass = new ptOfficeResClass();
		ptOfficeResClass.i_ChildNum = 0;
		if (this.OperateType == "upd")
		{
			ptOfficeResClass.TypeCode = this.LibraryCode;
		}
		else
		{
			if (this.LibraryCode == "")
			{
				ptOfficeResClass.TypeCode = OfficeCommonClass.GetNewSameLevelCode("OA_OfficeRes_Class", "TypeCode", "", 4);
			}
			else
			{
				string strWhere = string.Concat(new object[]
				{
					" TypeCode like ('",
					this.LibraryCode,
					"%') and len(TypeCode)>len('",
					this.LibraryCode,
					"') and left(TypeCode,",
					this.LibraryCode.Length,
					")='",
					this.LibraryCode,
					"'"
				});
				ptOfficeResClass.TypeCode = OfficeCommonClass.GetNewLowerLevelCode("OA_OfficeRes_Class", "TypeCode", this.LibraryCode, strWhere, this.LibraryCode.Length + 4);
			}
		}
		ptOfficeResClass.IsValid = "1";
		ptOfficeResClass.RecordDate = DateTime.Now;
		ptOfficeResClass.Remark = this.txtRemark.Text.Trim();
		ptOfficeResClass.TypeName = this.txtBookLibraryName.Text.Trim();
		ptOfficeResClass.UserCode = base.UserCode;
		return ptOfficeResClass;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ptOfficeResClass data = this.GetData();
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


