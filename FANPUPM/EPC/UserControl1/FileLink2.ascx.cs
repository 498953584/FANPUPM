using ASP;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_FileLink2 : System.Web.UI.UserControl
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
			this.txtname.Value = this.FilesBind(this.MID, this.FID.ToString());
			this.Btn_Upload.Attributes["onclick"] = string.Concat(new object[]
			{
				"javascript:return openannexpage('",
				this.FID.ToString(),
				"',",
				this.MID,
				",'",
				this.Type,
				"');"
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
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			moduleID.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_53_0 = dataTable.Rows.Count;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			stringBuilder.Append(dataRow["OriginalName"].ToString());
			stringBuilder.Append(", ");
		}
		string result = string.Empty;
		if (stringBuilder.Length > 2)
		{
			result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
		}
		return result;
	}
	protected void Butsc_Click(object sender, EventArgs e)
	{
		this.txtname.Value = this.FilesBind(this.MID, this.FID.ToString());
	}
}


