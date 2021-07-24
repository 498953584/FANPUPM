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
	public partial class WaiterEdit : BasePage, IRequiresSessionState
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
			DataTable dataTable = ConferenceManage.QueryTemplateFraeInfo("OA_Meeting_Templet_Waiter", recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtUserName.Text = dataTable.Rows[0]["WaiterName"].ToString();
				this.hdnUserName.Value = dataTable.Rows[0]["WaiterName"].ToString();
				this.hdnUserCode.Value = dataTable.Rows[0]["WaiterCode"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			string value = this.hdnUserName.Value;
			hashtable.Add("TempletID", this.TemplateID.ToString());
			hashtable.Add("WaiterCode", SqlStringConstructor.GetQuotedString(this.hdnUserCode.Value));
			hashtable.Add("WaiterName", SqlStringConstructor.GetQuotedString(value.ToString()));
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			if (this.RecordID == 0)
			{
				if (ConferenceManage.AddTemplateFraeInfo("[OA_Meeting_Templet_Waiter]", hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordId = " + this.RecordID.ToString();
				if (ConferenceManage.UpdTemplateFraeInfo("[OA_Meeting_Templet_Waiter]", hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


