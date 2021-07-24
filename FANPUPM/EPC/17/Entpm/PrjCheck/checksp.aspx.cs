using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CheckSP : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack && base.Request["pk"] != null)
			{
				this.bind(base.Request["pk"].ToString());
			}
		}
		private void bind(string pk)
		{
			CheckInfo checkInfo = CheckAction.GetCheckInfo(pk);
			DateTime now = DateTime.Now;
			this.DateBox_sprq.Text = string.Format("{0:yyyy-MM-dd}", now);
			this.TextBox_spyj.Text = checkInfo.ExamineApproveIdea;
			this.TextBox_spr.Text = checkInfo.ExamineApprovePerson;
			if (checkInfo.ExamineApproveResult == 1)
			{
				this.RadioButton_ok.Checked = true;
				return;
			}
			if (checkInfo.ExamineApproveResult == 0)
			{
				this.RadioButton_no.Checked = true;
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
		protected void Button_save_Click(object sender, EventArgs e)
		{
			if (this.TextBox_spyj.Text.Length > this.TextBox_spyj.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('审核意见输入的内容过长！')";
				return;
			}
			CheckInfo checkInfo = new CheckInfo();
			if (base.Request["pk"] != null)
			{
				checkInfo.ID = int.Parse(base.Request["pk"].ToString());
			}
			if (!string.IsNullOrEmpty(this.DateBox_sprq.Text.Trim().ToString()))
			{
				checkInfo.ExamineApproveDate = DateTime.Parse(this.DateBox_sprq.Text);
			}
			else
			{
				checkInfo.ExamineApproveDate = DateTime.Now;
			}
			checkInfo.ExamineApproveIdea = this.TextBox_spyj.Text;
			checkInfo.ExamineApprovePerson = this.TextBox_spr.Text;
			if (this.RadioButton_ok.Checked)
			{
				checkInfo.ExamineApproveResult = 1;
			}
			else
			{
				checkInfo.ExamineApproveResult = -1;
			}
			bool flag = CheckAction.CheckInfoOp(checkInfo, "Sp");
			if (flag)
			{
				if (!this.RadioButton_ok.Checked)
				{
					CheckAction.UpdateThisSate(checkInfo.ID);
				}
				string text = " parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/Entpm/PrjCheck/CheckList.aspx&Type=ShenHe&PrjState=0&Levels=0';";
				text += "alert('保存成功');";
				text += "top.frameWorkArea.window.desktop.getActive().close();";
				this.JavaScriptControl1.Text = text;
				return;
			}
			this.JavaScriptControl1.Text = "alert('保存失败！');";
		}
	}


