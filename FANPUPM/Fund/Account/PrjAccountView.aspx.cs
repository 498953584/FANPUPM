using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountView : NBasePage, IRequiresSessionState
{
	private string _ic = string.Empty;
	private accountMange am = new accountMange();

	public string _Ic
	{
		get
		{
			return this._ic;
		}
		set
		{
			this._ic = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["ic"] != null && base.Request.QueryString["ic"].ToString() != "")
		{
			this._Ic = base.Request.QueryString["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.upload.InnerHtml = this.FilesBind(this._Ic);
			this.ToInitialize();
		}
	}
	private void ToInitialize()
	{
		if (this._Ic != "" && this._Ic.Length == 36)
		{
			AccounModel accounModel = new AccounModel();
			AccountLogic accountLogic = new AccountLogic();
			accounModel = accountLogic.GetModel(this._ic);
			Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
			string strwhere = " AND AccountID='" + this._ic + "'";
			DataTable accounSumInfo = fund_Prj_Accoun.getAccounSumInfo(strwhere);
			if (accounModel != null)
			{
				this.txtaccountNum.Text = accounModel.accountNum;
				this.txtacountName.Text = accounModel.acountName;
				this.txtRemark.Text = accounModel.Remark;
				this.txtinitialFund.Text = accounModel.initialFund.ToString();
				this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
				this.lblPrintPeople.Text = PageHelper.QueryUser(this, base.UserCode);
				this.lblPrj.Text = accountLogic.getPrjName(accounModel.PrjGuid);
			}
			if (accounSumInfo.Rows.Count > 0)
			{
				if (accounSumInfo.Rows[0]["UsableJE"] != null && accounSumInfo.Rows[0]["UsableJE"].ToString() != "")
				{
					this.keyongyue.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["UsableJE"].ToString()));
				}
				else
				{
					this.keyongyue.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["JE"] != null && accounSumInfo.Rows[0]["JE"].ToString() != "")
				{
					this.zhanghuyue.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["JE"].ToString()));
				}
				else
				{
					this.zhanghuyue.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["OtherIncome"] != null && accounSumInfo.Rows[0]["OtherIncome"].ToString() != "")
				{
					this.qitashouru.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["OtherIncome"].ToString()));
				}
				else
				{
					this.qitashouru.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["ContIncomeRZ"] != null && accounSumInfo.Rows[0]["ContIncomeRZ"].ToString() != "")
				{
					this.ruzhang.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["ContIncomeRZ"].ToString()));
				}
				else
				{
					this.ruzhang.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["Loan"] != null && accounSumInfo.Rows[0]["Loan"].ToString() != "")
				{
					this.jiekuan.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["Loan"].ToString()));
				}
				else
				{
					this.jiekuan.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["ReturnLoanBJ"] != null && accounSumInfo.Rows[0]["ReturnLoanBJ"].ToString() != "")
				{
					this.benjin.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["ReturnLoanBJ"].ToString()));
				}
				else
				{
					this.benjin.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["ReturnLoanOther"] != null && accounSumInfo.Rows[0]["ReturnLoanOther"] != "")
				{
					this.lixijiqita.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["ReturnLoanOther"].ToString()));
				}
				else
				{
					this.lixijiqita.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["MustPaidCont"] != null && accounSumInfo.Rows[0]["MustPaidCont"].ToString() != "")
				{
					this.YZhetonge.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["MustPaidCont"].ToString()));
				}
				else
				{
					this.YZhetonge.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["MustPaidOtherCost"] != null && accounSumInfo.Rows[0]["MustPaidOtherCost"].ToString() != "")
				{
					this.YZjianjiefeiyong.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["MustPaidOtherCost"].ToString()));
				}
				else
				{
					this.YZjianjiefeiyong.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["UnpaidCont"] != null && accounSumInfo.Rows[0]["UnpaidCont"].ToString() != "")
				{
					this.WZhetonge.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["UnpaidCont"].ToString()));
				}
				else
				{
					this.WZhetonge.Text = "0.000";
				}
				if (accounSumInfo.Rows[0]["UnpaidOtherCost"] != null && accounSumInfo.Rows[0]["UnpaidOtherCost"].ToString() != "")
				{
					this.WZjianjiefeiyong.Text = string.Format("{0:0.000}", double.Parse(accounSumInfo.Rows[0]["UnpaidOtherCost"].ToString()));
					return;
				}
				this.WZjianjiefeiyong.Text = "0.000";
			}
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigHelper.Get("PrjAccount").ToString();
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
}


