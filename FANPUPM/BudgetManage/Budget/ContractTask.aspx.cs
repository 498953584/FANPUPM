using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_ContractTask : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.SowBtnGenerage();
			this.hfldInputUser.Value = HttpUtility.UrlEncode(PageHelper.QueryUser(this, base.UserCode));
		}
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text3 = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text3.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["orderNumber"] = text3;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = text2;
			bool flag = text2 == "0";
			if (flag)
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "详细信息";
				image.Attributes.Add("rowId", text);
				image.Attributes["onclick"] = "showInfo('" + text + "')";
				image.Style.Add("vertical-align", "middle");
				image.Style.Add("margin-right", "0");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[2].Controls.Add(image);
			}
		}
	}
	protected void btnDelTask_Click(object sender, System.EventArgs e)
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		CheckBox checkBox = this.gvBudget.HeaderRow.Cells[0].FindControl("cbAllBox") as CheckBox;
		BudContractConsTaskService budContractConsTaskService = new BudContractConsTaskService();
		if (checkBox != null)
		{
			if (checkBox.Checked)
			{
				cn.justwin.Domain.BudContractTask.ClearByPrjId(this.prjId);
				System.Collections.Generic.List<string> taskIds = (
					from t in cn.justwin.Domain.BudContractTask.GetAllByPrjId(this.prjId)
					select t.Id).ToList<string>();
				budContractConsTaskService.DeleteByTaskId(taskIds);
			}
			else
			{
				bool flag = false;
				System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
				if (listFromJson.Count == 1)
				{
					bool flag2 = cn.justwin.Domain.BudContractTask.IsLeaf(listFromJson[0]);
					flag = flag2;
				}
				else
				{
					System.Collections.Generic.List<string> orderNumberById = cn.justwin.Domain.BudContractTask.GetOrderNumberById(listFromJson);
					bool flag3 = cn.justwin.Domain.BudTask.IsStructured(orderNumberById);
					if (flag3)
					{
						bool flag4 = false;
						foreach (string current in listFromJson)
						{
							bool flag5 = cn.justwin.Domain.BudContractTask.CheckChilds(current);
							if (flag5)
							{
								flag4 = flag5;
								break;
							}
						}
						if (!flag4)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					base.RegisterScript("alert('系统提示：\\n请先删除子项！');");
					return;
				}
				cn.justwin.Domain.BudContractTask.Del(listFromJson);
				budContractConsTaskService.DeleteByTaskId(listFromJson);
			}
		}
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		budContractTaskService.SuperDel(this.prjId);
		stringBuilder.Append(string.Concat(new string[]
		{
			"location='ContractTask.aspx?prjId=",
			this.prjId,
			"&year=",
			this.year,
			"';"
		}));
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void btnGenerage_Click(object sender, System.EventArgs e)
	{
		this.GenerageBudgetByParent(this.hfldPrjId.Value);
	}
	private void InitPage()
	{
		this.BindGv();
	}
	private void BindGv()
	{
		DataTable allTask = cn.justwin.Domain.BudContractTask.GetAllTask(this.prjId, string.Empty, string.Empty, string.Empty);
		this.gvBudget.DataSource = allTask;
		this.gvBudget.DataBind();
		this.hfldPrjId.Value = this.prjId;
		cn.justwin.Domain.BudContractTaskAudit modelByPrjId = cn.justwin.Domain.BudContractTaskAudit.GetModelByPrjId(this.prjId);
		if (modelByPrjId != null)
		{
			this.hfldContractTaskAuditId.Value = modelByPrjId.Id;
			this.hfldFlowState.Value = modelByPrjId.FlowState.ToString();
			if (modelByPrjId.FlowState == -1 || modelByPrjId.FlowState == -3)
			{
				this.hfldIsLocked.Value = "False";
			}
			else
			{
				this.hfldIsLocked.Value = "True";
			}
		}
		else
		{
			this.hfldIsLocked.Value = "False";
			this.hfldFlowState.Value = "-1";
		}
		this.lblLockPrjMessage.Text = "(" + Common2.GetStateNoColor(this.hfldFlowState.Value.ToString()) + ")";
		this.hfldCheckedIds.Value = string.Empty;
	}
	private void SowBtnGenerage()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(this.hfldPrjId.Value);
		if (byId.TypeCode.Length == 10 && (this.hfldFlowState.Value == "-1" || this.hfldFlowState.Value == "-3") && ConfigHelper.Get("ProjectStandalone") == "0")
		{
			this.btnGenerage.Visible = true;
			return;
		}
		this.btnGenerage.Visible = false;
	}
	private void GenerageBudgetByParent(string prjId)
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
		PTPrjInfo parent = pTPrjInfoService.GetParent(prjId);
		string str = byId.TypeCode.Substring(6, 4);
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		budContractTaskService.DeleteByProject(prjId);
		System.Collections.Generic.IList<cn.justwin.Domain.Entities.BudContractTask> byProject = budContractTaskService.GetByProject(parent.PrjGuid.Value.ToString(), 999);
		for (int i = 0; i < byProject.Count; i++)
		{
			cn.justwin.Domain.Entities.BudContractTask budContractTask = byProject[i];
			budContractTask.PrjId = byId.PrjGuid.Value.ToString();
			budContractTask.TaskId = budContractTask.TaskId.Substring(0, 9) + str + budContractTask.TaskId.Substring(13);
			if (!string.IsNullOrEmpty(budContractTask.ParentId))
			{
				budContractTask.ParentId = budContractTask.ParentId.Substring(0, 9) + str + budContractTask.ParentId.Substring(13);
			}
			budContractTaskService.Add(budContractTask);
		}
		this.BindGv();
	}
}


