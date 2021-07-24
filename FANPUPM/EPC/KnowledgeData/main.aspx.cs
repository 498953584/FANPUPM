using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
	public partial class Main : PageBase, IRequiresSessionState
	{
		protected string strTree = "";
		protected string strRur = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string text = base.Request.QueryString["type"];
				string text2 = base.Request.QueryString["tag"];
				string str;
				if (text2 == "s")
				{
					str = "3";
				}
				else
				{
					str = "2";
				}
				string a;
				if ((a = text) != null)
				{
					if (a == "1")
					{
						this.strRur = "TypeList.aspx?TypeId=" + str + "&tag=" + text2;
						this.strTree = "TypeTree.aspx?type=" + str + "&tag=" + text2;
						return;
					}
					if (a == "2")
					{
						this.strRur = "DatumList.aspx?TypeId=&PrjCode=";
						this.strTree = "TypeTree.aspx?type=2";
						return;
					}
					if (a == "3")
					{
						this.strRur = "DatumAffirmList.aspx?TypeId=&PrjCode=";
						this.strTree = "TypeTree.aspx?type=3";
						return;
					}
					if (a == "4")
					{
						this.strRur = "DatumSeach.aspx?TypeId=&PrjCode=";
						this.strTree = "TypeTree.aspx?type=4";
						return;
					}
					if (a == "12")
					{
						this.strRur = "TypeList.aspx?TypeId=2";
						this.strTree = "TypeTree.aspx?type=2";
						return;
					}
					if (!(a == "13"))
					{
						return;
					}
					this.strRur = "TypeList.aspx?TypeId=3";
					this.strTree = "TypeTree.aspx?type=3";
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


