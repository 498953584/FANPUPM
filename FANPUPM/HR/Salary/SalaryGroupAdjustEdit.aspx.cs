using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Salary_SalaryGroupAdjustEdit : BasePage, IRequiresSessionState
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
	private SalaryGAAction sga
	{
		get
		{
			return new SalaryGAAction();
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
			userManageDb userManageDb = new userManageDb();
			this.txtUserCode.Text = userManageDb.GetUserName(base.UserCode);
			this.DBApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
			if (this.sga.Add(this.getSalaryGAModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sga.Update(this.getSalaryGAModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalaryGAModel getSalaryGAModel()
	{
		return new SalaryGAModel
		{
			AuditState = new int?(-1),
			IsConfirm = "0",
			Remark = this.txtRemark.Text,
			ApplyDate = new DateTime?(Convert.ToDateTime(this.DBApplyDate.Text)),
			UserCode = base.UserCode,
			RecordDate = new DateTime?(DateTime.Now),
			RecordID = this.rid
		};
	}
	protected void GetPageData()
	{
		SalaryGAModel model = this.sga.GetModel(this.rid);
		userManageDb userManageDb = new userManageDb();
		this.txtUserCode.Text = userManageDb.GetUserName(model.UserCode);
		this.txtRemark.Text = model.Remark;
		this.DBApplyDate.Text = model.ApplyDate.ToString();
	}
}


