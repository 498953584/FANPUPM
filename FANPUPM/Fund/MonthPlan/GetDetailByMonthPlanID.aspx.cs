using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
public partial class Fund_MonthPlan_GetDetailByMonthPlanID : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string s = string.Empty;
			if (base.Request.Form["mpid"] != null && base.Request.Form["mpid"].ToString() != "" && base.Request.Form["mpid"].ToString().Length == 36)
			{
				s = this.GetMonthPlanList(base.Request.Form["mpid"].ToString());
			}
			HttpContext.Current.Response.ContentType = "text/plain";
			HttpContext.Current.Response.Write(s);
			HttpContext.Current.Response.End();
		}
	}
	private string GetMonthPlanList(string _mpid)
	{
		new Fund_Plan_MonthMainAction();
		StringBuilder stringBuilder = new StringBuilder();
		DataTable dataTable = new DataTable();
		if (base.Request.Form["plantype"] != null && !(base.Request.Form["plantype"].ToString() == "payout"))
		{
		}
		stringBuilder.Append("<table class=\"gvdata\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gvwWebLineList\" style=\"width:100%;border-collapse:collapse;\">");
		stringBuilder.Append("<tr class=\"header\">");
		stringBuilder.Append("<th scope=\"col\" style=\"width:20px;\">序号</th>");
		stringBuilder.Append("<th scope=\"col\">合同</th>");
		stringBuilder.Append("<th scope=\"col\">本期计划金额</th>");
		stringBuilder.Append("<th scope=\"col\" style=\"width:30px;\">附件</th>");
		stringBuilder.Append("<th scope=\"col\">备注</th>");
		stringBuilder.Append("</tr>");
		if (dataTable.Rows.Count > 0)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				stringBuilder.Append(string.Concat(new string[]
				{
					"<tr id=\"",
					dataTable.Rows[i]["UID"].ToString(),
					"\" guid=\"",
					dataTable.Rows[i]["UID"].ToString(),
					"\">"
				}));
				stringBuilder.Append("<td align=\"center\">").Append((i + 1).ToString()).Append("</td>");
				stringBuilder.Append("<td style=\"width:280px; \">").Append(dataTable.Rows[i]["ContractName"].ToString()).Append("</td>");
				stringBuilder.Append("<td align=\"right\" style=\"width:100px;\">").Append(dataTable.Rows[i]["PlanMoney"].ToString()).Append("</td>");
				stringBuilder.Append("<td align=\"center\" style=\"width:30px;\">");
				string text = string.Empty;
				if (base.Request.Form["path"] != null)
				{
					text += dataTable.Rows[i]["UID"].ToString();
				}
				if (!string.IsNullOrEmpty(text))
				{
					text = HttpContext.Current.Server.MapPath(text);
				}
				DirectoryUtility directoryUtility = new DirectoryUtility(text);
				List<Annex> annex = directoryUtility.GetAnnex();
				ISerializable serializable = new JsonSerializer();
				string a = serializable.Serialize<List<Annex>>(annex);
				if (!(a == "[]"))
				{
					stringBuilder.Append("<span class=\"link\" onclick=\"queryAdjunct('" + dataTable.Rows[i]["UID"].ToString() + "')\">");
					stringBuilder.Append("<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />");
					stringBuilder.Append("</span>");
				}
				stringBuilder.Append("</td>");
				if (dataTable.Rows[i]["ReMark"].ToString().Length > 30)
				{
					stringBuilder.Append("<td>").Append(dataTable.Rows[i]["ReMark"].ToString().Substring(0, 29) + "...").Append("</td>");
				}
				else
				{
					stringBuilder.Append("<td>").Append(dataTable.Rows[i]["ReMark"].ToString()).Append("</td>");
				}
				stringBuilder.Append("</tr>");
			}
		}
		stringBuilder.Append("</table>");
		return stringBuilder.ToString();
	}
}


