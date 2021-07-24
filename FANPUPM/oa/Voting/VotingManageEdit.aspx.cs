using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Voting_VotingManageEdit : BasePage, IRequiresSessionState
{
	protected string State;
	protected int RecordID;

	public OAVotingInfoAction via
	{
		get
		{
			return new OAVotingInfoAction();
		}
	}
	public new string UserCode
	{
		get
		{
			object obj = this.ViewState["UserCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["UserCode"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.UserCode = this.Session["yhdm"].ToString();
		this.State = base.Request["Op"].ToString();
		this.RecordID = Convert.ToInt32(base.Request["ac"].ToString());
		if (!base.IsPostBack && base.Request["ac"] != "" && this.State == "edit")
		{
			this.setData();
		}
		this.BtnSel.Attributes["onclick"] = "javascript:selDept('" + this.UserCode + "');";
	}
	public OAVotingInfo getOVI()
	{
		return new OAVotingInfo
		{
			RecordID = this.RecordID,
			UserCode = this.Session["yhdm"].ToString(),
			RecordDate = DateTime.Now,
			Title = this.txtzdname.Text.ToString(),
			Range = this.hdnDept.Value.ToString(),
			VoteType = this.RBL.SelectedValue,
			Content = this.txtRemark.Text.ToString(),
			IsValid = "y",
			State = "0"
		};
	}
	public void setData()
	{
		new OAVotingInfo();
		DataTable list = this.via.GetList("RecordID=" + this.RecordID);
		if (list.Rows.Count > 0)
		{
			this.hdnDept.Value = list.Rows[0]["Range"].ToString();
			this.txtzdname.Text = list.Rows[0]["Title"].ToString();
			this.txtRemark.Text = list.Rows[0]["Content"].ToString();
			this.tbDept.Text = oa_Voting_VotingManageEdit.getDeptName(this.hdnDept.Value);
			this.RBL.SelectedValue = list.Rows[0]["VoteType"].ToString();
		}
	}
	protected static string getDeptName(string deptCode)
	{
		string text = "";
		int i = deptCode.IndexOf(",");
		if (i > 0)
		{
			while (i > 0)
			{
				string value = deptCode.Substring(0, i);
				text = text + oa_Voting_VotingManageEdit.getDept(Convert.ToInt32(value)) + ",";
				deptCode = deptCode.Substring(i + 1, deptCode.Length - i - 1);
				i = deptCode.IndexOf(",");
			}
			text += oa_Voting_VotingManageEdit.getDept(Convert.ToInt32(deptCode));
		}
		else
		{
			text += oa_Voting_VotingManageEdit.getDept(Convert.ToInt32(deptCode));
		}
		return text;
	}
	protected static string getDept(int deptCode)
	{
		string sqlString = "select V_BMMC from pt_d_bm where i_bmdm = " + deptCode.ToString() + " and c_sfyx='y'";
		object value = publicDbOpClass.ExecuteScalar(sqlString);
		return Convert.ToString(value);
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAVotingInfo oVI = this.getOVI();
		if (this.State == "add")
		{
			int num = this.via.Add(oVI);
			if (num > 0)
			{
				this.JS.Text = "alert('添加成功!');";
				JavaScriptControl expr_42 = this.JS;
				expr_42.Text += "returnValue=true;window.close();";
			}
		}
		if (this.State == "edit")
		{
			int num = this.via.Update(oVI);
			if (num > 0)
			{
				this.JS.Text = "alert('修改成功');";
				JavaScriptControl expr_90 = this.JS;
				expr_90.Text += "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('修改失败！');";
		}
	}
}


