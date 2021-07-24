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
public partial class Equ_Repair_RepairApplyEdit : NBasePage, IRequiresSessionState
{
	private EquRepairApplyService applySer = new EquRepairApplyService();
	private string action = string.Empty;
	private string equipmentType = "0";
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["equipmentType"]))
		{
			this.equipmentType = base.Request["equipmentType"].ToString();
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
			this.hfldEquipmentType.Value = this.equipmentType;
			this.BindRepairApply();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			string value = this.txtCode.Text.Trim();
			if (string.IsNullOrEmpty(value))
			{
				base.RegisterScript("top.ui.show('维修申请编号不能为空！')");
			}
			else
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				if (!(this.action == "add"))
				{
					this.applySer.GetById(this.id);
				}
				stringBuilder.Append("winclose('RepairApplyEdit', 'RepairApplyList.aspx?equipmentType=" + this.equipmentType + "', true)");
				base.RegisterScript(stringBuilder.ToString());
			}
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	private void BindRepairApply()
	{
		if (this.action == "edit")
		{
			EquRepairApply byId = this.applySer.GetById(this.id);
		}
	}
	private EquRepairApply GetModel(EquRepairApply model)
	{
		return null;
	}
}


