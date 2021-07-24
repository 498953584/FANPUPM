using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.RequestPayment.BLL;
using cn.justwin.Fund.RequestPayment.Model;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_RequestPayment_RequestPaymentView : NBasePage, IRequiresSessionState
{
	private RequestPayMain mainBll = new RequestPayMain();
	private RequestPayMainModel mainModel;
	private RequestPayDetail detailBll = new RequestPayDetail();
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private string ic = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.ic = base.Request["ic"];
			this.hdnCode.Value = base.Request.QueryString["ic"].ToString();
		}
		this.mainModel = this.mainBll.GetModel(new Guid(this.hdnCode.Value.ToString()));
		this.lblExplain.Text = this.mainModel.ReMark.ToString();
		this.lbluser.Text = PageHelper.QueryUser(this, this.mainModel.RPayUserCode);
		this.lblcode.Text = this.mainModel.RPayCode.ToString();
		this.DateInTime.Text = Convert.ToDateTime(this.mainModel.RPayDate).ToShortDateString();
		if (!string.IsNullOrEmpty(this.mainModel.PrjGuid.ToString()))
		{
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.mainModel.PrjGuid.ToString());
			if (modelByPrjGuid != null)
			{
				this.lblProject.Text = modelByPrjGuid.PrjName;
			}
		}
		DataTable list = this.detailBll.GetList(" RpayMainId='" + this.mainModel.RPayMainID + "'");
		this.gvwPlan.DataSource = list;
		this.gvwPlan.DataBind();
		this.flAnnx.MID = 1023;
		this.flAnnx.FID = this.ic;
		this.flAnnx.Type = 1;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		this.lblPrintPeople.Text = PageHelper.QueryUser(this, base.UserCode);
	}
	protected void gvwPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			Label label = (Label)e.Row.Cells[1].FindControl("lblPlanName");
			if (label.Text == "" || label.Text == null)
			{
				label.Text = "无计划";
				e.Row.Cells[1].Style.Add("Color", "red");
			}
			Label label2 = (Label)e.Row.Cells[4].FindControl("lblBalance");
			string value = label2.Text.ToString();
			if (Convert.ToDecimal(label2.Text.ToString()) <= 0m)
			{
				e.Row.Cells[4].Style.Add("Color", "red");
			}
			Label label3 = (Label)e.Row.Cells[5].FindControl("lblAllowMoney");
			string value2 = label3.Text.ToString();
			Label label4 = (Label)e.Row.Cells[6].FindControl("lblSurplus");
			label4.Text = (Convert.ToDecimal(value) - Convert.ToDecimal(value2)).ToString();
			Label label5 = (Label)e.Row.Cells[7].FindControl("lblRPMoney");
			if (Convert.ToDecimal(label5.Text.ToString()) - Convert.ToDecimal(label4.Text.ToString()) > 0m)
			{
				e.Row.Cells[7].Style.Add("Color", "red");
			}
		}
	}
}


