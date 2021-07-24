using ASP;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_FileLink : System.Web.UI.UserControl
{
	public int Type;
	public int MID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["MID"].ToString());
		}
		set
		{
			this.ViewState["MID"] = value;
		}
	}
	public string FID
	{
		get
		{
			return this.ViewState["FID"].ToString();
		}
		set
		{
			this.ViewState["FID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.LL.Text = this.FilesBind(this.MID, this.FID.ToString());
			this.Btn_Upload.Attributes["onclick"] = string.Concat(new object[]
			{
				"javascript:return openannexpage('",
				this.FID.ToString(),
				"',",
				this.MID,
				",",
				this.Type,
				");"
			});
			this.But_UpFile.Attributes["onclick"] = string.Concat(new object[]
			{
				"javascript:return upLoadFile('",
				this.FID.ToString(),
				"',",
				this.MID,
				");"
			});
			if (this.Type == 0)
			{
				this.Btn_Upload.Width = 0;
				this.But_UpFile.Width = 0;
				this.Btn_Upload.Style.Add("display", "none");
				this.But_UpFile.Style.Add("display", "none");
			}
		}
	}
	public string FilesBind(int moduleID, string recordCode)
	{
		string text = "";
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
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<a href='/Common/DownLoad2.aspx?path=",
				HttpUtility.UrlEncode(dataRow["FilePath"].ToString() + dataRow["AnnexName"].ToString(), Encoding.UTF8),
				"&Name=",
				HttpUtility.UrlEncode(dataRow["OriginalName"].ToString(), Encoding.UTF8),
				"'>",
				dataRow["OriginalName"].ToString(),
				"</a> "
			});
		}
		return text;
	}
	protected void Butsc_Click(object sender, EventArgs e)
	{
		this.LL.Text = this.FilesBind(this.MID, this.FID.ToString());
	}
}


