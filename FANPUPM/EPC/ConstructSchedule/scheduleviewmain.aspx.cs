using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Xml;
	public partial class ScheduleViewMain : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.strconn();
				this.UCProjectList.UserCode = base.UserCode;
				this.UCProjectList.SubPrjUrl = "../ConstructSchedule/ScheduleViewList.aspx";
				this.UCProjectList.TargetFrame = "InfoList";
				this.Page.RegisterStartupScript("", string.Concat(new object[]
				{
					"<script language=\"javascript\">InfoList.location = 'ScheduleViewList.aspx?PrjCode=&PrjGuid=",
					Guid.Empty,
					"&pc=",
					Guid.Empty,
					"&pn=&cc=';</script>"
				}));
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
		public void strconn()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(base.Server.MapPath("/Web.config"));
			foreach (XmlNode xmlNode in xmlDocument["configuration"]["connectionStrings"])
			{
				if (xmlNode.Name == "add" && xmlNode.Attributes[1].Name == "connectionString")
				{
					string text = xmlNode.Attributes[1].Value.ToString();
					string[] array = text.Split(new char[]
					{
						';'
					});
					string str = array[0].ToString();
					string str2 = array[4].ToString().Split(new char[]
					{
						'='
					})[1].ToString();
					string text2 = str2 + ";" + str;
					this.Session["HdnAspstr"] = string.Format(text2.Replace("\\", "\\\\"), new object[0]);
				}
			}
		}
	}


