using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Bulletin_BulletinLock : BasePage, IRequiresSessionState
{
	public System.Guid BulletinID
	{
		get
		{
			object obj = this.ViewState["BulletinID"];
			if (obj != null)
			{
				return (System.Guid)obj;
			}
			return System.Guid.Empty;
		}
		set
		{
			this.ViewState["BulletinID"] = value;
		}
	}
	public string CurrentDate
	{
		get
		{
			object obj = this.ViewState["CurrentDate"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["CurrentDate"] = value;
		}
	}
	public new string UserCode
	{
		get
		{
			object obj = this.ViewState["UserCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["UserCode"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			base.Request.Url.ToString();
			this.UserCode = this.Session["yhdm"].ToString();
			if (base.Request.QueryString["ic"] != "")
			{
				this.BulletinID = new System.Guid(base.Request.QueryString["ic"]);
				this.hdnRecordId.Value = System.Convert.ToString(this.BulletinID);
				this.setData();
			}
			else
			{
				this.BulletinID = System.Guid.Empty;
				this.hdnRecordId.Value = System.Convert.ToString(System.Guid.NewGuid());
				System.DateTime dateTime = default(System.DateTime);
				this.CurrentDate = System.DateTime.Now.ToString();
			}
			this.InitFuJian(this.BulletinID.ToString());
			this.FilesBind(2, this.hdnRecordId.Value.ToString());
			BulletionAction.AddBulletionLog(this.BulletinID.ToString(), this.UserCode);
		}
	}
	protected void setData()
	{
        DataTable dataTable = com.jwsoft.pm.entpm.action.BulletinManage.QueryOneBulletin(this.BulletinID);
		if (dataTable.Rows.Count == 1)
		{
			this.hdnDept.Value = dataTable.Rows[0]["DeptRange"].ToString();
			this.lbTitle.Text = dataTable.Rows[0]["V_TITLE"].ToString();
			this.LblCon.Text = dataTable.Rows[0]["V_CONTENT"].ToString();
			this.lbDept.Text = oa_Bulletin_BulletinLock.getDeptbyuser(dataTable.Rows[0]["V_RELUSERCODE"].ToString());
			this.lbDate.Text = dataTable.Rows[0]["DTM_RELEASETIME"].ToString();
			new userManageDb();
			this.LbUserName.Text = dataTable.Rows[0]["V_RELEASEUSER"].ToString();
		}
	}
	public void InitFuJian(string ID)
	{
		this.Literal1.Text = "暂无";
		string text = base.Server.MapPath("/UploadFiles/bulletin/" + ID);
		if (System.IO.Directory.Exists(text))
		{
			this.Literal1.Text = "";
			string[] files = System.IO.Directory.GetFiles(text);
			string arg_4E_0 = string.Empty;
			string text2 = string.Empty;
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string path = array[i];
				text2 = System.IO.Path.GetFileName(path);
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("<li><a class='link' target='_blank' href='/Common/DownLoad.aspx?path=");
				stringBuilder.Append(string.Concat(new string[]
				{
					"/UploadFiles/bulletin/",
					ID,
					"/",
					HttpUtility.UrlEncode(text2, System.Text.Encoding.UTF8),
					"'>"
				}));
				stringBuilder.Append(text2);
				stringBuilder.Append("</a></li>");
				Literal expr_F0 = this.Literal1;
				expr_F0.Text += stringBuilder.ToString();
			}
		}
	}
	protected static string getDeptName(string deptCode)
	{
		string text = "";
		if (deptCode == "")
		{
			text = "";
		}
		else
		{
			int i = deptCode.IndexOf(",");
			if (i > 0)
			{
				while (i > 0)
				{
					string value = deptCode.Substring(0, i);
					text = text + oa_Bulletin_BulletinLock.getDept(System.Convert.ToInt32(value)) + ",";
					deptCode = deptCode.Substring(i + 1, deptCode.Length - i - 1);
					i = deptCode.IndexOf(",");
				}
				text += oa_Bulletin_BulletinLock.getDept(System.Convert.ToInt32(deptCode));
			}
			else
			{
				text += oa_Bulletin_BulletinLock.getDept(System.Convert.ToInt32(deptCode));
			}
		}
		return text;
	}
	protected static string getDept(int deptCode)
	{
		string sqlString = "select V_BMMC from pt_d_bm where i_bmdm = " + deptCode.ToString() + " and c_sfyx='y'";
		object value = publicDbOpClass.ExecuteScalar(sqlString);
		return System.Convert.ToString(value);
	}
	protected static string getDeptbyuser(string yhdm)
	{
		string sqlString = "SELECT    dbo.PT_d_bm.V_BMMC FROM     dbo.PT_yhmc INNER JOIN     dbo.PT_d_bm ON dbo.PT_yhmc.i_bmdm = dbo.PT_d_bm.i_bmdm  WHERE     (dbo.PT_yhmc.v_yhdm = " + yhdm + ") ";
		object value = publicDbOpClass.ExecuteScalar(sqlString);
		return System.Convert.ToString(value);
	}
	private void FilesBind(int moduleID, string recordCode)
	{
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string value = dataRow["OriginalName"].ToString();
			string str = dataRow["AnnexName"].ToString();
			string str2 = dataRow["FilePath"].ToString() + str;
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("<li><a class='link' target='_blank' href='/Common/DownLoad.aspx?path=");
			stringBuilder.Append(HttpUtility.UrlEncode(str2, System.Text.Encoding.UTF8) + "'>");
			stringBuilder.Append(value);
			stringBuilder.Append("</a></li>");
			Literal expr_EB = this.Literal1;
			expr_EB.Text += stringBuilder.ToString();
		}
	}
}


