using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Fund.PrjLoad;
using cn.justwin.Fund.prjReturn;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Fund.PrjLoad;
using cn.justwin.stockBLL.Fund.prjReturn;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_prjReturn_prjReturnEdit : com.jwsoft.common.baseclass.NBasePage, IRequiresSessionState
{
	private PrjLoadLogic prjLoad = new PrjLoadLogic();
	private prjReturenBLL returnBLL = new prjReturenBLL();
	private PtYhmcBll yhmcBLL = new PtYhmcBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request["LoanID"] != null && base.Request["LoanID"] != "")
			{
				this.BindLoan(base.Request["LoanID"].ToString());
				this.txtTime.Text = Common2.GetTime(DateTime.Now);
				this.txtUser.Text = this.yhmcBLL.GetModelById(base.UserCode).v_xm;
				this.txtCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.txtData.Text = Common2.GetTime(DateTime.Now);
			}
			if (base.Request["id"] != null && base.Request["id"] != "")
			{
				this.lblTitle.Text = "编辑还款单";
				this.hdfReturnId.Value = base.Request["id"].ToString();
				this.Bind();
				return;
			}
			this.hdfReturnId.Value = Guid.NewGuid().ToString();
			this.lblTitle.Text = "新增还款单";
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
		this.hdfUserCode.Value = modelById.FR_name;
		this.txtData.Text = Common2.GetTime(modelById.FR_data);
		this.txtMoney.Text = modelById.FR_Money.ToString();
		this.txtInterest.Text = modelById.FR_interest.ToString();
		this.txtDeduct.Text = modelById.FR_deduct.ToString();
		this.txtremark.Text = modelById.FR_remark.ToString();
		this.txtTime.Text = Common2.GetTime(modelById.FR_Time);
	}
	public void BindLoan(string LoanId)
	{
		Guid guid = new Guid(LoanId);
		PrjLoadModel model = this.prjLoad.GetModel(guid);
		this.txtLoanID.Text = model.LoanCode;
		this.txtLoanFund.Text = model.LoanFund.ToString();
		this.txtLoanRate.Text = string.Format("{0:N2}", model.LoanRate * 100m) + "%";
		this.txtLoanDate.Text = Common2.GetTime(model.PlanReturnDate);
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
			if (model.PlanReturnDate < DateTime.Now && model.ReturnState == 0)
			{
				this.txtLoanDate.Style.Add("color", "red");
				this.txtNoRetMoney.Style.Add("color", "red");
			}
			if (model.LoanFund - Convert.ToDecimal(sumByLoanid.Rows[0]["FR_Money"]) < 0m)
			{
				this.txtNoRetMoney.Style.Add("color", "red");
			}
		}
		this.txtPeople.Text = this.yhmcBLL.GetModelById(model.LoanMan).v_xm;
		this.hdfUserCode.Value = model.LoanMan;
		this.rdbNo.Checked = true;
		this.rdbYes.Checked = false;
		if (model.ReturnState == 0)
		{
			this.rdbNo.Checked = true;
			this.rdbYes.Checked = false;
			return;
		}
		if (model.ReturnState == 1)
		{
			this.rdbYes.Checked = true;
			this.rdbNo.Checked = false;
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		PrjReturnModel prjReturnModel = new PrjReturnModel();
		prjReturnModel.FR_Code = this.txtCode.Text;
		prjReturnModel.FR_data = new DateTime?(DateTime.Now);
		if (this.txtData.Text.ToString() != "")
		{
			prjReturnModel.FR_data = new DateTime?(Convert.ToDateTime(this.txtData.Text.ToString()));
		}
		prjReturnModel.FR_deduct = new decimal?(0m);
		if (this.txtDeduct.Text.ToString() != "")
		{
			prjReturnModel.FR_deduct = new decimal?(Convert.ToDecimal(this.txtDeduct.Text.ToString()));
		}
		prjReturnModel.FR_flowState = new int?(-1);
		prjReturnModel.FR_id = new Guid(this.hdfReturnId.Value.ToString());
		prjReturnModel.LoanID = new Guid(base.Request["LoanID"].ToString());
		prjReturnModel.FR_interest = new decimal?(0m);
		if (this.txtInterest.Text.ToString() != "")
		{
			prjReturnModel.FR_interest = new decimal?(Convert.ToDecimal(this.txtInterest.Text.ToString()));
		}
		prjReturnModel.FR_Money = new decimal?(0m);
		if (this.txtMoney.Text.ToString() != "")
		{
			prjReturnModel.FR_Money = new decimal?(Convert.ToDecimal(this.txtMoney.Text.ToString()));
		}
		prjReturnModel.FR_name = this.hdfUserCode.Value.ToString();
		prjReturnModel.FR_remark = this.txtremark.Text;
		prjReturnModel.FR_Time = new DateTime?(Convert.ToDateTime(this.txtTime.Text.ToString()));
		prjReturnModel.FR_user = base.UserCode;
		int state = 0;
		if (this.rdbNo.Checked)
		{
			state = 0;
		}
		else
		{
			if (this.rdbYes.Checked)
			{
				state = 1;
			}
		}
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			new StringBuilder();
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			if (base.Request["id"] != null && base.Request["id"] != "")
			{
				prjReturnModel.FR_Time = new DateTime?(DateTime.Now);
				int num = this.returnBLL.Update(sqlTransaction, prjReturnModel);
				if (num > 0)
				{
					int num2 = this.prjLoad.updateReturnState(sqlTransaction, new Guid(base.Request["LoanID"].ToString()), state);
					if (num2 > 0)
					{
						sqlTransaction.Commit();
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_prjreturnlist' });");
					}
					else
					{
						sqlTransaction.Rollback();
						base.RegisterScript("top.ui.alert('修改失败');");
					}
				}
				else
				{
					sqlTransaction.Rollback();
					base.RegisterScript("top.ui.alert('修改失败');");
				}
			}
			else
			{
				int num3 = this.returnBLL.Add(sqlTransaction, prjReturnModel);
				if (num3 > 0)
				{
					int num4 = this.prjLoad.updateReturnState(sqlTransaction, new Guid(base.Request["LoanID"].ToString()), state);
					if (num4 > 0)
					{
						sqlTransaction.Commit();
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_prjreturnlist' });");
					}
					else
					{
						sqlTransaction.Rollback();
						base.RegisterScript("top.ui.alert('新增失败');");
					}
				}
				else
				{
					sqlTransaction.Rollback();
					base.RegisterScript("top.ui.alert('新增失败');");
				}
			}
			sqlConnection.Close();
		}
	}
}


