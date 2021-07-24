using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Project;
using cn.justwin.Tender;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_BidFailInfo : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.hfldUserCode.Value = base.UserCode;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPrjId.Value;
		if (!string.IsNullOrEmpty(value))
		{
			TenderInfo byId = TenderInfo.GetById(value);
			System.DateTime? outBidDate = null;
			if (!string.IsNullOrEmpty(this.txtOutBidDate.Text))
			{
				outBidDate = new System.DateTime?(System.Convert.ToDateTime(this.txtOutBidDate.Text));
			}
			byId.OutBidDate = outBidDate;
			bool? outBidIsReturn = null;
			if (this.RblOutBidIsReturn.SelectedValue == "0")
			{
				outBidIsReturn = new bool?(false);
			}
			else
			{
				if (this.RblOutBidIsReturn.SelectedValue == "1")
				{
					outBidIsReturn = new bool?(true);
				}
			}
			byId.OutBidIsReturn = outBidIsReturn;
			byId.OutBidRemark = this.txtOutBidRemark.Text;
			byId.PrjState = int.Parse(ProjectParameter.OutBid);
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(new System.Guid(value));
			byId2.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
			pTPrjInfoZTBService.Update(byId2);
			byId.UpdatePart(byId, ProjectParameter.OutBid);
			base.RegisterScript("top.ui.alert('落标资料保存成功！');top.ui.closeWin();top.ui.reloadTab();");
		}
	}
}


