using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Fund.AccounPayout.BLL;
using cn.justwin.Fund.AccounPayout.Model;
using cn.justwin.Fund.RequestPayment.BLL;
using cn.justwin.Fund.RequestPayment.Model;
using cn.justwin.stockBLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayout_AccountPayoutView : NBasePage, IRequiresSessionState
{
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private AccounPayoutModel accountModel;
	private AccounPayoutBLL AccountBll = new AccounPayoutBLL();
	private RequestPayMain RPBLL = new RequestPayMain();
	private RequestPayMainModel RPModel;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnAccountID.Value = base.Request.QueryString["ic"].ToString();
			this.accountModel = this.AccountBll.GetModel(new Guid(this.hdnAccountID.Value));
			this.lblcode.Text = this.accountModel.PayOutCode.ToString();
			this.lblInDate.Text = Convert.ToDateTime(this.accountModel.PayOutTime).ToString("yyyy-MM-dd");
			this.lblInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.accountModel.PayOutPeople);
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.accountModel.prjGuid);
			if (modelByPrjGuid != null)
			{
				this.lblProject.Text = modelByPrjGuid.PrjName;
			}
			this.lblPayMoney.Text = this.AccountBll.getMoneyByPayCode(this.accountModel.RPGuid.ToString()).ToString();
			PayoutPayment payoutPayment = new PayoutPayment();
			PayoutPaymentModel model = payoutPayment.GetModel(this.accountModel.RPGuid.ToString());
			if (model != null)
			{
				this.lblContPayCode.Text = model.PaymentCode.ToString();
				this.lblContMoney.Text = model.PaymentMoney.ToString();
			}
			else
			{
				this.lblWord.Text = "费用名称";
				string strwhere = " inDiaryId ='" + this.accountModel.RPGuid.ToString() + "' ";
				DataTable dtByWhere = OrganizationDiary.getDtByWhere(strwhere);
				if (dtByWhere.Rows.Count > 0)
				{
					if (string.IsNullOrEmpty(dtByWhere.Rows[0]["Total"].ToString()))
					{
						this.lblContMoney.Text = "0.00";
					}
					else
					{
						this.lblContMoney.Text = dtByWhere.Rows[0]["Total"].ToString();
					}
					this.lblContPayCode.Text = dtByWhere.Rows[0]["Name"].ToString();
				}
			}
			this.lblInMoney.Text = this.accountModel.PayOutMoney.ToString();
			this.lblHandler.Text = this.accountModel.Handler.ToString();
			this.lblRemark.Text = this.accountModel.Remark.ToString();
			this.lblContractName.Text = this.accountModel.ContractName.ToString();
			this.upload.InnerHtml = this.FilesBind(this.hdnAccountID.Value);
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			this.lblPrintPeople.Text = com.jwsoft.pm.entpm.action.PageHelper.QueryUser(this, base.UserCode);
			this.lblContMoney.Text = (string.IsNullOrEmpty(this.lblContMoney.Text) ? "0.00" : this.lblContMoney.Text);
			if (this.accountModel.PayOutMoney > Convert.ToDecimal(this.lblContMoney.Text) - Convert.ToDecimal(this.lblPayMoney.Text))
			{
				this.lblInMoney.Attributes.Add("style", "Color:red");
			}
			this.lbljianMoney.Text = (Convert.ToDecimal(this.lblContMoney.Text) - Convert.ToDecimal(this.lblPayMoney.Text)).ToString();
			Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
			string strwhere2 = " and AccountID='" + fund_Prj_Accoun.getAccountByPrjGuid(this.accountModel.prjGuid.ToString()) + "' ";
			DataTable accounSumInfo = fund_Prj_Accoun.getAccounSumInfo(strwhere2);
			this.lblAccountYue.Text = accounSumInfo.Rows[0]["JE"].ToString();
			if (base.Request.QueryString["see"] != null && base.Request.QueryString["see"].ToString() == "see")
			{
				this.lblAccountYue.Attributes.Add("style", "font-weight:bold");
			}
			else
			{
				this.bllProducer.Visible = false;
				this.btnPrnt.Visible = false;
				if (Convert.ToDecimal(this.lblAccountYue.Text.ToString()) < Convert.ToDecimal(this.lblInMoney.Text.ToString()))
				{
					Label expr_4D5 = this.lblAccountYue;
					expr_4D5.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本次记账金额大于账户余额！";
					this.lblAccountYue.Attributes.Add("style", "font-weight:bold;color:red");
				}
			}
			this.ShowGuideLine(this.accountModel.RPGuid.ToString());
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigurationManager.AppSettings["AccountPayOut"].ToString();
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
	private void ShowGuideLine(string paymentId)
	{
		try
		{
			if (!ContractParameter.IsPaymentAlarm)
			{
				this.trSate.Visible = false;
			}
			else
			{
				DataTable paymentMoney = this.AccountBll.GetPaymentMoney(paymentId);
				if (paymentMoney.Rows.Count > 0)
				{
					DataRow dataRow = paymentMoney.Rows[0];
					decimal num = Convert.ToDecimal(dataRow["ModifiedMoney"].ToString());
					this.lblContractAmount.Text = num.ToString("0.000");
					decimal d = Convert.ToDecimal(dataRow["payoutMoney"].ToString());
					this.lblPaymentedAmount.Text = d.ToString("0.000");
					decimal num2 = Convert.ToDecimal(this.lblInMoney.Text.Trim());
					this.lblPaymentAmount.Text = num2.ToString("0.000");
					if (num != 0m)
					{
						decimal num3 = d / num;
						this.lblRate.Text = string.Format("{0:P}", num3);
						PayoutPayment payoutPayment = new PayoutPayment();
						string stateByBalanceAmount = payoutPayment.GetStateByBalanceAmount(num3);
						this.lblState.Text = stateByBalanceAmount;
						this.SetLableColor(Common2.GetColorByState(stateByBalanceAmount));
					}
					else
					{
						this.lblRate.Text = "无合同金额";
						this.lblState.Text = "高";
						this.SetLableColor(Common2.GetColorByState("高"));
					}
				}
			}
		}
		catch
		{
		}
	}
	private void SetLableColor(Color c)
	{
		this.lblState.ForeColor = c;
		this.lblContractAmount.ForeColor = c;
		this.lblPaymentedAmount.ForeColor = c;
		this.lblPaymentAmount.ForeColor = c;
		this.lblRate.ForeColor = c;
	}
}


