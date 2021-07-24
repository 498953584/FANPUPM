using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_Resource_ResourceSelect_ResourceSelected : NBasePage, IRequiresSessionState
{
	protected ResourceLogicEdit resLogicEidt = new ResourceLogicEdit();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["rc"] == null || base.Request.QueryString["pt"] == null)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "alert", "<script>alert('系统提示：\\n页面错误！ \\n请找技术人员！');return false;</script>");
				return;
			}
			this.GVMaterial.PageSize = NBasePage.pagesize;
			this.DataBindGVMaterial();
		}
	}
	protected void DataBindGVMaterial()
	{
		ResourceSelectBllAction resourceSelectBllAction = new ResourceSelectBllAction();
		this.GVMaterial.DataSource = resourceSelectBllAction.GetResMaterialListByResCode((base.Request.QueryString["rc"] == "") ? "-1" : base.Request.QueryString["rc"]);
		this.GVMaterial.DataBind();
	}
	protected void GVMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			Label label = (Label)e.Row.Cells[1].FindControl("lblNo");
			label.Text = (this.GVMaterial.PageIndex * this.GVMaterial.PageSize + (e.Row.RowIndex + 1)).ToString();
			if (dataRowView["resourcecode"] != null && dataRowView["resourcecode"].ToString() != "")
			{
				((Label)e.Row.Cells[6].FindControl("lblUnit")).Text = this.resLogicEidt.GetUnitNameList(this.resLogicEidt.GetUnitId(dataRowView["resourcecode"].ToString()));
				((Label)e.Row.Cells[5].FindControl("lblPrice")).Text = this.resLogicEidt.GetNewResPrice(dataRowView["resourcecode"].ToString(), base.Request.QueryString["pt"].ToString());
			}
			CheckBox checkBox = (CheckBox)e.Row.Cells[0].FindControl("chkSelectIt");
			checkBox.Attributes["onclick"] = string.Concat(new string[]
			{
				"parent.SelectCBox2(this.checked,'",
				dataRowView["ResourceCode"].ToString(),
				"','GVMaterial','",
				base.Request.QueryString["pt"],
				"');"
			});
			checkBox.Checked = true;
		}
	}
	protected void GVMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVMaterial.PageIndex = e.NewPageIndex;
		this.DataBindGVMaterial();
	}
}


