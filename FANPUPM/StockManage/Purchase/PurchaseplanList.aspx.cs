using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_Purchase_PurchaseplanList : NBasePage, IRequiresSessionState
{
	private const string ChkPurchaseplanName = "chkBox";
	private const string lblPurchaseplanName = "lblPpcode";
	private SmPurchaseplanBll purchaseplan = new SmPurchaseplanBll();
	private SmPurchaseplanStockBll purchaseplanStock = new SmPurchaseplanStockBll();
	private ISerializable ser = new JsonSerializer();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize3;
		if (!base.IsPostBack)
		{
			string strWhere = " where flowstate = 1 ";
			if (!string.IsNullOrEmpty(base.Request["prjId"]))
			{
				base.Request["prjId"].ToString();
			}
			this.AspNetPager1.CurrentPageIndex = 1;
			System.Collections.Generic.List<SmPurchaseplanModel> listArray = this.purchaseplan.GetListArray(strWhere);
			this.AspNetPager1.RecordCount = listArray.Count;
			this.DataBindPurchaseplan(listArray.Take(this.AspNetPager1.PageSize).ToList<SmPurchaseplanModel>());
			this.DataBindPurchaseplanStock(this.GetPurchaseplanStock());
		}
	}
	protected void gvwPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseplan.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvwPurchaseplanStock_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["number"] = this.gvwPurchaseplanStock.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<SmPurchaseplanModel> listArray = this.purchaseplan.GetListArray(" where flowstate = 1");
		System.Collections.Generic.IEnumerable<SmPurchaseplanModel> source = listArray.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.DataBindPurchaseplan(source.ToList<SmPurchaseplanModel>());
		this.SetCheckedBoxStatus();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		System.Collections.Generic.List<SmPurchaseplanModel> list = this.SelectPurchaseplan();
		this.AspNetPager1.RecordCount = list.Count;
		this.DataBindPurchaseplan(list.Take(this.AspNetPager1.PageSize).ToList<SmPurchaseplanModel>());
	}
	private DataTable GetPurchaseplanStock()
	{
		System.Collections.Generic.List<string> purchasePlanStockList = this.GetPurchasePlanStockList();
		return this.purchaseplanStock.GetResourceByPpcodes(purchasePlanStockList.ToArray());
	}
	private System.Collections.Generic.List<SmPurchaseplanModel> SelectPurchaseplan()
	{
		System.DateTime startDate = Common2.GetStartDate(this.txtStartTime);
		System.DateTime dateTime = Common2.GetEndDate(this.txtEndTime);
		if (!string.IsNullOrEmpty(this.txtEndTime.Text))
		{
			dateTime = Common2.GetEndDate(this.txtEndTime).AddDays(1.0);
		}
		if (startDate == System.DateTime.MinValue || dateTime == System.DateTime.MinValue)
		{
			base.RegisterScript("alert('系统提示：\\n\\n日期格式错误')");
		}
		return this.purchaseplan.GetList(startDate, dateTime, this.txtPpcode.Text.Trim());
	}
	private void DataBindPurchaseplan(System.Collections.Generic.List<SmPurchaseplanModel> purchaseplanModels)
	{
		GridViewUtility.DataBind<SmPurchaseplanModel>(this.gvwPurchaseplan, purchaseplanModels);
	}
	private void DataBindPurchaseplanStock(DataTable table)
	{
		GridViewUtility.DataBind(this.gvwPurchaseplanStock, table);
	}
	private System.Collections.Generic.List<string> GetPurchasePlanStockList()
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string[] array = this.ser.Deserialize<string[]>(this.hfldPurchasePlanCodes.Value);
		if (array != null)
		{
			list = array.ToList<string>();
		}
		string[] array2 = list.ToArray();
		string value = string.Empty;
		if (array2.Length > 0)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				string value2 = array3[i];
				stringBuilder.Append("','").Append(value2);
			}
			stringBuilder.Append("'");
			value = stringBuilder.ToString().Substring(2);
		}
		else
		{
			value = "''";
		}
		this.hidenppcode.Value = value;
		return list;
	}
	private void DataBindPurchaseplanStock()
	{
		DataTable resourceByPurchasePcodes = this.purchaseplanStock.GetResourceByPurchasePcodes(this.GetPurchasePlanStockList().ToArray());
		GridViewUtility.DataBind(this.gvwPurchaseplanStock, resourceByPurchasePcodes);
	}
	private void SetCheckedBoxStatus()
	{
		System.Collections.Generic.List<string> purchasePlanStockList = this.GetPurchasePlanStockList();
		foreach (GridViewRow gridViewRow in this.gvwPurchaseplan.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			Label label = gridViewRow.FindControl("lblPpcode") as Label;
			if (checkBox != null && label != null && purchasePlanStockList.Contains(label.Text) && !checkBox.Checked)
			{
				checkBox.Checked = true;
			}
			else
			{
				if (checkBox != null && label != null && !purchasePlanStockList.Contains(label.Text) && checkBox.Checked)
				{
					checkBox.Checked = false;
				}
			}
		}
	}
	protected void btnRefresh_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldCurPurchasePlanCodes.Value))
		{
			System.Collections.Generic.List<string> purchasePlanStockList = this.GetPurchasePlanStockList();
			purchasePlanStockList.Add(this.hfldCurPurchasePlanCodes.Value);
			this.hfldPurchasePlanCodes.Value = this.ser.Serialize<string[]>(purchasePlanStockList.ToArray());
		}
		this.SetCheckedBoxStatus();
		this.DataBindPurchaseplanStock();
	}
}


