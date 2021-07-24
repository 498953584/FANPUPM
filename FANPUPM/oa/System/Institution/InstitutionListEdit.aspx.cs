using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InstitutionListEdit : BasePage, IRequiresSessionState
{
	private Guid InsGuid;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BtnSave.Attributes["onclick"] = "return ValidTXT();";
			this.txtClassName.Attributes["readonly"] = "readonly";
			if (base.Request.QueryString["op"] != null && base.Request.QueryString["op"] == "Edit")
			{
				if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"] != "")
				{
					this.Show_Data();
					this.HdnInsCode.Value = base.Request.QueryString["ic"].ToString();
				}
			}
			else
			{
				this.InsGuid = Guid.NewGuid();
				this.HdnInsCode.Value = this.InsGuid.ToString();
			}
			this.FileUpload1.RecordCode = this.InsGuid.ToString();
		}
	}
	protected void Show_Data()
	{
		string sqlString = " select * from Institutions where isvalid=1 and InsCode='" + base.Request.QueryString["ic"] + "' ";
		string sqlString2 = " select ClassName,LeveCode from InstitutionClass where LeveCode=(select ClassCode from Institutions where InsCode='" + base.Request.QueryString["ic"] + "') ";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
		if (dataTable.Rows.Count == 1)
		{
			this.txtInsName.Text = dataTable.Rows[0]["InsName"].ToString();
			this.txtInsCode.Text = dataTable.Rows[0]["UniqueCode"].ToString();
			this.txtClassName.Text = dataTable2.Rows[0]["ClassName"].ToString();
			this.HdnClassCode.Value = dataTable2.Rows[0]["LeveCode"].ToString();
			this.txtIssuDate.Text = DateTime.Parse(dataTable.Rows[0]["IssueDate"].ToString()).ToShortDateString();
			this.txtIssuPerson.Text = dataTable.Rows[0]["IssuePerson"].ToString();
			this.txtContent.Value = dataTable.Rows[0]["InsContent"].ToString();
			this.InsGuid = new Guid(base.Request.QueryString["ic"].ToString());
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		string text = this.txtContent.Value.Replace("'", "\"");
		if (base.Request.QueryString["op"] == "Add" && base.Request.QueryString["ic"] != null)
		{
			string text2 = " insert into Institutions(InsCode,ClassCode,UniqueCode,InsName,InsContent,IssuePerson,IssueDate,status,IsValid,UserCode,writedate) ";
			string text3 = text2;
			text2 = string.Concat(new string[]
			{
				text3,
				" values('",
				this.HdnInsCode.Value,
				"','",
				this.HdnClassCode.Value,
				"','",
				this.txtInsCode.Text,
				"','",
				this.txtInsName.Text,
				"','",
				text,
				"',"
			});
			string text4 = text2;
			text2 = string.Concat(new string[]
			{
				text4,
				"'",
				this.txtIssuPerson.Text,
				"','",
				(this.txtIssuDate.Text == "") ? DateTime.Now.ToShortDateString() : this.txtIssuDate.Text,
				"',-1,1,'",
				this.Session["yhdm"].ToString(),
				"','",
				DateTime.Now.ToString(),
				"') "
			});
			if (publicDbOpClass.ExecuteSQL(text2) > 0)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_InstitutionList' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('保存失败！')");
			}
		}
		if (base.Request.QueryString["op"] == "Edit" && base.Request.QueryString["ic"] != null)
		{
			string text5 = string.Concat(new string[]
			{
				" update Institutions set ClassCode='",
				this.HdnClassCode.Value,
				"',UniqueCode='",
				this.txtInsCode.Text.Trim(),
				"',InsName='",
				this.txtInsName.Text.Trim(),
				"',"
			});
			string text6 = text5;
			text5 = string.Concat(new string[]
			{
				text6,
				"InsContent='",
				text,
				"',IssuePerson='",
				this.txtIssuPerson.Text.Trim(),
				"',IssueDate='",
				this.txtIssuDate.Text,
				"' where inscode='",
				base.Request.QueryString["ic"],
				"' "
			});
			if (publicDbOpClass.ExecuteSQL(text5) > 0)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_InstitutionList' });");
				return;
			}
			base.RegisterScript("top.ui.alert('保存失败！')");
		}
	}
	protected void ClearCont()
	{
		this.txtInsName.Text = "";
		this.txtInsCode.Text = "";
		this.txtClassName.Text = "";
		this.HdnClassCode.Value = "";
		this.txtIssuPerson.Text = "";
		this.txtIssuDate.Text = "";
		this.txtContent.Value = "";
	}
}


