using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Voting_VotingOnLine : BasePage, IRequiresSessionState
{
	protected string votetype;
	protected string rid;

	public OAVotingOptionAction ooa
	{
		get
		{
			return new OAVotingOptionAction();
		}
	}
	public OAVotingRecrodAction ora
	{
		get
		{
			return new OAVotingRecrodAction();
		}
	}
	public int RecordCode
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDCODE"].ToString());
		}
		set
		{
			this.ViewState["RECORDCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.RecordCode = Convert.ToInt32(base.Request["ac"]);
			this.BindVoteType();
		}
	}
	protected void BindRB()
	{
		DataTable list = this.ooa.GetList("VotingInfoID=" + this.RecordCode);
		this.rbl.DataTextField = "Options";
		this.rbl.DataValueField = "RecordID";
		this.rbl.DataSource = list;
		this.rbl.DataBind();
		this.rbl.Items[0].Selected = true;
	}
	protected void BindCBL()
	{
		DataTable list = this.ooa.GetList("VotingInfoID=" + this.RecordCode);
		this.cbl.DataTextField = "Options";
		this.cbl.DataValueField = "RecordID";
		this.cbl.DataSource = list;
		this.cbl.DataBind();
	}
	protected void BindVoteType()
	{
		OAVotingInfoAction oAVotingInfoAction = new OAVotingInfoAction();
		DataTable list = oAVotingInfoAction.GetList(" RecordID=" + this.RecordCode);
		if (list.Rows.Count > 0)
		{
			this.votetype = list.Rows[0]["VoteType"].ToString();
			this.labzdname.Text = list.Rows[0]["Title"].ToString();
			if (this.votetype == "0")
			{
				this.BindRB();
			}
			if (this.votetype == "1")
			{
				this.BindCBL();
			}
		}
	}
	protected OAVotingRecord GetOAVotingRecord()
	{
		OAVotingRecord oAVotingRecord = new OAVotingRecord();
		oAVotingRecord.VotingRecordID = this.RecordCode;
		oAVotingRecord.Voter = this.Session["yhdm"].ToString();
		oAVotingRecord.VoteDate = DateTime.Now;
		OAVotingInfoAction oAVotingInfoAction = new OAVotingInfoAction();
		DataTable list = oAVotingInfoAction.GetList(" RecordID=" + this.RecordCode);
		if (list.Rows.Count > 0)
		{
			this.votetype = list.Rows[0]["VoteType"].ToString();
			if (this.votetype == "0")
			{
				oAVotingRecord.SelectOptions = this.rbl.SelectedValue;
			}
			if (this.votetype == "1")
			{
				for (int i = 0; i < this.cbl.Items.Count; i++)
				{
					if (this.cbl.Items[i].Selected)
					{
						OAVotingRecord expr_DE = oAVotingRecord;
						expr_DE.SelectOptions = expr_DE.SelectOptions + this.cbl.Items[i].Value + ",";
					}
				}
			}
		}
		return oAVotingRecord;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAVotingRecord oAVotingRecord = this.GetOAVotingRecord();
		OAVotingInfoAction oAVotingInfoAction = new OAVotingInfoAction();
		DataTable list = oAVotingInfoAction.GetList(" RecordID=" + this.RecordCode);
		if (this.ora.Add(oAVotingRecord) > 0)
		{
			this.votetype = list.Rows[0]["VoteType"].ToString();
			if (this.votetype == "0")
			{
				this.rid += this.rbl.SelectedValue;
				this.ooa.UpdatePoll(" RecordID =" + Convert.ToInt32(this.rid));
			}
			if (this.votetype == "1")
			{
				for (int i = 0; i < this.cbl.Items.Count; i++)
				{
					if (this.cbl.Items[i].Selected)
					{
						this.rid = this.rid + this.cbl.Items[i].Value + ",";
					}
				}
				this.ooa.UpdatePoll(" RecordID in(" + this.rid.Substring(0, this.rid.Length - 1) + ")");
			}
			this.JS.Text = "alert('提交成功!');";
			JavaScriptControl expr_16E = this.JS;
			expr_16E.Text += "returnValue=true;window.close();";
		}
	}
}


