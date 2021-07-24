using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_Resource_ResourceSelect_ResourceDetails : NBasePage, IRequiresSessionState
{
	protected ResourceLogicEdit resLogicEidt = new ResourceLogicEdit();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["rs"] == null || base.Request.QueryString["pt"] == null || base.Request.QueryString["cc"] == null)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "alert", "<script>alert('系统提示：\\n页面错误！ \\n请找技术人员！');return false;</script>");
				return;
			}
			this.GVMaterial.PageSize = NBasePage.pagesize;
			if (base.Request.QueryString["pt"] != null)
			{
				this.HdnPriceType.Value = base.Request.QueryString["pt"];
			}
			this.DataBindGVMaterial();
		}
	}
	protected void DataBindGVMaterial()
	{
		ResourceSelectBllAction resourceSelectBllAction = new ResourceSelectBllAction();
		DataTable materialList = resourceSelectBllAction.GetMaterialList(this.getStrWhere());
		if (materialList != null && materialList.Rows.Count > NBasePage.pagesize)
		{
			this.HdnIsPage.Value = "1";
		}
		Common2.BindGvTable(materialList, this.GVMaterial, false);
	}
	protected void GVMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				Label label = (Label)e.Row.Cells[1].FindControl("lblNo");
				label.Text = (this.GVMaterial.PageIndex * this.GVMaterial.PageSize + (e.Row.RowIndex + 1)).ToString();
				if (dataRowView["resourcecode"] != null && dataRowView["resourcecode"].ToString() != "")
				{
					((Label)e.Row.Cells[6].FindControl("lblUnit")).Text = this.resLogicEidt.GetUnitNameList(this.resLogicEidt.GetUnitId(dataRowView["resourcecode"].ToString()));
					((Label)e.Row.Cells[5].FindControl("lblPrice")).Text = this.resLogicEidt.GetNewResPrice(dataRowView["resourcecode"].ToString(), base.Request.QueryString["pt"]);
				}
				e.Row.ToolTip = dataRowView["resourcecode"].ToString();
				((CheckBox)e.Row.Cells[0].FindControl("chkSelectIt")).Attributes["onclick"] = string.Concat(new string[]
				{
					"parent.SelectCBox(this.checked,'",
					dataRowView["ResourceCode"].ToString(),
					"','GVMaterial','",
					base.Request.QueryString["pt"],
					"');"
				});
			}
			catch
			{
			}
		}
	}
	protected void GVMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVMaterial.PageIndex = e.NewPageIndex;
		this.DataBindGVMaterial();
	}
	protected void btnSertch_Click(object sender, EventArgs e)
	{
		this.DataBindGVMaterial();
	}
	protected string getStrWhere()
	{
		string text = "";
		if (this.txtResCode.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("ResourceCode", this.txtResCode.Text.Trim());
		}
		if (this.txtResName.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("ResourceName", this.txtResName.Text.Trim());
		}
		if (base.Request.QueryString["rs"] != "-2")
		{
			text = text + " and resourcestyle='" + base.Request.QueryString["rs"] + "'";
		}
		if (base.Request.QueryString["cc"] != "-1")
		{
			text = text + " and categorycode='" + base.Request.QueryString["cc"] + "' ";
		}
		return text;
	}
}


