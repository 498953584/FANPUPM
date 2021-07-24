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
public partial class oa_Voting_VotingInfoList : BasePage, IRequiresSessionState
{
	private string strWhere = "";
	public OAVotingInfoAction via
	{
		get
		{
			return new OAVotingInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		bool arg_16_0 = base.IsPostBack;
		this.Bind();
	}
	private void Bind()
	{
		DataTable selfVoting = this.via.GetSelfVoting(userManageDb.GetCurrentUserInfo().UserDepartCode);
		this.GVVoting.DataSource = selfVoting;
		this.GVVoting.DataBind();
	}
	protected void GVVoting_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["State"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string[] array = dataRowView["Range"].ToString().Trim(new char[]
			{
				','
			}).Split(new char[]
			{
				','
			});
			string text = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string deptCode = array2[i];
				text = text + "," + OAVotingInfoAction.getDeptName(deptCode);
			}
			e.Row.Cells[2].Text = text.Trim(new char[]
			{
				','
			});
			switch (int.Parse(dataRowView["State"].ToString()))
			{
			case 0:
				e.Row.Cells[4].Text = "未启动";
				return;
			case 1:
				e.Row.Cells[4].Text = "已启动";
				return;
			case 2:
				e.Row.Cells[4].Text = "已停止";
				break;
			default:
				return;
			}
		}
	}
	protected void GVVoting_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVVoting.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAVotingRecrodAction oAVotingRecrodAction = new OAVotingRecrodAction();
		DataTable votingRecordID = oAVotingRecrodAction.GetVotingRecordID(" Voter ='" + this.Session["yhdm"].ToString() + "'");
		bool flag = false;
		for (int i = 0; i < votingRecordID.Rows.Count; i++)
		{
			if (this.HdnRecordCode.Value == votingRecordID.Rows[i]["VotingRecordID"].ToString())
			{
				flag = true;
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('你已投过票!');", true);
				break;
			}
		}
		if (!flag)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "OpenWin('add')", true);
		}
	}
}


