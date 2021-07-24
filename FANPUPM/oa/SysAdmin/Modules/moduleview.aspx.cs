using ASP;
using cn.justwin.Web;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ModuleView : BasePage, IRequiresSessionState
	{
		protected string[] moduleCodeList;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.moduleCodeList = this.hdnModuleCodeList.Value.Split(new char[]
			{
				'^'
			});
			if (!this.Page.IsPostBack)
			{
				this.btnDel.Attributes["onclick"] = "if (!ClickBtn('del')) return false;";
				this.ShowTaskList();
				this.grdModules.Style["table-layout"] = "fixed";
			}
		}
		public void ShowTaskList()
		{
			DataTable dataTable = CacheHelper.Get("MODULEVIEW") as DataTable;
			if (dataTable == null)
			{
				dataTable = SystemModule.GetAllSubModule("");
				CacheHelper.Set("MODULEVIEW", dataTable);
			}
			DataTable dataTable2 = new DataTable();
			foreach (DataColumn dataColumn in dataTable.Columns)
			{
				dataTable2.Columns.Add(new DataColumn(dataColumn.ColumnName, dataColumn.DataType));
			}
			dataTable2.Columns.Add(new DataColumn("HeadImg", typeof(string)));
			dataTable2.Columns.Add(new DataColumn("Display", typeof(string)));
			dataTable2.Columns.Add(new DataColumn("BudgetCode", typeof(string)));
			DataRow dataRow = dataTable2.NewRow();
			dataRow["c_mkdm"] = "00";
			dataRow["v_mkmc"] = "系统模块列表";
			dataRow["v_cdlj"] = "";
			dataRow["i_childnum"] = 0;
			dataRow["HeadImg"] = "";
			dataRow["Display"] = "";
			dataRow["BudgetCode"] = "00";
			dataRow["IsBasic"] = "0";
			dataRow["IsMaintainable"] = "0";
			dataTable2.Rows.Add(dataRow);
			this.CreateChild(dataTable, dataTable2, "", 0, "", false);
			this.grdModules.DataSource = dataTable2;
			this.grdModules.DataBind();
		}
		public void CreateChild(DataTable dt, DataTable dt1, string moduleCode, int level, string inherHead, bool isExpend)
		{
			DataRow[] array = dt.Select(string.Concat(new string[]
			{
				"(c_mkdm like '",
				moduleCode.ToString(),
				"%')and(len(c_mkdm)= ",
				(moduleCode.Length + 2).ToString(),
				")"
			}), "i_xh,c_mkdm");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				DataRow dataRow = array[i];
				for (int j = 0; j < this.moduleCodeList.Length; j++)
				{
					if (this.moduleCodeList[j] == dataRow["c_mkdm"].ToString())
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
					isExpend = true;
				}
				else
				{
					if (!isExpend)
					{
						dataRow2["display"] = "none";
					}
				}
				dt1.Rows.Add(dataRow2);
				this.CreateChild(dt, dt1, dataRow["c_mkdm"].ToString(), level + 1, this.getInherImg(i, num, inherHead), isExpend && flag);
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
						if (this.moduleCodeList[i] == dr["c_mkdm"].ToString())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						result = "<img src=\"../../../images/tree/tminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
					}
					else
					{
						result = "<img src=\"../../../images/tree/tplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','t');\">";
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
							if (this.moduleCodeList[j] == dr["c_mkdm"].ToString())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = "<img src=\"../../../images/tree/lminus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
						}
						else
						{
							result = "<img src=\"../../../images/tree/lplus.gif\" align=\"absmiddle\" onclick=\"switchDisplay(this,'" + dr["c_mkdm"].ToString() + "','l');\">";
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
				if (dataRowView["c_mkdm"].ToString() == "00")
				{
					e.Item.Attributes["onclick"] = "clickRow(this,'',false);";
				}
				else
				{
					e.Item.Attributes["onclick"] = string.Concat(new string[]
					{
						"clickRow(this,'",
						dataRowView["c_mkdm"].ToString(),
						"',",
						((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
						");"
					});
				}
				e.Item.Attributes["onmouseover"] = "overRow(this);";
				e.Item.Attributes["onmouseout"] = "outRow(this);";
				e.Item.Style.Add("display", dataRowView["display"].ToString());
				e.Item.Attributes["id"] = dataRowView["budgetcode"].ToString();
				e.Item.Attributes["pid"] = dataRowView["budgetcode"].ToString();
				e.Item.Cells[3].Text = ((dataRowView["IsBasic"].ToString() == "1") ? "是" : "否");
				e.Item.Cells[4].Text = ((dataRowView["IsMaintainable"].ToString() == "1") ? "是" : "否");
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			CacheHelper.Delete("MODULEVIEW");
			string mkdm = this.hdnModuleCode.Value.ToString();
			if (!SystemModule.DelModule(mkdm))
			{
				this.Page.RegisterStartupScript("warn", "<script language=\"JavaScript\">alert('fsd');</script>");
			}
			this.ShowTaskList();
		}
	}


