using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.PrjLoad;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.stockBLL.Fund.PrjLoad;
using com.jwsoft.pm.entpm.action;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjLoanView : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"].ToString() != "")
		{
			this.bindModel(base.Request.QueryString["ic"].ToString());
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigurationManager.AppSettings["PrjLoan"].ToString();
		string result;
		try
		{
			string[] files = Directory.GetFiles(base.Server.MapPath(text) + recordCode);
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + recordCode;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
	private void bindModel(string ic)
	{
		this.upload.InnerHtml = this.FilesBind(ic);
		AccountLogic accountLogic = new AccountLogic();
		string userNameByUserCode = accountLogic.GetUserNameByUserCode(this.Session["yhdm"].ToString());
		this.ltadduser.Text = userNameByUserCode;
		this.ltadddate.Text = string.Format("{0:d}", DateTime.Now);
		PrjLoadModel prjLoadModel = new PrjLoadModel();
		PrjLoadLogic prjLoadLogic = new PrjLoadLogic();
		if (ic.Length == 36)
		{
			prjLoadModel = prjLoadLogic.GetModel(new Guid(ic));
			if (prjLoadModel != null)
			{
				this.ltrprjname.Text = prjLoadModel.PrjName;
				this.ltrAccountNum.Text = prjLoadModel.LoanCode;
				this.ltrLoanCause.Text = prjLoadModel.LoanCause;
				this.ltrLoanDate.Text = string.Format("{0:d}", prjLoadModel.LoanDate);
				this.ltrLoanFund.Text = string.Format("{0:0.000}", double.Parse(prjLoadModel.LoanFund.ToString()));
				this.ltrLoanMan.Text = prjLoadModel.LoanmanName;
				this.ltrLoanRate.Text = string.Format("{0:0.00}", double.Parse(prjLoadModel.LoanRate.ToString()) * 100.0) + "%";
				this.ltrPlanReturnDate.Text = string.Format("{0:d}", prjLoadModel.PlanReturnDate);
				this.ltrFlowState.Text = Common2.GetState(prjLoadModel.FlowState.ToString());
				if (prjLoadModel.FlowState.ToString() == "1")
				{
					this.trrepayment.Style.Add("display", "");
					Fund_Loan_Repayment fund_Loan_Repayment = new Fund_Loan_Repayment();
					DataTable repaymentInfoByLLoanID = fund_Loan_Repayment.GetRepaymentInfoByLLoanID(ic);
					if (repaymentInfoByLLoanID.Rows.Count > 0)
					{
						DataRow dataRow = repaymentInfoByLLoanID.Rows[0];
						int arg_24D_0 = (int)dataRow["syday"];
						if (prjLoadModel.ReturnState == 0)
						{
							this.lbRepayment.Text = "未还清";
							this.lbRepayment.Style.Add("color", "red");
						}
						else
						{
							if (prjLoadModel.ReturnState == 1)
							{
								this.lbRepayment.Text = "已还清";
								this.lbRepayment.Style.Add("color", "#008B45");
							}
						}
					}
				}
				string text = string.Empty;
				text = prjLoadModel.Remark;
				if (!string.IsNullOrEmpty(text) && text.Length > 180)
				{
					int num = prjLoadModel.Remark.Length / 180;
					for (int i = 1; i <= num; i++)
					{
						int num2 = 180 * i;
						if (num2 == 180)
						{
							num2 = 175;
						}
						text = text.Insert(num2, "<br/>");
					}
				}
				this.ltrRemark.Text = text;
			}
		}
	}
}


