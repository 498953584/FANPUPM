using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipOilWear_RefuelApplyEdit : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService refuelApplySer = new EquShipRefuelApplyService();
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["Id"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindRefurlApply();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if (this.action == "add")
			{
				this.refuelApplySer.Add(this.GetModel(null));
				stringBuilder.Append("top.ui.show('添加成功');").Append(System.Environment.NewLine);
			}
			else
			{
				EquShipRefuelApply byId = this.refuelApplySer.GetById(this.id);
				this.refuelApplySer.Update(this.GetModel(byId));
				stringBuilder.Append("top.ui.show('编辑成功');").Append(System.Environment.NewLine);
			}
			stringBuilder.Append("winclose('RefuelApplyEdit', 'RefuelApplyList.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	private void BindRefurlApply()
	{
		if (this.action == "edit")
		{
			EquShipRefuelApply byId = this.refuelApplySer.GetById(this.id);
		}
	}
	private EquShipRefuelApply GetModel(EquShipRefuelApply model)
	{
		return model;
	}
}


