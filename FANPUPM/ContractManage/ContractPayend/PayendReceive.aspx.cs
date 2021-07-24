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
public partial class ContractManage_ContractPayend_PayendReceive : NBasePage, IRequiresSessionState
{
	private ContractPayendBll contractPayendBll = new ContractPayendBll();

	protected override void OnInit(EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (base.UserCode == "00000000")
		{
			this.BindGv(Common2.GetTable("Con_ContractPayend", "order by InTime desc"));
			return;
		}
		this.BindGv(Common2.GetTable("Con_ContractPayend", " where BWasPerson like '%" + base.UserCode + "%' order by InTime desc"));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
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
		List<PayendFeedbackModel> listArray = new PayendFeedbackBll().GetListArray(string.Concat(new string[]
		{
			" where contractId='",
			contractId,
			"' and FeedbackPerson='",
			base.UserCode,
			"'"
		}));
		if (listArray.Count > 0)
		{
			state = listArray[0].FeedbackState;
		}
		return WebUtil.GetFeedBackState(state);
	}
}


