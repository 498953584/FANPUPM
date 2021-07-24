using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LockPage : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.ImageButton1.Click += new ImageClickEventHandler(this.ImageButton1_Click);
		}
		private void ImageButton1_Click(object sender, ImageClickEventArgs e)
		{
			if (userManageDb.ValidatorLoginAccess(this.tb_yhdm.Value, this.tb_pwd.Value) == null)
			{
				this.Js.Text = "alert('用户名或者密码输入错误，请重新输入！');";
				return;
			}
			this.Js.Text = "ReturnWindow();";
		}
	}


