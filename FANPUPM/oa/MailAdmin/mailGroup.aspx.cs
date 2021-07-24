using ASP;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_MailAdmin_mailGroup : Page, IRequiresSessionState
{
	private MailManage mail = new MailManage();

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		string text = this.txtName.Text.ToString();
		string text2 = this.txtCrac.Text.ToString();
		string text3 = this.Session["yhdm"].ToString();
		string sqlString = string.Concat(new string[]
		{
			"select * from EMS_MaileGroup where groupName='",
			text,
			"' and userID='",
			text3,
			"'"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('改组已存在'),window.opener.location = window.opener.location</script>");
			return;
		}
		string sqlString2 = string.Concat(new string[]
		{
			"insert into EMS_MaileGroup(groupName,groupDesc,userID) values ('",
			text,
			"','",
			text2,
			"','",
			text3,
			"')"
		});
		int num = publicDbOpClass.ExecuteSQL(sqlString2);
		if (num > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('添加成功'), window .close (),window.opener.location = window.opener.location</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('添加出错'),window.opener.location = window.opener.location</script>");
	}
}


