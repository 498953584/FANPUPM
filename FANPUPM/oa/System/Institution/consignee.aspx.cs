using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class Consignee : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["ucode"] != null && base.Request["un"] != null)
			{
				this.Session["HumanCode"] = ((base.Request["ucode"].ToString() == "") ? "" : (base.Request["ucode"].ToString().Replace(',', '!') + "!"));
				this.Session["HumanName"] = ((base.Request["un"].ToString() == "") ? "" : (base.Request["un"].ToString() + ","));
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


