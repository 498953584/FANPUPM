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
public partial class HR_Salary_SalaryGADetailEdit : BasePage, IRequiresSessionState
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
	private SalaryGADAction sgad
	{
		get
		{
			return new SalaryGADAction();
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
	private int DetailID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["DETAILID"]);
		}
		set
		{
			this.ViewState["DETAILID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["t"] != null || base.Request["did"] != null))
		{
			this.rid = new Guid(base.Request["rid"]);
			this.DetailID = Convert.ToInt32(base.Request["did"]);
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
			if (this.sgad.Add(this.getSalaryGADModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sgad.Update(this.getSalaryGADModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalaryGADModel getSalaryGADModel()
	{
		return new SalaryGADModel
		{
			EmployeeCode = this.hdnEmployeeCode.Value,
			DeptName = this.txtDeptName.Text,
			DutyName = this.txtDutyName.Text,
			OldSalary = new decimal?(Convert.ToDecimal(this.txtOldSalary.Text)),
			NewSalary = new decimal?(Convert.ToDecimal(this.txtNewSalary.Text)),
			AdjustReason = this.txtAdjustReason.Text,
			RecordID = this.rid,
			DetailID = this.DetailID
		};
	}
	protected void GetPageData()
	{
		SalaryGADModel model = this.sgad.GetModel(this.DetailID);
		this.hdnEmployeeCode.Value = model.EmployeeCode;
		userManageDb userManageDb = new userManageDb();
		this.txtEmployeeCode.Text = userManageDb.GetUserName(model.EmployeeCode);
		this.txtDeptName.Text = model.DeptName;
		this.txtDutyName.Text = model.DutyName;
		this.txtNewSalary.Text = model.NewSalary.ToString();
		this.txtOldSalary.Text = model.OldSalary.ToString();
		this.txtAdjustReason.Text = model.AdjustReason;
	}
	protected void btnEmployeeCode_Click(object sender, EventArgs e)
	{
		SalarySAAction salarySAAction = new SalarySAAction();
		DataTable dataTable = salarySAAction.PersonnelInfo(this.hdnEmployeeCode.Value.ToString());
		if (dataTable.Rows.Count > 0)
		{
			this.txtDeptName.Text = dataTable.Rows[0]["bmmc"].ToString();
			this.txtDutyName.Text = dataTable.Rows[0]["DutyName"].ToString();
			this.txtEmployeeCode.Text = dataTable.Rows[0]["v_xm"].ToString();
		}
	}
}


