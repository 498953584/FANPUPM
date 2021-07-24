using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Fund.AccounIncome.BLL;
using cn.justwin.Fund.AccounIncome.Model;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm;
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
public partial class Fund_AccounIncome_AccounIncomeView : NBasePage, IRequiresSessionState
{
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private Fund_Plan_MonthMainAction PlanAction = new Fund_Plan_MonthMainAction();
	private AccounIncomeModel accountModel;
	private AccounIncome AccountBll = new AccounIncome();
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnAccountID.Value = base.Request.QueryString["ic"].ToString();
			this.accountModel = this.AccountBll.GetModel(new Guid(this.hdnAccountID.Value));
			this.lblcode.Text = this.accountModel.InCode.ToString();
			this.lblInDate.Text = Convert.ToDateTime(this.accountModel.InDate).ToString("yyyy-MM-dd");
			this.lblContractName.Text = this.accountModel.ContractName.ToString();
			this.lblInPeople.Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.accountModel.InPeople);
			this.DateGetTime.Text = Convert.ToDateTime(this.accountModel.GetDate).ToString("yyyy-MM-dd");
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(this.accountModel.PrjGuid);
			if (modelByPrjGuid != null)
			{
				this.lblProject.Text = modelByPrjGuid.PrjName;
			}
			string a;
			if ((a = this.accountModel.Subject.ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (!(a == "1"))
					{
						if (a == "2")
						{
							this.lblsubject.Text = "其他入账";
						}
					}
					else
					{
						this.lblsubject.Text = "启动资金";
					}
				}
				else
				{
					this.lblsubject.Text = "合同入账";
				}
			}
			this.hdnsubject.Value = this.accountModel.Subject.ToString();
			this.lblRemark.Text = this.accountModel.Remark.ToString();
			DataTable allNews = this.AccountBll.GetAllNews(this.accountModel.ContractID);
			if (allNews.Rows.Count > 0)
			{
				if (!string.IsNullOrEmpty(allNews.Rows[0]["PlanDate"].ToString()))
				{
					this.lblPlan.Text = Convert.ToDateTime(allNews.Rows[0]["PlanDate"].ToString()).ToString("yyyy年MM月");
					this.lblPlanMoney.Text = allNews.Rows[0]["ThisBalance"].ToString();
				}
				else
				{
					this.lblPlan.Text = "";
					this.lblPlanMoney.Text = "0.00";
				}
				this.lblContName.Text = allNews.Rows[0]["cllectionCode"].ToString();
			}
			this.lblInMoney.Text = this.accountModel.InMoney.ToString();
			this.lblGetMoney.Text = this.accountModel.GetMoney.ToString();
			this.upload.InnerHtml = this.FilesBind(this.hdnAccountID.Value);
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			this.lblPrintPeople.Text = com.jwsoft.pm.entpm.action.PageHelper.QueryUser(this, base.UserCode);
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigurationManager.AppSettings["AccounIncome"].ToString();
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


