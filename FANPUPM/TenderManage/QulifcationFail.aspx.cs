using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_QulifcationFail : NBasePage, IRequiresSessionState
{
	private string SignUpWarnDay = ConfigurationManager.AppSettings["SignUpWarnDay"];

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
			string prjGuid = this.hfldPrjId.Value;
			if (!string.IsNullOrEmpty(prjGuid))
			{
				TenderInfo byId = TenderInfo.GetById(prjGuid);
				System.DateTime? projApplyDate = null;
				System.DateTime? qualificationFailData = null;
				if (!string.IsNullOrEmpty(this.txtApplyDate1.Text.Trim()))
				{
					projApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate1.Text.Trim()));
				}
				byId.ProjApplyDate = projApplyDate;
				System.DateTime? projApprovalDate = null;
				if (!string.IsNullOrEmpty(this.txtApprovalDate1.Text.Trim()))
				{
					projApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate1.Text.Trim()));
				}
				byId.ProjApprovalDate = projApprovalDate;
				System.DateTime? projTenderDate = null;
				if (!string.IsNullOrEmpty(this.txtTenderDate1.Text))
				{
					projTenderDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderDate1.Text.Trim()));
				}
				byId.ProjTenderDate = projTenderDate;
				if (!string.IsNullOrEmpty(this.txtRegistDeadline1.Text.Trim()))
				{
					byId.ProjRegistDeadline = new int?(System.Convert.ToInt32(this.txtRegistDeadline1.Text.Trim()));
				}
				byId.ProgAgent = this.hfldAgent1.Value;
				byId.PrequalificationRequire = this.txtPrequalificationRequire1.Text.Trim();
				if (!string.IsNullOrWhiteSpace(this.txtQualificationMargin1.Text))
				{
					byId.QualificationMargin = System.Convert.ToDecimal(this.txtQualificationMargin1.Text.Trim());
				}
				byId.QualificationReadOne = this.hfldQualificationReadOne1.Value;
				if (!string.IsNullOrEmpty(this.txtFailDate.Text))
				{
					qualificationFailData = new System.DateTime?(System.Convert.ToDateTime(this.txtFailDate.Text.Trim()));
				}
				byId.QualificationFailData = qualificationFailData;
				byId.QualificationFailReason = this.txtFailReason.Text.Trim();
				if (!string.IsNullOrEmpty(byId.QualificationReadOne))
				{
					PTDBSJService pTDBSJService = new PTDBSJService();
					pTDBSJService.Add(new PTDBSJ
					{
						I_XGID = byId.PrjGuid.ToString(),
						V_LXBM = "024",
						V_YHDM = byId.QualificationReadOne,
						DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
						C_OpenFlag = "0",
						V_Content = "项目：" + byId.PrjName + "已经开始预审失败",
						V_DBLJ = "TenderManage/InfoView.aspx?ic=" + byId.PrjGuid.ToString(),
						V_TPLJ = "new_Mail.gif"
					});
				}
				if (!string.IsNullOrEmpty(this.txtApplyDate1.Text.Trim()))
				{
					PTDBSJTodayService pTDBSJTodayService = new PTDBSJTodayService();
					System.Collections.Generic.List<PTDBSJToday> list = (
						from dbsj in pTDBSJTodayService
						where dbsj.I_XGID == prjGuid
						select dbsj).ToList<PTDBSJToday>();
					foreach (PTDBSJToday current in list)
					{
						pTDBSJTodayService.Delete(current);
					}
					System.Collections.Generic.List<TenderUser> byId2 = TenderUser.GetById(byId.PrjGuid.ToString());
					System.DateTime value = System.Convert.ToDateTime(this.txtApplyDate1.Text.Trim()).AddDays((double)(-(double)System.Convert.ToInt32(this.SignUpWarnDay)));
					foreach (TenderUser current2 in byId2)
					{
						pTDBSJTodayService.Add(new PTDBSJToday
						{
							I_XGID = prjGuid,
							V_LXBM = "027",
							V_YHDM = current2.UserCode,
							DTM_DBSJ = new System.DateTime?(value),
							V_TPLJ = "",
							V_DBLJ = "TenderManage/InfoView.aspx?ic=" + prjGuid,
							V_Content = "名称为：" + byId.PrjName + "的项目已经开始预审失败。",
							C_OpenFlag = "1"
						});
					}
				}
				PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
				PTPrjInfoZTB byId3 = pTPrjInfoZTBService.GetById(new System.Guid(prjGuid));
				byId3.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
				pTPrjInfoZTBService.Update(byId3);
				byId.UpdatePart(byId, ProjectParameter.QualificationFail);
				base.RegisterScript("top.ui.show('预审失败资料保存成功！');top.ui.winSuccess({parentName:'_QulifcationFail'});");
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('预审失败资料保存失败！');top.ui.winSuccess({parentName:'_QulifcationFail'});");
		}
	}
}


