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
	public partial class TemplateEdit : BasePage, IRequiresSessionState
	{

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
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = Convert.ToInt32(base.Request.QueryString["tid"]);
				this.ClassID = Convert.ToInt32(base.Request.QueryString["cid"]);
				if (this.TemplateID != 0)
				{
					this.setData(this.TemplateID);
				}
			}
		}
		protected void setData(int templateId)
		{
			DataTable dataTable = ConferenceManage.QueryMeetingTemplate(templateId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTemplateName.Text = dataTable.Rows[0]["TempletName"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ClassID", this.ClassID.ToString());
			hashtable.Add("TempletName", SqlStringConstructor.GetQuotedString(this.txtTemplateName.Text));
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			if (this.TemplateID == 0)
			{
				if (ConferenceManage.AddMeetingTemplate(hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordID = " + this.TemplateID.ToString();
				if (ConferenceManage.UpdMeetingTemplate(hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


