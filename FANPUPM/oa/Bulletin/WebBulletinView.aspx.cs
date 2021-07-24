using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Bulletin_WebBulletinView : Page, IRequiresSessionState
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
	public string UserCode
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
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			base.Request.Url.ToString();
			if (base.Request.QueryString["rid"] != "")
			{
				this.BulletinID = new System.Guid(base.Request.QueryString["rid"]);
				this.setData();
			}
			else
			{
				this.BulletinID = System.Guid.Empty;
				this.CurrentDate = System.DateTime.Now.ToString();
			}
		}
		this.FilesBind(2);
	}
	protected void setData()
	{
        DataTable dataTable = com.jwsoft.pm.entpm.action.BulletinManage.QueryOneBulletin(this.BulletinID);
		if (dataTable.Rows.Count == 1)
		{
			this.lbTitle.Text = dataTable.Rows[0]["V_TITLE"].ToString();
			this.hlUrl.Text = dataTable.Rows[0]["URL"].ToString();
			this.hlUrl.NavigateUrl = dataTable.Rows[0]["URL"].ToString();
			this.LblCon.Text = dataTable.Rows[0]["V_CONTENT"].ToString();
			this.lbDate.Text = dataTable.Rows[0]["DTM_RELEASETIME"].ToString();
		}
	}
	protected void FilesBind(int moduleID)
	{
		this.Literal1.Text = "";
		string recordCode = this.BulletinID.ToString();
		int annexType = 0;
		AnnexAction annexAction = new AnnexAction();
		DataTable fileList = annexAction.GetFileList(recordCode, annexType, moduleID);
		foreach (DataRow dataRow in fileList.Rows)
		{
			Literal expr_5E = this.Literal1;
			string text = expr_5E.Text;
			expr_5E.Text = string.Concat(new string[]
			{
				text,
				"<a href='",
				dataRow["FilePath"].ToString(),
				dataRow["AnnexName"].ToString(),
				"' target=_blank>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
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
					text = text + oa_Bulletin_WebBulletinView.getDept(System.Convert.ToInt32(value)) + ",";
					deptCode = deptCode.Substring(i + 1, deptCode.Length - i - 1);
					i = deptCode.IndexOf(",");
				}
				text += oa_Bulletin_WebBulletinView.getDept(System.Convert.ToInt32(deptCode));
			}
			else
			{
				text += oa_Bulletin_WebBulletinView.getDept(System.Convert.ToInt32(deptCode));
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
}


