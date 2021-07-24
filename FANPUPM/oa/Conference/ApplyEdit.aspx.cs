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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ApplyEdit : BasePage, IRequiresSessionState
	{
		public int RecordId
		{
			get
			{
				object obj = this.ViewState["RecordId"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RecordId"] = value;
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

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				this.UserCode = this.Session["yhdm"].ToString();
				this.hdnUserCode.Value = this.Session["yhdm"].ToString();
				userManageDb userManageDb = new userManageDb();
				this.txtUserName.Text = userManageDb.GetUserName(this.Session["yhdm"].ToString());
				this.RecordId = Convert.ToInt32(base.Request.QueryString["mid"]);
				if (this.RecordId != 0)
				{
					this.setData(this.RecordId);
					this.hdnRecordId.Value = this.RecordId.ToString();
				}
				else
				{
					object obj = publicDbOpClass.QuaryMaxid("OA_MeetingRoom_Apply", "RecordID");
					if (obj == null)
					{
						this.hdnRecordId.Value = "1";
					}
					else
					{
						int num = Convert.ToInt32(obj) + 1;
						this.hdnRecordId.Value = num.ToString();
					}
				}
				this.SqlEquipment.DataBind();
				this.gvEquipment.DataBind();
				this.BtnSelect.Attributes["onclick"] = "javascript:pickEquipment()";
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryApplyInfo(recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtMeetingRoom.Text = dataTable.Rows[0]["MeetingRoom"].ToString();
				this.hdnMeetinRoomID.Value = dataTable.Rows[0]["MeetingRoomID"].ToString();
				this.txtHumanNumber.Text = dataTable.Rows[0]["HumanNumber"].ToString();
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.dbUserDate.Text = dataTable.Rows[0]["UserDate"].ToString();
				this.ddltBeginHour.SelectedValue = dataTable.Rows[0]["BeginHour"].ToString();
				this.ddltBeginMinute.SelectedValue = dataTable.Rows[0]["BeginMinute"].ToString();
				this.ddltEndHour.SelectedValue = dataTable.Rows[0]["EndHour"].ToString();
				this.ddltEndMinute.SelectedValue = dataTable.Rows[0]["EndMinute"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
				this.hdnUserCode.Value = dataTable.Rows[0]["UserCode"].ToString();
				userManageDb userManageDb = new userManageDb();
				this.txtUserName.Text = userManageDb.GetUserName(dataTable.Rows[0]["UserCode"].ToString());
				this.gvEquipment.DataBind();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("MeetingRoomID", this.hdnMeetinRoomID.Value);
			hashtable.Add("HumanNumber", this.txtHumanNumber.Text);
			hashtable.Add("Title", SqlStringConstructor.GetQuotedString(this.txtTitle.Text));
			hashtable.Add("UserCode", SqlStringConstructor.GetQuotedString(this.hdnUserCode.Value));
			hashtable.Add("UserDate", SqlStringConstructor.GetQuotedString(this.dbUserDate.Text));
			hashtable.Add("BeginHour", this.ddltBeginHour.SelectedValue.ToString());
			hashtable.Add("BeginMinute", this.ddltBeginMinute.SelectedValue.ToString());
			hashtable.Add("EndHour", this.ddltEndHour.SelectedValue.ToString());
			hashtable.Add("EndMinute", this.ddltEndMinute.SelectedValue.ToString());
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			hashtable.Add("State", "0");
			if (this.RecordId == 0)
			{
				int applyRecordId = 0;
				object obj = ConferenceManage.AddApplyInfo(hashtable);
				if (obj != null)
				{
					applyRecordId = Convert.ToInt32(obj);
				}
				if (this.UpdateApplyEquipment(applyRecordId))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue='" + applyRecordId.ToString() + "';window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordID = " + this.RecordId.ToString();
				if (ConferenceManage.UpdApplyInfo(hashtable, where) && this.UpdateApplyEquipment(this.RecordId))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue='" + this.RecordId.ToString() + "';window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
		protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Row.Attributes["onclick"] = "OnRecord(this);";
			}
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			this.SqlEquipment.DataBind();
			this.gvEquipment.DataBind();
		}
		protected void BtnSelect_Click(object sender, EventArgs e)
		{
			this.SqlEquipment.DataBind();
			this.gvEquipment.DataBind();
		}
		protected string GetSelectCommandText()
		{
			string value = this.hdnEquipmentID.Value;
			string str;
			if (value != "")
			{
				str = string.Concat(new string[]
				{
					"SELECT [RecordID], [MeetingRoomID], [EquipmentName], [Model], [Number], [Content], [IsValid] FROM [OA_MeetingRoom_Equipment] WHERE ([MeetingRoomID] = ",
					this.hdnMeetinRoomID.Value,
					") AND ([RecordID] IN (",
					value,
					")) "
				});
			}
			else
			{
				str = "SELECT [RecordID], [MeetingRoomID], [EquipmentName], [Model], [Number], [Content], [IsValid] FROM [OA_MeetingRoom_Equipment] a,[OA_MeetingRoom_ApplyDetail] b WHERE (a.[RecordID] = b.[EquipmentIRecordID]) and (b.[ApplyRecordID] = " + this.hdnRecordId.Value + ")";
			}
			return str + " order by RecordID asc ";
		}
		protected bool UpdateApplyEquipment(int applyRecordId)
		{
			int count = this.gvEquipment.Rows.Count;
			int[] array = new int[count];
			int num = 0;
			foreach (GridViewRow gridViewRow in this.gvEquipment.Rows)
			{
				array[num] = (int)this.gvEquipment.DataKeys[gridViewRow.RowIndex]["RecordID"];
				num++;
			}
			return ConferenceManage.ApplyEquipment(applyRecordId, array);
		}
	}


