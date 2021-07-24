using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class buttom : BasePage, IRequiresSessionState
	{
		public string strSkinPath = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
			if (!this.Page.IsPostBack)
			{
				if (this.Session["yhdm"] != null)
				{
					userManageDb userManageDb = new userManageDb();
					Label expr_56 = this.l_name;
					expr_56.Text += userManageDb.GetUserName(this.Session["yhdm"].ToString());
					this.lb_cs.Text = "              登录次数：" + this.Session["ptcs"].ToString();
					if (base.Application["UserCodeCollection"] != null)
					{
						this.l_num.Text = " " + ((DataTable)base.Application["UserCodeCollection"]).Rows.Count.ToString() + " ";
					}
					else
					{
						this.l_num.Text = " 0 ";
					}
				}
				if (this.l_num.Text == " 0 ")
				{
					this.l_state.Text = "状态 :\u3000脱机";
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
		}
	}


