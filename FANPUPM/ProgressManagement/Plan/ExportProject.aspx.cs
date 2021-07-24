using ASP;
using PluSoft.Utils;
using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class demo_ExportProject : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		string text = System.Convert.ToString(base.Request["id"]);
		if (!string.IsNullOrEmpty(text))
		{
			System.Collections.Hashtable hashtable = DBProject.LoadProject(text);
			System.Collections.ArrayList tree = (System.Collections.ArrayList)hashtable["Tasks"];
			hashtable["Tasks"] = TreeUtil.ToList(tree, "-1", "children", "UID", "ParentTaskUID");
			string text2 = System.IO.Path.GetFileNameWithoutExtension(System.Convert.ToString(hashtable["Name"])) + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
			string text3 = HttpContext.Current.Server.MapPath("~/UploadFiles/Gantt/");
			if (!System.IO.Directory.Exists(text3))
			{
				System.IO.Directory.CreateDirectory(text3);
			}
			string text4 = text3 + text2;
			PlusProject.Write(text4, hashtable);
			base.Response.Clear();
			System.IO.FileStream fileStream = null;
			try
			{
				fileStream = new System.IO.FileStream(text4, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				int num = (int)fileStream.Length;
				byte[] buffer = new byte[num];
				int count = fileStream.Read(buffer, 0, num);
				HttpContext.Current.Response.ContentType = "application/octet-stream";
				HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(text2));
				HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			finally
			{
				fileStream.Close();
			}
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(text4);
			fileInfo.Delete();
			base.Response.End();
			return;
		}
		base.Response.Write("请传入正确的项目UID");
	}
}


