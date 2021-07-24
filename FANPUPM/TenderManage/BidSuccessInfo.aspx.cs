using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Project;
using cn.justwin.Tender;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_BidSuccessInfo : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldUserCode.Value = base.UserCode;
			ProjectInfo.BindPrj(this.dropParentProject);
			this.initPageInfo();
		}
	}
	protected void initPageInfo()
	{
		if (!string.IsNullOrEmpty(base.Request["PrjId"]))
		{
			string g = base.Request["PrjId"];
			PTPrjInfoZTB byId = new PTPrjInfoZTBService().GetById(new System.Guid(g));
			if (byId.ParentTypeCode != null)
			{
				PTPrjInfo entityByTypeCode = new PTPrjInfoService().GetEntityByTypeCode(byId.ParentTypeCode);
				if (entityByTypeCode != null)
				{
					this.dropParentProject.Items.FindByText(entityByTypeCode.PrjName).Selected = true;
				}
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPrjId.Value;
		if (!string.IsNullOrEmpty(value))
		{
			System.Guid id = new System.Guid(value);
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTBDetailService pTPrjInfoZTBDetailService = new PTPrjInfoZTBDetailService();
			PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(id);
			PTPrjInfoZTBDetail byId2 = pTPrjInfoZTBDetailService.GetById(value);
			TenderInfo.GetById(value);
			System.DateTime? successBidDate = null;
			if (!string.IsNullOrEmpty(this.txtSuccessBidDate.Text))
			{
				successBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtSuccessBidDate.Text.Trim()));
			}
			byId2.SuccessBidDate = successBidDate;
			decimal? num = null;
			if (!string.IsNullOrEmpty(this.txtSuccessBidPrice.Text))
			{
				num = new decimal?(System.Convert.ToDecimal(this.txtSuccessBidPrice.Text.Trim()));
			}
			byId2.SuccessBidPrice = num;
			byId2.SuccessBidRemark = this.txtSuccessBidRemark.Text.Trim();
			byId.PrjCost = new double?(System.Convert.ToDouble(num));
			byId.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
			byId.PrjState = new int?(int.Parse(ProjectParameter.WinBid));
			byId.ParentTypeCode = pTPrjInfoZTBService.GetParentTypeCode(this.dropParentProject.SelectedValue);
			pTPrjInfoZTBService.Update(byId);
			pTPrjInfoZTBDetailService.Update(byId2);
			base.RegisterScript("top.ui.alert('中标资料保存成功！');top.ui.closeWin();top.ui.reloadTab();");
		}
	}
}


