using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InstitutionView : BasePage, IRequiresSessionState
{
	private string ic = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ic = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["valid"] == "0")
			{
				base.Title = "废止制度查看";
				this.ShowData();
				return;
			}
			this.ShowData();
		}
	}
	protected void ShowData()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("SELECT I.*, IC.ReadOnly \n");
		stringBuilder.Append("FROM Institutions AS I \n");
		stringBuilder.Append("LEFT JOIN InstitutionClass AS IC ON IC.LeveCode = I.ClassCode \n");
		stringBuilder.AppendFormat("WHERE InsCode = '{0}' \n", this.ic);
		DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		if (dataTable.Rows.Count == 1)
		{
			try
			{
				this.lblInsTitle.Text = dataTable.Rows[0]["InsName"].ToString();
				this.lblInsCode.Text = dataTable.Rows[0]["UniqueCode"].ToString();
				this.lblIssuePerson.Text = dataTable.Rows[0]["IssuePerson"].ToString();
				if (dataTable.Rows[0]["IssueDate"].ToString() != "")
				{
					this.lblIssueDate.Text = DateTime.Parse(dataTable.Rows[0]["IssueDate"].ToString()).ToShortDateString();
				}
				this.ltrCont.Text = dataTable.Rows[0]["InsContent"].ToString();
				this.hfldReadOnly.Value = dataTable.Rows[0]["ReadOnly"].ToString();
			}
			catch
			{
			}
		}
		this.InitFuJian(this.ic);
		this.FilesBind(138, this.ic);
	}
	public void InitFuJian(string ID)
	{
		string text = base.Server.MapPath("/UploadFiles/Institution/" + ID);
		if (Directory.Exists(text))
		{
			this.Literal1.Text = "";
			string[] files = Directory.GetFiles(text);
			string arg_3E_0 = string.Empty;
			string text2 = string.Empty;
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string path = array[i];
				text2 = Path.GetFileName(path);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<li><a class='link' target='_blank' href='/Common/DownLoad.aspx?path=");
				stringBuilder.Append(string.Concat(new string[]
				{
					"/UploadFiles/Institution/",
					ID,
					"/",
					HttpUtility.UrlEncode(text2, Encoding.UTF8),
					"'>"
				}));
				stringBuilder.Append(text2);
				stringBuilder.Append("</a></li>");
				Literal expr_E0 = this.Literal1;
				expr_E0.Text += stringBuilder.ToString();
			}
		}
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<li><a class='link' target='_blank' href='/Common/DownLoad.aspx?path=");
			stringBuilder.Append(HttpUtility.UrlEncode(str2, Encoding.UTF8) + "'>");
			stringBuilder.Append(value);
			stringBuilder.Append("</a></li>");
			Literal expr_EB = this.Literal1;
			expr_EB.Text += stringBuilder.ToString();
		}
	}
}


