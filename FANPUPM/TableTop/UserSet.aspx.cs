using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.TableTopBLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UserSet : NBasePage, IRequiresSessionState
{
	private string usercode = string.Empty;

	public string Usercode
	{
		get
		{
			return this.usercode;
		}
		set
		{
			this.usercode = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (this.Session["yhdm"] != null)
		{
			this.usercode = this.Session["yhdm"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GetModel(this.usercode);
		}
	}
	protected void Btndefault_Click(object sender, EventArgs e)
	{
		ClientScriptManager arg_0B_0 = this.Page.ClientScript;
		if (!this.Exists(this.usercode))
		{
			base.RegisterScript("success();");
			return;
		}
		if (this.Delete(this.usercode))
		{
			base.RegisterScript("success();");
			return;
		}
		base.RegisterScript("failuer();");
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Exists(this.usercode))
		{
			if (this.Update())
			{
				base.RegisterScript("success();");
				return;
			}
			base.RegisterScript("failuer();");
			return;
		}
		else
		{
			if (this.Add())
			{
				base.RegisterScript("success();");
				return;
			}
			base.RegisterScript("failuer();");
			return;
		}
	}
	private void init(string sql)
	{
		DataTable dataTable = publicDbOpClass.DataTableQuary(sql);
		if (dataTable != null)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				this.GirdColNum.Items.FindByValue(dataRow["GirdColNum"].ToString()).Selected = true;
				this.RowInGrid.Items.FindByValue(dataRow["RowInGrid"].ToString()).Selected = true;
				this.GirdWidth.Items.FindByValue(dataRow["GirdWidth"].ToString()).Selected = true;
				this.ColGapWidth.Items.FindByValue(dataRow["ColGapWidth"].ToString()).Selected = true;
			}
		}
	}
	public bool Add()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("insert into frame_Desktop_UserSet(");
		stringBuilder.Append("userCode,GirdColNum,RowInGrid,GirdWidth,ColGapWidth,RowGapWidth,HideNullGrid)");
		stringBuilder.Append(" values (");
		stringBuilder.Append("@userCode,@GirdColNum,@RowInGrid,@GirdWidth,@ColGapWidth,@RowGapWidth,@HideNullGrid)");
		SqlParameter[] array = new SqlParameter[]
		{
			new SqlParameter("@userCode", SqlDbType.VarChar, 50),
			new SqlParameter("@GirdColNum", SqlDbType.Int, 4),
			new SqlParameter("@RowInGrid", SqlDbType.Int, 4),
			new SqlParameter("@GirdWidth", SqlDbType.Int, 4),
			new SqlParameter("@ColGapWidth", SqlDbType.Int, 4),
			new SqlParameter("@RowGapWidth", SqlDbType.Int, 4),
			new SqlParameter("@HideNullGrid", SqlDbType.NChar, 1)
		};
		array[0].Value = this.usercode;
		array[1].Value = int.Parse(this.GirdColNum.SelectedValue.ToString());
		array[2].Value = int.Parse(this.RowInGrid.SelectedValue.ToString());
		array[3].Value = int.Parse(this.GirdWidth.SelectedValue.ToString());
		array[4].Value = int.Parse(this.ColGapWidth.SelectedValue.ToString());
		array[5].Value = int.Parse(this.RowGapWidth.SelectedValue.ToString());
		array[6].Value = (this.CheckBox1.Checked ? "y" : "n");
		int num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), array);
		return num > 0;
	}
	public bool Exists(string userCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("select * from frame_Desktop_UserSet");
		stringBuilder.Append(" where userCode=@userCode ");
		SqlParameter[] array = new SqlParameter[]
		{
			new SqlParameter("@userCode", SqlDbType.VarChar, 50)
		};
		array[0].Value = userCode;
		object obj = SqlHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), array);
		return obj != null && obj.ToString() != "";
	}
	public bool Update()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("update frame_Desktop_UserSet set ");
		stringBuilder.Append("GirdColNum=@GirdColNum,");
		stringBuilder.Append("RowInGrid=@RowInGrid,");
		stringBuilder.Append("GirdWidth=@GirdWidth,");
		stringBuilder.Append("ColGapWidth=@ColGapWidth,");
		stringBuilder.Append("RowGapWidth=@RowGapWidth,");
		stringBuilder.Append("HideNullGrid=@HideNullGrid");
		stringBuilder.Append(" where userCode=@userCode ");
		SqlParameter[] array = new SqlParameter[]
		{
			new SqlParameter("@GirdColNum", SqlDbType.Int, 4),
			new SqlParameter("@RowInGrid", SqlDbType.Int, 4),
			new SqlParameter("@GirdWidth", SqlDbType.Int, 4),
			new SqlParameter("@ColGapWidth", SqlDbType.Int, 4),
			new SqlParameter("@RowGapWidth", SqlDbType.Int, 4),
			new SqlParameter("@HideNullGrid", SqlDbType.NChar, 1),
			new SqlParameter("@userCode", SqlDbType.VarChar, 50)
		};
		array[0].Value = int.Parse(this.GirdColNum.SelectedValue.ToString());
		array[1].Value = int.Parse(this.RowInGrid.SelectedValue.ToString());
		array[2].Value = int.Parse(this.GirdWidth.SelectedValue.ToString());
		array[3].Value = int.Parse(this.ColGapWidth.SelectedValue.ToString());
		array[4].Value = int.Parse(this.RowGapWidth.SelectedValue.ToString());
		array[5].Value = "y";
		array[6].Value = this.usercode;
		int num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), array);
		return num > 0;
	}
	public bool Delete(string userCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("delete from frame_Desktop_UserSet ");
		stringBuilder.Append(" where userCode=@userCode ");
		SqlParameter[] array = new SqlParameter[]
		{
			new SqlParameter("@userCode", SqlDbType.VarChar, 50)
		};
		array[0].Value = userCode;
		int num = publicDbOpClass.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), array);
		return num > 0;
	}
	public void GetModel(string userCode)
	{
		DesktopBLL desktopBLL = new DesktopBLL();
		DataTable userSet = desktopBLL.GetUserSet(base.UserCode, base.Request["deskwidth"].ToString());
		if (userSet.Rows.Count > 0)
		{
			if (userSet.Rows[0]["userCode"] != null && userSet.Rows[0]["userCode"].ToString() != "")
			{
				this.usercode = userSet.Rows[0]["userCode"].ToString();
			}
			if (userSet.Rows[0]["GirdColNum"] != null && userSet.Rows[0]["GirdColNum"].ToString() != "")
			{
				this.GirdColNum.Items.FindByValue(userSet.Rows[0]["GirdColNum"].ToString()).Selected = true;
			}
			if (userSet.Rows[0]["RowInGrid"] != null && userSet.Rows[0]["RowInGrid"].ToString() != "")
			{
				this.RowInGrid.Items.FindByValue(userSet.Rows[0]["RowInGrid"].ToString()).Selected = true;
			}
			if (userSet.Rows[0]["GirdWidth"] != null && userSet.Rows[0]["GirdWidth"].ToString() != "")
			{
				this.GirdWidth.Items.FindByValue(userSet.Rows[0]["GirdWidth"].ToString()).Selected = true;
			}
			if (userSet.Rows[0]["ColGapWidth"] != null && userSet.Rows[0]["ColGapWidth"].ToString() != "")
			{
				this.ColGapWidth.Items.FindByValue(userSet.Rows[0]["ColGapWidth"].ToString()).Selected = true;
			}
			if (userSet.Rows[0]["RowGapWidth"] != null && userSet.Rows[0]["RowGapWidth"].ToString() != "")
			{
				this.RowGapWidth.Items.FindByValue(userSet.Rows[0]["RowGapWidth"].ToString()).Selected = true;
			}
			if (userSet.Rows[0]["HideNullGrid"] != null && userSet.Rows[0]["HideNullGrid"].ToString() != "")
			{
				if (userSet.Rows[0]["HideNullGrid"].ToString() == "y")
				{
					this.CheckBox1.Checked = true;
					return;
				}
				this.CheckBox1.Checked = false;
			}
		}
	}
}


