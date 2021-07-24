using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Template_TemplateImport : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private System.Text.StringBuilder strJS = new System.Text.StringBuilder("setWidthHeight();");

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
		DataTable templateItem = BudTemplate.GetTemplateItem(this.tvBudget.SelectedValue);
		this.gvBudget.DataSource = templateItem;
		this.gvBudget.DataBind();
	}
	public void BindDrop()
	{
		this.ddlType.DataSource = BudTemplateType.GetAll();
		this.ddlType.DataTextField = "Name";
		this.ddlType.DataValueField = "Id";
		this.ddlType.DataBind();
		if (base.Request["year"] != null)
		{
			this.ddlType.SelectedValue = base.Request["year"];
		}
	}
	protected void BindTree()
	{
		this.tvBudget.Nodes.Clear();
		TreeNode treeNode = new TreeNode("模板", "0");
		treeNode.SelectAction = TreeNodeSelectAction.None;
		this.tvBudget.Nodes.AddAt(0, treeNode);
		if (string.IsNullOrEmpty(this.ddlType.SelectedValue))
		{
			return;
		}
		System.Collections.Generic.IList<BudTemplate> all = BudTemplate.GetAll(this.ddlType.SelectedValue);
		int num = 0;
		foreach (BudTemplate current in all)
		{
			num++;
			TreeNode treeNode2 = new TreeNode(current.Name, current.Id);
			if (num == 1)
			{
				treeNode2.Select();
			}
			if (base.Request["prjId"] != null && base.Request["prjId"] == treeNode2.Value)
			{
				treeNode2.Select();
			}
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.tvBudget.ExpandAll();
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
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
		this.BindGv();
	}
	protected void btnImport_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		CheckBox checkBox = this.gvBudget.HeaderRow.Cells[0].FindControl("cbAllBox") as CheckBox;
		if (checkBox != null)
		{
			if (checkBox.Checked)
			{
				list = BudTemplateItem.GetTemplateItemIds(this.tvBudget.SelectedValue);
			}
			else
			{
				string value = this.hfldCheckedIds.Value;
				if (value.Contains('['))
				{
					list = JsonHelper.GetListFromJson(value);
				}
				else
				{
					list.Add(value);
				}
			}
		}
		string text = base.Request["prjId"];
		if (!string.IsNullOrEmpty(text))
		{
			string text2 = base.Request["taskId"];
			bool flag = true;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
				Project.CoverVersion(text);
			}
			else
			{
				flag = (BudTask.GetById(text2) != null);
			}
			bool flag2 = false;
			if (flag)
			{
				System.Collections.Generic.List<BudTemplateItem> list2 = new System.Collections.Generic.List<BudTemplateItem>();
				foreach (string current in list)
				{
					BudTemplateItem byId = BudTemplateItem.GetById(current, this.tvBudget.SelectedValue);
					list2.Add(byId);
					if (byId != null)
					{
						flag2 = BudTask.CheckCode(byId.Code, text);
						if (flag2)
						{
							break;
						}
					}
				}
				if (flag2)
				{
					base.RegisterScript("alert('系统提示：\\n导入失败!\\n导入的模版中有节点的任务编码与已有的节点出现重复！');");
					return;
				}
				list2 = (
					from t in list2
					orderby t.OrderNumber
					select t).ToList<BudTemplateItem>();
				list.Clear();
				foreach (BudTemplateItem current2 in list2)
				{
					list.Add(current2.Id.ToString());
				}
				bool flag3 = BudTask.IsStructured(this.GetOrderNumbers(list));
				int num = 0;
				string start = string.Empty;
				System.Collections.Generic.List<BudTemplateItem> list3 = null;
				if (flag3)
				{
					num = this.GetOrderNumbers(list).Min((string m) => m.Length);
					start = BudTask.GetOrderNumber(text, text2);
					list3 = this.ChangeId(list, this.tvBudget.SelectedValue);
				}
				string inputUser = PageHelper.QueryUser(this, base.UserCode);
				int num2 = 0;
				foreach (string current3 in list)
				{
					BudTemplateItem budTemplateItem;
					if (flag3)
					{
						budTemplateItem = list3[num2];
					}
					else
					{
						budTemplateItem = BudTemplateItem.GetById(current3, this.tvBudget.SelectedValue);
						budTemplateItem.Id = System.Guid.NewGuid().ToString();
					}
					string id = budTemplateItem.Id;
					string parentTaskId = string.Empty;
					if (flag3)
					{
						parentTaskId = budTemplateItem.ParentId;
					}
					else
					{
						parentTaskId = text2;
					}
					BudTask budTask = BudTask.Create(id, parentTaskId, null, text, budTemplateItem.Code, budTemplateItem.Name, budTemplateItem.Unit, budTemplateItem.Quantity, null, null, true, budTemplateItem.Note, inputUser, System.DateTime.Now, null, null);
					if (flag3)
					{
						if (budTemplateItem.OrderNumber.Length == num)
						{
							start = BudTask.GetOrderNumber(text, text2);
							budTask.ParentId = text2;
						}
						budTask.OrderNumber = this.GetNewOrderNumber(start, budTemplateItem.OrderNumber, num);
					}
					BudTask.Add(budTask, false);
					System.Collections.Generic.List<TaskResource> list4 = BudTemplateItem.GetResourcesByTempItemId(current3).ToList<TaskResource>();
					if (list4.Count > 0)
					{
						foreach (TaskResource current4 in list4)
						{
							budTask.AddResource(current4.Resource, current4.Quantity, current4.Price, 1m, "add");
							BudTask.AddResource(budTask);
						}
					}
					num2++;
				}
				string arg_3F6_0 = base.Request["year"];
				this.strJS.Append("top.ui.tabSuccess({ parentName: '_BudgetPlaitList' });");
			}
			else
			{
				this.strJS.Append("alert('系统提示：\\n导入失败!\\n导入的根节点已不存在！');");
			}
		}
		base.RegisterScript(this.strJS.ToString());
	}
	public string GetNewOrderNumber(string start, string oldOrderNubmer, int length)
	{
		string empty = string.Empty;
		return start + oldOrderNubmer.Remove(0, length);
	}
	public System.Collections.Generic.List<string> GetOrderNumbers(System.Collections.Generic.List<string> lstIds)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		foreach (string current in lstIds)
		{
			string orderNumber = BudTemplateItem.GetById(current, this.tvBudget.SelectedValue).OrderNumber;
			list.Add(orderNumber);
		}
		list.Sort();
		return list;
	}
	public System.Collections.Generic.List<BudTemplateItem> ChangeId(System.Collections.Generic.List<string> lstIds, string templateId)
	{
		System.Collections.Generic.List<BudTemplateItem> list = new System.Collections.Generic.List<BudTemplateItem>();
		foreach (string current in lstIds)
		{
			BudTemplateItem byId = BudTemplateItem.GetById(current, templateId);
			list.Add(byId);
		}
		foreach (string current2 in lstIds)
		{
			string text = System.Guid.NewGuid().ToString();
			foreach (BudTemplateItem current3 in list)
			{
				if (current3.Id == current2)
				{
					current3.Id = text;
				}
				if (current3.ParentId == current2)
				{
					current3.ParentId = text;
				}
			}
		}
		return list;
	}
}


