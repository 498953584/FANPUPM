using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TechnologyProposalList : NBasePage, System.Web.SessionState.IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string str = base.Request.QueryString["ItemCode"];
			string str2 = base.Request.QueryString["Type"];
			if (!this.Page.IsPostBack)
			{
				base.Response.Cache.SetNoStore();
				string sqlString = "select Tech.*,Yhmc.v_xm from Prj_TechnologyProposal Tech INNER JOIN  PT_YHMC Yhmc ON Tech.TechProposaler = Yhmc.v_yhdm where  TechType='" + str2 + "' and TechItemId=" + str;
				this.dgList.DataSource = publicDbOpClass.DataTableQuary(sqlString);
				this.dgList.DataBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
		}
		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
			}
		}
	}


