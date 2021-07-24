using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class Broker : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string str = "";
			string value = "";
			int num = 0;
			try
			{
				str = base.Request.QueryString.ToString();
				num = Convert.ToInt32(base.Request.QueryString["go"].Trim());
			}
			catch
			{
				base.Response.End();
			}
			switch (num)
			{
			case 1:
				value = "SubDept.aspx?" + str;
				break;
			case 2:
				value = "showMember.aspx?" + str;
				break;
			case 3:
				value = "showPrivilage.aspx?" + str;
				break;
			}
			this.FraOperate.Attributes["src"] = value;
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


