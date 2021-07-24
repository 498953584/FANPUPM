using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Template_BudTemplateList : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private System.Text.StringBuilder strJS = new System.Text.StringBuilder("setWidthHeight();");
	private string tempId = string.Empty;
	private string tempType = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["tempId"]))
		{
			this.tempId = base.Request["tempId"];
		}
		if (!string.IsNullOrEmpty(base.Request["tempType"]))
		{
			this.tempType = base.Request["tempType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.BindTree();
			this.BindGv();
		}
	}
	public void BindGv()
	{
		DataTable templateItem = BudTemplate.GetTemplateItem(this.tvBudget.SelectedValue);
		this.gvBudget.DataSource = templateItem;
		this.gvBudget.DataBind();
		this.hfldCheckedIds.Value = string.Empty;
		this.hfldTempId.Value = this.tvBudget.SelectedValue;
		this.hfldTemTypeId.Value = this.ddlType.SelectedValue;
	}
	public void BindDrop()
	{
		this.ddlType.DataSource = BudTemplateType.GetAll();
		this.ddlType.DataTextField = "Name";
		this.ddlType.DataValueField = "Id";
		this.ddlType.DataBind();
		if (base.Request["tempType"] != null)
		{
			this.ddlType.SelectedValue = base.Request["tempType"];
		}
	}
	protected void BindTree()
	{
		this.tvBudget.Nodes.Clear();
		TreeNode treeNode = new TreeNode("模板", "0");
		treeNode.SelectAction = TreeNodeSelectAction.None;
		this.tvBudget.Nodes.AddAt(0, treeNode);
		System.Collections.Generic.IList<BudTemplate> list = BudTemplate.Select(this.ddlType.SelectedValue);
		int num = 0;
		if (list.Count<BudTemplate>() == 0)
		{
			this.btnAdd.Disabled = true;
		}
		else
		{
			this.btnAdd.Disabled = false;
		}
		foreach (BudTemplate current in list)
		{
			num++;
			TreeNode treeNode2 = new TreeNode(current.Name, current.Id);
			if (num == 1)
			{
				treeNode2.Select();
			}
			if (base.Request["tempId"] != null && base.Request["tempId"] == treeNode2.Value)
			{
				treeNode2.Select();
			}
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.tvBudget.ExpandAll();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		CheckBox checkBox = this.gvBudget.HeaderRow.Cells[0].FindControl("cbAllBox") as CheckBox;
		if (checkBox != null && checkBox.Checked)
		{
			BudTemplateItem.Delete(this.tvBudget.SelectedValue);
			this.strJS.Append(string.Concat(new string[]
			{
				"location='BudTemplateList.aspx?tempId=",
				this.tvBudget.SelectedValue,
				"&tempType=",
				this.ddlType.SelectedValue,
				"';"
			}));
			base.RegisterScript(this.strJS.ToString());
			return;
		}
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldCheckedIds.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldCheckedIds.Value);
		}
		else
		{
			list.Add(this.hfldCheckedIds.Value);
		}
		bool flag = false;
		System.Collections.Generic.List<string> orderNumberById = BudTemplateItem.GetOrderNumberById(list);
		if (orderNumberById.Count > 1)
		{
			flag = BudTask.IsStructured(orderNumberById);
		}
		try
		{
			if (flag)
			{
				list.Reverse();
			}
			BudTemplateItem.Delete(list);
			this.strJS.Append("alert('系统提示：\\n删除成功！');");
			this.strJS.Append(string.Concat(new string[]
			{
				"location='BudTemplateList.aspx?tempId=",
				this.tvBudget.SelectedValue,
				"&tempType=",
				this.ddlType.SelectedValue,
				"';"
			}));
		}
		catch
		{
			this.strJS.Append("alert('系统提示：\\n请先删除子项！')");
		}
		base.RegisterScript(this.strJS.ToString());
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["TemplateItemId"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["orderNumber"] = text;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
		this.hfldTempId.Value = this.tvBudget.SelectedValue;
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
		this.BindGv();
		this.hfldTemTypeId.Value = this.ddlType.SelectedValue;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


