using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.PlanSummary.BLL;
using cn.justwin.Fund.PlanSummary.Model;
using com.jwsoft.pm.entpm;
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
	private PlanSummaryMain MainModel;
	private PlanSummaryMainBLL MainBLL = new PlanSummaryMainBLL();
	private PlanSummaryDetail DetailModel;
	private PlanSummaryDetailBLL DetailBLL = new PlanSummaryDetailBLL();
	private decimal LastPlanMoney = 0.00m;
	private decimal LastActualMoney = 0.00m;
	private decimal CurrPlanMoney = 0.00m;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnAccountID.Value = base.Request.QueryString["ic"].ToString();
			this.MainModel = this.MainBLL.GetModel(new Guid(this.hdnAccountID.Value));
			if (this.MainModel.PlanType == "payout")
			{
				this.lblTitle.Text = "月支计划上报查看";
			}
			else
			{
				this.lblTitle.Text = "月收计划上报查看";
			}
			this.lblPlanName.Text = this.MainModel.PlanName.ToString();
			this.lblRemark.Text = this.MainModel.remark.ToString();
			this.lblInDate.Text = Convert.ToDateTime(this.MainModel.ReportTime).ToString("yyyy-MM-dd");
			this.lblInPeople.Text = PageHelper.QueryUser(this, this.MainModel.Reporter);
			DataTable list = this.DetailBLL.GetList(" MID ='" + this.hdnAccountID.Value + "' ");
			for (int i = 0; i < list.Rows.Count; i++)
			{
				this.LastPlanMoney += Convert.ToDecimal(list.Rows[i]["LastPlanMoney"].ToString());
				this.LastActualMoney += Convert.ToDecimal(list.Rows[i]["LastActualMoney"].ToString());
				this.CurrPlanMoney += Convert.ToDecimal(list.Rows[i]["CurrPlanMoney"].ToString());
			}
			this.gvwDetail.DataSource = list;
			this.gvwDetail.DataBind();
			this.upload.InnerHtml = this.FilesBind(this.hdnAccountID.Value);
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			this.lblPrintPeople.Text = PageHelper.QueryUser(this, base.UserCode);
		}
	}
	public string FilesBind(string recordCode)
	{
		string text = ConfigurationManager.AppSettings["PlanSummary"].ToString();
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
	protected void gvwDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwDetail.DataKeys[e.Row.RowIndex].Value.ToString();
			return;
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Attributes.Add("Style", "text-align:right;");
			e.Row.Cells[1].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[2].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[3].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[4].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[5].Attributes.Add("Style", "font-weight:bold;");
			e.Row.Cells[1].Text = "总计";
			e.Row.Cells[2].Text = this.LastPlanMoney.ToString();
			e.Row.Cells[3].Text = this.LastActualMoney.ToString();
            if (this.LastPlanMoney.ToString() == "0.000" || this.LastPlanMoney.ToString() == "0.00" || this.LastPlanMoney.ToString() == "0.0" || this.LastPlanMoney.ToString() == "0")
			{
				e.Row.Cells[4].Text = "0.00%";
			}
			else
			{
				e.Row.Cells[4].Text = Math.Round(Convert.ToDecimal(this.LastActualMoney / this.LastPlanMoney * 100m), 2).ToString() + "%";
			}
			e.Row.Cells[5].Text = this.CurrPlanMoney.ToString();
		}
	}
}


