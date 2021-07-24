using qiupeng.sql;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
namespace qiupeng.hr
{
	public class divhr
	{
		private Db List = new Db();
		public string HRSType(string str)
		{
			if (str == "1")
			{
				str = "民族";
			}
			if (str == "2")
			{
				str = "籍贯";
			}
			if (str == "3")
			{
				str = "政治面貌";
			}
			if (str == "4")
			{
				str = "学历";
			}
			if (str == "5")
			{
				str = "专业";
			}
			if (str == "6")
			{
				str = "职称";
			}
			if (str == "7")
			{
				str = "员工类型";
			}
			if (str == "8")
			{
				str = "调动类型";
			}
			if (str == "9")
			{
				str = "离职类型";
			}
			if (str == "10")
			{
				str = "复职类型";
			}
			if (str == "11")
			{
				str = "奖励类型";
			}
			if (str == "12")
			{
				str = "惩罚类型";
			}
			if (str == "13")
			{
				str = "技能分类";
			}
			if (str == "14")
			{
				str = "体检结果";
			}
			return str;
		}
		public string HrMoban(string str)
		{
			if (str == "1")
			{
				str = "用人申请";
			}
			if (str == "2")
			{
				str = "简历管理";
			}
			if (str == "3")
			{
				str = "正式录用";
			}
			return str;
		}
		public string TypeCode(string FileName, string Table, string CodeId)
		{
			string sql = string.Concat(new string[]
			{
				"select ",
				FileName,
				" from ",
				Table,
				" where id='",
				CodeId,
				"' "
			});
			SqlDataReader list = this.List.GetList(sql);
			string result;
			if (list.Read())
			{
				result = list["" + FileName + ""].ToString();
			}
			else
			{
				result = "";
			}
			list.Close();
			return result;
		}
		public string TypeCodeFX(string FileName, string Table, string CodeId)
		{
			string sql = string.Concat(new string[]
			{
				"select ",
				FileName,
				" from ",
				Table,
				" where id='",
				CodeId,
				"' "
			});
			SqlDataReader list = this.List.GetList(sql);
			string result;
			if (list.Read())
			{
				result = list["" + FileName + ""].ToString();
			}
			else
			{
				result = "未分类";
			}
			list.Close();
			return result;
		}
		public string AllDataFile(string CodeId, string TypeId)
		{
			string sql = string.Concat(new string[]
			{
				"select Name from qp_crm_AllDataFile where id='",
				CodeId,
				"' and type='",
				TypeId,
				"'"
			});
			SqlDataReader list = this.List.GetList(sql);
			string result;
			if (list.Read())
			{
				result = list["Name"].ToString();
			}
			else
			{
				result = "";
			}
			list.Close();
			return result;
		}
		public string SaleUser()
		{
			string result = null;
			string sql = "select contentuser from qp_crm_SaleUser";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				string text = null;
				string text2 = "" + list["contentuser"].ToString() + "";
				ArrayList arrayList = new ArrayList();
				string[] array = text2.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					text = text + "'" + array[i] + "',";
				}
				text += "'0'";
				result = text;
			}
			list.Close();
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
		public void Insertlsz(string HetongID, string Leixing, string Xiangqing)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_hr_Hetongls (HetongID,Leixing,Username,Realname,SetTimes,Xiangqing) values ('",
				HetongID,
				"','",
				Leixing,
				"','",
				HttpContext.Current.Session["Username"],
				"','",
				HttpContext.Current.Session["Realname"],
				"','",
				DateTime.Now.ToString(),
				"','",
				Xiangqing,
				"')"
			});
			this.List.ExeSql(sql);
		}
	}
}
