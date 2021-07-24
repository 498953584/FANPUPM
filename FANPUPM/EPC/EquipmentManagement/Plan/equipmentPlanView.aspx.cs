using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_EquipmentManagement_Plan_equipmentPlanView : PageBase, IRequiresSessionState
{
	private EquipmentAction equipmentAction = new EquipmentAction();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.RestoreEquipmentPlan(base.Request["ic"].ToString());
		}
	}
	private void RestoreEquipmentPlan(string PlanId)
	{
		EquipmentPlanInfo equipmentPlanInfo = new EquipmentPlanInfo();
		if (PlanId.Length > 20)
		{
			equipmentPlanInfo = this.equipmentAction.GetwfPlanforGuid(PlanId);
		}
		else
		{
			equipmentPlanInfo = this.equipmentAction.GetSingleEquipmentPlan(PlanId);
		}
		this.txtPlanCode.Text = equipmentPlanInfo.PlanCode;
		this.txtPlanMaker.Text = equipmentPlanInfo.PlanMaker;
		this.txtRemark.Text = equipmentPlanInfo.Remark;
		this.calPlanCreatTime.Text = equipmentPlanInfo.PlanCreatTime.ToShortDateString();
		this.calEnterDate.Text = equipmentPlanInfo.EnterDate.ToShortDateString();
		this.calExitDate.Text = equipmentPlanInfo.ExitDate.ToShortDateString();
		this.txtPrjName.Text = this.GetPrjName(equipmentPlanInfo.PrjCode.ToString());
		this.ViewState["ResourcesTable"] = this.equipmentAction.GetResourceByPlanCode(equipmentPlanInfo.PlanCode);
		this.grdDetail.DataSource = this.ViewState["ResourcesTable"];
		this.grdDetail.DataBind();
	}
	public string GetPrjName(string prjGuid)
	{
		string sqlString = "select prjName from pt_prjInfo where prjGuid='" + prjGuid + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		string result = "";
		if (dataTable.Rows.Count > 0)
		{
			result = dataTable.Rows[0][0].ToString();
		}
		return result;
	}
}


