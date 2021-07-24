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
public partial class TenderManage_QulificationPass : NBasePage, IRequiresSessionState
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
				System.DateTime? qualificationPassDate = null;
				System.DateTime? projApplyDate = null;
				if (!string.IsNullOrEmpty(this.txtApplyDate.Text.Trim()))
				{
					projApplyDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApplyDate.Text.Trim()));
				}
				byId.ProjApplyDate = projApplyDate;
				System.DateTime? projApprovalDate = null;
				if (!string.IsNullOrEmpty(this.txtApprovalDate.Text.Trim()))
				{
					projApprovalDate = new System.DateTime?(System.Convert.ToDateTime(this.txtApprovalDate.Text.Trim()));
				}
				byId.ProjApprovalDate = projApprovalDate;
				System.DateTime? projTenderDate = null;
				if (!string.IsNullOrEmpty(this.txtTenderDate.Text))
				{
					projTenderDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderDate.Text));
				}
				byId.ProjTenderDate = projTenderDate;
				if (!string.IsNullOrEmpty(this.txtRegistDeadline.Text.Trim()))
				{
					byId.ProjRegistDeadline = new int?(System.Convert.ToInt32(this.txtRegistDeadline.Text.Trim()));
				}
				byId.ProgAgent = this.hfldAgent.Value;
				byId.PrequalificationRequire = this.txtPrequalificationRequire.Text;
				if (!string.IsNullOrWhiteSpace(this.txtQualificationMargin.Text))
				{
					byId.QualificationMargin = System.Convert.ToDecimal(this.txtQualificationMargin.Text);
				}
				byId.QualificationReadOne = this.hfldQualificationReadOne.Value;
				if (!string.IsNullOrEmpty(this.txtPassDate.Text))
				{
					qualificationPassDate = new System.DateTime?(System.Convert.ToDateTime(this.txtPassDate.Text));
				}
				byId.QualificationPassDate = qualificationPassDate;
				byId.QualificationPassReason = this.txtPassReason.Text;
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
						V_Content = "项目：" + byId.PrjName + "已经开始预审通过",
						V_DBLJ = "TenderManage/InfoView.aspx?ic=" + byId.PrjGuid.ToString(),
						V_TPLJ = "new_Mail.gif"
					});
				}
				if (!string.IsNullOrEmpty(this.txtApplyDate.Text.Trim()))
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
					System.DateTime value = System.Convert.ToDateTime(this.txtApplyDate.Text.Trim()).AddDays((double)(-(double)System.Convert.ToInt32(this.SignUpWarnDay)));
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
							V_Content = "名称为：" + byId.PrjName + "的项目已经开始预审通过。",
							C_OpenFlag = "1"
						});
					}
				}
				PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
				PTPrjInfoZTB byId3 = pTPrjInfoZTBService.GetById(new System.Guid(prjGuid));
				byId3.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
				pTPrjInfoZTBService.Update(byId3);
				byId.UpdatePart(byId, ProjectParameter.QualificationPass);
				base.RegisterScript("top.ui.show('预审通过资料保存成功！');top.ui.winSuccess({parentName:'_QulificationPass'});");
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('预审通过资料保存失败！');top.ui.winSuccess({parentName:'_QulificationPass'});");
		}
	}
}


