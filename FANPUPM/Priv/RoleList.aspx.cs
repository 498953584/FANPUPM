using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Priv_RoleList : NBasePage, IRequiresSessionState
{
	private PrivRoleService roleSer = new PrivRoleService();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	protected void gvwRole_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwRole.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldRoleId.Value);
		foreach (string current in listFromJson)
		{
			PrivRole byId = this.roleSer.GetById(current);
			if (byId != null)
			{
				this.roleSer.Delete(byId);
			}
		}
		this.InitPage();
	}
	private void InitPage()
	{
		List<PrivRole> dataSource = (
			from r in this.roleSer
			orderby r.No
			select r).ToList<PrivRole>();
		this.gvwRole.DataSource = dataSource;
		this.gvwRole.DataBind();
	}
	protected string GetUserNameCsv(object roleId)
	{
		PrivUserRoleService privUserRoleService = new PrivUserRoleService();
		IList<string> userByRole = privUserRoleService.GetUserByRole(roleId.ToString());
		PTYhmcService pTYhmcService = new PTYhmcService();
		IList<string> nameList = pTYhmcService.GetNameList(userByRole);
		return nameList.ToCsv();
	}
}


