using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.PrjLoad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjLoan : NBasePage, IRequiresSessionState
{
	private PrjLoadLogic prjlodaBll = new PrjLoadLogic();
	public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldAdjunctPath.Value = ConfigurationManager.AppSettings["PrjLoan"];
			this.gvBudget.PageSize = NBasePage.pagesize4;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (this.hfldaccountId.Value.ToString() == "" && base.Request["accountid"] != null)
		{
			this.hfldaccountId.Value = base.Request["accountid"].ToString();
		}
		if (this.hfldaccountId.Value.ToString() != "")
		{
			DataTable dataSource = new DataTable();
			dataSource = this.prjlodaBll.GetLoadListByPrjGuid(this.hfldaccountId.Value.ToString());
			this.gvBudget.DataSource = dataSource;
		}
		this.gvBudget.DataBind();
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = dataRowView["PrjGuid"].ToString();
			e.Row.Attributes["onclick"] = "clickRows('" + this.gvBudget.DataKeys[e.Row.RowIndex]["LoanID"].ToString() + "',this)";
			Label label = e.Row.Cells[2].FindControl("labLoanCode") as Label;
			label.Attributes["onclick"] = "shoView('" + this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString() + "')";
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		bool flag = true;
		PrjLoadLogic prjLoadLogic = new PrjLoadLogic();
		string arg_0D_0 = string.Empty;
		List<string> list = new List<string>();
		if (this.hdnLoanID.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hdnLoanID.Value);
		}
		else
		{
			list.Add(this.hdnLoanID.Value);
		}
		using (SqlConnection sqlConnection = new SqlConnection(PrjLoan.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (!string.IsNullOrEmpty(list[i].ToString()) && !prjLoadLogic.Delete(new Guid(list[i].ToString()), sqlTransaction))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					sqlTransaction.Commit();
					this.BindGv();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("top.ui.show('删除成功');");
					base.RegisterScript(stringBuilder.ToString());
				}
				else
				{
					sqlTransaction.Rollback();
					sqlConnection.Close();
					base.RegisterScript("top.ui.alert('删除失败！');");
				}
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				sqlConnection.Close();
				base.RegisterScript("top.ui.alert('删除失败！');");
			}
		}
	}
}


