using qiupeng.publiccs;
using qiupeng.sql;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace qiupeng.ys
{
	public class divys
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
				if (type == "5")
				{
					string sql = "select * from qp_ys_Huankuan where id='" + id + "'";
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{还款人}", list["Realname"].ToString());
						AStr = AStr.Replace("{还款时间}", DateTime.Parse(list["NowTimes"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{办理状态}", list["LcZhuangtai"].ToString().Replace("1", "等待办理").Replace("2", "正在办理").Replace("3", "通过审批").Replace("4", "终止审批"));
						AStr = AStr.Replace("{标题}", list["Biaoti"].ToString());
						AStr = AStr.Replace("{还款金额}", list["Zongji"].ToString());
						AStr = AStr.Replace("{备注}", list["Beizhu"].ToString());
						string sql2 = "select BuMenId from qp_oa_username where username='" + list["username"].ToString() + "'";
						SqlDataReader list2 = this.List.GetList(sql2);
						if (list2.Read())
						{
							AStr = AStr.Replace("{还款人部门}", this.pulicss.TypeCode("qp_oa_Bumen", list2["BuMenId"].ToString()));
						}
						list2.Close();
						string text = "";
						text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">借款单</td><td align=\"center\">借款日期</td><td align=\"center\">借款金额</td><td align=\"center\">还时需还款金额</td><td align=\"center\">现收</td></tr>";
						string sql3 = "select A.* from qp_ys_HuankuanMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
						SqlDataReader list3 = this.List.GetList(sql3);
						while (list3.Read())
						{
							text += "<tr>";
							text = text + "<td>" + this.TypeCode("Biaoti", "qp_ys_Jiekuan", list3["JieKuanDan"].ToString()) + "&nbsp;</td>";
							text = text + "<td>" + list3["JieKuanRiqi"].ToString() + "&nbsp;</td>";
							text = text + "<td>" + list3["JieKuanJine"].ToString() + "&nbsp;</td>";
							text = text + "<td>" + list3["YiHuanJine"].ToString() + "&nbsp;</td>";
							text = text + "<td>" + list3["XianShou"].ToString() + "&nbsp;</td>";
							text += "</tr>";
						}
						list3.Close();
						text += "</table>";
						AStr = AStr.Replace("{还款明细表}", text);
					}
					list.Close();
				}
				if (type == "4")
				{
					string sql = "select * from qp_ys_Jiekuan where id='" + id + "'";
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{借款人}", list["Realname"].ToString());
						AStr = AStr.Replace("{借款时间}", DateTime.Parse(list["NowTimes"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{办理状态}", list["LcZhuangtai"].ToString().Replace("1", "等待办理").Replace("2", "正在办理").Replace("3", "通过审批").Replace("4", "终止审批"));
						AStr = AStr.Replace("{标题}", list["Biaoti"].ToString());
						AStr = AStr.Replace("{借款金额}", list["Zongji"].ToString());
						string sql2 = "select BuMenId from qp_oa_username where username='" + list["username"].ToString() + "'";
						SqlDataReader list2 = this.List.GetList(sql2);
						if (list2.Read())
						{
							AStr = AStr.Replace("{借款人部门}", this.pulicss.TypeCode("qp_oa_Bumen", list2["BuMenId"].ToString()));
						}
						list2.Close();
						AStr = AStr.Replace("{借款方式}", list["JiekuanFs"].ToString());
						AStr = AStr.Replace("{关联预算单}", this.TypeCode("Name", "qp_ys_Zhifu", list["ZhifuFs"].ToString()));
						AStr = AStr.Replace("{项目名称}", this.TypeCode("Name", "qp_ys_Xiangmu", list["Xiangmu"].ToString()));
						AStr = AStr.Replace("{客户名称}", this.TypeCode("ZhuJiJc", "qp_crm_Customer", list["Kehu"].ToString()));
						AStr = AStr.Replace("{预计还款日}", DateTime.Parse(list["Huankuanri"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{支付方式}", this.TypeCode("Name", "qp_ys_Zhifu", list["ZhifuFs"].ToString()));
						AStr = AStr.Replace("{原因}", list["Yuanyin"].ToString());
						AStr = AStr.Replace("{备注}", list["Beizhu"].ToString());
						if (list["JiekuanFs"].ToString() == "预算内")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">预算金额</td><td align=\"center\">借时可用金额</td><td align=\"center\">描述</td><td align=\"center\">借款金额</td></tr>";
							string sql3 = "select A.* from qp_ys_JiekuanMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["YusuanJine"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Keyongjine"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Miaosu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["JiekuanJine"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{借款明细表}", text);
						}
						if (list["JiekuanFs"].ToString() == "预算外")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">描述</td><td align=\"center\">借款金额</td></tr>";
							string sql3 = "select A.* from qp_ys_JiekuanMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + list3["Miaosu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["JiekuanJine"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{借款明细表}", text);
						}
					}
					list.Close();
				}
				if (type == "3")
				{
					string sql = "select * from qp_ys_Baoxiao where id='" + id + "'";
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{报销人}", list["Realname"].ToString());
						string sql2 = "select BuMenId from qp_oa_username where username='" + list["username"].ToString() + "'";
						SqlDataReader list2 = this.List.GetList(sql2);
						if (list2.Read())
						{
							AStr = AStr.Replace("{报销人部门}", this.pulicss.TypeCode("qp_oa_Bumen", list2["BuMenId"].ToString()));
						}
						list2.Close();
						AStr = AStr.Replace("{报销时间}", DateTime.Parse(list["NowTimes"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{办理状态}", list["LcZhuangtai"].ToString().Replace("1", "等待办理").Replace("2", "正在办理").Replace("3", "通过审批").Replace("4", "终止审批"));
						AStr = AStr.Replace("{标题}", list["Biaoti"].ToString());
						AStr = AStr.Replace("{支付方式}", this.TypeCode("Name", "qp_ys_Zhifu", list["ZhifuFs"].ToString()));
						AStr = AStr.Replace("{报销方式}", list["Fangshi"].ToString());
						AStr = AStr.Replace("{单据}", list["Danju"].ToString());
						AStr = AStr.Replace("{报销金额}", list["Zongji"].ToString());
						AStr = AStr.Replace("{备注}", list["Beizhu"].ToString());
						AStr = AStr.Replace("{冲抵金额}", list["ChongdiJine"].ToString());
						AStr = AStr.Replace("{应付金额}", list["YinfuJine"].ToString());
						AStr = AStr.Replace("{项目名称}", this.TypeCode("Name", "qp_ys_Xiangmu", list["Xiangmu"].ToString()));
						AStr = AStr.Replace("{客户名称}", this.TypeCode("ZhuJiJc", "qp_crm_Customer", list["Kehu"].ToString()));
						if (list["Fangshi"].ToString() == "预算内")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">描述</td><td align=\"center\">关联预算单</td><td align=\"center\">报销金额</td><td align=\"center\">预算金额</td><td align=\"center\">可用金额</td></tr>";
							string sql3 = "select A.* from qp_ys_BaoxiaoMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Miaosu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + this.TypeCode("Mingcheng", "qp_ys_Bianzhi", list3["YusuanId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["JiekuanJine"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["YusuanJine"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Keyongjine"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{报销明细表}", text);
						}
						if (list["Fangshi"].ToString() == "预算外")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">描述</td><td align=\"center\">报销金额</td></tr>";
							string sql3 = "select A.* from qp_ys_BaoxiaoMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Miaosu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["JiekuanJine"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{报销明细表}", text);
						}
						string text2 = "";
						text2 += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">借款单</td><td align=\"center\">借款日期</td><td align=\"center\">借款金额</td><td align=\"center\">需还款金额</td><td align=\"center\">冲抵金额</td></tr>";
						string sql4 = "select A.* from qp_ys_BaoxiaoCd as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
						SqlDataReader list4 = this.List.GetList(sql4);
						while (list4.Read())
						{
							text2 += "<tr>";
							text2 = text2 + "<td>" + this.TypeCode("Biaoti", "qp_ys_Jiekuan", list4["JieKuanDan"].ToString()) + "&nbsp;</td>";
							text2 = text2 + "<td>" + list4["JieKuanRiqi"].ToString() + "&nbsp;</td>";
							text2 = text2 + "<td>" + list4["JieKuanJine"].ToString() + "&nbsp;</td>";
							text2 = text2 + "<td>" + list4["YiHuanJine"].ToString() + "&nbsp;</td>";
							text2 = text2 + "<td>" + list4["XianShou"].ToString() + "&nbsp;</td>";
							text2 += "</tr>";
						}
						list4.Close();
						text2 += "</table>";
						AStr = AStr.Replace("{冲抵借款明细表}", text2);
					}
					list.Close();
				}
				if (type == "1")
				{
					string sql = "select * from qp_ys_Bianzhi where id='" + id + "'";
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{创建人}", list["Realname"].ToString());
						string sql2 = "select BuMenId from qp_oa_username where username='" + list["username"].ToString() + "'";
						SqlDataReader list2 = this.List.GetList(sql2);
						if (list2.Read())
						{
							AStr = AStr.Replace("{创建人部门}", this.pulicss.TypeCode("qp_oa_Bumen", list2["BuMenId"].ToString()));
						}
						list2.Close();
						AStr = AStr.Replace("{创建时间}", DateTime.Parse(list["NowTimes"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{办理状态}", list["LcZhuangtai"].ToString().Replace("1", "等待办理").Replace("2", "正在办理").Replace("3", "通过审批").Replace("4", "终止审批"));
						AStr = AStr.Replace("{编号}", list["Bianhao"].ToString());
						AStr = AStr.Replace("{名称}", list["Mingcheng"].ToString());
						AStr = AStr.Replace("{状态}", list["Zhuangtai"].ToString());
						AStr = AStr.Replace("{类型}", list["Leixing"].ToString());
						AStr = AStr.Replace("{周期}", list["Zhouqi"].ToString());
						AStr = AStr.Replace("{年份}", list["Nianfen"].ToString());
						AStr = AStr.Replace("{控制范围}", list["KzRealname"].ToString());
						AStr = AStr.Replace("{管理人员}", list["GlRealname"].ToString());
						AStr = AStr.Replace("{合计}", list["Zongji"].ToString());
						if (list["Zhouqi"].ToString() == "月")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">备注</td><td align=\"center\">1月</td><td align=\"center\">2月</td><td align=\"center\">3月</td><td align=\"center\">4月</td><td align=\"center\">5月</td><td align=\"center\">6月</td><td align=\"center\">7月</td><td align=\"center\">8月</td><td align=\"center\">9月</td><td align=\"center\">10月</td><td align=\"center\">11月</td><td align=\"center\">12月</td><td align=\"center\">合计</td></tr>";
							string sql3 = "select A.* from qp_ys_BianzhiMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan5"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan6"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan7"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan8"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan9"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan10"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan11"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan12"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Heji"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{预算单明细}", text);
						}
						if (list["Zhouqi"].ToString() == "季度")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">备注</td><td align=\"center\">一季度(1-3月)</td><td align=\"center\">二季度(4-6月)</td><td align=\"center\">三季度(7-9月)</td><td align=\"center\">四季度(10-12月)</td><td align=\"center\">合计</td></tr>";
							string sql3 = "select A.* from qp_ys_BianzhiMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Heji"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{预算单明细}", text);
						}
						if (list["Zhouqi"].ToString() == "年")
						{
							string text = "";
							text += "<table width=\"100%\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td align=\"center\">科目名称</td><td align=\"center\">备注</td><td align=\"center\">年预算</td></tr>";
							string sql3 = "select A.* from qp_ys_BianzhiMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{预算单明细}", text);
						}
					}
					list.Close();
				}
				if (type == "2")
				{
					string sql = "select * from qp_ys_Tiaozheng where id='" + id + "'";
					SqlDataReader list = this.List.GetList(sql);
					if (list.Read())
					{
						AStr = AStr.Replace("{创建人}", list["Realname"].ToString());
						string sql2 = "select BuMenId from qp_oa_username where username='" + list["username"].ToString() + "'";
						SqlDataReader list2 = this.List.GetList(sql2);
						if (list2.Read())
						{
							AStr = AStr.Replace("{创建人部门}", this.pulicss.TypeCode("qp_oa_Bumen", list2["BuMenId"].ToString()));
						}
						list2.Close();
						string text3 = this.TypeCode("Zhouqi", "qp_ys_Bianzhi", list["YuSuanId"].ToString());
						AStr = AStr.Replace("{创建时间}", DateTime.Parse(list["NowTimes"].ToString()).ToShortDateString());
						AStr = AStr.Replace("{办理状态}", list["LcZhuangtai"].ToString().Replace("1", "等待办理").Replace("2", "正在办理").Replace("3", "通过审批").Replace("4", "终止审批"));
						AStr = AStr.Replace("{标题}", list["Biaoti"].ToString());
						AStr = AStr.Replace("{关联预算单}", this.TypeCode("Mingcheng", "qp_ys_Bianzhi", list["YuSuanId"].ToString()));
						AStr = AStr.Replace("{周期}", text3);
						string text4 = this.TypeCode("Nianfen", "qp_ys_Bianzhi", list["YuSuanId"].ToString());
						AStr = AStr.Replace("{年份}", text4);
						AStr = AStr.Replace("{合计}", list["Zongji"].ToString());
						AStr = AStr.Replace("{备注}", list["Beizhu"].ToString());
						if (text3 == "月")
						{
							string text = "";
							string text5 = text;
							text = string.Concat(new string[]
							{
								text5,
								"<table border=\"1\" cellspacing=\"0\" bordercolor=\"#000000\" cellpadding=\"4\" width=\"100%\"><tr><td width=\"12%\" align=\"middle\" rowspan=\"2\">科目名称</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年1月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年2月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年3月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年4月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年5月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年6月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年7月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年8月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年9月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年10月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年11月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年12月</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年合计</td><td width=\"7%\" align=\"middle\" rowspan=\"2\">备注</td></tr>"
							});
							text += "<tr><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td><td  align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td></tr>";
							string sql3 = "select A.* from qp_ys_TiaozhengMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan5"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan5"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan6"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan6"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan7"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan7"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan8"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan8"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan9"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan9"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan10"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan10"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan11"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan11"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan12"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan12"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Heji"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Heji"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{调整明细表}", text);
						}
						if (text3 == "季度")
						{
							string text = "";
							string text5 = text;
							text = string.Concat(new string[]
							{
								text5,
								"<table border=\"1\" cellspacing=\"0\" bordercolor=\"#000000\" cellpadding=\"4\" width=\"100%\"><tr><td width=\"12%\" align=\"middle\" rowspan=\"2\">科目名称</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年一季度(1-3月)</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年二季度(4-6月)</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年三季度(7-9月)</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年四季度(10-12月)</td><td colspan=\"2\" align=\"middle\">",
								text4,
								"年合计</td><td width=\"7%\" align=\"middle\" rowspan=\"2\">备注</td></tr>"
							});
							text += "<tr><td width=\"9%\" align=\"middle\">原金额</td><td width=\"9%\" align=\"middle\">调整金额</td><td width=\"9%\" align=\"middle\">原金额</td><td width=\"9%\" align=\"middle\">调整金额</td><td width=\"8%\" align=\"middle\">原金额</td><td width=\"10%\" align=\"middle\">调整金额</td><td width=\"7%\" align=\"middle\">原金额</td><td width=\"7%\" align=\"middle\">调整金额</td><td width=\"7%\" align=\"middle\">原金额</td><td width=\"6%\" align=\"middle\">调整金额</td></tr>";
							string sql3 = "select A.* from qp_ys_TiaozhengMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan2"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan3"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan4"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Heji"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Heji"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{调整明细表}", text);
						}
						if (text3 == "年")
						{
							string text = "";
							text = text + "<table border=\"1\" cellspacing=\"0\" bordercolor=\"#000000\" cellpadding=\"4\" width=\"100%\"><tr><td width=\"12%\" align=\"middle\" rowspan=\"2\">科目名称</td><td colspan=\"2\" align=\"middle\">" + text4 + "年预算</td><td width=\"7%\" align=\"middle\" rowspan=\"2\">备注</td></tr>";
							text += "<tr><td align=\"middle\">原金额</td><td  align=\"middle\">调整金额</td></tr>";
							string sql3 = "select A.* from qp_ys_TiaozhengMx as [A]  where KeyNum='" + list["KeyNum"].ToString() + "' order by A.id asc";
							SqlDataReader list3 = this.List.GetList(sql3);
							while (list3.Read())
							{
								text += "<tr>";
								text = text + "<td>" + this.TypeCode("Name", "qp_ys_Kemu", list3["KemuId"].ToString()) + "&nbsp;</td>";
								text = text + "<td>" + list3["Y_Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Yusuan1"].ToString() + "&nbsp;</td>";
								text = text + "<td>" + list3["Beizhu"].ToString() + "&nbsp;</td>";
								text += "</tr>";
							}
							list3.Close();
							text += "</table>";
							AStr = AStr.Replace("{调整明细表}", text);
						}
					}
					list.Close();
				}
				result = AStr;
			}
			return result;
		}
		public void SetZhuti(TextBox TextBox, string leixing)
		{
			string sql = "select * from qp_ys_Bianhao where leixing='" + leixing + "'";
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
		public void Insertlsz(string XmId, string Leixing, string Jine, string Username, string Realname)
		{
			string sql = string.Concat(new string[]
			{
				"insert into qp_ys_JieKuanLS (XmId,Leixing,Jine,Riqi,Username,Realname) values ('",
				XmId,
				"','",
				Leixing,
				"','",
				Jine,
				"','",
				DateTime.Now.ToString(),
				"','",
				Username,
				"','",
				Realname,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public string TypeCodeYsKm(string FileName, string Table, string KemuId, string KeyNum)
		{
			string sql = string.Concat(new string[]
			{
				"select ",
				FileName,
				" from ",
				Table,
				" where KemuId='",
				KemuId,
				"' and KeyNum='",
				KeyNum,
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
					string sql = "select count(id) from qp_ys_Bianzhi where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_ys_Bianzhi where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_ys_Bianzhi where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "2")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_ys_Tiaozheng where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_ys_Tiaozheng where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_ys_Tiaozheng where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "3")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_ys_Baoxiao where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_ys_Baoxiao where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_ys_Baoxiao where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "4")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_ys_Jiekuan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_ys_Jiekuan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_ys_Jiekuan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			if (leixing == "5")
			{
				if (bianhao == "{DN}")
				{
					string sql = "select count(id) from qp_ys_Huankuan where datediff(dd,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{MN}")
				{
					string sql = "select count(id) from qp_ys_Huankuan where datediff(mm,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
				if (bianhao == "{YN}")
				{
					string sql = "select count(id) from qp_ys_Huankuan where datediff(yy,getdate(),NowTimes)=0";
					int num = this.List.GetCount(sql) + 1;
					string text = "" + num + "";
					result = "" + text.PadLeft(3, '0') + "";
				}
			}
			return result;
		}
	}
}
