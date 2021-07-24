using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_SetAdjunct : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		PTPrjMemberService pTPrjMemberService = new PTPrjMemberService();
		try
		{
			PTPrjMember byId = pTPrjMemberService.GetById(this.hfldPrjMemberID.Value);
			if (byId != null)
			{
				byId.Post = this.txtMPost.Text.Trim();
				byId.Technical = this.txtMTechnical.Text.Trim();
				byId.TrainingInformation = this.txtMTrainingInformation.Text.Trim();
				byId.PastPerformance = this.txtMPastPerformance.Text.Trim();
				byId.PostAndCompetency = this.txtMPostAndCompetency.Text.Trim();
				byId.EducationalBackground = this.txtMEducationalBackground.Text.Trim();
				pTPrjMemberService.Update(byId);
				base.RegisterScript("top.ui.winSuccess({ parentName:'_SetAdjunct'});top.ui.closeWin();top.ui.show('保存成功！');");
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('保存失败！');");
		}
	}
}


