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
	public partial class BoardroomManage : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.GridView1.DataBind();
				this.GridView2.DataBind();
				this.btnArrange.Attributes["onclick"] = "javascript:return openWin('Arrange');";
				this.btnDealWith.Attributes["onclick"] = "javascript:return openWin('DealWith');";
				this.BtnCancel.Attributes["onclick"] = "javascript:if(!confirm('确认会议取消吗？取消后不能恢复！')){return false;}";
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
					"OnRecord(this);ClickRow('",
					text,
					"','",
					text2,
					"','",
					text3,
					"');this.cells[0].childNodes[0].click();"
				});
				userManageDb userManageDb = new userManageDb();
				e.Row.Cells[10].Text = userManageDb.GetUserName(((DataRowView)e.Row.DataItem)["UserCode"].ToString());
			}
		}
		protected void btnArrange_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
			this.GridView2.DataBind();
		}
		protected void btnDealWith_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
			this.GridView2.DataBind();
		}
		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.GridView2.DataBind();
		}
		protected void LbtnUntread_Click(object sender, EventArgs e)
		{
			this.SqlBoardroomApply.SelectCommand = string.Concat(new string[]
			{
				"SELECT [RecordID], [MeetingRoomID],(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as MeetingRoom,(select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as ManagerCode, [HumanNumber], [Title], [UserDate], [BeginHour], [BeginMinute], [EndHour], [EndMinute], [Content], [State],[UserCode],(case when State=-1 then '退回' when state = 0 then '未提交' when state = 1 then '未安排' when state = 2 then '已安排' end) as ApplyState FROM [OA_MeetingRoom_Apply] WHERE State = -1 AND MeetingRoomID IN (SELECT RecordID FROM OA_MeetingRoom_Info WHERE ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"')"
			});
			this.GridView1.DataBind();
		}
		protected void LbtnNotPlan_Click(object sender, EventArgs e)
		{
			this.SqlBoardroomApply.SelectCommand = string.Concat(new string[]
			{
				"SELECT [RecordID], [MeetingRoomID],(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as MeetingRoom,(select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as ManagerCode, [HumanNumber], [Title], [UserDate], [BeginHour], [BeginMinute], [EndHour], [EndMinute], [Content], [State],[UserCode],(case when State=-1 then '退回' when state = 1 then '未提交' when state = 1 then '未安排' when state = 2 then '已安排' end) as ApplyState FROM [OA_MeetingRoom_Apply] WHERE State = 1 AND MeetingRoomID IN (SELECT RecordID FROM OA_MeetingRoom_Info WHERE ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"')"
			});
			this.GridView1.DataBind();
		}
		protected void LbtnYesPlan_Click(object sender, EventArgs e)
		{
			this.SqlBoardroomApply.SelectCommand = string.Concat(new string[]
			{
				"SELECT [RecordID], [MeetingRoomID],(select MeetingRoom from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as MeetingRoom,(select ManagerCode from OA_MeetingRoom_Info where RecordID = MeetingRoomID AND ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"') as ManagerCode, [HumanNumber], [Title], [UserDate], [BeginHour], [BeginMinute], [EndHour], [EndMinute], [Content], [State],[UserCode],(case when State=-1 then '退回' when state = 1 then '未提交' when state = 1 then '未安排' when state = 2 then '已安排' end) as ApplyState FROM [OA_MeetingRoom_Apply] WHERE State = 2 AND MeetingRoomID IN (SELECT RecordID FROM OA_MeetingRoom_Info WHERE ManagerCode = '",
				this.Session["yhdm"].ToString(),
				"')"
			});
			this.GridView1.DataBind();
		}
		protected void BtnCancel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.DelBoardroom(recordId))
			{
				this.JS.Text = "alert('取消成功！');";
				this.GridView1.DataBind();
			}
		}
	}


