using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_Window_Start : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			TenderInfo byId = TenderInfo.GetById(this.prjId);
			DateTime? projStartDate = null;
			if (!string.IsNullOrEmpty(this.txtProjStartDate.Text))
			{
				projStartDate = new DateTime?(Convert.ToDateTime(this.txtProjStartDate.Text));
			}
			byId.ProjStartDate = projStartDate;
			byId.ProjStartRemark = this.txtStartRemark.Text;
			byId.PrjDutyPerson = this.hfldPrjDutyPerson.Value;
			byId.PrjManager = this.txtStartManager.Text;
			byId.UpdatePart(byId, "3");
			base.RegisterShow("系统提示", "启动成功");
		}
		catch (Exception)
		{
			base.RegisterShow("系统提示", "启动失败");
		}
	}
}


