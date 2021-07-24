using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Template_TemplateList : NBasePage, IRequiresSessionState
{
	private string TempTypeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["TempTypeId"]))
		{
			this.TempTypeId = base.Request["TempTypeId"];
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
			this.BindGv();
			if (this.tvTempType.SelectedValue == "" || this.tvTempType.SelectedValue == "0")
			{
				this.btnAdd.Attributes.Add("disabled", "disabled");
			}
			else
			{
				this.btnAdd.Attributes.Remove("disabled");
			}
			this.hfldTempTypeId.Value = this.tvTempType.SelectedValue;
		}
	}
	public void BindGv()
	{
		System.Collections.Generic.IList<BudTemplate> dataSource = new System.Collections.Generic.List<BudTemplate>();
		dataSource = BudTemplate.Select(NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex - 1, this.tvTempType.SelectedValue);
		this.gvTemplate.DataSource = dataSource;
		this.gvTemplate.DataBind();
		this.AspNetPager1.RecordCount = BudTemplate.GetAll(this.tvTempType.SelectedValue).Count;
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
		this.AddTempTypeIdToUrl();
		this.hfldTempTypeId.Value = this.tvTempType.SelectedValue;
	}
	private void AddTempTypeIdToUrl()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.Append("if (window.location.href.indexOf('tempTypeId') == -1) {").AppendLine();
		stringBuilder.Append("    window.location.href+='?&tempTypeId=" + this.tvTempType.SelectedValue + "';").AppendLine();
		stringBuilder.Append("} else {").AppendLine();
		stringBuilder.Append("    var index = window.location.href.indexOf('tempTypeId');").AppendLine();
		stringBuilder.Append("    var newUrl = window.location.href.substring(0,index);").AppendLine();
		stringBuilder.Append("    newUrl += 'tempTypeId=" + this.tvTempType.SelectedValue + "';").AppendLine();
		stringBuilder.Append("    window.location.href = newUrl;").AppendLine();
		stringBuilder.Append("}").AppendLine();
		base.RegisterScript(stringBuilder.ToString());
	}
	public void BindTree()
	{
		this.tvTempType.Nodes.Clear();
		TreeNode treeNode = new TreeNode("类型", "0");
		this.tvTempType.Nodes.AddAt(0, treeNode);
		System.Collections.Generic.IList<BudTemplateType> all = BudTemplateType.GetAll();
		for (int i = 0; i < all.Count; i++)
		{
			TreeNode treeNode2 = new TreeNode(all[i].Name, all[i].Id.ToString());
			if (base.Request["tempTypeId"] != null && base.Request["tempTypeId"] == treeNode2.Value)
			{
				treeNode2.Select();
			}
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.tvTempType.ExpandAll();
		if (string.IsNullOrEmpty(this.TempTypeId.Trim()))
		{
			this.tvTempType.Nodes[0].Selected = true;
			this.AddTempTypeIdToUrl();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPurchaseChecked.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		try
		{
			BudTemplate.Delete(list);
			base.RegisterScript("alert('系统提示：\\n删除成功！')");
			this.AddTempTypeIdToUrl();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n请先删除子项！')");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvTemplate.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


