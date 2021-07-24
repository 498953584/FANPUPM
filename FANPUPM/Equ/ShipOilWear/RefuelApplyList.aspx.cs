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
public partial class Equ_ShipOilWear_RefuelApplyList : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService refuelApplySer = new EquShipRefuelApplyService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
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
		System.Collections.Generic.List<EquShipRefuelApply> refuelApply = this.GetRefuelApply();
		this.AspNetPager1.RecordCount = refuelApply.Count;
		this.gvRefuelApply.DataSource = refuelApply.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvRefuelApply.DataBind();
	}
	private System.Collections.Generic.List<EquShipRefuelApply> GetRefuelApply()
	{
		this.txtCode.Text.Trim();
		this.txtPrjName.Text.Trim();
		new PTPrjInfoService();
		return null;
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGV();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
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
				EquShipRefuelApply byId = this.refuelApplySer.GetById(current);
				this.refuelApplySer.Delete(byId);
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGV();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	protected void gvRefuelApply_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvRefuelApply.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvRefuelApply.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjId"] = this.gvRefuelApply.DataKeys[e.Row.RowIndex].Values["PrjId"].ToString();
		}
	}
}


