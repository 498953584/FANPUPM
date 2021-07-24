using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DeptSet : PageBase, IRequiresSessionState
	{
		protected int code;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["sid"]))
			{
				this.code = Convert.ToInt32(base.Request["sid"].ToString());
			}
			if (!base.IsPostBack)
			{
				string text = this.Session["yhdm"].ToString();
				if (text.Length != 0)
				{
					Table table = this.CreateNodeDept((this.code == 0) ? 1 : this.code, false);
					if (table.Rows.Count > 0)
					{
						this.Controls.Add(table);
					}
				}
			}
		}
		private Table CreateNodeDept(int iDeptCode, bool isTrue)
		{
			Table table = new Table();
			table.Attributes["align"] = "center";
			Table table2 = new Table();
			DeptManageDb deptManageDb = new DeptManageDb();
			DataTable subDepartment = deptManageDb.GetSubDepartment1(iDeptCode, isTrue);
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			if (subDepartment.Rows.Count > 0)
			{
				for (int i = 0; i < subDepartment.Rows.Count; i++)
				{
					DataRow deptDRow = subDepartment.Rows[i];
					if (iDeptCode == 0)
					{
						table2 = this.CreateDeptTable1(deptDRow, false);
						if (table2.Rows.Count > 0)
						{
							tableCell.Controls.Add(table2);
							tableCell.HorizontalAlign = HorizontalAlign.Center;
						}
						else
						{
							tableCell.Text = "";
						}
						tableRow.Cells.Add(tableCell);
						table.Rows.Add(tableRow);
					}
					else
					{
						if (subDepartment.Rows.Count == 1)
						{
							tableRow = new TableRow();
							tableCell = new TableCell();
							table2 = this.CreateDeptTable1(deptDRow, false);
							if (table2.Rows.Count > 0)
							{
								table2.CellSpacing = 0;
								table2.CellPadding = 0;
								table2.BorderWidth = 0;
								tableCell.Controls.Add(table2);
							}
							else
							{
								tableCell.Text = "&nbsp;";
							}
							tableRow.Cells.Add(tableCell);
							table.Rows.Add(tableRow);
						}
						else
						{
							if (i == 0)
							{
								tableRow = new TableRow();
								tableCell = new TableCell();
								tableCell.Text = "<TABLE border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\"><TR><TD><Img src=\"Images/spacer.gif\" border=\"0\"></TD><TD background=\"Images/dd.bmp\" width=\"3\"></TD><TD background=\"Images/top_line.gif\"><Img src=\"Images/spacer.gif\" border=\"0\"></TD></TR></TABLE>";
								tableRow.Cells.Add(tableCell);
								tableCell = new TableCell();
								tableCell.Text = "<TABLE border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\"><TR><TD background=\"Images/top_line.gif\"></TD></TR></TABLE>";
								tableCell.ColumnSpan = (subDepartment.Rows.Count - 2) * 2 + 1;
								tableRow.Cells.Add(tableCell);
								tableCell = new TableCell();
								tableCell.Text = "<TABLE border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\"><TR><TD background=\"Images/top_line.gif\"><Img src=\"Images/spacer.gif\" border=\"0\"></TD><TD background=\"Images/dd.bmp\" width=\"3\"></TD><TD><Img src=\"Images/spacer.gif\" border=\"0\"></TD></TR></TABLE>";
								tableRow.Attributes["align"] = "center";
								tableRow.Attributes["valign"] = "top";
								tableRow.Cells.Add(tableCell);
								tableRow.Height = 3;
								table.Rows.Add(tableRow);
								tableRow = new TableRow();
								tableCell = new TableCell();
								table2 = this.CreateDeptTable1(deptDRow, true);
								if (table2.Rows.Count > 0)
								{
									table2.CellSpacing = 0;
									table2.CellPadding = 0;
									table2.BorderWidth = 0;
									tableCell.Controls.Add(table2);
								}
								else
								{
									tableCell.Text = "&nbsp;";
								}
								tableRow.Cells.Add(tableCell);
							}
							else
							{
								if (i == subDepartment.Rows.Count - 1)
								{
									tableCell = new TableCell();
									tableCell.Text = "&nbsp;";
									tableCell.Width = 10;
									tableRow.Cells.Add(tableCell);
									tableCell = new TableCell();
									table2 = this.CreateDeptTable1(deptDRow, true);
									if (table2.Rows.Count > 0)
									{
										table2.CellSpacing = 0;
										table2.CellPadding = 0;
										table2.BorderWidth = 0;
										tableCell.Controls.Add(table2);
									}
									else
									{
										tableCell.Text = "&nbsp;";
									}
									tableRow.Cells.Add(tableCell);
									table.Rows.Add(tableRow);
								}
								else
								{
									tableCell = new TableCell();
									tableCell.Text = "&nbsp;";
									tableCell.Width = 10;
									tableRow.Cells.Add(tableCell);
									tableCell = new TableCell();
									table2 = this.CreateDeptTable1(deptDRow, true);
									if (table2.Rows.Count > 0)
									{
										table2.CellSpacing = 0;
										table2.CellPadding = 0;
										table2.BorderWidth = 0;
										tableCell.Controls.Add(table2);
									}
									else
									{
										tableCell.Text = "&nbsp;";
									}
									tableRow.Cells.Add(tableCell);
								}
							}
						}
					}
				}
			}
			table.CellSpacing = 0;
			table.CellPadding = 0;
			table.BorderWidth = 0;
			return table;
		}
		private Table CreateDeptTable1(DataRow deptDRow, bool isTrue)
		{
			Table table = new Table();
			Table table2 = new Table();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			if (Convert.ToInt32(deptDRow["i_sjdm"].ToString()) != 0 && isTrue)
			{
				tableCell.Text = "<TABLE border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><TR><TD><Img src=\"Images/spacer.gif\" border=\"0\"></TD><TD width=\"19\"><Img src=\"Images/line_top.gif\" border=\"0\"></TD><TD><Img src=\"Images/spacer.gif\" border=\"0\"></TD></TR></TABLE>";
				tableRow.Cells.Add(tableCell);
				tableRow.Attributes["valign"] = "top";
				table.Rows.Add(tableRow);
			}
			tableCell = new TableCell();
			tableRow = new TableRow();
			tableCell.Text = string.Concat(new string[]
			{
				"<TABLE border=\"1\" style=\"border:solid 1px\" cellpadding=\"2\" cellspacing=\"0\" align=\"center\"><TR><TD style=\"color:#333333;background-color:#f6f6ff;cursor:hand\" onmouseover=\"mouseOver(this);\" onmouseout=\"mouseOut(this);\" onclick=\"showDept(",
				deptDRow["i_bmdm"].ToString().Trim(),
				");\" align=\"center\">",
				deptDRow["v_bmmc"].ToString(),
				"</TD></TR></TABLE>"
			});
			tableCell.ColumnSpan = Convert.ToInt32(deptDRow["i_xjbm"].ToString());
			tableRow.Attributes["align"] = "center";
			tableRow.Attributes["valign"] = "bottom";
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			if (Convert.ToInt32(deptDRow["i_xjbm"].ToString()) != 0)
			{
				tableRow = new TableRow();
				tableCell = new TableCell();
				tableCell.Text = "<Img src=\"Images/line_top.gif\">";
				tableCell.ColumnSpan = Convert.ToInt32(deptDRow["i_xjbm"].ToString());
				tableRow.Attributes["align"] = "center";
				tableRow.Attributes["valign"] = "top";
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
				tableRow = new TableRow();
				tableCell = new TableCell();
				table2 = this.CreateNodeDept(Convert.ToInt32(deptDRow["i_bmdm"].ToString()), true);
				if (table2.Rows.Count > 0)
				{
					table2.BorderWidth = 0;
					table2.CellSpacing = 0;
					table2.CellPadding = 0;
					tableCell.Controls.Add(table2);
				}
				else
				{
					tableCell.Text = "&nbsp;";
				}
				tableRow.Cells.Add(tableCell);
				table.Rows.Add(tableRow);
			}
			table.CellSpacing = 0;
			table.CellPadding = 0;
			table.BorderWidth = 0;
			table.Width = Unit.Percentage(100.0);
			table.Height = Unit.Percentage(100.0);
			return table;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


