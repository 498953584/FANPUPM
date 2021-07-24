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
public partial class HR_Organization_JobFamilyThirdEdit : BasePage, IRequiresSessionState
{

	public PTDRoleAction mcAction
	{
		get
		{
			return new PTDRoleAction();
		}
	}
	public string RoleTypeCode
	{
		get
		{
			object obj = this.ViewState["RoleTypeCode"];
			if (obj != null)
			{
				return (string)this.ViewState["RoleTypeCode"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["RoleTypeCode"] = value;
		}
	}
	public string ParentCode
	{
		get
		{
			object obj = this.ViewState["ParentCode"];
			if (obj != null)
			{
				return (string)this.ViewState["ParentCode"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["ParentCode"] = value;
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
		if (base.Request["cc"] == null || base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.RoleTypeCode = base.Request["cc"].ToString().Trim();
		this.OperateType = base.Request["t"].ToString().Trim();
		if (!this.Page.IsPostBack)
		{
			this.txtRoleTypeCode.Enabled = false;
			if (this.OperateType == "upd")
			{
				this.ClassDisplay();
				return;
			}
			this.txtRoleTypeCode.Text = this.mcAction.GetNewRoleTypeCode(this.RoleTypeCode);
		}
	}
	private void ClassDisplay()
	{
		string strWhere = " RoleTypeCode='" + this.RoleTypeCode + "' ";
		DataTable list = this.mcAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			this.txtAchievement.Text = list.Rows[0]["Achievement"].ToString();
			this.txtAgeRequet.Text = list.Rows[0]["AgeRequet"].ToString();
			this.txtAudition.Text = list.Rows[0]["Audition"].ToString();
			this.txtAvoirdupois.Text = list.Rows[0]["Avoirdupois"].ToString();
			this.txtCommunicate.Text = list.Rows[0]["Communicate"].ToString();
			this.txtCompetency.Text = list.Rows[0]["Competency"].ToString();
			this.txtDucationalBackground.Text = list.Rows[0]["DucationalBackground"].ToString();
			this.txtDutyDescribe.Text = list.Rows[0]["DutyDescribe"].ToString();
			this.txtDutyExplain.Text = list.Rows[0]["DutyExplain"].ToString();
			this.txtDutyTask.Text = list.Rows[0]["DutyTask"].ToString();
			this.txtExpand.Text = list.Rows[0]["Expand"].ToString();
			this.txtExperience.Text = list.Rows[0]["Experience"].ToString();
			this.txtEye.Text = list.Rows[0]["Eye"].ToString();
			this.txtMoralCharacter.Text = list.Rows[0]["MoralCharacter"].ToString();
			this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
			this.txtRoleTypeCode.Text = list.Rows[0]["RoleTypeCode"].ToString();
			this.txtRoleTypeName.Text = list.Rows[0]["RoleTypeName"].ToString();
			this.txtSexRequest.Text = list.Rows[0]["SexRequest"].ToString();
			this.txtStatureRequest.Text = list.Rows[0]["StatureRequest"].ToString();
			this.txtTalk.Text = list.Rows[0]["Talk"].ToString();
		}
	}
	private PTDRole GetData()
	{
		PTDRole pTDRole = new PTDRole();
		pTDRole.RoleTypeCode = this.txtRoleTypeCode.Text;
		if (this.OperateType == "add")
		{
			pTDRole.ParentCode = this.RoleTypeCode;
		}
		else
		{
			pTDRole.ParentCode = this.txtRoleTypeCode.Text.Substring(0, 6);
		}
		pTDRole.Achievement = this.txtAchievement.Text;
		pTDRole.AgeRequet = this.txtAgeRequet.Text;
		pTDRole.Audition = this.txtAudition.Text;
		pTDRole.Avoirdupois = this.txtAvoirdupois.Text;
		pTDRole.Communicate = this.txtCommunicate.Text;
		pTDRole.Competency = this.txtCompetency.Text;
		pTDRole.DucationalBackground = this.txtDucationalBackground.Text;
		pTDRole.DutyDescribe = this.txtDutyDescribe.Text;
		pTDRole.DutyExplain = this.txtDutyExplain.Text;
		pTDRole.DutyTask = this.txtDutyTask.Text;
		pTDRole.Expand = this.txtExpand.Text;
		pTDRole.Experience = this.txtExperience.Text;
		pTDRole.Eye = this.txtEye.Text;
		pTDRole.MoralCharacter = this.txtMoralCharacter.Text;
		pTDRole.Remark = this.txtRemark.Text;
		pTDRole.RoleTypeName = this.txtRoleTypeName.Text;
		pTDRole.SexRequest = this.txtSexRequest.Text;
		pTDRole.StatureRequest = this.txtStatureRequest.Text;
		pTDRole.Talk = this.txtTalk.Text;
		return pTDRole;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		PTDRole data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.mcAction.AddThird(data);
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
			int num = this.mcAction.UpdateThird(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "没有相关数据可更新!";
		}
	}
}


