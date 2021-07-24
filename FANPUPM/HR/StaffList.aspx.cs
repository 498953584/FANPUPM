using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class HR_StaffList : NBasePage, IRequiresSessionState
{
	private PTDUTYAction hrAction = new PTDUTYAction();
	private PTYhmcService yhmcSer = new PTYhmcService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Tree_Bind();
			this.BindGv();
		}
	}
	protected void tvDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void BindGv()
	{
		string userCode = this.txtCode.Text.Trim();
		string userName = this.txtName.Text.Trim();
		string roleTypeName = this.txtPost.Text.Trim();
		int? state = null;
		if (!string.IsNullOrEmpty(this.ddlState.SelectedValue))
		{
			state = new int?(System.Convert.ToInt32(this.ddlState.SelectedValue));
		}
		int? bmdm = null;
		if (!string.IsNullOrEmpty(this.tvDepartment.SelectedValue.Trim()))
		{
			bmdm = new int?(System.Convert.ToInt32(this.tvDepartment.SelectedValue.Trim()));
		}
		this.AspNetPager1.RecordCount = this.yhmcSer.GetEmployeesCount(userCode, userName, roleTypeName, bmdm, state);
		this.gvEmployee.DataSource = this.yhmcSer.GetEmployees(userCode, userName, roleTypeName, bmdm, state, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvEmployee.DataBind();
	}
	private void Tree_Bind()
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Selected = true;
				this.tvDepartment.Nodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExp_Click1(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		string userCode = this.txtCode.Text.Trim();
		string userName = this.txtName.Text.Trim();
		string roleTypeName = this.txtPost.Text.Trim();
		int? state = null;
		if (!string.IsNullOrEmpty(this.ddlState.SelectedValue))
		{
			state = new int?(System.Convert.ToInt32(this.ddlState.SelectedValue));
		}
		int? bmdm = null;
		if (!string.IsNullOrEmpty(this.tvDepartment.SelectedValue.Trim()))
		{
			bmdm = new int?(System.Convert.ToInt32(this.tvDepartment.SelectedValue.Trim()));
		}
		dataTable = this.yhmcSer.GetEmployees(userCode, userName, roleTypeName, bmdm, state, 0, 0);
		dataTable = this.FormateExportTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "员工一览表", "员工一览表", "员工一览表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	public DataTable FormateExportTable(DataTable dt)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号", typeof(string));
		dataTable.Columns.Add("员工编号", typeof(string));
		dataTable.Columns.Add("员工姓名", typeof(string));
		dataTable.Columns.Add("岗位", typeof(string));
		dataTable.Columns.Add("入司日期", typeof(string));
		dataTable.Columns.Add("离职日期", typeof(string));
		dataTable.Columns.Add("在职天数", typeof(string));
		dataTable.Columns.Add("状态", typeof(string));
		dataTable.Columns.Add("联系电话", typeof(string));
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = dt.Rows[i]["Num"].ToString();
			dataRow["员工编号"] = dt.Rows[i]["UserCode"].ToString();
			dataRow["员工姓名"] = dt.Rows[i]["v_xm"].ToString();
			dataRow["岗位"] = dt.Rows[i]["RoleTypeName"].ToString();
			dataRow["入司日期"] = System.Convert.ToDateTime(dt.Rows[i]["EnterCorpDate"]).ToString("yyyy-MM-dd");
			if (dt.Rows[i]["LeaveDate"] != System.DBNull.Value)
			{
				dataRow["离职日期"] = System.Convert.ToDateTime(dt.Rows[i]["LeaveDate"]).ToString("yyyy-MM-dd");
			}
			else
			{
				dataRow["离职日期"] = "";
			}
			dataRow["在职天数"] = dt.Rows[i]["Workingdates"].ToString();
			dataRow["状态"] = dt.Rows[i]["StateName"].ToString();
			dataRow["联系电话"] = dt.Rows[i]["MobilePhoneCode"].ToString();
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
	protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEmployee.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


