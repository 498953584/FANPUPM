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
public partial class TenderManage_GiveUpInfo : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string str = base.Request["prjId"];
			this.fileGiveUp.Class = "ProjectFile";
			this.fileGiveUp.RecordCode = str + "_" + ProjectParameter.GiveUpState;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			string g = base.Request["prjId"].ToString();
			System.Guid id = new System.Guid(g);
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(id);
			if (byId != null)
			{
				byId.GiveUpTime = Common2.ConverToDateTime(this.txtGiveUpTime.Text.Trim());
				if (byId.PrjState.ToString() != ProjectParameter.GiveUpState)
				{
					byId.OldState = byId.PrjState;
					if (byId.PrjState.ToString() == ProjectParameter.Initiate || byId.PrjState.ToString() == ProjectParameter.InitiateFail || byId.PrjState.ToString() == ProjectParameter.Approval)
					{
						byId.OldState = new int?(int.Parse(ProjectParameter.Approval));
					}
					if (byId.InitiateFlowState.ToString() == "1" && (byId.PrjState.ToString() == ProjectParameter.Initiate || byId.PrjState.ToString() == ProjectParameter.Prequalification || byId.PrjState.ToString() == ProjectParameter.QualificationPass || byId.PrjState.ToString() == ProjectParameter.QualificationFail))
					{
						byId.OldState = new int?(int.Parse(ProjectParameter.Initiate));
					}
					if (byId.PftFlowState.ToString() == "1" && (byId.PrjState.ToString() == ProjectParameter.QualificationPass || byId.PrjState.ToString() == ProjectParameter.Bid))
					{
						byId.OldState = new int?(int.Parse(ProjectParameter.QualificationPass));
					}
				}
				byId.PrjState = new int?(System.Convert.ToInt32(ProjectParameter.GiveUpState));
				byId.GiveUpReason = this.txtGiveUPReason.Text.Trim();
				byId.GiveUpNote = this.txtNote.Text.Trim();
				byId.Operator = this.hfldOperator.Value;
				byId.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
				byId.IsGiveUp = true;
				pTPrjInfoZTBService.Update(byId);
				base.RegisterScript("top.ui.show('保存成功！');top.ui.winSuccess({parentName:'_GiveUpInfo'});");
			}
		}
		catch
		{
			base.RegisterScript("top.ui.closeWin();top.ui.alert('保存失败！');");
		}
	}
}


