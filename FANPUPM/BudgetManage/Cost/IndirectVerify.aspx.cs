using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_IndirectVerify : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindDrop();
		this.BindTree();
		this.BindGv();
	}
	public void BindGv()
	{
		if (this.ddlYear.SelectedValue == "zzjg")
		{
			this.gvBudget.DataSource = OrganizationBudget.GetAllNotEReport(this.tvBudget.SelectedValue);
		}
		else
		{
			this.gvBudget.DataSource = IndirectBudget.GetAllNotEReport(this.tvBudget.SelectedValue);
		}
		this.gvBudget.DataBind();
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
		this.ddlYear.Items.Add(new ListItem("组织机构", "zzjg"));
		if (base.Request["year"] != null)
		{
			this.ddlYear.SelectedValue = base.Request["year"];
		}
	}
	protected void BindTree()
	{
		if (this.ddlYear.SelectedValue == "zzjg")
		{
			this.BindZZJGTree();
		}
		else
		{
			ProjectTree projectTree = new ProjectTree();
			projectTree.BindTreeNodes(this.tvBudget, this.Session["PmSet"], base.UserCode, base.Request["id"], this.ddlYear.SelectedItem.Text, this.ddlYear.SelectedValue);
		}
		this.tvBudget.ExpandAll();
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
		this.BindGv();
	}
	protected void SaveData(string state)
	{
		string selectedValue = this.tvBudget.SelectedValue;
		com.jwsoft.pm.entpm.PageHelper.QueryUser(this, base.UserCode);
		System.Collections.Generic.IDictionary<string, decimal> accountAmount = this.GetAccountAmount();
		this.UpdateAccountAndState(accountAmount, selectedValue, state);
	}
	protected void AlertMessage(string message)
	{
		base.RegisterScript(string.Concat(new string[]
		{
			"setWidthHeight();alert('系统提示：\\n",
			message,
			"！');location='IndirectVerify.aspx?year=",
			this.ddlYear.SelectedValue,
			"&id=",
			this.tvBudget.SelectedValue,
			"'"
		}));
	}
	protected void UpdateAccountAndState(System.Collections.Generic.IDictionary<string, decimal> dicBudAmount, string prjId, string state)
	{
		if (dicBudAmount.Count > 0)
		{
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldCheckeds.Value);
			foreach (string current in dicBudAmount.Keys)
			{
				string text = current;
				decimal accountAmount = dicBudAmount[current];
				if (listFromJson.Contains(text))
				{
					if (this.ddlYear.SelectedValue == "zzjg")
					{
						OrganizationBudget byId = OrganizationBudget.GetById(text);
						byId.State = state;
						byId.AccountAmount = accountAmount;
						byId.Update(byId);
					}
					else
					{
						IndirectBudget byId2 = IndirectBudget.GetById(text);
						byId2.AccountAmount = accountAmount;
						byId2.State = state;
						byId2.Update(byId2);
					}
				}
			}
		}
	}
	protected void DelDesktopNotifi()
	{
		if (this.ddlYear.SelectedValue == "zzjg")
		{
			if (!OrganizationBudget.isHaveEReport(this.tvBudget.SelectedValue))
			{
				OrganizationBudget.AddOrDelDesktopNotifications(this.tvBudget.SelectedValue, false);
				return;
			}
		}
		else
		{
			if (!IndirectBudget.isHaveEReport(this.tvBudget.SelectedValue))
			{
				IndirectBudget.AddOrDelDesktopNotifications(this.tvBudget.SelectedValue, false);
			}
		}
	}
	protected System.Collections.Generic.IDictionary<string, decimal> GetAccountAmount()
	{
		System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
		for (int i = 1; i < this.gvBudget.Rows.Count; i++)
		{
			GridViewRow gridViewRow = this.gvBudget.Rows[i];
			TextBox textBox = gridViewRow.FindControl("txtAccountAmount") as TextBox;
			if (textBox != null)
			{
				string key = this.gvBudget.DataKeys[i]["Id"].ToString();
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
			if (e.Row.RowIndex == 0)
			{
				TextBox textBox = e.Row.FindControl("txtAccountAmount") as TextBox;
				if (textBox != null)
				{
					textBox.Attributes["readonly"] = "readonly";
					textBox.Style.Add("background-color", "#ffffc0");
				}
				e.Row.Cells[4].Text = string.Empty;
			}
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["Id"].ToString();
			string state = this.gvBudget.DataKeys[e.Row.RowIndex]["State"].ToString();
			TextBox textBox2 = e.Row.FindControl("txtAccountAmount") as TextBox;
			if (textBox2 != null)
			{
				if (this.IsReadOnly(state))
				{
					textBox2.Enabled = false;
					return;
				}
				textBox2.Text = this.gvBudget.DataKeys[e.Row.RowIndex]["BudgetAmount"].ToString();
			}
		}
	}
	protected bool IsReadOnly(string state)
	{
		bool result = false;
		if (state != null && state == "2")
		{
			result = true;
		}
		return result;
	}
	protected void btnPass_Click(object sender, System.EventArgs e)
	{
		this.SaveData("2");
		this.AlertMessage("审核成功");
	}
	protected void btnCancelVerify_Click(object sender, System.EventArgs e)
	{
		this.SaveData("4");
		this.AlertMessage("取消审核成功");
	}
	protected void btnCancelEReport_Click(object sender, System.EventArgs e)
	{
		this.SaveData("3");
		this.DelDesktopNotifi();
		this.AlertMessage("取消上报成功");
	}
	protected void BindZZJGTree()
	{
		this.tvBudget.Nodes.Clear();
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				this.tvBudget.Nodes.Add(treeNode);
				if (base.Request["id"] != null && treeNode.Value == base.Request["id"])
				{
					treeNode.Select();
				}
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
			if (base.Request["id"] == null)
			{
				this.tvBudget.Nodes[0].Select();
			}
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				if (base.Request["id"] != null && treeNode.Value == base.Request["id"])
				{
					treeNode.Select();
				}
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
}


