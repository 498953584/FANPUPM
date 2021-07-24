using qiupeng.sql;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace qiupeng.publiccs
{
    public static class ProManagr 
    {
        public static string riqi;
    }
	public class publics
	{
		private Db List = new Db();
		private BindDrowDownList list = new BindDrowDownList();
		private static string Key = "ABCDEGHIJKLMNPQRSTUVXYZ";
		private static byte[] Keys = new byte[]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};
		public void Insert_KuFangCs()
		{
			DateTime dateTime = new DateTime(int.Parse(DateTime.Now.Year.ToString()), int.Parse(DateTime.Now.Month.ToString()), 1);
			DateTime dateTime2 = dateTime.AddMonths(-1);
			string sql = string.Concat(new string[]
			{
				"select  id from [qp_crm_KuFangCs] as [A] where Nianfen='",
				dateTime2.Year.ToString(),
				"' and Yuefen='",
				dateTime2.Month.ToString(),
				"'"
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (!sqlDataReader.Read())
			{
				string sql2 = "select A.id from [qp_crm_ChangPin] as [A] inner join [qp_crm_ChangPinLx] as [B] on [A].[typeid] = [B].[Id]  where A.ifdel='0'";
				SqlDataReader sqlDataReader2 = this.List.GetList(sql2);
				while (sqlDataReader2.Read())
				{
					string sql3 = string.Concat(new object[]
					{
						"insert into qp_crm_KuFangCs (Nianfen,Yuefen,Shuliang,CpId) values ('",
						dateTime2.Year.ToString(),
						"','",
						dateTime2.Month.ToString(),
						"','",
						this.XianKucun(sqlDataReader2["id"].ToString()),
						"','",
						sqlDataReader2["id"],
						"')"
					});
					this.List.ExeSql(sql3);
				}
				sqlDataReader2.Close();
			}
			sqlDataReader.Close();
		}
		private string XianKucun(string CpId)
		{
			string sql = "select sum(A.Shuliang) as AllShuliang from [qp_crm_KuFangLB] as [A] inner join [qp_crm_ChangPin] as [B] on [A].[CpId] = [B].id  where [A].[CpId] = '" + CpId + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			string result;
			if (sqlDataReader.Read())
			{
				if (sqlDataReader["AllShuliang"].ToString() == "")
				{
					result = "0";
				}
				else
				{
					result = sqlDataReader["AllShuliang"].ToString();
				}
			}
			else
			{
				result = "0";
			}
			sqlDataReader.Close();
			return result;
		}
		public bool IsNumberic(string str)
		{
			bool result;
			try
			{
				decimal num = Convert.ToDecimal(str);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public void ToExcel(Control ctl, string FileName)
		{
			HttpContext.Current.Response.Charset = "UTF-8";
			HttpContext.Current.Response.ContentEncoding = Encoding.Default;
			HttpContext.Current.Response.ContentType = "application/ms-excel";
			HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName + ".xls");
			ctl.Page.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			ctl.RenderControl(writer);
			HttpContext.Current.Response.Write(stringWriter.ToString());
			HttpContext.Current.Response.End();
		}
		public void QuanXianVis(Control ctl, string qxname, string qxstr)
		{
			if (!this.StrIFInStr(qxname, qxstr))
			{
				ctl.Visible = false;
			}
			else
			{
				ctl.Visible = true;
			}
		}
		public string CheckBoxChange(string str)
		{
			if (str == "True")
			{
				str = "1";
			}
			else
			{
				str = "0";
			}
			return str;
		}
		public string CheckText(string str)
		{
			if (str == "True")
			{
				str = "是";
			}
			else
			{
				str = "否";
			}
			return str;
		}
		public string CheckInt(string str)
		{
			if (str == "1")
			{
				str = "是";
			}
			else
			{
				str = "否";
			}
			return str;
		}
		public void QuanXianBack(string qxname, string qxstr)
		{
			if (!this.StrIFInStr(qxname, qxstr))
			{
				HttpContext.Current.Response.Redirect("/erqx.aspx");
			}
		}
		public bool ipquanstr(string Str1, string Str2)
		{
			return !(Str2 != "*") || Str2.IndexOf(Str1) >= 0;
		}
		public bool StrIFInStr(string Str1, string Str2)
		{
			return Str2.IndexOf(Str1) >= 0;
		}
		public string pdamainJk()
		{
			string result = null;
			string sql = "select ''+OALm+','+CRMLm+','+PmLm+'' as str from qp_pda_SysMk";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["str"].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public string pdamain(string str)
		{
			string result = null;
			string sql = "select " + str + " from qp_pda_SysMk";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["" + str + ""].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public string pdauser()
		{
			string text = null;
			string sql = "select A.id,A.Username, A.Realname from [qp_oa_username] as [A] inner join [qp_oa_Bumen] as [B] on [A].[BuMenId] = [B].[Id] inner join [qp_oa_Juese] as [C] on [A].[JueseId] = [C].[Id] inner join [qp_oa_Zhiwei] as [D] on [A].[Zhiweiid] = [D].[Id] where 1=1  and  ifdel='0' order by A.paixu asc";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			while (sqlDataReader.Read())
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"<li><input name=\"test\" type=\"checkbox\"  value=\"",
					sqlDataReader["Username"],
					"|",
					sqlDataReader["Realname"],
					"\"/>",
					sqlDataReader["Realname"],
					"</li>"
				});
			}
			sqlDataReader.Close();
			return text;
		}
		public string pdafoot(string str)
		{
			string str2 = null;
			string sql = "select * from qp_pda_SysMk";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				if (sqlDataReader["OA"].ToString() == "1")
				{
					str2 += "<li><a href=\"javascript:void(0)\" onclick=\"LoadingShow();location.href='/pda/main.aspx?leixing=1';\"><img src=\"/pda/images/1.png\" width=\"20\" height=\"19\" border=\"0\">OA</a></li>";
				}
				if (sqlDataReader["CRM"].ToString() == "1")
				{
					str2 += "<li><a href=\"javascript:void(0)\" onclick=\"LoadingShow();location.href='/pda/main.aspx?leixing=2';\"><img src=\"/pda/images/5.png\" width=\"20\" height=\"19\" border=\"0\">销售</a></li>";
				}
				if (sqlDataReader["PM"].ToString() == "1")
				{
					str2 += "<li><a href=\"javascript:void(0)\" onclick=\"LoadingShow();location.href='/pda/main.aspx?leixing=3';\"><img src=\"/pda/images/3.png\" width=\"20\" height=\"19\" border=\"0\">工程</a></li>";
				}
			}
			sqlDataReader.Close();
			str2 = str2 + "<li><a href=\"javascript:void(0)\" onclick=\"LoadingShow();location.href='" + str + "';\"><img src=\"/pda/images/2.png\" width=\"20\" height=\"19\" border=\"0\">刷新</a></li>";
			return str2 + "<li><a href=\"javascript:void(0)\" onclick=\"LoadingShow();location.href='/pda/LogOut.aspx';\"><img src=\"/pda/images/4.png\" width=\"20\" height=\"19\" border=\"0\">退出</a></li>";
		}
		public string fenye()
		{
			string sql = "select fenye from qp_oa_fenye where username='" + HttpContext.Current.Session["username"] + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			string result;
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["fenye"].ToString();
			}
			else
			{
				result = "15";
			}
			sqlDataReader.Close();
			return result;
		}
		public void addfenye(string yema)
		{
			string sql = "select fenye from qp_oa_fenye where username='" + HttpContext.Current.Session["username"] + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				string sql2 = string.Concat(new object[]
				{
					"Update qp_oa_fenye  Set fenye='",
					yema,
					"'  where Username='",
					HttpContext.Current.Session["username"],
					"'"
				});
				this.List.ExeSql(sql2);
			}
			else
			{
				string sql3 = string.Concat(new object[]
				{
					"insert into qp_oa_fenye (fenye,username) values ('",
					yema,
					"','",
					HttpContext.Current.Session["username"],
					"')"
				});
				this.List.ExeSql(sql3);
			}
			sqlDataReader.Close();
		}
		public bool scquanstr(string Str1, string Str2)
		{
			return !(Str2 != "*") || Str2.IndexOf(Str1) >= 0;
		}
		public bool StrCheckRight(string str)
		{
			return !(str == ".asp") && !(str == ".cer") && !(str == ".asa") && !(str == ".cdx") && !(str == ".htr") && !(str == ".cgi") && !(str == ".aspx") && !(str == ".asp.jpg") && !(str == ".cer.jpg") && !(str == ".asa.jpg") && !(str == ".cdx.jpg") && !(str == ".htr.jpg,") && !(str == ".cgi.jpg") && !(str == ".aspx.jpg");
		}
		public string GetFormatStr(string AStr)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("<", "〈");
				AStr = AStr.Replace(">", "〉");
				AStr = AStr.Replace("'", "’");
				AStr = AStr.Trim();
				result = AStr;
			}
			return result;
		}
		public string GetFormatStrbjq(string AStr)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("'", "’");
				result = AStr;
			}
			return result;
		}
		public string GetFormatStrbjq_show(string AStr)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("’", "'");
				result = AStr;
			}
			return result;
		}
		public string GetFormatStrmb(string AStr)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("'", "’");
				result = AStr;
			}
			return result;
		}
		public string TbToLb(string AStr)
		{
			string result;
			if ("" == AStr)
			{
				result = "";
			}
			else
			{
				AStr = AStr.Replace("\n", "<br>");
				AStr = AStr.Replace(" ", "&nbsp;&nbsp;");
				result = AStr;
			}
			return result;
		}
		public void BindListListBox(string Table, ListBox _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				this.BindChildListBox(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		private void BindChildListBox(string Table, string ParentID, string separator, ListBox _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=",
				ParentID,
				" ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = separator + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				string separator2 = separator + "---";
				this.BindChildListBox(Table, sqlDataReader["id"].ToString(), separator2, _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListGd(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			ListItem listItem = new ListItem();
			listItem.Text = "不归档";
			listItem.Value = "0";
			_DropDownList.Items.Add(listItem);
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem2 = new ListItem();
				listItem2.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem2.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem2);
				this.BindChild(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListNothing(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				this.BindChild(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListkong(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			ListItem listItem = new ListItem();
			listItem.Text = "";
			listItem.Value = "";
			_DropDownList.Items.Add(listItem);
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem2 = new ListItem();
				listItem2.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem2.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem2);
				this.BindChild(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListBm(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			ListItem listItem = new ListItem();
			listItem.Text = "根节点";
			listItem.Value = "0";
			_DropDownList.Items.Add(listItem);
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem2 = new ListItem();
				listItem2.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem2.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem2);
				this.BindChild(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		private void BindChild(string Table, string ParentID, string separator, DropDownList _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select id,Name from ",
				Table,
				" where ParentNodesID=",
				ParentID,
				" ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = separator + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				string separator2 = separator + "---";
				this.BindChild(Table, sqlDataReader["id"].ToString(), separator2, _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListNothingDisk(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select A.id,A.Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				this.BindChildDesk(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListkongDisk(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			ListItem listItem = new ListItem();
			listItem.Text = "";
			listItem.Value = "";
			_DropDownList.Items.Add(listItem);
			string sql2 = string.Concat(new string[]
			{
				"select A.id,A.Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem2 = new ListItem();
				listItem2.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem2.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem2);
				this.BindChildDesk(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public void BindListBmDisk(string Table, DropDownList _DropDownList, string sql, string sort)
		{
			ListItem listItem = new ListItem();
			listItem.Text = "根节点";
			listItem.Value = "0";
			_DropDownList.Items.Add(listItem);
			string sql2 = string.Concat(new string[]
			{
				"select A.id,A.Name from ",
				Table,
				" where ParentNodesID=0 ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem2 = new ListItem();
				listItem2.Text = "|-" + sqlDataReader["Name"].ToString();
				listItem2.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem2);
				this.BindChildDesk(Table, sqlDataReader["id"].ToString(), "|---", _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		private void BindChildDesk(string Table, string ParentID, string separator, DropDownList _DropDownList, string sql, string sort)
		{
			string sql2 = string.Concat(new string[]
			{
				"select A.id,A.Name from ",
				Table,
				" where ParentNodesID=",
				ParentID,
				" ",
				sql,
				" ",
				sort,
				""
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql2);
			while (sqlDataReader.Read())
			{
				ListItem listItem = new ListItem();
				listItem.Text = separator + sqlDataReader["Name"].ToString();
				listItem.Value = sqlDataReader["id"].ToString();
				_DropDownList.Items.Add(listItem);
				string separator2 = separator + "---";
				this.BindChildDesk(Table, sqlDataReader["id"].ToString(), separator2, _DropDownList, sql, sort);
			}
			sqlDataReader.Close();
		}
		public string GetGongZuoLiu(string strname)
		{
			string result = "";
			if (strname == "1")
			{
				result = "合同订单";
			}
			if (strname == "2")
			{
				result = "销售出库";
			}
			if (strname == "3")
			{
				result = "采购入库";
			}
			if (strname == "4")
			{
				result = "库存盘点";
			}
			if (strname == "5")
			{
				result = "销售费用";
			}
			if (strname == "6")
			{
				result = "采购订单";
			}
			if (strname == "7")
			{
				result = "合同流程";
			}
			if (strname == "8")
			{
				result = "会议申请流程";
			}
			if (strname == "9")
			{
				result = "用车申请流程";
			}
			if (strname == "10")
			{
				result = "手机短信申请流程";
			}
			if (strname == "11")
			{
				result = "资产购买申请流程";
			}
			if (strname == "12")
			{
				result = "资产报废申请流程";
			}
			if (strname == "13")
			{
				result = "资产借用申请流程";
			}
			if (strname == "14")
			{
				result = "资产使用申请流程";
			}
			if (strname == "15")
			{
				result = "资产维修申请流程";
			}
			if (strname == "16")
			{
				result = "出差管理流程";
			}
			if (strname == "17")
			{
				result = "休假管理流程";
			}
			if (strname == "18")
			{
				result = "加班管理流程";
			}
			if (strname == "19")
			{
				result = "报价审批流程";
			}
			if (strname == "20")
			{
				result = "投诉办理流程";
			}
			if (strname == "21")
			{
				result = "客服办理流程";
			}
			if (strname == "22")
			{
				result = "维修工单";
			}
			if (strname == "23")
			{
				result = "生产领料流程";
			}
			if (strname == "24")
			{
				result = "生产退料流程";
			}
			if (strname == "25")
			{
				result = "生产完工流程";
			}
			if (strname == "26")
			{
				result = "采购计划流程";
			}
			if (strname == "27")
			{
				result = "完工入库流程";
			}
			if (strname == "28")
			{
				result = "工程垫支流程";
			}
			if (strname == "29")
			{
				result = "支出申请流程";
			}
			if (strname == "30")
			{
				result = "材料预算流程";
			}
			if (strname == "31")
			{
				result = "采购计划流程";
			}
			if (strname == "32")
			{
				result = "采购订单流程";
			}
			if (strname == "33")
			{
				result = "材料入库流程";
			}
			if (strname == "34")
			{
				result = "材料领用流程";
			}
			if (strname == "35")
			{
				result = "材料调拨流程";
			}
			if (strname == "36")
			{
				result = "材料盘点流程";
			}
			if (strname == "37")
			{
				result = "运输车辆流程";
			}
			if (strname == "38")
			{
				result = "外包机械流程";
			}
			if (strname == "39")
			{
				result = "租赁材料流程";
			}
			if (strname == "40")
			{
				result = "设备领用流程";
			}
			if (strname == "41")
			{
				result = "设备归还流程";
			}
			if (strname == "42")
			{
				result = "资质领用流程";
			}
			if (strname == "43")
			{
				result = "资质归还流程";
			}
			if (strname == "44")
			{
				result = "甲供材料流程";
			}
			if (strname == "45")
			{
				result = "材料退库流程";
			}
			if (strname == "46")
			{
				result = "材料报损流程";
			}
			if (strname == "47")
			{
				result = "材料退货流程";
			}
			if (strname == "48")
			{
				result = "收入申请流程";
			}
			if (strname == "49")
			{
				result = "项目申请流程";
			}
			if (strname == "50")
			{
				result = "预算编制流程";
			}
			if (strname == "51")
			{
				result = "预算调整流程";
			}
			if (strname == "52")
			{
				result = "报销申请流程";
			}
			if (strname == "53")
			{
				result = "借款申请流程";
			}
			if (strname == "54")
			{
				result = "还款申请流程";
			}
			return result;
		}
		public void SpInsertLog(string BuZhouName, string GlNumber, string Yijian, string Username, string Realname, string Zhuangtai, string GlNum, string CaoZuo, string Endtime, string Leixing)
		{
			string sql = string.Concat(new string[]
			{
				"insert into qp_Pro_WorkFlowMsg (BuZhouName,GlNumber,Yijian,Username,Realname,Zhuangtai,Starttime,Endtime,GlNum,CaoZuo,Leixing) values ('",
				BuZhouName,
				"','",
				GlNumber,
				"','",
				Yijian,
				"','",
				Username,
				"','",
				Realname,
				"','",
				Zhuangtai,
				"','",
				DateTime.Now.ToString(),
				"','",
				Endtime,
				"','",
				GlNum,
				"','",
				CaoZuo,
				"','",
				Leixing,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public void SpUpdateLog(string GlNumber, string Yijian, string Zhuangtai, string GlNum, string CaoZuo, string Endtime)
		{
			if (Zhuangtai == "2")
			{
				string sql = string.Concat(new string[]
				{
					"Update qp_Pro_WorkFlowMsg  Set  Zhuangtai='",
					Zhuangtai,
					"',CaoZuo='",
					CaoZuo,
					"',Endtime='",
					Endtime,
					"' where GlNumber='",
					GlNumber,
					"' and  GlNum='",
					GlNum,
					"'"
				});
				this.List.ExeSql(sql);
			}
			else
			{
				if (Yijian.Trim() != "")
				{
					string sql = string.Concat(new object[]
					{
						"Update qp_Pro_WorkFlowMsg  Set  Yijian=Yijian+'",
						Yijian,
						"(",
						HttpContext.Current.Session["realname"],
						"-",
						DateTime.Now.ToString(),
						")<br>',Realname=replace(Realname,'",
						HttpContext.Current.Session["realname"],
						"','<font color=\"blue\">",
						HttpContext.Current.Session["realname"],
						"(",
						DateTime.Now.ToString(),
						")</font>'),Zhuangtai='",
						Zhuangtai,
						"',CaoZuo='",
						CaoZuo,
						"',Endtime='",
						Endtime,
						"' where GlNumber='",
						GlNumber,
						"' and  GlNum='",
						GlNum,
						"'"
					});
					this.List.ExeSql(sql);
				}
				else
				{
					string sql = string.Concat(new object[]
					{
						"Update qp_Pro_WorkFlowMsg  Set  Realname=replace(Realname,'",
						HttpContext.Current.Session["realname"],
						"','<font color=\"blue\">",
						HttpContext.Current.Session["realname"],
						"(",
						DateTime.Now.ToString(),
						")</font>'),Zhuangtai='",
						Zhuangtai,
						"',CaoZuo='",
						CaoZuo,
						"',Endtime='",
						Endtime,
						"' where GlNumber='",
						GlNumber,
						"' and  GlNum='",
						GlNum,
						"'"
					});
					this.List.ExeSql(sql);
				}
			}
		}
		public void InsertLog(string Name, string MkName)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_oa_SystemLog (Name,MkName,Username,Realname,Nowtimes,Ip,BuMenId) values ('",
				Name,
				"','",
				MkName,
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
		public void InsertNaoZhong(string titles, string txtime, string Types, string NbSms, string SjSms, string LjUrl)
		{
			string sql = string.Concat(new object[]
			{
				"insert into qp_oa_Naozhong (titles,txtime,Types,Iftx,NbSms,SjSms,Username,LjUrl) values ('",
				titles,
				"','",
				txtime,
				"','",
				Types,
				"','0','",
				NbSms,
				"','",
				SjSms,
				"','",
				HttpContext.Current.Session["username"],
				"','",
				LjUrl,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public void InsertNaoZhongSd(string titles, string txtime, string Types, string NbSms, string SjSms, string LjUrl, string User)
		{
			string sql = string.Concat(new string[]
			{
				"insert into qp_oa_Naozhong (titles,txtime,Types,Iftx,NbSms,SjSms,Username,LjUrl) values ('",
				titles,
				"','",
				txtime,
				"','",
				Types,
				"','0','",
				NbSms,
				"','",
				SjSms,
				"','",
				User,
				"','",
				LjUrl,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public void InsertMessage(string titles, string acceptusername, string acceptrealname, string url)
		{
			Random random = new Random();
			string text = random.Next(10000).ToString();
			string text2 = string.Concat(new string[]
			{
				"",
				DateTime.Now.Year.ToString(),
				"",
				DateTime.Now.Month.ToString(),
				"",
				DateTime.Now.Day.ToString(),
				"",
				DateTime.Now.Hour.ToString(),
				"",
				DateTime.Now.Minute.ToString(),
				"",
				DateTime.Now.Second.ToString(),
				"",
				DateTime.Now.Millisecond.ToString(),
				"",
				text,
				""
			});
			string sql = string.Concat(new object[]
			{
				"insert into qp_oa_Messages  (titles,itimes,acceptusername,acceptrealname,sendusername,sendrealname,sfck,url,number,tablekey) values ('",
				titles,
				"','",
				DateTime.Now.ToString(),
				"','",
				acceptusername,
				"','",
				acceptrealname,
				"','",
				HttpContext.Current.Session["username"],
				"','",
				HttpContext.Current.Session["realname"],
				"','0','",
				url,
				"','",
				text2,
				"','1')"
			});
			this.List.ExeSql(sql);
			string sql2 = string.Concat(new object[]
			{
				"insert into qp_oa_Messages  (titles,itimes,acceptusername,acceptrealname,sendusername,sendrealname,sfck,url,number,tablekey) values ('",
				titles,
				"','",
				DateTime.Now.ToString(),
				"','",
				acceptusername,
				"','",
				acceptrealname,
				"','",
				HttpContext.Current.Session["username"],
				"','",
				HttpContext.Current.Session["realname"],
				"','0','",
				url,
				"','",
				text2,
				"','2')"
			});
			this.List.ExeSql(sql2);
		}
		public void InsertSms(string MoveTel, string Msg)
		{
			if (MoveTel.Trim() != "")
			{
				string sql = string.Concat(new object[]
				{
					"insert into send_info   (s_mob,s_com,s_info,s_style,s_time,s_sendtime,s_userid,s_Client,s_Inputtime,s_realname) values ('",
					MoveTel,
					"','1','",
					Msg,
					"','0','",
					DateTime.Now.ToString(),
					"','','",
					HttpContext.Current.Session["username"],
					"','','",
					DateTime.Now.ToString(),
					"','",
					HttpContext.Current.Session["realname"],
					"')"
				});
				this.List.ExeSql(sql);
			}
		}
		public void InsertSmsSj(string MoveTel, string Msg, string Shijian)
		{
			if (MoveTel.Trim() != "")
			{
				if (HttpContext.Current.Session["duanxin"].ToString() == "1")
				{
					string sql = string.Concat(new object[]
					{
						"insert into send_info   (s_mob,s_com,s_info,s_style,s_time,s_sendtime,s_userid,s_Client,s_Inputtime,s_realname) values ('",
						MoveTel,
						"','1','",
						Msg,
						"','0','",
						Shijian,
						"','','",
						HttpContext.Current.Session["username"],
						"','','",
						DateTime.Now.ToString(),
						"','",
						HttpContext.Current.Session["realname"],
						"')"
					});
					this.List.ExeSql(sql);
				}
				else
				{
					HttpContext.Current.Response.Write("<script language=javascript>alert('手机短信未开通');</script>");
				}
			}
		}
		public void InsertSmsSjUser(string MoveTel, string Msg, string Shijian, string s_userid, string s_realname)
		{
			if (MoveTel.Trim() != "")
			{
				if (HttpContext.Current.Session["duanxin"].ToString() == "1")
				{
					string sql = string.Concat(new string[]
					{
						"insert into send_info   (s_mob,s_com,s_info,s_style,s_time,s_sendtime,s_userid,s_Client,s_Inputtime,s_realname) values ('",
						MoveTel,
						"','1','",
						Msg,
						"','0','",
						Shijian,
						"','','",
						s_userid,
						"','','",
						DateTime.Now.ToString(),
						"','",
						s_realname,
						"')"
					});
					this.List.ExeSql(sql);
				}
				else
				{
					HttpContext.Current.Response.Write("<script language=javascript>alert('手机短信未开通');</script>");
				}
			}
		}
		public int HuifangTime(string id)
		{
			int result = 0;
			if (id == "1")
			{
				string sql = "select * from qp_crm_Huifang";
				SqlDataReader sqlDataReader = this.List.GetList(sql);
				if (sqlDataReader.Read())
				{
					result = int.Parse(sqlDataReader["kehu"].ToString());
				}
				sqlDataReader.Close();
			}
			if (id == "2")
			{
				string sql = "select * from qp_crm_Huifang";
				SqlDataReader sqlDataReader = this.List.GetList(sql);
				if (sqlDataReader.Read())
				{
					result = int.Parse(sqlDataReader["jihui"].ToString());
				}
				sqlDataReader.Close();
			}
			if (id == "3")
			{
				string sql = "select * from qp_crm_Huifang";
				SqlDataReader sqlDataReader = this.List.GetList(sql);
				if (sqlDataReader.Read())
				{
					result = int.Parse(sqlDataReader["baojia"].ToString());
				}
				sqlDataReader.Close();
			}
			if (id == "4")
			{
				string sql = "select * from qp_crm_Huifang";
				SqlDataReader sqlDataReader = this.List.GetList(sql);
				if (sqlDataReader.Read())
				{
					result = int.Parse(sqlDataReader["jiaowang"].ToString());
				}
				sqlDataReader.Close();
			}
			return result;
		}
		public string Daibanshiyi_User(string Name, string UserId, string BuMenId)
		{
			string result = "0";
			if (Name == "当月订单数量")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_crm_HeTong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and  Year(QyShijian)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(QyShijian)='",
					DateTime.Now.Month.ToString(),
					"' and C.username='",
					UserId,
					"' and A.LcZhuangtai='3' and A.IfDel=0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月创建客户")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from  [qp_crm_Customer] as [A] inner join [qp_crm_DataFile] as [B] on [A].[JieDuan] = [B].id  inner join [qp_crm_DataFile] as [C] on [A].[ZhongLei] = [C].id where A.IfDel=0 and A.Username='",
					UserId,
					"' and  Year(SetTimes)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(SetTimes)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月创建商机")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from  [qp_crm_JiHui] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Jieduan] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='",
					UserId,
					"' and  Year(NowTimes)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(NowTimes)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月交往记录")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_crm_JiaoWang] as [A] inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='",
					UserId,
					"' and  Year(Shijian)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(Shijian)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访客户")
			{
				string sql = "select count(A.id) from  [qp_crm_Customer] as [A] inner join [qp_crm_DataFile] as [B] on [A].[JieDuan] = [B].id  inner join [qp_crm_DataFile] as [C] on [A].[ZhongLei] = [C].id where A.IfDel=0 and A.Username='" + UserId + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访销售机会")
			{
				string sql = "select count(A.id) from  [qp_crm_JiHui] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Jieduan] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + UserId + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访报价单")
			{
				string sql = "select count(A.id) from  [qp_crm_BaoJia] as [A]   inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + UserId + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访交往记录")
			{
				string sql = "select count(A.id) from [qp_crm_JiaoWang] as [A] inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + UserId + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读新闻")
			{
				string sql = "select count(id) from qp_oa_NewsMg where CHARINDEX('," + UserId + ",',','+YdUsername+',')   =0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读消息")
			{
				string sql = "select count(id) from qp_oa_Messages where sfck='0' and acceptusername='" + UserId + "' and tablekey='1'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读邮件")
			{
				string sql = "select count(id) from qp_oa_NbEmail_sj where  IfRead='0' and  IfDel='0' and  JsUsername='" + UserId + "'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "文件接收")
			{
				string sql = string.Concat(new string[]
				{
					"select count(id) from qp_oa_InfoSend where  CHARINDEX(',",
					UserId,
					",',','+JsUsername+',') > 0 and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日日程")
			{
				string sql = string.Concat(new string[]
				{
					"select count(id) from qp_oa_MyRicheng where username='",
					UserId,
					"'  and convert(char(10),cast(StartTime as datetime),120)=convert(char(10),cast('",
					DateTime.Now.ToShortDateString(),
					"' as datetime),120)"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日会议")
			{
				string sql = string.Concat(new string[]
				{
					"select count(id) from qp_oa_MettingApply where  (State='2' or State='3'  or State='4' or State='5' ) and   CHARINDEX(',",
					UserId,
					",',','+NbPeopleUser+',')   >   0   and convert(char(10),cast(Starttime as datetime),120)=convert(char(10),cast('",
					DateTime.Now.ToShortDateString(),
					"' as datetime),120)"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读通知")
			{
				string sql = string.Concat(new string[]
				{
					"select count(id) from qp_oa_BumenNewsMg where  ((CHARINDEX(',",
					BuMenId,
					",',','+ZdBumenId+',') > 0 and States='2') or (CHARINDEX(',",
					UserId,
					",',','+ZdUsername+',') > 0 and States='3') or (States='1')) and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "部门主页")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_oa_BumenWz] as [A] inner join [qp_oa_BumenZy] as [C] on [A].[ZhuyeId] = [C].[Id] inner join [qp_oa_Bumen] as [B] on [C].[BuMenId] = [B].[Id] inner join [qp_oa_BumenWzLb] as [D] on [A].[WzLeibie] = [D].[Id]  where ((CHARINDEX(',",
					BuMenId,
					",',','+C.ZdBumenId1+',') > 0 and C.States1='2') or (CHARINDEX(',",
					UserId,
					",',','+C.ZdUsername1+',') > 0 and C.States1='3') or (C.States1='1')) and CHARINDEX(',",
					UserId,
					",',','+A.YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "会议审批")
			{
				string sql = "select count(id) from qp_oa_MettingApply where  CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "车辆审批")
			{
				string sql = "select count(A.id) from [qp_oa_CarApply] as [A] join [qp_oa_CarInfo] as [C] on [A].[CarId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "计划监控")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_oa_MyPlan] as [A] inner join [qp_oa_MyPlanLx] as [B] on [A].[Leixing] = [B].[Id] where 1=1 and  CHARINDEX(','+A.Username+',',','+(select RyUsername from qp_oa_MyPlanSz where ZgUsername='",
					UserId,
					"')+',') > 0 and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "日志批注")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_oa_MyRizhi] as [A] inner join [qp_oa_RizhiLx] as [B] on [A].[LeiBie] = [B].[Id]  where CHARINDEX(','+Username+',',','+(select RyUsername from qp_oa_Rizhisz where ZgUsername='",
					UserId,
					"')+',') > 0 and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "任务监控")
			{
				int num = 0;
				string sql2 = string.Concat(new string[]
				{
					"select A.WcTime,A.Benbi,A.id,A.titles,A.Starttime,A.Endtime,A.ZbUsername,A.ZbRealname,A.Leixing,A.Benbi,A.State,A.Username,A.Realname,A.SetTime, B.[name] as LeiBieName from [qp_oa_Renwu] as [A] inner join [qp_oa_RenwuLx] as [B] on [A].[Leixing] = [B].[Id] left join [qp_oa_RenwuXb] as [C] on [A].[id] = [C].[Keyid]  where ((CHARINDEX(','+A.ZbUsername+',',','+(select RyUsername from qp_oa_Renwusz where ZgUsername='",
					UserId,
					"')+',') > 0) or (CHARINDEX(','+C.Username+',',','+(select RyUsername from qp_oa_Renwusz where ZgUsername='",
					UserId,
					"')+',') > 0)) and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0  group by  A.WcTime,A.Benbi,A.id,A.titles,A.Starttime,A.Endtime,A.ZbUsername,A.ZbRealname,A.Leixing,A.Benbi,A.State,A.Username,A.Realname,A.SetTime,B.[name] "
				});
				SqlDataReader sqlDataReader = this.List.GetList(sql2);
				while (sqlDataReader.Read())
				{
					num++;
				}
				sqlDataReader.Close();
				result = "" + num + "";
			}
			if (Name == "待办流程")
			{
				string sql = "select count(A.id)  from [qp_oa_AddWorkFlow] as [A] inner join [qp_oa_AddWorkFlowPicRy] as [C] on [A].[Number] = [C].KeyFile and [A].[UpNodeNum] = [C].xuhao and [A].[GlNum] = [C].GlNum where BLusername='" + UserId + "' and ((States='未接收' and State='正在办理') or (States='办理中' and State='正在办理')) and Ifdel='0'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产购买审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResGm] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产报废审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResBf] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产借用审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResAppJy] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产使用审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResAppSy] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产维修审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResWx] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "合同订单审批")
			{
				string sql = "select count(A.id) from [qp_crm_HeTong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2') and A.IfDel=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "销售出库审批")
			{
				string sql = "select count(A.id) from [qp_crm_ChuKu] as [A] inner join [qp_crm_KuFang] as [B] on [A].[CkId] = [B].id   inner join [qp_crm_HeTong] as [D] on [A].[HtDd] = [D].id  inner join [qp_crm_Customer] as [C] on [D].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "采购订单审批")
			{
				string sql = "select count(A.id) from [qp_crm_CaiGou] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_GongyinShang] as [C] on [A].[GysId] = [C].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2') and  C.IfDel=0 and A.IfDel=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "采购入库审批")
			{
				string sql = "select count(A.id) from [qp_crm_RuKu] as [A] inner join [qp_crm_CaiGou] as [D] on [A].[CgId] = [D].id  inner join [qp_crm_GongyinShang] as [C] on [D].[GysId] = [C].id where C.IfDel=0 and  CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "库存盘点审批")
			{
				string sql = "select count(A.id) from [qp_crm_Pandian] as [A] where 1=1 and CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "销售费用审批")
			{
				string sql = "select count(A.id) from [qp_crm_FeiYong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Leibie] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读工程通知")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from  [qp_pm_Gonggao] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [D].[Leibie] = [C].id join [qp_pm_ProUser] as [E] on [A].[XmId] = [E].XmId where E.Username='",
					UserId,
					"' and (CHARINDEX('aaaa1j','|'+Quanxian+'|')   >   0 ) and CHARINDEX(',",
					UserId,
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程支出申请")
			{
				string sql = "select count(A.id) from [qp_pm_Feiyong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [A].[Leibie] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程收入申请")
			{
				string sql = "select count(A.id) from [qp_pm_Shouru] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [A].[Leibie] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程甲供材料")
			{
				string sql = "select count(A.id) from [qp_pm_Jiagong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料预算")
			{
				string sql = "select count(A.id) from [qp_pm_YsCheliang] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程采购计划")
			{
				string sql = "select count(A.id) from [qp_pm_Jihua] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程采购订单")
			{
				string sql = "select count(A.id) from [qp_pm_Dingdan] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料入库")
			{
				string sql = "select count(A.id) from [qp_pm_Ruku] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料领用")
			{
				string sql = "select count(A.id) from [qp_pm_Lingyong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料报损")
			{
				string sql = "select count(A.id) from [qp_pm_Baosun] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料退货")
			{
				string sql = "select count(A.id) from [qp_pm_Tuihuo] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料调拨")
			{
				string sql = "select count(A.id) from [qp_pm_Diaobo] as [A] join [qp_pm_ProInfo] as [G] on [A].[DrXmId] = [G].id join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id join [qp_pm_Cangku] as [H] on [A].[DrCangku] = [H].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料盘点")
			{
				string sql = "select count(A.id) from [qp_pm_Pandian] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程运输车辆")
			{
				string sql = "select count(A.id) from [qp_pm_YsCheliang] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程外包机械")
			{
				string sql = "select count(A.id) from [qp_pm_WbJixie] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程租赁材料")
			{
				string sql = "select count(A.id) from [qp_pm_WbCailiao] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程设备领用")
			{
				string sql = "select count(A.id) from [qp_pm_ShebeiLy] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Shebei] as [C] on [A].[ShebeiId] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程设备归还")
			{
				string sql = "select count(A.id) from [qp_pm_ShebeiGh] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Shebei] as [C] on [A].[ShebeiId] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程资质领用")
			{
				string sql = "select count(A.id) from [qp_pm_ZizhiLy] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Zizhi] as [C] on [A].[ZizhiId] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程资质归还")
			{
				string sql = "select count(A.id) from [qp_pm_ZizhiGh] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Zizhi] as [C] on [A].[ZizhiId] = [C].id where CHARINDEX('," + UserId + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "项目提醒")
			{
				string sql = string.Concat(new string[]
				{
					"select count(A.id) from [qp_pm_Tixing]  as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProUser] as [E] on [A].[XmId] = [E].XmId where A.Username='",
					UserId,
					"' and E.Username='",
					UserId,
					"' and (CHARINDEX('aaaa1n','|'+Quanxian+'|')   >   0 ) and datediff(dd,getdate(),A.TxTime)=0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "预算编制审批")
			{
				string sql = "select count(A.id) from [qp_ys_Bianzhi] as [A] where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "预算调整审批")
			{
				string sql = "select count(A.id) from [qp_ys_Tiaozheng] as [A] join [qp_ys_Bianzhi] as [D] on [A].[YuSuanId] = [D].id where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "报销审批")
			{
				string sql = "select count(A.id) from [qp_ys_Baoxiao] as [A] where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "借款审批")
			{
				string sql = "select count(A.id) from [qp_ys_Jiekuan] as [A] where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "还款审批")
			{
				string sql = "select count(A.id) from [qp_ys_Huankuan] as [A] where CHARINDEX('," + UserId + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			return result;
		}
		public string Daibanshiyi(string Name)
		{
			string result = "0";
			if (Name == "当月订单数量")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_crm_HeTong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and  Year(QyShijian)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(QyShijian)='",
					DateTime.Now.Month.ToString(),
					"' and C.username='",
					HttpContext.Current.Session["username"],
					"' and A.LcZhuangtai='3' and A.IfDel=0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月创建客户")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from  [qp_crm_Customer] as [A] inner join [qp_crm_DataFile] as [B] on [A].[JieDuan] = [B].id  inner join [qp_crm_DataFile] as [C] on [A].[ZhongLei] = [C].id where A.IfDel=0 and A.Username='",
					HttpContext.Current.Session["Username"],
					"' and  Year(SetTimes)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(SetTimes)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月创建商机")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from  [qp_crm_JiHui] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Jieduan] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='",
					HttpContext.Current.Session["username"],
					"' and  Year(NowTimes)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(NowTimes)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "当月交往记录")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_crm_JiaoWang] as [A] inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='",
					HttpContext.Current.Session["username"],
					"' and  Year(Shijian)='",
					DateTime.Now.Year.ToString(),
					"' and  Month(Shijian)='",
					DateTime.Now.Month.ToString(),
					"'"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访客户")
			{
				string sql = "select count(A.id) from  [qp_crm_Customer] as [A] inner join [qp_crm_DataFile] as [B] on [A].[JieDuan] = [B].id  inner join [qp_crm_DataFile] as [C] on [A].[ZhongLei] = [C].id where A.IfDel=0 and A.Username='" + HttpContext.Current.Session["Username"] + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访销售机会")
			{
				string sql = "select count(A.id) from  [qp_crm_JiHui] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Jieduan] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + HttpContext.Current.Session["username"] + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访报价单")
			{
				string sql = "select count(A.id) from  [qp_crm_BaoJia] as [A]   inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + HttpContext.Current.Session["username"] + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日回访交往记录")
			{
				string sql = "select count(A.id) from [qp_crm_JiaoWang] as [A] inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and C.Username='" + HttpContext.Current.Session["username"] + "' and datediff(dd,getdate(),A.HfTime)=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读新闻")
			{
				string sql = "select count(id) from qp_oa_NewsMg where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+YdUsername+',')   =0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读消息")
			{
				string sql = "select count(id) from qp_oa_Messages where sfck='0' and acceptusername='" + HttpContext.Current.Session["username"] + "' and tablekey='1'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读邮件")
			{
				string sql = "select count(id) from qp_oa_NbEmail_sj where  IfRead='0' and  IfDel='0' and  JsUsername='" + HttpContext.Current.Session["username"] + "'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "文件接收")
			{
				string sql = string.Concat(new object[]
				{
					"select count(id) from qp_oa_InfoSend where  CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+JsUsername+',') > 0 and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日日程")
			{
				string sql = string.Concat(new object[]
				{
					"select count(id) from qp_oa_MyRicheng where username='",
					HttpContext.Current.Session["username"],
					"'  and convert(char(10),cast(StartTime as datetime),120)=convert(char(10),cast('",
					DateTime.Now.ToShortDateString(),
					"' as datetime),120)"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "今日会议")
			{
				string sql = string.Concat(new object[]
				{
					"select count(id) from qp_oa_MettingApply where  (State='2' or State='3'  or State='4' or State='5' ) and   CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+NbPeopleUser+',')   >   0   and convert(char(10),cast(Starttime as datetime),120)=convert(char(10),cast('",
					DateTime.Now.ToShortDateString(),
					"' as datetime),120)"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读通知")
			{
				string sql = string.Concat(new object[]
				{
					"select count(id) from qp_oa_BumenNewsMg where  ((CHARINDEX(',",
					HttpContext.Current.Session["BuMenId"],
					",',','+ZdBumenId+',') > 0 and States='2') or (CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+ZdUsername+',') > 0 and States='3') or (States='1')) and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "部门主页")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_oa_BumenWz] as [A] inner join [qp_oa_BumenZy] as [C] on [A].[ZhuyeId] = [C].[Id] inner join [qp_oa_Bumen] as [B] on [C].[BuMenId] = [B].[Id] inner join [qp_oa_BumenWzLb] as [D] on [A].[WzLeibie] = [D].[Id]  where ((CHARINDEX(',",
					HttpContext.Current.Session["BuMenId"],
					",',','+C.ZdBumenId1+',') > 0 and C.States1='2') or (CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+C.ZdUsername1+',') > 0 and C.States1='3') or (C.States1='1')) and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+A.YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "会议审批")
			{
				string sql = "select count(id) from qp_oa_MettingApply where  CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "车辆审批")
			{
				string sql = "select count(A.id) from [qp_oa_CarApply] as [A] join [qp_oa_CarInfo] as [C] on [A].[CarId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "计划监控")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_oa_MyPlan] as [A] inner join [qp_oa_MyPlanLx] as [B] on [A].[Leixing] = [B].[Id] where 1=1 and  CHARINDEX(','+A.Username+',',','+(select RyUsername from qp_oa_MyPlanSz where ZgUsername='",
					HttpContext.Current.Session["username"],
					"')+',') > 0 and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "日志批注")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_oa_MyRizhi] as [A] inner join [qp_oa_RizhiLx] as [B] on [A].[LeiBie] = [B].[Id]  where CHARINDEX(','+Username+',',','+(select RyUsername from qp_oa_Rizhisz where ZgUsername='",
					HttpContext.Current.Session["username"],
					"')+',') > 0 and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "任务监控")
			{
				int num = 0;
				string sql2 = string.Concat(new object[]
				{
					"select A.WcTime,A.Benbi,A.id,A.titles,A.Starttime,A.Endtime,A.ZbUsername,A.ZbRealname,A.Leixing,A.Benbi,A.State,A.Username,A.Realname,A.SetTime, B.[name] as LeiBieName from [qp_oa_Renwu] as [A] inner join [qp_oa_RenwuLx] as [B] on [A].[Leixing] = [B].[Id] left join [qp_oa_RenwuXb] as [C] on [A].[id] = [C].[Keyid]  where ((CHARINDEX(','+A.ZbUsername+',',','+(select RyUsername from qp_oa_Renwusz where ZgUsername='",
					HttpContext.Current.Session["username"],
					"')+',') > 0) or (CHARINDEX(','+C.Username+',',','+(select RyUsername from qp_oa_Renwusz where ZgUsername='",
					HttpContext.Current.Session["username"],
					"')+',') > 0)) and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0  group by  A.WcTime,A.Benbi,A.id,A.titles,A.Starttime,A.Endtime,A.ZbUsername,A.ZbRealname,A.Leixing,A.Benbi,A.State,A.Username,A.Realname,A.SetTime,B.[name] "
				});
				SqlDataReader sqlDataReader = this.List.GetList(sql2);
				while (sqlDataReader.Read())
				{
					num++;
				}
				sqlDataReader.Close();
				result = "" + num + "";
			}
			if (Name == "待办流程")
			{
				string sql = "select count(A.id)  from [qp_oa_AddWorkFlow] as [A] inner join [qp_oa_AddWorkFlowPicRy] as [C] on [A].[Number] = [C].KeyFile and [A].[UpNodeNum] = [C].xuhao and [A].[GlNum] = [C].GlNum where BLusername='" + HttpContext.Current.Session["username"] + "' and ((States='未接收' and State='正在办理') or (States='办理中' and State='正在办理')) and Ifdel='0'";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产购买审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResGm] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产报废审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResBf] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产借用审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResAppJy] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产使用审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResAppSy] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "资产维修审批")
			{
				string sql = "select count(A.id) from [qp_oa_ResWx] as [A] join [qp_oa_ResourcesAdd] as [C] on [A].[ZyId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "合同订单审批")
			{
				string sql = "select count(A.id) from [qp_crm_HeTong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2') and A.IfDel=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "销售出库审批")
			{
				string sql = "select count(A.id) from [qp_crm_ChuKu] as [A] inner join [qp_crm_KuFang] as [B] on [A].[CkId] = [B].id   inner join [qp_crm_HeTong] as [D] on [A].[HtDd] = [D].id  inner join [qp_crm_Customer] as [C] on [D].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "采购订单审批")
			{
				string sql = "select count(A.id) from [qp_crm_CaiGou] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Fenlei] = [B].id  inner join [qp_crm_GongyinShang] as [C] on [A].[GysId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2') and  C.IfDel=0 and A.IfDel=0";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "采购入库审批")
			{
				string sql = "select count(A.id) from [qp_crm_RuKu] as [A] inner join [qp_crm_CaiGou] as [D] on [A].[CgId] = [D].id  inner join [qp_crm_GongyinShang] as [C] on [D].[GysId] = [C].id where C.IfDel=0 and  CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "库存盘点审批")
			{
				string sql = "select count(A.id) from [qp_crm_Pandian] as [A] where 1=1 and CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "销售费用审批")
			{
				string sql = "select count(A.id) from [qp_crm_FeiYong] as [A] inner join [qp_crm_DataFile] as [B] on [A].[Leibie] = [B].id  inner join [qp_crm_Customer] as [C] on [A].[KhId] = [C].id where C.IfDel=0 and CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "未读工程通知")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from  [qp_pm_Gonggao] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [D].[Leibie] = [C].id join [qp_pm_ProUser] as [E] on [A].[XmId] = [E].XmId where E.Username='",
					HttpContext.Current.Session["username"],
					"' and (CHARINDEX('aaaa1j','|'+Quanxian+'|')   >   0 ) and CHARINDEX(',",
					HttpContext.Current.Session["username"],
					",',','+YdUsername+',')   =0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程支出申请")
			{
				string sql = "select count(A.id) from [qp_pm_Feiyong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [A].[Leibie] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程收入申请")
			{
				string sql = "select count(A.id) from [qp_pm_Shouru] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProData] as [C] on [A].[Leibie] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程甲供材料")
			{
				string sql = "select count(A.id) from [qp_pm_Jiagong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料预算")
			{
				string sql = "select count(A.id) from [qp_pm_YsCheliang] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程采购计划")
			{
				string sql = "select count(A.id) from [qp_pm_Jihua] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程采购订单")
			{
				string sql = "select count(A.id) from [qp_pm_Dingdan] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料入库")
			{
				string sql = "select count(A.id) from [qp_pm_Ruku] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料领用")
			{
				string sql = "select count(A.id) from [qp_pm_Lingyong] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料报损")
			{
				string sql = "select count(A.id) from [qp_pm_Baosun] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料退货")
			{
				string sql = "select count(A.id) from [qp_pm_Tuihuo] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料调拨")
			{
				string sql = "select count(A.id) from [qp_pm_Diaobo] as [A] join [qp_pm_ProInfo] as [G] on [A].[DrXmId] = [G].id join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id join [qp_pm_Cangku] as [H] on [A].[DrCangku] = [H].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程材料盘点")
			{
				string sql = "select count(A.id) from [qp_pm_Pandian] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id join [qp_pm_Cangku] as [F] on [A].[Cangku] = [F].id  where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
            if (Name == "租赁计划单")
			{
				string sql = "select count(A.id) from [qp_pm_YsCheliang] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
            if (Name == "租赁租用单")
			{
				string sql = "select count(A.id) from [qp_pm_WbJixie] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
            if (Name == "租赁退租单")
			{
				string sql = "select count(A.id) from [qp_pm_WbCailiao] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程设备领用")
			{
				string sql = "select count(A.id) from [qp_pm_ShebeiLy] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Shebei] as [C] on [A].[ShebeiId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程设备归还")
			{
				string sql = "select count(A.id) from [qp_pm_ShebeiGh] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Shebei] as [C] on [A].[ShebeiId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程资质领用")
			{
				string sql = "select count(A.id) from [qp_pm_ZizhiLy] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Zizhi] as [C] on [A].[ZizhiId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "工程资质归还")
			{
				string sql = "select count(A.id) from [qp_pm_ZizhiGh] as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_Zizhi] as [C] on [A].[ZizhiId] = [C].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+DqBlUsername+',')   >0 and (LcZhuangtai='1' or LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "项目提醒")
			{
				string sql = string.Concat(new object[]
				{
					"select count(A.id) from [qp_pm_Tixing]  as [A] join [qp_pm_ProInfo] as [D] on [A].[XmId] = [D].id  join [qp_pm_ProUser] as [E] on [A].[XmId] = [E].XmId where A.Username='",
					HttpContext.Current.Session["username"],
					"' and E.Username='",
					HttpContext.Current.Session["username"],
					"' and (CHARINDEX('aaaa1n','|'+Quanxian+'|')   >   0 ) and datediff(dd,getdate(),A.TxTime)=0"
				});
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "预算编制审批")
			{
				string sql = "select count(A.id) from [qp_ys_Bianzhi] as [A] where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "预算调整审批")
			{
				string sql = "select count(A.id) from [qp_ys_Tiaozheng] as [A] join [qp_ys_Bianzhi] as [D] on [A].[YuSuanId] = [D].id where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "报销审批")
			{
				string sql = "select count(A.id) from [qp_ys_Baoxiao] as [A] where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "借款审批")
			{
				string sql = "select count(A.id) from [qp_ys_Jiekuan] as [A] where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			if (Name == "还款审批")
			{
				string sql = "select count(A.id) from [qp_ys_Huankuan] as [A] where CHARINDEX('," + HttpContext.Current.Session["username"] + ",',','+A.DqBlUsername+',')   >0 and (A.LcZhuangtai='1' or A.LcZhuangtai='2')";
				result = "" + this.List.GetCount(sql) + "";
			}
			return result;
		}
		public string JiFenDengji(string Username)
		{
			string result = null;
			string sql = "select top 18 jifen,username,realname from qp_oa_username where username='" + Username + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				string sql2 = string.Concat(new object[]
				{
					"select dengji from qp_oa_Zst_dengji where  ",
					sqlDataReader["jifen"],
					">=Fenshu1 and  ",
					sqlDataReader["jifen"],
					"<=Fenshu2"
				});
				SqlDataReader sqlDataReader2 = this.List.GetList(sql2);
				if (sqlDataReader2.Read())
				{
					result = "d_" + sqlDataReader2["dengji"] + ".gif";
				}
				sqlDataReader2.Close();
			}
			sqlDataReader.Close();
			return result;
		}
		public string Getyangshi()
		{
			string sql = "select * from qp_oa_yangshi where Username='" + HttpContext.Current.Session["username"] + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			string result;
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["name"].ToString();
			}
			else
			{
				result = "bluecss";
			}
			sqlDataReader.Close();
			return result;
		}
		public string GetSms()
		{
			string sql = "select * from qp_oa_smssy";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			string result;
			if (sqlDataReader.Read())
			{
				result = "0," + sqlDataReader["content"].ToString() + "";
			}
			else
			{
				result = "0";
			}
			sqlDataReader.Close();
			return result;
		}
		public string SystemCode(string FieldName, string CodeId)
		{
			string result = null;
			string sql = string.Concat(new string[]
			{
				"select * from SystemCode where FieldName='",
				FieldName,
				"' and  CodeId='",
				CodeId,
				"'"
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["CodeName"].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public string JifenGuize(string Leibie)
		{
			string result = null;
			string sql = "select * from qp_oa_Zst_guize where Leibie='" + Leibie + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["gongsi"].ToString() + sqlDataReader["Fenshu"].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public string TypeCode(string Table, string CodeId)
		{
			string result = null;
			string sql = string.Concat(new string[]
			{
				"select Name from ",
				Table,
				" where id='",
				CodeId,
				"' "
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["Name"].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public string TypeCodeAll(string KeyData, string Table, string CodeId)
		{
			string result = null;
			string sql = string.Concat(new string[]
			{
				"select ",
				KeyData,
				" from ",
				Table,
				" where id='",
				CodeId,
				"' "
			});
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["" + KeyData + ""].ToString();
			}
			sqlDataReader.Close();
			return result;
		}
		public void Insertfile(string Name, string NewName, string KeyField, string filetype)
		{
			string sql = string.Concat(new string[]
			{
				"insert into qp_oa_fileupload   (Name,NewName,KeyField,filetype) values ('",
				this.GetFormatStr(Name),
				"','",
				NewName,
				"','",
				KeyField,
				"','",
				filetype,
				"')"
			});
			this.List.ExeSql(sql);
		}
		public string Getfiletype(string rightName)
		{
			string sql = "select * from qp_oa_filetype   where name='" + rightName + "'";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			string result;
			if (sqlDataReader.Read())
			{
				result = sqlDataReader["name"].ToString().Replace(".", "");
			}
			else
			{
				result = "unknow";
			}
			sqlDataReader.Close();
			return result;
		}
		public void GetFile(string num, Label _Label)
		{
			string sql = "select  * from qp_oa_fileupload where KeyField='" + num + "' order by id desc";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			_Label.Text = null;
			int num2 = 0;
			_Label.Text += "<table width=100% border=0 cellspacing=0 cellpadding=4>";
			_Label.Text += "<tr>";
			while (sqlDataReader.Read())
			{
				string text = _Label.Text;
				_Label.Text = string.Concat(new string[]
				{
					text,
					" <td width=100% ><img src=\"/oaimg/filetype/",
					sqlDataReader["filetype"].ToString(),
					".gif\" align=\"baseline\"/> <a href=/file_down.aspx?number=",
					sqlDataReader["NewName"].ToString(),
					"  target=_blank>",
					sqlDataReader["Name"].ToString(),
					"</a><a href='javascript:void(0)' onclick=\"fileadd('",
					sqlDataReader["NewName"].ToString(),
					"')\" class=zccss>[转存]</a></td>"
				});
				num2++;
				if (num2 == 1)
				{
					_Label.Text += "</tr><TR>";
					num2 = 0;
				}
			}
			_Label.Text += " </table>";
			sqlDataReader.Close();
		}
		public void GetFileSj(string num, Label _Label)
		{
			string sql = "select  * from qp_oa_fileupload where KeyField='" + num + "' order by id desc";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			_Label.Text = null;
			int num2 = 0;
			_Label.Text += "<table width=100% border=0 cellspacing=0 cellpadding=4>";
			_Label.Text += "<tr>";
			while (sqlDataReader.Read())
			{
				string text = _Label.Text;
				_Label.Text = string.Concat(new string[]
				{
					text,
					" <td width=100% ><img src=\"/oaimg/filetype/",
					sqlDataReader["filetype"].ToString(),
					".gif\" align=\"baseline\"/> <a href=/file_down.aspx?number=",
					sqlDataReader["NewName"].ToString(),
					"  target=_blank>",
					sqlDataReader["Name"].ToString(),
					"</a></td>"
				});
				num2++;
				if (num2 == 1)
				{
					_Label.Text += "</tr><TR>";
					num2 = 0;
				}
			}
			_Label.Text += " </table>";
			sqlDataReader.Close();
		}
		public string GetFileNameNumber(string id)
		{
			string str = null;
			string sql = "select Number from qp_oa_DIYForm   where  id in (" + id + "0)";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			while (sqlDataReader.Read())
			{
				str = str + "'" + sqlDataReader["Number"].ToString() + "',";
			}
			sqlDataReader.Close();
			return str + "'0'";
		}
		public string GetFileNameName(string id)
		{
			string text = null;
			string sql = "select FormName from qp_oa_DIYForm   where  id in (" + id + "0)";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			while (sqlDataReader.Read())
			{
				text = text + "" + sqlDataReader["FormName"].ToString() + ",";
			}
			sqlDataReader.Close();
			return text;
		}
		public string GetFileBBS(string num)
		{
			string text = null;
			string sql = "select  * from qp_oa_fileupload where KeyField='" + num + "' order by id desc";
			SqlDataReader sqlDataReader = this.List.GetList(sql);
			int num2 = 0;
			text += "<table width=100% border=0 cellspacing=0 cellpadding=4>";
			text += "<tr>";
			while (sqlDataReader.Read())
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" <td width=100% ><img src=\"/oaimg/filetype/",
					sqlDataReader["filetype"].ToString(),
					".gif\" align=\"baseline\"/> <a href=/file_down.aspx?number=",
					sqlDataReader["NewName"].ToString(),
					"  target=_blank>",
					sqlDataReader["Name"].ToString(),
					"</a><a href='javascript:void(0)' onclick=\"fileadd('",
					sqlDataReader["NewName"].ToString(),
					"')\" class=zccss>[转存]</a></td>"
				});
				num2++;
				if (num2 == 1)
				{
					text += "</tr><TR>";
					num2 = 0;
				}
			}
			text += " </table>";
			sqlDataReader.Close();
			return text;
		}
		public string GetChecked(CheckBoxList checkList)
		{
			string str = "0,";
			for (int i = 0; i < checkList.Items.Count; i++)
			{
				if (checkList.Items[i].Selected)
				{
					str = str + "" + checkList.Items[i].Value + ",";
				}
			}
			return str + "0";
		}
		public string SetChecked(CheckBoxList checkList, string selval)
		{
			for (int i = 0; i < checkList.Items.Count; i++)
			{
				checkList.Items[i].Selected = false;
				string value = "," + checkList.Items[i].Value + ",";
				if (selval.IndexOf(value) != -1)
				{
					checkList.Items[i].Selected = true;
				}
			}
			return selval;
		}
		public string ShowDateTime(double strSecond)
		{
			string result = string.Empty;
			if (strSecond >= 0.0)
			{
				long num = Convert.ToInt64(strSecond);
				result = string.Concat(new object[]
				{
					num / 86400L,
					"天",
					num % 86400L / 3600L,
					"小时",
					num % 86400L % 3600L / 60L,
					"分钟",
					num % 86400L % 3600L % 60L % 60L,
					"秒"
				});
			}
			return result;
		}
		public bool setCookie(string strName, string strValue, int strDay)
		{
			bool result;
			try
			{
				HttpCookie httpCookie = new HttpCookie(strName);
				httpCookie.Expires = DateTime.Now.AddDays((double)strDay);
				httpCookie.Value = HttpContext.Current.Server.UrlEncode(strValue);
				HttpContext.Current.Response.Cookies.Add(httpCookie);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public string getCookie(string strName)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			string result;
			if (httpCookie != null)
			{
				result = httpCookie.Value.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}
		public bool delCookie(string strName)
		{
			bool result;
			try
			{
				HttpCookie httpCookie = new HttpCookie(strName);
				httpCookie.Expires = DateTime.Now.AddDays(-1.0);
				HttpContext.Current.Response.Cookies.Add(httpCookie);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public string DESEncrypt(string encryptStr, string key, string IV)
		{
			key += "12345678";
			IV += "12345678";
			key = key.Substring(0, 8);
			IV = IV.Substring(0, 8);
			ICryptoTransform transform = new DESCryptoServiceProvider
			{
				Key = Encoding.UTF8.GetBytes(key),
				IV = Encoding.UTF8.GetBytes(IV)
			}.CreateEncryptor();
			byte[] bytes = Encoding.UTF8.GetBytes(encryptStr);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			string text = Convert.ToBase64String(memoryStream.ToArray());
			Random random = new Random();
			for (int i = 0; i < 3; i++)
			{
				int num = random.Next(36);
				char c = Convert.ToChar(num + 65);
				text = text.Substring(0, 2 * i + 1) + c.ToString() + text.Substring(2 * i + 1);
			}
			return text;
		}
		public string DESDecrypt(string encryptedValue, string key, string IV)
		{
			string text = encryptedValue;
			string result;
			if (text.Length < 24)
			{
				result = "";
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					text = text.Substring(0, i + 1) + text.Substring(i + 2);
				}
				encryptedValue = text;
				key += "12345678";
				IV += "12345678";
				key = key.Substring(0, 8);
				IV = IV.Substring(0, 8);
				try
				{
					ICryptoTransform transform = new DESCryptoServiceProvider
					{
						Key = Encoding.UTF8.GetBytes(key),
						IV = Encoding.UTF8.GetBytes(IV)
					}.CreateDecryptor();
					byte[] array = Convert.FromBase64String(encryptedValue);
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.FlushFinalBlock();
					cryptoStream.Close();
					result = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
				catch (Exception var_7_101)
				{
					result = null;
				}
			}
			return result;
		}
		public string GetWK()
		{
			string text = null;
			ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection instances = managementClass.GetInstances();
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					if ((bool)managementObject["IPEnabled"])
					{
						text = managementObject["MacAddress"].ToString();
					}
				}
			}
			return text.Replace(" ", "").Replace("-", "").Replace(":", "");
		}
		public static string GetCpuId()
		{
			string text = null;
			ManagementClass managementClass = new ManagementClass("Win32_Processor");
			try
			{
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject.Properties["ProcessorId"].Value.ToString();
					}
				}
			}
			catch (Exception ex)
			{
				text = ex.ToString();
			}
			if (HttpContext.Current.Application["CPUID"] == null)
			{
				HttpContext.Current.Application.Lock();
				HttpContext.Current.Application["CPUID"] = text;
				HttpContext.Current.Application.UnLock();
			}
			return text;
		}
		public string GetMAC()
		{
			string wK = this.GetWK();
			return this.md5(wK, 32);
		}
		public string md5(string str, int code)
		{
			string result;
			if (code == 16)
			{
				result = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
			}
			else
			{
				if (code == 32)
				{
					result = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
				}
				else
				{
					result = "00000000000000000000000000000000";
				}
			}
			return result;
		}
	}
}
