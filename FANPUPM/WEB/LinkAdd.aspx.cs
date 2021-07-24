using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class LinkAdd : BasePage, IRequiresSessionState
	{

		private void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.hdnType.Value = base.Request.QueryString["type"];
				if (this.hdnType.Value != "3" && this.hdnType.Value != "4")
				{
					this.tr1.Attributes["style"] = "DISPLAY: none";
					return;
				}
				this.cb.Checked = true;
				this.tr1.Attributes["style"] = "DISPLAY: ";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			string text = this.txtName.Text;
			string text2 = this.txtUrl.Text;
			string text3 = this.txtLogo.Text;
			string text4 = "";
			if (this.cb.Checked)
			{
				text4 = this.hdnType.Value;
			}
			string a = text2.Trim().Substring(0, 7);
			if (a != "http://")
			{
				text2 = "http://" + text2;
			}
			string text5 = " insert into Web_FriendLink values('" + text.Trim() + "',";
			string text6 = text5;
			text5 = string.Concat(new string[]
			{
				text6,
				" '",
				text2.Trim(),
				"','",
				text3.Trim(),
				"','",
				text4.Trim(),
				"')"
			});
			int num = publicDbOpClass.ExecSqlString(text5);
			if (num == 1)
			{
				this.js.Text = "alert('保存成功!');window.returnValue=true;window.close();";
				return;
			}
			this.js.Text = "alert('添加失败！');";
		}
	}


