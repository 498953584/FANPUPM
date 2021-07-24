using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Purchase_Purchase : NBasePage, IRequiresSessionState
{
	private readonly Purchase purchase = new Purchase();
	private string prjId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.gvwPurchase.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["PrjGuid"]))
		{
			this.prjId = base.Request["PrjGuid"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldPrjId.Value = this.prjId;
			this.DataBindPurchase(this.purchase.GetAllPurchase(this.prjId, 0));
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			this.lblProject.Text = pTPrjInfoService.GetById(this.prjId).PrjName;
		}
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPurchase.PageIndex = e.NewPageIndex;
		this.DataBindPurchase(this.purchase.GetAllPurchase(this.prjId, 0));
	}
	protected void gvwPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["prjGuid"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["auditContent"] = "采购单：" + this.gvwPurchase.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		DataTable table = this.purchase.GetTable(startDate, endDate, this.txtPcode.Text.Trim(), this.txtPeople.Text.Trim(), this.prjId, this.txtConName.Text.Trim(), 0);
		this.DataBindPurchase(table);
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPurchaseChecked.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
		}
		else
		{
			list.Add(this.hfldPurchaseChecked.Value);
		}
		if (this.purchase.Delete(list) == 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		this.DataBindPurchase(this.purchase.GetAllPurchase(this.prjId, 0));
	}
	private void DataBindPurchase(DataTable dataSource)
	{
		GridViewUtility.DataBind(this.gvwPurchase, dataSource);
	}
}


