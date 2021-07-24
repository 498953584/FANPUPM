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
public partial class HR_Salary_SalaryTempletEdit : BasePage, IRequiresSessionState
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
	private SalaryTAction sta
	{
		get
		{
			return new SalaryTAction();
		}
	}
	private int rid
	{
		get
		{
			return (int)this.ViewState["RID"];
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
			this.rid = Convert.ToInt32(base.Request["rid"]);
			this.Type = base.Request["t"].ToString();
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
			if (this.sta.Add(this.getSalaryTModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sta.Update(this.getSalaryTModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalaryTModel getSalaryTModel()
	{
		return new SalaryTModel
		{
			TempletID = this.rid,
			TempletName = this.txtTempletName.Text,
			ClassCode = this.DDLClassCode.SelectedValue,
			UserCode = this.Session["yhdm"].ToString(),
			RecordDate = new DateTime?(DateTime.Now),
			UseState = new int?(0)
		};
	}
	protected void GetPageData()
	{
		SalaryTModel model = this.sta.GetModel(this.rid);
		this.txtTempletName.Text = model.TempletName;
		this.DDLClassCode.SelectedValue = model.ClassCode;
	}
}


