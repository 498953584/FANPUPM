using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_IncometBudget : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		string prjID = string.Empty;
		string contractId = string.Empty;
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(base.Request["prjId"]))
			{
				prjID = base.Request["prjId"];
			}
			if (!string.IsNullOrEmpty(base.Request["contractID"]))
			{
				contractId = base.Request["contractID"];
			}
			this.bindContractBudget(prjID, contractId);
		}
	}
	protected void bindContractBudget(string prjID, string contractId)
	{
		BudContractTask.GetTaskInfoByPrjIdAndConIdAndOrderNum(prjID, contractId, "", "", "", "");
		DataTable conByParent = BudContractTask.GetConByParent(prjID, contractId, "", "", "", "", 0);
		this.gvBudget.DataSource = conByParent;
		this.gvBudget.DataBind();
	}
	public void gvContractBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["orderNumber"] = text;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
		}
	}
}


