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
public partial class Equ_Repair_RepairReportList : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();
	private string equipmentType = "0";

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
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
			this.hfldEquipmentType.Value = this.equipmentType;
			this.BindGV();
		}
	}
	private void BindGV()
	{
		System.Collections.Generic.List<EquRepairReport> repairReports = this.GetRepairReports();
		this.AspNetPager1.RecordCount = repairReports.Count;
		this.gvRepair.DataSource = repairReports.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvRepair.DataBind();
	}
	private System.Collections.Generic.List<EquRepairReport> GetRepairReports()
	{
		return null;
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		new WFInstanceMainService();
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldIdsChecked.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldIdsChecked.Value);
		}
		else
		{
			list.Add(this.hfldIdsChecked.Value);
		}
		try
		{
			foreach (string current in list)
			{
				BusinessData.Delete("148", current);
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGV();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
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
	protected void gvRepair_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRepair.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvRepair.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


