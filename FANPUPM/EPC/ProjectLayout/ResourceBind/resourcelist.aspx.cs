using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceList : BasePage, IRequiresSessionState
	{

		protected Guid ProjectCode
		{
			get
			{
				return (Guid)this.ViewState["PROJECTCODE"];
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected ResourceBindAction rba
		{
			get
			{
				return new ResourceBindAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["PrjCode"] == null)
				{
					return;
				}
				this.ProjectCode = new Guid(base.Request["PrjCode"]);
				this.ResourceBind();
				this.BtnRBind.Attributes["onclick"] = "javascript:return openBindList();";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgd_List.ItemDataBound += new DataGridItemEventHandler(this.dgd_List_ItemDataBound);
		}
		private void ResourceBind()
		{
			this.dgd_List.DataSource = this.rba.ResourceList(this.ProjectCode);
			this.dgd_List.DataBind();
		}
		private void dgd_List_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgd_List.ClientID.ToString(),
					"');doRows('1','",
					e.Item.Cells[1].Text.Replace('\r', ' ').Replace('\n', ' '),
					"','0')"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
			}
		}
		protected void BtnRBind_Click(object sender, EventArgs e)
		{
			this.ResourceBind();
		}
	}


