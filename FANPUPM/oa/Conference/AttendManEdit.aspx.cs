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
	public partial class AttendManEdit : BasePage, IRequiresSessionState
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
		public int RecordID
		{
			get
			{
				object obj = this.ViewState["RecordID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RecordID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.TemplateID = Convert.ToInt32(base.Request.QueryString["tid"]);
				this.RecordID = Convert.ToInt32(base.Request.QueryString["rid"]);
				if (this.RecordID != 0)
				{
					this.setData(this.RecordID);
				}
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryTemplateFraeInfo("OA_Meeting_Templet_AttendMan", recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTopic.Text = dataTable.Rows[0]["Topic"].ToString();
				this.txtAttendManName.Text = dataTable.Rows[0]["AttendManNames"].ToString();
				this.hdnAttendManName.Value = dataTable.Rows[0]["AttendManCodes"].ToString();
				this.hdnName.Value = dataTable.Rows[0]["AttendManNames"].ToString();
				this.txtNumber.Text = dataTable.Rows[0]["Number"].ToString();
				this.hdnNumber.Value = dataTable.Rows[0]["Number"].ToString();
				if ((string)dataTable.Rows[0]["IsSms"] == "1")
				{
					this.ckbIsSms.Checked = true;
				}
				else
				{
					this.ckbIsSms.Checked = false;
				}
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			string value = this.hdnName.Value;
			string value2 = this.hdnNumber.Value;
			hashtable.Add("TempletID", this.TemplateID.ToString());
			hashtable.Add("Topic", SqlStringConstructor.GetQuotedString(this.txtTopic.Text));
			hashtable.Add("AttendManCodes", SqlStringConstructor.GetQuotedString(this.hdnAttendManName.Value));
			hashtable.Add("AttendManNames", SqlStringConstructor.GetQuotedString(value.ToString()));
			hashtable.Add("Number", value2.ToString());
			if (this.ckbIsSms.Checked)
			{
				hashtable.Add("IsSms", SqlStringConstructor.GetQuotedString("1"));
			}
			else
			{
				hashtable.Add("IsSms", SqlStringConstructor.GetQuotedString("0"));
			}
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			if (this.RecordID == 0)
			{
				if (ConferenceManage.AddTemplateFraeInfo("[OA_Meeting_Templet_AttendMan]", hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordID = " + this.RecordID.ToString();
				if (ConferenceManage.UpdTemplateFraeInfo("[OA_Meeting_Templet_AttendMan]", hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


