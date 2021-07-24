using ASP;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class Common_DownLoad : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		HttpUtility.UrlDecode(base.Request["path"]);
		this.DownLoad(base.Request.QueryString["path"]);
	}
	private void DownLoad(string path)
	{
		if (!System.IO.File.Exists(path))
		{
			path = base.Server.MapPath(path);
		}
		if (path != null && System.IO.File.Exists(path))
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
			base.Response.Clear();
			base.Response.AddHeader("Content-Disposition", "attachment;   filename=" + this.EncodeFileName(fileInfo.Name));
			base.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
			base.Response.ContentType = "application/octet-stream";
			base.Response.WriteFile(fileInfo.FullName);
			base.Response.End();
		}
	}
	private string EncodeFileName(string fileName)
	{
		string browser = base.Request.Browser.Browser;
		if (browser.ToUpper() == "IE" || browser.ToUpper() == "CHROME")
		{
			fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
		}
		fileName = fileName.Replace("+", "%20");
		fileName = fileName.Replace("%2b", "+");
		return fileName;
	}
}


