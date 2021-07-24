using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_DivSelectContrtAccon : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();
	private string _mpidStr = string.Empty;
	private string _prjId = string.Empty;
	private string bName = "";
	private DataTable dtName;

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwContract.PageSize = NBasePage.pagesize;
		this.GetCorpName();
		if (base.Request.QueryString["prjcode"] != null && base.Request.QueryString["prjcode"].ToString() != "")
		{
			this.hdnProjectCode.Value = base.Request.QueryString["prjcode"].ToString();
			this._prjId = base.Request.QueryString["prjcode"].ToString();
		}
		if (base.Request.QueryString["mpid"] != null && base.Request.QueryString["mpid"].ToString() != "")
		{
			this._mpidStr = base.Request.QueryString["mpid"].ToString();
		}
		if (!base.IsPostBack)
		{
			DataTable contracts = this.SearchContracts();
			this.DataBindContracts(contracts);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		DataTable contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
	private DataTable SearchContracts()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" cpc.PrjGuid='").Append(this.hdnProjectCode.Value.ToString()).Append("' ");
		if (!string.IsNullOrEmpty(this.txtContractCode.Text.Trim()))
		{
			stringBuilder.Append(" AND cpc.ContractCode LIKE '%").Append(this.txtContractCode.Text.Trim()).Append("%' ");
		}
		if (!string.IsNullOrEmpty(this.txtContractName.Text.Trim()))
		{
			stringBuilder.Append(" AND  cpc.ContractName LIKE '%").Append(this.txtContractName.Text.Trim()).Append("%' ");
		}
		stringBuilder.Append(" and  cpc.ContractID  not in  (SELECT fpmd.ContractID  FROM [Fund_Plan_MonthDetail] fpmd where fpmd.MonthPlanID ='" + this._mpidStr + "')");
		return this.payoutContract.GetList_New(stringBuilder.ToString(), base.UserCode);
	}
	private void DataBindContracts(DataTable contracts)
	{
		this.gvwContract.DataSource = contracts;
		this.gvwContract.DataBind();
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			if (dataRowView["BName"] != null)
			{
				DataRow[] array = this.dtName.Select("corpid=" + dataRowView["BName"].ToString());
				if (array.Length > 0)
				{
					this.bName = array[0]["corpName"].ToString();
				}
			}
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dbClickRow('",
				dataRowView["ContractID"].ToString(),
				"','",
				dataRowView["ContractName"].ToString(),
				"','",
				this.bName,
				"')"
			});
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRow('",
				dataRowView["ContractID"].ToString(),
				"','",
				dataRowView["ContractName"].ToString(),
				"','",
				this.bName,
				"')"
			});
		}
	}
	private void GetCorpName()
	{
		string text = "select corpid,corpName FROM XPM_Basic_ContactCorp";
		this.dtName = SqlHelper.ExecuteQuery(CommandType.Text, text.ToString(), null);
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		DataTable contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
}


