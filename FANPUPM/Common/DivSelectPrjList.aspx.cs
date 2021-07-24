using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockDAL;
using cn.justwin.Tender;
using cn.justwin.Web;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Common_DivSelectPrjList : NBasePage, IRequiresSessionState
{
	private string tcode = string.Empty;
	private PersistScrollPosition PersistScrollPosition1;
	private PrjInfo prjInfo = new PrjInfo();
	private string applyPage = string.Empty;

	protected override void OnInitComplete(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["tcode"]))
		{
			this.tcode = base.Request["tcode"];
		}
		if (!string.IsNullOrEmpty(base.Request["ApplyPage"]))
		{
			this.applyPage = base.Request["ApplyPage"].ToString();
		}
		base.OnInitComplete(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!this.Page.IsPostBack)
		{
			this.hfldApplyPage.Value = this.applyPage;
			this.hdfRQis.Value = base.Request["is"].ToString();
			this.ShowTaskList("", "");
		}
	}
	public void ShowTaskList(string prjcode, string prjname)
	{
		this.Session["yhdm"].ToString();
		this.AspNetPager1.RecordCount = this.prjInfo.GetProjectCountByCodes(base.UserCode, prjcode, prjname, this.GetPrjCode(), System.Convert.ToInt32(this.hdfRQis.Value));
		DataTable projectByCodes = this.prjInfo.GetProjectByCodes(base.UserCode, prjcode, prjname, this.GetPrjCode(), System.Convert.ToInt32(this.hdfRQis.Value), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		projectByCodes.Columns.Add(new DataColumn("isMainContract", typeof(string)));
		for (int i = 0; i < projectByCodes.Rows.Count; i++)
		{
			DataRow dataRow = projectByCodes.Rows[i];
			if (dataRow["TypeCode"].ToString().Length != 5)
			{
				projectByCodes.Rows[i]["isMainContract"] = "False";
			}
			else
			{
				projectByCodes.Rows[i]["isMainContract"] = "True";
			}
		}
		projectByCodes.Columns.Add(new DataColumn("HeadImg", typeof(string)));
		projectByCodes.Columns.Add(new DataColumn("Display", typeof(string)));
		projectByCodes.Columns.Add(new DataColumn("BudgetCode", typeof(string)));
		this.grdModules.DataSource = projectByCodes;
		this.grdModules.DataBind();
	}
	private string GetPrjCode()
	{
		string cmdText = "SELECT prjCode + ',' FROM dbo.Sm_Treasury WHERE tcode != @tcode FOR XML PATH('')";
		SqlParameter[] commandParameters = new SqlParameter[]
		{
			new SqlParameter("@tcode", this.tcode)
		};
		string @string = DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
		System.Collections.Generic.List<string> list = (
			from s in @string.Split(new char[]
			{
				','
			})
			where s.Length > 0
			select s).ToList<string>();
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		foreach (string current in list)
		{
			stringBuilder.Append(",'").Append(current).Append("'");
		}
		if (stringBuilder.Length > 0)
		{
			return stringBuilder.Remove(0, 1).ToString();
		}
		return string.Empty;
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
			if (dataRowView["SetUpFlowState"].ToString() == "1" && dataRowView["PrjState"].ToString() != "17" && dataRowView["ISDISPLAY"].ToString() != "0" && dataRowView["PrjState"].ToString() != "1" && dataRowView["PrjState"].ToString() != "2" && dataRowView["PrjState"].ToString() != "3" && dataRowView["PrjState"].ToString() != "4" && dataRowView["PrjState"].ToString() != "6" && dataRowView["PrjState"].ToString() != "14" && dataRowView["PrjState"].ToString() != "15" && dataRowView["PrjState"].ToString() != "16" && dataRowView["PrjState"].ToString() != "18")
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
			e.Item.Attributes["prjName"] = ((HiddenField)e.Item.FindControl("hdfPrjName")).Value;
			e.Item.Attributes["id"] = dataRowView["budgetcode"].ToString();
			e.Item.Attributes["prjState"] = dataRowView["PrjState"].ToString();
			e.Item.Attributes["setUpFlowState"] = dataRowView["SetUpFlowState"].ToString();
			e.Item.Attributes["ISDISPLAY"] = dataRowView["ISDISPLAY"].ToString();
			if (dataRowView["SetUpFlowState"].ToString() == "1" && dataRowView["PrjState"].ToString() != "17" && dataRowView["ISDISPLAY"].ToString() != "0" && dataRowView["PrjState"].ToString() != "1" && dataRowView["PrjState"].ToString() != "2" && dataRowView["PrjState"].ToString() != "3" && dataRowView["PrjState"].ToString() != "4" && dataRowView["PrjState"].ToString() != "6" && dataRowView["PrjState"].ToString() != "14" && dataRowView["PrjState"].ToString() != "15" && dataRowView["PrjState"].ToString() != "16" && dataRowView["PrjState"].ToString() != "18")
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
			e.Item.Cells[9].Text = "";
			if (ConfigHelper.Get("IsNewProject") == "0")
			{
				if (dataRowView["PrjState"].ToString() == "4")
				{
					e.Item.Cells[9].Text = "在建";
				}
				if (dataRowView["PrjState"].ToString() == "-1")
				{
					e.Item.Cells[9].ForeColor = Color.Blue;
					e.Item.Cells[9].Text = "在建";
				}
				if (dataRowView["PrjState"].ToString() == "0")
				{
					e.Item.Cells[9].Text = "";
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
					e.Item.Cells[9].Text = TypeList.GetNameByCode(code);
				}
				catch
				{
				}
			}
			if (dataRowView["TypeCode"].ToString().Length > 5)
			{
				e.Item.Cells[2].Text = dataRowView["TypeCode"].ToString().Substring(0, 5) + "<font color=\"#ff0000\">" + dataRowView["TypeCode"].ToString().Substring(5, dataRowView["TypeCode"].ToString().Length - 5) + "</font>";
			}
			e.Item.Cells[3].ToolTip = e.Item.Cells[3].Text;
			e.Item.Cells[3].Text = StringUtility.GetStr(dataRowView["prjName"].ToString(), 0, 12);
			e.Item.Cells[4].ToolTip = e.Item.Cells[4].Text;
			e.Item.Cells[4].Text = StringUtility.GetStr(dataRowView["Owner"].ToString(), 0, 6);
			e.Item.Cells[8].ToolTip = e.Item.Cells[8].Text;
			e.Item.Cells[8].Text = StringUtility.GetStr(dataRowView["PrjPlace"].ToString(), 0, 6);
			e.Item.Attributes["id"] = dataRowView["prjGuid"].ToString();
			string text = base.Request["prjCode"].ToString();
			if (text != "")
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (text2 == dataRowView["prjGuid"].ToString() && text2 != "")
					{
						CheckBox checkBox = e.Item.FindControl("CheckBox1") as CheckBox;
						checkBox.Checked = true;
						HiddenField expr_AE6 = this.hdCode;
						expr_AE6.Value = expr_AE6.Value + "," + text2;
						HiddenField expr_B03 = this.hdName;
						expr_B03.Value = expr_B03.Value + dataRowView["prjName"] + ",";
					}
				}
			}
		}
	}
	protected void SearchBt_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
		this.ShowTaskList(this.prjcode.Text.ToString(), this.prjname.Text.ToString());
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.ShowTaskList(this.prjcode.Text.Trim(), this.prjname.Text.Trim());
	}
}


