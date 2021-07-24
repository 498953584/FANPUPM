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
public partial class oa_Voting_VotingManage : BasePage, IRequiresSessionState
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
		bool arg_06_0 = base.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "OpenWin('add');";
		this.btnEdit.Attributes["onclick"] = "OpenWin('edit');";
		this.btnDel.Attributes["onclick"] = "return confirm('确定删除投票项目吗?');";
		this.Bind();
	}
	private void Bind()
	{
		DataTable list = this.via.GetList(" UserCode = '" + base.UserCode + "'");
		this.GVVoting.DataSource = list;
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
	protected void btnDel_Click(object sender, EventArgs e)
	{
		OAVotingOptionAction oAVotingOptionAction = new OAVotingOptionAction();
		DataTable list = oAVotingOptionAction.GetList(" VotingInfoID =" + Convert.ToInt32(this.HdnRecordCode.Value));
		if (list.Rows.Count <= 0)
		{
			if (this.via.Delete(Convert.ToInt32(this.HdnRecordCode.Value)) > 0)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('删除成功!');", true);
			}
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('删除失败!请先删除选项!');", true);
		}
		this.Bind();
	}
	protected void btnStart_Click(object sender, EventArgs e)
	{
		OAVotingOptionAction oAVotingOptionAction = new OAVotingOptionAction();
		DataTable list = oAVotingOptionAction.GetList(" VotingInfoID =" + Convert.ToInt32(this.HdnRecordCode.Value));
		if (list.Rows.Count <= 0)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('项目不完整，启动失败!');", true);
		}
		if (list.Rows.Count > 0)
		{
			if (this.via.UpdateStart(Convert.ToInt32(this.HdnRecordCode.Value)) > 0)
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('项目启动成功!');", true);
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('项目启动失败!');", true);
			}
		}
		this.Bind();
	}
	protected void btnStop_Click(object sender, EventArgs e)
	{
		if (this.via.UpdateStop(Convert.ToInt32(this.HdnRecordCode.Value)) > 0)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('项目关闭成功!');", true);
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('项目关闭失败!');", true);
		}
		this.Bind();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.Bind();
	}
}


