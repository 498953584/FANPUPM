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
public partial class Equ_Repair_SelectRepairApply : NBasePage, IRequiresSessionState
{
	private EquRepairApplyService applySer = new EquRepairApplyService();
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
		System.Collections.Generic.List<EquRepairApply> repairApply = this.GetRepairApply();
		this.AspNetPager1.RecordCount = repairApply.Count;
		this.gvRepairApply.DataSource = repairApply.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvRepairApply.DataBind();
	}
	private System.Collections.Generic.List<EquRepairApply> GetRepairApply()
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
	protected void gvRepairApply_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRepairApply.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvRepairApply.DataKeys[e.Row.RowIndex].Values["Code"].ToString();
			string text = this.gvRepairApply.DataKeys[e.Row.RowIndex].Values["RepairType"].ToString();
			e.Row.Attributes["repairType"] = text;
			e.Row.Attributes["repairTypeName"] = Common2.GetRepairType(text);
		}
	}
}


