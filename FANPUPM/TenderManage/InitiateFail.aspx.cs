using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_InitiateFail : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldUserCode.Value = base.UserCode;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			string value = this.hfldPrjId.Value;
			if (!string.IsNullOrEmpty(value))
			{
				PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
				PTPrjInfoZTBDetailService pTPrjInfoZTBDetailService = new PTPrjInfoZTBDetailService();
				PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(new System.Guid(value));
				PTPrjInfoZTBDetail byId2 = pTPrjInfoZTBDetailService.GetById(value);
				if (!string.IsNullOrEmpty(this.txtApplyDate1.Text.Trim()))
				{
					byId2.ProjApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate1.Text.Trim()));
				}
				if (!string.IsNullOrEmpty(this.txtProjStartDate1.Text.Trim()))
				{
					byId2.ProjStartDate = new System.DateTime?(System.Convert.ToDateTime(this.txtProjStartDate1.Text.Trim()));
				}
				byId2.ProjStartRemark = this.txtStartRemark1.Text.Trim();
				byId.PrjState = new int?(System.Convert.ToInt32(ProjectParameter.InitiateFail));
				byId.IsGiveUp = false;
				byId.OldState = null;
				byId.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
				pTPrjInfoZTBService.Update(byId);
				pTPrjInfoZTBDetailService.Update(byId2);
				base.RegisterScript("top.ui.show('报名不通过资料保存成功！');top.ui.winSuccess({parentName:'_InitiateFail'});");
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('报名不通过资料保存失败！');top.ui.winSuccess({parentName:'_InitiateFail'});");
		}
	}
}


