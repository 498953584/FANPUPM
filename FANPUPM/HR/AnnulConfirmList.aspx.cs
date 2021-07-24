using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_AnnulConfirmList : BasePage, IRequiresSessionState
{

	private ApplicationAction AA
	{
		get
		{
			return new ApplicationAction();
		}
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BtnConfirm.Attributes["onclick"] = "openConfirm();";
		}
	}
	protected void GVConfirm_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["RecordID"].ToString() + "');";
			e.Row.Cells[0].Text = System.Convert.ToString(e.Row.RowIndex + 1);
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[1].Text = userManageDb.GetUserName(e.Row.Cells[1].Text);
			string text;
			switch (text = e.Row.Cells[2].Text)
			{
			case "1":
				e.Row.Cells[2].Text = "事假";
				break;
			case "2":
				e.Row.Cells[2].Text = "婚假";
				break;
			case "3":
				e.Row.Cells[2].Text = "年休假";
				break;
			case "4":
				e.Row.Cells[2].Text = "工伤";
				break;
			case "5":
				e.Row.Cells[2].Text = "病假";
				break;
			case "6":
				e.Row.Cells[2].Text = "产假";
				break;
			case "7":
				e.Row.Cells[2].Text = "丧假";
				break;
			}
			switch (int.Parse(dataRowView["AuditState"].ToString()))
			{
			case -1:
				e.Row.Cells[4].Text = "未启动流程";
				break;
			case 1:
				e.Row.Cells[4].Text = "已批准请假";
				break;
			}
			string text2;
			if ((text2 = e.Row.Cells[6].Text) != null)
			{
				if (text2 == "0")
				{
					e.Row.Cells[6].Text = "未销假";
					e.Row.Cells[6].Style.Add("color", "#FF3030");
					return;
				}
				if (!(text2 == "1"))
				{
					return;
				}
				e.Row.Cells[6].Text = "已销假";
			}
		}
	}
	protected void BtnConfirm_Click(object sender, System.EventArgs e)
	{
		this.GVConfirm.DataBind();
	}
	protected void GVConfirm_DataBound(object sender, System.EventArgs e)
	{
		for (int i = 0; i < this.GVConfirm.Rows.Count; i++)
		{
			if (this.GVConfirm.Rows[i].Cells[6].Text.Trim() != "已销假" && this.GVConfirm.Rows[i].Cells[3].Text != "")
			{
				System.DateTime t = System.DateTime.Parse(this.GVConfirm.Rows[i].Cells[3].Text);
				if (this.GVConfirm.DataKeys[i].Values["Days"].ToString() != "" && this.GVConfirm.DataKeys[i].Values["Days"].ToString() != null)
				{
					t.AddDays(System.Math.Ceiling(System.Convert.ToDouble(this.GVConfirm.DataKeys[i].Values["Days"].ToString())));
				}
				System.DateTime t2 = System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
				if (t < t2)
				{
					HRLeaveApplication hRLeaveApplication = new HRLeaveApplication();
					hRLeaveApplication.RecordID = (System.Guid)this.GVConfirm.DataKeys[i].Values["RecordID"];
					hRLeaveApplication.IsConfirm = "1";
					hRLeaveApplication.LeaveDays = System.Convert.ToDecimal(this.GVConfirm.DataKeys[i].Values["Days"].ToString());
					userManageDb userManageDb = new userManageDb();
					hRLeaveApplication.ConfirmUser = userManageDb.GetUserName(this.Session["yhdm"].ToString());
					this.AA.AnnulConfirmUpdate(hRLeaveApplication);
				}
			}
		}
	}
}


