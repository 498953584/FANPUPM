using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostAccounting : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.gvBudget.DataSource = CostAccounting.GetAll();
		this.gvBudget.DataBind();
	}
	protected void gv_Budget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			CostAccounting byId = CostAccounting.GetById(text);
			e.Row.Attributes["layer"] = (byId.Code.Length / 3).ToString();
			e.Row.Attributes["orderNumber"] = byId.Code;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["code"] = this.gvBudget.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string text2 = e.Row.Cells[3].Text;
			if (text2 == "D")
			{
				text2 = "直接成本";
			}
			else
			{
				if (text2 == "I")
				{
					text2 = "间接成本";
				}
				else
				{
					text2 = string.Empty;
				}
			}
			e.Row.Cells[3].Text = text2;
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldChecked.Value);
		string str = "失败,正在使用不能删除";
		bool flag = CostAccounting.Del(listFromJson);
		if (flag)
		{
			str = "成功";
		}
		base.RegisterScript("top.ui.show('删除" + str + "！');location='CostAccounting.aspx';");
	}
}


