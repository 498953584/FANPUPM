using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Document_MyDocumentLock : BasePage, IRequiresSessionState
{

	public Guid FileID
	{
		get
		{
			object obj = this.ViewState["FileID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["FileID"] = value;
		}
	}
	public string CorpCode
	{
		get
		{
			object obj = this.ViewState["CorpCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["CorpCode"] = value;
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
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.CorpCode = base.Request["code"];
			this.UserCode = base.Request["ucd"];
			this.FileID = new Guid(base.Request["ic"]);
			this.setData();
			this.FilesBind(this.FileID.ToString(), 1);
		}
	}
	protected void setData()
	{
		DataTable dataTable = DocumentAction.QueryOneSendFile(this.FileID);
		if (dataTable.Rows.Count == 1)
		{
			this.LbTitle.Text = dataTable.Rows[0]["Title"].ToString();
			this.LbUserName.Text = dataTable.Rows[0]["UserName"].ToString();
			this.LbRecordDate.Text = Convert.ToDateTime(dataTable.Rows[0]["RecordDate"].ToString()).ToShortDateString();
			this.LbRemark.Text = dataTable.Rows[0]["Remark"].ToString();
		}
	}
	protected void FilesBind(string recordCode, int moduleID)
	{
		this.Literal1.Text = "";
		int annexType = 0;
		AnnexAction annexAction = new AnnexAction();
		DataTable fileList = annexAction.GetFileList(recordCode, annexType, moduleID);
		foreach (DataRow dataRow in fileList.Rows)
		{
			Literal expr_47 = this.Literal1;
			string text = expr_47.Text;
			expr_47.Text = string.Concat(new string[]
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
}


