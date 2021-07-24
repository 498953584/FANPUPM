using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
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
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudgetPlaitList : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private BudTaskService budTaskSer = new BudTaskService();

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
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.InitPage();
			this.SowBtnGenerage();
			this.hfldInputUser.Value = HttpUtility.UrlEncode(PageHelper.QueryUser(this, base.UserCode));
			if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
			{
				this.hfldProjectCount.Value = "1";
			}
		}
	}
	protected void btnGenerage_Click(object sender, System.EventArgs e)
	{
		this.GenerageBudgetByParent(this.hfldPrjId.Value);
	}
	public void InitPage()
	{
		if (this.hfldIsWBSRelevance.Value == "1")
		{
			this.BindDropPriceType();
		}
		else
		{
			this.ddlPriceType.Visible = false;
		}
		this.BindGv();
	}
	public void BindGv()
	{
		this.hfldPriceType.Value = this.ddlPriceType.SelectedValue.Trim();
		DataTable table = this.budTaskSer.GetTable(this.prjId);
		this.gvBudget.DataSource = table;
		this.gvBudget.DataBind();
		this.hfldCheckedIds.Value = string.Empty;
		this.hfldPrjId.Value = this.prjId;
		this.LockButton();
	}
	protected void BindDropPriceType()
	{
		System.Collections.Generic.IList<ResPriceType> all = ResPriceType.GetAll(base.UserCode);
		this.ddlPriceType.DataSource = all;
		this.ddlPriceType.DataTextField = "Name";
		this.ddlPriceType.DataValueField = "Id";
		this.ddlPriceType.DataBind();
		ListItem item = new ListItem("目标成本", "");
		this.ddlPriceType.Items.Insert(0, item);
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		if (this.gvBudget.Rows.Count == 0)
		{
			return;
		}
		BudTaskService budTaskService = new BudTaskService();
		CheckBox checkBox = this.gvBudget.HeaderRow.Cells[0].FindControl("cbAllBox") as CheckBox;
		if (checkBox != null && checkBox.Checked)
		{
			budTaskService.SuperDel(this.prjId);
			base.RegisterScript(string.Concat(new string[]
			{
				"location='BudgetPlaitList.aspx?prjId=",
				this.prjId,
				"&year=",
				this.year,
				"';"
			}));
			return;
		}
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string value = this.hfldCheckedIds.Value;
		if (value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		if (!cn.justwin.Domain.BudTask.CheckDel(list, this.prjId))
		{
			base.RegisterScript("top.ui.alert('请先删除子项')");
		}
		else
		{
			PayoutTarget payoutTarget = new PayoutTarget();
			payoutTarget.DelByTaskId(list);
			foreach (string current in list)
			{
				try
				{
					cn.justwin.Domain.BudTask.DeleteReourceByTaskId(current);
					cn.justwin.Domain.BudTask.Delete(current);
				}
				catch
				{
				}
			}
		}
		base.RegisterScript(string.Concat(new string[]
		{
			"location='BudgetPlaitList.aspx?prjId=",
			this.prjId,
			"&year=",
			this.year,
			"';"
		}));
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
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
	protected void ddlPriceType_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.ddlPriceType.SelectedValue))
		{
			cn.justwin.Domain.BudTask.UpdateResourcePrice(this.prjId, 1, this.ddlPriceType.SelectedValue);
		}
		this.BindGv();
	}
	private void LockButton()
	{
		bool flag = cn.justwin.Domain.BudTaskChange.IsAllowBudget(this.prjId);
		this.hfldIsLocked.Value = (!flag).ToString();
		cn.justwin.Domain.BudTaskChange taskChange = cn.justwin.Domain.BudTaskChange.GetTaskChange(this.prjId);
		if (taskChange != null)
		{
			this.hfldTaskChangeId.Value = taskChange.Id;
			this.hfldFlowstate.Value = ((!taskChange.Flowstate.HasValue) ? "" : taskChange.Flowstate.ToString());
			if (DBHelper.GetInt(taskChange.Version) == 1 && this.hfldFlowstate.Value == "0")
			{
				this.hfldIsAllowWithdraw.Value = "True";
			}
			this.lblLockPrjMessage.Text = "(" + Common2.GetStateNoColor(this.hfldFlowstate.Value.ToString()) + ")";
			if (DBHelper.GetInt(taskChange.Version) != 1)
			{
				this.lblLockPrjMessage.Text = "(已审核)";
				return;
			}
		}
		else
		{
			this.hfldTaskChangeId.Value = "";
			this.hfldFlowstate.Value = "-1";
			this.hfldIsLocked.Value = "False";
			this.lblLockPrjMessage.Text = "(未提交)";
		}
	}
	protected void btnSaveTemplate_Click(object sender, System.EventArgs e)
	{
		string text = string.Empty;
		CheckBox checkBox = this.gvBudget.HeaderRow.Cells[0].FindControl("cbAllBox") as CheckBox;
		if (checkBox != null)
		{
			if (checkBox.Checked)
			{
				BudTaskService budTaskService = new BudTaskService();
				System.Collections.Generic.IList<cn.justwin.Domain.Entities.BudTask> byProject = budTaskService.GetByProject(this.prjId, 999);
				foreach (cn.justwin.Domain.Entities.BudTask current in byProject)
				{
					text = text + "\"" + current.TaskId + "\",";
				}
				if (byProject.Count == 1)
				{
					text = text.Substring(1, text.Length - 2);
				}
				else
				{
					text = "[" + text.Substring(0, text.Length - 1) + "]";
				}
			}
			else
			{
				text = this.hfldCheckedIds.Value;
			}
		}
		this.Session["taskIds"] = text;
		base.RegisterScript("saveTem()");
	}
	private void SowBtnGenerage()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(this.hfldPrjId.Value);
		if (byId.TypeCode.Length == 10 && (this.hfldFlowstate.Value == "-1" || this.hfldFlowstate.Value == "-3") && ConfigHelper.Get("ProjectStandalone") == "0")
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
		BudTaskService budTaskService = new BudTaskService();
		budTaskService.DeleteByProject(prjId);
		System.Collections.Generic.IList<cn.justwin.Domain.Entities.BudTask> byProject = budTaskService.GetByProject(parent.PrjGuid.Value.ToString(), 999);
		for (int i = 0; i < byProject.Count; i++)
		{
			cn.justwin.Domain.Entities.BudTask budTask = byProject[i];
			budTask.PrjId = byId.PrjGuid.Value.ToString();
			budTask.TaskId = budTask.TaskId.Substring(0, 9) + str + budTask.TaskId.Substring(13);
			if (!string.IsNullOrEmpty(budTask.ParentId))
			{
				budTask.ParentId = budTask.ParentId.Substring(0, 9) + str + budTask.ParentId.Substring(13);
			}
			budTaskService.Add(budTask);
		}
		this.BindGv();
	}
}


