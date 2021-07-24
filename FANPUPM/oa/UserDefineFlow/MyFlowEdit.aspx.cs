using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_UserDefineFlow_MyFlowEdit : NBasePage, IRequiresSessionState
{

	public OAWFApplyItemAction hrAction
	{
		get
		{
			return new OAWFApplyItemAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
		}
	}
	public System.Guid RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (System.Guid)this.ViewState["RECORDID"];
			}
			return System.Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDID"] = value;
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
	public int TemplateID
	{
		get
		{
			object obj = this.ViewState["TEMPLATEID"];
			if (obj != null)
			{
				return (int)this.ViewState["TEMPLATEID"];
			}
			return 0;
		}
		set
		{
			this.ViewState["TEMPLATEID"] = value;
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (base.Request["rid"] == null || base.Request["tid"] == null || base.Request["t"] == null || base.Request["value"] == null)
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
				this.RecordID = new System.Guid(base.Request["rid"].ToString());
			}
			else
			{
				this.RecordID = System.Guid.NewGuid();
			}
			this.HdnRecordCode.Value = this.RecordID.ToString();
			this.TemplateID = System.Convert.ToInt32(base.Request["tid"].ToString());
			this.GetBusinessClass();
			if (this.OperateType == "upd")
			{
				this.EditDisplay();
			}
			this.hfdNodeValue.Value = base.Request["value"];
			this.FileUpload1.Folder = "/UploadFiles/Audit/" + this.RecordID.ToString();
			this.FileUpload1.RecordCode = "fujian";
		}
	}
	private void GetBusinessClass()
	{
		DataTable businessClassList = this.hrAction.GetBusinessClassList(this.TemplateID);
		if (businessClassList.Rows.Count > 0)
		{
			this.BusinessClass = businessClassList.Rows[0]["BusinessClass"].ToString();
		}
	}
	private void EditDisplay()
	{
		OAWFApplyItem model = this.hrAction.GetModel(this.RecordID);
		if (model != null)
		{
			this.txtLayoutName.Text = model.Title;
			this.txtRemark.Text = model.Remark;
		}
	}
	private string FileNameOrPath(int intFlag)
	{
		string text = "";
		System.Collections.ArrayList annexList = this._AnnexAction.GetAnnexList(this.RecordID.ToString(), 0, 88);
		if (annexList.Count > 0)
		{
			if (intFlag == 0)
			{
				for (int i = 0; i < annexList.Count; i++)
				{
					text = text + ((AnnexInfo)annexList[i]).OriginalName + ",";
				}
			}
			else
			{
				for (int j = 0; j < annexList.Count; j++)
				{
					text = text + ((AnnexInfo)annexList[j]).FilePath + ",";
				}
			}
		}
		return text;
	}
	private OAWFApplyItem GetData()
	{
		return new OAWFApplyItem
		{
			AuditState = -1,
			BusinessClass = this.BusinessClass,
			RecordID = this.RecordID,
			Remark = this.txtRemark.Text,
			UserCode = this.Session["yhdm"].ToString(),
			Title = this.txtLayoutName.Text,
			RecordDate = System.DateTime.Now,
			OriginalName = this.FileNameOrPath(0).Trim(new char[]
			{
				','
			}),
			FilePath = "",
			TemplateID = this.TemplateID
		};
	}
	protected void btnAdd_Click(object sender, System.EventArgs e)
	{
		OAWFApplyItem data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.hrAction.Add(data);
			if (num > 0)
			{
				base.RegisterScript("successed();");
			}
			else
			{
				base.RegisterScript("top.ui.alert('没有相关数据可添加');");
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.hrAction.Update(data);
			if (num > 0)
			{
				base.RegisterScript("successed();");
				return;
			}
			base.RegisterScript("top.ui.alert('没有相关数据可更新');");
		}
	}
}


