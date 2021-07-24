using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Calendar_CalendarMain : BasePage, IRequiresSessionState
{
	private int IntYear
	{
		get
		{
			return Convert.ToInt32(this.ViewState["INTYEAR"]);
		}
		set
		{
			this.ViewState["INTYEAR"] = value;
		}
	}
	private int IntMonth
	{
		get
		{
			return Convert.ToInt32(this.ViewState["INTMONTH"]);
		}
		set
		{
			this.ViewState["INTMONTH"] = value;
		}
	}
	private CalendarInfoAction cia
	{
		get
		{
			return new CalendarInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.IntYear = DateTime.Now.Year;
			this.IntMonth = DateTime.Now.Month;
			this.CreateTableRows(this.IntYear, this.IntMonth);
			this.LbYear.Text = this.IntYear.ToString();
			this.LbMonth.Text = this.IntMonth.ToString();
			this.PageDataBind();
			this.frmPage.Attributes["src"] = "CalendarSel.aspx?t=Add&dt=" + DateTime.Now.ToShortDateString();
		}
	}
	private void CreateTableRows(int year, int month)
	{
		int[,] array = new int[7, 7];
		int num = 1;
		int num2 = DateTime.DaysInMonth(year, month);
		int num3 = 1;
		int num4 = 1;
		while (num4 < 7 && num3 != num2)
		{
			for (int i = num; i <= num2; i++)
			{
				string value = string.Concat(new object[]
				{
					year,
					"-",
					month,
					"-",
					i.ToString()
				});
				if (array[num4, Convert.ToInt32(Convert.ToDateTime(value).DayOfWeek.GetHashCode().ToString())] != 0)
				{
					num = i;
					break;
				}
				array[num4, Convert.ToInt32(Convert.ToDateTime(value).DayOfWeek.GetHashCode().ToString())] = i;
				if (Convert.ToInt32(Convert.ToDateTime(value).DayOfWeek.GetHashCode().ToString()) == 6)
				{
					num = i + 1;
					break;
				}
				num3 = i;
			}
			num4++;
		}
		for (int j = 1; j < 7; j++)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			for (int k = 0; k < 7; k++)
			{
				if (array[j, k] != 0)
				{
					string text = string.Concat(new string[]
					{
						this.IntYear.ToString(),
						"-",
						this.IntMonth.ToString(),
						"-",
						array[j, k].ToString()
					});
					HtmlTableCell htmlTableCell = new HtmlTableCell();
					htmlTableCell.InnerText = array[j, k].ToString();
					int num5 = this.RecordNumber(text);
					if (num5 > 0)
					{
						htmlTableCell.InnerHtml = string.Concat(new string[]
						{
							"<A HREF=\"CalendarSel.aspx?t=Add&dt=",
							text,
							"\"  title='请添加事件' CLASS='NOEVENT' target = 'frmPage'><font color='#0000ff' size='3'><b>",
							array[j, k].ToString(),
							"</b></font></a>"
						});
					}
					else
					{
						htmlTableCell.InnerHtml = string.Concat(new string[]
						{
							"<A HREF='javascript:openwin(\"CalendarAdd.aspx\",\"",
							this.Session["yhdm"].ToString(),
							"\",\"",
							text,
							"\",0);'  title='请添加事件' CLASS='NOEVENT'><font color='#0000ff' size='4'><b>",
							array[j, k].ToString(),
							"</b></font></a>"
						});
					}
					htmlTableCell.Align = "center";
					htmlTableCell.VAlign = "middle";
					htmlTableCell.Width = "30px";
					htmlTableCell.Height = "20px";
					htmlTableRow.Cells.Add(htmlTableCell);
					HtmlTableCell htmlTableCell2 = new HtmlTableCell();
					htmlTableCell2.Width = "30px";
					htmlTableCell2.Height = "5px";
					int num6 = 5 * num5;
					if (num5 > 0)
					{
						htmlTableCell2.InnerHtml = "<img src='images/red.gif' width='" + num6 + "px' height='5px' border='0'>";
					}
					htmlTableRow2.Cells.Add(htmlTableCell2);
				}
				else
				{
					HtmlTableCell htmlTableCell3 = new HtmlTableCell();
					htmlTableCell3.InnerText = "";
					htmlTableCell3.Align = "center";
					htmlTableCell3.VAlign = "middle";
					htmlTableCell3.Width = "30px";
					htmlTableCell3.Height = "20px";
					htmlTableRow.Cells.Add(htmlTableCell3);
					HtmlTableCell htmlTableCell4 = new HtmlTableCell();
					htmlTableCell4.Width = "30px";
					htmlTableCell4.Height = "5px";
					htmlTableRow2.Cells.Add(htmlTableCell4);
				}
			}
			htmlTableRow.BgColor = "#eeeeee";
			this.tbDay.Rows.Add(htmlTableRow);
			htmlTableRow2.BgColor = "#eeeeee";
			this.tbDay.Rows.Add(htmlTableRow2);
		}
	}
	protected void ImbMonth_Click(object sender, ImageClickEventArgs e)
	{
		this.IntMonth--;
		if (this.IntMonth == 0)
		{
			this.IntMonth = 1;
		}
		this.CreateTableRows(this.IntYear, this.IntMonth);
		this.LbMonth.Text = Convert.ToString(this.IntMonth);
		this.LbYear.Text = Convert.ToString(this.IntYear);
	}
	protected void ImbYear_Click(object sender, ImageClickEventArgs e)
	{
		this.IntYear--;
		this.CreateTableRows(this.IntYear, this.IntMonth);
		this.LbYear.Text = Convert.ToString(this.IntYear);
	}
	protected void ImbYearAdd_Click(object sender, ImageClickEventArgs e)
	{
		this.IntYear++;
		this.CreateTableRows(this.IntYear, this.IntMonth);
		this.LbYear.Text = Convert.ToString(this.IntYear);
	}
	protected void ImbMonthAdd_Click(object sender, ImageClickEventArgs e)
	{
		this.IntMonth++;
		if (this.IntMonth == 13)
		{
			this.IntMonth = 1;
		}
		this.CreateTableRows(this.IntYear, this.IntMonth);
		this.LbMonth.Text = Convert.ToString(this.IntMonth);
		this.LbYear.Text = Convert.ToString(this.IntYear);
	}
	private void PageDataBind()
	{
		int year = DateTime.Now.Year;
		for (int i = year - 3; i <= year + 5; i++)
		{
			this.DDLyear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
		}
		this.DDLyear.SelectedValue = year.ToString();
	}
	private int RecordNumber(string tjsj)
	{
		DataTable list = this.cia.GetList(string.Concat(new string[]
		{
			"datediff(dd,RecordDate,'",
			tjsj,
			" ')=0 and UserCode = '",
			this.Session["yhdm"].ToString(),
			"'"
		}));
		return list.Rows.Count;
	}
	protected void Imbgo_Click(object sender, ImageClickEventArgs e)
	{
		this.IntYear = Convert.ToInt32(this.DDLyear.SelectedValue);
		this.IntMonth = Convert.ToInt32(this.DDLMonth.SelectedValue);
		this.CreateTableRows(this.IntYear, this.IntMonth);
		this.LbMonth.Text = this.IntMonth.ToString();
		this.LbYear.Text = this.IntYear.ToString();
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			HyperLink hyperLink = (HyperLink)e.Row.Cells[0].FindControl("HLtitle");
			hyperLink.NavigateUrl = "javascript:void(0);";
			hyperLink.Attributes["onclick"] = "openCalendarView('" + dataRowView["RecordID"].ToString() + "')";
		}
	}
}


