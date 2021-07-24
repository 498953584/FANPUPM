using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Edit1 : NBasePage, IRequiresSessionState
	{
		private string _Type = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.SetBind();
			}
			if (base.Request["op"] == null)
			{
				this.FileLink1.Type = 1;
				return;
			}
			this.btnSave.Width = 0;
			this.TextBox1.ReadOnly = true;
			this.DateBox1.ReadOnly = true;
			this.TextBox4.ReadOnly = true;
			if (base.Request["op"] == "query")
			{
				this.btnSave.Width = 0;
				this.TextBox1.Enabled = false;
				this.DateBox1.Enabled = false;
				this.TextBox4.Enabled = false;
				this.FileLink1.Visible = false;
				this.Literal1.Text = FileView.FilesBind(1743, base.Request.QueryString["id"]);
				return;
			}
			this.btnSave.Width = 0;
			this.TextBox1.ReadOnly = true;
			this.DateBox1.ReadOnly = true;
			this.TextBox4.ReadOnly = true;
		}
		private void SetBind()
		{
			string text = base.Request.QueryString["id"];
			DataTable single = mgReport1.GetSingle(text);
			if (single.Rows.Count > 0)
			{
				DataRow dataRow = single.Rows[0];
				this.TextBox1.Text = dataRow["ReportDpt"].ToString();
				string text2 = (dataRow["ReportTime"] == DBNull.Value) ? string.Empty : DateTime.Parse(dataRow["ReportTime"].ToString()).ToShortDateString();
				this.DateBox1.Text = text2;
				this.TextBox4.Text = dataRow["Remark"].ToString();
			}
			this.FileLink1.MID = 1743;
			this.FileLink1.FID = text;
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
			string text = this.TextBox1.Text.Trim();
			string text2 = this.TextBox4.Text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				this.Js.Text = "top.ui.show('部门名称不能为空！')";
				return;
			}
			if (text2.Length > 900)
			{
				this.Js.Text = "top.ui.show('曝光内容长度" + text2.Length + "大于900个字符！未保存成功！')";
				return;
			}
			DateTime? reportTime = null;
			string value = this.DateBox1.Text.Trim();
			if (!string.IsNullOrEmpty(value))
			{
				reportTime = new DateTime?(Convert.ToDateTime(value));
			}
			string reportId = base.Request.QueryString["id"];
			bool flag = mgReport1.UpdateInfo(text, reportTime, text2, reportId);
			if (flag)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_proglist' });");
				return;
			}
			this.Js.Text = "top.ui.alert('更新失败');";
		}
	}


