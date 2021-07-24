using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LinkmanSearch : BasePage, IRequiresSessionState
	{
		protected AddressListDb ald;
		protected string _iCorpCode;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ald = new AddressListDb();
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["iClient"] == null)
				{
					this.lblTitle.Text = "最近增加的联系人";
					DataTable dataSource = this.ald.eLastLinkmanQuary(15);
					this.dgLinkman.DataSource = dataSource;
					this.dgLinkman.DataBind();
					return;
				}
				string text = base.Request.QueryString["iClient"].ToString();
				this.lblTitle.Text = base.Request.QueryString["strClientName"].ToString() + " 下的联系人";
				this.Panel1.Visible = true;
				DataTable dataTable = this.ald.eGetCorpInfo(text);
				this.lblAddress.Text = dataTable.Rows[0]["v_dz"].ToString();
				this.lblPostalCode.Text = dataTable.Rows[0]["v_yb"].ToString();
				DataTable dataSource2 = this.ald.eGetCorpLinkman(text);
				this.dgLinkman.DataSource = dataSource2;
				this.dgLinkman.DataBind();
				this._iCorpCode = text;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgLinkman.ItemDataBound += new DataGridItemEventHandler(this.dgLinkman_ItemDataBound);
		}
		private void dgLinkman_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"javascript:window.showModalDialog('Broker.aspx?Op=Browse&iClient=",
					this._iCorpCode,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"',window,'dialogHeight:500px;dialogWidth:450px;center:1;help:0;status:0;');return false;"
				});
				e.Item.ToolTip = "双击查看详细信息";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[0].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('Broker.aspx?Op=Browse&iClient=",
					this._iCorpCode,
					"&id=",
					this.dgLinkman.DataKeys[e.Item.ItemIndex].ToString(),
					"','','left=100,top=100,width=450,height=500,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));"
				});
			}
		}
	}


