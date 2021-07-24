using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_StatCountEdit : BasePage, IRequiresSessionState
{
	protected StatAction sa
	{
		get
		{
			return new StatAction();
		}
	}
	protected int bmdm
	{
		get
		{
			return Convert.ToInt32(this.ViewState["BMDM"]);
		}
		set
		{
			this.ViewState["BMDM"] = value;
		}
	}
	protected DateTime CountDate
	{
		get
		{
			return Convert.ToDateTime(this.ViewState["COUNTDATE"]);
		}
		set
		{
			this.ViewState["COUNTDATE"] = value;
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

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && (base.Request["bm"] != null | base.Request["d"] != null | base.Request["uc"] != null))
		{
			this.bmdm = Convert.ToInt32(base.Request["bm"]);
			this.CountDate = Convert.ToDateTime(base.Request["d"]);
			this.UserCode = base.Request["uc"].ToString();
			this.PageDataBind();
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.sa.Update(this.getHRLeaveStat()) == 1)
		{
			this.UpdateNewAddColumnValue();
			this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	protected HRLeaveStat getHRLeaveStat()
	{
		return new HRLeaveStat
		{
			iYear = this.CountDate.Year,
			iMonth = this.CountDate.Month,
			UserCode = this.UserCode,
			Later = Convert.ToDecimal(this.txtLater.Text),
			LeaveEarly = Convert.ToDecimal(this.txtLeaveEarly.Text),
			Holiday1 = Convert.ToDecimal(this.txtHoliday1.Text),
			Holiday2 = Convert.ToDecimal(this.txtHoliday2.Text),
			Holiday3 = Convert.ToDecimal(this.txtHoliday3.Text),
			Holiday4 = Convert.ToDecimal(this.txtHoliday4.Text),
			Holiday5 = Convert.ToDecimal(this.txtHoliday5.Text),
			Holiday6 = Convert.ToDecimal(this.txtHoliday6.Text),
			Holiday7 = Convert.ToDecimal(this.txtHoliday7.Text)
		};
	}
	protected void PageDataBind()
	{
		HRLeaveStat model = this.sa.GetModel(this.CountDate.Year, this.CountDate.Month, this.UserCode);
		this.txtLater.Text = model.Later.ToString();
		this.txtLeaveEarly.Text = model.LeaveEarly.ToString();
		this.txtHoliday1.Text = model.Holiday1.ToString();
		this.txtHoliday2.Text = model.Holiday2.ToString();
		this.txtHoliday3.Text = model.Holiday3.ToString();
		this.txtHoliday4.Text = model.Holiday4.ToString();
		this.txtHoliday5.Text = model.Holiday5.ToString();
		this.txtHoliday6.Text = model.Holiday6.ToString();
		this.txtHoliday7.Text = model.Holiday7.ToString();
		DataTable newAddColumnValue = this.GetNewAddColumnValue();
		this.txtHoliday8.Text = ((newAddColumnValue.Rows[0]["holiday8"].ToString() == "") ? "0.0" : newAddColumnValue.Rows[0]["holiday8"].ToString());
		this.txtFactDay.Text = ((newAddColumnValue.Rows[0]["factday"].ToString() == "") ? "0.0" : newAddColumnValue.Rows[0]["factday"].ToString());
	}
	protected void UpdateNewAddColumnValue()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new string[]
		{
			" Update HR_Leave_Stat set Holiday8='",
			this.txtHoliday8.Text,
			"',FactDay='",
			this.txtFactDay.Text,
			"'"
		}));
		stringBuilder.Append(" where iYear='" + this.CountDate.Year + "'").Append(string.Concat(new object[]
		{
			" and iMonth='",
			this.CountDate.Month,
			"' and usercode='",
			this.UserCode,
			"'"
		}));
		publicDbOpClass.ExecSqlString(stringBuilder.ToString());
	}
	protected DataTable GetNewAddColumnValue()
	{
		DataTable dataTable = new DataTable();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" select holiday8,factday from HR_Leave_Stat").Append(" where iYear='" + this.CountDate.Year + "'").Append(" and iMonth='" + this.CountDate.Month + "'").Append(" and usercode='" + this.UserCode + "' ");
		return publicDbOpClass.DataTableQuary(stringBuilder.ToString());
	}
}


