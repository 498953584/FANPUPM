using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class pickEquipment : BasePage, IRequiresSessionState
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
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RecordId = Convert.ToInt32(base.Request.QueryString["aid"]);
				this.gvEquipment.DataBind();
				this.setChecked(this.RecordId);
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			string text = "";
			foreach (GridViewRow gridViewRow in this.gvEquipment.Rows)
			{
				CheckBox checkBox = (CheckBox)gridViewRow.Cells[0].FindControl("cbSel");
				if (checkBox.Checked)
				{
					text = text + this.gvEquipment.DataKeys[gridViewRow.RowIndex]["RecordID"].ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			this.JS.Text = "window.returnValue ='" + text + "';window.close();";
		}
		protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Row.Attributes["onclick"] = "OnRecord(this);";
			}
		}
		protected void cbSelAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			if (checkBox.Text == "全选")
			{
				foreach (GridViewRow gridViewRow in this.gvEquipment.Rows)
				{
					CheckBox checkBox2 = (CheckBox)gridViewRow.Cells[0].FindControl("cbSel");
					checkBox2.Checked = checkBox.Checked;
				}
			}
		}
		protected void setChecked(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryApplyEquipment(recordId);
			if (dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					int num = (int)dataTable.Rows[i]["EquipmentIRecordID"];
					foreach (GridViewRow gridViewRow in this.gvEquipment.Rows)
					{
						CheckBox checkBox = (CheckBox)gridViewRow.Cells[0].FindControl("cbSel");
						int num2 = (int)this.gvEquipment.DataKeys[gridViewRow.RowIndex]["RecordID"];
						if (num2 == num)
						{
							checkBox.Checked = true;
						}
						else
						{
							checkBox.Checked = false;
						}
					}
				}
			}
		}
	}


