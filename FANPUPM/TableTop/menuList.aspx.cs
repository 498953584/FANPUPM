using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.TableTopDAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class menuList : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();
	public string dtrow;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfModel.Value = "0";
			string key;
			switch (key = base.Request["op"].ToString())
			{
			case "1":
				this.labTitle.Text = "内部应用一";
				goto IL_145;
			case "2":
				this.labTitle.Text = "内部应用二";
				goto IL_145;
			case "3":
				this.labTitle.Text = "内部应用三";
				goto IL_145;
			case "4":
				this.labTitle.Text = "内部应用四";
				goto IL_145;
			case "5":
				this.labTitle.Text = "内部应用五";
				goto IL_145;
			case "6":
				this.labTitle.Text = "内部应用六";
				goto IL_145;
			}
			this.labTitle.Text = "内部应用";
			IL_145:
			this.hdfOp.Value = base.Request["op"].ToString();
			this.Bind();
		}
	}
	public void Bind()
	{
		DataTable dataTable = this.deskTop.meunList(base.UserCode, base.Request["op"].ToString());
		this.gvwModelList.DataSource = dataTable;
		this.gvwModelList.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			this.btnSave.Enabled = true;
			return;
		}
		this.btnSave.Enabled = false;
	}
	protected void gvwModelList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString();
			TextBox textBox = e.Row.FindControl("txtOrderID") as TextBox;
			TextBox textBox2 = e.Row.FindControl("txtModelName") as TextBox;
			DataTable userModelInfo = this.deskTop.GetUserModelInfo(base.UserCode, this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString());
			if (userModelInfo.Rows.Count > 0)
			{
				textBox.Text = userModelInfo.Rows[0][2].ToString();
				textBox2.Text = userModelInfo.Rows[0][3].ToString();
			}
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + this.gvwModelList.DataKeys[e.Row.RowIndex].Value.ToString() + "');";
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwModelList.Rows.Count; i++)
			{
				TextBox textBox = (TextBox)this.gvwModelList.Rows[i].FindControl("txtModelName");
				TextBox textBox2 = (TextBox)this.gvwModelList.Rows[i].FindControl("txtOrderID");
				menuModel menuModel = new menuModel();
				menuModel.userCode = base.UserCode;
				menuModel.ModelId = this.gvwModelList.DataKeys[i].Value.ToString();
				if (textBox.Text.ToString() != "")
				{
					menuModel.MNewName = textBox.Text.ToString();
				}
				else
				{
					menuModel.MNewName = this.gvwModelList.Rows[i].Cells[3].Text.ToString();
				}
				if (textBox2.Text != "")
				{
					menuModel.orderId = new int?(Convert.ToInt32(textBox2.Text.ToString()));
				}
				else
				{
					menuModel.orderId = new int?(i + 1);
				}
				menuModel.Sequence = base.Request["op"].ToString();
				this.deskTop.UpdMenu(menuModel, sqlTransaction);
			}
			sqlTransaction.Commit();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("top.ui.show( '保存成功！'); \n");
			stringBuilder.Append("top.ui.closeTab() \n;");
			base.RegisterScript(stringBuilder.ToString());
			this.Bind();
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			for (int i = 0; i < this.gvwModelList.Rows.Count; i++)
			{
				CheckBox checkBox = (CheckBox)this.gvwModelList.Rows[i].FindControl("chk");
				if (checkBox.Checked)
				{
					this.deskTop.DelMenu(base.UserCode, this.gvwModelList.DataKeys[i].Value.ToString(), sqlTransaction);
				}
			}
			sqlTransaction.Commit();
		}
		this.Bind();
	}
}


