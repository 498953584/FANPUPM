using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_StateChangeQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.BindData();
		}
	}
	private void BindData()
	{
		string text = base.Request["ic"].ToString();
		PTPrjInfoStateChange byPrjIdByOrder;
		if (string.IsNullOrEmpty(base.Request["pass"]))
		{
			byPrjIdByOrder = new PTPrjInfoStateChangeService().GetByPrjIdByOrder(text, 1);
		}
		else
		{
			byPrjIdByOrder = new PTPrjInfoStateChangeService().GetByPrjIdByOrder(text, -1);
		}
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		System.Guid id = new System.Guid(text);
		PTPrjInfoZTB byId = new PTPrjInfoZTBService().GetById(id);
		if (byPrjIdByOrder != null)
		{
			if (!string.IsNullOrEmpty(byPrjIdByOrder.ChangeUser))
			{
				PTyhmc byId2 = new PTYhmcService().GetById(byPrjIdByOrder.ChangeUser);
				this.lblChangeMan.Text = byId2.v_xm;
			}
			this.lblChangeTime.Text = Common2.GetTime(byPrjIdByOrder.ChangeTime.ToString());
			this.lblOldState.Text = basicCodeListService.GetNameByTypeAndCode("ProjectState", byPrjIdByOrder.OldState.Value);
			this.lblChangeState.Text = basicCodeListService.GetNameByTypeAndCode("ProjectState", byPrjIdByOrder.ChangeState.Value);
			this.lblprjChangeReason.Text = byPrjIdByOrder.ChangeReason;
			this.lblPrjRemark.Text = byPrjIdByOrder.Note;
		}
		if (byId != null)
		{
			this.lblPrjCode.Text = byId.PrjCode;
			this.lblPrjName.Text = byId.PrjName;
			this.lblStartDate.Text = Common2.GetTime(byId.StartDate).ToString();
			this.lblEndDate.Text = Common2.GetTime(byId.EndDate).ToString();
		}
	}
}


