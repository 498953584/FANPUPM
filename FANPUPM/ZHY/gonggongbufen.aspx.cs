using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostFrameAnalyze : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string tempCode = base.Request.QueryString["TempCode"].ToString();
				string prjGuid = base.Request.QueryString["PrjGuid"].ToString();
				this.DG_ListBind(tempCode, prjGuid);
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
		protected void DG_list_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onclick"] = "doClick(this,'" + this.DG_list.ClientID + "');";
				decimal d = Convert.ToDecimal(e.Item.Cells[4].Text) / Convert.ToDecimal(e.Item.Cells[3].Text);
				e.Item.Cells[7].Text = Math.Round(d * 100m, 2).ToString() + "%";
			}
		}
		public void DG_ListBind(string TempCode, string PrjGuid)
		{
			DataSet allNews = BindBudgetAction.getAllNews(PrjGuid, TempCode);
			this.DG_list.DataSource = allNews;
			this.DG_list.DataBind();
		}
	}


