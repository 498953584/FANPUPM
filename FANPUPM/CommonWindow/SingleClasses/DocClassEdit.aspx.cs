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
	public partial class DocClassEdit : BasePage, IRequiresSessionState
	{

		public string ClassTypeCode
		{
			get
			{
				object obj = this.ViewState["ClassTypeCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ClassTypeCode"] = value;
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
				return string.Empty;
			}
			set
			{
				this.ViewState["UserCode"] = value;
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
		public string Types
		{
			get
			{
				object obj = this.ViewState["Types"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Types"] = value;
			}
		}
		public string PageTitle
		{
			get
			{
				object obj = this.ViewState["PageTitle"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PageTitle"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = base.Request.QueryString["code"];
				this.ClassTypeCode = base.Request.QueryString["ctc"];
				this.PageTitle = base.Request.QueryString["title"];
				this.Types = base.Request.QueryString["f"];
				this.Page.Header.Title = this.PageTitle + "维护";
				this.Td_title.InnerText = this.PageTitle;
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				if (this.ClassID != 0)
				{
					this.setData(this.ClassID);
				}
			}
		}
		protected void setData(int classId)
		{
			DataTable dataTable = DocumentAction.QueryOneDocumentClass(classId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtClassCode.Text = dataTable.Rows[0]["ClassCode"].ToString();
				this.txtClassName.Text = dataTable.Rows[0]["ClassName"].ToString();
				this.txtRemark.Text = dataTable.Rows[0]["Remark"].ToString();
				string text = dataTable.Rows[0]["IsValid"].ToString();
				if (text != "" && text != null)
				{
					this.ddltIsValid.ClearSelection();
					this.ddltIsValid.Items.FindByValue(text).Selected = true;
				}
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			string pStr = this.ddltIsValid.SelectedValue.ToString();
			string pStr2;
			if (this.Types == "0")
			{
				pStr2 = "%";
			}
			else
			{
				DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
				if (dataTable.Rows.Count == 0)
				{
					pStr2 = "%";
				}
				else
				{
					pStr2 = dataTable.Rows[0]["CorpCode"].ToString();
				}
			}
			if (string.IsNullOrEmpty(this.txtClassCode.Text.Trim()))
			{
				this.JS.Text = "alert('分类编号不允许为空！');";
				this.txtClassCode.Focus();
				return;
			}
			if (string.IsNullOrEmpty(this.txtClassName.Text.Trim()))
			{
				this.JS.Text = "alert('分类名称不允许为空！');";
				this.txtClassName.Focus();
				return;
			}
			if (this.txtRemark.Text.Trim().Length > 100)
			{
				this.JS.Text = "alert('备注长度不许超过100个字符！');";
				this.txtRemark.Focus();
				return;
			}
			if (this.ClassID == 0)
			{
				DataTable dataTable2 = DocumentAction.QueryOneDocumentClasscode(this.txtClassCode.Text.Trim(), this.ClassTypeCode);
				if (dataTable2.Rows.Count > 0)
				{
					this.JS.Text = "alert('分类编号不允许重复！');";
					this.txtClassCode.Focus();
					return;
				}
				DataTable dataTable3 = DocumentAction.QueryOneDocumentClassname(this.txtClassName.Text.Trim(), this.ClassTypeCode);
				if (dataTable3.Rows.Count > 0)
				{
					this.JS.Text = "alert('分类名称不允许重复！')";
					this.txtClassName.Focus();
					return;
				}
			}
			else
			{
				DataTable dataTable4 = DocumentAction.QueryOneDocumentClass(this.ClassID);
				if (dataTable4.Rows.Count > 0)
				{
					string b = dataTable4.Rows[0]["ClassCode"].ToString();
					string b2 = dataTable4.Rows[0]["ClassName"].ToString();
					if (this.txtClassCode.Text.Trim() != b)
					{
						DataTable dataTable5 = DocumentAction.QueryOneDocumentClasscode(this.txtClassCode.Text.Trim(), this.ClassTypeCode);
						if (dataTable5.Rows.Count > 0)
						{
							this.JS.Text = "alert('分类编号不允许重复！');";
							this.txtClassCode.Focus();
							return;
						}
					}
					if (this.txtClassName.Text.Trim() != b2)
					{
						DataTable dataTable6 = DocumentAction.QueryOneDocumentClassname(this.txtClassName.Text.Trim(), this.ClassTypeCode);
						if (dataTable6.Rows.Count > 0)
						{
							this.JS.Text = "alert('分类名称不允许重复！')";
							this.txtClassName.Focus();
							return;
						}
					}
				}
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ClassTypeCode", SqlStringConstructor.GetQuotedString(this.ClassTypeCode));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(pStr2));
			hashtable.Add("ClassCode", SqlStringConstructor.GetQuotedString(this.txtClassCode.Text.Trim()));
			hashtable.Add("ClassName", SqlStringConstructor.GetQuotedString(this.txtClassName.Text.Trim()));
			hashtable.Add("Remark", SqlStringConstructor.GetQuotedString(this.txtRemark.Text.Trim()));
			hashtable.Add("IsValid", SqlStringConstructor.GetQuotedString(pStr));
			if (this.ClassID == 0)
			{
				if (DocumentAction.AddDocClass(hashtable))
				{
					this.JS.Text = "successed('保存');";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where ClassID = '" + this.ClassID + " '";
				if (DocumentAction.UpdDocClass(hashtable, where))
				{
					this.JS.Text = "successed('保存');";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


