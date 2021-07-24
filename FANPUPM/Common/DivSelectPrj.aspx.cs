using ASP;
using cn.justwin.BLL;
using cn.justwin.stockDAL;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Drawing;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Common_DivSelectPrj : NBasePage, IRequiresSessionState
{
	protected string[] moduleCodeList;
	private PrjInfo prjInfo = new PrjInfo();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.grdModules.PageSize = NBasePage.pagesize;
		this.moduleCodeList = this.hdnModuleCodeList.Value.Split(new char[]
		{
			'^'
		});
		if (!this.Page.IsPostBack)
		{
			this.hdfRQis.Value = base.Request["is"].ToString();
			this.ShowTaskList("", "");
		}
	}
	public void ShowTaskList(string prjcode, string prjname)
	{
		DataTable dataTable = new DataTable();
		string usercode = this.Session["yhdm"].ToString();
		this.AspNetPager1.RecordCount = this.prjInfo.GetProjectCountByCodes(usercode, prjcode, prjname, Common_DivSelectPrj.GetPrjCode(), System.Convert.ToInt32(this.hdfRQis.Value));
		dataTable = this.prjInfo.GetProjectByCodes(usercode, prjcode, prjname, Common_DivSelectPrj.GetPrjCode(), System.Convert.ToInt32(this.hdfRQis.Value), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		dataTable.Columns.Add(new DataColumn("isMainContract", typeof(string)));
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.Rows[i];
			if (dataRow["TypeCode"].ToString().Length != 5)
			{
				dataTable.Rows[i]["isMainContract"] = "False";
			}
			else
			{
				dataTable.Rows[i]["isMainContract"] = "True";
			}
		}
		dataTable.Columns.Add(new DataColumn("HeadImg", typeof(string)));
		dataTable.Columns.Add(new DataColumn("Display", typeof(string)));
		dataTable.Columns.Add(new DataColumn("BudgetCode", typeof(string)));
		this.grdModules.DataSource = dataTable;
		this.grdModules.DataBind();
	}
	public static string GetPrjCode()
	{
		string sqlString = "select prjCode from dbo.Sm_Treasury";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		string text = "";
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				if (dataRow[0].ToString() != "")
				{
					string[] array = dataRow[0].ToString().Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text2 = array2[i];
						if (text2 != "")
						{
							if (text == "")
							{
								text = text + "'" + text2 + "'";
							}
							else
							{
								text = text + ",'" + text2 + "'";
							}
						}
					}
				}
			}
		}
		return text;
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
			if (dataRowView["SetUpFlowState"].ToString() == "1" && dataRowView["PrjState"].ToString() != "17")
			{
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
			}
			else
			{
				e.Item.Attributes["onclick"] = "clickRow(this,'',false,'','','0');";
			}
			e.Item.Attributes["isMainContract"] = ((HiddenField)e.Item.FindControl("hfldIsMainContract")).Value;
			e.Item.Attributes["id"] = dataRowView["budgetcode"].ToString();
			if (dataRowView["SetUpFlowState"].ToString() == "1" && dataRowView["PrjState"].ToString() != "17")
			{
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
			}
			e.Item.Cells[8].Text = "";
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
				if (dataRowView["TypeCode"].ToString() == "00")
				{
					e.Item.Attributes["onclick"] = "clickRow(this,'',false,'','','');";
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
						"','",
						dataRowView["prjCode"].ToString(),
						"');"
					});
				}
				e.Item.Attributes["ondblclick"] = string.Concat(new string[]
				{
					"dbClickRow(this,'",
					dataRowView["prjguid"].ToString(),
					"','",
					dataRowView["prjname"].ToString(),
					"','",
					dataRowView["prjCode"].ToString(),
					"',",
					((int)dataRowView["i_childnum"] == 0) ? "true" : "false",
					")"
				});
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
			e.Item.Attributes["id"] = dataRowView["prjGuid"].ToString();
		}
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
		this.ShowTaskList(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.ShowTaskList(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
	}
}


