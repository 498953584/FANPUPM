using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_PopedomSetup_ProjectList : BasePage, IRequiresSessionState
{

	protected string BussinessCode
	{
		get
		{
			return this.ViewState["BUSSINESSCODE"].ToString();
		}
		set
		{
			this.ViewState["BUSSINESSCODE"] = value;
		}
	}
	protected new string UserCode
	{
		get
		{
			return this.ViewState["USERCODE"].ToString();
		}
		set
		{
			this.ViewState["USERCODE"] = value;
		}
	}
	protected PrivProjectListAction ppl
	{
		get
		{
			return new PrivProjectListAction();
		}
	}
	protected DataTable PrjDt
	{
		get
		{
			return this.ppl.GetList(string.Concat(new string[]
			{
				"BussinessCode ='",
				this.BussinessCode,
				"' and Usercode = '",
				this.UserCode,
				"'"
			}));
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["bc"] != null || base.Request["uc"] != null))
		{
			this.BussinessCode = base.Request["bc"].ToString();
			this.UserCode = base.Request["uc"].ToString();
		}
	}
	protected void GVProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + 1);
			for (int i = 0; i < this.PrjDt.Rows.Count; i++)
			{
				if (dataRowView["ProjectId"].ToString() == this.PrjDt.Rows[i]["ProjectId"].ToString())
				{
					((CheckBox)e.Row.Cells[0].FindControl("CBSelect")).Checked = true;
					return;
				}
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		DataTable projectList = this.GetProjectList();
		this.ppl.Delete(this.BussinessCode, this.UserCode);
		if (this.ppl.AddList(projectList) == 1)
		{
			this.JS.Text = "alert('保存成功!');";
			this.GVProjectList.DataBind();
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	protected DataTable GetProjectList()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add(new DataColumn("BussinessCode", typeof(string)));
		dataTable.Columns.Add(new DataColumn("UserCode", typeof(string)));
		dataTable.Columns.Add(new DataColumn("ProjectID", typeof(int)));
		for (int i = 0; i < this.GVProjectList.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			DataView arg_8C_0 = (DataView)this.GVProjectList.Rows[i].DataItem;
			CheckBox checkBox = (CheckBox)this.GVProjectList.Rows[i].FindControl("CBSelect");
			if (checkBox.Checked)
			{
				dataRow["BussinessCode"] = this.BussinessCode;
				dataRow["UserCode"] = this.UserCode;
				dataRow["ProjectID"] = Convert.ToInt32(this.GVProjectList.DataKeys[i]["ProjectId"]);
				dataTable.Rows.Add(dataRow);
			}
		}
		return dataTable;
	}
}


