using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CodeClassList : PageBase, IRequiresSessionState
	{

		protected CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.BtnAdd.Attributes["onclick"] = "return openContractEdit(1);";
				this.BtnMod.Attributes["onclick"] = "return openContractEdit(2);";
				this.DgdCodeType_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgdCodeType.ItemDataBound += new DataGridItemEventHandler(this.DgdCodeType_ItemDataBound);
		}
		private void DgdCodeType_Bind()
		{
			this.DgdCodeType.DataSource = this.CodingAct.QueryCodeTypeList();
			this.DgdCodeType.DataBind();
		}
		private void DgdCodeType_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DgdCodeType.ClientID,
					"');doClickRow('",
					e.Item.Cells[4].Text.ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				return;
			default:
				return;
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.DgdCodeType_Bind();
		}
		protected void BtnMod_Click(object sender, EventArgs e)
		{
			this.DgdCodeType_Bind();
		}
	}


