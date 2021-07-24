using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_StartWorkReportView : NBasePage, IRequiresSessionState
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
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.reportId = base.Request["ic"].ToString();
			PTStartReport byId = this.startReportServer.GetById(this.reportId);
			if (byId != null)
			{
				this.prjId = byId.PrjGuid;
			}
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(this.reportId))
			{
				this.BindInfos();
			}
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.FileUpload1.InnerHtml = this.FilesBind(this.reportId);
		}
	}
	protected void BindInfos()
	{
		this.hfldStartReportId.Value = this.reportId;
		try
		{
			PTStartReport byId = this.startReportServer.GetById(this.reportId);
			if (byId != null)
			{
				this.BindProject(byId.PrjGuid);
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
				if (!string.IsNullOrEmpty(byId.ImplDep))
				{
					System.Collections.Generic.IList<XpmCode> list = new System.Collections.Generic.List<XpmCode>();
					XpmCodeServices xpmCodeServices = new XpmCodeServices();
					list = xpmCodeServices.GetBySignCode("dd");
					foreach (XpmCode current in list)
					{
						if (current.NoteID == System.Convert.ToInt32(byId.ImplDep))
						{
							this.txtImplDep.Text = current.CodeName;
						}
					}
				}
				this.lblActualPrincipal.Text = byId.ActualPrincipal;
				System.Guid id = new System.Guid(this.prjId);
				PTPrjInfoZTB byId2 = new PTPrjInfoZTBService().GetById(id);
				if (byId2 != null)
				{
					this.lblMainPrincipal.Text = byId2.WorkUnit;
					this.txtPrjName.Text = byId2.PrjName;
					this.hfldPrjState.Value = byId2.PrjState.ToString();
				}
				PTPrjInfo byId3 = new PTPrjInfoService().GetById(this.prjId);
				if (byId3 != null)
				{
					this.lblMainPrincipal.Text = byId3.WorkUnit;
					this.txtPrjName.Text = byId3.PrjName;
					this.hfldPrjState.Value = byId3.PrjState.ToString();
				}
			}
			else
			{
				this.BindProject(this.reportId);
			}
		}
		catch
		{
			this.BindProject(this.reportId);
		}
	}
	protected void BindProject(string prjId)
	{
		PTPrjInfoZTB byId = new PTPrjInfoZTBService().GetById(new System.Guid(prjId));
		if (byId != null)
		{
			this.txtPrjName.Text = byId.PrjName;
			this.hfldPrjState.Value = byId.PrjState.ToString();
		}
		PTPrjInfo byId2 = new PTPrjInfoService().GetById(prjId);
		if (byId2 != null)
		{
			this.txtPrjName.Text = byId2.PrjName;
			this.hfldPrjState.Value = byId2.PrjState.ToString();
		}
	}
	public string FilesBind(string consID)
	{
		string text = ConfigurationManager.AppSettings["StartReport"].ToString();
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + consID);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + consID;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
}


