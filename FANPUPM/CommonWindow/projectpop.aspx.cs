using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
	public partial class PrjSel : BasePage, IRequiresSessionState
	{
		protected string[] moduleCodeList;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.moduleCodeList = this.hdnModuleCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				this.ShowTaskList("", "");
				this.grdModules.Style["table-layout"] = "fixed";
			}
		}
		public void ShowTaskList(string prjcode, string prjname)
		{
			DataTable dataTable = new DataTable();
			DataTable dataTable2 = new DataTable();
			string usercode = this.Session["yhdm"].ToString();
			this.AspNetPager1.RecordCount = new PTPrjInfoBll().GetProjectCount(usercode, prjcode, prjname);
			dataTable = new PTPrjInfoBll().GetProject(usercode, prjcode, prjname, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
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
		public void CreateChild(DataTable dt, DataTable dt1, string moduleCode, int level, string inherHead, bool isExpend)
		{
			string sort = "StartDate DESC";
			if (moduleCode.Length == 5)
			{
				sort = "TypeCode ASC";
			}
			DataRow[] array = dt.Select(string.Concat(new string[]
			{
				"(TypeCode like '",
				moduleCode.ToString(),
				"%')and(len(TypeCode)= ",
				(moduleCode.Length + 5).ToString(),
				")"
			}), sort);
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
		protected override void OnInit(System.EventArgs e)
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
					e.Item.Attributes["onclick"] = "clickRow(this,'',false,'','');";
				}
				else
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"clickRow(this,'",
						dataRowView["TypeCode"].ToString(),
						"',",
						((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
						",'",
						dataRowView["prjguid"].ToString(),
						"','",
						dataRowView["prjname"].ToString(),
						"');"
					});
				}
				e.Item.Attributes["onmouseover"] = "overRow(this);";
				e.Item.Attributes["onmouseout"] = "outRow(this);";
				e.Item.Style.Add("display", dataRowView["display"].ToString());
				e.Item.Attributes["id"] = dataRowView["budgetcode"].ToString();
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"dbClickRow(this,'",
					dataRowView["prjguid"].ToString(),
					"','",
					dataRowView["prjname"].ToString(),
					"',",
					((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
					")"
				});
				if (ConfigHelper.Get("IsNewProject") == "0")
				{
					if (dataRowView["PrjState"].ToString() == "4")
					{
						e.Item.Cells[8].Text = "在建";
					}
					if (dataRowView["PrjState"].ToString() == "-1")
					{
						e.Item.Cells[8].ForeColor = Color.Blue;
						e.Item.Cells[8].Text = "在建";
					}
					if (dataRowView["PrjState"].ToString() == "0")
					{
						e.Item.Cells[8].Text = "";
					}
				}
				else
				{
					try
					{
						int code = System.Convert.ToInt32(dataRowView["PrjState"]);
						e.Item.Cells[8].Text = TypeList.GetNameByCode(code);
					}
					catch
					{
					}
				}
				if (dataRowView["TypeCode"].ToString().Length > 5)
				{
					e.Item.Cells[1].Text = dataRowView["TypeCode"].ToString().Substring(0, 5) + "<font color=\"#ff0000\">" + dataRowView["TypeCode"].ToString().Substring(5, dataRowView["TypeCode"].ToString().Length - 5) + "</font>";
				}
				e.Item.Cells[3].Text = StringUtility.GetStr(dataRowView["Owner"].ToString(), 0, 6);
				e.Item.Cells[7].Text = StringUtility.GetStr(dataRowView["PrjPlace"].ToString(), 0, 6);
				HyperLink hyperLink = (HyperLink)e.Item.Cells[2].Controls[0];
				hyperLink.Text = StringUtility.GetStr(hyperLink.Text);
				hyperLink.Style.Add("onMouseOver", "document.getElementById('btnEdit').Color='red'");
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = "../EPC/Pm/PrjInfo/PrjInfoView.aspx?typecode=" + dataRowView["TypeCode"].ToString();
			}
		}
		protected void SearchBt_Click(object sender, System.EventArgs e)
		{
		}
		protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
		{
			this.ShowTaskList(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
		}
	}


