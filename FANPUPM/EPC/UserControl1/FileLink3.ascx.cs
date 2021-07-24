using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_FileLink3 : System.Web.UI.UserControl
{

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
	public int Type
	{
		get
		{
			if (this.ViewState["Type"] != null)
			{
				return Convert.ToInt32(this.ViewState["Type"].ToString());
			}
			return 0;
		}
		set
		{
			this.ViewState["Type"] = value;
		}
	}
	public string PrjGuid
	{
		get
		{
			object obj = this.ViewState["prjguid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["prjguid"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.LL.Text = this.FilesBind(this.MID, this.FID.ToString(), this.PrjGuid);
			this.But_UpFile.Attributes["onclick"] = string.Concat(new object[]
			{
				"javascript:return upLoadFile('",
				this.FID.ToString(),
				"',",
				this.MID,
				",'",
				this.PrjGuid,
				"');"
			});
			this.btnDel.Style.Add("display", "none");
			if (this.Type == 0)
			{
				this.But_UpFile.Width = 0;
				this.But_UpFile.Style.Add("display", "none");
			}
		}
	}
	public void LoadCon()
	{
		this.LL.Text = this.FilesBind(this.MID, this.FID.ToString(), this.PrjGuid);
		this.But_UpFile.Attributes["onclick"] = string.Concat(new object[]
		{
			"javascript:return upLoadFile('",
			this.FID.ToString(),
			"',",
			this.MID,
			",'",
			this.PrjGuid,
			"');"
		});
		this.btnDel.Style.Add("display", "none");
	}
	public string FilesBind(int moduleID, string recordCode, string prjGuid)
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
		int num = 0;
		if (dataTable.Rows.Count > 0)
		{
			text += "<table class=\"table-none\">";
			foreach (DataRow dataRow in dataTable.Rows)
			{
				num++;
				text += "<tr><td style=\"height:19;border:solid 0px #4F92E7;\" >";
				string text2 = dataRow["AnnexCode"].ToString();
				if (this.Type != 0)
				{
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						num.ToString(),
						"、<a   href='",
						dataRow["FilePath"].ToString(),
						dataRow["AnnexName"].ToString(),
						"' target=_blank>",
						dataRow["OriginalName"].ToString(),
						"</a> <img   src='/images/Del1.gif'  title='删除附件' style=\"cursor:pointer;\" runat=\"server\"  onclick=\"delupFile('",
						text2,
						"')\" ></img>  "
					});
				}
				else
				{
					string text4 = text;
					text = string.Concat(new string[]
					{
						text4,
						num.ToString(),
						"、<a   href='",
						dataRow["FilePath"].ToString(),
						dataRow["AnnexName"].ToString(),
						"' target=_blank>",
						dataRow["OriginalName"].ToString(),
						"</a> "
					});
				}
				text += "</tr></td>";
			}
			text += "</table><br/>";
		}
		return text;
	}
	protected void Butsc_Click(object sender, EventArgs e)
	{
		this.LL.Text = this.FilesBind(this.MID, this.FID.ToString(), this.PrjGuid);
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		AnnexAction annexAction = new AnnexAction();
		Guid annexCode = new Guid(this.hdAnnxCode.Value);
		annexAction.DelAnnexTemp(annexCode);
		this.LL.Text = this.FilesBind(this.MID, this.FID.ToString(), this.PrjGuid);
	}
}


