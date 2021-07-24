using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_PrjMemberView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initialize();
		}
	}
	protected void Initialize()
	{
		string prjId = base.Request["ic"];
		this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		string text = string.Empty;
		try
		{
			text = ProjectInfo.GetProjectName(prjId);
		}
		catch
		{
		}
		this.lblMemberTitle.Text = text;
		this.bindGv();
	}
	protected void bindGv()
	{
		this.gvwPrjMembers.DataSource = PrjMember.GetMembers(base.Request["ic"], "", "", "", "", "", 1, 2147483647);
		this.gvwPrjMembers.DataBind();
	}
	protected void gvwPrjMembers_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			return;
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string recordCode = this.gvwPrjMembers.DataKeys[e.Row.RowIndex]["PrjMemberId"].ToString();
			e.Row.Cells[e.Row.Cells.Count - 1].Text = this.GetAdjunct(recordCode);
		}
	}
	protected string GetAdjunct(string recordCode)
	{
		string text = ConfigHelper.Get("ProjectFile");
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + recordCode);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
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


