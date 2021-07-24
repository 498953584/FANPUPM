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
public partial class oa_System_zgsSystemInfoEdit : BasePage, IRequiresSessionState
{
	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}
	public Guid RecordCode
	{
		get
		{
			object obj = this.ViewState["RECORDCODE"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDCODE"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDCODE"] = value;
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
		this.OperateType = base.Request["Op"].ToString();
		if (!base.IsPostBack)
		{
			if (base.Request["RecordID"] != "")
			{
				this.RecordCode = new Guid(base.Request["RecordID"]);
				this.hdnRecordId.Value = Convert.ToString(this.RecordCode);
			}
			else
			{
				this.RecordCode = Guid.Empty;
				this.hdnRecordId.Value = Convert.ToString(Guid.NewGuid());
			}
			if (this.OperateType == "edit")
			{
				this.SystemInfoEditDisplay();
			}
			if (this.OperateType == "see")
			{
				this.SystemInfoEditDisplay();
				this.btnAdd.Visible = false;
			}
		}
		this.FilesBind(3);
	}
	protected void FilesBind(int moduleID)
	{
		this.Literal1.Text = "";
		string value = this.hdnRecordId.Value;
		int annexType = 0;
		AnnexAction annexAction = new AnnexAction();
		DataTable fileList = annexAction.GetFileList(value, annexType, moduleID);
		foreach (DataRow dataRow in fileList.Rows)
		{
			Literal expr_54 = this.Literal1;
			string text = expr_54.Text;
			expr_54.Text = string.Concat(new string[]
			{
				text,
				"<a href='",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"' target=_blank>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
		}
	}
	private SystemInfoModel GetSIM()
	{
		string corpCode = "";
		DataTable corpCode2 = this.sia.GetCorpCode("v_yhdm='" + this.Session["yhdm"].ToString() + "')");
		if (corpCode2.Rows.Count > 0)
		{
			corpCode = corpCode2.Rows[0]["CorpCode"].ToString();
		}
		return new SystemInfoModel
		{
			RecordID = Guid.NewGuid(),
			AuditState = -1,
			ClassID = Convert.ToInt32(base.Request["cid"]),
			SystemType = "2",
			SystemName = this.txtzdname.Text.Trim(),
			RecordDate = (this.txtDate.Text == "") ? DateTime.Now : Convert.ToDateTime(this.txtDate.Text),
			UserCode = this.Session["yhdm"].ToString(),
			SignDate = DateTime.Now,
			Remark = this.txtRemark.Text.ToString(),
			CorpCode = corpCode,
			SystemCode = ""
		};
	}
	private SystemInfoModel GetSystemInfoModel()
	{
		string corpCode = "";
		DataTable corpCode2 = this.sia.GetCorpCode("v_yhdm='" + this.Session["yhdm"].ToString() + "')");
		if (corpCode2.Rows.Count > 0)
		{
			corpCode = corpCode2.Rows[0]["CorpCode"].ToString();
		}
		return new SystemInfoModel
		{
			RecordID = new Guid(base.Request["RecordID"]),
			AuditState = -1,
			ClassID = Convert.ToInt32(base.Request["cid"]),
			SystemType = "2",
			SystemName = this.txtzdname.Text.Trim(),
			RecordDate = (this.txtDate.Text == "") ? DateTime.Now : Convert.ToDateTime(this.txtDate.Text),
			UserCode = this.Session["yhdm"].ToString(),
			SignDate = DateTime.Now,
			Remark = this.txtRemark.Text.ToString(),
			CorpCode = corpCode,
			SystemCode = ""
		};
	}
	private void SystemInfoEditDisplay()
	{
		string strWhere = " RecordID='" + this.RecordCode + "' ";
		DataTable list = this.sia.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtzdname.Text = list.Rows[0]["SystemName"].ToString();
			this.txtDate.Text = Convert.ToDateTime(list.Rows[0]["RecordDate"]).ToString("yyyy-MM-dd");
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		if (this.OperateType == "add")
		{
			SystemInfoModel sIM = this.GetSIM();
			int num = this.sia.Add(sIM);
			if (num > 0)
			{
				this.JS.Text = "alert('添加成功!');";
				this.JS.Text = "returnValue=true;window.close();";
			}
		}
		if (this.OperateType == "edit")
		{
			SystemInfoModel systemInfoModel = this.GetSystemInfoModel();
			int num = this.sia.Update(systemInfoModel);
			if (num > 0)
			{
				this.JS.Text = "alert('修改成功!');";
				this.JS.Text = "returnValue=true;window.close();";
			}
		}
	}
}


