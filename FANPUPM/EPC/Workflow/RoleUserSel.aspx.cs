using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_RoleUserSel : NBasePage, IRequiresSessionState
{
	public int RoleID
	{
		get
		{
			object obj = this.ViewState["RoleID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["RoleID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.RoleID = System.Convert.ToInt32(base.Request["ri"]);
			this.txtUserCode.Attributes["ReadOnly"] = "true";
			this.Bind_Duty(this.hdnUserCode.Value);
		}
	}
	protected void GVDuty_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = System.Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.GVDuty.DataKeys[e.Row.RowIndex].Value.ToString(),
				"','",
				dataRowView["v_xm"].ToString(),
				"');"
			});
		}
	}
	protected void BtnSave_Click(object sender, System.EventArgs e)
	{
		bool flag = true;
		bool flag2 = false;
		string[] array = this.hdnUserCode.Value.Substring(0, this.hdnUserCode.Value.Length - 1).Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (!FlowRoleAction.IsSameUser(this.RoleID, array[i]))
			{
				flag = FlowRoleAction.AddUser(this.RoleID, array[i], "00");
			}
			if (!flag)
			{
				break;
			}
		}
		if (flag)
		{
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_roleuser' });");
			return;
		}
		if (flag2)
		{
			base.RegisterScript("top.ui.tabError();");
		}
	}
	private void Bind_Duty(string UserCode)
	{
		this.GVDuty.DataSource = FlowRoleAction.GetUserInfo(UserCode);
		this.GVDuty.DataBind();
	}
	protected void btnUserCode_Click(object sender, System.EventArgs e)
	{
		new userManageDb();
		string[] array = this.hdnUserCode.Value.Split(new char[]
		{
			','
		});
		string text = "";
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string str = array2[i];
			text = text + "'" + str + "',";
		}
		this.Bind_Duty(text.Substring(0, text.Length - 1));
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		this.hdnUserCode.Value = this.hdnUserCode.Value.Replace(this.hdnUserId.Value + ",", "");
		this.txtUserCode.Text = this.txtUserCode.Text.Replace(this.hdnUserName.Value + ",", "");
		string[] array = this.hdnUserCode.Value.Substring(0, this.hdnUserCode.Value.Length - 1).Split(new char[]
		{
			','
		});
		string text = "";
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string str = array2[i];
			text = text + "'" + str + "',";
		}
		this.Bind_Duty(text.Substring(0, text.Length - 1));
	}
}


