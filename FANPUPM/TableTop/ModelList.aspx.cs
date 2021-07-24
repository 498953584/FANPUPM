using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.TableTopDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TableTop_ModelList : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();
	public string dtrow;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfModel.Value = "0";
			this.Bind();
			if (base.Request.QueryString["orderid"] != null)
			{
				int num = this.deskTop.updateOrderID(base.Request.QueryString["orderid"].ToString(), base.UserCode);
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.Write(num);
				HttpContext.Current.Response.End();
			}
		}
	}
	public void Bind()
	{
		DataTable dataTable = this.deskTop.checkModel(base.UserCode);
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
				DeskTopDalModel deskTopDalModel = new DeskTopDalModel();
				deskTopDalModel.userCode = base.UserCode;
				deskTopDalModel.ModelId = this.gvwModelList.DataKeys[i].Value.ToString();
				if (textBox.Text.ToString() != "")
				{
					deskTopDalModel.MNewName = textBox.Text.ToString();
				}
				else
				{
					deskTopDalModel.MNewName = this.gvwModelList.Rows[i].Cells[3].Text.ToString();
				}
				string value = this.hfldtrorder.Value;
				string[] array = value.Split(new char[]
				{
					'|'
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					string text = array2[j];
					if (!string.IsNullOrEmpty(text))
					{
						string[] array3 = text.Split(new char[]
						{
							':'
						});
						dictionary.Add(array3[1], array3[0]);
					}
				}
				deskTopDalModel.orderId = new int?(int.Parse(dictionary[deskTopDalModel.ModelId.ToString()]));
				this.deskTop.Update(deskTopDalModel, sqlTransaction);
			}
			sqlTransaction.Commit();
			base.RegisterScript(" top.ui.generateDeskTop(); \n top.ui.show('保存成功');\n top.ui.closeTab(); \n");
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
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
					this.deskTop.DeleteModel(base.UserCode, this.gvwModelList.DataKeys[i].Value.ToString(), sqlTransaction);
				}
			}
			sqlTransaction.Commit();
		}
		this.Bind();
		base.RegisterScript("top.ui.show( '删除成功!');");
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void linkUp_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void linkDown_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
}


