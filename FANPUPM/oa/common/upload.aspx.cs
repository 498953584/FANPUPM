using ASP;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class upload : Page, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.doFormUploadDisk();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		public void doFormUploadDisk()
		{
			if (!this.Page.IsPostBack)
			{
				string str = base.Server.MapPath(this.Session["uploadpath"].ToString());
				HttpFileCollection files = base.Request.Files;
				for (int i = 0; i < files.Count; i++)
				{
					HttpPostedFile httpPostedFile = files[i];
					if (files.GetKey(i).ToUpper() == "EDITFILE")
					{
						string text = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf('\\') + 1);
						httpPostedFile.SaveAs(str + "\\" + text);
						base.Response.Write("文件名称: " + text + "<br>");
						base.Response.Write("大小: " + httpPostedFile.ContentLength.ToString() + "\t字节<br>");
						base.Response.Write("已成功保存到Web服务器上。<br>");
					}
				}
			}
		}
	}


