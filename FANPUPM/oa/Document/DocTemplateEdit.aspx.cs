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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DocTemplateEdit : BasePage, IRequiresSessionState
	{
		

		public int ClassID
		{
			get
			{
				object obj = this.ViewState["ClassID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ClassID"] = value;
			}
		}
		public int TemplateID
		{
			get
			{
				object obj = this.ViewState["TemplateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TemplateID"] = value;
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
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.TemplateID = Convert.ToInt32(base.Request["tid"]);
				this.UserCode = base.Request["code"];
				if (this.TemplateID != 0)
				{
					this.setData();
				}
				else
				{
					this.tr_add.Visible = true;
					this.tr_edit.Visible = false;
				}
				this.ImageBtn.Attributes["onclick"] = " return confirm('确定删除公文模板文件吗？');";
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ClassID", this.ClassID.ToString());
			hashtable.Add("TempletName", SqlStringConstructor.GetQuotedString(this.txtTemplatName.Text));
			hashtable.Add("UserCode", SqlStringConstructor.GetQuotedString(this.hdnUserCode.Value));
			hashtable.Add("UploadTime", SqlStringConstructor.GetQuotedString(this.DBoxUploadTime.Text));
			hashtable.Add("Remark", SqlStringConstructor.GetQuotedString(this.txtRemark.Text));
			if (this.txtFilePath.HasFile)
			{
				HttpPostedFile postedFile = this.txtFilePath.PostedFile;
				com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
				string[] array = fileUpload.Upload(postedFile, 1);
				hashtable.Add("OriginalName", SqlStringConstructor.GetQuotedString(array[0]));
				hashtable.Add("FilePath", SqlStringConstructor.GetQuotedString(array[1]));
			}
			if (this.TemplateID == 0)
			{
				if (DocumentAction.AddDocTemplate(hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where TempletID = '" + this.TemplateID + " '";
				if (DocumentAction.UpdDocTemplate(hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
		protected void setData()
		{
			DataTable dataTable = DocumentAction.QueryOneDocTemplate(this.TemplateID);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTemplatName.Text = dataTable.Rows[0]["TempletName"].ToString();
				this.hdnUserCode.Value = dataTable.Rows[0]["UserCode"].ToString();
				this.txtUserCode.Text = dataTable.Rows[0]["UserName"].ToString();
				this.DBoxUploadTime.Text = dataTable.Rows[0]["UploadTime"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				this.hdnFilePath.Value = dataTable.Rows[0]["FilePath"].ToString();
				this.txtOriginalName.Text = dataTable.Rows[0]["OriginalName"].ToString();
				this.annexName.InnerText = dataTable.Rows[0]["OriginalName"].ToString();
			}
			if (this.hdnFilePath.Value.Length > 0)
			{
				this.tr_add.Visible = false;
				this.tr_edit.Visible = true;
				return;
			}
			this.tr_add.Visible = true;
			this.tr_edit.Visible = false;
		}
		protected void ImageBtn_Click(object sender, ImageClickEventArgs e)
		{
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string value = this.hdnFilePath.Value;
			if (fileUpload.Delete(value))
			{
				this.tr_add.Visible = true;
				this.tr_edit.Visible = false;
				Hashtable hashtable = new Hashtable();
				hashtable.Add("OriginalName", SqlStringConstructor.GetQuotedString(""));
				hashtable.Add("FilePath", SqlStringConstructor.GetQuotedString(""));
				string where = " where TempletID = '" + this.TemplateID + " '";
				if (DocumentAction.UpdDocTemplate(hashtable, where))
				{
					this.JS.Text = "alert('模板文件删除成功！');";
					return;
				}
				this.JS.Text = "alert('模板文件删除失败！');";
			}
		}
	}


