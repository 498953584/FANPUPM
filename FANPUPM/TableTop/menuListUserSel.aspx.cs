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
public partial class allUserModel : NBasePage, IRequiresSessionState
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
		DataTable allModel = this.deskTop.GetAllModel(base.UserCode, this.code.Text.ToString(), this.name.Text.ToString());
		this.gvwModelList.DataSource = allModel;
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
			int num = this.deskTop.GetRowMenuId(base.UserCode, base.Request["op"].ToString());
			for (int i = 0; i < this.gvwModelList.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvwModelList.Rows[i].FindControl("chk");
				if (checkBox.Checked)
				{
					menuModel menuModel = new menuModel();
					menuModel.userCode = base.UserCode;
					menuModel.ModelId = this.gvwModelList.DataKeys[i].Value.ToString();
					menuModel.MNewName = this.gvwModelList.Rows[i].Cells[2].Text.ToString();
					menuModel.orderId = new int?(num + 1);
					menuModel.AddTime = DateTime.Now;
					menuModel.Sequence = base.Request["op"].ToString();
					this.deskTop.AddMenu(menuModel, sqlTransaction);
					num++;
				}
			}
			sqlTransaction.Commit();
		}
		base.RegisterScript("top.ui.winSuccess();top.ui.reloadTab();");
	}
	protected void SearchBt_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
}


