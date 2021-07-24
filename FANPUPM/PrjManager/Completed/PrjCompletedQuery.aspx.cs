using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using com.jwsoft.pm.entpm;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_Completed_PrjCompletedQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["ic"]))
		{
			this.lblPrjName.Text = ProjectInfo.GetProjectName(base.Request["ic"].ToString());
			this.BindGv(base.Request["ic"].ToString());
		}
	}
	public void BindGv(string prjGuid)
	{
		this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		DataTable prjComplete = PrjCompleted.GetPrjComplete(prjGuid);
		this.gvComplete.DataSource = prjComplete;
		this.gvComplete.DataBind();
		bool flag = PrjCompleted.IsCompleted(prjGuid);
		if (flag)
		{
			this.trCompleted.Visible = true;
			this.lblCompletedDate.Text = Common2.GetTime(PrjCompleted.GetCompletedDate(prjGuid));
			this.td_completedNote.InnerHtml = PrjCompleted.GetCompletedNote(prjGuid);
		}
	}
	public string FilesBind(string id)
	{
		string str = ConfigurationManager.AppSettings["FileInfo"].ToString();
		string result;
		try
		{
			DataTable filesName = CompletedAnnex.GetFilesName(base.Request["ic"].ToString(), id);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			for (int i = 0; i < filesName.Rows.Count; i++)
			{
				string value = string.Empty;
				string str2 = str + "/" + filesName.Rows[i]["FileNewName"].ToString();
				value = string.Concat(new string[]
				{
					"<a  class=\"link\" style=\"white-space:nowrap; color:black;\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					filesName.Rows[i]["FileName"].ToString(),
					"</a> <br/>"
				});
				stringBuilder.Append(value);
			}
			result = stringBuilder.ToString();
		}
		catch
		{
			result = "";
		}
		return result;
	}
}


