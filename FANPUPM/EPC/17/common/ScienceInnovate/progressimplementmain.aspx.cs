using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressImplementMain : NBasePage, IRequiresSessionState
	{
		protected int pageSize = 10;
		protected string prjCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["prjCode"] == null)
				{
					return;
				}
				this.ViewState["PRJCODE"] = base.Request.Params["prjCode"].ToString();
				this.prjCode = this.ViewState["PRJCODE"].ToString();
				if (base.Request.Params["Type"] != null)
				{
					if (base.Request.Params["Type"] == "Edit")
					{
						this.lbltitle.Text = "技术进步项目实施";
					}
					else
					{
						this.lbltitle.Text = "技术进步实施评价";
					}
					this.hidType.Value = base.Request.Params["Type"].ToString();
				}
				this.BindData();
			}
			this.prjCode = this.ViewState["PRJCODE"].ToString();
		}
		private void BindData()
		{
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			ProgressPlanCollection auditedProgressPlanInfos = progressPlanAction.GetAuditedProgressPlanInfos(this.prjCode);
			this.dgMain.DataSource = auditedProgressPlanInfos;
			this.dgMain.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgMain.ItemDataBound += new DataGridItemEventHandler(this.dgMain_ItemDataBound);
		}
		private void dgMain_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["id"] = this.dgMain.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow(this,'",
					this.dgMain.ClientID,
					"','",
					this.dgMain.DataKeys[e.Item.ItemIndex].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void pc_PageIndexChange(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void dgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgMain.CurrentPageIndex = e.NewPageIndex;
			this.BindData();
		}
	}


