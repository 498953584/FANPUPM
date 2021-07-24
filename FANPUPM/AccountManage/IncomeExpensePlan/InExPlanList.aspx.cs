using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using System;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_IncomeExpensePlan_InExPlanList : NBasePage, IRequiresSessionState
{
	private InExPlanBLL InExPlan = new InExPlanBLL();
	private IEPInfoBLL IEPinfo = new IEPInfoBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind();
		}
	}
	public void Bind()
	{
		this.gvwInExPlan.DataSource = this.InExPlan.GetList();
		this.gvwInExPlan.DataBind();
	}
	protected void gvwInExPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["ID"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["id"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["auditContent"] = "支出合同 ：" + this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.gvwInExPlan.DataKeys[e.Row.RowIndex].Value.ToString(),
				"','",
				this.gvwInExPlan.DataKeys[e.Row.RowIndex].Values[1].ToString(),
				"');"
			});
		}
	}
	protected void gvwInExPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwInExPlan.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				for (int i = 0; i < this.gvwInExPlan.Rows.Count; i++)
				{
					CheckBox checkBox = this.gvwInExPlan.Rows[i].FindControl("chk") as CheckBox;
					if (checkBox.Checked && checkBox != null)
					{
						HiddenField hiddenField = this.gvwInExPlan.Rows[i].FindControl("hidIEPid") as HiddenField;
						this.IEPinfo.DelByIEPid(sqlTransaction, hiddenField.Value.ToString());
						this.InExPlan.Del(sqlTransaction, hiddenField.Value.ToString());
					}
				}
				sqlTransaction.Commit();
				this.Bind();
			}
			catch
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			}
		}
	}
}


