using qiupeng.sql;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace qiupeng.pm
{
	public class divpm
	{
		private Db List = new Db();
		public string PMType(string str)
		{
			if (str == "1")
			{
				str = "项目类别";
			}
			if (str == "2")
			{
				str = "成员角色";
			}
			if (str == "3")
			{
				str = "支出类别";
			}
			if (str == "4")
			{
				str = "外包罚款分类";
			}
			if (str == "5")
			{
				str = "往来单位类别";
			}
			if (str == "6")
			{
				str = "设备类别";
			}
			if (str == "7")
			{
				str = "资质类别";
			}
			if (str == "8")
			{
				str = "工程巡查类别";
			}
			if (str == "9")
			{
				str = "外包机械分类";
			}
			if (str == "10")
			{
				str = "收入类别";
			}
			return str;
		}
		public void InsertXmLog(string XmId, string Ziduan, string Yuanlai, string Xianzai)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_pm_XmLog (XmId,Username,Realname,Settime,Ziduan,Yuanlai,Xianzai) values ('",
				XmId,
				"','",
				HttpContext.Current.Session["Username"],
				"','",
				HttpContext.Current.Session["Realname"],
				"','",
				DateTime.Now.ToString(),
				"','",
				Ziduan,
				"','",
				Yuanlai,
				"','",
				Xianzai,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public void Insertlsz(string XmId, string Leixing, string CkId, string CailiaoId, string RkShuliang, string CkShuliang, string Shujubiao, string ShujubiaoId)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_pm_KuFangLS (XmId,Leixing,CkId,CailiaoId,RkShuliang,CkShuliang,Riqi,Username,Realname,Shujubiao,ShujubiaoId) values ('",
				XmId,
				"','",
				Leixing,
				"','",
				CkId,
				"','",
				CailiaoId,
				"','",
				RkShuliang,
				"','",
				CkShuliang,
				"','",
				DateTime.Now.ToShortDateString(),
				"','",
				HttpContext.Current.Session["Username"],
				"','",
				HttpContext.Current.Session["Realname"],
				"','",
				Shujubiao,
				"','",
				ShujubiaoId,
				"')"
			});
			this.List.ExeSql(sql);
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
		public void SetZhuti(TextBox TextBox, string leixing)
		{
			string sql = "select * from qp_pm_Bianhao where leixing='" + leixing + "'";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				if (list["Qiyong"].ToString() == "1")
				{
					string text = list["Bianhao"].ToString();
					text = text.Replace("{Y}", DateTime.Now.Year.ToString());
					text = text.Replace("{M}", DateTime.Now.Month.ToString().PadLeft(2, '0'));
					text = text.Replace("{D}", DateTime.Now.Day.ToString().PadLeft(2, '0'));
					text = text.Replace("{H}", DateTime.Now.Hour.ToString());
					text = text.Replace("{I}", DateTime.Now.Minute.ToString());
					text = text.Replace("{S}", DateTime.Now.Second.ToString());
					text = text.Replace("{U}", HttpContext.Current.Session["realname"].ToString());
					text = text.Replace("{W}", this.GetWeekDay());
					text = text.Replace("{DN}", this.SetZhutiCount(leixing, "{DN}"));
					text = text.Replace("{MN}", this.SetZhutiCount(leixing, "{MN}"));
					text = text.Replace("{YN}", this.SetZhutiCount(leixing, "{YN}"));
					if (list["Xiugai"].ToString() == "0")
					{
						TextBox.Attributes.Add("readonly", "readonly");
						TextBox.CssClass = "ReadOnlyText";
					}
					TextBox.Text = text;
				}
			}
			list.Close();
		}
		public void SetZhutiUpdate(TextBox TextBox, string leixing)
		{
			string sql = "select * from qp_pm_Bianhao where leixing='" + leixing + "'";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				if (list["Xiugai"].ToString() == "0")
				{
					TextBox.Attributes.Add("readonly", "readonly");
					TextBox.CssClass = "ReadOnlyText";
				}
			}
			list.Close();
		}
		public string SetZhutiCount(string leixing, string bianhao)
		{
			string result = "";
			if (leixing == "1")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ProInfo where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ProInfo where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ProInfo where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "2")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Hetong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Hetong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Hetong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "3")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_JiaoWang where datediff(dd,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_JiaoWang where datediff(mm,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_JiaoWang where datediff(yy,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "4")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Feiyong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Feiyong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Feiyong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "5")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Jiagong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Jiagong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Jiagong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "6")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Yusuan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Yusuan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Yusuan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "7")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Jihua where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Jihua where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Jihua where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "8")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Dingdan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Dingdan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Dingdan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "9")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Ruku where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Ruku where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Ruku where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "10")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Lingyong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Lingyong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Lingyong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "11")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Diaobo where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Diaobo where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Diaobo where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "12")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Pandian where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Pandian where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Pandian where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "13")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Xuncha where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Xuncha where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Xuncha where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "14")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_FbHetong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_FbHetong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_FbHetong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "15")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_FbChufa where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_FbChufa where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_FbChufa where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "16")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_YsCheliang where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_YsCheliang where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_YsCheliang where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "17")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_WbJixie where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_WbJixie where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_WbJixie where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "18")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_WbCailiao where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_WbCailiao where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_WbCailiao where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "19")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiLy where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiLy where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiLy where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "20")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiGh where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiGh where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ShebeiGh where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "21")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiLy where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiLy where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiLy where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "22")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiGh where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiGh where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ZizhiGh where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "23")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJh where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJh where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJh where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "24")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJl where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJl where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_ShoukuanJl where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "25")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJh where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJh where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJh where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "26")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJl where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJl where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_FukuanJl where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "27")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_FbFukuan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_FbFukuan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_FbFukuan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "28")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Tuiku where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Tuiku where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Tuiku where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "29")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Baosun where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Baosun where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Baosun where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "30")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_pm_Tuihuo where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_pm_Tuihuo where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_pm_Tuihuo where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			return result;
		}
		public string ChlidQX()
		{
			return "";
		}
		public string ChlidQXXM()
		{
			string text = "";
			string sql = string.Concat(new object[]
			{
				"select Quanxian from qp_pm_ProUser where  Username='",
				HttpContext.Current.Session["username"],
				"' and Xmid='",
				HttpContext.Current.Session["xmid"],
				"'"
			});
			SqlDataReader list = this.List.GetList(sql);
			while (list.Read())
			{
				text += list["Quanxian"].ToString();
			}
			list.Close();
			return text;
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
		public string TypeCodeAll(string FileName, string Table, string CodeId, string KeyId)
		{
			string sql = string.Concat(new string[]
			{
				"select ",
				FileName,
				" from ",
				Table,
				" where ",
				KeyId,
				"='",
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
        public string TypeCodeAllInt(string FileName, string Table, string CodeId, string KeyId)
        {
            string sql = string.Concat(new string[]
			{
				"select ",
				FileName,
				" from ",
				Table,
				" where ",
				KeyId,
				"='",
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
                result = "0";
            }
            list.Close();
            return result;
        }
		public void InsertLog(string Name, string XmId)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_pm_SystemLog (XmId,Name,Username,Realname,Nowtimes,Ip,BuMenId) values ('",
				XmId,
				"','",
				Name,
				"','",
				HttpContext.Current.Session["username"],
				"','",
				HttpContext.Current.Session["realname"],
				"','",
				DateTime.Now.ToString(),
				"','",
				HttpContext.Current.Request.UserHostAddress,
				"','",
				HttpContext.Current.Session["BuMenId"],
				"')"
			});
			this.List.ExeSql(sql);
		}
	}
}
