using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.TableTopDAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TableTop_UserModelList : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind();
		}
	}
	public void Bind()
	{
		DataTable modelInfo = this.deskTop.GetModelInfo(base.UserCode);
		this.gvwModelList.DataSource = modelInfo;
		this.gvwModelList.DataBind();
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			int num = this.deskTop.GetRowId(base.UserCode);
			for (int i = 0; i < this.gvwModelList.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvwModelList.Rows[i].FindControl("chk");
				if (checkBox.Checked)
				{
					DeskTopDalModel deskTopDalModel = new DeskTopDalModel();
					deskTopDalModel.userCode = base.UserCode;
					deskTopDalModel.ModelId = this.gvwModelList.DataKeys[i].Value.ToString();
					deskTopDalModel.MNewName = this.gvwModelList.Rows[i].Cells[2].Text.ToString();
					deskTopDalModel.orderId = new int?(num + 1);
					this.deskTop.Add(deskTopDalModel, sqlTransaction);
					num++;
				}
			}
			sqlTransaction.Commit();
		}
		base.RegisterScript("successed();");
	}
}


