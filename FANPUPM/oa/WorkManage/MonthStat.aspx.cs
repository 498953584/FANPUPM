using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_MonthStat : BasePage, IRequiresSessionState
{
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)this.ViewState["STRWHERE"];
			}
			return "";
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Date_Bind();
		}
	}
	private void Date_Bind()
	{
		this.DDLYear.Items.Clear();
		int year = DateTime.Now.Year;
		for (int i = year; i > year - 5; i--)
		{
			this.DDLYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
		}
		this.DDLMonth.Items.Clear();
		for (int j = 1; j <= 12; j++)
		{
			this.DDLMonth.Items.Add(new ListItem(j.ToString(), j.ToString()));
		}
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			decimal monthStat_Person = OAOfficeCommonClas.GetMonthStat_Person(dataRowView["v_bmbm"].ToString(), int.Parse(this.DDLYear.SelectedValue), int.Parse(this.DDLMonth.SelectedValue));
			decimal monthStat_Department = OAOfficeCommonClas.GetMonthStat_Department(dataRowView["v_bmbm"].ToString(), int.Parse(this.DDLYear.SelectedValue), int.Parse(this.DDLMonth.SelectedValue));
			e.Row.Cells[2].Text = Convert.ToString(monthStat_Person + monthStat_Department);
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
}


