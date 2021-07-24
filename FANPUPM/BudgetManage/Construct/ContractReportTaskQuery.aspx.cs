using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ContractReportTaskQuery : NBasePage, IRequiresSessionState
{
	private string rptId = string.Empty;
	private BudContractConsReportService rptSer = new BudContractConsReportService();
	private BudContractConsTaskService ctSer = new BudContractConsTaskService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.rptId = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initial();
		}
	}
	protected void gvwConsTask_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwConsTask.DataKeys[e.Row.RowIndex]["ConsTaskId"].ToString();
		}
	}
	protected string CalcCompletePercent(object amount, object total)
	{
		if (amount == null || total == null)
		{
			return string.Empty;
		}
		decimal num = 0m;
		try
		{
			num = System.Convert.ToDecimal(amount) / System.Convert.ToDecimal(total);
		}
		catch
		{
		}
		return string.Format("{0:P}", num);
	}
	private void Initial()
	{
		BudContractConsReport byId = this.rptSer.GetById(this.rptId);
		this.lblDate.Text = byId.InputDate.ToString("yyyy-MM-dd");
		this.txtNode.Value = byId.Note;
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.PrjId);
		if (byId2 != null)
		{
			this.lblPrjName.Text = byId2.PrjName;
		}
		System.Collections.Generic.List<BudContractConsTask> list = (
			from ct in this.ctSer
			where ct.RptId == this.rptId
			select ct).ToList<BudContractConsTask>();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		foreach (BudContractConsTask current in list)
		{
			if (current.ContractTask == null)
			{
				current.ContractTask = budContractTaskService.GetTaskById(current.TaskId);
			}
		}
		this.gvwConsTask.DataSource = list;
		this.gvwConsTask.DataBind();
	}
}


