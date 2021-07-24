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
	public partial class TemplateFraeList : BasePage, IRequiresSessionState
	{
		public int templateID
		{
			get
			{
				object obj = this.ViewState["templateID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["templateID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.templateID = Convert.ToInt32(base.Request["tid"]);
				this.btnAdd.Attributes["onclick"] = "openTemplateFraeEdit('0',1,'" + this.templateID + "')";
				this.btnEdit.Attributes["onclick"] = "openTemplateFraeEdit('0',2,'" + this.templateID + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd1.Attributes["onclick"] = "openTemplateFraeEdit('1',1,'" + this.templateID + "')";
				this.btnEdit1.Attributes["onclick"] = "openTemplateFraeEdit('1',2,'" + this.templateID + "')";
				this.btnDel1.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd2.Attributes["onclick"] = "openTemplateFraeEdit('2',1,'" + this.templateID + "')";
				this.btnEdit2.Attributes["onclick"] = "openTemplateFraeEdit('2',2,'" + this.templateID + "')";
				this.btnDel2.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAdd3.Attributes["onclick"] = "openTemplateFraeEdit('3',1,'" + this.templateID + "')";
				this.btnEdit3.Attributes["onclick"] = "openTemplateFraeEdit('3',2,'" + this.templateID + "')";
				this.btnDel3.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.gvAttendMan.DataBind();
			}
		}
		protected void gvAttendMan_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvAttendMan.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
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
			this.gvAttendMan.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvAttendMan.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnTemplateID.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Templet_AttendMan]", recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvAttendMan.DataBind();
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
			int recordId = Convert.ToInt32(this.hdnTemplateID1.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Templet_Waiter]", recordId))
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
			int recordId = Convert.ToInt32(this.hdnTemplateID2.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Templet_Equipment]", recordId))
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
			int recordId = Convert.ToInt32(this.hdnTemplateID3.Value);
			if (ConferenceManage.DelTemplateFraeInfo("[OA_Meeting_Templet_Project]", recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvProject.DataBind();
			}
		}
	}


