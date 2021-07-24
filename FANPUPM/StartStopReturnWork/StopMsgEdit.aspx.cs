using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_StopMsgEdit : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string stopMsgId = string.Empty;
	private string year = string.Empty;
	private string type = string.Empty;
	private PTStopMsgService stopMsgServer = new PTStopMsgService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["stopMsgId"]))
		{
			this.stopMsgId = base.Request["stopMsgId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindProject();
			if (this.type == "edit")
			{
				this.BindInfos();
			}
			else
			{
				this.hfldStopMsgId.Value = System.Guid.NewGuid().ToString();
				this.hfldInputDate.Value = System.DateTime.Now.ToString();
				this.hfldInputUser.Value = base.UserCode;
			}
			this.FileUpload1.RecordCode = this.hfldStopMsgId.Value;
		}
	}
	protected void BindInfos()
	{
		this.hfldStopMsgId.Value = this.stopMsgId;
		PTStopMsg byId = this.stopMsgServer.GetById(this.stopMsgId);
		if (byId != null)
		{
			this.txtConstArea.Text = byId.ConstArea;
			this.txtConstructionUnit.Text = byId.ConstUnit;
			this.txtProjectMileage.Text = byId.ProjectMileage;
			this.txtStopDate.Text = byId.StopDate.ToString("yyyy-MM-dd");
			this.txtStopReason.Text = byId.StopReason;
			this.txtMainContent.Text = byId.MainContent;
			this.txtProjectProblem.Text = byId.ProjectProblem;
			this.txtProblemReason.Text = byId.ProblemReason;
			this.txtImpactLossDegree.Text = byId.ImpactLossDegree;
			this.txtRemedialMeasure.Text = byId.RemedialMeasure;
			this.txtSupervisorSign.Text = byId.SupervisorSign;
			if (byId.SupervisorSignDate.HasValue)
			{
				this.txtSupervisorSignDate.Text = byId.SupervisorSignDate.Value.ToString("yyyy-MM-dd");
			}
			this.txtGeneralSign.Text = byId.GeneralSign;
			if (byId.GeneralSignDate.HasValue)
			{
				this.txtGeneralSignDate.Text = byId.GeneralSignDate.Value.ToString("yyyy-MM-dd");
			}
			this.hfldInputDate.Value = byId.InputDate.ToString();
			this.hfldInputUser.Value = byId.InputUser;
		}
	}
	protected void BindProject()
	{
		ProjectInfo byId = ProjectInfo.GetById(this.prjId);
		this.txtPrjName.Text = byId.PrjName;
		this.hfldPrjGuid.Value = this.prjId;
	}
	protected PTStopMsg GetModel()
	{
		PTStopMsg pTStopMsg = new PTStopMsg();
		pTStopMsg.StopMsgId = this.hfldStopMsgId.Value;
		pTStopMsg.PrjGuid = this.hfldPrjGuid.Value.Trim();
		pTStopMsg.ConstArea = this.txtConstArea.Text.Trim();
		pTStopMsg.ConstUnit = this.txtConstructionUnit.Text.Trim();
		pTStopMsg.ProjectMileage = this.txtProjectMileage.Text.Trim();
		pTStopMsg.StopDate = System.Convert.ToDateTime(this.txtStopDate.Text.Trim());
		pTStopMsg.StopReason = this.txtStopReason.Text.Trim();
		pTStopMsg.MainContent = this.txtMainContent.Text.Trim();
		pTStopMsg.ProjectProblem = this.txtProjectProblem.Text.Trim();
		pTStopMsg.ProblemReason = this.txtProblemReason.Text.Trim();
		pTStopMsg.ImpactLossDegree = this.txtImpactLossDegree.Text.Trim();
		pTStopMsg.RemedialMeasure = this.txtRemedialMeasure.Text.Trim();
		pTStopMsg.SupervisorSign = this.txtSupervisorSign.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtSupervisorSignDate.Text.Trim()))
		{
			pTStopMsg.SupervisorSignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSupervisorSignDate.Text.Trim()));
		}
		else
		{
			pTStopMsg.SupervisorSignDate = null;
		}
		pTStopMsg.GeneralSign = this.txtGeneralSign.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtGeneralSignDate.Text.Trim()))
		{
			pTStopMsg.GeneralSignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtGeneralSignDate.Text.Trim()));
		}
		else
		{
			pTStopMsg.GeneralSignDate = null;
		}
		pTStopMsg.FlowState = new int?(-1);
		pTStopMsg.InputUser = this.hfldInputUser.Value;
		pTStopMsg.InputDate = System.Convert.ToDateTime(this.hfldInputDate.Value);
		return pTStopMsg;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.txtStopDate.Text.Trim()))
		{
			base.RegisterShow("系统提示", "停工日期必须输入!");
			return;
		}
		if (this.type == "edit")
		{
			PTStopMsg model = this.GetModel();
			this.stopMsgServer.Update(model);
		}
		else
		{
			PTStopMsg model2 = this.GetModel();
			this.stopMsgServer.Add(model2);
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_StopMsgEdit' });");
	}
}


