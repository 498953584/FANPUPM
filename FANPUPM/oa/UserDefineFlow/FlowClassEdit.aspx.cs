using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_UserDefineFlow_FlowClassEdit : NBasePage, IRequiresSessionState
{
	public WFBusinessClassAction mcAction
	{
		get
		{
			return new WFBusinessClassAction();
		}
	}
	public string BusinessCode
	{
		get
		{
			object obj = this.ViewState["BusinessCode"];
			if (obj != null)
			{
				return (string)this.ViewState["BusinessCode"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["BusinessCode"] = value;
		}
	}
	public string BusinessClass
	{
		get
		{
			object obj = this.ViewState["BusinessClass"];
			if (obj != null)
			{
				return (string)this.ViewState["BusinessClass"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["BusinessClass"] = value;
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
	public string strID
	{
		get
		{
			object obj = this.ViewState["id"];
			if (obj != null)
			{
				return (string)this.ViewState["id"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["id"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			if (base.Request["cc"] == null || base.Request["cl"] == null || base.Request["t"] == null || base.Request["id"] == null)
			{
				base.RegisterScript("top.ui.alert('系统提示, '参数错误'); ");
				return;
			}
			this.BusinessCode = base.Request["cc"].ToString().Trim();
			this.BusinessClass = base.Request["cl"].ToString().Trim();
			this.OperateType = base.Request["t"].ToString().Trim();
			this.strID = base.Request["id"].ToString().Trim();
			if (this.OperateType == "upd")
			{
				this.ClassDisplay();
			}
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " id='" + this.strID + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtBookLibraryName.Text = list.Rows[0]["BusinessClassName"].ToString();
			this.BusinessClass = list.Rows[0]["BusinessClass"].ToString();
			this.BusinessCode = list.Rows[0]["BusinessCode"].ToString();
		}
	}
	private com.jwsoft.pm.entpm.model.WFBusinessClass GetData()
	{
		com.jwsoft.pm.entpm.model.WFBusinessClass wFBusinessClass = new com.jwsoft.pm.entpm.model.WFBusinessClass();
		wFBusinessClass.BusinessCode = this.BusinessCode;
		if (this.OperateType == "upd")
		{
			wFBusinessClass.BusinessClass = this.BusinessClass;
		}
		else
		{
			wFBusinessClass.BusinessClass = this.mcAction.GetNewBusinessClass(this.BusinessCode);
		}
		wFBusinessClass.BusinessClassName = this.txtBookLibraryName.Text.Trim();
		return wFBusinessClass;
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		if (this.txtBookLibraryName.Text == "")
		{
			base.RegisterScript("top.ui.alert('流程名称不能为空'); ");
			return;
		}
		com.jwsoft.pm.entpm.model.WFBusinessClass data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.mcAction.Add(data);
			WFprivilege wFprivilege = new WFprivilege();
			wFprivilege.businessClass = data.BusinessClass;
			wFprivilege.userlist = base.UserCode;
			WFprivilegeService wFprivilegeService = new WFprivilegeService();
			wFprivilegeService.Add(wFprivilege);
			if (num > 0)
			{
				string message = "top.ui.show( '保存成功'); \ntop.ui.closeWin(); \ntop.ui.reloadTab(); \n";
				base.RegisterScript(message);
			}
			else
			{
				base.RegisterScript("top.ui.alert('没有相关数据可添加'); ");
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.mcAction.Update(data);
			if (num > 0)
			{
				string message2 = "top.ui.show( '保存成功'); \ntop.ui.closeWin(); \ntop.ui.reloadTab(); \n";
				base.RegisterScript(message2);
				return;
			}
			base.RegisterScript("top.ui.alert('没有相关数据可更新'); ");
		}
	}
}


