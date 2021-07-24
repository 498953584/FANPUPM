using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Priv_RoleEdit : NBasePage, IRequiresSessionState
{
	private PrivRoleService roleSer = new PrivRoleService();
	private string roleId = string.Empty;
	private string action = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.roleId = base.Request["id"];
		}
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.action == "add")
		{
			this.AddRole();
			return;
		}
		this.EditRole();
	}
	private void InitPage()
	{
		if (this.action == "edit")
		{
			PrivRole byId = this.roleSer.GetById(this.roleId);
			this.txtName.Text = byId.Name;
		}
	}
	private void AddRole()
	{
		PrivRole privRole = new PrivRole();
		privRole.Id = Guid.NewGuid().ToString();
		privRole.Name = this.txtName.Text.Trim();
		privRole.No = this.roleSer.GetNextNo();
		this.roleSer.Add(privRole);
		base.RegisterScript("succeed();");
	}
	private void EditRole()
	{
		PrivRole byId = this.roleSer.GetById(this.roleId);
		byId.Name = this.txtName.Text.Trim();
		this.roleSer.Update(byId);
		base.RegisterScript("succeed();");
	}
}


