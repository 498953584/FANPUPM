using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_SupplierGrade_SuperResultDetail : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			int spid = int.Parse(base.Request.QueryString["spid"].ToString());
			this.bindgv(spid);
		}
	}
	public void bindgv(int spid)
	{
		string sqlString = "select * from Res_SuperGradeTab where superid=" + spid;
		DataTable dataSource = publicDbOpClass.DataTableQuary(sqlString);
		this.GridView1.DataSource = dataSource;
		this.GridView1.DataBind();
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GridView1.PageIndex = e.NewPageIndex;
		this.bindgv(int.Parse(base.Request.QueryString["spid"].ToString()));
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1 && e.Row.RowType == DataControlRowType.DataRow)
		{
			Label label = e.Row.FindControl("labCount") as Label;
			decimal d = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["fileisall"].ToString());
			decimal d2 = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["numisover"].ToString());
			decimal d3 = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["lookisgood"].ToString());
			decimal d4 = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["tpyeisright"].ToString());
			decimal d5 = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["timeisquk"].ToString());
			decimal d6 = Convert.ToDecimal(((DataRowView)e.Row.DataItem)["smallisok"].ToString());
			label.Text = (d + d2 + d3 + d4 + d5 + d6).ToString();
			e.Row.Attributes["id"] = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


