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
	public partial class SuperviseSp : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.DateBox_sprq.Text = DateTime.Now.ToString("d");
				if (base.Request["pk"] != null)
				{
					this.bind(base.Request["pk"].ToString());
				}
			}
		}
		private void bind(string pk)
		{
			SuperviseInfo superviseInfo = SuperviseAction.GetSuperviseInfo(pk);
			this.DateBox_sprq.Text = ((superviseInfo.ApproveDate.ToString("d") == "0001/1/1") ? DateTime.Now.ToString("d") : superviseInfo.ApproveDate.ToString("d"));
			this.TextBox_spyj.Text = superviseInfo.ApproveIdea;
			this.TextBox_spr.Text = superviseInfo.ApprovePerson;
			if (superviseInfo.ApproveResult == 1)
			{
				this.RadioButton_ok.Checked = true;
				return;
			}
			if (superviseInfo.ApproveResult == 0)
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
			SuperviseInfo superviseInfo = new SuperviseInfo();
			if (base.Request["pk"] != null)
			{
				superviseInfo.ID = int.Parse(base.Request["pk"].ToString());
			}
			superviseInfo.ApproveDate = DateTime.Parse(this.DateBox_sprq.Text);
			superviseInfo.ApproveIdea = this.TextBox_spyj.Text;
			superviseInfo.ApprovePerson = this.TextBox_spr.Text;
			if (this.RadioButton_ok.Checked)
			{
				superviseInfo.ApproveResult = 1;
			}
			else
			{
				superviseInfo.ApproveResult = -1;
			}
			bool flag = SuperviseAction.SuperviseInfoOp(superviseInfo, "Sp");
			if (flag)
			{
				base.RegisterScript(" top.ui.tabSuccess({ parentName: '_superviselist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败！');");
		}
	}


