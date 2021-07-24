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
	public partial class ContactCorpList : PageBase, IRequiresSessionState
	{

		protected int CorpTypeID
		{
			get
			{
				return Convert.ToInt32(this.ViewState["CORPTYPEID"]);
			}
			set
			{
				this.ViewState["CORPTYPEID"] = value;
			}
		}
		public string WindowType
		{
			get
			{
				object obj = this.ViewState["WINDOWTYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["WINDOWTYPE"] = value;
			}
		}
		protected ContactCorpAction Cont
		{
			get
			{
				return new ContactCorpAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["cti"] == null || base.Request["w"] == null)
				{
					this.JS.Text = "alert('参数错误！');window.close();";
					return;
				}
				this.CorpTypeID = Convert.ToInt32(base.Request["cti"]);
				this.WindowType = base.Request["w"];
				if (this.WindowType == "1")
				{
					this.BtnOK.Visible = true;
					this.BtnClose.Visible = true;
				}
				this.HdnTypeID.Value = this.CorpTypeID.ToString();
				this.Page_CustomInit();
				this.DgrdList_Bind();
				if (this.CorpTypeID == 4 || this.CorpTypeID == 5 || this.CorpTypeID == 10 || this.CorpTypeID == 19 || this.CorpTypeID == 20)
				{
					this.BtnAdd.Width = 0;
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
			this.DgrdList.ItemDataBound += new DataGridItemEventHandler(this.DgrdList_ItemDataBound);
		}
		private void DgrdList_Bind()
		{
			this.DgrdList.DataSource = this.Cont.QueryCorpList(this.CorpTypeID);
			this.DgrdList.DataBind();
		}
		private void Page_CustomInit()
		{
			this.BtnOK.Attributes["onclick"] = "dbclose();";
			this.BtnClose.Attributes["onclick"] = "window.close();";
			this.BtnAdd.Attributes["onclick"] = "if (!openContractEdit(1)){return false;}";
			this.BtnMod.Attributes["onclick"] = "if (!openContractEdit(2)){return false;}";
		}
		private void DgrdList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				com.jwsoft.pm.entpm.model.ContactCorp contactCorp = (com.jwsoft.pm.entpm.model.ContactCorp)e.Item.DataItem;
				e.Item.Cells[2].Text = com.jwsoft.pm.entpm.PageHelper.QueryBasicCode(this, BasicType.CorpKind, contactCorp.CorpKind);
				e.Item.Cells[3].Text = com.jwsoft.pm.entpm.PageHelper.QueryBasicCode(this, BasicType.FareSort, contactCorp.WorkType);
				if (this.WindowType == "1")
				{
					e.Item.Attributes["ondblclick"] = "dbclose();";
				}
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.DgrdList.ClientID,
					"');doClickRow('",
					contactCorp.CorpID,
					"','",
					contactCorp.CorpName,
					"','",
					contactCorp.AuditState.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			}
			default:
				return;
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			if (this.Cont.DelContactCorp(Convert.ToInt32(this.HdnCorpID.Value)) != 1)
			{
				this.JS.Text = "alert('删除数据出错！');";
				return;
			}
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
		protected void BtnMod_Click(object sender, EventArgs e)
		{
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
	}


