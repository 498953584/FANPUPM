using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_AllocRole : NBasePage, IRequiresSessionState
{
	private PrivRoleService roleSer = new PrivRoleService();
	private PrivBusiDataRoleService dataSer = new PrivBusiDataRoleService();
	private string tableName = string.Empty;
	private System.Collections.Generic.IList<string> dataIdList = new System.Collections.Generic.List<string>();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["tableName"]))
		{
			this.tableName = base.Request["tableName"];
		}
		if (!string.IsNullOrEmpty(base.Request["dataId"]))
		{
			this.dataIdList = JsonHelper.GetListFromJson(base.Request["dataId"]);
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldRoleId.Value);
		if (this.tableName == "PT_MK")
		{
			this.dataSer.DeleteByRes(this.dataIdList[0]);
			PTMKService pTMKService = new PTMKService();
			this.dataIdList = pTMKService.GetParentAndChildren(this.dataIdList[0]);
		}
		if (this.dataIdList.Count == 1)
		{
			this.dataSer.DeleteByRes(this.dataIdList[0]);
		}
		foreach (string current in listFromJson)
		{
			foreach (string current2 in this.dataIdList)
			{
				PrivBusiDataRole privBusiDataRole = new PrivBusiDataRole();
				privBusiDataRole.Id = System.Guid.NewGuid().ToString();
				privBusiDataRole.RoleId = current;
				privBusiDataRole.TableName = this.tableName;
				privBusiDataRole.BusiDataId = current2;
				if (!this.dataSer.Exists(privBusiDataRole))
				{
					this.dataSer.Add(privBusiDataRole);
				}
			}
		}
		base.RegisterScript("succeed();");
	}
	private void InitPage()
	{
		if (this.dataIdList.Count == 1)
		{
			System.Collections.Generic.IList<string> roleId = this.dataSer.GetRoleId(this.dataIdList[0], this.tableName);
			this.hfldRoleId.Value = JsonHelper.JsonSerializer<string[]>(roleId.ToArray<string>());
		}
		System.Collections.Generic.List<PrivRole> dataSource = (
			from r in this.roleSer
			orderby r.No
			select r).ToList<PrivRole>();
		this.gvwRole.DataSource = dataSource;
		this.gvwRole.DataBind();
	}
}


