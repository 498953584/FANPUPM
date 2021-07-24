using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Modify_ApplyAdd : NBasePage, IRequiresSessionState
{
	protected string verId;
	protected string type;
	protected string progressId;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.verId = base.Request["verId"];
		this.type = base.Request["type"];
		this.progressId = base.Request["progressId"];
		if (!base.IsPostBack)
		{
			this.BindData();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = this.txtModifyVersionCode.Text.Trim();
		bool flag = false;
		if (string.Compare(this.type, "add", true) == 0)
		{
			flag = this.IsExist(this.progressId, text);
			if (!flag)
			{
				this.AddApply();
			}
		}
		else
		{
			string text2 = this.ViewState["versionCode"].ToString();
			if (!text2.Equals(text))
			{
				flag = this.IsExist(this.progressId, text);
			}
			if (!flag)
			{
				this.EditApply();
			}
		}
		if (flag)
		{
			this.Warning();
			return;
		}
		base.RegisterScript("top.ui.tabSuccess({ parentName: '_ApplyAdd' });");
	}
	protected bool IsExist(string progressId, string versionCode)
	{
		return cn.justwin.BLL.ProgressManagement.Version.CheckVersionCode(progressId, versionCode);
	}
	protected void Warning()
	{
		base.RegisterScript("alert('调整计划版本号重复，请重新录入');$('#txtModifyVersionCode').focus();");
	}
	protected void DisplayGantt(string verId)
	{
		this.divGantt.Visible = true;
		this.divApply.Visible = !this.divGantt.Visible;
		this.divFooter.Visible = !this.divGantt.Visible;
		base.RegisterScript("loadGantt('" + verId + "')");
	}
	protected string AddApply()
	{
		cn.justwin.BLL.ProgressManagement.Version byId = cn.justwin.BLL.ProgressManagement.Version.GetById(this.verId);
		byId.InputDate = System.Convert.ToDateTime(this.txtModifyDate.Text);
		byId.InputUser = this.hfldUserCode.Value;
		byId.Note = this.txtModifyNote.Text.Trim();
		byId.VersionCode = this.txtModifyVersionCode.Text.Trim();
		byId.VersionName = this.txtModifyProgressName.Text.Trim();
		return byId.AddModifyApply(byId);
	}
	protected void EditApply()
	{
		string name = this.txtModifyProgressName.Text.Trim();
		string versionCode = this.txtModifyVersionCode.Text.Trim();
		string note = this.txtModifyNote.Text.Trim();
		string userCode = this.hfldUserCode.Value;
		if (this.txtModifyUser.Value == "")
		{
			userCode = string.Empty;
		}
		cn.justwin.BLL.ProgressManagement.Version.Update(this.verId, name, versionCode, userCode, note);
	}
	protected void BindData()
	{
		DataTable modifyInfo = cn.justwin.BLL.ProgressManagement.Version.GetModifyInfo(this.verId);
		DataRow dataRow = modifyInfo.Rows[0];
		if (string.Compare(this.type, "edit", true) == 0)
		{
			this.txtOldProgressName.Text = dataRow["PVersionName"].ToString();
			this.txtOldVersionCode.Text = dataRow["PVersionCode"].ToString();
			this.txtModifyProgressName.Text = dataRow["VersionName"].ToString();
			this.txtModifyUser.Value = dataRow["UserName"].ToString();
			this.hfldUserCode.Value = dataRow["UserCode"].ToString();
			this.txtModifyVersionCode.Text = dataRow["VersionCode"].ToString();
			this.txtModifyNote.Text = dataRow["Note"].ToString();
			this.txtModifyDate.Text = Common2.GetTime(dataRow["InputDate"]);
			this.ViewState["versionCode"] = this.txtModifyVersionCode.Text;
			return;
		}
		this.txtOldProgressName.Text = dataRow["VersionName"].ToString();
		this.txtOldVersionCode.Text = dataRow["VersionCode"].ToString();
		this.txtModifyDate.Text = System.DateTime.Now.ToString("yyyy-M-d");
		this.txtModifyUser.Value = PageHelper.QueryUser(this, base.UserCode);
		this.hfldUserCode.Value = base.UserCode;
	}
}


