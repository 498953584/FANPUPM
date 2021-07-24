using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectMyProject : NBasePage, IRequiresSessionState
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
		}
	}
	public void ShowTaskList(string prjcode, string prjname)
	{
		DataTable dataSource = new DataTable();
		new DataTable();
		string text = this.Session["yhdm"].ToString();
		string cmdText = "select *from dbo.PT_PrjInfo where PrjGuid in (select convert(uniqueidentifier,prjNum) from dbo.fund_Prjaccount  where prjNum!='00000000-0000-0000-0000-000000000000' and  authorizer like '%" + text + "%')";
		if (!(text.Trim() != "00000000"))
		{
			cmdText = "select *from dbo.PT_PrjInfo where PrjGuid in (select convert(uniqueidentifier,prjNum) from dbo.fund_Prjaccount  where prjNum!='00000000-0000-0000-0000-000000000000')";
		}
		dataSource = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
		this.grdModules.DataSource = dataSource;
		this.grdModules.DataBind();
	}
	protected DataTable ReTable(DataTable dt, string filter)
	{
		DataRow[] array = dt.Select(filter);
		DataTable dataTable = dt.Clone();
		for (int i = 0; i < array.Length; i++)
		{
			dataTable.ImportRow(array[i]);
		}
		return dataTable;
	}
	public void CreateChild(DataTable dt, DataTable dt1, string moduleCode, int level, string inherHead, bool isExpend)
	{
		DataRow[] array = dt.Select(string.Concat(new string[]
		{
			"(TypeCode like '",
			moduleCode.ToString(),
			"%')and(len(TypeCode)= ",
			(moduleCode.Length + 5).ToString(),
			")"
		}), "i_xh,TypeCode");
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
			string text = ((Label)e.Item.FindControl("lblname")).Text;
			string value = ((HiddenField)e.Item.FindControl("hdfacid")).Value;
			e.Item.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dbClickRow(this,'",
				dataRowView["prjguid"].ToString(),
				"','",
				dataRowView["prjname"].ToString(),
				"','",
				text,
				"','",
				value,
				"','",
				dataRowView["PrjCost"].ToString(),
				"',",
				((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
				")"
			});
			e.Item.Cells[5].Text = "";
			if (dataRowView["PrjState"].ToString() == "4")
			{
				e.Item.Cells[5].Text = "在建";
			}
			if (dataRowView["PrjState"].ToString() == "-1")
			{
				e.Item.Cells[5].ForeColor = Color.Blue;
				e.Item.Cells[5].Text = "在建";
			}
			if (dataRowView["PrjState"].ToString() == "0")
			{
				e.Item.Cells[5].Text = "";
			}
			if (dataRowView["TypeCode"].ToString().Length > 5)
			{
				e.Item.Cells[1].Text = dataRowView["TypeCode"].ToString().Substring(0, 5) + "<font color=\"#ff0000\">" + dataRowView["TypeCode"].ToString().Substring(5, dataRowView["TypeCode"].ToString().Length - 5) + "</font>";
			}
			e.Item.Cells[3].Text = StringUtility.GetStr(dataRowView["Owner"].ToString(), 0, 6);
			e.Item.Attributes["id"] = dataRowView["prjGuid"].ToString();
		}
	}
	protected string GetBankName(string contenum)
	{
		new prjaccountModel();
		accountMange accountMange = new accountMange();
		DataTable table = accountMange.GetTable(" prjNum='" + contenum + "'");
		if (table.Rows.Count == 1)
		{
			return table.Rows[0]["accountNum"].ToString();
		}
		return string.Empty;
	}
	protected string GetBankNum(string contenum)
	{
		new prjaccountModel();
		accountMange accountMange = new accountMange();
		DataTable table = accountMange.GetTable(" prjNum='" + contenum + "'");
		if (table.Rows.Count == 1)
		{
			return table.Rows[0]["accountID"].ToString();
		}
		return string.Empty;
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
	}
}


