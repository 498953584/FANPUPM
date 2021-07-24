using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_PopedomSetup_PopedomSetup : BasePage, IRequiresSessionState
{

	protected string BussinessCode
	{
		get
		{
			return this.ViewState["BUSSINESSCODE"].ToString();
		}
		set
		{
			this.ViewState["BUSSINESSCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["c"] != null)
		{
			this.BussinessCode = base.Request["c"].ToString();
			this.BtnPopedomSetup.Attributes["onclick"] = "openEdit('" + this.BussinessCode + "')";
			this.Session["YHList"] = this.GetUserList();
		}
	}
	protected void GVUserList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView arg_21_0 = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.BussinessCode,
				"','",
				e.Row.Cells[1].Text,
				"')"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected ArrayList GetUserList()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < this.GVUserList.Rows.Count; i++)
		{
			arrayList.Add(this.GVUserList.Rows[i].Cells[1].Text);
		}
		return arrayList;
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		SqlDataSource expr_06 = this.SqlUserList;
		expr_06.SelectCommand = expr_06.SelectCommand + " and v_xm like '%" + this.TxtSearch.Text + "%'";
		this.GVUserList.DataBind();
	}
	protected void BtnPopedomSetup_Click(object sender, EventArgs e)
	{
		this.GVUserList.DataBind();
	}
}


