using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IndirectBudget : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private static string inputUser = string.Empty;
	private string tvFristValue = string.Empty;
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
			this.BindGv();
			BudgetManage_Cost_IndirectBudget.inputUser = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
		}
	}
	public void BindGv()
	{
		this.setWF();
		if (string.IsNullOrEmpty(this.prjId))
		{
			this.btnSave.Enabled = false;
		}
		this.hfldPrjId.Value = this.prjId;
		this.hfldInputUser.Value = base.UserCode;
		if (this.year == "zzjg")
		{
			this.gvBudget.DataSource = OrganizationBudget.GetAll(this.prjId);
		}
		else
		{
			this.gvBudget.DataSource = IndirectBudget.GetAll(this.prjId);
		}
		this.gvBudget.DataBind();
		base.RegisterScript("keyPress()");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.SaveData();
		this.ViewState["PrjId"] = this.prjId;
		this.AlertMessage("保存成功");
	}
	protected void SaveData()
	{
		System.Collections.Generic.IDictionary<string, decimal> budgetAmount = this.GetBudgetAmount();
		if (this.year == "zzjg")
		{
			bool flag = OrganizationBudget.IsExistOrgan(this.prjId);
			if (flag)
			{
				this.UpdateOrganBudget(budgetAmount, this.prjId);
				return;
			}
			this.AddOrganBudget(budgetAmount, this.prjId);
			return;
		}
		else
		{
			bool flag = IndirectBudget.IsExistPrjId(this.prjId);
			if (flag)
			{
				this.UpdateBudget(budgetAmount, this.prjId);
				return;
			}
			this.AddBudget(budgetAmount, this.prjId);
			return;
		}
	}
	protected void AlertMessage(string message)
	{
		base.RegisterScript(string.Concat(new string[]
		{
			"setWidthHeight(); top.ui.show('",
			message,
			"！'); location='IndirectBudget.aspx?year=",
			this.year,
			"&prjId=",
			this.prjId,
			"'"
		}));
	}
	protected void UpdateBudget(System.Collections.Generic.IDictionary<string, decimal> dicBudAmount, string prjId)
	{
		if (dicBudAmount.Count > 0)
		{
			foreach (string current in dicBudAmount.Keys)
			{
				string text = current;
				decimal num = dicBudAmount[current];
				IndirectBudget byPrjIdAndCBSCode = IndirectBudget.GetByPrjIdAndCBSCode(prjId, text);
				if (byPrjIdAndCBSCode != null)
				{
					byPrjIdAndCBSCode.AccountAmount = num;
					byPrjIdAndCBSCode.Update(byPrjIdAndCBSCode);
				}
				else
				{
					IndirectBudget indirectBudget = IndirectBudget.Create(System.Guid.NewGuid().ToString(), prjId, text, num, num, BudgetManage_Cost_IndirectBudget.inputUser, System.DateTime.Now, string.Empty);
					indirectBudget.Add(indirectBudget);
				}
			}
		}
	}
	protected void UpdateOrganBudget(System.Collections.Generic.IDictionary<string, decimal> dicBudAmount, string organizationBudgetId)
	{
		if (dicBudAmount.Count > 0)
		{
			foreach (string current in dicBudAmount.Keys)
			{
				string text = current;
				decimal num = dicBudAmount[current];
				OrganizationBudget byOrganAndCBSCode = OrganizationBudget.GetByOrganAndCBSCode(organizationBudgetId, text);
				if (byOrganAndCBSCode != null)
				{
					byOrganAndCBSCode.AccountAmount = num;
					byOrganAndCBSCode.Update(byOrganAndCBSCode);
				}
				else
				{
					OrganizationBudget organizationBudget = OrganizationBudget.Create(System.Guid.NewGuid().ToString(), organizationBudgetId, text, num, num, BudgetManage_Cost_IndirectBudget.inputUser, System.DateTime.Now, string.Empty);
					organizationBudget.Add(organizationBudget);
				}
			}
		}
	}
	protected void AddBudget(System.Collections.Generic.IDictionary<string, decimal> dicBudAmount, string prjId)
	{
		if (dicBudAmount.Count > 0)
		{
			foreach (string current in dicBudAmount.Keys)
			{
				string id = System.Guid.NewGuid().ToString();
				string empty = string.Empty;
				string code = current;
				decimal num = dicBudAmount[current];
				IndirectBudget indirectBudget = IndirectBudget.Create(id, prjId, code, num, num, BudgetManage_Cost_IndirectBudget.inputUser, System.DateTime.Now, empty);
				indirectBudget.Add(indirectBudget);
			}
		}
	}
	protected void AddOrganBudget(System.Collections.Generic.IDictionary<string, decimal> dicBudAmount, string organizationBudgetId)
	{
		if (dicBudAmount.Count > 0)
		{
			foreach (string current in dicBudAmount.Keys)
			{
				System.Guid.NewGuid().ToString();
				string empty = string.Empty;
				string code = current;
				decimal num = dicBudAmount[current];
				OrganizationBudget organizationBudget = OrganizationBudget.Create(System.Guid.NewGuid().ToString(), organizationBudgetId, code, num, num, BudgetManage_Cost_IndirectBudget.inputUser, System.DateTime.Now, empty);
				organizationBudget.Add(organizationBudget);
			}
		}
	}
	protected System.Collections.Generic.IDictionary<string, decimal> GetBudgetAmount()
	{
		System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
		int index = 1;
		foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
		{
			TextBox textBox = gridViewRow.FindControl("txtBudgetAmount") as TextBox;
			if (textBox != null)
			{
				string key = gridViewRow.Cells[index].Text.Trim();
				decimal value = 0m;
				try
				{
					value = decimal.Parse(textBox.Text);
				}
				catch
				{
				}
				dictionary.Add(key, value);
			}
		}
		return dictionary;
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			TextBox textBox = e.Row.FindControl("txtBudgetAmount") as TextBox;
			if (textBox != null && e.Row.RowIndex == 0)
			{
				textBox.Attributes["class"] = "decimal_input readonly total_budget";
				textBox.Attributes["readonly"] = "readonly";
				textBox.Style.Add("background-color", "#F5F5F5");
			}
			else
			{
				if (textBox != null && e.Row.RowIndex != 0)
				{
					textBox.Attributes["class"] = "decimal_input budget";
				}
			}
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["CBSCode"].ToString();
		}
	}
	protected bool IsReadOnly(string state)
	{
		bool result = true;
		if (state != null)
		{
			if (!(state == "0"))
			{
				if (state == "3")
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}
		}
		return result;
	}
	protected void setWF()
	{
		int flowState = IndirectBudget.GetFlowState(this.prjId);
		this.hfldFlowState.Value = flowState.ToString();
		string text = System.Guid.NewGuid().ToString();
		if (flowState != -1)
		{
			text = IndirectBudget.GetGuid(this.prjId);
		}
		this.hfldGuid.Value = text;
		this.lblWFState.Text = Common2.GetState(flowState.ToString());
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		if (this.year == "zzjg")
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"setWFButtonState('",
				text,
				"', '', '",
				flowState,
				"', true);"
			}));
		}
		else
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"setWFButtonState('",
				text,
				"', '",
				this.prjId,
				"', '",
				flowState,
				"', true);"
			}));
		}
		if (flowState != -1 && flowState != -3 && base.UserCode != "00000000")
		{
			stringBuilder.Append("$('input[type=\"text\"]').each(function (i) { $(this).attr('disabled', 'disabled');});$('#btnSave').attr('disabled','disabled');");
		}
		if (this.year == "zzjg")
		{
			this.WF1.BusiCode = "124";
		}
		base.RegisterScript(stringBuilder.ToString());
	}
}


