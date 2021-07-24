using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_RetMsgEdit : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string retMsgId = string.Empty;
	private string year = string.Empty;
	private string type = string.Empty;
	private PTRetMsgService retMsgServer = new PTRetMsgService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["retMsgId"]))
		{
			this.retMsgId = base.Request["retMsgId"].ToString();
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
				this.hfldRetMsgId.Value = System.Guid.NewGuid().ToString();
				this.hfldInputDate.Value = System.DateTime.Now.ToString();
				this.hfldInputUser.Value = base.UserCode;
			}
			this.FileUpload1.RecordCode = this.hfldRetMsgId.Value;
		}
	}
	protected void BindInfos()
	{
		this.hfldRetMsgId.Value = this.retMsgId;
		PTRetMsg byId = this.retMsgServer.GetById(this.retMsgId);
		if (byId != null)
		{
			this.txtConstArea.Text = byId.ConstArea;
			this.txtConstructionUnit.Text = byId.ConstUnit;
			this.txtProjectMileage.Text = byId.ProjectMileage;
			this.txtRetDate.Text = byId.RetDate.ToString("yyyy-MM-dd");
			this.txtMainContent.Text = byId.MainContent;
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
			this.txtBuildUnitOpinion.Text = byId.BuildUnitOpinion;
			this.txtBuildUnitPerson.Text = byId.BuildUnitPerson;
			if (byId.BuildUnitSignDate.HasValue)
			{
				this.txtBuildUnitSignDate.Text = byId.BuildUnitSignDate.Value.ToString("yyyy-MM-dd");
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
		PTStopMsgService source = new PTStopMsgService();
		PTStopMsg pTStopMsg = (
			from stop in source
			where stop.PrjGuid == this.prjId
			select stop into list
			orderby list.InputDate descending
			select list).FirstOrDefault<PTStopMsg>();
		if (pTStopMsg != null)
		{
			this.hfldStopMsgId.Value = pTStopMsg.StopMsgId;
		}
	}
	protected PTRetMsg GetModel()
	{
		PTRetMsg pTRetMsg = new PTRetMsg();
		pTRetMsg.RetMsgId = this.hfldRetMsgId.Value;
		pTRetMsg.PrjGuid = this.hfldPrjGuid.Value.Trim();
		pTRetMsg.ConstArea = this.txtConstArea.Text.Trim();
		pTRetMsg.ConstUnit = this.txtConstructionUnit.Text.Trim();
		pTRetMsg.ProjectMileage = this.txtProjectMileage.Text.Trim();
		pTRetMsg.RetDate = System.Convert.ToDateTime(this.txtRetDate.Text.Trim());
		pTRetMsg.MainContent = this.txtMainContent.Text.Trim();
		pTRetMsg.SupervisorSign = this.txtSupervisorSign.Text.Trim();
		pTRetMsg.StopMsgId = this.hfldStopMsgId.Value.Trim();
		if (!string.IsNullOrEmpty(this.txtSupervisorSignDate.Text.Trim()))
		{
			pTRetMsg.SupervisorSignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSupervisorSignDate.Text.Trim()));
		}
		else
		{
			pTRetMsg.SupervisorSignDate = null;
		}
		pTRetMsg.GeneralSign = this.txtGeneralSign.Text.Trim();
		if (!string.IsNullOrEmpty(this.txtGeneralSignDate.Text.Trim()))
		{
			pTRetMsg.GeneralSignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtGeneralSignDate.Text.Trim()));
		}
		else
		{
			pTRetMsg.GeneralSignDate = null;
		}
		pTRetMsg.BuildUnitOpinion = this.txtBuildUnitOpinion.Text;
		pTRetMsg.BuildUnitPerson = this.txtBuildUnitPerson.Text;
		if (!string.IsNullOrEmpty(this.txtBuildUnitSignDate.Text.Trim()))
		{
			pTRetMsg.BuildUnitSignDate = new System.DateTime?(System.Convert.ToDateTime(this.txtBuildUnitSignDate.Text.Trim()));
		}
		else
		{
			pTRetMsg.BuildUnitSignDate = null;
		}
		pTRetMsg.FlowState = new int?(-1);
		pTRetMsg.InputUser = this.hfldInputUser.Value;
		pTRetMsg.InputDate = System.Convert.ToDateTime(this.hfldInputDate.Value);
		return pTRetMsg;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.txtRetDate.Text.Trim()))
		{
			base.RegisterShow("系统提示", "复工日期必须输入!");
			return;
		}
		PTRetMsg model = this.GetModel();
		try
		{
			if (this.type == "edit")
			{
				this.retMsgServer.Update(model);
			}
			else
			{
				this.retMsgServer.Add(model);
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_RetMsgEdit' });");
		}
		catch
		{
			base.RegisterScript("top.ui.alert('该项目找不到对应的停工单，添加失败')");
		}
	}
}


