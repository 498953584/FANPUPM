using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Purchase_EquipmentPurchase : NBasePage, IRequiresSessionState
{
	private readonly Purchase purchase = new Purchase();

	protected override void OnInit(EventArgs e)
	{
		this.gvwPurchase.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindPurchase(this.purchase.GetAllPurchase("", 1));
			this.hfldAdjunctPath.Value = ConfigHelper.Get("EquipmentPurchase");
		}
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPurchase.PageIndex = e.NewPageIndex;
		this.DataBindPurchase(this.purchase.GetAllPurchase("", 1));
	}
	protected void gvwPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["guid"] = this.gvwPurchase.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new DateTime?(Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new DateTime?(Convert.ToDateTime(this.txtEndTime.Text.Trim()));
		}
		DataTable table = this.purchase.GetTable(startDate, endDate, this.txtPcode.Text.Trim(), this.txtPeople.Text.Trim(), "", this.txtConName.Text.Trim(), 1);
		this.DataBindPurchase(table);
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldPurchaseChecked.Value.Contains("["))
		{
			List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
			List<string> list2 = new List<string>();
			foreach (string current in listFromJson)
			{
				list2.Add(this.purchase.GetCodeByGuid(current));
			}
			list = list2;
		}
		else
		{
			string guid = string.Empty;
			string item = string.Empty;
			guid = this.hfldPurchaseChecked.Value;
			item = this.purchase.GetCodeByGuid(guid);
			list.Add(item);
		}
		if (this.purchase.Delete(list) == 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		this.DataBindPurchase(this.purchase.GetAllPurchase("", 1));
	}
	private void DataBindPurchase(DataTable dataSource)
	{
		GridViewUtility.DataBind(this.gvwPurchase, dataSource);
	}
}


