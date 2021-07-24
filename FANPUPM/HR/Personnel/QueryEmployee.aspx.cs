using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class HR_Personnel_QueryEmployee : NBasePage, IRequiresSessionState
{
	private PTDUTYAction ptDutyAction = new PTDUTYAction();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDdl();
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = this.ptDutyAction.GetEmployees_Count(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtDepartment.Text.Trim(), this.txtPost.Text.Trim(), this.ddlEducationalBackground.Text.Trim(), this.ddlClassID.SelectedValue, this.ddlPostAndRank.Text.Trim(), this.ddlPositionLevel.SelectedValue);
		DataTable employeesData = this.ptDutyAction.GetEmployeesData(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtDepartment.Text.Trim(), this.txtPost.Text.Trim(), this.ddlEducationalBackground.Text.Trim(), this.ddlClassID.SelectedValue, this.ddlPostAndRank.Text.Trim(), this.ddlPositionLevel.SelectedValue, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvEmployee.DataSource = employeesData;
		this.gvEmployee.DataBind();
		if (employeesData.Rows.Count > 0)
		{
			this.Button2.Enabled = true;
			return;
		}
		this.Button2.Enabled = false;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void ddlPositionLevel_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = this.ptDutyAction.GetPostAndRank(this.ddlPositionLevel.SelectedValue);
		this.ddlPostAndRank.Items.Clear();
		this.ddlPostAndRank.Items.Add(new ListItem("", ""));
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			this.ddlPostAndRank.Items.Add(new ListItem(dataTable.Rows[i]["RecordID"].ToString(), dataTable.Rows[i]["PostAndRank"].ToString()));
		}
	}
	public void BindDdl()
	{
		DataTable dataTable = this.ptDutyAction.GetDdlPositionLevel();
		this.ddlPositionLevel.Items.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			string text = ConverRMB.ConvertU(System.Convert.ToInt32(dataTable.Rows[i][0].ToString())) + "级";
			this.ddlPositionLevel.Items.Add(new ListItem(text, dataTable.Rows[i][0].ToString()));
		}
		this.ddlPositionLevel.Items.Insert(0, "");
		DataTable postAndRank = this.ptDutyAction.GetPostAndRank(string.Empty);
		this.ddlPostAndRank.DataTextField = "PostAndRank";
		this.ddlPostAndRank.DataValueField = "RecordID";
		this.ddlPostAndRank.DataSource = postAndRank;
		this.ddlPostAndRank.DataBind();
		this.ddlPostAndRank.Items.Insert(0, "");
		DataTable dataSource = this.ptDutyAction.GetDdlEducationalBackground();
		this.ddlEducationalBackground.DataTextField = "educationalBackground";
		this.ddlEducationalBackground.DataValueField = "educationalBackground";
		this.ddlEducationalBackground.DataSource = dataSource;
		this.ddlEducationalBackground.DataBind();
		this.ddlEducationalBackground.Items.Insert(0, "");
		DataTable dataSource2 = this.ptDutyAction.GetDdlClassID();
		this.ddlClassID.DataTextField = "ClassName";
		this.ddlClassID.DataValueField = "ClassID";
		this.ddlClassID.DataSource = dataSource2;
		this.ddlClassID.DataBind();
		this.ddlClassID.Items.Insert(0, "");
	}
	protected void btnExp_Click1(object sender, System.EventArgs e)
	{
		this.ExportToExcel("application/ms-excel", "员工基本信息.xls");
	}
	public void ExportToExcel(string FileType, string FileName)
	{
		base.Response.Charset = "UTF8";
		base.Response.ContentEncoding = System.Text.Encoding.UTF8;
		base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
		base.Response.ContentType = FileType;
		this.EnableViewState = false;
		System.IO.StringWriter stringWriter = new System.IO.StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		DataTable employeesData = this.ptDutyAction.GetEmployeesData(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtDepartment.Text.Trim(), this.txtPost.Text.Trim(), this.ddlEducationalBackground.Text.Trim(), this.ddlClassID.SelectedValue, this.ddlPostAndRank.Text.Trim(), this.ddlPositionLevel.SelectedValue, 1, this.AspNetPager1.RecordCount);
		GridView gridView = new GridView();
		gridView.DataSource = this.FormateExportTable(employeesData);
		gridView.DataBind();
		gridView.RenderControl(writer);
		base.Response.Write(stringWriter.ToString());
		base.Response.End();
	}
	public DataTable FormateExportTable(DataTable dt)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号", typeof(string));
		dataTable.Columns.Add("员工编号", typeof(string));
		dataTable.Columns.Add("员工姓名", typeof(string));
		dataTable.Columns.Add("部门名称", typeof(string));
		dataTable.Columns.Add("岗位", typeof(string));
		dataTable.Columns.Add("入司日期", typeof(string));
		dataTable.Columns.Add("年龄", typeof(string));
		dataTable.Columns.Add("职级", typeof(string));
		dataTable.Columns.Add("职称", typeof(string));
		dataTable.Columns.Add("学历", typeof(string));
		dataTable.Columns.Add("人员类型", typeof(string));
		dataTable.Columns.Add("系统账号", typeof(string));
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["序号"] = dt.Rows[i]["Num"].ToString();
			dataRow["员工编号"] = dt.Rows[i]["UserCode"].ToString();
			dataRow["员工姓名"] = dt.Rows[i]["v_xm"].ToString();
			dataRow["部门名称"] = dt.Rows[i]["v_bmqc"].ToString();
			dataRow["岗位"] = dt.Rows[i]["RoleTypeName"].ToString();
			dataRow["入司日期"] = dt.Rows[i]["EnterCorpDate"].ToString();
			dataRow["年龄"] = dt.Rows[i]["Age"].ToString();
			dataRow["职级"] = this.ConvertWay(dt.Rows[i]["PositionLevel"].ToString());
			dataRow["职称"] = dt.Rows[i]["PostName"].ToString();
			dataRow["学历"] = dt.Rows[i]["EducationalBackground"].ToString();
			dataRow["人员类型"] = dt.Rows[i]["ClassName"].ToString();
			dataRow["系统账号"] = dt.Rows[i]["V_Dlid"].ToString();
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
	public string ConvertWay(string strPositionLevel)
	{
		if (!string.IsNullOrEmpty(strPositionLevel.Trim()))
		{
			strPositionLevel = ConverRMB.ConvertU(System.Convert.ToInt32(strPositionLevel.Trim())) + "级";
		}
		return strPositionLevel;
	}
	protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEmployee.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


