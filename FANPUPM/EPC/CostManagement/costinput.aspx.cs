using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
	public partial class CostInput : PageBase, IRequiresSessionState
	{
		public string PrjCode;
		public string Type;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] != null)
			{
				this.PrjCode = base.Request["PrjCode"].ToString();
			}
			if (base.Request["Type"] != null)
			{
				this.Type = base.Request["Type"].ToString();
			}
			this.DataBind();
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


