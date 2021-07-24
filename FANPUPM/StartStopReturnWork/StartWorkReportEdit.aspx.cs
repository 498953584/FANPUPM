using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_StartWorkReportEdit : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string prjState = string.Empty;
	private string reportId = string.Empty;
	private PTStartReportService startReportServer = new PTStartReportService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["prjState"]))
		{
			this.prjState = base.Request["prjState"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["reportId"]))
		{
			this.reportId = base.Request["reportId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			TypeList.BindXmgroupDrop(this.dropImplDep);
			this.BindProject();
			if (!string.IsNullOrEmpty(this.reportId))
			{
				this.BindInfos();
			}
			else
			{
				this.hfldStartReportId.Value = System.Guid.NewGuid().ToString();
				this.hfldInputDate.Value = System.DateTime.Now.ToString();
				this.hfldInputUser.Value = base.UserCode;
			}
			this.FileUpload1.RecordCode = this.hfldStartReportId.Value;
		}
	}
	protected void BindInfos()
	{
		this.hfldStartReportId.Value = this.reportId;
		PTStartReport byId = this.startReportServer.GetById(this.reportId);
		if (byId != null)
		{
			this.txtSingleProjectName.Text = byId.SingleProjectName;
			this.txtProjectPlace.Text = byId.ProjectPlace;
			this.txtConstructionUnit.Text = byId.ConstructionUnit;
			if (byId.ApplyStartDate.HasValue)
			{
				this.txtApplyStartDate.Text = byId.ApplyStartDate.Value.ToString("yyyy-MM-dd");
			}
			if (byId.RealityStartDate.HasValue)
			{
				this.txtRealityStartDate.Text = byId.RealityStartDate.Value.ToString("yyyy-MM-dd");
			}
			this.txtMainContent.Text = byId.MainContent;
			this.txtPrepareCondition.Text = byId.PrepareCondition;
			this.txtExistenceProblem.Text = byId.ExistenceProblem;
			this.txtSupervisorUnitOpinion.Text = byId.SupervisorUnitOpinion;
			this.txtBuildUnitOpinion.Text = byId.BuildUnitOpinion;
			this.txtApplyUnit.Text = byId.ApplyUnit;
			this.txtAuditUnit.Text = byId.AuditUnit;
			this.txtActualPrincipal.Text = byId.ActualPrincipal;
			this.hfldInputUser.Value = byId.InputUser;
			this.hfldInputDate.Value = byId.InputDate.ToString();
			if (!string.IsNullOrEmpty(byId.ImplDep))
			{
				this.dropImplDep.SelectedValue = byId.ImplDep;
			}
		}
	}
	protected void BindProject()
	{
		ProjectInfo byId = ProjectInfo.GetById(this.prjId);
		this.txtPrjName.Text = byId.PrjName;
		this.hfldPrjGuid.Value = this.prjId;
		this.hfldPrjState.Value = this.prjState;
		System.Guid id = new System.Guid(this.prjId);
		PTPrjInfoZTB byId2 = new PTPrjInfoZTBService().GetById(id);
		if (byId2 != null)
		{
			this.txtMainPrincipal.Text = byId2.WorkUnit;
		}
		PTPrjInfo byId3 = new PTPrjInfoService().GetById(this.prjId);
		if (byId3 != null)
		{
			this.txtMainPrincipal.Text = byId3.WorkUnit;
		}
	}
	protected PTStartReport GetModel(PTStartReport startReport)
	{
		startReport.ReportId = this.hfldStartReportId.Value;
		startReport.PrjGuid = this.hfldPrjGuid.Value;
		startReport.SingleProjectName = this.txtSingleProjectName.Text.Trim();
		startReport.ProjectPlace = this.txtProjectPlace.Text.Trim();
		startReport.ConstructionUnit = this.txtConstructionUnit.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtApplyStartDate.Text.Trim()))
		{
			startReport.ApplyStartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyStartDate.Text.Trim()));
		}
		else
		{
			startReport.ApplyStartDate = null;
		}
		if (!string.IsNullOrEmpty(this.txtRealityStartDate.Text.Trim()))
		{
			startReport.RealityStartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtRealityStartDate.Text.Trim()));
		}
		else
		{
			startReport.RealityStartDate = null;
		}
		if (!string.IsNullOrEmpty(this.dropImplDep.SelectedValue))
		{
			startReport.ImplDep = this.dropImplDep.SelectedValue;
		}
		startReport.MainContent = this.txtMainContent.Text.Trim();
		startReport.PrepareCondition = this.txtPrepareCondition.Text.Trim();
		startReport.ExistenceProblem = this.txtExistenceProblem.Text.Trim();
		startReport.SupervisorUnitOpinion = this.txtSupervisorUnitOpinion.Text.Trim();
		startReport.BuildUnitOpinion = this.txtBuildUnitOpinion.Text.Trim();
		startReport.ApplyUnit = this.txtApplyUnit.Text.Trim();
		startReport.AuditUnit = this.txtAuditUnit.Text.Trim();
		startReport.InputUser = this.hfldInputUser.Value;
		startReport.InputDate = System.Convert.ToDateTime(this.hfldInputDate.Value);
		startReport.ActualPrincipal = this.txtActualPrincipal.Text.Trim();
		return startReport;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			if (!string.IsNullOrEmpty(this.reportId))
			{
				PTStartReport byId = this.startReportServer.GetById(this.reportId);
				if (!(byId.FlowState == -1) && !(byId.FlowState == -3) && !(base.UserCode == "00000000"))
				{
					base.RegisterShow("系统提示", "此项目的开工报告已经提交审核，不能再做修改!");
					return;
				}
				PTStartReport model = this.GetModel(byId);
				this.startReportServer.Update(model);
			}
			else
			{
				PTStartReport pTStartReport = new PTStartReport();
				pTStartReport = this.GetModel(pTStartReport);
				pTStartReport.FlowState = new int?(-1);
				this.startReportServer.Add(pTStartReport);
			}
		}
		catch
		{
			base.RegisterShow("系统提示", "保存失败!");
			return;
		}
		string message = "winclose('StartWorkReportEdit','StartWorkReportList.aspx', true)";
		base.RegisterScript(message);
	}
}


