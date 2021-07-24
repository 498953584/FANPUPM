using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class BoardroomEdit : BasePage, IRequiresSessionState
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
		public int MeetingroomID
		{
			get
			{
				object obj = this.ViewState["MeetingroomID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["MeetingroomID"] = value;
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
				return string.Empty;
			}
			set
			{
				this.ViewState["CorpCode"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				this.CorpCode = base.Request.QueryString["code"];
				this.RecordId = Convert.ToInt32(base.Request.QueryString["mid"]);
				if (this.RecordId != 0)
				{
					this.MeetingroomID = this.RecordId;
					this.setData();
				}
				else
				{
					this.SetCorpName(this.CorpCode);
					object obj = ConferenceManage.QueryMeetingroomID();
					if (obj == null)
					{
						this.MeetingroomID = 1;
					}
					else
					{
						this.MeetingroomID = Convert.ToInt32(obj) + 1;
					}
				}
				this.Bind();
			}
		}
		protected void setData()
		{
			DataTable dataTable = ConferenceManage.QueryOneBoardroom(this.RecordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtMeetingRoom.Text = dataTable.Rows[0]["MeetingRoom"].ToString();
				this.txtCorpCode.Text = dataTable.Rows[0]["CorpName"].ToString();
				this.hdnCorpCode.Value = dataTable.Rows[0]["CorpCode"].ToString();
				this.txtLocation.Text = dataTable.Rows[0]["Location"].ToString();
				this.txtHumanNumber.Text = dataTable.Rows[0]["humanNumber"].ToString();
				this.txtUserName.Text = dataTable.Rows[0]["ManagerName"].ToString();
				this.hdnUserCode.Value = dataTable.Rows[0]["ManagerCode"].ToString();
				this.txtRelationMode.Text = dataTable.Rows[0]["RelationMode"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void SetCorpName(string corpCode)
		{
			string sqlString = "select corpName from pt_d_corpcode where corpcode = '" + corpCode + "'";
			object value = publicDbOpClass.ExecuteScalar(sqlString);
			this.txtCorpCode.Text = Convert.ToString(value);
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.txtHumanNumber.Text != "")
			{
				num = Convert.ToInt32(this.txtHumanNumber.Text);
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("RecordID", this.MeetingroomID.ToString());
			hashtable.Add("MeetingRoom", SqlStringConstructor.GetQuotedString(this.txtMeetingRoom.Text));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(this.CorpCode.ToString()));
			hashtable.Add("Location", SqlStringConstructor.GetQuotedString(this.txtLocation.Text));
			hashtable.Add("HumanNumber", num.ToString());
			hashtable.Add("ManagerCode", SqlStringConstructor.GetQuotedString(this.hdnUserCode.Value));
			hashtable.Add("RelationMode", SqlStringConstructor.GetQuotedString(this.txtRelationMode.Text));
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			hashtable.Add("IsValid", SqlStringConstructor.GetQuotedString("y"));
			if (this.RecordId == 0)
			{
				if (ConferenceManage.AddBoardroom(hashtable) && this.Update() == 1)
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordID = " + this.RecordId.ToString();
				if (ConferenceManage.UpdBoardroom(hashtable, where) && this.Update() == 1)
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
		protected void Bind()
		{
			ConferenceInfoCollection equipment = ConferenceManage.GetEquipment(this.MeetingroomID);
			this.dgEquipment_Bind(equipment);
		}
		protected void dgEquipment_Bind(ConferenceInfoCollection dt)
		{
			this.dgEquipment.DataSource = dt;
			this.Session["DataTable"] = dt;
			this.dgEquipment.DataBind();
		}
		protected void dgEquipment_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				ConferenceInfo arg_35_0 = (ConferenceInfo)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);;clickTenderRow('" + e.Item.ItemIndex + "');";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				return;
			}
			case ListItemType.SelectedItem:
				break;
			case ListItemType.EditItem:
			{
				ConferenceInfo arg_AF_0 = (ConferenceInfo)e.Item.DataItem;
				e.Item.Attributes["onclick"] = "OnRecord(this);;clickTenderRow('" + e.Item.ItemIndex + "');";
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				break;
			}
			default:
				return;
			}
		}
		private ConferenceInfoCollection dgEquipment_GetData()
		{
			ConferenceInfoCollection conferenceInfoCollection = (ConferenceInfoCollection)this.Session["DataTable"];
			for (int i = this.dgEquipment.Items.Count - 1; i >= 0; i--)
			{
				DataGridItem dataGridItem = this.dgEquipment.Items[i];
				switch (dataGridItem.ItemType)
				{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					if (((Label)dataGridItem.Cells[2].FindControl("lbEquipmentName")).Text.ToString() != "")
					{
						conferenceInfoCollection[i].MeetingRoomID = this.MeetingroomID;
						conferenceInfoCollection[i].IsValid = "y";
						conferenceInfoCollection[i].EquipmentName = ((Label)dataGridItem.Cells[1].FindControl("lbEquipmentName")).Text;
						conferenceInfoCollection[i].Model = ((Label)dataGridItem.Cells[2].FindControl("lbModel")).Text;
						conferenceInfoCollection[i].Number = Convert.ToInt32((((Label)dataGridItem.Cells[3].FindControl("lbNumber")).Text == "") ? "0" : ((Label)dataGridItem.Cells[3].FindControl("lbNumber")).Text);
						conferenceInfoCollection[i].Content = ((Label)dataGridItem.Cells[4].FindControl("lbContent")).Text;
					}
					break;
				case ListItemType.EditItem:
					if (((TextBox)dataGridItem.Cells[2].FindControl("txtEquipmentName")).Text.ToString() != "")
					{
						conferenceInfoCollection[i].MeetingRoomID = this.MeetingroomID;
						conferenceInfoCollection[i].IsValid = "y";
						conferenceInfoCollection[i].EquipmentName = ((TextBox)dataGridItem.Cells[1].FindControl("txtEquipmentName")).Text;
						conferenceInfoCollection[i].Model = ((TextBox)dataGridItem.Cells[2].FindControl("txtModel")).Text;
						conferenceInfoCollection[i].Number = Convert.ToInt32((((TextBox)dataGridItem.Cells[3].FindControl("txtNumber")).Text == "") ? "0" : ((TextBox)dataGridItem.Cells[3].FindControl("txtNumber")).Text);
						conferenceInfoCollection[i].Content = ((TextBox)dataGridItem.Cells[4].FindControl("txtContent")).Text;
					}
					break;
				}
			}
			return conferenceInfoCollection;
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			ConferenceInfoCollection conferenceInfoCollection = this.dgEquipment_GetData();
			conferenceInfoCollection.Insert(0, new ConferenceInfo
			{
				MeetingRoomID = this.MeetingroomID,
				IsValid = "y",
				EquipmentName = string.Empty,
				Model = string.Empty,
				Number = 0,
				Content = string.Empty
			});
			this.dgEquipment.EditItemIndex = 0;
			this.dgEquipment_Bind(conferenceInfoCollection);
		}
		protected void BtnEdit_Click(object sender, EventArgs e)
		{
			this.dgEquipment.EditItemIndex = int.Parse(this.HdnSelectedIndex.Value);
			ConferenceInfoCollection dt = this.dgEquipment_GetData();
			this.dgEquipment_Bind(dt);
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			this.dgEquipment.EditItemIndex = -1;
			ConferenceInfoCollection conferenceInfoCollection = this.dgEquipment_GetData();
			conferenceInfoCollection.RemoveAt(int.Parse(this.HdnSelectedIndex.Value));
			this.dgEquipment_Bind(conferenceInfoCollection);
		}
		protected int Update()
		{
			ConferenceInfoCollection tc = this.dgEquipment_GetData();
			if (ConferenceManage.UpdateEquipment(tc, this.RecordId) == 1)
			{
				return 1;
			}
			return 0;
		}
	}


