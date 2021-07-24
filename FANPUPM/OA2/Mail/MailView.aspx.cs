using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OA2_Mail_MailView : NBasePage, IRequiresSessionState
{
	private string zt = string.Empty;
	private string mailId = string.Empty;
	private MailService mailService = new MailService();
	private PTYhmcService ptyhcmcservice = new PTYhmcService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["mailId"]))
		{
			this.mailId = base.Request["mailId"];
		}
		if (!string.IsNullOrEmpty(base.Request["zt"]))
		{
			this.zt = base.Request["zt"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (this.zt == "1")
			{
				this.lbltx.Text = "发送成功!";
				this.BindData();
				return;
			}
			this.lbltx.Text = "保存成功!";
			this.TDAllName.Visible = false;
		}
	}
	private void BindData()
	{
		if (!string.IsNullOrEmpty(this.mailId))
		{
			Mail byId = this.mailService.GetById(this.mailId);
			this.TDAllName.Visible = true;
			string[] first = byId.AllMailToCode.Split(new char[]
			{
				','
			});
			string[] second = byId.AllCopytoCode.Split(new char[]
			{
				','
			});
			System.Collections.Generic.List<string> list = first.Union(second).ToList<string>();
			this.lblName.Text = string.Empty;
			this.lblB.Text = string.Empty;
			for (int i = 0; i < list.Count; i++)
			{
				PTyhmc pTyhmc = new PTyhmc();
				if (!string.IsNullOrEmpty(list[i]))
				{
					pTyhmc = this.ptyhcmcservice.GetById(list[i]);
					Label expr_C9 = this.lblB;
					expr_C9.Text = expr_C9.Text + "&nbsp;&nbsp" + pTyhmc.Bm.V_BMMC + "&nbsp;&nbsp;&nbsp;&nbsp;</br>";
					Label expr_F5 = this.lblName;
					expr_F5.Text = expr_F5.Text + pTyhmc.v_xm + "</br>";
				}
			}
		}
	}
}


