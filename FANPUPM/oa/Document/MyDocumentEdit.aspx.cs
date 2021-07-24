using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MyDocumentEdit : BasePage, IRequiresSessionState
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
		public string TemplateName
		{
			get
			{
				object obj = this.ViewState["TemplateName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TemplateName"] = value;
			}
		}
		public string NewFileName
		{
			get
			{
				object obj = this.ViewState["NewFileName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["NewFileName"] = value;
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
					this.setData();
				}
				else
				{
					this.FileID = Guid.NewGuid();
					this.txtRecordDate.Text = DateTime.Today.ToShortDateString();
				}
			}
			this.btnAnnex.Attributes["onclick"] = "javascript:if(!UpFile('" + this.FileID + "')) return false;";
		}
		protected void setData()
		{
			DataTable dataTable = DocumentAction.QueryOneSendFile(this.FileID);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.txtUserName.Text = dataTable.Rows[0]["UserName"].ToString();
				this.hdnUserCode.Value = dataTable.Rows[0]["UserCode"].ToString();
				this.txtRecordDate.Text = Convert.ToDateTime(dataTable.Rows[0]["RecordDate"].ToString()).ToShortDateString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				this.hdnFilePath.Value = dataTable.Rows[0]["FilePath"].ToString();
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			DateTime arg_05_0 = DateTime.Now;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("FileID", SqlStringConstructor.GetQuotedString(this.FileID.ToString()));
			hashtable.Add("Title", SqlStringConstructor.GetQuotedString(this.txtTitle.Text));
			hashtable.Add("Remark", SqlStringConstructor.GetQuotedString(this.txtRemark.Text));
			hashtable.Add("IsPigeonhole", SqlStringConstructor.GetQuotedString("0"));
			hashtable.Add("UserCode", SqlStringConstructor.GetQuotedString(this.Session["yhdm"].ToString()));
			hashtable.Add("RecordDate", SqlStringConstructor.GetQuotedString(this.txtRecordDate.Text.ToString()));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(this.CorpCode));
			hashtable.Add("AuditState", "-1");
			if (base.Request.QueryString["fid"] == "")
			{
				if (DocumentAction.AddSendFileInfo(hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where FileID = '" + this.FileID.ToString() + " '";
				if (DocumentAction.UpdSendFileInfo(hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


