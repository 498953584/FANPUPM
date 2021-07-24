using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Equipment_EquipmentUser : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindState();
			this.BindGV();
		}
	}
	private void BindGV()
	{
		string arg_0B_0 = this.ddlState.SelectedValue;
		this.txtCode.Text.Trim();
		this.txtName.Text.Trim();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	private void BindState()
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		this.ddlState.DataSource = basicCodeListService.GetByType("EquState");
		this.ddlState.DataTextField = "ItemName";
		this.ddlState.DataValueField = "ItemCode";
		this.ddlState.DataBind();
		this.ddlState.Items.Insert(0, new ListItem("", ""));
	}
	protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected string GetCorpName(object corpId)
	{
		string result = string.Empty;
		if (corpId != null)
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(corpId));
			if (byId != null)
			{
				result = byId.CorpName;
			}
		}
		return result;
	}
}


