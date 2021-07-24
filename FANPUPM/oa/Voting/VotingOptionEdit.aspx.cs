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
public partial class oa_Voting_VotingOptionEdit : BasePage, IRequiresSessionState
{
	public OAVotingOptionAction ooa
	{
		get
		{
			return new OAVotingOptionAction();
		}
	}
	public string OP
	{
		get
		{
			return this.ViewState["OP"].ToString();
		}
		set
		{
			this.ViewState["OP"] = value;
		}
	}
	public int RecordID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDID"]);
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["rid"] != "")
			{
				this.RecordID = Convert.ToInt32(base.Request["rid"].ToString());
			}
			if (base.Request["rid"] == "")
			{
				this.RecordID = 0;
			}
			this.OP = base.Request["Op"].ToString();
			if (this.OP == "edit")
			{
				this.SetData();
			}
		}
	}
	protected OAVotingOption GetVotingOption()
	{
		return new OAVotingOption
		{
			RecordID = this.RecordID,
			VotingInfoID = Convert.ToInt32(base.Request["ac"].ToString()),
			Option = this.txtzdname.Text.ToString(),
			Poll = 0,
			Remark = this.txtRemark.Text.ToString()
		};
	}
	protected void SetData()
	{
		DataTable list = this.ooa.GetList(" RecordID =" + base.Request["rid"].ToString());
		this.txtzdname.Text = list.Rows[0]["Options"].ToString();
		this.txtRemark.Text = list.Rows[0]["Remark"].ToString();
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAVotingOption votingOption = this.GetVotingOption();
		if (this.OP == "add" && this.ooa.Add(votingOption) > 0)
		{
			this.JS.Text = "alert('添加成功!');";
			JavaScriptControl expr_3E = this.JS;
			expr_3E.Text += "returnValue=true;window.close();";
		}
		if (this.OP == "edit" && this.ooa.Update(votingOption) > 0)
		{
			this.JS.Text = "alert('更新成功!');";
			JavaScriptControl expr_8A = this.JS;
			expr_8A.Text += "returnValue=true;window.close();";
		}
	}
}


