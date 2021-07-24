using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceGather : PageBase, IRequiresSessionState
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
		protected Guid TaskBookCode
		{
			get
			{
				return (Guid)this.ViewState["TaskBookCode"];
			}
			set
			{
				this.ViewState["TaskBookCode"] = value;
			}
		}
		protected TaskBookAction tba
		{
			get
			{
				return new TaskBookAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["bc"] == null)
			{
				this.Page.RegisterStartupScript("", "<script>alert('参数错误！');window.close();</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"]);
			this.TaskBookCode = new Guid(base.Request["bc"]);
			if (!this.Page.IsPostBack)
			{
				this.Data_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdRelation.ItemDataBound += new DataGridItemEventHandler(this.dgdRelation_ItemDataBound);
		}
		private void Data_Bind()
		{
			this.dgdRelation.DataSource = this.tba.GetConstructResourceGather(this.ProjectCode, this.TaskBookCode);
			this.dgdRelation.DataBind();
		}
		private void dgdRelation_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				return;
			default:
				return;
			}
		}
	}


