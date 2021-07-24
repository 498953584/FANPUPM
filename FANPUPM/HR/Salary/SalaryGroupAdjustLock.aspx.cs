using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryGroupAdjustLock : BasePage, IRequiresSessionState
{

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
	private SalaryGAAction sga
	{
		get
		{
			return new SalaryGAAction();
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
		SalaryGAModel model = this.sga.GetModel(this.InstanceCode);
		userManageDb userManageDb = new userManageDb();
		this.LbUserCode.Text = userManageDb.GetUserName(model.UserCode);
		this.LbRemark.Text = model.Remark;
		this.LbApplyDate.Text = model.ApplyDate.ToString();
		SqlDataSource expr_65 = this.SqGroupAdjustDetail;
		object selectCommand = expr_65.SelectCommand;
		expr_65.SelectCommand = string.Concat(new object[]
		{
			selectCommand,
			" WHERE [RecordID] = '",
			this.InstanceCode,
			"' "
		});
		this.GVGroupAdjustDetail.DataBind();
	}
	protected void GVGroupAdjustDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[0].Text = userManageDb.GetUserName(dataRowView["EmployeeCode"].ToString());
		}
	}
}


