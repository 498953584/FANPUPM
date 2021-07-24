using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Fund.prjReturn;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_prjReturn_prjReturnList : NBasePage, IRequiresSessionState
{
	private prjReturenBLL prBLL = new prjReturenBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldLoanID.Value = base.Request["LoanID"].ToString();
			this.gvBudget.PageSize = NBasePage.pagesize4;
			if (this.hfldLoanID.Value.ToString() != "" && base.Request["returnState"].ToString() != "" && base.Request["returnState"].ToString() != "1")
			{
				this.btnAdd.Disabled = false;
			}
			this.Bind();
		}
	}
	public void Bind()
	{
		string text = " PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' ) ";
		if (this.hfldLoanID.Value != "")
		{
			text = text + " and r.LoanID='" + this.hfldLoanID.Value + "'";
		}
		DataTable infoByWhe = this.prBLL.GetInfoByWhe(text, true);
		this.gvBudget.DataSource = infoByWhe;
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = dataRowView["PrjGuid"].ToString();
		}
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("chk") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						Guid id = new Guid(checkBox.ToolTip);
						int num = this.prBLL.Delete(sqlTransaction, id);
						if (num < 1)
						{
							throw new Exception("行" + gridViewRow.RowIndex + "删除失败！");
						}
					}
				}
				sqlTransaction.Commit();
				base.RegisterScript("alert('系统提示：\\n\\n数据删除成功！');");
				base.RegisterScript("window.location = window.location");
				this.Bind();
			}
			catch (Exception arg)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n" + arg + "');");
			}
		}
	}
}


