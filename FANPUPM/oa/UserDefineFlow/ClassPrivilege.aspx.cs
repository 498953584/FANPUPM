using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_UserDefineFlow_ClassPrivilege : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string text = base.Request["bclass"];
			if (!string.IsNullOrEmpty(text))
			{
				this.InitPage(text);
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = base.Request["bclass"];
		if (!string.IsNullOrEmpty(base.Request["bclass"]))
		{
			string[] array = this.hiddenuser.Value.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				string text2 = " delete from wf_privilege where businessclass='" + text + "'";
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = text2;
					text2 = string.Concat(new string[]
					{
						text3,
						"  insert into wf_privilege values('",
						text,
						"','",
						array[i],
						"')  "
					});
				}
				publicDbOpClass.ExecSqlString(text2);
			}
		}
		string text4 = "parent.desktop.flowclass.location='/oa/UserDefineFlow/FlowClass.aspx';";
		text4 += "alert('权限设定成功');";
		text4 += "top.frameWorkArea.window.desktop.getActive().close();";
		this.JS.Text = text4;
	}
	protected void InitPage(string businessclass)
	{
		string sqlString = "select * from WF_Business_Class where businessclass='" + businessclass + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			this.txtclassname.Text = dataTable.Rows[0]["BusinessClassName"].ToString();
		}
		string sqlString2 = "select * from wf_privilege where businessclass='" + businessclass + "'";
		DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString2);
		if (dataTable2.Rows.Count > 0)
		{
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				if (i < dataTable2.Rows.Count - 1)
				{
					HtmlInputHidden expr_94 = this.hiddenuser;
					expr_94.Value = expr_94.Value + dataTable2.Rows[i]["userlist"].ToString() + ",";
					TextBox expr_CB = this.txtuserlist;
					expr_CB.Text = expr_CB.Text + PageHelper.QueryUser(this, dataTable2.Rows[i]["userlist"].ToString()) + ",";
				}
				else
				{
					HtmlInputHidden expr_10A = this.hiddenuser;
					expr_10A.Value += dataTable2.Rows[i]["userlist"].ToString();
					TextBox expr_13C = this.txtuserlist;
					expr_13C.Text += PageHelper.QueryUser(this, dataTable2.Rows[i]["userlist"].ToString());
				}
			}
		}
	}
}


