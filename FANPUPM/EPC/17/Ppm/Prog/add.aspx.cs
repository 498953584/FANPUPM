using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Add1 : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			int maxId = mgReport1.GetMaxId();
			this.FileLink1.MID = 1743;
			this.FileLink1.FID = maxId.ToString();
			this.FileLink1.Type = 1;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			string reportDpt = this.TextBox1.Text.Trim();
			DateTime? reportTime = null;
			string value = this.DateBox1.Text.Trim();
			if (!string.IsNullOrEmpty(value))
			{
				reportTime = new DateTime?(Convert.ToDateTime(value));
			}
			string text = this.TextBox4.Text.Trim();
			int maxId = mgReport1.GetMaxId();
			if (text.Length > 900)
			{
				this.Js.Text = "top.ui.show('曝光内容长度" + text.Length + "大于900个字符！未保存成功！')";
				return;
			}
			bool flag = mgReport1.InsertInfo(maxId, reportDpt, reportTime, text);
			if (flag)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_proglist' });");
				return;
			}
			this.Js.Text = "top.ui.alert('保存失败')";
		}
	}


