using qiupeng.sql;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace qiupeng.form
{
	public class divform
	{
		private Db List = new Db();
		public string bmstr = null;
		public string FormatKjStrH(string num, string lcid, string AStr, string bdmc, string mcwh, string lcksrq, string lckssj, int lsh)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("datafld=\"SYS_realname_" + num + "\"", string.Concat(new object[]
				{
					" datafld=\"SYS_realname_",
					num,
					"\" value=\"",
					HttpContext.Current.Session["realname"],
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_Unit_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_Unit_",
					num,
					"\" value=\"",
					this.BumenName(HttpContext.Current.Session["BuMenId"].ToString()),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_Respon_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_Respon_",
					num,
					"\" value=\"",
					this.JueseName(HttpContext.Current.Session["JueseId"].ToString()),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_Post_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_Post_",
					num,
					"\" value=\"",
					this.ZhiweiName(HttpContext.Current.Session["Zhiweiid"].ToString()),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_UnitZgA_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_UnitZgA_",
					num,
					"\" value=\"",
					this.BumenZgA(HttpContext.Current.Session["BuMenId"].ToString()),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_UnitZgB_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_UnitZgB_",
					num,
					"\" value=\"",
					this.BumenZgB(HttpContext.Current.Session["BuMenId"].ToString()),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_UnitZgC_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_UnitZgC_",
					num,
					"\" value=\"",
					this.BumenZgC(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateTime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateTime_",
					num,
					"\" value=\"",
					DateTime.Now.ToShortDateString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateYear_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateYear_",
					num,
					"\" value=\"",
					DateTime.Now.Year.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateMonth_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateMonth_",
					num,
					"\" value=\"",
					DateTime.Now.Month.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateDate_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateDate_",
					num,
					"\" value=\"",
					DateTime.Now.Day.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateHour_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateHour_",
					num,
					"\" value=\"",
					DateTime.Now.Hour.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateMinute_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateMinute_",
					num,
					"\" value=\"",
					DateTime.Now.Minute.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateSecond_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateSecond_",
					num,
					"\" value=\"",
					DateTime.Now.Second.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateDayOfWeek_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateDayOfWeek_",
					num,
					"\" value=\"",
					this.GetWeekDay(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateATime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateATime_",
					num,
					"\" value=\"",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateBTime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateBTime_",
					num,
					"\" value=\"",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateCTime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateCTime_",
					num,
					"\" value=\"",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateDTime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateDTime_",
					num,
					"\" value=\"",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_DateETime_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_DateETime_",
					num,
					"\" value=\"",
					DateTime.Now.ToShortDateString(),
					"\u3000",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_username_" + num + "\"", string.Concat(new object[]
				{
					"  datafld=\"SYS_username_",
					num,
					"\" value=\"",
					HttpContext.Current.Session["username"],
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_RRDateDate_" + num + "\"", string.Concat(new object[]
				{
					"  datafld=\"SYS_RRDateDate_",
					num,
					"\" value=\"",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToShortDateString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_RRDateTime_" + num + "\"", string.Concat(new object[]
				{
					"  datafld=\"SYS_RRDateTime_",
					num,
					"\" value=\"",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToString(),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_IpAddress_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_IpAddress_",
					num,
					"\" value=\"",
					HttpContext.Current.Request.UserHostAddress,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_bdmc_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_bdmc_",
					num,
					"\" value=\"",
					bdmc,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_mcwh_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_mcwh_",
					num,
					"\" value=\"",
					mcwh,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_lcksrq_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_lcksrq_",
					num,
					"\" value=\"",
					lcksrq,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_lckssj_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_lckssj_",
					num,
					"\" value=\"",
					lckssj,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_lsh_" + num + "\"", string.Concat(new object[]
				{
					"  datafld=\"SYS_lsh_",
					num,
					"\" value=\"",
					lsh,
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_AllJbrA_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_AllJbrA_",
					num,
					"\" value=\"",
					this.FlowZbr(lcid),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_AllJbrB_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_AllJbrB_",
					num,
					"\" value=\"",
					this.FlowJbr(lcid),
					"\""
				}));
				AStr = AStr.Replace("datafld=\"SYS_AllYbr_" + num + "\"", string.Concat(new string[]
				{
					"  datafld=\"SYS_AllYbr_",
					num,
					"\" value=\"",
					this.FlowYbr(lcid),
					"\""
				}));
				AStr = AStr.Replace("{宏}部门列表_" + num + "", "" + this.SysBumenLb(num, 0, "|-") + "");
				AStr = AStr.Replace("{宏}用户列表_" + num + "", "" + this.SysRenyuanLb() + "");
				AStr = AStr.Replace("{宏}职位列表_" + num + "", "" + this.SysZhiweiLb() + "");
				AStr = AStr.Replace("{宏}角色列表_" + num + "", "" + this.SysJueseLb() + "");
				AStr = AStr.Replace("{宏}我的客户列表_" + num + "", "" + this.SysKehuLb() + "");
				AStr = AStr.Replace("datafld=SYS_realname_" + num + "", string.Concat(new object[]
				{
					" datafld=SYS_realname_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					""
				}));
				AStr = AStr.Replace("datafld=SYS_Unit_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_Unit_",
					num,
					" value=",
					this.BumenName(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_Respon_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_Respon_",
					num,
					" value=",
					this.JueseName(HttpContext.Current.Session["JueseId"].ToString()),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_Post_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_Post_",
					num,
					" value=",
					this.ZhiweiName(HttpContext.Current.Session["Zhiweiid"].ToString()),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_UnitZgA_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_UnitZgA_",
					num,
					" value=",
					this.BumenZgA(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_UnitZgB_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_UnitZgB_",
					num,
					" value=",
					this.BumenZgB(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_UnitZgC_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_UnitZgC_",
					num,
					" value=",
					this.BumenZgC(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateTime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateTime_",
					num,
					" value=",
					DateTime.Now.ToShortDateString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateYear_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateYear_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateMonth_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateMonth_",
					num,
					" value=",
					DateTime.Now.Month.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateDate_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateDate_",
					num,
					" value=",
					DateTime.Now.Day.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateHour_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateHour_",
					num,
					" value=",
					DateTime.Now.Hour.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateMinute_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateMinute_",
					num,
					" value=",
					DateTime.Now.Minute.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateSecond_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateSecond_",
					num,
					" value=",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateDayOfWeek_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateDayOfWeek_",
					num,
					" value=",
					this.GetWeekDay(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateATime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateATime_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日"
				}));
				AStr = AStr.Replace("datafld=SYS_DateBTime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateBTime_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月"
				}));
				AStr = AStr.Replace("datafld=SYS_DateCTime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateCTime_",
					num,
					" value=",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日"
				}));
				AStr = AStr.Replace("datafld=SYS_DateDTime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateDTime_",
					num,
					" value=",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_DateETime_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_DateETime_",
					num,
					" value=",
					DateTime.Now.ToShortDateString(),
					"\u3000",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_username_" + num + "", string.Concat(new object[]
				{
					"  datafld=SYS_username_",
					num,
					" value=",
					HttpContext.Current.Session["username"],
					""
				}));
				AStr = AStr.Replace("datafld=SYS_RRDateDate_" + num + "", string.Concat(new object[]
				{
					"  datafld=SYS_RRDateDate_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToShortDateString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_RRDateTime_" + num + "", string.Concat(new object[]
				{
					"  datafld=SYS_RRDateTime_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToString(),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_IpAddress_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_IpAddress_",
					num,
					" value=",
					HttpContext.Current.Request.UserHostAddress,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_bdmc_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_bdmc_",
					num,
					" value=",
					bdmc,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_mcwh_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_mcwh_",
					num,
					" value=",
					mcwh,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_lcksrq_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_lcksrq_",
					num,
					" value=",
					lcksrq,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_lckssj_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_lckssj_",
					num,
					" value=",
					lckssj,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_lsh_" + num + "", string.Concat(new object[]
				{
					"  datafld=SYS_lsh_",
					num,
					" value=",
					lsh,
					""
				}));
				AStr = AStr.Replace("datafld=SYS_AllJbrA_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_AllJbrA_",
					num,
					" value=",
					this.FlowZbr(lcid),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_AllJbrB_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_AllJbrB_",
					num,
					" value=",
					this.FlowJbr(lcid),
					""
				}));
				AStr = AStr.Replace("datafld=SYS_AllYbr_" + num + "", string.Concat(new string[]
				{
					"  datafld=SYS_AllYbr_",
					num,
					" value=",
					this.FlowYbr(lcid),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_realname_" + num + "", string.Concat(new object[]
				{
					" dataFld=SYS_realname_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_Unit_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_Unit_",
					num,
					" value=",
					this.BumenName(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_Respon_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_Respon_",
					num,
					" value=",
					this.JueseName(HttpContext.Current.Session["JueseId"].ToString()),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_Post_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_Post_",
					num,
					" value=",
					this.ZhiweiName(HttpContext.Current.Session["Zhiweiid"].ToString()),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_UnitZgA_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_UnitZgA_",
					num,
					" value=",
					this.BumenZgA(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_UnitZgB_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_UnitZgB_",
					num,
					" value=",
					this.BumenZgB(HttpContext.Current.Session["BuMenId"].ToString()),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_UnitZgC_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_UnitZgC_",
					num,
					" value=",
					this.BumenZgC(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateTime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateTime_",
					num,
					" value=",
					DateTime.Now.ToShortDateString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateYear_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateYear_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateMonth_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateMonth_",
					num,
					" value=",
					DateTime.Now.Month.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateDate_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateDate_",
					num,
					" value=",
					DateTime.Now.Day.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateHour_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateHour_",
					num,
					" value=",
					DateTime.Now.Hour.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateMinute_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateMinute_",
					num,
					" value=",
					DateTime.Now.Minute.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateSecond_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateSecond_",
					num,
					" value=",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateDayOfWeek_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateDayOfWeek_",
					num,
					" value=",
					this.GetWeekDay(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateATime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateATime_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日"
				}));
				AStr = AStr.Replace("dataFld=SYS_DateBTime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateBTime_",
					num,
					" value=",
					DateTime.Now.Year.ToString(),
					"年",
					DateTime.Now.Month.ToString(),
					"月"
				}));
				AStr = AStr.Replace("dataFld=SYS_DateCTime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateCTime_",
					num,
					" value=",
					DateTime.Now.Month.ToString(),
					"月",
					DateTime.Now.Day.ToString(),
					"日"
				}));
				AStr = AStr.Replace("dataFld=SYS_DateDTime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateDTime_",
					num,
					" value=",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_DateETime_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_DateETime_",
					num,
					" value=",
					DateTime.Now.ToShortDateString(),
					"\u3000",
					DateTime.Now.Hour.ToString(),
					":",
					DateTime.Now.Minute.ToString(),
					":",
					DateTime.Now.Second.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_username_" + num + "", string.Concat(new object[]
				{
					"  dataFld=SYS_username_",
					num,
					" value=",
					HttpContext.Current.Session["username"],
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_RRDateDate_" + num + "", string.Concat(new object[]
				{
					"  dataFld=SYS_RRDateDate_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToShortDateString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_RRDateTime_" + num + "", string.Concat(new object[]
				{
					"  dataFld=SYS_RRDateTime_",
					num,
					" value=",
					HttpContext.Current.Session["realname"],
					"",
					DateTime.Now.ToString(),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_IpAddress_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_IpAddress_",
					num,
					" value=",
					HttpContext.Current.Request.UserHostAddress,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_bdmc_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_bdmc_",
					num,
					" value=",
					bdmc,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_mcwh_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_mcwh_",
					num,
					" value=",
					mcwh,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_lcksrq_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_lcksrq_",
					num,
					" value=",
					lcksrq,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_lckssj_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_lckssj_",
					num,
					" value=",
					lckssj,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_lsh_" + num + "", string.Concat(new object[]
				{
					"  dataFld=SYS_lsh_",
					num,
					" value=",
					lsh,
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_AllJbrA_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_AllJbrA_",
					num,
					" value=",
					this.FlowZbr(lcid),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_AllJbrB_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_AllJbrB_",
					num,
					" value=",
					this.FlowJbr(lcid),
					""
				}));
				AStr = AStr.Replace("dataFld=SYS_AllYbr_" + num + "", string.Concat(new string[]
				{
					"  dataFld=SYS_AllYbr_",
					num,
					" value=",
					this.FlowYbr(lcid),
					""
				}));
				AStr = AStr.Replace(" =\"\"", "");
				result = AStr;
			}
			return result;
		}
		public string FormatKjStrZh(string AStr)
		{
			AStr = AStr.Replace("’", "'").Replace("display:none", "").Replace("DISPLAY: none", "").Replace("background-color: #f9f9f9", "").Replace("BACKGROUND-COLOR: #f9f9f9", "").Replace("BACKGROUND-COLOR: #F9F9F9", "").Replace("BACKGROUND-COLOR: #F9FBD5", "").Replace("dragAble", "dragLCAble").Replace("disabled", "").Replace("readonly", "").Replace("readOnly", "").Replace("{宏}用户姓名", "").Replace("{宏}用户部门短名称", "").Replace("{宏}用户部门长名称", "").Replace("{宏}用户角色", "").Replace("{宏}用户职位", "").Replace("{宏}用户部门主管(本部门)", "").Replace("{宏}用户部门主管(上级部门)", "").Replace("{宏}用户部门主管(一级部门)", "").Replace("{宏}当前日期(形如2009-1-1)", "").Replace("{宏}当前日期(年)", "").Replace("{宏}当前日期(月)", "").Replace("{宏}当前日期(日)", "").Replace("{宏}当前日期(时)", "").Replace("{宏}当前日期(分)", "").Replace("{宏}当前日期(秒)", "").Replace("{宏}当前日期(星期)", "").Replace("{宏}当前日期(形如2009年1月1日)", "").Replace("{宏}当前日期(形如2009年1月)", "").Replace("{宏}当前日期(形如1月1日)", "").Replace("{宏}当前时间(形如12:12:12)", "").Replace("{宏}当前时间(形如2009-12-12 12:12:12)", "").Replace("{宏}用户ID", "").Replace("{宏}用户的姓名+日期", "").Replace("{宏}用户的姓名+具体日期时间", "").Replace("{宏}经办人IP", "").Replace("{宏}表单名称", "").Replace("{宏}名称/文号", "").Replace("{宏}流程开始日期", "").Replace("{宏}流程开始的日期+时间", "").Replace("{宏}流水号", "").Replace("{宏}当前步骤主办人", "").Replace("{宏}当前步骤所有经办人", "").Replace("{宏}所有已经办人员", "");
			return AStr;
		}
		private string SysBumenLb(string num, int IDStr, string line)
		{
			string str = null;
			this.SysBumenLbC(IDStr, line);
			return str + this.bmstr;
		}
		private void SysBumenLbC(int IDStr, string line)
		{
			string sql = "select id,Name,ParentNodesID from qp_oa_bumen where ParentNodesID=" + IDStr.ToString() + "";
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				if (IDStr == 0)
				{
					string text = this.bmstr;
					this.bmstr = string.Concat(new string[]
					{
						text,
						"<option value=\"",
						list["Name"].ToString(),
						"\">|-",
						list["Name"].ToString(),
						"</option>"
					});
					int iDStr = int.Parse(list["ID"].ToString());
					this.SysBumenLbC(iDStr, "|-");
				}
				else
				{
					string text2 = "" + line + "--";
					string text = this.bmstr;
					this.bmstr = string.Concat(new string[]
					{
						text,
						"<option value=\"",
						list["Name"].ToString(),
						"\">",
						text2,
						"",
						list["Name"].ToString(),
						"</option>"
					});
					int iDStr = int.Parse(list["ID"].ToString());
					this.SysBumenLbC(iDStr, text2);
				}
			}
			list.Close();
		}
		private string SysRenyuanLb()
		{
			string text = null;
			string sql = "select Realname from qp_oa_Username where   Iflogin='1' and StardType='1' and ifdel='0'";
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"<option value=\"",
					list["Realname"].ToString(),
					"\">",
					list["Realname"].ToString(),
					"</option>"
				});
			}
			list.Close();
			return text;
		}
		private string SysZhiweiLb()
		{
			string text = null;
			string sql = "select name from qp_oa_Zhiwei";
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"<option value=\"",
					list["name"].ToString(),
					"\">",
					list["name"].ToString(),
					"</option>"
				});
			}
			list.Close();
			return text;
		}
		private string SysJueseLb()
		{
			string text = null;
			string sql = "select name from qp_oa_Juese";
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"<option value=\"",
					list["name"].ToString(),
					"\">",
					list["name"].ToString(),
					"</option>"
				});
			}
			list.Close();
			return text;
		}
		private string SysKehuLb()
		{
			string text = null;
			string sql = "select Name from qp_crm_Customer where IfDel=0 and Username='" + HttpContext.Current.Session["Username"] + "'";
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"<option value=\"",
					list["Name"].ToString(),
					"\">",
					list["Name"].ToString(),
					"</option>"
				});
			}
			list.Close();
			return text;
		}
		public string FormatWh(string AStr, string LcNameId, string BianhaoWs, string FlowName)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("{Y}", DateTime.Now.Year.ToString());
				AStr = AStr.Replace("{M}", DateTime.Now.Month.ToString());
				AStr = AStr.Replace("{D}", DateTime.Now.Day.ToString());
				AStr = AStr.Replace("{H}", DateTime.Now.Hour.ToString());
				AStr = AStr.Replace("{I}", DateTime.Now.Minute.ToString());
				AStr = AStr.Replace("{S}", DateTime.Now.Second.ToString());
				AStr = AStr.Replace("{F}", FlowName);
				AStr = AStr.Replace("{U}", HttpContext.Current.Session["realname"].ToString());
				AStr = AStr.Replace("{DN}", this.BumenBh(HttpContext.Current.Session["BuMenId"].ToString()));
				AStr = AStr.Replace("{SD}", this.BumenName(HttpContext.Current.Session["BuMenId"].ToString()));
				AStr = AStr.Replace("{R}", this.JueseName(HttpContext.Current.Session["JueseId"].ToString()));
				AStr = AStr.Replace("{N}", "" + LcNameId + "");
				AStr = AStr.Replace("{W}", this.GetWeekDay());
				result = AStr;
			}
			return result;
		}
		private string GetWeekDay()
		{
			string result = "";
			switch (DateTime.Now.DayOfWeek)
			{
			case DayOfWeek.Sunday:
				result = "星期日";
				break;
			case DayOfWeek.Monday:
				result = "星期一";
				break;
			case DayOfWeek.Tuesday:
				result = "星期二";
				break;
			case DayOfWeek.Wednesday:
				result = "星期三";
				break;
			case DayOfWeek.Thursday:
				result = "星期四";
				break;
			case DayOfWeek.Friday:
				result = "星期五";
				break;
			case DayOfWeek.Saturday:
				result = "星期六";
				break;
			}
			return result;
		}
		public string FlowYbr(string id)
		{
			string sql = "select YJBObjecName from qp_oa_AddWorkFlow where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["YJBObjecName"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string FlowZbr(string id)
		{
			string sql = "select ZbObjectName from qp_oa_AddWorkFlow where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["ZbObjectName"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string FlowJbr(string id)
		{
			string sql = "select JbObjectName from qp_oa_AddWorkFlow where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["JbObjectName"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string BumenZgA(string id)
		{
			string sql = "select BmRealname from qp_oa_Bumen where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["BmRealname"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string BumenZgB(string id)
		{
			string text = null;
			string sql = "select ParentNodesID from qp_oa_Bumen where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				string sql2 = "select BmRealname from qp_oa_Bumen where  id='" + list["ParentNodesID"].ToString() + "'";
				SqlDataReader list2 = this.List.GetList(sql2);
				if (list2.Read())
				{
					text = list2["BmRealname"].ToString();
				}
				else
				{
					text = "";
				}
				list2.Close();
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string BumenZgC()
		{
			string sql = "select BmRealname from qp_oa_Bumen where  ParentNodesID='0' and  CHARINDEX(QxString,(select QxString from qp_oa_Bumen where id='" + HttpContext.Current.Session["BuMenId"].ToString() + "')) > 0";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["BmRealname"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string BumenName(string id)
		{
			string sql = "select Name from qp_oa_Bumen where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["Name"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string BumenBh(string id)
		{
			string sql = "select Bianhao from qp_oa_Bumen where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["Bianhao"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string ZhiweiName(string id)
		{
			string sql = "select Name from qp_oa_Zhiwei where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["Name"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public string JueseName(string id)
		{
			string sql = "select Name from qp_oa_Juese where  id='" + id + "'";
			SqlDataReader list = this.List.GetList(sql);
			string text;
			if (list.Read())
			{
				text = list["Name"].ToString();
			}
			else
			{
				text = "";
			}
			list.Close();
			if (text.Trim() == "")
			{
				text = "\u3000";
			}
			return text;
		}
		public void BindListDz(Label _Label, string NodeId)
		{
			string sql = "select * from qp_oa_Bumen where id='" + NodeId + "'";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				_Label.Text = "" + list["Name"].ToString();
				this.BindChildDz(_Label, list["ParentNodesID"].ToString());
			}
			list.Close();
		}
		private void BindChildDz(Label _Label, string ParentID)
		{
			string sql = "select * from qp_oa_Bumen where id=" + ParentID + "";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				_Label.Text = string.Concat(new string[]
				{
					"",
					list["Name"].ToString(),
					"/",
					_Label.Text,
					""
				});
				if (list["ParentNodesID"].ToString() != "0")
				{
					this.BindChildDz(_Label, list["ParentNodesID"].ToString());
				}
			}
			list.Close();
		}
		public void AddWorkFlowLog(string Email, string Number, string FormName, string Buzhou, string Contents, string ZbOrJb)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_oa_AddWorkFlowLog (Email,Number,FormName,Buzhou,Contents,ZbOrJb,Username,Realname,Settime) values ('",
				Email,
				"','",
				Number,
				"','",
				FormName,
				"','",
				Buzhou,
				"','",
				Contents,
				"','",
				ZbOrJb,
				"','",
				HttpContext.Current.Session["Username"],
				"','",
				HttpContext.Current.Session["Realname"],
				"','",
				DateTime.Now.ToString(),
				"')"
			});
			this.List.ExeSql(sql);
		}
	}
}
