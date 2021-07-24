using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class BoardroomApply : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.GridView1.DataBind();
				this.GridView2.DataBind();
				this.btnAdd.Attributes["onclick"] = "javascript:return openWin('add');";
				this.btnEdit.Attributes["onclick"] = "javascript:return openWin('edit');";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定会议室申请信息吗？')){return false;}";
				this.Button1.Attributes["style"] = "display:none";
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
			this.GridView2.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
			this.GridView2.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.DelApplyInfo(recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.GridView1.DataBind();
				this.GridView2.DataBind();
			}
		}
		protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				e.Row.Attributes["onclick"] = "OnRecord(this);";
			}
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.Pager)
			{
				e.Row.Cells[0].Attributes["style"] = "display:none";
			}
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.GridView1.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["State"].ToString();
				string text3 = ((DataRowView)e.Row.DataItem)["ManagerCode"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"ClickRow('",
					text,
					"','",
					text2,
					"','",
					text3,
					"');document.getElementById('Button1').click();OnRecord(this);"
				});
				userManageDb userManageDb = new userManageDb();
				e.Row.Cells[12].Text = userManageDb.GetUserName(((DataRowView)e.Row.DataItem)["UserCode"].ToString());
			}
		}
		protected void btnApply_Click(object sender, EventArgs e)
		{
			int applyRecordId = 0;
			if (this.hdnRecordID.Value != "")
			{
				applyRecordId = Convert.ToInt32(this.hdnRecordID.Value);
			}
			DateTime sendTime = default(DateTime);
			sendTime = DateTime.Now;
			if (PublicInterface.SendSmsMsg(new SMSLog
			{
				SendUser = this.Session["yhdm"].ToString(),
				ReceiveUser = this.hdnMangeCode.Value.ToString(),
				Message = "会议室申请已提交，请及时安排！",
				SendTime = sendTime,
				I_XGID = applyRecordId.ToString(),
				V_LXBM = "010"
			}) == 1)
			{
				if (ConferenceManage.SetApplyState(1, applyRecordId))
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('会议室申请已提交,系统将自动通知管理员!');;", true);
				}
				else
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('会议室取消申请失败!');", true);
				}
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('发送短信消息失败！');;", true);
			}
			this.GridView1.DataBind();
		}
		protected void btnCancelApply_Click(object sender, EventArgs e)
		{
			int applyRecordId = 0;
			if (this.hdnRecordID.Value != "")
			{
				applyRecordId = Convert.ToInt32(this.hdnRecordID.Value);
			}
			DateTime dateTime = default(DateTime);
			dateTime = DateTime.Now;
			if (PublicInterface.SendSmsMsg(new SMSLog
			{
				SendUser = this.Session["yhdm"].ToString(),
				ReceiveUser = this.hdnMangeCode.Value.ToString(),
				Message = "会议室申请已取消！",
				I_XGID = applyRecordId.ToString(),
				V_LXBM = "011",
				SendTime = Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd"))
			}) == 1)
			{
				if (ConferenceManage.SetApplyState(0, applyRecordId))
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('会议室申请已取消,系统将自动通知管理员!');;", true);
				}
				else
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('会议室取消申请失败!');", true);
				}
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "alert('发送短信消息失败！');;", true);
			}
			this.GridView1.DataBind();
		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.GridView2.DataBind();
		}
	}


