using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryAdjustLock : BasePage, IRequiresSessionState
{

	private SalarySAAction sia
	{
		get
		{
			return new SalarySAAction();
		}
	}
	public Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["ic"] != null)
		{
			this.InstanceCode = new Guid(base.Request["ic"]);
			this.GetPageData();
		}
	}
	protected void GetPageData()
	{
		SalarySAModel model = this.sia.GetModel(this.InstanceCode);
		userManageDb userManageDb = new userManageDb();
		this.LbEmployeeCode.Text = userManageDb.GetUserName(model.EmployeeCode);
		this.LbDeptName.Text = model.DeptName;
		this.LbDutyName.Text = model.DutyName;
		this.LbNewSalary.Text = model.NewSalary.ToString();
		this.LbOldSalary.Text = model.OldSalary.ToString();
		this.LbAdjustReason.Text = model.AdjustReason;
		this.LbRemark.Text = model.Remark;
	}
}


