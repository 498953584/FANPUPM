using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LoanTable : PageBase, IRequiresSessionState
	{
		protected FundApplication fac = new FundApplication();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.DGrdList.DataSource = this.fac.getFundSchedule();
				this.DGrdList.DataBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdList.ItemDataBound += new DataGridItemEventHandler(this.DGrdList_ItemDataBound);
		}
		private void DGrdList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = "OnRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
	}


