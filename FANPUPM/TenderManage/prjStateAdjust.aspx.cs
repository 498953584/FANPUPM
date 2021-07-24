using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_prjStateAdjust : NBasePage, IRequiresSessionState
{
	private PTPrjInfoStateChangeService prjInfoStatChgSev = new PTPrjInfoStateChangeService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.bindDrop();
			this.BindData();
		}
	}
	protected void bindDrop()
	{
		TypeList.BindDrop(this.drpDLstState, false, "", null, new System.Collections.Generic.List<int>
		{
			1,
			2,
			3,
			4,
			5,
			6,
			15,
			16,
			19
		});
	}
	private void BindData()
	{
		string text = base.Request["prjId"].ToString();
		TenderInfo byId = TenderInfo.GetById(text);
		this.prjName.Text = byId.PrjName;
		this.prjCode.Text = byId.PrjCode;
		this.txtPrjState.Text = this.GetStateName(byId.PrjState);
		this.adjTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
		this.drpDLstState.SelectedValue = byId.PrjState.ToString();
		PTPrjInfoStateChange byPrjIdByOrder = this.prjInfoStatChgSev.GetByPrjIdByOrder(text, -1);
		if (byPrjIdByOrder != null)
		{
			this.drpDLstState.SelectedValue = byPrjIdByOrder.ChangeState.ToString();
			if (!string.IsNullOrEmpty(byPrjIdByOrder.ChangeTime.ToString()))
			{
				this.adjTime.Text = byPrjIdByOrder.ChangeTime.Value.ToString("yyyy-MM-dd");
			}
			this.adjReason.Text = byPrjIdByOrder.ChangeReason;
			PTyhmc byId2 = new PTYhmcService().GetById(byPrjIdByOrder.ChangeUser);
			if (byId2 != null)
			{
				this.hfldUserCode.Value = byId2.v_yhdm;
				this.txtAdjustMan.Text = byId2.v_xm;
			}
			this.remark.Text = byPrjIdByOrder.Note;
			this.ViewState["PrjChgStID"] = byPrjIdByOrder.Id;
		}
		this.ViewState["prjState"] = byId.PrjState.ToString();
		this.ViewState["prjGuId"] = byId.PrjGuid.ToString();
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		string b = this.ViewState["prjState"].ToString();
		this.ViewState["prjGuId"].ToString();
		if (this.drpDLstState.SelectedValue == b)
		{
			base.RegisterScript("top.ui.alert('项目状态没有改变!')");
			return;
		}
		if (string.IsNullOrEmpty(this.drpDLstState.SelectedValue))
		{
			base.RegisterScript("top.ui.alert('调整项目状态不能为空!')");
			return;
		}
		PTPrjInfoStateChange pTPrjInfoStateChange = new PTPrjInfoStateChange();
		if (this.ViewState["PrjChgStID"] != null)
		{
			string id = this.ViewState["PrjChgStID"].ToString();
			pTPrjInfoStateChange = this.prjInfoStatChgSev.GetById(id);
			pTPrjInfoStateChange.OldState = new int?(int.Parse(this.ViewState["prjState"].ToString()));
			pTPrjInfoStateChange.ChangeState = new int?(int.Parse(this.drpDLstState.SelectedValue));
			pTPrjInfoStateChange.ChangeTime = new System.DateTime?(System.DateTime.Parse(this.adjTime.Text));
			pTPrjInfoStateChange.ChangeReason = this.adjReason.Text.Trim();
			pTPrjInfoStateChange.ChangeUser = this.hfldUserCode.Value.Trim();
			pTPrjInfoStateChange.Note = this.remark.Text;
			pTPrjInfoStateChange.FlowState = new int?(-1);
			pTPrjInfoStateChange.InputDate = new System.DateTime?(System.DateTime.Now);
			pTPrjInfoStateChange.InputUser = base.UserCode;
			this.prjInfoStatChgSev.Update(pTPrjInfoStateChange);
		}
		else
		{
			pTPrjInfoStateChange.Id = System.Guid.NewGuid().ToString();
			pTPrjInfoStateChange.PrjId = this.ViewState["prjGuId"].ToString();
			pTPrjInfoStateChange.OldState = new int?(int.Parse(this.ViewState["prjState"].ToString()));
			pTPrjInfoStateChange.ChangeState = new int?(int.Parse(this.drpDLstState.SelectedValue));
			pTPrjInfoStateChange.ChangeTime = new System.DateTime?(System.DateTime.Parse(this.adjTime.Text));
			pTPrjInfoStateChange.ChangeReason = this.adjReason.Text.Trim();
			pTPrjInfoStateChange.ChangeUser = this.hfldUserCode.Value.Trim();
			pTPrjInfoStateChange.Note = this.remark.Text;
			pTPrjInfoStateChange.FlowState = new int?(-1);
			pTPrjInfoStateChange.InputDate = new System.DateTime?(System.DateTime.Now);
			pTPrjInfoStateChange.InputUser = base.UserCode;
			this.prjInfoStatChgSev.Add(pTPrjInfoStateChange);
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId = pTPrjInfoZTBService.GetById(new System.Guid(this.ViewState["prjGuId"].ToString()));
			byId.ChangeFlowSate = -1;
			pTPrjInfoZTBService.Update(byId);
		}
		base.RegisterScript("top.ui.show('保存成功！');top.ui.winSuccess({parentName:'_prjStateAdjust'});");
	}
	private string GetStateName(int state)
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		return basicCodeListService.GetNameByTypeAndCode("ProjectState", state);
	}
}


