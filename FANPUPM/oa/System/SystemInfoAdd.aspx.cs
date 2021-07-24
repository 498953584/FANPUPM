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
public partial class oa_System_SystemInfoAdd : BasePage, IRequiresSessionState
{

	protected int ClassID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["CLASSID"]);
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}
	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
	private Guid RecordID
	{
		get
		{
			return (Guid)this.ViewState["RECORDID"];
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	protected string SystemType
	{
		get
		{
			return this.ViewState["SYSTEMTYPE"].ToString();
		}
		set
		{
			this.ViewState["SYSTEMTYPE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request["cd"] != null || base.Request["ctc"] != null || base.Request["t"] != null || base.Request["rid"] != null)
			{
				if (base.Request["ctc"].ToString() == "002")
				{
					this.SystemType = "1";
					this.LbTitle.Text = "集团制度管理";
				}
				else
				{
					if (base.Request["ctc"].ToString() == "003")
					{
						this.SystemType = "2";
						this.LbTitle.Text = "子公司制度分类";
					}
				}
				this.RecordID = new Guid(base.Request["rid"]);
				this.Type = base.Request["t"].ToString();
				this.ClassID = Convert.ToInt32(base.Request["cd"]);
				if (this.Type != "Add")
				{
					this.GetPageData();
				}
				else
				{
					this.RecordID = Guid.NewGuid();
				}
				this.hdnRecordId.Value = this.RecordID.ToString();
				this.FileLink1.MID = 3;
				this.FileLink1.FID = this.hdnRecordId.Value;
				this.FileLink1.Type = 1;
				if (this.Type == "View")
				{
					this.FileLink1.Type = 0;
					this.BtnSave.Width = 0;
					this.txtRemark.Enabled = (this.txtSignMan.Enabled = (this.txtSystemName.Enabled = (this.DBSignDate.Enabled = false)));
				}
			}
			if (base.Request["ic"] != null && base.Request["ic"].ToString() != "")
			{
				this.FileLink1.MID = 3;
				this.FileLink1.FID = base.Request["ic"].ToString();
				this.FileLink1.Type = 0;
				this.LbTitle.Visible = false;
				this.BtnSave.Width = 0;
				this.BtnClose.Style.Add("width", "0");
				this.RecordID = new Guid(base.Request["ic"]);
				this.GetPageData();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.sia.Add(this.getSystemInfoModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sia.Update(this.getSystemInfoModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SystemInfoModel getSystemInfoModel()
	{
		return new SystemInfoModel
		{
			RecordID = this.RecordID,
			AuditState = -1,
			ClassID = this.ClassID,
			SystemType = this.SystemType,
			SystemName = this.txtSystemName.Text,
			RecordDate = DateTime.Now,
			UserCode = this.Session["yhdm"].ToString(),
			SignDate = Convert.ToDateTime(this.DBSignDate.Text),
			Remark = this.txtRemark.Text.ToString(),
			CorpCode = this.Session["CorpCode"].ToString(),
			SignMan = this.txtSignMan.Text,
			SystemCode = "",
			IsCurrent = this.chkNowSystem.Checked ? "1" : "0"
		};
	}
	protected void GetPageData()
	{
		SystemInfoModel model = this.sia.GetModel(this.RecordID);
		this.txtSystemName.Text = model.SystemName;
		this.txtSignMan.Text = model.SignMan;
		this.txtRemark.Text = model.Remark;
		this.DBSignDate.Text = model.SignDate.ToString("yyyy-MM-dd");
		this.chkNowSystem.Checked = (model.IsCurrent == "1");
	}
}


