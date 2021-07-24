using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractType_ManageConType : NBasePage, IRequiresSessionState
{
	private ContractType contractType = new ContractType();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.BindContractType();
		}
	}
	public void BindContractType()
	{
		int count = this.contractType.GetCount("", "", base.UserCode, new bool?(false));
		this.lbtInValid.DataSource = this.contractType.GetList("", "", 1, count, base.UserCode, new bool?(false));
		this.lbtInValid.DataTextField = "TypeName";
		this.lbtInValid.DataValueField = "TypeId";
		this.lbtInValid.DataBind();
		if (this.lbtInValid.Items.Count > 0)
		{
			this.lbtInValid.SelectedIndex = 0;
		}
		int count2 = this.contractType.GetCount("", "", base.UserCode, new bool?(true));
		this.lbtValid.DataSource = this.contractType.GetList("", "", 1, count2, base.UserCode, new bool?(true));
		this.lbtValid.DataTextField = "TypeName";
		this.lbtValid.DataValueField = "TypeId";
		this.lbtValid.DataBind();
		if (this.lbtValid.Items.Count > 0)
		{
			this.lbtValid.SelectedIndex = 0;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.hdInValidTypeId.Value != "")
		{
			this.contractType.UpdateTypeValid(this.hdInValidTypeId.Value.Substring(0, this.hdInValidTypeId.Value.Length - 1), "toInValid");
		}
		if (this.hdValidTypeId.Value != "")
		{
			this.contractType.UpdateTypeValid(this.hdValidTypeId.Value.Substring(0, this.hdValidTypeId.Value.Length - 1), "toValid");
		}
		this.BindContractType();
		base.RegisterScript("top.ui.closeWin(); top.ui.reloadTab();");
	}
}


