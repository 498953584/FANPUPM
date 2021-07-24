using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TechnologyProposalEdit : NBasePage, IRequiresSessionState
	{
		private string ItemCode;
		private string Type;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ItemCode = base.Request.QueryString["ItemCode"];
			this.Type = base.Request.QueryString["Type"];
			if (!this.Page.IsPostBack)
			{
				if (this.Type == "1")
				{
					this.lb_Surprise.Text = "施工组织设计";
				}
				else
				{
					this.lb_Surprise.Text = "专项方案";
				}
			}
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			if (currentUserInfo != null)
			{
				this.txtMan.Text = currentUserInfo.UserName;
			}
			else
			{
				this.js.Text = "alert(\"用户身份过期，请重新登录！\");window.close();";
			}
			this.txtDate.Text = DateTime.Today.ToShortDateString();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			string text = "";
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			if (currentUserInfo != null)
			{
				text = currentUserInfo.UserCode;
			}
			else
			{
				this.js.Text = "alert(\"用户身份过期，请重新登录！\");window.close();";
			}
			string text2 = "declare @Index int; set @Index=0";
			text2 += " select @Index=TechIndex from Prj_TechnologyProposal where TechType={0} and TechItemId='{1}' and TechProposaler='{2}'";
			text2 += " insert into Prj_TechnologyProposal values('{1}','{3}','{4}','{2}',{0})";
			text2 = string.Format(text2, new object[]
			{
				this.Type,
				this.ItemCode,
				text,
				this.txtContent.Text,
				this.txtDate.Text
			});
			if (publicDbOpClass.NonQuerySqlString(text2))
			{
				string text3 = "parent.desktop.flowclass.location=parent.desktop.flowclass.location;";
				text3 += "alert('保存成功');";
				text3 += "top.frameWorkArea.window.desktop.getActive().close();";
				this.js.Text = text3;
				return;
			}
			this.js.Text = "alter(\"网络连接失败，请稍候再试！\");window.close();";
		}
	}


