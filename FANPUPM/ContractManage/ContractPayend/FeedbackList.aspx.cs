using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_FeedbackList : NBasePage, IRequiresSessionState
{
	private ContractPayendBll contractPayendBll = new ContractPayendBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.BindGv(Common2.GetTable("Con_PayendFeedback", " where contractId='" + base.Request.QueryString["cid"] + "'  order by InTime desc"));
	}
	public void BindGv(DataTable storageDataSource)
	{
		this.rpFeedback.DataSource = storageDataSource;
		this.rpFeedback.DataBind();
	}
	public string GetBPerson(string userCode)
	{
		string text = "";
		userCode = StringUtility.GetArrayToInStr(userCode.Split(new char[]
		{
			','
		}));
		IList<PtYhmc> allModelByWhere = new PtYhmcBll().GetAllModelByWhere(" where v_yhdm in(" + userCode + ")");
		foreach (PtYhmc current in allModelByWhere)
		{
			text = text + current.v_xm + ",";
		}
		return text;
	}
	public string GetFeedbackState(string contractId)
	{
		string state = "";
		List<PayendFeedbackModel> listArray = new PayendFeedbackBll().GetListArray(" where contractId='" + contractId + "'");
		if (listArray.Count > 0)
		{
			state = listArray[0].FeedbackState;
		}
		return WebUtil.GetFeedBackState(state);
	}
}


