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
public partial class oa_Voting_VotingResults : BasePage, IRequiresSessionState
{
	public OAVotingOptionAction ooa
	{
		get
		{
			return new OAVotingOptionAction();
		}
	}
	public int RecordCord
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDCORD"].ToString());
		}
		set
		{
			this.ViewState["RECORDCORD"] = value;
		}
	}
	public int RecordID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDID"].ToString());
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			if (base.Request["ac"] != "")
			{
				this.RecordCord = Convert.ToInt32(base.Request["ac"].ToString());
				this.Hdnrid.Value = base.Request["ac"].ToString();
			}
			if (base.Request["rid"] != "")
			{
				this.RecordID = Convert.ToInt32(base.Request["rid"]);
			}
			this.Bind();
		}
		this.btnAdd.Attributes["onclick"] = "OpenWin('add');";
		this.btnEdit.Attributes["onclick"] = "OpenWin('edit');";
		this.btnDel.Attributes["onclick"] = "return confirm('确定删除投票选项吗?');";
	}
	private void Bind()
	{
		DataTable percentList = this.ooa.GetPercentList("VotingInfoID=" + this.RecordCord);
		this.GVVoting.DataSource = percentList;
		this.GVVoting.DataBind();
	}
	protected void GVVoting_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["VotingInfoID"].ToString(),
				"','",
				dataRowView["RecordID"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void GVVoting_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVVoting.PageIndex = e.NewPageIndex;
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
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.ooa.Delete(Convert.ToInt32(this.HdnRecordID.Value)) > 0)
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('删除成功!');", true);
		}
		else
		{
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "", "alert('删除失败!');", true);
		}
		this.Bind();
	}
}


