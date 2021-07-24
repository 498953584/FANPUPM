using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using cn.justwin.DAL;
using cn.justwin.PrjManager;
using cn.justwin.stockBLL.TableTopBLL;
using cn.justwin.Tender;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class test : NBasePage, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();
	protected string HideNullGrid
	{
		get
		{
			object obj = this.ViewState["HideNullGrid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "n";
		}
		set
		{
			this.ViewState["HideNullGrid"] = value;
		}
	}
	protected int _ColCount
	{
		get
		{
			object obj = this.ViewState["_ColCount"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["_ColCount"] = value;
		}
	}
	protected int _GridWidth
	{
		get
		{
			object obj = this.ViewState["_GridWidth"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 300;
		}
		set
		{
			this.ViewState["_GridWidth"] = value;
		}
	}
	protected int _GridHeight
	{
		get
		{
			object obj = this.ViewState["_GridHeight"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 200;
		}
		set
		{
			this.ViewState["_GridHeight"] = value;
		}
	}
	protected int _GridColSpaceWidth
	{
		get
		{
			object obj = this.ViewState["_GridColSpaceWidth"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 240;
		}
		set
		{
			this.ViewState["_GridColSpaceWidth"] = value;
		}
	}
	protected int _GridRowSpaceWidth
	{
		get
		{
			object obj = this.ViewState["_GridRowSpaceWidth"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 20;
		}
		set
		{
			this.ViewState["_GridRowSpaceWidth"] = value;
		}
	}
	protected int _FixRowCount
	{
		get
		{
			object obj = this.ViewState["_FixRowCount"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 240;
		}
		set
		{
			this.ViewState["_FixRowCount"] = value;
		}
	}
	protected string _DeskWidth
	{
		get
		{
			object obj = this.ViewState["_DeskWidth"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "1366";
		}
		set
		{
			this.ViewState["_DeskWidth"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			if (base.Request["deskwidth"] != null && base.Request["deskwidth"].ToString() != "")
			{
				this._DeskWidth = base.Request["deskwidth"].ToString();
			}
			this.Session["deskwidth"] = this._DeskWidth;
			DataTable userSet = this.deskTop.GetUserSet(base.UserCode, this._DeskWidth);
			if (userSet.Rows.Count > 0)
			{
				this._ColCount = Convert.ToInt32(userSet.Rows[0]["GirdColNum"].ToString());
				this._GridWidth = Convert.ToInt32(userSet.Rows[0]["GirdWidth"].ToString());
				this._GridColSpaceWidth = Convert.ToInt32(userSet.Rows[0]["ColGapWidth"].ToString());
				this._GridRowSpaceWidth = Convert.ToInt32(userSet.Rows[0]["RowGapWidth"].ToString());
				this._FixRowCount = Convert.ToInt32(userSet.Rows[0]["RowInGrid"].ToString());
				this._GridHeight = (this._FixRowCount + 2) * 25;
				this.HideNullGrid = userSet.Rows[0]["HideNullGrid"].ToString();
			}
			this.tblData_Draw();
		}
	}
	public void ShowColumn(int width)
	{
		DataTable analysisSource = Progress.GetAnalysisSource();
		if (analysisSource.Rows.Count > 0)
		{
			this.Chart1.Visible = true;
			this.tdProject.Style.Remove("height");
			if (analysisSource.Rows.Count <= 30)
			{
				this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 30.0;
			}
			else
			{
				this.Chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (double)analysisSource.Rows.Count;
			}
			this.Chart1.Width = width;
			for (int i = 0; i < analysisSource.Rows.Count; i++)
			{
				this.Chart1.Series["Default"].Points.AddXY(analysisSource.Rows[i]["PrjName"].ToString(), new object[]
				{
					double.Parse(analysisSource.Rows[i]["PERCENTCOMPLETE_"].ToString()) / 100.0
				});
			}
			return;
		}
		this.Chart1.Visible = false;
		this.tdProject.Style.Add("height", "180px");
	}
	public void UpdateAttrib()
	{
		foreach (Series current in this.Chart1.Series)
		{
			for (int i = 0; i < current.Points.Count; i++)
			{
				string text = current.Points[i].AxisLabel + "的进度完成量为" + (current.Points[i].YValues[0] * 100.0).ToString() + "%";
				current.Points[i].Url = "#";
				current.Points[i].MapAreaAttributes = string.Concat(new string[]
				{
					"onmouseover=\" DisplayTooltip('",
					text,
					"');\" onmouseout=\"DisplayTooltip('');\" onclick=\"query('",
					current.Points[i].AxisLabel,
					"');\""
				});
			}
		}
	}
    private void tblData_Draw()
    {
        DataTable userModelInfo = this.deskTop.GetUserModelInfo(base.UserCode);
        this.tblData.Attributes["width"] = Unit.Pixel(this._ColCount * (this._GridWidth + this._GridColSpaceWidth)).ToString();
        DataRow[] source = userModelInfo.Select("ModelID=780501");
        if (source.Count<DataRow>() > 0)
        {
            userModelInfo.Rows.Remove(source[0]);
            int num = (this._ColCount * this._GridWidth) + ((this._ColCount - 1) * this._GridColSpaceWidth);
            this.divProject.Style.Add("display", "");
            this.divProject.Style.Add("margin-top", (this._GridRowSpaceWidth / 2) + "px");
            int num13 = this._ColCount * (this._GridWidth + this._GridColSpaceWidth);
            this.divProject.Style.Add(" width", num13.ToString() + "px");
            this.tabProject.Style.Add(" width", num.ToString() + "px");
            this.ShowColumn(num - 40);
            this.UpdateAttrib();
        }
        else
        {
            this.divProject.Style.Add("display", "none");
        }
        int num3 = (int)Math.Ceiling((decimal)(userModelInfo.Rows.Count / this._ColCount));
        for (int i = 0; i < num3; i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            for (int j = 0; j < this._ColCount; j++)
            {
                cell = new HtmlTableCell();
                cell.Attributes["align"] = "center";
                cell.Attributes["width"] = this._GridWidth.ToString();
                cell.Attributes["height"] = this._GridHeight.ToString();
                cell.Attributes["wrap"] = "true";
                int num6 = (i * this._ColCount) + j;
                cell.Attributes["tag"] = num6.ToString();
                string text = "";
                if (num6 >= userModelInfo.Rows.Count)
                {
                    goto Label_1208;
                }
                string str2 = "更多";
                string str3 = userModelInfo.Rows[num6][0].ToString();
                string tbName = userModelInfo.Rows[num6][1].ToString();
                string colName = userModelInfo.Rows[num6][2].ToString();
                string colTime = userModelInfo.Rows[num6][3].ToString();
                string selWhere = userModelInfo.Rows[num6][4].ToString();
                string str8 = userModelInfo.Rows[num6][8].ToString();
                string str9 = userModelInfo.Rows[num6][5].ToString();
                string str10 = userModelInfo.Rows[num6][6].ToString();
                string coldId = userModelInfo.Rows[num6][7].ToString();
                DataTable wFInfo = new DataTable();
                switch (str3)
                {
                    case "283818":
                        wFInfo = this.deskTop.GetWFInfo(base.UserCode, this._FixRowCount);
                        goto Label_063E;

                    case "2842":
                        wFInfo = this.deskTop.GetWarningInfo(base.UserCode, this._FixRowCount);
                        goto Label_063E;

                    case "2801":
                        wFInfo = this.deskTop.GetDBInfo(base.UserCode, this._FixRowCount);
                        goto Label_063E;

                    case "381705":
                        wFInfo = this.deskTop.GetWebLinkInfo(base.UserCode, this._FixRowCount);
                        str2 = "设置";
                        goto Label_063E;

                    default:
                        if (((!(str3 == "381703") && !(str3 == "381707")) && (!(str3 == "381708") && !(str3 == "381709"))) && (!(str3 == "381710") && !(str3 == "381711")))
                        {
                            goto Label_05F9;
                        }
                        switch (str3)
                        {
                            case "381703":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "1");
                                goto Label_05F0;

                            case "381707":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "2");
                                goto Label_05F0;

                            case "381708":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "3");
                                goto Label_05F0;

                            case "381709":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "4");
                                goto Label_05F0;

                            case "381710":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "5");
                                goto Label_05F0;

                            case "381711":
                                wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "6");
                                goto Label_05F0;
                        }
                        wFInfo = this.deskTop.GetMenuLinkInfo(base.UserCode, this._FixRowCount, "1");
                        break;
                }
            Label_05F0:
                str2 = "设置";
                goto Label_063E;
            Label_05F9:
                if (str3 == "280305")
                {
                    new BulletionAction();
                    wFInfo = BulletionAction.ReturnTable(base.UserCode, " and dtm_expriesdate>getdate()-1 and auditstate=1");
                }
                else
                {
                    wFInfo = this.deskTop.GetShowInfo(colName, colTime, tbName, selWhere, coldId, this._FixRowCount);
                }
            Label_063E:
                if (wFInfo.Rows.Count != 0)
                {
                    string str24 = string.Concat(new object[] { "<table modelId=\"", str3, "\" width=\"", this._GridWidth, "\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" });
                    text = (str24 + "<tr onmousemove='showDel(\"Img_del" + str3 + "\")'  onmouseout='showDelT(\"Img_del" + str3 + "\")'><td><img src=\"image/bg_01.gif\" style=\"float:left; width:12px;height:25px\" /></td><td background=\"image/bg_02.gif\" class=\"header-b\" style=\"text-align:left;\" > " + str8 + "</td><td><img src=\"image/bg_03.gif\" title=\"隐藏此栏目\" id=\"Img_del" + str3 + "\"  onclick=\"grid_del(" + str3 + ")\"   style=\"float:right;width:12px;height:25px\" /></td></tr>") + "<tr><td background=\"image/bg_11.gif\" width=\"12px\"></td><td valign=\"top\"><div class=\"pagebody\"><table>";
                    int count = wFInfo.Rows.Count;
                    if (this._FixRowCount < wFInfo.Rows.Count)
                    {
                        count = this._FixRowCount;
                    }
                    for (int k = 0; k < count; k++)
                    {
                        string str12 = str10 + wFInfo.Rows[k][2];
                        string str13 = "";
                        string str14 = wFInfo.Rows[k][0].ToString();
                        string str15 = string.Empty;
                        switch (str3)
                        {
                            case "283818":
                                {
                                    if ((wFInfo.Rows[k]["cs"].ToString() != "") && (Convert.ToInt32(wFInfo.Rows[k]["cs"].ToString()) > 0))
                                    {
                                        str13 = "<font color=\"red\">【超时】</font>";
                                    }
                                    string str16 = wFInfo.Rows[k]["InstanceCode"].ToString();
                                    string str17 = wFInfo.Rows[k]["NoteID"].ToString();
                                    string str18 = wFInfo.Rows[k]["IsAllPass"].ToString();
                                    string str19 = wFInfo.Rows[k]["NodeID"].ToString();
                                    string str20 = wFInfo.Rows[k]["BusinessCode"].ToString();
                                    string str21 = wFInfo.Rows[k]["BusinessClass"].ToString();
                                    str12 = "../EPC/Workflow/AuditFrame.aspx?ic=" + str16 + "&id=" + str17 + "&nid=" + str19 + "&pass=" + str18 + "&bc=" + str20 + "&bcl=" + str21;
                                    DataTable table3 = publicDbOpClass.DataTableQuary(" select * from WF_BusinessCode where BusinessCode='" + str20 + "' ");
                                    try
                                    {
                                        DataTable table4 = publicDbOpClass.DataTableQuary(" select  " + table3.Rows[0]["NameField"].ToString() + " from  " + table3.Rows[0]["LinkTable"].ToString() + "  where " + table3.Rows[0]["PrimaryField"].ToString() + " ='" + str16 + "' ");
                                        if (str20 == "089")
                                        {
                                            if (!string.IsNullOrEmpty(table4.Rows[0][0].ToString()))
                                            {
                                                str14 = str14 + "—" + TenderInfo.GetProjectName(table4.Rows[0][0].ToString());
                                            }
                                        }
                                        else if ((str20 == "100") || (str20 == "106"))
                                        {
                                            if (!string.IsNullOrEmpty(table4.Rows[0][0].ToString()))
                                            {
                                                str14 = str14 + "—" + ProjectInfo.GetProjectName(table4.Rows[0][0].ToString());
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(table4.Rows[0][0].ToString()))
                                        {
                                            str14 = str14 + "—" + table4.Rows[0][0].ToString();
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                }
                            case "2801":
                                str12 = "/" + wFInfo.Rows[k]["V_DBLJ"].ToString();
                                str15 = wFInfo.Rows[k]["I_DBSJ_ID"].ToString();
                                break;

                            case "2842":
                                str12 = StringUtility.DealUrl(wFInfo.Rows[k]["URI"].ToString()) + "id=" + wFInfo.Rows[k]["WarningId"].ToString();
                                str15 = wFInfo.Rows[k]["WarningId"].ToString();
                                str14 = wFInfo.Rows[k]["WarningTitle"].ToString();
                                break;

                            case "381705":
                                str12 = wFInfo.Rows[k]["WebAddr"].ToString();
                                break;

                            case "381703":
                            case "381707":
                            case "381708":
                            case "381709":
                            case "381710":
                            case "381711":
                                if (wFInfo.Rows[k]["v_cdlj"].ToString().Substring(0, 1) == "/")
                                {
                                    str12 = ".." + wFInfo.Rows[k]["v_cdlj"].ToString();
                                }
                                else
                                {
                                    str12 = "../" + wFInfo.Rows[k]["v_cdlj"].ToString();
                                }
                                str14 = wFInfo.Rows[k]["MNewName"].ToString();
                                break;

                            default:
                                if (str3 == "280305")
                                {
                                    str9 = "../oa/Bulletin/BulletinManage.aspx?type=see";
                                    str12 = "../oa/bulletin/BulletinLock.aspx?ic=" + wFInfo.Rows[k][0].ToString();
                                    str14 = wFInfo.Rows[k][4].ToString();
                                }
                                break;
                        }
                        int length = (this._GridWidth - 200) / 10;
                        str13 = str13 + ((str14.Length > length) ? (str14.Substring(0, length).ToString() + "...") : str14.ToString());
                        string str22 = "";
                        if (str3 != "280305")
                        {
                            if ((wFInfo.Rows[k][1].ToString().ToString() != "") && (wFInfo.Rows[k][1].ToString() != null))
                            {
                                str22 = wFInfo.Rows[k][1].ToString();
                            }
                        }
                        else if ((wFInfo.Rows[k][7].ToString().ToString() != "") && (wFInfo.Rows[k][7].ToString() != null))
                        {
                            str22 = Convert.ToDateTime(wFInfo.Rows[k][7].ToString()).ToString("yyyy-MM-dd");
                        }
                        if (str3 == "381705")
                        {
                            string str25 = text;
                            text = str25 + "<tr class=\"border-a\"><td align =\"left\" title=\"" + wFInfo.Rows[k][0].ToString() + "\" ><img src=\"image/dian.jpg\" style=\"float:left; width:2px;height:2px;margin:5px 5px 5px 0px; \"  /><a  target=\"_new\"   onclick='OpenWeb(\"" + str12 + "\")'>" + str13 + "</a></td><td width=\"70px\" align =\"left\">" + str22 + "</td></tr>";
                        }
                        else
                        {
                            string str26 = text;
                            text = str26 + "<tr id=\"" + str15 + "\" class=\"border-a\"><td align =\"left\" title=\"" + str14 + "\" ><img src=\"image/dian.jpg\" style=\"float:left; width:2px;height:2px;margin:5px 5px 5px 0px;\" /><a  href='javascript:openTab(\"" + str12 + "\",\"" + ((str14.Length > 10) ? (str14.Substring(0, 10).ToString() + "...") : str14.ToString()) + "\")'>" + str13 + "</a></td><td width=\"70px\" align =\"left\">" + str22 + "</td></tr>";
                        }
                    }
                    if (wFInfo.Rows.Count < this._FixRowCount)
                    {
                        for (int m = 0; m < (this._FixRowCount - wFInfo.Rows.Count); m++)
                        {
                            text = text + "<tr class=\"border-b\"><td>&nbsp;</td><td>&nbsp;</td></tr>";
                        }
                    }
                    string str27 = text + "</table></div></td><td background=\"image/bg_13.gif\" wdith=\"12px\">&nbsp;</td></tr>";
                    text = (str27 + "<tr valign=\"top\"> <td background=\"image/bg_11.gif\" width=\"12px\">&nbsp;</td><td width=\"100%\"><span class=\"readmore\"><a href='javascript:openTab(\"" + str9 + "\",\"" + str8 + "\")'>" + str2 + ">></a></td><td background=\"image/bg_13.gif\" width=\"11px\">&nbsp</td></tr>") + "<tr><td height=\"10px\" valign=\"top\"><img src=\"image/bg_21.gif\" style=\"float:left;width:12px; height:10px;\"/></td><td height=\"10px\" valign=\"top\" background=\"image/bg_22.gif\"></td> <td valign=\"top\"><img src=\"image/bg_23.gif\" style=\"float:left;width:12px; height:10px;\"/></td></tr>" + "</table>";
                }
                else
                {
                    if (str3 == "280305")
                    {
                        str9 = "../oa/Bulletin/BulletinManage.aspx?type=see";
                    }
                    string str28 = "<table width=\"" + this._GridWidth + "\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                    text = (str28 + "<tr onmousemove='showDel(\"Img_del" + str3 + "\")'  onmouseout='showDelT(\"Img_del" + str3 + "\")'><td><img src=\"image/bg_01.gif\" style=\"float:left; width:12px;height:25px\" /></td><td background=\"image/bg_02.gif\" class=\"header-b\" style=\"text-align:left;\" > " + str8 + "</td><td><img src=\"image/bg_03.gif\" title=\"隐藏此栏目\" id=\"Img_del" + str3 + "\"  onclick=\"grid_del(" + str3 + ")\"   style=\"float:right;width:12px;height:25px\" /></td></tr>") + "<tr><td background=\"image/bg_11.gif\" width=\"12px\"></td><td valign=\"top\"><div class=\"pagebody\"><table>";
                    for (int n = 0; n < this._FixRowCount; n++)
                    {
                        text = text + "<tr class=\"border-b\"><td>&nbsp;</td><td>&nbsp;</td></tr>";
                    }
                    string str23 = text + "</table></div></td><td background=\"image/bg_13.gif\" wdith=\"12px\">&nbsp;</td></tr>";
                    text = (str23 + "<tr valign=\"top\"> <td background=\"image/bg_11.gif\" width=\"12px\">&nbsp;</td><td width=\"100%\"><span class=\"readmore\"><a href='javascript:openTab(\"" + str9 + "\",\"" + str8 + "\")'>" + str2 + ">></a></td><td background=\"image/bg_13.gif\" width=\"11px\">&nbsp</td></tr>") + "<tr><td height=\"10px\" valign=\"top\"><img src=\"image/bg_21.gif\" style=\"float:left;width:12px; height:10px;\"/></td><td height=\"10px\" valign=\"top\" background=\"image/bg_22.gif\"></td> <td valign=\"top\"><img src=\"image/bg_23.gif\" style=\"float:left;width:12px; height:10px;\"/></td></tr>" + "</table>";
                }
            Label_1208:
                cell.Controls.Add(new LiteralControl(text));
                row.Cells.Add(cell);
            }
            row.Attributes["height"] = ((this._GridHeight + this._GridRowSpaceWidth) + 15).ToString();
            this.tblData.Rows.Add(row);
        }
    }
	protected void btndel_Click(object sender, EventArgs e)
	{
		DesktopBLL desktopBLL = new DesktopBLL();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			desktopBLL.DeleteModel(base.UserCode, this.hdnmkid.Value.ToString(), sqlTransaction);
			sqlTransaction.Commit();
		}
		this.tblData_Draw();
	}
}


