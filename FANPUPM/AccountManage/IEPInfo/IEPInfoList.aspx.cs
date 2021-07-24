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
public partial class AccountManage_IEPInfo_IEPInfoList : NBasePage, IRequiresSessionState
{
	private IEPInfoBLL IEPInfo = new IEPInfoBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfIEPId.Value = base.Request["id"].ToString();
			this.Bind();
			if (this.hdfIEPId.Value == "")
			{
				this.btnAdd.Disabled = true;
				return;
			}
			this.btnAdd.Disabled = false;
		}
	}
	public void Bind()
	{
		string value = this.hdfIEPId.Value;
		this.gvwIEPInfo.DataSource = this.IEPInfo.GetList(value);
		this.gvwIEPInfo.DataBind();
	}
	protected void gvwIEPInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["ID"] = this.gvwIEPInfo.DataKeys[e.Row.RowIndex].Values[0].ToString();
		}
	}
	protected void gvwIEPInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwIEPInfo.PageIndex = e.NewPageIndex;
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
				for (int i = 0; i < this.gvwIEPInfo.Rows.Count; i++)
				{
					CheckBox checkBox = this.gvwIEPInfo.Rows[i].FindControl("chk") as CheckBox;
					if (checkBox.Checked && checkBox != null)
					{
						HiddenField hiddenField = this.gvwIEPInfo.Rows[i].FindControl("hidIEPid") as HiddenField;
						this.IEPInfo.Del(sqlTransaction, hiddenField.Value.ToString());
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


