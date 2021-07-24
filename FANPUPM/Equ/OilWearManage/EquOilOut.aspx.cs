using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_OilWearManage_EquOilOut : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equipmentSer = new EquEquipmentService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		this.txtCode.Text.Trim();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwOilOut_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwOilOut.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwOilOut.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjId"] = this.gvwOilOut.DataKeys[e.Row.RowIndex].Values["PrjId"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldId.Value);
		}
		else
		{
			list.Add(this.hfldId.Value);
		}
		try
		{
			foreach (string arg_51_0 in list)
			{
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGvw();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
	protected string GetOutType(object hireType)
	{
		string result = string.Empty;
		if (hireType == null)
		{
			result = "";
		}
		else
		{
			if (hireType.ToString() == "S")
			{
				result = "自用";
			}
			if (hireType.ToString() == "L")
			{
				result = "外租";
			}
		}
		return result;
	}
}


