using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class demo_ExportProject : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		string text = System.Convert.ToString(base.Request["id"]);
		string version = base.Request["version"];
		if (!string.IsNullOrEmpty(text))
		{
			System.Collections.Hashtable hashtable = DBProject.LoadProject(text, version);
			System.Collections.ArrayList tree = (System.Collections.ArrayList)hashtable["Tasks"];
			hashtable["Tasks"] = TreeUtil.ToList(tree, "-1", "children", "UID", "ParentTaskUID");
			string text2 = System.IO.Path.GetFileNameWithoutExtension(System.Convert.ToString(hashtable["Name"])) + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
			string text3 = HttpContext.Current.Server.MapPath("~/UploadFiles/Gantt/" + text2);
			PlusProject.Write(text3, hashtable);
			base.Response.Clear();
			base.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(text2, System.Text.Encoding.UTF8));
			base.Response.WriteFile(text3);
			base.Response.Flush();
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(text3);
			fileInfo.Delete();
			base.Response.End();
			return;
		}
		base.Response.Write("请传入正确的项目UID");
	}
}


