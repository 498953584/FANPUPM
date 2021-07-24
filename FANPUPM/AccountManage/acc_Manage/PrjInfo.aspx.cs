using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PrjInfo : BasePage, IRequiresSessionState
	{
		private accountMange am = new accountMange();
		protected string[] moduleCodeList;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.moduleCodeList = this.hdnModuleCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				this.ShowTaskList();
				this.grdModules.Style["table-layout"] = "fixed";
			}
		}
		public void ShowTaskList()
		{
			DataTable dataTable = new DataTable();
			DataTable dataTable2 = new DataTable();
			dataTable = PrjInfo.GetPrjInfoInfo();
			foreach (DataColumn dataColumn in dataTable.Columns)
			{
				dataTable2.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable2.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable2.Columns.Add(new DataColumn("Display", typeof(string)));
			dataTable2.Columns.Add(new DataColumn("BudgetCode", typeof(string)));
			DataRow dataRow = dataTable2.NewRow();
			dataRow["TypeCode"] = "";
			dataRow["PrjCode"] = "项目信息列表";
			dataRow["PrjName"] = "";
			dataRow["PrjPlace"] = "";
			dataRow["Owner"] = "";
			dataRow["i_childnum"] = 0;
			dataRow["HeadImg"] = "";
			dataRow["Display"] = "";
			dataRow["BudgetCode"] = "";
			dataRow["startdate"] = "1900-01-01";
			dataRow["EndDate"] = "2015-01-01";
			dataRow["PrjState"] = "0";
			dataTable2.Rows.Add(dataRow);
			this.CreateChild(dataTable, dataTable2, "", 0, "", false);
			this.grdModules.DataSource = dataTable2;
			this.grdModules.DataBind();
		}
		public static DataTable GetPrjInfoInfo()
		{
			string sqlString = "select prjNum from dbo.fund_Prjaccount";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			string text = "";
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (dataTable.Rows[i][0].ToString() != "")
				{
					text = text + ",'" + dataTable.Rows[i][0].ToString() + "'";
				}
			}
			int num = text.Length - 1;
			string sqlString2;
			if (num > 0)
			{
				sqlString2 = "select * from PT_PrjInfo where IsValid=1 and prjGuid not in (" + text.Substring(1, num) + ")";
			}
			else
			{
				sqlString2 = "select * from PT_PrjInfo where IsValid=1 ";
			}
			return publicDbOpClass.DataTableQuary(sqlString2);
		}
		public void CreateChild(DataTable dt, DataTable dt1, string moduleCode, int level, string inherHead, bool isExpend)
		{
			DataRow[] array = dt.Select(string.Concat(new string[]
			{
				"(TypeCode like '",
				moduleCode.ToString(),
				"%')and(len(TypeCode)= ",
				(moduleCode.Length + 3).ToString(),
				")"
			}), "i_xh,TypeCode ");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this.moduleCodeList.Length; j++)
				{
					if (this.moduleCodeList[j] == dataRow["TypeCode"].ToString())
					{
						flag = true;
						break;
					}
				}
				DataRow dataRow2 = dt1.NewRow();
				dataRow2.ItemArray = dataRow.ItemArray;
				dataRow2["HeadImg"] = inherHead + this.getHeadImg(i, num, dataRow);
				dataRow2["BudgetCode"] = moduleCode;
				if (level == 0)
				{
					dataRow2["display"] = "block";
					isExpend = true;
				}
				else
				{
					if (isExpend)
					{
						dataRow2["display"] = "block";
					}
					else
					{
						dataRow2["display"] = "none";
					}
				}
				dt1.Rows.Add(dataRow2);
				this.CreateChild(dt, dt1, dataRow["TypeCode"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag);
			}
		}
		public string getHeadImg(int currentIndex, int length, DataRow dr)
		{
			string result = "";
			bool flag = false;
			if (currentIndex != length - 1)
			{
				if ((int)dr["i_childnum"] > 0)
				{
					for (int i = 0; i < this.moduleCodeList.Length; i++)
					{
						if (this.moduleCodeList[i] == dr["TypeCode"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TypeCode"].ToString() + "','t');\">";
					}
					else
					{
						result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TypeCode"].ToString() + "','t');\">";
					}
				}
				else
				{
					result = "<img src=\"../../../images/tree/t.gif\" align=\"absmiddle\">";
				}
			}
			else
			{
				if (currentIndex == length - 1)
				{
					if ((int)dr["i_childnum"] > 0)
					{
						for (int j = 0; j < this.moduleCodeList.Length; j++)
						{
							if (this.moduleCodeList[j] == dr["TypeCode"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TypeCode"].ToString() + "','l');\">";
						}
						else
						{
							result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["TypeCode"].ToString() + "','l');\">";
						}
					}
					else
					{
						result = "<img src=\"../../../images/tree/l.gif\" align=\"absmiddle\">";
					}
				}
			}
			return result;
		}
		public string getInherImg(int currentIndex, int length, string inherHead)
		{
			string str;
			if (currentIndex == length - 1)
			{
				str = "<img src=\"../../../images/tree/white.gif\" align=\"absmiddle\">";
			}
			else
			{
				str = "<img src=\"../../../images/tree/i.gif\" align=\"absmiddle\">";
			}
			return inherHead + str;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.grdModules.ItemDataBound += new DataGridItemEventHandler(this.grd_ModuleList_ItemDataBound);
		}
		private void grd_ModuleList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.EditItem)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				if (dataRowView["TypeCode"].ToString() == "00")
				{
					e.Item.Attributes["onclick"] = "clickRow(this,'',false);";
				}
				else
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"clickRow(this,'",
						dataRowView["TypeCode"].ToString(),
						"',",
						((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
						");"
					});
				}
				e.Item.Attributes["onmouseover"] = "overRow(this);";
				e.Item.Attributes["onmouseout"] = "outRow(this);";
				e.Item.Style.Add("display", dataRowView["display"].ToString());
				e.Item.Attributes["id"] = dataRowView["budgetcode"].ToString();
				if (dataRowView["PrjState"].ToString() == "-1")
				{
					e.Item.Cells[9].ForeColor = Color.Blue;
					e.Item.Cells[9].Text = "在建";
				}
				if (dataRowView["PrjState"].ToString() == "4")
				{
					e.Item.Cells[9].Text = "在建";
				}
				if (dataRowView["PrjState"].ToString() == "6")
				{
					e.Item.Cells[9].Text = "停工";
				}
				if (dataRowView["PrjState"].ToString() == "7")
				{
					e.Item.Cells[9].Text = "竣工";
				}
				if (dataRowView["PrjState"].ToString() == "8")
				{
					e.Item.Cells[9].Text = "验收";
				}
				if (dataRowView["PrjState"].ToString() == "9")
				{
					e.Item.Cells[9].Text = "维保";
				}
				if (dataRowView["PrjState"].ToString() == "0")
				{
					e.Item.Cells[9].Text = "";
				}
				if (dataRowView["TypeCode"].ToString().Length > 3)
				{
					e.Item.Cells[2].Text = dataRowView["TypeCode"].ToString().Substring(0, 3) + "<font color=\"#ff0000\">" + dataRowView["TypeCode"].ToString().Substring(3, dataRowView["TypeCode"].ToString().Length - 3) + "</font>";
				}
				HyperLink hyperLink = (HyperLink)e.Item.Cells[3].Controls[0];
				hyperLink.ToolTip = hyperLink.Text;
				if (hyperLink.Text.Length > 8)
				{
					hyperLink.Text = hyperLink.Text.Substring(0, 7) + "...";
				}
				hyperLink.Style.Add("onMouseOver", "document.getElementById('btnEdit').Color='red'");
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = "javascript:void(window.open('/EPC/Pm/PrjInfo/PrjInfoView.aspx?typecode=" + dataRowView["TypeCode"].ToString() + "','','left=150,top=150,width=840,height=530,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));";
				string text = e.Item.Cells[8].Text;
				e.Item.Cells[8].ToolTip = text;
				if (text.Length > 7)
				{
					e.Item.Cells[8].Text = text.Substring(0, 6) + "...";
				}
				if (e.Item.Cells[4].Text.ToString().Trim().Length > 7)
				{
					e.Item.Cells[4].Text = e.Item.Cells[4].Text.ToString().Trim().Substring(0, 6) + "...";
				}
			}
			if (e.Item.ItemIndex == 0)
			{
				e.Item.Cells[6].Text = "";
				e.Item.Cells[7].Text = "";
				e.Item.ToolTip = "选择此行，点击新增增加新项目";
			}
		}
		protected void btnOK_Click(object sender, EventArgs e)
		{
			string a = base.Request["limits"].ToString();
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				try
				{
					List<prjaccountModel> list = new List<prjaccountModel>();
					for (int i = 0; i < this.grdModules.Items.Count; i++)
					{
						CheckBox checkBox = (CheckBox)this.grdModules.Items[i].FindControl("chkAcc");
						if (checkBox.Checked)
						{
							string str;
							if (i < 10)
							{
								str = "0" + i.ToString();
							}
							else
							{
								str = i.ToString();
							}
							prjaccountModel prjaccountModel = new prjaccountModel();
							prjaccountModel.accountNum = DateTime.Now.ToString("yyyyMMddHHmmss") + str;
							prjaccountModel.acountName = "";
							HiddenField hiddenField = (HiddenField)this.grdModules.Items[i].FindControl("hdfPrjNum");
							prjaccountModel.prjNum = new Guid(hiddenField.Value.ToString());
							prjaccountModel.creatData = new DateTime?(DateTime.Now);
							prjaccountModel.createMan = base.UserCode;
							prjaccountModel.isActivity = new int?(0);
							prjaccountModel.contractNum = "";
							prjaccountModel.remark = "";
							string authorizer = "";
							if (a == "0")
							{
								authorizer = this.am.GetPrjUserCodeByprj(prjaccountModel.prjNum.ToString());
							}
							prjaccountModel.authorizer = authorizer;
							list.Add(prjaccountModel);
						}
					}
					this.am.Add(sqlTransaction, list);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(" parent.desktop.flowclass.location='/AccountManage/acc_Manage/accountList.aspx';").Append(Environment.NewLine);
					stringBuilder.Append("alert('系统提示：\\n\\n添加成功！');").Append(Environment.NewLine);
					stringBuilder.Append("top.frameWorkArea.window.desktop.getActive().close();");
					this.js.Text = stringBuilder.ToString();
					sqlTransaction.Commit();
				}
				catch
				{
					sqlTransaction.Rollback();
					this.js.Text = "alert('系统提示：\\n\\n添加失败！');";
				}
			}
		}
	}


