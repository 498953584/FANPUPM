using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SendFileEdit : BasePage, IRequiresSessionState
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
				if (base.Request.QueryString["fid"] != "")
				{
					this.FileID = new Guid(base.Request["fid"]);
				}
				else
				{
					this.FileID = Guid.Empty;
				}
				if (this.FileID != Guid.Empty)
				{
					this.setData();
				}
				else
				{
					this.tr_add.Visible = true;
					this.tr_edit.Visible = false;
				}
				if (base.Request["t"] != null && base.Request["t"].ToString() == "3")
				{
					this.BtnAdd.Visible = false;
				}
			}
		}
		protected void setData()
		{
			DataTable dataTable = DocumentAction.QueryOneSendFile(this.FileID);
			if (dataTable.Rows.Count == 1)
			{
				this.txtFileNumber.Text = dataTable.Rows[0]["FileNumber"].ToString();
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.txtSignUserName.Text = dataTable.Rows[0]["SignUserName"].ToString();
				this.txtDraftDept.Text = dataTable.Rows[0]["DraftDept"].ToString();
				this.txtIssueRange.Text = dataTable.Rows[0]["IssueRange"].ToString();
				this.txtIssuePage.Text = dataTable.Rows[0]["IssuePage"].ToString();
				this.txtShareNumber.Text = dataTable.Rows[0]["ShareNumber"].ToString();
				this.txtIssueDate.Text = dataTable.Rows[0]["IssueDate"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				this.hdnFilePath.Value = dataTable.Rows[0]["FilePath"].ToString();
				this.txtOriginalName.Text = dataTable.Rows[0]["OriginalName"].ToString();
				this.annexName.InnerText = dataTable.Rows[0]["OriginalName"].ToString();
				if (this.hdnFilePath.Value.Length > 0)
				{
					this.tr_add.Visible = false;
					this.tr_edit.Visible = true;
					return;
				}
				this.tr_add.Visible = true;
				this.tr_edit.Visible = false;
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			Guid.NewGuid();
			DateTime arg_0B_0 = DateTime.Now;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("FileNumber", SqlStringConstructor.GetQuotedString(this.txtFileNumber.Text));
			hashtable.Add("SignUserName", SqlStringConstructor.GetQuotedString(this.txtSignUserName.Text));
			hashtable.Add("DraftDept", SqlStringConstructor.GetQuotedString(this.txtDraftDept.Text));
			hashtable.Add("IssueRange", SqlStringConstructor.GetQuotedString(this.txtIssueRange.Text));
			hashtable.Add("IssuePage", SqlStringConstructor.GetQuotedString(this.txtIssuePage.Text));
			hashtable.Add("ShareNumber", SqlStringConstructor.GetQuotedString(this.txtShareNumber.Text));
			hashtable.Add("IssueDate", SqlStringConstructor.GetQuotedString(this.txtIssueDate.Text));
			hashtable.Add("Remark", SqlStringConstructor.GetQuotedString(this.txtRemark.Text));
			if (this.FileUpload1.HasFile)
			{
				HttpPostedFile postedFile = this.FileUpload1.PostedFile;
				com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
				string[] array = fileUpload.Upload(postedFile, 1);
				hashtable.Add("OriginalName", SqlStringConstructor.GetQuotedString(array[0]));
				hashtable.Add("FilePath", SqlStringConstructor.GetQuotedString(array[1]));
			}
			string where = " where FileID = '" + this.FileID.ToString() + " '";
			if (DocumentAction.UpdSendFileInfo(hashtable, where))
			{
				this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败！');";
		}
	}


