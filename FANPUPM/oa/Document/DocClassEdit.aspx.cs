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
				return "";
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
				return "";
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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = base.Request["code"];
				this.ClassTypeCode = base.Request["ctc"];
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
			DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
			string pStr2;
			if (dataTable.Rows.Count == 0)
			{
				pStr2 = "%";
			}
			else
			{
				pStr2 = dataTable.Rows[0]["CorpCode"].ToString();
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("ClassTypeCode", SqlStringConstructor.GetQuotedString(this.ClassTypeCode));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(pStr2));
			hashtable.Add("ClassCode", SqlStringConstructor.GetQuotedString(this.txtClassCode.Text));
			hashtable.Add("ClassName", SqlStringConstructor.GetQuotedString(this.txtClassName.Text));
			hashtable.Add("IsValid", SqlStringConstructor.GetQuotedString(pStr));
			if (this.ClassID == 0)
			{
				if (DocumentAction.AddDocClass(hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
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
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


