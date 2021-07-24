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
using Wuqi.Webdiyer;
public partial class Equ_Repair_SelectRepairPlan : NBasePage, IRequiresSessionState
{
	private EquRepairPlanService planSer = new EquRepairPlanService();
	private string equipmentType = "0";

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["equipmentType"]))
		{
			this.equipmentType = base.Request["equipmentType"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	private void BindGV()
	{
		System.Collections.Generic.List<EquRepairPlan> repairPlan = this.GetRepairPlan();
		this.AspNetPager1.RecordCount = repairPlan.Count;
		this.gvRepairPlan.DataSource = repairPlan.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvRepairPlan.DataBind();
	}
	private System.Collections.Generic.List<EquRepairPlan> GetRepairPlan()
	{
		this.txtCode.Text.Trim();
		return null;
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void gvRepairPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRepairPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["equipmentId"] = this.gvRepairPlan.DataKeys[e.Row.RowIndex].Values["EquipmentId"].ToString();
			e.Row.Attributes["code"] = this.gvRepairPlan.DataKeys[e.Row.RowIndex].Values["Code"].ToString();
			e.Row.Attributes["repairType"] = this.gvRepairPlan.DataKeys[e.Row.RowIndex].Values["RepairType"].ToString();
			e.Row.Attributes["rmFlag"] = this.gvRepairPlan.DataKeys[e.Row.RowIndex].Values["RmFlag"].ToString();
		}
	}
}


