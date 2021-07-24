using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometContract_MultiSelectConTask : NBasePage, IRequiresSessionState
{
	private BudContractTaskService ctSer = new BudContractTaskService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsCallback)
		{
			if (!string.IsNullOrEmpty(base.Request["prjId"]))
			{
				this.hfldprjId.Value = base.Request["prjId"];
			}
			if (!string.IsNullOrEmpty(base.Request["contractId"]))
			{
				this.hfldContractId.Value = base.Request["contractId"];
			}
			this.BindContractTask();
		}
	}
	protected string GetTypeByOrderNumber(string orderNumber)
	{
		string result = string.Empty;
		XPMBasicCodeListService xPMBasicCodeListService = new XPMBasicCodeListService();
		XPMBasicCodeList nameByCodeId = xPMBasicCodeListService.GetNameByCodeId(orderNumber.Length / 3, "TaskType");
		if (nameByCodeId != null)
		{
			result = nameByCodeId.CodeName;
		}
		return result;
	}
	private void BindContractTask()
	{
		string value = this.hfldprjId.Value;
		string value2 = this.hfldContractId.Value;
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
		{
			dataSource = cn.justwin.Domain.BudContractTask.GetTaskInfoByPrjIdAndConId(value, value2, "", "", "");
		}
		this.gvwConTask.DataSource = dataSource;
		this.gvwConTask.DataBind();
	}
	protected void gvwConTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvwConTask.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			int subCount = this.ctSer.GetSubCount(text);
			string text2 = this.gvwConTask.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text2.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["orderNumber"] = text2;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = subCount.ToString();
		}
	}
	protected void btnBindTask_Click(object sender, System.EventArgs e)
	{
		this.BindContractTask();
	}
}


