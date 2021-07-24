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
	public partial class ItemProgSp : NBasePage, IRequiresSessionState
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
			ItemProgInfo itemProgInfo = ItemProgAction.GetItemProgInfo(pk);
			DateTime now = DateTime.Now;
			this.DateBox_sprq.Text = string.Format("{0:yyyy-MM-dd}", now);
			this.TextBox_spyj.Text = itemProgInfo.ApproveIdea;
			this.TextBox_spr.Text = itemProgInfo.ApprovePerson;
			if (itemProgInfo.ApproveResult == 1)
			{
				this.RadioButton_ok.Checked = true;
				return;
			}
			if (itemProgInfo.ApproveResult == 0)
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
			ItemProgInfo itemProgInfo = new ItemProgInfo();
			if (base.Request["pk"] != null)
			{
				itemProgInfo.ID = int.Parse(base.Request["pk"].ToString());
			}
			itemProgInfo.ApproveDate = DateTime.Parse(this.DateBox_sprq.Text);
			itemProgInfo.ApproveIdea = this.TextBox_spyj.Text;
			itemProgInfo.ApprovePerson = this.TextBox_spr.Text;
			if (this.RadioButton_ok.Checked)
			{
				itemProgInfo.ApproveResult = 1;
			}
			else
			{
				itemProgInfo.ApproveResult = -1;
			}
			bool flag = ItemProgAction.ItemProgInfoOp(itemProgInfo, "Sp");
			if (flag)
			{
				string text = " parent.desktop.flowclass.location='/EPC/17/Frame.aspx?url=../../epc/17/PPM/Prog/ItemProgList.aspx&Type=ShenHe&PrjState=0&Levels=0';";
				text += "alert('保存成功');";
				text += "top.frameWorkArea.window.desktop.getActive().close();";
				this.JavaScriptControl1.Text = text;
				return;
			}
			this.JavaScriptControl1.Text = "alert('保存失败！');";
		}
	}


