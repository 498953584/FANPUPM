using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.FundConfig;
using cn.justwin.stockBLL.Fund.FundConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class Fund_MonthPlan_checkBtnWeave : NBasePage, IRequiresSessionState
{
	private string result = "false";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["valMPID"] != null && base.Request.QueryString["valMPID"].ToString() != "")
			{
				this.result = this.checkWeave();
			}
			HttpContext.Current.Response.ContentType = "text/plain";
			HttpContext.Current.Response.Write(this.result);
			HttpContext.Current.Response.End();
		}
	}
	private string checkWeave()
	{
		configBll configBll = new configBll();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(" ParaName='BeginDate' UNION ALL ");
		stringBuilder.Append(" SELECT * FROM Fund_Config WHERE ParaName='EndDate' UNION ALL");
		stringBuilder.Append(" SELECT * FROM Fund_Config WHERE ParaName='isEndNowMonth'  ");
		IList<configModel> list = new List<configModel>();
		if (this.ViewState["list"] == null)
		{
			list = configBll.GetList(stringBuilder.ToString());
			this.ViewState["list"] = list;
		}
		else
		{
			list = (this.ViewState["list"] as IList<configModel>);
		}
		DateTime now = DateTime.Now;
		int num = 6;
		int num2 = 0;
		int num3 = 0;
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ParaName == "BeginDate")
				{
					if (list[i].ParaValue.ToString() != "")
					{
						num = int.Parse(list[i].ParaValue.ToString());
					}
				}
				else
				{
					if (list[i].ParaName == "EndDate")
					{
						if (list[i].ParaValue != null && list[i].ParaValue.ToString() != "")
						{
							num2 = int.Parse(list[i].ParaValue.ToString());
						}
					}
					else
					{
						if (list[i].ParaName == "isEndNowMonth")
						{
							num3 = int.Parse(list[i].ParaValue.ToString());
						}
					}
				}
			}
		}
		if (num3 == 1)
		{
			if (num2 > 0)
			{
				if (now.Day > num && now.Day < num2)
				{
					this.result = "true";
				}
				else
				{
					this.result = "false|不在时间段内";
				}
			}
			else
			{
				if (now.Day > num)
				{
					this.result = "true";
				}
				else
				{
					this.result = "false|不在时间段内";
				}
			}
		}
		else
		{
			num2 = DateTime.DaysInMonth(now.Year, now.Month) + num2;
			if (now.Day > num && now.Day < num2)
			{
				this.result = "true";
			}
			else
			{
				this.result = "false|不在时间段内";
			}
		}
		return this.result;
	}
}


