using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_RoleProjectEdit : NBasePage, IRequiresSessionState
{
	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
	private RoleProjectAction rpa
	{
		get
		{
			return new RoleProjectAction();
		}
	}
	private int rid
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["RID"]);
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}
	protected int RoleID
	{
		get
		{
			return System.Convert.ToInt32(this.ViewState["ROLEID"]);
		}
		set
		{
			this.ViewState["ROLEID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.txtUserName.Attributes["ReadOnly"] = "true";
			if (base.Request["rid"] != null || base.Request["t"] != null || base.Request["rl"] == null)
			{
				this.rid = System.Convert.ToInt32(base.Request["rid"]);
				this.Type = base.Request["t"].ToString();
				this.RoleID = System.Convert.ToInt32(base.Request["rl"]);
				this.btnEmployeeCode.Attributes["onclick"] = "pickPerson();return false;";
				if (this.Type != "Add")
				{
					this.GetPageData();
					return;
				}
				SqlDataSource expr_F8 = this.SqlProjectList;
				expr_F8.SelectCommand += " WHERE ([i_xh] in (0))";
				this.GVProjectList.DataBind();
			}
		}
	}
	protected void BtnSave_Click(object sender, System.EventArgs e)
	{
		RoleProjectModel roleProjectModel = this.getRoleProjectModel();
		if (!(roleProjectModel.ProjectCodes.ToString().Trim() != "") || !(roleProjectModel.UserCode.ToString().Trim() != ""))
		{
			base.RegisterScript(" top.ui.alert('没有选择人员或所负责的项目为空'); \n");
			return;
		}
		if (this.Type == "Add")
		{
			if (!(this.hdnProjectCode.Value != "123456789"))
			{
				base.RegisterScript(" top.ui.alert('请选择所关联的项目，关联项目不能为空'); \n");
				return;
			}
			if (this.rpa.Add(this.getRoleProjectModel()) == 1)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_roleprojectlist'} );");
				return;
			}
			base.RegisterScript("top.ui.tabError();");
			return;
		}
		else
		{
			if (!(this.hdnProjectCode.Value != "123456789"))
			{
				base.RegisterScript(" top.ui.alert('请选择所关联的项目，关联项目不能为空'); \n");
				return;
			}
			if (this.rpa.Update(this.getRoleProjectModel()) == 1)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_roleprojectlist'} );");
				return;
			}
			base.RegisterScript("top.ui.tabError();");
			return;
		}
	}
	protected RoleProjectModel getRoleProjectModel()
	{
		return new RoleProjectModel
		{
			ProjectCodes = this.hdnProjectCode.Value,
			RoleID = new int?(this.RoleID),
			RoleProjectID = this.rid,
			UserCode = this.hdnUserCode.Value
		};
	}
	protected void GetPageData()
	{
		RoleProjectModel model = this.rpa.GetModel(this.rid);
		userManageDb userManageDb = new userManageDb();
		this.txtUserName.Text = userManageDb.GetUserName(model.UserCode);
		this.hdnUserCode.Value = model.UserCode;
		this.hdnProjectCode.Value = model.ProjectCodes;
		SqlDataSource expr_57 = this.SqlProjectList;
		expr_57.SelectCommand = expr_57.SelectCommand + " WHERE ([i_xh] in ( " + this.hdnProjectCode.Value + "))";
		this.GVProjectList.DataBind();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		if (this.DelProjectID(this.hdnProjectId.Value) == 1)
		{
			SqlDataSource expr_1A = this.SqlProjectList;
			expr_1A.SelectCommand = expr_1A.SelectCommand + " WHERE ([i_xh] in ( " + this.hdnProjectCode.Value + "))";
			this.GVProjectList.DataBind();
			this.JS.Text = "alert('删除成功！');";
			return;
		}
		this.JS.Text = "alert('删除失败！');";
	}
	protected int DelProjectID(string prjid)
	{
		string[] array = this.hdnProjectCode.Value.Split(new char[]
		{
			','
		});
		string text = "";
		int result = 0;
		if (array.Length == 1)
		{
			if (prjid == this.hdnProjectCode.Value)
			{
				this.hdnProjectCode.Value = "123456789";
				result = 1;
			}
		}
		else
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				if (prjid != text2.Trim())
				{
					text = text + text2.Trim() + ",";
					result = 1;
				}
			}
			this.hdnProjectCode.Value = text.Substring(0, text.Length - 1);
		}
		return result;
	}
	protected void GVProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
			e.Row.Attributes["id"] = (e.Row.RowIndex + 1).ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["i_xh"].ToString() + "');";
		}
	}
	protected void btnSelProject_Click(object sender, System.EventArgs e)
	{
		SqlDataSource expr_06 = this.SqlProjectList;
		expr_06.SelectCommand = expr_06.SelectCommand + " WHERE ([i_xh] in ( " + this.hdnProjectCode.Value + "))";
		this.GVProjectList.DataBind();
	}
}


