using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_RetMsgView : NBasePage, IRequiresSessionState
{
	private string retMsgId = string.Empty;
	private PTRetMsgService retMsgService = new PTRetMsgService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["retMsgId"]))
		{
			this.retMsgId = base.Request["retMsgId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.retMsgId = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(this.retMsgId))
			{
				this.BindInfos();
			}
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.FileUpload1.InnerHtml = this.FilesBind(this.retMsgId);
		}
	}
	protected void BindInfos()
	{
		string text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日";
		PTRetMsg byId = this.retMsgService.GetById(this.retMsgId);
		if (byId != null)
		{
			ProjectInfo byId2 = ProjectInfo.GetById(byId.PrjGuid);
			this.txtPrjName.Text = byId2.PrjName;
			this.txtConstArea.Text = byId.ConstArea;
			this.txtConstructionUnit.Text = byId.ConstUnit;
			this.txtProjectMileage.Text = byId.ProjectMileage;
			this.txtRetDate.Text = byId.RetDate.ToString("yyyy年MM月dd日");
			this.txtMainContent.Text = byId.MainContent;
			this.txtSupervisorSign.Text = byId.SupervisorSign;
			if (byId.SupervisorSignDate.HasValue)
			{
				this.txtSupervisorSignDate.Text = byId.SupervisorSignDate.Value.ToString("yyyy年MM月dd日");
			}
			else
			{
				this.txtSupervisorSignDate.Text = text;
			}
			this.txtGeneralSign.Text = byId.GeneralSign;
			if (byId.GeneralSignDate.HasValue)
			{
				this.txtGeneralSignDate.Text = byId.GeneralSignDate.Value.ToString("yyyy年MM月dd日");
			}
			else
			{
				this.txtGeneralSignDate.Text = text;
			}
			this.txtBuildUnitOpinion.Text = byId.BuildUnitOpinion;
			this.txtBuildUnitPerson.Text = byId.BuildUnitPerson;
			if (byId.BuildUnitSignDate.HasValue)
			{
				this.txtBuildUnitSignDate.Text = byId.BuildUnitSignDate.Value.ToString("yyyy年MM月dd日");
				return;
			}
			this.txtBuildUnitSignDate.Text = text;
		}
	}
	public string FilesBind(string consID)
	{
		string text = ConfigurationManager.AppSettings["RetMsg"].ToString();
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


