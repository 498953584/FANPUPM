using ASP;
using cn.justwin.BLL;
using cn.justwin.SMS;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SMS_Fram_SMSOneAdd : NBasePage, IRequiresSessionState
{
	public static DataTable reTable = new DataTable();
	private int PageSize;
	private int PageCount;
	private int RecordCount;
	private int CurrentPage;
	private SqlConnection myCon = new SMS().MSConnection();
	public string Cont = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		this.PageSize = 10;
		if (!base.IsPostBack)
		{
			this.txtPersons.AutoCompleteType = AutoCompleteType.Disabled;
			this.ViewState["IsSearchClick"] = false;
			this.RecordCount = this.GetRecordNumber();
			this.ViewState["recordcount"] = this.RecordCount;
			this.PageCount = (this.RecordCount + this.PageSize - 1) / this.PageSize;
			this.ViewState["pagecount"] = this.PageCount;
			if ((int)this.ViewState["pagecount"] == 0)
			{
				this.CurrentPage = 0;
			}
			else
			{
				this.CurrentPage = (int)this.ViewState["pagecount"] - 1;
			}
			this.ViewState["pageindex"] = this.CurrentPage;
			this.lblPageCounts.Text = this.PageCount.ToString();
			this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
			this.txtWriteText.Attributes["onkeyup"] = "return showTextAreaStyle()";
			this.bindDataList();
		}
	}
	protected void bindDataList()
	{
		string text = "select * from sendedoutbox where 1=1 ";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		if ((bool)this.ViewState["IsSearchClick"] && !string.IsNullOrEmpty(this.txtDateDown.Text))
		{
			text = text + " and convert(varchar(100),sendtime,23)='" + DateTime.Parse(this.txtDateDown.Text).ToString("yyyy'-'MM'-'dd") + "'";
		}
		text += " order by sendtime asc";
		this.DataList1.DataSource = this.CreateSource(text);
		this.DataList1.DataBind();
	}
	protected int GetRecordNumber()
	{
		string text = "select count(*) from sendedoutbox where 1=1";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		SqlCommand sqlCommand = new SqlCommand(text, this.myCon);
		int result = (int)sqlCommand.ExecuteScalar();
		this.myCon.Close();
		return result;
	}
	protected DataView CreateSource(string condition)
	{
		int startRecord = this.PageSize * this.CurrentPage;
		DataSet dataSet = new DataSet();
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(condition, this.myCon);
		sqlDataAdapter.Fill(dataSet, startRecord, this.PageSize, "sendedoutbox");
		if (this.myCon.State == ConnectionState.Open)
		{
			this.myCon.Close();
		}
		return dataSet.Tables["sendedoutbox"].DefaultView;
	}
	protected void BtnMe_Click(object sender, EventArgs e)
	{
		DataTable findTab = new DataTable();
		string text = "";
		if (this.hddPeoples.Value == "")
		{
			if (this.txtPersons.Text != "")
			{
				string text2 = this.txtPersons.Text;
				string[] allNames = text2.Split(new char[]
				{
					','
				});
				int num = this.CheckSend(findTab, allNames, "handWrite");
				if (num == 4 || num == 5)
				{
					return;
				}
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.hdIds.Value))
			{
				string[] array = this.hdIds.Value.Split(new char[]
				{
					','
				});
				string[] array2 = new string[array.Length - 1];
				for (int i = 0; i < array.Length - 1; i++)
				{
					string sqlString = "select mobilephonecode,V_XM from pt_yhmc where v_yhdm='" + array[i] + "'";
					DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
					string value = dataTable.Rows[0]["v_xm"].ToString();
					if (this.txtPersons.Text.Contains(value))
					{
						string text3 = dataTable.Rows[0]["mobilephonecode"].ToString();
						if (!string.IsNullOrEmpty(text3))
						{
							array2[i] = text3;
						}
						else
						{
							string str = publicDbOpClass.ExecuteScalar("select v_xm from pt_yhmc where v_yhdm='" + array[i] + "'").ToString();
							text = text + str + ",";
						}
					}
				}
				if (this.txtPersons.Text.Replace(this.hddPeoples.Value, "").Trim() == "")
				{
					int num2 = this.CheckSend(findTab, array2, "SendBySel");
					if (num2 == 1 && !string.IsNullOrEmpty(text))
					{
						base.RegisterScript("alert('对不起，姓名为" + text + "的人员找不到对应的手机号！请另行通知。')");
					}
					if (num2 == 4 || num2 == 5)
					{
						return;
					}
				}
				else
				{
					string text4 = this.txtPersons.Text.Replace(this.hddPeoples.Value, "").Trim();
					string[] array3 = text4.Split(new char[]
					{
						','
					});
					string[] array4 = new string[array2.Length + array3.Length];
					array2.CopyTo(array4, 0);
					array3.CopyTo(array4, array2.Length);
					int num3 = this.CheckSend(findTab, array4, "AllAction");
					if (num3 == 1 && !string.IsNullOrEmpty(text))
					{
						base.RegisterScript("alert('对不起，姓名为" + text + "的人员找不到对应的手机号！请另行通知。')");
					}
					if (num3 == 4 || num3 == 5)
					{
						return;
					}
				}
				if (this.hdnumbers.Value != "")
				{
					string[] array5 = this.hdnumbers.Value.Split(new char[]
					{
						','
					});
					for (int j = 0; j < array5.Length - 1; j++)
					{
					}
				}
			}
		}
		if (this.hddStyle.Value == "1")
		{
			this.headerTab.Style.Clear();
			this.headerTab.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(images/duanxin_r1_c00y.jpg)");
			this.headerTab.Style.Add(HtmlTextWriterStyle.Width, "380px");
			this.needTd.Style.Clear();
			this.needTd.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(images/testMyPic.gif)");
			this.needTd.Style.Add(HtmlTextWriterStyle.Width, "8px");
			this.needTd.Style.Add(HtmlTextWriterStyle.Height, "32px");
			this.sec.Style.Add(HtmlTextWriterStyle.Display, "block");
		}
		string text5 = this.txtPersons.Text.Substring(0, this.txtPersons.Text.Length - 1);
		Literal expr_3DD = this.LtlCont;
		object text6 = expr_3DD.Text;
		expr_3DD.Text = string.Concat(new object[]
		{
			text6,
			"<span style='color:blue;'>",
			text5,
			"&nbsp;&nbsp;&nbsp;&nbsp;",
			DateTime.Now,
			"</span><br/>&nbsp;&nbsp;&nbsp;&nbsp;",
			this.txtWriteText.Text,
			"<br/>"
		});
		this.txtWriteText.Text = "";
		this.hddPeoples.Value = "";
		this.hdIds.Value = "";
	}
	protected void FirPage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = 0;
		this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
		this.ViewState["pageindex"] = 0;
		this.bindDataList();
		this.CheckOpen();
	}
	protected void NextPage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = (int)this.ViewState["pageindex"];
		if (this.CurrentPage < (int)this.ViewState["pagecount"] - 1)
		{
			this.CurrentPage++;
		}
		else
		{
			if (this.CurrentPage == (int)this.ViewState["pagecount"] - 1)
			{
				base.RegisterScript("alert('已经是最后一页了!')");
				return;
			}
		}
		this.ViewState["pageindex"] = this.CurrentPage;
		this.CurPageIndex.Text = ((int)this.ViewState["pageindex"] + 1).ToString();
		this.bindDataList();
		this.CheckOpen();
	}
	protected void PrePage_Click(object sender, EventArgs e)
	{
		this.CurrentPage = (int)this.ViewState["pageindex"];
		if (this.CurrentPage > 0)
		{
			this.CurrentPage--;
		}
		else
		{
			if (this.CurrentPage == 0)
			{
				base.RegisterScript("alert('已经是第一页了!')");
				return;
			}
		}
		this.ViewState["pageindex"] = this.CurrentPage;
		this.CurPageIndex.Text = ((int)this.ViewState["pageindex"] + 1).ToString();
		this.bindDataList();
		this.CheckOpen();
	}
	protected void LastPage_Click(object sender, EventArgs e)
	{
		if ((int)this.ViewState["pageindex"] > 1)
		{
			this.CurrentPage = (int)this.ViewState["pagecount"] - 1;
			this.CurPageIndex.Text = this.ViewState["pagecount"].ToString();
			this.ViewState["pageindex"] = (int)this.ViewState["pagecount"] - 1;
			this.bindDataList();
			this.CheckOpen();
			return;
		}
		this.CurrentPage = 1;
	}
	protected void CanSearch_Click(object sender, EventArgs e)
	{
		string text = "select * from sendedoutbox where 1=1";
		if (base.UserCode != "00000000")
		{
			text = text + " AND substring(username,1,8)='" + base.UserCode + "'";
		}
		if (!string.IsNullOrEmpty(this.txtDateDown.Text))
		{
			text = text + " and convert(varchar(100),sendtime,23)='" + DateTime.Parse(this.txtDateDown.Text).ToString("yyyy'-'MM'-'dd") + "'";
		}
		text += " order by sendtime desc";
		DataView dataSource = this.CreateSource(text);
		DataTable tableByQuery = this.GetTableByQuery(text);
		if (tableByQuery.Rows.Count > this.PageSize)
		{
			this.CurrentPage = 0;
			this.ViewState["pageindex"] = 0;
			this.RecordCount = tableByQuery.Rows.Count;
			this.PageCount = (this.RecordCount + this.PageSize - 1) / this.PageSize;
			if (this.RecordCount < (int)this.ViewState["recordcount"])
			{
				this.ViewState["pagecount"] = this.PageCount;
				this.ViewState["IsSearchClick"] = true;
				bool arg_14E_0 = (bool)this.ViewState["IsSearchClick"];
			}
			else
			{
				this.Session["IsSearchClick"] = false;
				bool arg_17C_0 = (bool)this.ViewState["IsSearchClick"];
			}
			this.lblPageCounts.Text = this.PageCount.ToString();
			this.CurPageIndex.Text = (this.CurrentPage + 1).ToString();
		}
		this.DataList1.DataSource = dataSource;
		this.DataList1.DataBind();
		this.CheckOpen();
	}
	protected DataTable BandTwice()
	{
		if (!SMS_Fram_SMSOneAdd.reTable.Columns.Contains("username"))
		{
			SMS_Fram_SMSOneAdd.reTable.Columns.Add("username", typeof(string));
		}
		if (!SMS_Fram_SMSOneAdd.reTable.Columns.Contains("mbno"))
		{
			SMS_Fram_SMSOneAdd.reTable.Columns.Add("mbno", typeof(string));
		}
		if (!SMS_Fram_SMSOneAdd.reTable.Columns.Contains("msg"))
		{
			SMS_Fram_SMSOneAdd.reTable.Columns.Add("msg", typeof(string));
		}
		if (!SMS_Fram_SMSOneAdd.reTable.Columns.Contains("sendtime"))
		{
			SMS_Fram_SMSOneAdd.reTable.Columns.Add("sendtime", typeof(DateTime));
		}
		base.RegisterScript("alert('" + this.hddMaxId.Value + "')");
		base.RegisterScript("alert('" + this.hddMinId.Value + "')");
		if (!string.IsNullOrEmpty(this.hddMaxId.Value) && !string.IsNullOrEmpty(this.hddMinId.Value))
		{
			string text = string.Concat(new object[]
			{
				"select username,mbno,msg,sendtime from sendedoutbox where convert(varchar(100),sendtime,23)=convert(varchar(100),getdate(),23) and convert(int,substring(username,10,len(username))) between ",
				int.Parse(this.hddMinId.Value),
				" and ",
				int.Parse(this.hddMaxId.Value),
				" order by sendtime desc"
			});
			DataTable tableByQuery = this.GetTableByQuery(text);
			base.RegisterScript("alert('" + text + "')");
			base.RegisterScript("alert('" + tableByQuery.Rows.Count + "')");
			DataRowCollection rows = tableByQuery.Rows;
			base.RegisterScript("alert('" + rows.Count + "')");
			for (int i = 0; i < rows.Count; i++)
			{
				if (SMS_Fram_SMSOneAdd.reTable.Rows.Count > 0)
				{
					string str = rows[i]["username"].ToString();
					DataRow[] array = SMS_Fram_SMSOneAdd.reTable.Select("username='" + str + "'");
					if (array.Length < 1)
					{
						SMS_Fram_SMSOneAdd.reTable.Rows.Add(rows[i].ItemArray);
					}
				}
				else
				{
					SMS_Fram_SMSOneAdd.reTable.Rows.Add(rows[i].ItemArray);
				}
			}
		}
		return SMS_Fram_SMSOneAdd.reTable;
	}
	protected void CheckOpen()
	{
		if (this.hddStyle.Value == "1")
		{
			this.headerTab.Style.Clear();
			this.headerTab.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(images/duanxin_r1_c00y.jpg)");
			this.headerTab.Style.Add(HtmlTextWriterStyle.Width, "380px");
			this.needTd.Style.Clear();
			this.needTd.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(images/testMyPic.gif)");
			this.needTd.Style.Add(HtmlTextWriterStyle.Width, "8px");
			this.needTd.Style.Add(HtmlTextWriterStyle.Height, "32px");
			this.sec.Style.Add(HtmlTextWriterStyle.Display, "block");
		}
	}
	protected int CheckSend(DataTable findTab, string[] allNames, string SendType)
	{
		int num = 0;
		int result = 0;
		if (!(this.txtWriteText.Text != "") || this.txtWriteText.Text.Length > 100)
		{
			string innerHtml = "<font color='red' >消息内容不能为空且字数不能多于100字</font><img alt='关闭' src='images/hh.gif' align='right' onclick='downClose()' style='padding-top:5px;padding-right:8px;' id='imgClose'/>";
			this.idRemark.InnerHtml = innerHtml;
			return 4;
		}
		List<string> list = new List<string>();
		for (int i = 0; i < allNames.Length; i++)
		{
			if (!string.IsNullOrEmpty(allNames[i]))
			{
				list.Add(allNames[i]);
			}
		}
		if (list.Count > 0)
		{
			TextBox expr_6E = this.txtWriteText;
			expr_6E.Text = expr_6E.Text + "[" + WebUtil.GetUserNames(base.UserCode) + "]";
			for (int j = 0; j < list.Count; j++)
			{
				SMS sMS = new SMS();
				ArrayList arrayList = sMS.Send(SendType, list[j], this.txtWriteText.Text, "", "", "");
				bool flag = (bool)arrayList[0];
				string str = (string)arrayList[1];
				string text = SendType + str;
				if (j == 0)
				{
					this.hddMinId.Value = text.Substring(9);
				}
				if (j == list.Count - 1)
				{
					this.hddMaxId.Value = text.Substring(9);
				}
				string strSql = "select username,mbno,msg,sendtime,bad_why from badoutbox  where username='" + text + "' ";
				DataTable tableByQuery = this.GetTableByQuery(strSql);
				if (tableByQuery.Rows.Count > 0)
				{
					findTab = tableByQuery.Clone();
					findTab.Clear();
					findTab.Rows.Add(tableByQuery.Rows[0].ItemArray);
				}
				else
				{
					tableByQuery.Dispose();
				}
				if (flag)
				{
					num++;
				}
			}
			if (num == list.Count && findTab.Rows.Count == 0)
			{
				result = 1;
			}
			if ((num < list.Count && num > 0) || (findTab.Rows.Count < list.Count && findTab.Rows.Count != 0))
			{
				string text2 = "部分信息不能发送,详细内容如下:\n";
				for (int k = 0; k < findTab.Rows.Count; k++)
				{
					DataRow dataRow = findTab.Rows[k];
					object obj = text2;
					text2 = string.Concat(new object[]
					{
						obj,
						"号码为",
						dataRow["mbno"],
						"的手机号在",
						dataRow["sendtime"],
						"发送失败.失败原因:",
						dataRow["bad_why"],
						";\n"
					});
				}
				base.RegisterScript("alert('" + text2 + "')");
				result = 2;
				this.txtWriteText.Text = "";
			}
			if (findTab.Rows.Count == list.Count)
			{
				base.RegisterScript("alert('完全发送失败,请检查填写或者网络设备,稍后再发或另行通知!')");
				result = 3;
			}
			return result;
		}
		base.RegisterScript("alert('未从接收人栏中找到任何有效的手机号码!请重新设定!')");
		this.txtWriteText.Text = "";
		return 5;
	}
	protected DataTable GetTableByQuery(string strSql)
	{
		DataTable result;
		try
		{
			DataSet dataSet = new DataSet();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSql, this.myCon);
			sqlDataAdapter.Fill(dataSet);
			result = dataSet.Tables[0];
		}
		catch (Exception arg)
		{
			base.RegisterScript("alert('" + arg + "')");
			this.myCon.Close();
			result = null;
		}
		finally
		{
			this.myCon.Close();
		}
		return result;
	}
	protected string GetNameByPhone(string phoneNumber)
	{
		string sqlString = "select v_xm from pt_yhmc where mobilephonecode='" + phoneNumber + "'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			return dataTable.Rows[0][0].ToString();
		}
		return phoneNumber;
	}
	protected void BtnAgin_Click(object sender, EventArgs e)
	{
		DataTable findTab = new DataTable();
		string[] array = this.txtPersons.Text.Split(new char[]
		{
			','
		});
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IndexOf(':') != -1)
			{
				arrayList.Add(array[i].Split(new char[]
				{
					':'
				})[1]);
			}
			else
			{
				arrayList.Add(array[i]);
			}
		}
		string[] allNames = (string[])arrayList.ToArray(typeof(string));
		int num = this.CheckSend(findTab, allNames, base.UserCode);
		if (num == 4 || num == 5)
		{
			return;
		}
		this.CheckOpen();
		string text = string.Empty;
		if (this.txtPersons.Text.Substring(this.txtPersons.Text.Length - 1, 1) != ",")
		{
			text = this.txtPersons.Text.Substring(0, this.txtPersons.Text.Length);
		}
		else
		{
			text = this.txtPersons.Text.Substring(0, this.txtPersons.Text.Length - 1);
		}
		Literal expr_134 = this.LtlCont;
		object text2 = expr_134.Text;
		expr_134.Text = string.Concat(new object[]
		{
			text2,
			"<span style='color:blue;'>",
			text,
			"&nbsp;&nbsp;&nbsp;&nbsp;",
			DateTime.Now,
			"</span><br/>&nbsp;&nbsp;&nbsp;&nbsp;",
			this.txtWriteText.Text,
			"<br/>"
		});
		this.txtWriteText.Text = "";
	}
}


