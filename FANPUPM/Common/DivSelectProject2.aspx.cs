using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectProject2 : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.BindProject2(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
		}
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.BindProject2(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
	}
	protected string GetOwnerName(object ownerId)
	{
		string result;
		try
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(ownerId));
			result = byId.CorpName;
		}
		catch
		{
			result = "";
		}
		return result;
	}
	private void BindProject2(string code, string name)
	{
		System.Collections.Generic.IList<SelectProject> project = SelectProjectHelper.GetProject(base.UserCode, Parameters.PrjAvaildState5, code, name);
		this.gvwPrj.DataSource = project;
		this.gvwPrj.DataBind();
	}
	protected void gvwPrj_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPrj.PageIndex = e.NewPageIndex;
		this.BindProject2(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
	}
	protected void gvwPrj_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrj.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["code"] = this.gvwPrj.DataKeys[e.Row.RowIndex]["Code"].ToString();
			e.Row.Attributes["name"] = this.gvwPrj.DataKeys[e.Row.RowIndex]["Name"].ToString();
			e.Row.Attributes["typeCode"] = this.gvwPrj.DataKeys[e.Row.RowIndex]["TypeCode"].ToString();
			bool flag = System.Convert.ToBoolean(this.gvwPrj.DataKeys[e.Row.RowIndex]["IsParent"]);
			e.Row.Attributes["isMainContract"] = flag.ToString();
		}
	}
}


