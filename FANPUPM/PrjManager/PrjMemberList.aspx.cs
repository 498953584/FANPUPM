using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjMemberList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initialize();
			this.hfldAdjunctPath.Value = WebConfigurationManager.AppSettings["ProjectFile"];
		}
	}
	protected void Initialize()
	{
		this.bindGv();
	}
	protected void bindGv()
	{
		string prjGuid = base.Request["id"];
		string memberName = this.txtName.Text.Trim();
		string postName = this.txtPost.Text.Trim();
		string educationalBackground = this.txtEducationalBackground.Text.Trim();
		string technical = this.txtTechnical.Text.Trim();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.AspNetPager1.RecordCount = PrjMember.GetMembersCount(prjGuid, memberName, postName, educationalBackground, string.Empty, technical);
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		this.gvwPrjMembers.DataSource = PrjMember.GetMembers(prjGuid, memberName, postName, educationalBackground, string.Empty, technical, currentPageIndex, this.AspNetPager1.PageSize);
		this.gvwPrjMembers.DataBind();
	}
	protected void gvwPrjMembers_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvwPrjMembers.DataKeys[e.Row.RowIndex]["PrjMemberId"].ToString();
			e.Row.Attributes["id"] = value;
		}
	}
	protected string GetAdjunct(string recordCode)
	{
		string text = ConfigHelper.Get("ProjectFile");
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + recordCode);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
	protected void btnQueryInfo_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void btnUpdatePastPerformance_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldCheckedIds.Value;
		string arg_17_0 = this.txtMPastPerformance.Text;
		try
		{
			PrjMember.Update(new PrjMember
			{
				PrjMemberId = value,
				Post = this.txtMPost.Text,
				Technical = this.txtMTechnical.Text,
				TrainingInformation = this.txtMTrainingInformation.Text,
				PastPerformance = this.txtMPastPerformance.Text,
				PostAndCompetency = this.txtMPostAndCompetency.Text,
				EducationalBackground = this.txtMEducationalBackground.Text
			});
			this.bindGv();
		}
		catch
		{
			base.RegisterScript("top.ui.alert('更新失败');");
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldCheckedIds.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		try
		{
			foreach (string current in listFromJson)
			{
				PrjMember.DelMember(current);
			}
		}
		catch
		{
			base.RegisterScript("top.ui.alert('删除失败');");
		}
		this.bindGv();
		base.RegisterScript("top.ui.show('删除成功!');");
	}
}


