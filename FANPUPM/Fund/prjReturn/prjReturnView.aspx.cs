using ASP;
using cn.justwin.Fund.PrjLoad;
using cn.justwin.Fund.prjReturn;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Fund.PrjLoad;
using cn.justwin.stockBLL.Fund.prjReturn;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_prjReturn_prjReturnView : NBasePage, IRequiresSessionState
{
	private PrjLoadLogic prjLoad = new PrjLoadLogic();
	private prjReturenBLL returnBLL = new prjReturenBLL();
	private PtYhmcBll yhmcBLL = new PtYhmcBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdfReturnId.Value = base.Request["ic"].ToString();
			this.Bind();
			this.lblBllProducer.Text = this.yhmcBLL.GetModelById(base.UserCode).v_xm;
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		}
	}
	public void Bind()
	{
		Guid fRid = new Guid(this.hdfReturnId.Value.ToString());
		PrjReturnModel modelById = this.returnBLL.GetModelById(fRid);
		this.txtCode.Text = modelById.FR_Code;
		List<string> list = new List<string>();
		list.Add(modelById.FR_name);
		list.Add(modelById.FR_user);
		List<string> names = this.yhmcBLL.GetNames(list);
		int num = 0;
		foreach (string current in names)
		{
			if (num == 0)
			{
				this.txtPeople.Text = current;
			}
			else
			{
				if (num == 1)
				{
					this.txtUser.Text = current;
				}
			}
			num++;
		}
		this.txtData.Text = Convert.ToDateTime(modelById.FR_data).ToShortDateString();
		this.txtMoney.Text = modelById.FR_Money.ToString();
		this.txtInterest.Text = modelById.FR_interest.ToString();
		this.txtDeduct.Text = modelById.FR_deduct.ToString();
		this.txtremark.Text = modelById.FR_remark.ToString();
		this.txtTime.Text = Convert.ToDateTime(modelById.FR_Time).ToShortDateString();
		this.BindLoan(modelById.LoanID.ToString());
	}
	public void BindLoan(string LoanId)
	{
		Guid guid = new Guid(LoanId);
		PrjLoadModel model = this.prjLoad.GetModel(guid);
		this.txtLoanID.Text = model.LoanCode;
		this.txtLoanFund.Text = model.LoanFund.ToString();
		this.txtLoanRate.Text = string.Format("{0:N2}", model.LoanRate * 100m) + "%";
		this.txtLoanDate.Text = Convert.ToDateTime(model.LoanDate).ToShortDateString();
		this.txtReturnMoney.Text = "0.00";
		this.txtNoRetMoney.Text = "0.00";
		this.txtReturnInterest.Text = "0.00";
		this.txtReturnOther.Text = "0.00";
		DataTable sumByLoanid = this.returnBLL.GetSumByLoanid(guid);
		if (sumByLoanid != null && sumByLoanid.Rows.Count > 0)
		{
			this.txtReturnMoney.Text = sumByLoanid.Rows[0]["FR_Money"].ToString();
			this.txtNoRetMoney.Text = (model.LoanFund - Convert.ToDecimal(sumByLoanid.Rows[0]["FR_Money"])).ToString();
			this.txtReturnInterest.Text = sumByLoanid.Rows[0]["Interest"].ToString();
			this.txtReturnOther.Text = sumByLoanid.Rows[0]["Deduct"].ToString();
		}
		if (model.PlanReturnDate < DateTime.Now && model.ReturnState == 0)
		{
			this.txtLoanDate.Style.Add("color", "red");
			this.txtNoRetMoney.Style.Add("color", "red");
		}
		if (model.LoanFund - Convert.ToDecimal(sumByLoanid.Rows[0]["FR_Money"]) < 0m)
		{
			this.txtNoRetMoney.Style.Add("color", "red");
		}
		if (model.ReturnState == 0)
		{
			this.labType.Text = "未还清";
			this.labType.Style.Add("color", "red");
			return;
		}
		if (model.ReturnState == 1)
		{
			this.labType.Text = "已还清";
			this.labType.Style.Add("color", "#008B45");
		}
	}
}


