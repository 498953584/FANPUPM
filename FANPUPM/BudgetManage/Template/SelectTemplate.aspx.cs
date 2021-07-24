using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Template_SelectTemplate : NBasePage, IRequiresSessionState
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
		if (base.Request["type"] != null)
		{
			this.ddlType.SelectedValue = base.Request["type"];
		}
		if (this.ddlType.Items.Count == 0)
		{
			this.btnSaveAs.Disabled = true;
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
			if (base.Request["template"] != null && base.Request["template"] == treeNode2.Value)
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
	protected void btnSave_Click1(object sender, System.EventArgs e)
	{
		string value = this.hfldCheckedIds.Value;
		int count = BudTemplateItem.GetResourcesByTempItemId(value).Count;
		if (count > 0)
		{
			this.strJS.Append("alert('系统提示：\\n此节点已配置资源，不能保存在此节点下！');");
		}
		else
		{
			bool flag = base.Request["save"] != null;
			if (flag)
			{
				this.strJS.Append("alert('系统提示：\\n已保存，不能重复保存！');");
			}
			else
			{
				string text = (this.Session["taskIds"] == null) ? "" : this.Session["taskIds"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
					if (text.Contains("["))
					{
						list = JsonHelper.GetListFromJson(text);
					}
					else
					{
						list.Add(text);
					}
					string inputUser = PageHelper.QueryUser(this, base.UserCode);
					BudTemplateItem.SaveTemplate(list, this.tvBudget.SelectedValue, value, inputUser);
					this.ViewState["save"] = true;
					this.btnSave.Enabled = false;
					this.strJS.Append("alert('系统提示：\\n保存成功！');");
					this.strJS.Append(string.Concat(new string[]
					{
						"location='SelectTemplate.aspx?type=",
						this.ddlType.SelectedValue,
						"&template=",
						this.tvBudget.SelectedValue,
						"&save=1';"
					}));
				}
			}
		}
		base.RegisterScript(this.strJS.ToString());
	}
	protected void btnSaveTemplate_Click(object sender, System.EventArgs e)
	{
		bool flag = base.Request["saveAs"] != null;
		if (flag)
		{
			this.strJS.Append("alert('系统提示：\\n已保存为新模板，不能重复保存！');");
		}
		else
		{
			string text = (this.Session["taskIds"] == null) ? "" : this.Session["taskIds"].ToString();
			if (!string.IsNullOrEmpty(text))
			{
				System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
				if (text.Contains("["))
				{
					list = JsonHelper.GetListFromJson(text);
				}
				else
				{
					list.Add(text);
				}
				BudTask.GetOrderNumberById(list);
				this.hdtemplateName.Value.Trim();
				string empty = string.Empty;
				this.AddTemplate(ref empty);
				string inputUser = PageHelper.QueryUser(this, base.UserCode);
				BudTemplateItem.SaveTemplate(list, empty, "", inputUser);
				this.strJS.Append("alert('系统提示：\\n保存成功！');");
				this.strJS.Append(string.Concat(new string[]
				{
					"location='SelectTemplate.aspx?type=",
					this.ddlType.SelectedValue,
					"&template=",
					empty,
					"&saveAs=1';"
				}));
				this.ViewState["saveAs"] = true;
				this.btnSaveAs.Disabled = true;
			}
		}
		base.RegisterScript(this.strJS.ToString());
	}
	private void AddTemplate(ref string typeId)
	{
		string selectedValue = this.ddlType.SelectedValue;
		if (!string.IsNullOrEmpty(selectedValue))
		{
			typeId = System.Guid.NewGuid().ToString();
			BudTemplateType byId = BudTemplateType.GetById(selectedValue);
			BudTemplate budTemplate = BudTemplate.Create(typeId, System.DateTime.Now.ToString("yyyyMMddHHmmsss"), this.hdtemplateName.Value.Trim(), PageHelper.QueryUser(this, base.UserCode), new System.DateTime?(System.DateTime.Now), byId);
			BudTemplate.Add(budTemplate);
		}
	}
}


