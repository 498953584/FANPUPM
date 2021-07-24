using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProjectBidFrame : PageBase, IRequiresSessionState
	{

		public string Url
		{
			get
			{
				return this.ViewState["Url"].ToString();
			}
			set
			{
				this.ViewState["Url"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["Url"] != null && base.Request.QueryString["Url"].ToString() != "")
				{
					this.Url = base.Request.QueryString["Url"].ToString();
					this.iframeMain.Attributes["src"] = this.Url;
				}
				if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
				{
					string a = base.Request.QueryString["Type"].ToString().Trim();
					if (a == "Market")
					{
						this.lblDate.Text = "添加时间：";
						this.lblPrice.Text = "项目估价：";
						this.lblBidDept.Visible = false;
						this.tbBidDept.Visible = false;
						this.IMG1.Visible = false;
						this.lblBingoCorp.Visible = false;
						this.tbBingoCorp.Visible = false;
						this.ImgBingoCorp.Visible = false;
					}
					else
					{
						if (a == "InviteBid")
						{
							this.lblDate.Text = "招标时间：";
							this.lblPrice.Text = "投标报价：";
							this.lblStatus.Visible = false;
							this.ddlStatus.Visible = false;
							this.lblBingoCorp.Visible = false;
							this.tbBingoCorp.Visible = false;
							this.ImgBingoCorp.Visible = false;
						}
						else
						{
							if (a == "BingoBid")
							{
								this.lblDate.Text = "开标时间：";
								this.lblPrice.Text = "中 标 价：";
								this.lblBidDept.Visible = false;
								this.tbBidDept.Visible = false;
								this.IMG1.Visible = false;
								this.lblBidDept.Visible = false;
								this.tbBingoCorp.Visible = true;
							}
						}
					}
				}
				this.ddlCorp.DataSource = ContactCorpAction.getOwnerCorpList();
				this.ddlCorp.DataTextField = "CorpName";
				this.ddlCorp.DataValueField = "CorpID";
				this.ddlCorp.DataBind();
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddlPrjType, BasicType.ProjectType);
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddlInviteBidType, BasicType.InviteBidType);
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddlInviteBidMode, BasicType.InviteBidMode);
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.ddlAddress, BasicType.ProjectArea);
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
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
			{
				SearchInfo searchInfo = new SearchInfo();
				string text = base.Request.QueryString["Type"].ToString().Trim();
				if (text == "Market")
				{
					searchInfo.BeginDate = this.dbBegin.Text.Trim();
					searchInfo.EndDate = this.dbEnd.Text.Trim();
					searchInfo.InfoCode = this.tbInfoCode.Text.Trim();
					searchInfo.PrjName = this.tbPrjName.Text.Trim();
					searchInfo.PrjCorp = this.ddlCorp.SelectedValue;
					searchInfo.PrjType = this.ddlPrjType.SelectedValue;
					searchInfo.Price = this.ddlPrice.SelectedValue;
					searchInfo.PrjAddress = this.ddlAddress.SelectedValue;
					searchInfo.Status = this.ddlStatus.SelectedValue;
					searchInfo.InviteBidType = this.ddlInviteBidType.SelectedValue;
					searchInfo.InviteBidMode = this.ddlInviteBidMode.SelectedValue;
				}
				else
				{
					if (text == "InviteBid")
					{
						searchInfo.BeginDate = this.dbBegin.Text.Trim();
						searchInfo.EndDate = this.dbEnd.Text.Trim();
						searchInfo.InfoCode = this.tbInfoCode.Text.Trim();
						searchInfo.PrjName = this.tbPrjName.Text.Trim();
						searchInfo.PrjCorp = this.ddlCorp.SelectedValue;
						searchInfo.PrjType = this.ddlPrjType.SelectedValue;
						searchInfo.Price = this.ddlPrice.SelectedValue;
						searchInfo.PrjAddress = this.ddlAddress.SelectedValue;
						searchInfo.InviteBidType = this.ddlInviteBidType.SelectedValue;
						searchInfo.InviteBidMode = this.ddlInviteBidMode.SelectedValue;
						searchInfo.InviteBidDept = this.HdnDeptID.Value.Trim();
					}
					else
					{
						if (text == "BingoBid")
						{
							searchInfo.BeginDate = this.dbBegin.Text.Trim();
							searchInfo.EndDate = this.dbEnd.Text.Trim();
							searchInfo.InfoCode = this.tbInfoCode.Text.Trim();
							searchInfo.PrjName = this.tbPrjName.Text.Trim();
							searchInfo.PrjCorp = this.ddlCorp.SelectedValue;
							searchInfo.PrjType = this.ddlPrjType.SelectedValue;
							searchInfo.Price = this.ddlPrice.SelectedValue;
							searchInfo.PrjAddress = this.ddlAddress.SelectedValue;
							searchInfo.InviteBidType = this.ddlInviteBidType.SelectedValue;
							searchInfo.InviteBidMode = this.ddlInviteBidMode.SelectedValue;
							searchInfo.BingoCorp = this.HdnBingoCorp.Value.Trim();
						}
					}
				}
				string str = SearchAction.returnQueryFilter(searchInfo, text);
				if (base.Request.QueryString["Url"] != null && base.Request.QueryString["Url"].ToString() != "")
				{
					this.Url = base.Request.QueryString["Url"].ToString();
					this.iframeMain.Attributes["src"] = this.Url + "?where=" + str;
				}
			}
		}
	}


