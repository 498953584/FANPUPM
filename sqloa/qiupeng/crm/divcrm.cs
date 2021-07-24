using qiupeng.publiccs;
using qiupeng.sql;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace qiupeng.crm
{
	public class divcrm
	{
		private Db List = new Db();
		private publics pulicss = new publics();
		public string PrintForm(string id, string AStr, string type)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				if (type == "1")
				{
					string sql = string.Concat(new object[]
					{
						"select * from qp_crm_Customer where id='",
						id,
						"' and  IfDel=0 and Username='",
						HttpContext.Current.Session["Username"],
						"'"
					});
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{客户名称}", list["Name"].ToString());
						AStr = AStr.Replace("{助记简称}", list["ZhuJiJc"].ToString());
						AStr = AStr.Replace("{下次回访时间}", list["HfTime"].ToString());
						AStr = AStr.Replace("{客户价值评估}", list["JiaZhiPg"].ToString().Replace("1", "高").Replace("2", "中").Replace("3", "低"));
						AStr = AStr.Replace("{客户种类}", this.TypeCode("Name", "qp_crm_DataFile", list["ZhongLei"].ToString()));
						AStr = AStr.Replace("{客户信用等级}", list["XinYongDj"].ToString().ToString().Replace("1", "高").Replace("2", "中").Replace("3", "低"));
						AStr = AStr.Replace("{客户行业}", this.TypeCode("Name", "qp_crm_DataFile", list["HangYe"].ToString()));
						AStr = AStr.Replace("{客户关系等级}", this.TypeCode("Name", "qp_crm_DataFile", list["DengJi"].ToString()));
						AStr = AStr.Replace("{客户人员规模}", list["Name"].ToString());
						AStr = AStr.Replace("{客户来源}", this.TypeCode("Name", "qp_crm_DataFile", list["LaiYuan"].ToString()));
						AStr = AStr.Replace("{客户阶段}", this.TypeCode("Name", "qp_crm_DataFile", list["JieDuan"].ToString()));
						AStr = AStr.Replace("{客户简介}", this.pulicss.TbToLb(list["JianJie"].ToString()));
						AStr = AStr.Replace("{客户区域}", this.TypeCode("Name", "qp_crm_Diqu", list["GuoJia"].ToString()));
						AStr = AStr.Replace("{电话}", list["Dianhua"].ToString());
						AStr = AStr.Replace("{省份}", this.TypeCode("Name", "qp_crm_Shengfen", list["Shengfen"].ToString()));
						AStr = AStr.Replace("{传真}", list["Chuanzheng"].ToString());
						AStr = AStr.Replace("{城市}", this.TypeCode("Name", "qp_crm_Chengshi", list["Chengshi"].ToString()));
						AStr = AStr.Replace("{网址}", list["Wangzhi"].ToString());
						AStr = AStr.Replace("{电子邮箱}", list["YouBian"].ToString());
						AStr = AStr.Replace("{地址}", list["Dizhi"].ToString());
						AStr = AStr.Replace("{备注}", this.pulicss.TbToLb(list["Remark"].ToString()));
						AStr = AStr.Replace("{创建人}", list["Realname"].ToString());
						AStr = AStr.Replace("{创建时间}", list["SetTimes"].ToString().Replace("00:00:00", "").Replace("1900-01-01", "").Replace("0:00:00", "").Replace("1900-1-1", ""));
					}
					list.Close();
				}
				result = AStr;
			}
			return result;
		}
		public void SetZhuti(TextBox TextBox, string leixing)
		{
			string sql = "select * from qp_crm_Bianhao where leixing='" + leixing + "'";
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
			string sql = "select * from qp_crm_Bianhao where leixing='" + leixing + "'";
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
					string sql = "select count(id) from qp_crm_GuanHuai where datediff(dd,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_GuanHuai where datediff(mm,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_GuanHuai where datediff(yy,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "2")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_RenWu where datediff(dd,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_RenWu where datediff(mm,getdate(),SetTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_RenWu where datediff(yy,getdate(),SetTimes)=0";
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
					string sql = "select count(id) from qp_crm_JiHui where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_JiHui where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_JiHui where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "5")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_BaoJia where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_BaoJia where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_BaoJia where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "6")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_Kaidan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_Kaidan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_Kaidan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "7")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_HeTong where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_HeTong where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_HeTong where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "8")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ChuKu where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ChuKu where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ChuKu where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "9")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_TouSu where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_TouSu where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_TouSu where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "10")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_KeFu where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_KeFu where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_KeFu where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "11")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScXuqiu where datediff(dd,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScXuqiu where datediff(mm,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScXuqiu where datediff(yy,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "12")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScGongdan where datediff(dd,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScGongdan where datediff(mm,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScGongdan where datediff(yy,getdate(),Settime)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "13")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScLingliaodan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScLingliaodan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScLingliaodan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "14")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScTuiliaodan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScTuiliaodan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScTuiliaodan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "15")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScWanchengdan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScWanchengdan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScWanchengdan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "16")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScCaigouJihua where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScCaigouJihua where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScCaigouJihua where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "17")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_CaiGou where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_CaiGou where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_CaiGou where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "18")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_RuKu where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_RuKu where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_RuKu where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "19")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_RuKuZhiJie where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_RuKuZhiJie where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_RuKuZhiJie where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "20")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_RkYuanliaoZhiJie where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_RkYuanliaoZhiJie where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_RkYuanliaoZhiJie where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "21")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ChuKuZhiJie where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ChuKuZhiJie where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ChuKuZhiJie where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "22")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_CkYuanliaoZhiJie where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_CkYuanliaoZhiJie where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_CkYuanliaoZhiJie where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "23")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_Pandian where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_Pandian where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_Pandian where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "24")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_crm_ScRukudan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_crm_ScRukudan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_crm_ScRukudan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			return result;
		}
		public void InsertKhLog(string KhId, string Ziduan, string Yuanlai, string Xianzai)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_crm_KhLog (KhId,Username,Realname,Settime,Ziduan,Yuanlai,Xianzai) values ('",
				KhId,
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
		public void InsertLxrLog(string LxrId, string Ziduan, string Yuanlai, string Xianzai)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_crm_LxrLog (LxrId,Username,Realname,Settime,Ziduan,Yuanlai,Xianzai) values ('",
				LxrId,
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
		public string CRMsType(string str)
		{
			if (str == "1")
			{
				str = "客户价值评估";
			}
			if (str == "2")
			{
				str = "客户信用等级";
			}
			if (str == "3")
			{
				str = "客户种类";
			}
			if (str == "4")
			{
				str = "客户行业";
			}
			if (str == "5")
			{
				str = "客户关系等级";
			}
			if (str == "6")
			{
				str = "客户人员规模";
			}
			if (str == "7")
			{
				str = "客户来源";
			}
			if (str == "8")
			{
				str = "客户阶段";
			}
			if (str == "9")
			{
				str = "联系人分类";
			}
			if (str == "10")
			{
				str = "客户关怀类型";
			}
			if (str == "11")
			{
				str = "待办事宜类型";
			}
			if (str == "12")
			{
				str = "交往类型";
			}
			if (str == "13")
			{
				str = "销售机会来源";
			}
			if (str == "14")
			{
				str = "销售机会阶段";
			}
			if (str == "15")
			{
				str = "竞争对手能力";
			}
			if (str == "16")
			{
				str = "销售费用类别";
			}
			if (str == "17")
			{
				str = "合同订单分类";
			}
			if (str == "18")
			{
				str = "回款记录分类";
			}
			if (str == "19")
			{
				str = "票据类型";
			}
			if (str == "20")
			{
				str = "QA库分类";
			}
			if (str == "21")
			{
				str = "投诉分类";
			}
			if (str == "22")
			{
				str = "紧急程度";
			}
			if (str == "23")
			{
				str = "服务类型";
			}
			if (str == "24")
			{
				str = "服务方式";
			}
			if (str == "25")
			{
				str = "采购分类";
			}
			if (str == "26")
			{
				str = "投诉分类";
			}
			if (str == "27")
			{
				str = "市场活动类型";
			}
			if (str == "28")
			{
				str = "广告投放类型";
			}
			if (str == "29")
			{
				str = "供应商所属分类";
			}
			if (str == "30")
			{
				str = "供应商信用等级";
			}
			if (str == "31")
			{
				str = "付款方式";
			}
			if (str == "32")
			{
				str = "直接入库类型";
			}
			if (str == "33")
			{
				str = "直接出库类型";
			}
			if (str == "34")
			{
				str = "采购分类";
			}
			if (str == "35")
			{
				str = "工作报告类别";
			}
			if (str == "36")
			{
				str = "销售目标类型";
			}
			if (str == "37")
			{
				str = "维修故障分类";
			}
			if (str == "38")
			{
				str = "维修求助方式";
			}
			if (str == "39")
			{
				str = "物料类型";
			}
			if (str == "40")
			{
				str = "原材料类型";
			}
			return str;
		}
		public void GsCustomer()
		{
			string sql = "select * from qp_crm_Baohu";
			SqlDataReader list = this.List.GetList(sql);
			if (list.Read())
			{
				if (list["key1"].ToString() == "1")
				{
					string sql2 = string.Concat(new object[]
					{
						"Update qp_crm_Customer   Set Gonghai=1,Username='',Realname=''  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,JwTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["jiaowang"].ToString(),
						" and Gonghai='0'"
					});
					this.List.ExeSql(sql2);
					string sql3 = string.Concat(new object[]
					{
						"select id from qp_crm_Customer  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,JwTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["jiaowang"].ToString(),
						" and Gonghai='0'"
					});
					SqlDataReader list2 = this.List.GetList(sql3);
					while (list2.Read())
					{
						string sql4 = string.Concat(new object[]
						{
							"insert into qp_crm_Biangeng   (KhId,Neirong,Username,Realname,NowTimes) values ('",
							list2["id"].ToString(),
							"','未跟进，自动移除','",
							HttpContext.Current.Session["Username"],
							"','",
							HttpContext.Current.Session["Realname"],
							"','",
							DateTime.Now.ToString(),
							"')"
						});
						this.List.ExeSql(sql4);
					}
					list2.Close();
				}
				if (list["key2"].ToString() == "1")
				{
					string sql5 = string.Concat(new object[]
					{
						"Update qp_crm_Customer   Set Gonghai=1,Username='',Realname=''  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,HkTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["huikuan"].ToString(),
						" and Gonghai='0'"
					});
					this.List.ExeSql(sql5);
					string sql3 = string.Concat(new object[]
					{
						"select id from qp_crm_Customer  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,JwTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["jiaowang"].ToString(),
						" and Gonghai='0'"
					});
					SqlDataReader list2 = this.List.GetList(sql3);
					while (list2.Read())
					{
						string sql4 = string.Concat(new object[]
						{
							"insert into qp_crm_Biangeng   (KhId,Neirong,Username,Realname,NowTimes) values ('",
							list2["id"].ToString(),
							"','未回款，自动移除','",
							HttpContext.Current.Session["Username"],
							"','",
							HttpContext.Current.Session["Realname"],
							"','",
							DateTime.Now.ToString(),
							"')"
						});
						this.List.ExeSql(sql4);
					}
					list2.Close();
				}
				if (list["key3"].ToString() == "1")
				{
					string sql5 = string.Concat(new object[]
					{
						"Update qp_crm_Customer   Set Gonghai=1,Username='',Realname=''  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,QdTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["Qiandan"].ToString(),
						" and Gonghai='0'"
					});
					this.List.ExeSql(sql5);
					string sql3 = string.Concat(new object[]
					{
						"select id from qp_crm_Customer  where Username='",
						HttpContext.Current.Session["Username"],
						"' and datediff(dd,JwTime,'",
						DateTime.Now.ToShortDateString(),
						"')>",
						list["jiaowang"].ToString(),
						" and Gonghai='0'"
					});
					SqlDataReader list2 = this.List.GetList(sql3);
					while (list2.Read())
					{
						string sql4 = string.Concat(new object[]
						{
							"insert into qp_crm_Biangeng   (KhId,Neirong,Username,Realname,NowTimes) values ('",
							list2["id"].ToString(),
							"','未签单，自动移除','",
							HttpContext.Current.Session["Username"],
							"','",
							HttpContext.Current.Session["Realname"],
							"','",
							DateTime.Now.ToString(),
							"')"
						});
						this.List.ExeSql(sql4);
					}
					list2.Close();
				}
			}
			list.Close();
		}
		public string GetGongZuoLiu(string str)
		{
			if (str == "1")
			{
				str = "销售出库";
			}
			if (str == "2")
			{
				str = "采购入库";
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
		public string TypeCodeDr(string FileName, string Table, string CodeName)
		{
			string sql = string.Concat(new string[]
			{
				"select id from ",
				Table,
				" where ",
				FileName,
				"='",
				CodeName,
				"'"
			});
			SqlDataReader list = this.List.GetList(sql);
			string result;
			if (list.Read())
			{
				result = list["id"].ToString();
			}
			else
			{
				result = "0";
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
		public void Insertlsz(string Leixing, string CkId, string CpId, string RkShuliang, string CkShuliang, string Shujubiao, string ShujubiaoId)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_crm_KuFangLS (Leixing,CkId,CpId,RkShuliang,CkShuliang,Riqi,Zhuangtai,ZhixingrenUser,ZhixingrenName,Shujubiao,ShujubiaoId) values ('",
				Leixing,
				"','",
				CkId,
				"','",
				CpId,
				"','",
				RkShuliang,
				"','",
				CkShuliang,
				"','",
				DateTime.Now.ToShortDateString(),
				"','1','",
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
		public void Updatelsz(string Leixing, string Shujubiao, string ShujubiaoId)
		{
			string sql = string.Concat(new string[]
			{
				"Update qp_crm_KuFangLS  Set Zhuangtai=2  where Shujubiao='",
				Shujubiao,
				"' and ShujubiaoId='",
				ShujubiaoId,
				"' and Leixing='",
				Leixing,
				"'"
			});
			this.List.ExeSql(sql);
		}
		public void Updatelsz_sl(string Leixing, string RkShuliang, string CkShuliang, string Shujubiao, string ShujubiaoId)
		{
			string sql = string.Concat(new string[]
			{
				"Update qp_crm_KuFangLS  Set RkShuliang=",
				RkShuliang,
				",CkShuliang=",
				CkShuliang,
				"  where Shujubiao='",
				Shujubiao,
				"' and ShujubiaoId='",
				ShujubiaoId,
				"' and Leixing='",
				Leixing,
				"'"
			});
			this.List.ExeSql(sql);
		}
	}
}
