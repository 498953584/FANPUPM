﻿using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ProjectDocumentSearch : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.UProjectList11.UserCode = base.UserCode;
			this.UProjectList11.PrjUrl = "../ProjectOver/DocumentListView.aspx";
			this.UProjectList11.SubPrjUrl = "../ProjectOver/DocumentListView.aspx";
			this.UProjectList11.TargetFrame = "fraList";
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

