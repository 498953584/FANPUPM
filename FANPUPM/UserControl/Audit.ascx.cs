using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class UserControl_Audit : System.Web.UI.UserControl
{
	private string pcId = string.Empty;
	private string showAudit = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.pcId = base.Request["ic"];
		}
		if (!string.IsNullOrEmpty(base.Request["showAudit"]))
		{
			this.showAudit = base.Request["showAudit"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && this.showAudit == "1")
		{
			this.InitialRptAudit();
		}
	}
	public string GetNamePath(object userCode)
	{
		string result = string.Empty;
		PTLOGINService pTLOGINService = new PTLOGINService();
		PTLOGIN byUserCode = pTLOGINService.GetByUserCode(userCode.ToString());
		if (byUserCode != null)
		{
			result = byUserCode.AuditNameImagePath;
		}
		return result;
	}
	public string GetName(object userCode)
	{
		string result = string.Empty;
		PTYhmcService pTYhmcService = new PTYhmcService();
		PTyhmc byId = pTYhmcService.GetById(userCode.ToString());
		if (byId != null)
		{
			result = byId.v_xm;
		}
		return result;
	}
	public PTyhmc GetOrginator()
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		PTYhmcService pTYhmcService = new PTYhmcService();
		WFInstanceMain wFInstanceMain = wFInstanceMainService.GetByInstanceCode(this.pcId).FirstOrDefault<WFInstanceMain>();
		if (wFInstanceMain != null)
		{
			return pTYhmcService.GetById(wFInstanceMain.Organiger);
		}
		return null;
	}
	public string GetOriginatorDate()
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		WFInstanceMain wFInstanceMain = wFInstanceMainService.GetByInstanceCode(this.pcId).FirstOrDefault<WFInstanceMain>();
		if (wFInstanceMain != null)
		{
			return wFInstanceMain.StartTime.Value.ToString("yyyy-MM-dd");
		}
		return string.Empty;
	}
	private void InitialRptAudit()
	{
		try
		{
			WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
			WFInstanceService source = new WFInstanceService();
			IList<WFInstanceMain> byInstanceCode = wFInstanceMainService.GetByInstanceCode(this.pcId);
			List<WFInstance> list = new List<WFInstance>();
			foreach (WFInstanceMain im in byInstanceCode)
			{
				List<WFInstance> list2 = (
					from i in source
					where i.ID == (int?)im.ID
					orderby i.AuditDate descending
					select i).ToList<WFInstance>();
				foreach (WFInstance current in list2)
				{
					if (im.ID == byInstanceCode.Last<WFInstanceMain>().ID || current.Sing.Value != -1)
					{
						list.Add(current);
					}
				}
			}
			if (list.Count > 0)
			{
				this.rptAudit.DataSource = list;
				this.rptAudit.DataBind();
			}
		}
		catch
		{
		}
	}
	public string GetAnnex(string wfmId, string noteid)
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		WFInstanceMain byId = wFInstanceMainService.GetById(Convert.ToInt32(wfmId));
		string text = ConfigurationManager.AppSettings["Audit"].ToString();
		string result;
		try
		{
			string[] files = Directory.GetFiles(string.Concat(new object[]
			{
				base.Server.MapPath(text),
				"\\",
				byId.InstanceCode,
				"\\",
				noteid
			}));
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = string.Concat(new object[]
				{
					text,
					"/",
					byId.InstanceCode,
					"/",
					noteid
				});
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
				stringBuilder.Append(" <br/ >");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 6)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 6);
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


