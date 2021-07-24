using ASP;
using cn.justwin.BLL;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_PrjTypeList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindTV();
			this.hdPrjTypeID.Value = this.tvPrjType.SelectedValue.ToString();
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = PrjType.GetCountByType(this.tvPrjType.SelectedValue);
		System.Collections.Generic.List<PrjType> byType = PrjType.GetByType(this.tvPrjType.SelectedValue, this.AspNetPager1.CurrentPageIndex - 1, this.AspNetPager1.PageSize);
		this.gvPrjType.DataSource = byType;
		this.gvPrjType.DataBind();
	}
	protected void gvPrjType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvPrjType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void BindTV()
	{
		System.Collections.Generic.Dictionary<string, string> codeType = PrjType.GetCodeType();
		this.tvPrjType.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "工程类型";
		treeNode.Value = "";
		this.tvPrjType.Nodes.Add(treeNode);
		TreeNode treeNode2 = new TreeNode();
		treeNode2.Text = codeType["ConstructType"].ToString();
		treeNode2.Value = "ConstructType";
		if (base.Request["tv"] != null && base.Request["tv"] == treeNode2.Value)
		{
			treeNode2.Select();
		}
		treeNode.ChildNodes.Add(treeNode2);
		TreeNode treeNode3 = new TreeNode();
		treeNode3.Text = codeType["DesignType"].ToString();
		treeNode3.Value = "DesignType";
		treeNode.ChildNodes.Add(treeNode3);
		if (base.Request["tv"] != null && base.Request["tv"] == treeNode3.Value)
		{
			treeNode3.Select();
		}
		if (base.Request["tv"] == null)
		{
			treeNode2.Select();
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.hdPrjTypeID.Value = this.tvPrjType.SelectedValue.ToString();
		this.BindGv();
		this.AspNetPager1.CurrentPageIndex = 1;
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
			PrjType.Delete(this.tvPrjType.SelectedValue, list);
			base.RegisterScript("alert('系统提示：\\n\\n删除成功!');");
			this.BindGv();
		}
		catch (System.Exception ex)
		{
			base.RegisterScript("alert('系统提示：\\n\\n" + ex.Message + "!');");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


