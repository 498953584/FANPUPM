using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConferenceArrangeFraeList : BasePage, IRequiresSessionState
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
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.MeetingInfoID = Convert.ToInt32(base.Request["mid"]);
				this.btnAdd.Attributes["onclick"] = "openArrangeFraeEdit('0',1,'" + this.MeetingInfoID + "')";
				this.btnEdit.Attributes["onclick"] = "openArrangeFraeEdit('0',2,'" + this.MeetingInfoID + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd1.Attributes["onclick"] = "openArrangeFraeEdit('1',1,'" + this.MeetingInfoID + "')";
				this.btnEdit1.Attributes["onclick"] = "openArrangeFraeEdit('1',2,'" + this.MeetingInfoID + "')";
				this.btnDel1.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd2.Attributes["onclick"] = "openArrangeFraeEdit('2',1,'" + this.MeetingInfoID + "')";
				this.btnEdit2.Attributes["onclick"] = "openArrangeFraeEdit('2',2,'" + this.MeetingInfoID + "')";
				this.btnDel2.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd3.Attributes["onclick"] = "openArrangeFraeEdit('3',1,'" + this.MeetingInfoID + "')";
				this.btnEdit3.Attributes["onclick"] = "openArrangeFraeEdit('3',2,'" + this.MeetingInfoID + "')";
				this.btnDel3.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.gvSubsection.DataBind();
			}
		}
		protected void gvSubsection_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvSubsection.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('0','" + str + "');";
			}
		}
		protected void gvWaiter_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvWaiter.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('1','" + str + "');";
			}
		}
		protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvEquipment.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('2','" + str + "');";
			}
		}
		protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvProject.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('3','" + str + "');";
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.gvSubsection.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvSubsection.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnMeetingInfoID.Value);
			if (ConferenceManage.DelSubsection(recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvSubsection.DataBind();
			}
		}
		protected void btnAdd1_Click(object sender, EventArgs e)
		{
			this.gvWaiter.DataBind();
		}
		protected void btnEdit1_Click(object sender, EventArgs e)
		{
			this.gvWaiter.DataBind();
		}
		protected void btnDel1_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnMeetingInfoID1.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Waiter]", recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvWaiter.DataBind();
			}
		}
		protected void btnAdd2_Click(object sender, EventArgs e)
		{
			this.gvEquipment.DataBind();
		}
		protected void btnEdit2_Click(object sender, EventArgs e)
		{
			this.gvEquipment.DataBind();
		}
		protected void btnDel2_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnMeetingInfoID2.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Equipment]", recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvEquipment.DataBind();
			}
		}
		protected void btnAdd3_Click(object sender, EventArgs e)
		{
			this.gvProject.DataBind();
		}
		protected void btnEdit3_Click(object sender, EventArgs e)
		{
			this.gvProject.DataBind();
		}
		protected void btnDel3_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnMeetingInfoID3.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Project]", recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvProject.DataBind();
			}
		}
	}


