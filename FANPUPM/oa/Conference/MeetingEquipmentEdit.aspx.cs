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
	public partial class MeetingEquipmentEdit : BasePage, IRequiresSessionState
	{
		public int MeetingInfoID
		{
			get
			{
				object obj = this.ViewState["MeetingInfoID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["MeetingInfoID"] = value;
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
				this.MeetingInfoID = Convert.ToInt32(base.Request.QueryString["mid"]);
				this.RecordID = Convert.ToInt32(base.Request.QueryString["rid"]);
				if (this.RecordID != 0)
				{
					this.setData(this.RecordID);
				}
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryTemplateFraeInfo("OA_Meeting_Equipment", recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtEquipmentName.Text = dataTable.Rows[0]["EquipmentName"].ToString();
				this.txtNumber.Text = dataTable.Rows[0]["Number"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			int num = 0;
			if (this.txtNumber.Text != "")
			{
				num = Convert.ToInt32(this.txtNumber.Text);
			}
			hashtable.Add("MeetingInfoID", this.MeetingInfoID.ToString());
			hashtable.Add("EquipmentName", SqlStringConstructor.GetQuotedString(this.txtEquipmentName.Text));
			hashtable.Add("Number", num.ToString());
			if (this.RecordID == 0)
			{
				if (ConferenceManage.AddTemplateFraeInfo("[OA_Meeting_Equipment]", hashtable))
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
				if (ConferenceManage.UpdTemplateFraeInfo("[OA_Meeting_Equipment]", hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


