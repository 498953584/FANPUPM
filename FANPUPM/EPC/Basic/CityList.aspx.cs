using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_AddCity : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTV();
			this.hdProvinceID.Value = this.tvProVince.SelectedValue.ToString();
			this.gvCity.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	protected void BindTV()
	{
		Dictionary<string, string> dictionaryOfProvice = new BasicProvinceService().GetDictionaryOfProvice();
		this.tvProVince.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "省份信息";
		treeNode.Value = "";
		this.tvProVince.Nodes.Add(treeNode);
		foreach (KeyValuePair<string, string> current in dictionaryOfProvice)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = current.Value;
			treeNode2.Value = current.Key;
			treeNode.ChildNodes.Add(treeNode2);
		}
	}
	protected void BindGv()
	{
		DataTable dtbByProviceId = new BasicCityService().GetDtbByProviceId(this.hdProvinceID.Value);
		this.gvCity.DataSource = dtbByProviceId;
		this.gvCity.DataBind();
	}
	protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hdProvinceID.Value = this.tvProVince.SelectedValue.ToString();
		this.BindGv();
	}
	protected void gvCity_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvCity.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void gvPrjType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvCity.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["provinceId"] = this.gvCity.DataKeys[e.Row.RowIndex]["ProvinceId"].ToString();
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		string value = this.hfldCityID.Value;
		if (!string.IsNullOrEmpty(value))
		{
			BasicCityService basicCityService = new BasicCityService();
			BasicCity cityByCityID = basicCityService.GetCityByCityID(value);
			try
			{
				basicCityService.Delete(cityByCityID);
				this.BindGv();
				base.RegisterScript("top.ui.show('系统提示：\\n\\n删除成功!');");
			}
			catch
			{
				base.RegisterScript("top.ui.alert('系统提示：\\n\\n删除失败!');");
			}
		}
	}
}


