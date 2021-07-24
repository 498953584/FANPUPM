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
public partial class HR_Salary_SalaryIncomeTaxEdit : BasePage, IRequiresSessionState
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
	private SalaryITAction sit
	{
		get
		{
			return new SalaryITAction();
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
			if (this.sit.Add(this.getSalaryIncomeTaxModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.sit.Update(this.getSalaryIncomeTaxModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected SalaryIncomeTaxModel getSalaryIncomeTaxModel()
	{
		return new SalaryIncomeTaxModel
		{
			TaxRateID = this.rid,
			LowerLimit = new int?(Convert.ToInt32(this.txtLowerLimit.Text)),
			UpperLimit = new int?(Convert.ToInt32(this.txtUpperLimit.Text)),
			Deduct = new int?(Convert.ToInt32(this.txtDeduct.Text)),
			TaxRate = new decimal?(Convert.ToDecimal(this.txtTaxRate.Text) / 100m)
		};
	}
	protected void GetPageData()
	{
		SalaryIncomeTaxModel model = this.sit.GetModel(this.rid);
		this.txtLowerLimit.Text = model.LowerLimit.ToString();
		this.txtUpperLimit.Text = model.UpperLimit.ToString();
		this.txtDeduct.Text = model.Deduct.ToString();
		this.txtTaxRate.Text = model.TaxRate.ToString();
	}
}


