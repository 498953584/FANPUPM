using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class down : BasePage, IRequiresSessionState
	{

		public string FileName
		{
			get
			{
				object obj = this.ViewState["FileName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FileName"] = value;
			}
		}
		public string FilePath
		{
			get
			{
				object obj = this.ViewState["FilePath"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FilePath"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.FileName = base.Request["fileName"];
				this.FilePath = base.Request["filePath"];
				if (this.FilePath != "")
				{
					string path = HttpContext.Current.Server.MapPath(this.FilePath);
					FileStream fileStream = null;
					try
					{
						fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
						int num = (int)fileStream.Length;
						byte[] buffer = new byte[num];
						int count = fileStream.Read(buffer, 0, num);
						HttpContext.Current.Response.ContentType = "application/octet-stream";
						HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.FileName, Encoding.UTF8));
						HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
						HttpContext.Current.Response.Flush();
						HttpContext.Current.Response.End();
						return;
					}
					finally
					{
						fileStream.Close();
					}
				}
				base.Response.Write("文件不存在！");
			}
		}
	}


