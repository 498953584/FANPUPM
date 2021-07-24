using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryAdjustEdit : BasePage, IRequiresSessionState
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
	private SalarySAAction sia
	{
		get
		{
			return new SalarySAAction();
		}
	}
	private Guid rid
	{
		get
		{
			return (Guid)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["t"] != null))
		{
			this.rid = new Guid(base.Request["rid"]);
			this.Type = base.Request["t"].ToString();
			this.btnEmployeeCode.Attributes["onclick"] = "pickPerson();";
			if (this.Type != "Add")
			{
				this.GetPageData();
			}
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.sia.Add(this.getSalarySAModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sia.Update(this.getSalarySAModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalarySAModel getSalarySAModel()
	{
		return new SalarySAModel
		{
			AuditState = new int?(-1),
			EmployeeCode = this.hdnEmployeeCode.Value,
			DeptName = this.txtDeptName.Text,
			DutyName = this.txtDutyName.Text,
			OldSalary = new decimal?(Convert.ToDecimal(this.txtOldSalary.Text)),
			NewSalary = new decimal?(Convert.ToDecimal(this.txtNewSalary.Text)),
			IsConfirm = "0",
			UserCode = this.Session["yhdm"].ToString(),
			RecordDate = new DateTime?(DateTime.Now),
			AdjustReason = this.txtAdjustReason.Text,
			Remark = this.txtRemark.Text,
			RecordID = this.rid
		};
	}
	protected void GetPageData()
	{
		SalarySAModel model = this.sia.GetModel(this.rid);
		this.hdnEmployeeCode.Value = model.EmployeeCode;
		userManageDb userManageDb = new userManageDb();
		this.txtEmployeeCode.Text = userManageDb.GetUserName(model.EmployeeCode);
		this.txtDeptName.Text = model.DeptName;
		this.txtDutyName.Text = model.DutyName;
		this.txtNewSalary.Text = model.NewSalary.ToString();
		this.txtOldSalary.Text = model.OldSalary.ToString();
		this.txtAdjustReason.Text = model.AdjustReason;
		this.txtRemark.Text = model.Remark;
	}
	protected void btnEmployeeCode_Click(object sender, EventArgs e)
	{
		DataTable dataTable = this.sia.PersonnelInfo(this.hdnEmployeeCode.Value.ToString());
		if (dataTable.Rows.Count > 0)
		{
			this.txtDeptName.Text = dataTable.Rows[0]["bmmc"].ToString();
			this.txtDutyName.Text = dataTable.Rows[0]["DutyName"].ToString();
			this.txtEmployeeCode.Text = dataTable.Rows[0]["v_xm"].ToString();
			this.txtOldSalary.Text = this.sia.OldSalary(this.hdnEmployeeCode.Value.ToString()).ToString();
		}
	}
}


