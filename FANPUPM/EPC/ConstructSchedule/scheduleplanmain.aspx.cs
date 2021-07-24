using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
	public partial class SchedulePlanMain : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				this.UProjectList1.SubPrjUrl = "../ConstructSchedule/WBSQuery.aspx";
				this.UProjectList1.TargetFrame = "InfoList";
				this.strconn();
			}
			this.BtnSchedulePlan.Attributes["onclick"] = "javascript:return OpenScheudleWin('Plan');";
			this.BtnView.Attributes["onclick"] = "javascript:return OpenScheudleWin('View');";
			this.BtnGTT.Attributes["onclick"] = "javascript:return OpenScheudleWin('GTT');";
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
					this.Hdnstr.Value = string.Format(text2.Replace("\\", "\\\\"), new object[0]);
				}
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
	}


